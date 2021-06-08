using SqlSugar;
using Sukt.Module.Core;

namespace DestinyCore.ETLDispatchCenter.EntityFrameworkCore.SqlSugar
{
    public interface ISqlSugarDbContext : IScopedDependency
    {
        SqlSugarClient GetSqlSugarClient(ConnectionConfig connection);
    }
}
