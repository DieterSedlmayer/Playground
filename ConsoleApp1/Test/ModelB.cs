namespace ConsoleApp1.Test;

[ModelRepository("global", "assets")]
public class ModelB
{
    #region Fields

    private DateTime? _createdOn;
    private Guid? _id;
    private IDictionary<string, object?>? _properties;

    #endregion Fields

    #region Properties

    public DateTime CreatedOn
    {
        get => _createdOn ??= DateTime.UtcNow;
        init => _createdOn = value;
    }

    public DateTime? DeletedOn { get; init; }

    public Guid Id
    {
        get => _id ??= Guid.NewGuid();
        init => _id = value;
    }

    public DateTime? ModifiedOn { get; init; }

    public IDictionary<string, object?> Properties
    {
        get => _properties ??= new Dictionary<string, object?>();
        init => _properties = value;
    }

    #endregion Properties
}
