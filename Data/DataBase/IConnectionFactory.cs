namespace ProductAPI.Data.DataBase;

public interface IConnectionFactory
{
    public IDBConnection GetConnection();
}
