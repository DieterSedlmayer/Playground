using MongoDB.Driver;

namespace ConsoleApp1;

/// <summary>
/// 
/// </summary>
public class Pager<TInputDocument, TOutputDocument>(
    Repository<TInputDocument> repository,
    FilterDefinition<TInputDocument>? filter,
    IEnumerable<IPipelineStageDefinition> stages,
    int pageLength)
{
    #region Fields

    private AggregateFacet<TInputDocument, AggregateCountResult>? _aggregateCountFacet;

    #endregion Fields

    #region Properties

    /// <summary>
    /// 
    /// </summary>
    private AggregateFacet<TInputDocument, AggregateCountResult> AggregateCountFacet
        => _aggregateCountFacet ??=
        AggregateFacet.Create(
            "countFacet",
            PipelineDefinition<TInputDocument, AggregateCountResult>.Create(
                [PipelineStageDefinitionBuilder.Count<TInputDocument>()]));

    /// <summary>
    /// 
    /// </summary>
    private IMongoCollection<TInputDocument> Collection { get; init; }
        // TODO - MESSAGE
        = repository?.Collection ?? throw new ArgumentException(nameof(repository));

    /// <summary>
    /// 
    /// </summary>
    private FilterDefinition<TInputDocument> Filter { get; init; }
        = filter ?? FilterDefinition<TInputDocument>.Empty;

    /// <summary>
    /// 
    /// </summary>
    private int PageLength { get; init; }
        // TODO - MESSAGE
        = pageLength > 0 ? pageLength : throw new ArgumentException(nameof(pageLength));

    /// <summary>
    /// 
    /// </summary>
    private IEnumerable<IPipelineStageDefinition> Stages { get; init; }
        // TODO - MESSAGE
        = stages?.OfType<IPipelineStageDefinition>() ?? throw new ArgumentException(nameof(stages));

    #endregion Properties

    #region Methods

    /// <summary>
    /// 
    /// </summary>
    public async Task<PageViewModel<TOutputDocument>?> GetPageAsync(
        int pageIndex, CancellationToken cancellationToken = default)
    {
        var result
            = await Collection
            .Aggregate()
            .Match(Filter)
            .Facet(
                AggregateCountFacet,
                AggregateFacet.Create(
                    "documentFacet",
                    PipelineDefinition<TInputDocument, TOutputDocument>.Create(
                        Stages
                        .Append(PipelineStageDefinitionBuilder.Skip<TOutputDocument>(pageIndex * PageLength))
                        .Append(PipelineStageDefinitionBuilder.Limit<TOutputDocument>(PageLength)))))
            .FirstOrDefaultAsync(cancellationToken)
            .ConfigureAwait(false);
        if (result is null)
            return null;

        var count
            = result
            .Facets
            .FirstOrDefault(_ => string.Equals(_.Name, "countFacet", StringComparison.Ordinal))?
            .Output<AggregateCountResult>()[0].Count ?? 0;

        var documents
            = result
            .Facets
            .FirstOrDefault(_ => string.Equals(_.Name, "documentFacet", StringComparison.Ordinal))?
            .Output<TOutputDocument>() ?? [];

        return new(documents, pageIndex, Convert.ToInt64(Math.Ceiling((double)count / PageLength)));
    }

    #endregion Methods
}
