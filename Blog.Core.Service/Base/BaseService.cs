using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Blog.Core.IService.Base;
using Blog.Core.Model;
using Blog.Core.Repository.Base;
using SqlSugar;

namespace Blog.Core.Service.Base
{
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : class, new()
    {
        public IBaseRepository<TEntity> repository;

        public async Task<int> Add(TEntity model)
        {
            return await repository.Add(model);
        }

        public async Task<int> Add(IEnumerable<TEntity> models)
        {
            return await repository.AddRange(models);
        }

        public async Task<int> Delete(TEntity model)
        {
            return await repository.Delete(model);
        }

        public async Task<int> DeleteById(object id)
        {
            return await repository.DeleteByID(id);
        }

        public async Task<int> DeleteByIds(object[] ids)
        {
            return await repository.DeleteByIDs(ids);
        }

        public async Task<IEnumerable<TEntity>> Query()
        {
            return await repository.Query();
        }

        public async Task<IEnumerable<TEntity>> Query(string where)
        {
            return await repository.Query(where);
        }

        public async Task<IEnumerable<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression)
        {
            return await repository.Query(whereExpression);
        }

        public async Task<IEnumerable<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, string orderByFields)
        {
            return await repository.Query(whereExpression, orderByFields);
        }

        public async Task<IEnumerable<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, object>> orderByExpression, bool isAsc = true)
        {
            return await repository.Query(whereExpression, orderByExpression, isAsc);
        }

        public async Task<IEnumerable<TEntity>> Query(string where, string orderByFields)
        {
            return await repository.Query(where, orderByFields);
        }

        public async Task<IEnumerable<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, int top, string orderByFields)
        {
            return await repository.Query(whereExpression, top, orderByFields);
        }

        public async Task<IEnumerable<TEntity>> Query(string where, int top, string orderByFileds)
        {
            return await repository.Query(where, top, orderByFileds);
        }

        public async Task<IEnumerable<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, int pageIndex, int pageSize, string orderByFields)
        {
            return await repository.Query(whereExpression, pageIndex, pageSize, orderByFields);
        }

        public async Task<IEnumerable<TEntity>> Query(string where, int pageIndex, int pageSize, string orderByFields)
        {
            return await repository.Query(where, pageIndex, pageSize, orderByFields);
        }

        public async Task<TEntity> QueryById(object objId)
        {
            return await repository.QueryByID(objId);
        }

        public async Task<TEntity> QueryById(object objId, bool useCache = false)
        {
            return await repository.QueryByID(objId, useCache);
        }

        public async Task<IEnumerable<TEntity>> QueryByIDs(object[] objIds)
        {
            return await repository.QueryByIDs(objIds);   
        }

        public async Task<IEnumerable<TResult>> QueryMuch<T1, T2, T3, TResult>(Expression<Func<T1, T2, T3, object[]>> joinExpression, Expression<Func<T1, T2, T3, TResult>> selectExpression, Expression<Func<T1, T2, T3, bool>> whereExpression = null) where T1 : class, new()
        {
            return await repository.QueryMuch<T1, T2, T3, TResult>(joinExpression, selectExpression, whereExpression);
        }

        public async Task<PageModel<TEntity>> QueryPage(Expression<Func<TEntity, bool>> whereExpression, int pageIndex = 1, int pageSize = 20, string orderByFields = null)
        {
            return await QueryPage(whereExpression, pageIndex, pageSize, orderByFields);
        }

        public async Task<IEnumerable<TEntity>> QuerySql(string sql, SugarParameter[] parameters = null)
        {
            return await repository.QuerySql(sql, parameters);
        }

        public async Task<DataTable> QueryTable(string sql, SugarParameter[] parameters = null)
        {
            return await repository.QueryTable(sql, parameters);
        }

        public async Task<int> Update(TEntity model)
        {
            return await repository.Update(model);
        }

        public async Task<int> Update(TEntity entity, string where)
        {
            return await repository.Update(entity, where);
        }

        public async Task<int> Update(object operateAnonymousObjects)
        {
            return await repository.Update(operateAnonymousObjects);
        }

        public async Task<int> Update(TEntity entity, string[] columns = null, string[] ignoreColumns = null, string where = "")
        {
            return await repository.Update(entity, columns, ignoreColumns, where);
        }
    }
}