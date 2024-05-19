using Npgsql;
using System.Data;

namespace ProductAPI.Data.DataBase;

internal class PostgresConnection : IDBConnection, IDisposable
{
    private NpgsqlConnection _connection;
    private readonly NpgsqlConnectionStringBuilder _connectionString;

    public PostgresConnection(string connectionString)
    {
        _connectionString = new NpgsqlConnectionStringBuilder(connectionString);
        _connection = new NpgsqlConnection(_connectionString.ConnectionString);
    }

    public async Task<bool> CloseAsync()
    {
        if (_connection.State == ConnectionState.Open)
        {
            await _connection.CloseAsync();
        }

        return _connection.State == ConnectionState.Closed;
    }

    public async Task<bool> OpenAsync()
    {
        if (_connection.State == ConnectionState.Closed)
        {
            _connection = new NpgsqlConnection(_connectionString.ConnectionString);

            await _connection.OpenAsync();
        }

        return _connection.State == ConnectionState.Open;
    }

    public bool Close()
    {
        if (_connection.State == ConnectionState.Open)
        {
            _connection.Close();
        }

        return _connection.State == ConnectionState.Closed;
    }

    public bool Open()
    {
        if (_connection.State == ConnectionState.Closed)
        {
            _connection.Open();
        }

        return _connection.State == ConnectionState.Open;
    }

    public bool GetItems<T>(T item)
    {
        if (_connection.State == ConnectionState.Closed)
        {
            _connection.Open();
        }

        return _connection.State == ConnectionState.Open;
    }

    public async Task<DataTable> ExecuteReaderAsync(string sql, IDictionary<string, object> parameters)
    {
        var cmd = new NpgsqlCommand(sql, _connection);

        foreach (var pm in parameters)
        {
            cmd.Parameters.AddWithValue(pm.Key, pm.Value);
        }

        var reader = await cmd.ExecuteReaderAsync();
        var data = new DataTable();

        for (int i = 0; i < reader.FieldCount; i++)
        {
            data.Columns.Add(reader.GetName(i));
        }

        while (await reader.ReadAsync())
        {
            var row = data.NewRow();
            for (int i = 0; i < reader.FieldCount; i++)
            {
                row[i] = reader[i];
            }
            data.Rows.Add(row);
        }

        return data;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            if (_connection != null)
            {
                _connection.Dispose();
                _connection = null;
            }
        }
    }

    public async Task<bool> ExecuteQueryAsync(string sql, IDictionary<string, object> parameters)
    {
        var cmd = new NpgsqlCommand(sql, _connection);

        foreach (var pm in parameters)
        {
            cmd.Parameters.AddWithValue(pm.Key, pm.Value);
        }

        var result = await cmd.ExecuteNonQueryAsync();

        return result > 1;
    }

    ~PostgresConnection()
    {
        Dispose(false);
    }
}
