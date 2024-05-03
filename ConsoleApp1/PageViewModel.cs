namespace ConsoleApp1;

/// <summary>
/// 
/// </summary>
public sealed record class PageViewModel<TDocument>(IReadOnlyList<TDocument> Documents, int PageIndex, long PagesCount)
{ }
