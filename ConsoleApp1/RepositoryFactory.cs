using MongoDB.Driver;
using System.Reflection;

namespace ConsoleApp1;

/// <summary>
/// 
/// </summary>
public sealed class RepositoryFactory(MongoClient client)
{
    #region Fields

    private readonly MongoClient _client = client;

    #endregion Fields

    #region Methods

    /// <summary>
    /// 
    /// </summary>
    private IMongoCollection<TDocument> GetCollection<TDocument>(string databaseName, string collectionName)
        => _client.GetDatabase(databaseName).GetCollection<TDocument>(collectionName);

    /// <summary>
    /// 
    /// </summary>
    public Repository<TDocument> GetRepository<TDocument>()
    {
        var attribute = typeof(TDocument).GetCustomAttribute<ModelRepositoryAttribute>();
        return attribute is null
            // TODO - MESSAGE
            ? throw new InvalidOperationException("")
            : GetRepository<TDocument>(attribute.RepositoryType, attribute.DatabaseName, attribute.CollectionName);
    }

    /// <summary>
    /// 
    /// </summary>
    public TRepository GetRepository<TRepository, TDocument>()
        where TRepository : Repository<TDocument>
        => GetRepository<TDocument>() as TRepository ??
        // TODO - MESSAGE
        throw new InvalidOperationException("");

    /// <summary>
    /// 
    /// </summary>
    public TRepository GetRepository<TRepository, TDocument>(string databaseName, string collectionName)
        where TRepository : Repository<TDocument>
        => GetRepository<TDocument>(typeof(TRepository), databaseName, collectionName) as TRepository ??
        // TODO - MESSAGE
        throw new InvalidOperationException("");

    /// <summary>
    /// 
    /// </summary>
    private Repository<TDocument> GetRepository<TDocument>(
        Type? repositoryType, string databaseName, string collectionName)
    {
        var repository
            = Activator.CreateInstance(
                repositoryType ?? typeof(Repository<TDocument>),
                GetCollection<TDocument>(
                    MapDatabaseName(databaseName), MapCollectionName(collectionName))) as Repository<TDocument> ??
                    // TODO - MESSAGE
                    throw new InvalidOperationException("");

        return typeof(TDocument).IsAssignableFrom(repository.DocumentType)
            ? repository
            // TODO - MESSAGE
            : throw new InvalidOperationException("");
    }

    /// <summary>
    /// 
    /// </summary>
    private string MapCollectionName(string collectionName)
        => collectionName;

    /// <summary>
    /// 
    /// </summary>
    private string MapDatabaseName(string databaseName)
        => databaseName;

    #endregion Methods
}
