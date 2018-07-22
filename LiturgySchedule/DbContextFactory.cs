using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;
using System.Data.Common;

namespace RTH.LiturgySchedule {
public class DbContextFactory : IDisposable 
{
    private DbConnection _connection;
    private DbContextOptions<DataModel> _options;

    public DbContextFactory() { }  // will be an in-memory database

    public DbContextFactory(DbContextOptions<DataModel> options, bool reset = false) // will use passed in options
    {
        _options = options;
        if (reset) DataModel.ResetDatabase(options);
    }
    private DbContextOptions<DataModel> CreateOptions()
    {
        return new DbContextOptionsBuilder<DataModel>()
            .UseSqlite(_connection).Options;
    }

    public DataModel CreateContext()
    {

        if (_options != null) return new DataModel(_options);

        // in-memory
        if (_connection == null)
        {
            _connection = new SqliteConnection("DataSource=:memory:");
            _connection.Open();

            var options = CreateOptions();
            using (var context = new DataModel(CreateOptions()))
            {
                context.Database.EnsureCreated();
            }
        }

        return new DataModel(CreateOptions());
    }

    public void Dispose()
    {
        if (_connection != null)
        {
            _connection.Dispose();
            _connection = null;
        }
    }
}
}