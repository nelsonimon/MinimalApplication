using Microsoft.Data.Sqlite;
using System.Data;

namespace MinimalApplication.Infrastructure.Data;

public interface IDatabase
{
    void Setup();
}
public sealed class DbSession : IDisposable
{
    private readonly DatabaseConfig _databaseConfig;

    private Guid _id;
    public IDbConnection Connection { get; }
    public IDbTransaction Transaction { get; set; }

    public DbSession(DatabaseConfig databaseConfig)
    {
        _databaseConfig = databaseConfig;
        _id = Guid.NewGuid();
        Connection = new SqliteConnection(_databaseConfig.Name);
        Connection.Open();
    }
    public void Dispose() => Connection?.Dispose();

}
