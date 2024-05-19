using System.Data;

namespace ProductAPI.Data.DataBase;

public interface IDBConnection : IDisposable
{
    public Task<bool> CloseAsync();

    public Task<bool> OpenAsync();

    public bool Close();

    public bool Open();

    public Task<DataTable> ExecuteReaderAsync(string sql, IDictionary<string, object> parameters);

    public Task<bool> ExecuteQueryAsync(string sql, IDictionary<string, object> parameters);
}
