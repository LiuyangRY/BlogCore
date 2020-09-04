using System;
using System.Data;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SqlSugar;
using Blog.Core.Model.Models;

namespace Blog.Core.Repository.Base
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// 根据主键查询
        /// </summary>
        /// <param name="objId">主键</param>
        /// <returns>查询结果</returns>
        Task<TEntity> QueryByID(object objId);

        /// <summary>
        /// 根据主键查询
        /// </summary>
        /// <param name="objId">主键</param>
        /// <param name="useCache">是否使用缓存（默认不使用）</param>
        /// <returns>查询结果</returns>
        Task<TEntity> QueryByID(object objId, bool useCache = false);

        /// <summary>
        /// 根据多个主键查询结果集
        /// </summary>
        /// <param name="objIds">主键数组</param>
        /// <returns>查询结果集</returns>
        Task<IEnumerable<TEntity>> QueryByIDs(object[] objIds);

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model">模型</param>
        /// <returns>新增数量</returns>
        Task<int> Add(TEntity model);

        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="models">模型集合</param>
        /// <returns>新增数量</returns>
        Task<int> AddRange(IEnumerable<TEntity> models);

        /// <summary>
        /// 根据主键删除
        /// </summary>
        /// <param name="objId">主键</param>
        /// <returns>删除的数量</returns>
        Task<int> DeleteByID(object objId);

        /// <summary>
        /// 根据实体删除
        /// </summary>
        /// <param name="model">需要删除的实体</param>
        /// <returns>删除的数量</returns>
        Task<int> Delete(TEntity model);

        /// <summary>
        /// 根据多个主键删除
        /// </summary>
        /// <param name="objIds">主键数组</param>
        /// <returns>删除的数量</returns>
        Task<int> DeleteByIDs(object[] objIds);

        /// <summary>
        /// 根据实体更新
        /// </summary>
        /// <param name="model">更新实体</param>
        /// <returns>更新的数量</returns>
        Task<int> Update(TEntity model);

        /// <summary>
        /// 根据实体和条件更新
        /// </summary>
        /// <param name="model">更新实体</param>
        /// <param name="where">更新条件</param>
        /// <returns>更新的数量</returns>
        Task<int> Update(TEntity model, string where);

        /// <summary>
        /// 更新匿名对象
        /// </summary>
        /// <param name="operateAnonymousObjects">匿名对象</param>
        /// <returns>更新的数量</returns>
        Task<int> Update(object operateAnonymousObjects);

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="model">实体</param>
        /// <param name="columns">更新列（默认为null）</param>
        /// <param name="ignoreColumn">忽略列（默认为null）</param>
        /// <param name="where">更新条件（默认为null）</param>
        /// <returns>更新的数量</returns>
        Task<int> Update(TEntity model, string[] columns = null, string[] ignoreColumn = null, string where = null);

        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns>所有实体</returns>
        Task<IEnumerable<TEntity>> Query();

        /// <summary>
        /// 根据条件查询
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns>符合条件的结果集</returns>
        Task<IEnumerable<TEntity>> Query(string where);

        /// <summary>
        /// 根据条件查询
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <returns>符合条件的结果集</returns>
        Task<IEnumerable<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression);

        /// <summary>
        /// 根据条件查询并排序
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="orderFields">排序字段（多个字段间用“,”进行分隔（英文字符））</param>
        /// <returns>符合条件的排序结果集</returns>
        Task<IEnumerable<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, string orderFields);

        /// <summary>
        /// 根据条件和排序规则查询
        /// </summary>
        /// <param name="whereExpression">查询条件</param>
        /// <param name="orderExpression">排序规则</param>
        /// <param name="isAsc">是否升序（默认升序）</param>
        /// <returns>符合条件的排序结果集</returns>
        Task<IEnumerable<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, object>> orderExpression, bool isAsc = true);

        /// <summary>
        /// 根据条件查询，并按指定字段排序
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <param name="orderFields">排序字段（多个字段间用“,”进行分隔（英文字符））</param>
        /// <returns>符合条件的排序结果集</returns>
        Task<IEnumerable<TEntity>> Query(string where, string orderFields);

        /// <summary>
        /// 根据条件查询，并根据指定字段排序后取指定数量结果集
        /// </summary>
        /// <param name="whereExpression">查询条件</param>
        /// <param name="top">指定查询数量</param>
        /// <param name="orderFields">排序字段（多个字段间用“,”进行分隔（英文字符））</param>
        /// <returns>符合条件的指定数量排序结果集</returns>
        Task<IEnumerable<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, int top, string orderFields);

        /// <summary>
        /// 根据条件查询，并根据指定字段排序后取指定数量结果集
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <param name="top">指定查询数量</param>
        /// <param name="orderFields">排序字段（多个字段间用“,”进行分隔（英文字符））</param>
        /// <returns>符合条件的指定数量排序结果集</returns>
        Task<IEnumerable<TEntity>> Query(string where, int top, string orderFields);

        /// <summary>
        /// 根据Sql语句进行查询
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="parameters">参数</param>
        /// <returns>查询结果集</returns>
        Task<IEnumerable<TEntity>> QuerySql(string sql, SugarParameter[] parameters = null);

        /// <summary>
        /// 根据Sql语句查询DataTable结果集
        /// </summary>
        /// <param name="sql">Sql语句</param>
        /// <param name="parameters">参数</param>
        /// <returns>DataTable结果集</returns>
        Task<DataTable> QueryTable(string sql, SugarParameter[] parameters = null);

        /// <summary>
        /// 根据条件查询，并按指定字段排序后取分页结果集
        /// </summary>
        /// <param name="whereExpression">查询条件</param>
        /// <param name="pageIndex">分页页码索引</param>
        /// <param name="pageSize">页面数据数量</param>
        /// <param name="orderFields">排序字段</param>
        /// <returns>符合条件按指定字段排序的分页结果集</returns>
        Task<IEnumerable<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, int pageIndex, int pageSize, string orderFields);

        /// <summary>
        /// 根据条件查询，并按指定字段排序后取分页结果集
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <param name="pageIndex">分页页码索引</param>
        /// <param name="pageSize">页面数据数量</param>
        /// <param name="orderFields">排序字段（多个字段间用“,”进行分隔（英文字符））</param>
        /// <returns>符合条件按指定字段排序的分页结果集</returns>
        Task<IEnumerable<TEntity>> Query(string where, int pageIndex, int pageSize, string orderFields);

        /// <summary>
        /// 根据条件查询，并按指定字段排序后取分页结果集
        /// </summary>
        /// <param name="whereExpression">查询条件</param>
        /// <param name="pageIndex">分页页码索引（默认为0）</param>
        /// <param name="pageSize">页面数据数量（默认为20）</param>
        /// <param name="orderFields">排序字段（多个字段间用“,”进行分隔（英文字符））（默认为null）</param>
        /// <returns>符合条件按指定字段排序的分页结果集</returns>
        Task<PageModel<TEntity>> QueryPage(Expression<Func<TEntity, bool>> whereExpression, int pageIndex = 0, int pageSize = 20, string orderFields = null);

        /// <summary>
        /// 多表查询
        /// </summary>
        /// <param name="joinExpression">连接表达式</param>
        /// <param name="selectExpression">选择表达式</param>
        /// <param name="whereExpression">条件表达式</param>
        /// <typeparam name="T1">泛型类型</typeparam>
        /// <typeparam name="T2">泛型类型</typeparam>
        /// <typeparam name="T3">泛型类型</typeparam>
        /// <typeparam name="TResult">泛型类型</typeparam>
        /// <returns>多表查询结果集</returns>
        Task<IEnumerable<TResult>> QueryMuch<T1, T2, T3, TResult>(Expression<Func<T1, T2, T3, object[]>> joinExpression, Expression<Func<T1, T2, T3, TResult>> selectExpression, Expression<Func<T1, T2, T3, bool>> whereExpression) where T1 : class, new();
    }
}