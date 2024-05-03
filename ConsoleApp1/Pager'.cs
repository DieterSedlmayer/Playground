using MongoDB.Driver;

namespace ConsoleApp1;

/// <summary>
/// 
/// </summary>
public class Pager<TDocument>(
    Repository<TDocument> repository,
    FilterDefinition<TDocument>? filter,
    IEnumerable<IPipelineStageDefinition> stages,
    int pageSize) : Pager<TDocument, TDocument>(repository, filter, stages, pageSize)
{ }
