using ProductAPI.Configs;
using System.Collections.Concurrent;

namespace ProductAPI.Data.DataBase;

public class ProductDatabaseConnectionFactory : IConnectionFactory, IDisposable
{
    private readonly ConcurrentBag<IDBConnection> _connPool;
    private readonly IAppSettingsHelper _appSettingsHelper;
    private readonly CancellationTokenSource _cancellationTokenSource;

    public ProductDatabaseConnectionFactory(IAppSettingsHelper appSettingsHelper)
    {
        _appSettingsHelper = appSettingsHelper;
        _connPool = new ConcurrentBag<IDBConnection>();
        _cancellationTokenSource = new CancellationTokenSource();

        LoadConnections();
    }

    private void LoadConnections()
    {
        var connString = _appSettingsHelper.GetConnectionString("ProductConnectionString");
        var connCountConfig = _appSettingsHelper.GetAppSettings("NumberPostgresConnections");

        if (!int.TryParse(connCountConfig, out var connCount))
        {
            throw new ArgumentException(nameof(connCountConfig));
        }

        Task.Run(() =>
        {
            while (!_cancellationTokenSource.Token.IsCancellationRequested)
            {
                if (_connPool.IsEmpty)
                {
                    for (int i = _connPool.Count; i < connCount; i++)
                    {
                        var conn = new PostgresConnection(connString);
                        conn.Open();
                        _connPool.Add(conn);
                    }
                }

                Thread.Sleep(10);
            }
        }, _cancellationTokenSource.Token);
    }

    public IDBConnection GetConnection()
    {
        while (_connPool.TryTake(out var conn))
        {
            return conn;
        }

        var connString = _appSettingsHelper.GetConnectionString("ProductConnectionString");
        var newConn = new PostgresConnection(connString);

        newConn.Open();

        return newConn;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        _cancellationTokenSource.Cancel();

        foreach (var conn in _connPool)
        {
            conn.Dispose();
        }
        _connPool.Clear();
    }
}
