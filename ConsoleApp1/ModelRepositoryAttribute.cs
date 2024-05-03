namespace ConsoleApp1;

/// <summary>
/// 
/// </summary>
[AttributeUsage(
    AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Struct,
    AllowMultiple = false,
    Inherited = true)]
public sealed class ModelRepositoryAttribute(string databaseName, string collectionName, Type? repositoryType = null)
    : Attribute
{
    #region Properties

    /// <summary>
    /// 
    /// </summary>
    public string CollectionName { get; private init; }
        = string.IsNullOrWhiteSpace(collectionName)
        // TODO - MESSAGE
        ? throw new ArgumentException("", nameof(collectionName))
        : collectionName;

    /// <summary>
    /// 
    /// </summary>
    public string DatabaseName { get; private init; }
        = string.IsNullOrWhiteSpace(databaseName)
        // TODO - MESSAGE
        ? throw new ArgumentException("", nameof(databaseName))
        : databaseName;

    /// <summary>
    /// 
    /// </summary>
    public Type? RepositoryType { get; private init; } = repositoryType;

    #endregion Properties
}
