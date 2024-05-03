using MongoDB.Driver;
using System.Linq.Expressions;

namespace ConsoleApp1;
public static class RepositoryExtensions
{
    #region Methods

    /// <summary>
    /// 
    /// </summary>
    public static Pager<TDocument> CreatePager<TDocument>(
        this Repository<TDocument> repository,
        int pageLength,
        Expression<Func<TDocument, bool>>? filter,
        SortDefinition<TDocument>? sort = null)
        => new(
            repository,
            filter,
            sort is null ? [] : [PipelineStageDefinitionBuilder.Sort(sort)],
            pageLength);

    /// <summary>
    /// 
    /// </summary>
    public static Pager<TDocument> CreatePager<TDocument>(
        this Repository<TDocument> repository,
        int pageLength,
        FilterDefinition<TDocument>? filter = null,
        SortDefinition<TDocument>? sort = null)
        => new(
            repository,
            filter,
            sort is null ? [] : [PipelineStageDefinitionBuilder.Sort(sort)],
            pageLength);

    #endregion Methods
}
