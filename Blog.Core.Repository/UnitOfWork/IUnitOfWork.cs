using SqlSugar;

namespace Blog.Core.Repository.UnitOfWork
{
    public interface IUnitOfWork
    {
        SqlSugarClient GetDBClient();

        void BeginTran();

        void CommitTran();

        void RollBackTran();
    }
}