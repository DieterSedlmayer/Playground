using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Collections;
using System.Linq.Expressions;

namespace ConsoleApp1;

/// <summary>
/// 
/// </summary>
public class Repository<TDocument>(IMongoCollection<TDocument> collection) : IMongoQueryable<TDocument>
{
    #region Fields

    private IMongoQueryable<TDocument>? _queryable;

    #endregion Fields

    #region Properties

    /// <summary>
    /// 
    /// </summary>
    BsonDocument[] IMongoQueryable.LoggedStages
        => AsQueryable().LoggedStages;

    /// <summary>
    /// 
    /// </summary>
    Type IQueryable.ElementType
        => AsQueryable().ElementType;

    /// <summary>
    /// 
    /// </summary>
    Expression IQueryable.Expression
        => AsQueryable().Expression;

    /// <summary>
    /// 
    /// </summary>
    IQueryProvider IQueryable.Provider
        => AsQueryable().Provider;

    /// <summary>
    /// 
    /// </summary>
    public IMongoCollection<TDocument> Collection { get; init; }
        // TODO - MESSAGE
        = collection ?? throw new ArgumentNullException(nameof(collection));

    /// <summary>
    /// 
    /// </summary>
    public string CollectionName
        => CollectionNamespace.CollectionName;

    /// <summary>
    /// Gets the namespace of the collection.
    /// </summary>
    public CollectionNamespace CollectionNamespace
        => Collection.CollectionNamespace;

    /// <summary>
    /// Gets the database.
    /// </summary>
    public IMongoDatabase Database
        => Collection.Database;

    /// <summary>
    /// 
    /// </summary>
    public string DatabaseName
        => CollectionNamespace.DatabaseNamespace.DatabaseName;

    /// <summary>
    /// 
    /// </summary>
    public Type DocumentType
        => typeof(TDocument);

    /// <summary>
    /// 
    /// </summary>
    protected Type ElementType
        => AsQueryable().ElementType;

    /// <summary>
    /// 
    /// </summary>
    protected Expression Expression
        => AsQueryable().Expression;

    /// <summary>
    /// 
    /// </summary>
    protected IReadOnlyCollection<BsonDocument> LoggedStages
        => AsQueryable().LoggedStages;

    /// <summary>
    /// 
    /// </summary>
    protected IQueryProvider Provider
        => AsQueryable().Provider;

    /// <summary>
    /// Gets the settings.
    /// </summary>
    public MongoCollectionSettings Settings
        => Collection.Settings.FrozenCopy();

    #endregion Properties

    #region Methods

    /// <summary>
    /// 
    /// </summary>
    IAsyncCursor<TDocument> IAsyncCursorSource<TDocument>.ToCursor(CancellationToken cancellationToken)
        => AsQueryable().ToCursor(cancellationToken);

    /// <summary>
    /// 
    /// </summary>
    Task<IAsyncCursor<TDocument>> IAsyncCursorSource<TDocument>.ToCursorAsync(CancellationToken cancellationToken)
        => AsQueryable().ToCursorAsync(cancellationToken);

    /// <summary>
    /// 
    /// </summary>
    IEnumerator IEnumerable.GetEnumerator()
        => GetEnumerator();

    /// <summary>
    /// 
    /// </summary>
    IEnumerator<TDocument> IEnumerable<TDocument>.GetEnumerator()
        => GetEnumerator();

    /// <summary>
    /// 
    /// </summary>
    QueryableExecutionModel IMongoQueryable.GetExecutionModel()
        => AsQueryable().GetExecutionModel();

    /// <summary>
    /// 
    /// </summary>
    public IAggregateFluent<TDocument> Aggregate(AggregateOptions? options = null)
        => Collection.Aggregate(options);

    /// <summary>
    /// Runs an aggregation pipeline.
    /// </summary>
    public virtual async Task<List<TResult>> AggregateAsync<TResult>(
        PipelineDefinition<TDocument, TResult> pipeline,
        AggregateOptions? options = null,
        CancellationToken cancellationToken = default)
        => await (await Collection.AggregateAsync(pipeline, options, cancellationToken).ConfigureAwait(false))
        .ToListAsync(cancellationToken)
        .ConfigureAwait(false);

