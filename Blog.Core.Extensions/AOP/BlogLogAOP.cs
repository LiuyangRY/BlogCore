using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Castle.DynamicProxy;
using Newtonsoft.Json;

namespace Blog.Core.Extensions.AOP
{
    /// <summary>
    /// 日志拦截器
    /// </summary>
    public class BlogLogAOP : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            // 记录被拦截方法信息的日志信息
            var interceptorInfo = "" +
                $"【当前执行方法】：{ invocation.Method.Name} \r\n" +
                $"【携带的参数有】： {string.Join(", ", invocation.Arguments.Select(a => (a ?? "").ToString()).ToArray())} \r\n";

            try
            {
                // 在被拦截的方法执行完毕后 继续执行当前方法，注意是被拦截的是异步的
                invocation.Proceed();

                // 异步获取异常，先执行
                if (IsAsyncMethod(invocation.Method))
                {
                    #region 方案一
                    // Wait task execution and modify return value
                    if (invocation.Method.ReturnType == typeof(Task))
                    {
                        invocation.ReturnValue = InternalAsyncHelper.AwaitTaskWithPostActionAndFinally(
                            (Task)invocation.ReturnValue,
                            async () => await SuccessAction(invocation, interceptorInfo),/*成功时执行*/
                            ex =>
                            {
                                System.Console.WriteLine(ex.Message);
                            });
                    }
                    // Task<TResult>
                    else
                    {
                        invocation.ReturnValue = InternalAsyncHelper.CallAwaitTaskWithPostActionAndFinallyAndGetResult(
                         invocation.Method.ReturnType.GenericTypeArguments[0],
                         invocation.ReturnValue,
                          async (o) => await SuccessAction(invocation, interceptorInfo, o),/*成功时执行*/
                         ex =>
                         {
                             System.Console.WriteLine(ex);
                         });
                    }
                    #endregion

                    // 如果方案一不行，试试这个方案
                    #region 方案二
                    //var type = invocation.Method.ReturnType;
                    //var resultProperty = type.GetProperty("Result");
                    //dataIntercept += ($"【执行完成结果】：{JsonConvert.SerializeObject(resultProperty.GetValue(invocation.ReturnValue))}");

                    //Parallel.For(0, 1, e =>
                    //{
                    //    LogLock.OutSql2Log("AOPLog", new string[] { dataIntercept });
                    //});
                    #endregion
                }
                else
                {
                    // 同步1
                    interceptorInfo += ($"【执行完成结果】：{invocation.ReturnValue}");
                    Parallel.For(0, 1, e =>
                    {
                        System.Console.WriteLine(interceptorInfo);
                    });
                }
            }
            catch (Exception ex)// 同步2
            {
                System.Console.WriteLine(ex.Message + interceptorInfo);
            }
        }

        public static bool IsAsyncMethod(MethodInfo method)
        {
            return (
                method.ReturnType == typeof(Task) ||
                (method.ReturnType.IsGenericType && method.ReturnType.GetGenericTypeDefinition() == typeof(Task<>))
            );
        }

        private async Task SuccessAction(IInvocation invocation, string interceptorInfo, object o = null)
        {
            interceptorInfo += ($"【执行完成结果】：{JsonConvert.SerializeObject(o)}");

            await Task.Run(() =>
            {
                Parallel.For(0, 1, e =>
                {
                    System.Console.WriteLine(interceptorInfo);
                });
            });
        }

        internal static class InternalAsyncHelper
        {
            public static async Task AwaitTaskWithPostActionAndFinally(Task actualReturnValue, Func<Task> postAction, Action<Exception> finalAction)
            {
                Exception exception = null;
                try
                {
                    await actualReturnValue;
                    await postAction();
                }
                catch(Exception ex)
                {
                    exception = ex;
                }
                finally
                {
                    finalAction(exception);
                }
            }

            public static async Task<T> AwaitTaskWithPostActionAndFinallyAndGetResult<T>(Task<T> actualReturnValue, Func<object, Task> postAction, Action<Exception> finalAction)
            {
                Exception exception = null;
                try
                {
                    var result = await actualReturnValue;
                    await postAction(result);
                    return result;
                }
                catch(Exception ex)
                {
                    exception = ex;
                    throw ex;
                }
                finally
                {
                    finalAction(exception);
                }
            }

            public static object CallAwaitTaskWithPostActionAndFinallyAndGetResult(Type taskReturnType, object actualReturnValue, Func<object, Task> postAction, Action<Exception> finalAction)
            {
                return typeof(InternalAsyncHelper)
                    .GetMethod("AwaitTaskWithPostActionAndFinallyAndGetResult", BindingFlags.Public | BindingFlags.Static)
                    .MakeGenericMethod(taskReturnType)
                    .Invoke(null, new object[] { actualReturnValue, postAction, finalAction });
            }
        }
    }
}