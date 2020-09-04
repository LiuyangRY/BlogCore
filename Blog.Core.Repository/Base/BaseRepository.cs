using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Blog.Core.Model.Models;
using Blog.Core.Repository.Sugar;
using SqlSugar;

namespace Blog.Core.Repository.Base
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, new()
    {
        /// <summary>
        /// 数据库上下文
        /// </summary>
        public DbContext Context { get; set; }

        /// <summary>
        /// 数据库连接对象
        /// </summary>
        /// <value></value>
        internal SqlSugarClient Db { get; private set; }

        /// <summary>
        /// 数据库处理实体对象
        /// </summary>
        /// <value></value>
        internal SimpleClient<TEntity> entityDb { get; private set; }

        public BaseRepository()
        {
            DbContext.Init(BaseDBConfig.ConnectString);
            Context = DbContext.GetContext();
            Db = Context.Db;
            entityDb = Context.GetEntityDB<TEntity>(Db);
        }

        public async Task<int> Add(TEntity model)
        {
            return await Db.Insertable(model)
                .ExecuteCommandAsync();
        }

        public async Task<int> Delete(TEntity model)
        {
            return await Db.Deleteable<TEntity>(model)
                .ExecuteCommandAsync();
        }

        public async Task<int> DeleteByID(object objId)
        {
            return await Db.Deleteable<TEntity>(objId)
                .ExecuteCommandAsync();
        }

        public async Task<int> DeleteByIDs(object[] objIds)
        {
            return await Db.Deleteable<TEntity>()
                .In(objIds)
                .ExecuteCommandAsync();
        }

        public async Task<IEnumerable<TEntity>> Query()
        {
            return await Db.Queryable<TEntity>()
                .ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> Query(string where)
        {
            return await Db.Queryable<TEntity>()
                .WhereIF(string.IsNullOrWhiteSpace(where), where)
                .ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression)
        {
            return await Db.Queryable<TEntity>()
                .WhereIF(whereExpression != null, whereExpression)
                .ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, string orderFields)
        {
            return await Db.Queryable<TEntity>()
                .WhereIF(whereExpression != null, whereExpression)
                .OrderByIF(string.IsNullOrWhiteSpace(orderFields), orderFields)
                .ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, object>> orderExpression, bool isAsc = true)
        {
            return await Db.Queryable<TEntity>()
                .WhereIF(whereExpression != null, whereExpression)
                .OrderByIF(orderExpression != null, orderExpression, isAsc ? OrderByType.Asc : OrderByType.Desc)
                .ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> Query(string where, string orderFields)
        {
            return await Db.Queryable<TEntity>()
                .WhereIF(string.IsNullOrWhiteSpace(where), where)
                .OrderByIF(string.IsNullOrWhiteSpace(orderFields), orderFields)
                .ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, int top, string orderFields)
        {
            return await Db.Queryable<TEntity>()
                .WhereIF(whereExpression != null, whereExpression)
                .OrderByIF(string.IsNullOrWhiteSpace(orderFields), orderFields)
                .Take(top)
                .ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> Query(string where, int top, string orderFields)
        {
            return await Db.Queryable<TEntity>()
                .WhereIF(string.IsNullOrWhiteSpace(where), where)
                .OrderByIF(string.IsNullOrWhiteSpace(orderFields), orderFields)
                .Take(top)
                .ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> Query(string where, int pageIndex, int pageSize, string orderFields)
        {
            return await Db.Queryable<TEntity>()
                .WhereIF(string.IsNullOrWhiteSpace(where), where)
                .OrderByIF(string.IsNullOrWhiteSpace(orderFields), orderFields)
                .ToPageListAsync(pageIndex, pageSize);
        }

        public async Task<IEnumerable<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, int pageIndex, int pageSize, string orderFields)
        {
            return await Db.Queryable<TEntity>()
                .WhereIF(whereExpression != null, whereExpression)
                .OrderByIF(string.IsNullOrWhiteSpace(orderFields), orderFields)
                .ToPageListAsync(pageIndex, pageSize);
        }

        public async Task<TEntity> QueryByID(object objId)
        {
            return await Db.Queryable<TEntity>()
                .InSingleAsync(objId);
        }

        public async Task<TEntity> QueryByID(object objId, bool useCache = false)
        {
            return await Db.Queryable<TEntity>()
                .WithCacheIF(useCache)
                .InSingleAsync(objId);
        }

        public async Task<IEnumerable<TEntity>> QueryByIDs(object[] objIds)
        {
            return await Db.Queryable<TEntity>()
                .In(objIds)
                .ToListAsync();
        }

        public async Task<int> Update(TEntity model)
        {
            return await Db.Updateable<TEntity>(model)
                .ExecuteCommandAsync();
        }

        public async Task<int> Update(TEntity model, string where)
        {
            return await Update(model, null, null, where);
        }

        public async Task<int> Update(TEntity model, string[] columns = null, string[] ignoreColumn = null, string where = null)
        {
            IUpdateable<TEntity> up = await Task.Run(() => Db.Updateable<TEntity>(model));
            if(columns != null && columns.Length > 0)
            {
                up = await Task.Run(() => up.UpdateColumns(columns));
            }
            if(ignoreColumn != null && ignoreColumn.Length > 0)
            {
                up = await Task.Run(() => up.IgnoreColumns(ignoreColumn));
            }
            if(string.IsNullOrWhiteSpace(where))
            {
                up = await Task.Run(() => up.Where(where));
            }
            return await up.ExecuteCommandAsync();
        }

        public async Task<int> AddRange(IEnumerable<TEntity> models)
        {
            return await Db.Insertable<TEntity>(models.ToArray())
                .ExecuteCommandAsync();
        }

        public async Task<int> Update(object operateAnonymousObjects)
        {
            return await Db.Updateable<TEntity>(operateAnonymousObjects)
                .ExecuteCommandAsync();
        }

        public async Task<IEnumerable<TEntity>> QuerySql(string sql, SugarParameter[] parameters = null)
        {
            return await Db.Ado.SqlQueryAsync<TEntity>(sql, parameters);
        }

        public async Task<DataTable> QueryTable(string sql, SugarParameter[] parameters = null)
        {
            return await Db.Ado.GetDataTableAsync(sql, parameters);
        }

        public async Task<PageModel<TEntity>> QueryPage(Expression<Func<TEntity, bool>> whereExpression, int pageIndex = 0, int pageSize = 20, string orderFields = null)
        {
            RefAsync<int> totalCount = 0;
            var data = await Db.Queryable<TEntity>()
                .OrderByIF(!string.IsNullOrWhiteSpace(orderFields), orderFields)
                .WhereIF(whereExpression != null, whereExpression)
                .ToPageListAsync(pageIndex, pageSize, totalCount);

            int pageCount = (Math.Ceiling(totalCount.ObjToDecimal() / pageSize.ObjToDecimal()))
                .ObjToInt();
            return new PageModel<TEntity>()
            {
                DataCount = totalCount,
                PageCount = pageCount,
                Page = pageIndex,
                PageSize = pageSize,
                Data = data
            };
        }

        public async Task<IEnumerable<TResult>> QueryMuch<T1, T2, T3, TResult>(Expression<Func<T1, T2, T3, object[]>> joinExpression, Expression<Func<T1, T2, T3, TResult>> selectExpression, Expression<Func<T1, T2, T3, bool>> whereExpression) where T1 : class, new()
        {
            if(whereExpression == null)
            {
                return await Db.Queryable(joinExpression)
                    .Select(selectExpression)
                    .ToListAsync();
            }
            return await Db.Queryable(joinExpression)
                .Where(whereExpression)
                .Select(selectExpression)
                .ToListAsync();
        }
    }
}