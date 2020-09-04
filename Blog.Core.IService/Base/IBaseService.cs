using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Blog.Core.Model.Models;
using SqlSugar;

namespace Blog.Core.IService.Base
{
    public interface IBaseService<TEntity> where TEntity : class
    {
        Task<TEntity> QueryById(object objId);
        Task<TEntity> QueryById(object objId, bool useCache = false);
        Task<IEnumerable<TEntity>> QueryByIDs(object[] ids);

        Task<int> Add(TEntity model);

        Task<int> Add(IEnumerable<TEntity> models);

        Task<int> DeleteById(object id);

        Task<int> Delete(TEntity model);

        Task<int> DeleteByIds(object[] ids);

        Task<int> Update(TEntity model);
        Task<int> Update(TEntity entity, string where);

        Task<int> Update(object operateAnonymousObjects);

        Task<int> Update(TEntity entity, string[] columns = null, string[] ignoreColumns = null, string strWhere = "");

        Task<IEnumerable<TEntity>> Query();
        Task<IEnumerable<TEntity>> Query(string where);
        Task<IEnumerable<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression);
        Task<IEnumerable<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, string orderByFields);
        Task<IEnumerable<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, object>> orderByExpression, bool isAsc = true);
        Task<IEnumerable<TEntity>> Query(string where, string orderByFields);
        Task<IEnumerable<TEntity>> QuerySql(string sql, SugarParameter[] parameters = null);
        Task<DataTable> QueryTable(string sql, SugarParameter[] parameters = null);

        Task<IEnumerable<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, int top, string orderByFields);
        Task<IEnumerable<TEntity>> Query(string where, int top, string orderByFields);

        Task<IEnumerable<TEntity>> Query(
            Expression<Func<TEntity, bool>> whereExpression, int pageIndex, int pageSize, string orderByFields);
        Task<IEnumerable<TEntity>> Query(string where, int pageIndex, int pageSize, string orderByFields);


        Task<PageModel<TEntity>> QueryPage(Expression<Func<TEntity, bool>> whereExpression, int pageIndex = 1, int pageSize = 20, string orderByFields = null);

        Task<IEnumerable<TResult>> QueryMuch<T1, T2, T3, TResult>(
            Expression<Func<T1, T2, T3, object[]>> joinExpression,
            Expression<Func<T1, T2, T3, TResult>> selectExpression,
            Expression<Func<T1, T2, T3, bool>> whereExpression = null) where T1 : class, new();
    }
}