    public virtual Task AggregateToCollectionAsync<TResult>(
        PipelineDefinition<TDocument, TResult> pipeline,
        AggregateOptions? options = null,
        CancellationToken cancellationToken = default)
        => Collection.AggregateToCollectionAsync(pipeline, options, cancellationToken);

    /// <summary>
    /// 
    /// </summary>
    public IMongoQueryable<TDocument> AsQueryable()
        => _queryable ??= Collection.AsQueryable();

    /// <summary>
    /// 
    /// </summary>
    public virtual IMongoQueryable<TDocument> AsQueryable(AggregateOptions aggregateOptions)
        => Collection.AsQueryable(aggregateOptions);

    /// <summary>
    /// Performs multiple write operations.
    /// </summary>
    public virtual Task<BulkWriteResult<TDocument>> BulkWriteAsync(
        IEnumerable<WriteModel<TDocument>> requests,
        BulkWriteOptions? options = null,
        CancellationToken cancellationToken = default)
        => Collection.BulkWriteAsync(requests, options, cancellationToken);

    /// <summary>
    /// 
    /// </summary>
    public virtual Task<long> CountAsync(
        Expression<Func<TDocument, bool>> filter,
        CountOptions? options = null,
        CancellationToken cancellationToken = default)
        => Collection.CountDocumentsAsync(filter, options, cancellationToken);

    /// <summary>
    /// 
    /// </summary>
    public virtual Task<long> CountAsync(
        FilterDefinition<TDocument> filter, CountOptions? options = null, CancellationToken cancellationToken = default)
        => Collection.CountDocumentsAsync(filter, options, cancellationToken);

    /// <summary>
    /// Deletes multiple documents.
    /// </summary>
    public virtual Task<DeleteResult> DeleteManyAsync(
        Expression<Func<TDocument, bool>> filter, CancellationToken cancellationToken = default)
        => Collection.DeleteManyAsync(filter, cancellationToken);

    /// <summary>
    /// Deletes multiple documents.
    /// </summary>
    public virtual Task<DeleteResult> DeleteManyAsync(
        Expression<Func<TDocument, bool>> filter, DeleteOptions options, CancellationToken cancellationToken = default)
        => Collection.DeleteManyAsync(filter, options, cancellationToken);

    /// <summary>
    /// Deletes multiple documents.
    /// </summary>
    public virtual Task<DeleteResult> DeleteManyAsync(
        FilterDefinition<TDocument> filter, CancellationToken cancellationToken = default)
        => Collection.DeleteManyAsync(filter, cancellationToken);

    /// <summary>
    /// Deletes multiple documents.
    /// </summary>
    public virtual Task<DeleteResult> DeleteManyAsync(
        FilterDefinition<TDocument> filter, DeleteOptions options, CancellationToken cancellationToken = default)
        => Collection.DeleteManyAsync(filter, options, cancellationToken);

    /// <summary>
    /// Deletes a single document.
    /// </summary>
    public virtual Task<DeleteResult> DeleteOneAsync(
        Expression<Func<TDocument, bool>> filter, CancellationToken cancellationToken = default)
        => Collection.DeleteOneAsync(filter, cancellationToken);

    /// <summary>
    /// Deletes a single document.
    /// </summary>
    public virtual Task<DeleteResult> DeleteOneAsync(
        Expression<Func<TDocument, bool>> filter, DeleteOptions options, CancellationToken cancellationToken = default)
        => Collection.DeleteOneAsync(filter, options, cancellationToken);

    /// <summary>
    /// Deletes a single document.
    /// </summary>
    public virtual Task<DeleteResult> DeleteOneAsync(
        FilterDefinition<TDocument> filter, CancellationToken cancellationToken = default)
        => Collection.DeleteOneAsync(filter, cancellationToken);

    /// <summary>
    /// Deletes a single document.
    /// </summary>
    public virtual Task<DeleteResult> DeleteOneAsync(
        FilterDefinition<TDocument> filter, DeleteOptions options, CancellationToken cancellationToken = default)
        => Collection.DeleteOneAsync(filter, options, cancellationToken);

