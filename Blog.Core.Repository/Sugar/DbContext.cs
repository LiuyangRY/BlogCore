using System;
using SqlSugar;

namespace Blog.Core.Repository.Sugar
{
    public class DbContext
    {
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public static string ConnectionString { get; set; } = BaseDBConfig.ConnectString;

        /// <summary>
        /// 数据库类型
        /// </summary>
        public static DbType DbType { get; set; }

        /// <summary>
        /// 数据库连接对象
        /// </summary>
        public SqlSugarClient Db { get; set; }

        /// <summary>
        /// 数据库上下文实例
        /// </summary>
        public static DbContext context 
        { 
            get
            {
                return new DbContext();
            } 
        }

        /// <summary>
        /// DbContext构造函数
        /// </summary>
        /// <param name="isAutoCloseConnection">是否自动关闭连接（默认自动关闭）</param>
        public DbContext(bool isAutoCloseConnection = true)
        {
            Db = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = ConnectionString,
                DbType = DbType,
                IsAutoCloseConnection = isAutoCloseConnection,
                IsShardSameThread = true,
                ConfigureExternalServices = new ConfigureExternalServices()
                {
                    
                },
                MoreSettings = new ConnMoreSettings()
                {
                    IsAutoRemoveDataCache = true
                }
            });
        }

        #region 实例方法
            /// <summary>
            /// 获取数据库处理对象
            /// </summary>
            /// <typeparam name="T">实体类型数据库处理对象</typeparam>
            public SimpleClient<T> GetEntityDB<T>() where T : class, new()
            {
                return new SimpleClient<T>(Db);
            }

            /// <summary>
            /// 获取数据库处理对象
            /// </summary>
            /// <typeparam name="T">实体类型数据库处理对象</typeparam>
            public SimpleClient<T> GetEntityDB<T>(SqlSugarClient client) where T : class, new()
            {
                return new SimpleClient<T>(client);
            }

            #region 根据数据库表生产实体类
                /// <summary>
                /// 根据数据库表生成模型
                /// </summary>
                /// <param name="path">生成路径</param>
                /// <param name="space">命名空间</param>
                public void CreateClassFileFromDBTable(string path, string space)
                {
                    CreateClassFileFromDBTable(path, space, null);
                }

                /// <summary>
                /// 根据数据库表生成模型
                /// </summary>
                /// <param name="path">生成路径</param>
                /// <param name="space">命名空间</param>
                /// <param name="tableNames">表名</param>
                public void CreateClassFileFromDBTable(string path, string space, string[] tableNames)
                {
                    CreateClassFileFromDBTable(path, space, tableNames, string.Empty);
                }

                /// <summary>
                /// 根据数据库表生成模型
                /// </summary>
                /// <param name="path">生成路径</param>
                /// <param name="space">命名空间</param>
                /// <param name="tableNames">表名</param>
                /// <param name="interfaceName">接口名</param>
                /// <param name="serializable">是否可以序列化（默认为否）</param>
                public void CreateClassFileFromDBTable(string path, string space, string[] tableNames, string interfaceName, bool serializable = false)
                {
                    if(tableNames != null && tableNames.Length > 0)
                    {
                        Db.DbFirst.Where(tableNames)
                            .IsCreateDefaultValue()
                            .IsCreateAttribute()
                            .SettingClassTemplate(t => t = @"
                                {using} namespace {Namespace}
                                {
                                    {ClassDescription}{SugarTable}" + (serializable ? "[Serializable]" : "") + @"
                                    public partial class {ClassName}" + (string.IsNullOrWhiteSpace(interfaceName) ? "" : (" : " + interfaceName)) + @"
                                    { 
                                        public {ClassName}()
                                        {
                                            {Constructor}
                                        }

                                        {PropertyName}
                                    }
                                }
                            ")
                            .SettingPropertyTemplate(t => t = @"
                                {SugarColumn}
                                public {PropertyType} {PropertyName}
                                {
                                    get
                                    {
                                        return _{PropertyName};
                                    }
                                    set
                                    {
                                        if(_{PropertyName} != value)
                                        {
                                            base.SetValueCall(" + "\"{PropertyName}\", _{PropertyName}" + @");
                                        }
                                        _{PropertyName} = value;
                                    }
                                }
                            ")
                            .SettingPropertyDescriptionTemplate(t => t = @"
                                private {PropertyType} _{PropertyName};\r\n" + t)
                            .SettingConstructorTemplate(t => t = @"
                                this._{PropertyName} = {DefaultValue};")
                            .CreateClassFile(path, space);
                    }
                }
            #endregion

            #region 根据实体类生成数据库表
                /// <summary>
                /// 根据实体类生成数据库表
                /// </summary>
                /// <param name="isBackupTable">是否生成备份表</param>
                /// <param name="entities">指定实体</param>
                public void CreateTableFromEntity(bool isBackupTable, params Type[] entities)
                {
                   if(isBackupTable)
                   {
                       Db.CodeFirst.BackupTable().InitTables(entities);
                   } 
                   else
                   {
                       Db.CodeFirst.InitTables(entities);
                   }
                }
            #endregion
            
            #region 静态方法
                /// <summary>
                /// 获得DbContext
                /// </summary>
                /// <param name="isAutoCloseConnection">是否自动关闭连接</param>
                /// <returns>DbContext实例</returns>
                public static DbContext GetContext(bool isAutoCloseConnection = true)
                {
                    return new DbContext(isAutoCloseConnection);
                }

                /// <summary>
                /// 设置初始化参数
                /// </summary>
                /// <param name="connectionString">数据库连接字符串</param>
                /// <param name="type">数据库类型</param>
                public static void Init(string connectionString, DbType type = SqlSugar.DbType.SqlServer)
                {
                    ConnectionString = connectionString;
                    DbType = type;
                }

                /// <summary>
                /// 获取数据库连接配置
                /// </summary>
                /// <param name="isAutoCloseConnection">是否自动断开连接（默认为是）</param>
                /// <param name="isShardSameThread">相同线程是否共享连接（默认为否）</param>
                /// <returns>数据库连接配置</returns>
                public static ConnectionConfig GetConnectionConfig(bool isAutoCloseConnection = true, bool isShardSameThread = false)
                {
                    return new ConnectionConfig()
                    {
                        ConnectionString = ConnectionString,
                        DbType = DbType,
                        IsAutoCloseConnection = isAutoCloseConnection,
                        ConfigureExternalServices = new ConfigureExternalServices()
                        {

                        },
                        IsShardSameThread = isShardSameThread
                    };
                }

                /// <summary>
                /// 获取一个自定义的SugarClient
                /// </summary>
                /// <param name="config">连接配置</param>
                /// <returns>SugarClient</returns>
                public static SqlSugarClient GetSugarClient(ConnectionConfig config)
                {
                    return new SqlSugarClient(config);
                }

                /// <summary>
                /// 获取一个自定义的数据库处理对象
                /// </summary>
                /// <typeparam name="T">实体类型</typeparam>
                public static SimpleClient<T> GetCustomEntityDB<T>(SqlSugarClient client) where T : class, new()
                {
                    return new SimpleClient<T>(client);
                }

                /// <summary>
                /// 获取一个自定义的数据库处理对象
                /// </summary>
                /// <typeparam name="T">实体类型</typeparam>
                /// <param name="config">连接配置</param>
                public static SimpleClient<T> GetCustomEntityDB<T>(ConnectionConfig config) where T : class, new()
                {
                    SqlSugarClient client = GetSugarClient(config);
                    return new SimpleClient<T>(client);
                }
            #endregion
        #endregion
    }
}