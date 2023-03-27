using StackExchange.Redis;

namespace RedisPubSubChat;

public class RedisClient : IDisposable
{
    public ConnectionMultiplexer connection;
    public string Channel = "Joe";
    public string Channel2 = "Elie";
    private readonly IConfiguration _configuration;
    public RedisClient(IConfiguration configuration)
    {
        _configuration = configuration;
        string connectionString = _configuration["RedisConnectionString"];
        var options = ConfigurationOptions.Parse(connectionString);
        options.Password = _configuration["RedisPassword"];
        connection = ConnectionMultiplexer.Connect(options);
    }

    private bool _disposedValue;

    ~RedisClient() => Dispose(false);

    // Public implementation of Dispose pattern callable by consumers.
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    // Protected implementation of Dispose pattern.
    protected virtual void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            if (disposing)
            {
                // TODO: dispose managed state (managed objects)
            }

            // TODO: free unmanaged resources (unmanaged objects) and override finalizer
            // TODO: set large fields to null
            _disposedValue = true;
        }
    }

    public ISubscriber GetSubscriber()
    {
        return connection.GetSubscriber();
    }
}