    /// <summary>
    /// Gets the distinct values for a specified field.
    /// </summary>
    public virtual async Task<List<TField>> DistinctAsync<TField>(
        Expression<Func<TDocument, TField>> field,
        Expression<Func<TDocument, bool>> filter,
        DistinctOptions? options = null,
        CancellationToken cancellationToken = default)
        => await (await Collection.DistinctAsync(field, filter, options, cancellationToken).ConfigureAwait(false))
        .ToListAsync(cancellationToken)
        .ConfigureAwait(false);

    /// <summary>
    /// Gets the distinct values for a specified field.
    /// </summary>
    public virtual async Task<List<TField>> DistinctAsync<TField>(
        Expression<Func<TDocument, TField>> field,
        FilterDefinition<TDocument> filter,
        DistinctOptions? options = null,
        CancellationToken cancellationToken = default)
        => await (await Collection.DistinctAsync(field, filter, options, cancellationToken).ConfigureAwait(false))
        .ToListAsync(cancellationToken)
        .ConfigureAwait(false);

    /// <summary>
    /// Gets the distinct values for a specified field.
    /// </summary>
    public virtual async Task<List<TField>> DistinctAsync<TField>(
        FieldDefinition<TDocument, TField> field,
        Expression<Func<TDocument, bool>> filter,
        DistinctOptions? options = null,
        CancellationToken cancellationToken = default)
        => await (await Collection.DistinctAsync(field, filter, options, cancellationToken).ConfigureAwait(false))
        .ToListAsync(cancellationToken)
        .ConfigureAwait(false);

    /// <summary>
    /// Gets the distinct values for a specified field.
    /// </summary>
    public virtual async Task<List<TField>> DistinctAsync<TField>(
        FieldDefinition<TDocument, TField> field,
        FilterDefinition<TDocument> filter,
        DistinctOptions? options = null,
        CancellationToken cancellationToken = default)
        => await (await Collection.DistinctAsync(field, filter, options, cancellationToken).ConfigureAwait(false))
        .ToListAsync(cancellationToken)
        .ConfigureAwait(false);

    /// <summary>
    /// Gets the distinct values for a specified array field.
    /// </summary>
    public virtual async Task<List<TItem>> DistinctManyAsync<TItem>(
        Expression<Func<TDocument, IEnumerable<TItem>>> field,
        Expression<Func<TDocument, bool>> filter,
        DistinctOptions? options = null,
        CancellationToken cancellationToken = default)
        => await (await Collection.DistinctManyAsync(field, filter, options, cancellationToken).ConfigureAwait(false))
        .ToListAsync(cancellationToken)
        .ConfigureAwait(false);

    /// <summary>
    /// Gets the distinct values for a specified array field.
    /// </summary>
    public virtual async Task<List<TItem>> DistinctManyAsync<TItem>(
        Expression<Func<TDocument, IEnumerable<TItem>>> field,
        FilterDefinition<TDocument> filter,
        DistinctOptions? options = null,
        CancellationToken cancellationToken = default)
        => await (await Collection.DistinctManyAsync(field, filter, options, cancellationToken).ConfigureAwait(false))
        .ToListAsync(cancellationToken)
        .ConfigureAwait(false);

    /// <summary>
    /// Gets the distinct values for a specified array field.
    /// </summary>
    public virtual async Task<List<TItem>> DistinctManyAsync<TItem>(
        FieldDefinition<TDocument, IEnumerable<TItem>> field,
        Expression<Func<TDocument, bool>> filter,
        DistinctOptions? options = null,
        CancellationToken cancellationToken = default)
        => await (await Collection.DistinctManyAsync(field, filter, options, cancellationToken).ConfigureAwait(false))
        .ToListAsync(cancellationToken)
        .ConfigureAwait(false);

