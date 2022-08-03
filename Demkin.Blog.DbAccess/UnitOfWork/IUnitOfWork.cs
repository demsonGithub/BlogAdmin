using SqlSugar;

namespace Demkin.Blog.DbAccess.UnitOfWork
{
    public interface IUnitOfWork
    {
        SqlSugarScope GetDbClient();

        void BeginTran();

        void CommitTran();

        void RollbackTran();
    }
}