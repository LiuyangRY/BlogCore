using Blog.Core.Model.Models;
using Blog.Core.Repository.UnitOfWork;
using Microsoft.Extensions.Logging;
using SqlSugar;
using System;

namespace Blog.Core.Repository.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ISqlSugarClient sqlSugarClient;
        
        private readonly ILogger<UnitOfWork> logger;

        public UnitOfWork(ISqlSugarClient client, ILogger<UnitOfWork> log)
        {
            sqlSugarClient = client;
            logger = log;
        }

        public SqlSugarClient GetDBClient()
        {
            return sqlSugarClient as SqlSugarClient;
        }

        public void BeginTran()
        {
            GetDBClient().BeginTran();
        }

        public void CommitTran()
        {
            try
            {
                GetDBClient().CommitTran();
            }
            catch (Exception ex)
            {
                GetDBClient().RollbackTran();
                logger.LogError($"{ex.Message}\r\n{ex.InnerException}");
            }
        }

        public void RollBackTran()
        {
            GetDBClient().RollbackTran();
        }
    }
}