    /// <summary>
    /// Gets the distinct values for a specified array field.
    /// </summary>
    public virtual async Task<List<TItem>> DistinctManyAsync<TItem>(
        FieldDefinition<TDocument, IEnumerable<TItem>> field,
        FilterDefinition<TDocument> filter,
        DistinctOptions? options = null,
        CancellationToken cancellationToken = default)
        => await (await Collection.DistinctManyAsync(field, filter, options, cancellationToken).ConfigureAwait(false))
        .ToListAsync(cancellationToken)
        .ConfigureAwait(false);

    /// <summary>
    /// Returns an estimate of the number of documents in the collection.
    /// </summary>
    public virtual Task<long> EstimatedDocumentCountAsync(
        EstimatedDocumentCountOptions? options = null, CancellationToken cancellationToken = default)
        => Collection.EstimatedDocumentCountAsync(options, cancellationToken);

    /// <summary>
    /// Begins a fluent find interface.
    /// </summary>
    public virtual IFindFluent<TDocument, TDocument> Find(
        Expression<Func<TDocument, bool>> filter, FindOptions? options = null)
        => Collection.Find(filter, options);

    /// <summary>
    /// Begins a fluent find interface.
    /// </summary>
    public virtual IFindFluent<TDocument, TDocument> Find(
        FilterDefinition<TDocument> filter, FindOptions? options = null)
        => Collection.Find(filter, options);

    /// <summary>
    /// Finds the documents matching the filter.
    /// </summary>
    public virtual async Task<List<TDocument>> FindAsync(
        Expression<Func<TDocument, bool>> filter,
        FindOptions<TDocument, TDocument>? options = null,
        CancellationToken cancellationToken = default)
        => await (await Collection.FindAsync(filter, options, cancellationToken).ConfigureAwait(false))
        .ToListAsync(cancellationToken)
        .ConfigureAwait(false);

    /// <summary>
    /// Finds the documents matching the filter.
    /// </summary>
    public virtual async Task<List<TDocument>> FindAsync(
        FilterDefinition<TDocument> filter,
        FindOptions<TDocument, TDocument>? options = null,
        CancellationToken cancellationToken = default)
        => await (await Collection.FindAsync(filter, options, cancellationToken).ConfigureAwait(false))
        .ToListAsync(cancellationToken)
        .ConfigureAwait(false);

    /// <summary>
    /// Finds the documents matching the filter.
    /// </summary>
    public virtual async Task<List<TProjection>> FindAsync<TProjection>(
        FilterDefinition<TDocument> filter,
        FindOptions<TDocument, TProjection>? options = null,
        CancellationToken cancellationToken = default)
        => await (await Collection.FindAsync(filter, options, cancellationToken).ConfigureAwait(false))
        .ToListAsync(cancellationToken)
        .ConfigureAwait(false);

    /// <summary>
    /// Finds a single document and deletes it atomically.
    /// </summary>
    public virtual Task<TDocument> FindOneAndDeleteAsync(
        Expression<Func<TDocument, bool>> filter,
        FindOneAndDeleteOptions<TDocument, TDocument>? options = null,
        CancellationToken cancellationToken = default)
        => Collection.FindOneAndDeleteAsync(filter, options, cancellationToken);

    /// <summary>
    /// Finds a single document and deletes it atomically.
    /// </summary>
    public virtual Task<TProjection> FindOneAndDeleteAsync<TProjection>(
        Expression<Func<TDocument, bool>> filter,
        FindOneAndDeleteOptions<TDocument, TProjection>? options = null,
        CancellationToken cancellationToken = default)
        => Collection.FindOneAndDeleteAsync(filter, options, cancellationToken);

    /// <summary>
    /// Finds a single document and deletes it atomically.
    /// </summary>
    public virtual Task<TDocument> FindOneAndDeleteAsync(
        FilterDefinition<TDocument> filter,
        FindOneAndDeleteOptions<TDocument, TDocument>? options = null,
        CancellationToken cancellationToken = default)
        => Collection.FindOneAndDeleteAsync(filter, options, cancellationToken);

    /// <summary>
    /// Finds a single document and deletes it atomically.
    /// </summary>
    public virtual Task<TProjection> FindOneAndDeleteAsync<TProjection>(
        FilterDefinition<TDocument> filter,
        FindOneAndDeleteOptions<TDocument, TProjection>? options = null,
        CancellationToken cancellationToken = default)
        => Collection.FindOneAndDeleteAsync(filter, options, cancellationToken);

    /// <summary>
    /// Finds a single document and replaces it atomically.
    /// </summary>
    public virtual Task<TDocument> FindOneAndReplaceAsync(
        Expression<Func<TDocument, bool>> filter,
        TDocument replacement,
        FindOneAndReplaceOptions<TDocument, TDocument>? options = null,
        CancellationToken cancellationToken = default)
        => Collection.FindOneAndReplaceAsync(filter, replacement, options, cancellationToken);

    /// <summary>
    /// Finds a single document and replaces it atomically.
    /// </summary>
    public virtual Task<TProjection> FindOneAndReplaceAsync<TProjection>(
        Expression<Func<TDocument, bool>> filter,
        TDocument replacement,
        FindOneAndReplaceOptions<TDocument, TProjection>? options = null,
        CancellationToken cancellationToken = default)
        => Collection.FindOneAndReplaceAsync(filter, replacement, options, cancellationToken);

    /// <summary>
    /// Finds a single document and replaces it atomically.
    /// </summary>
    public virtual Task<TDocument> FindOneAndReplaceAsync(
        FilterDefinition<TDocument> filter,
        TDocument replacement,
        FindOneAndReplaceOptions<TDocument, TDocument>? options = null,
        CancellationToken cancellationToken = default)
        => Collection.FindOneAndReplaceAsync(filter, replacement, options, cancellationToken);

    /// <summary>
    /// Finds a single document and replaces it atomically.
    /// </summary>
    public virtual Task<TProjection> FindOneAndReplaceAsync<TProjection>(
        FilterDefinition<TDocument> filter,
        TDocument replacement,
        FindOneAndReplaceOptions<TDocument, TProjection>? options = null,
        CancellationToken cancellationToken = default)
        => Collection.FindOneAndReplaceAsync(filter, replacement, options, cancellationToken);

    /// <summary>
    /// Finds a single document and updates it atomically.
    /// </summary>
    public virtual Task<TDocument> FindOneAndUpdateAsync(
        Expression<Func<TDocument, bool>> filter,
        UpdateDefinition<TDocument> update,
        FindOneAndUpdateOptions<TDocument, TDocument>? options = null,
        CancellationToken cancellationToken = default)
        => Collection.FindOneAndUpdateAsync(filter, update, options, cancellationToken);

    /// <summary>
    /// Finds a single document and updates it atomically.
    /// </summary>
    public virtual Task<TProjection> FindOneAndUpdateAsync<TProjection>(
        Expression<Func<TDocument, bool>> filter,
        UpdateDefinition<TDocument> update,
        FindOneAndUpdateOptions<TDocument, TProjection>? options = null,
        CancellationToken cancellationToken = default)
        => Collection.FindOneAndUpdateAsync(filter, update, options, cancellationToken);

    /// <summary>
    /// Finds a single document and updates it atomically.
    /// </summary>
    public virtual Task<TDocument> FindOneAndUpdateAsync(
        FilterDefinition<TDocument> filter,
        UpdateDefinition<TDocument> update,
        FindOneAndUpdateOptions<TDocument, TDocument>? options = null,
        CancellationToken cancellationToken = default)
        => Collection.FindOneAndUpdateAsync(filter, update, options, cancellationToken);

    /// <summary>
    /// Finds a single document and updates it atomically.
    /// </summary>
    public virtual Task<TProjection> FindOneAndUpdateAsync<TProjection>(
        FilterDefinition<TDocument> filter,
        UpdateDefinition<TDocument> update,
        FindOneAndUpdateOptions<TDocument, TProjection>? options = null,
        CancellationToken cancellationToken = default)
        => Collection.FindOneAndUpdateAsync(filter, update, options, cancellationToken);

    /// <summary>
    /// 
    /// </summary>
    public IEnumerator<TDocument> GetEnumerator()
        => AsQueryable().GetEnumerator();

    /// <summary>
    /// 
    /// </summary>
    protected QueryableExecutionModel GetExecutionModel()
        => AsQueryable().GetExecutionModel();

    /// <summary>
    /// Inserts many documents.
    /// </summary>
    public virtual Task InsertManyAsync(
        IEnumerable<TDocument> documents,
        InsertManyOptions? options = null,
        CancellationToken cancellationToken = default)
        => Collection.InsertManyAsync(documents, options, cancellationToken);

    /// <summary>
    /// Inserts a single document.
    /// </summary>
    public virtual Task InsertOneAsync(
        TDocument document, InsertOneOptions? options = null, CancellationToken cancellationToken = default)
        => Collection.InsertOneAsync(document, options, cancellationToken);

    /// <summary>
    /// Returns a filtered collection that appears to contain only documents of the derived type.
    /// All operations using this filtered collection will automatically use discriminators as necessary.
    /// </summary>
    public TRepository OfType<TRepository, TDerivedDocument>()
        where TDerivedDocument : TDocument
        where TRepository : Repository<TDerivedDocument>
        => Activator.CreateInstance(typeof(TRepository), Collection.OfType<TDerivedDocument>()) as TRepository ??
       // TODO - MESSAGE
       throw new InvalidOperationException();

    /// <summary>
    /// Replaces a single document.
    /// </summary>
    public virtual Task<ReplaceOneResult> ReplaceOneAsync(
        Expression<Func<TDocument, bool>> filter,
        TDocument replacement,
        ReplaceOptions? options = null,
        CancellationToken cancellationToken = default)
        => Collection.ReplaceOneAsync(filter, replacement, options, cancellationToken);

    /// <summary>
    /// Replaces a single document.
    /// </summary>
    public virtual Task<ReplaceOneResult> ReplaceOneAsync(
        FilterDefinition<TDocument> filter,
        TDocument replacement,
        ReplaceOptions? options = null,
        CancellationToken cancellationToken = default)
        => Collection.ReplaceOneAsync(filter, replacement, options, cancellationToken);

    /// <summary>
    /// 
    /// </summary>
    protected IAsyncCursor<TDocument> ToCursor(CancellationToken cancellationToken)
        => AsQueryable().ToCursor(cancellationToken);

    /// <summary>
    /// 
    /// </summary>
    protected Task<IAsyncCursor<TDocument>> ToCursorAsync(CancellationToken cancellationToken)
        => AsQueryable().ToCursorAsync(cancellationToken);

    /// <summary>
    /// Updates many documents.
    /// </summary>
    public virtual Task<UpdateResult> UpdateManyAsync(
        Expression<Func<TDocument, bool>> filter,
        UpdateDefinition<TDocument> update,
        UpdateOptions? options = null,
        CancellationToken cancellationToken = default)
        => collection.UpdateManyAsync(filter, update, options, cancellationToken);

    /// <summary>
    /// Updates many documents.
    /// </summary>
    public virtual Task<UpdateResult> UpdateManyAsync(
        FilterDefinition<TDocument> filter,
        UpdateDefinition<TDocument> update,
        UpdateOptions? options = null,
        CancellationToken cancellationToken = default)
        => Collection.UpdateManyAsync(filter, update, options, cancellationToken);

    /// <summary>
    /// Updates a single document.
    /// </summary>
    public virtual Task<UpdateResult> UpdateOneAsync(
        Expression<Func<TDocument, bool>> filter,
        UpdateDefinition<TDocument> update,
        UpdateOptions? options = null,
        CancellationToken cancellationToken = default)
        => Collection.UpdateOneAsync(filter, update, options, cancellationToken);

    /// <summary>
    /// Updates a single document.
    /// </summary>
    public virtual Task<UpdateResult> UpdateOneAsync(
        FilterDefinition<TDocument> filter,
        UpdateDefinition<TDocument> update,
        UpdateOptions? options = null,
        CancellationToken cancellationToken = default)
        => Collection.UpdateOneAsync(filter, update, options, cancellationToken);

    #endregion Methods
}
