using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Test;

[ModelRepository("77", "sessions")]
public class ModelA
{
    #region Fields

    private DateTime? _createdOn;
    private Guid? _giud;

    #endregion Fields

    #region Properties

    public DateTime CreatedOn 
    { 
        get => _createdOn ??= DateTime.UtcNow; 
        init => _createdOn = value; 
    }

    public Guid Guid 
    { 
        get => _giud ??= Guid.NewGuid(); 
        init => _giud = value; 
    }

    public DateTime? ModifiedOn { get; init; }

    public string? Type { get; init; }

    #endregion Properties
}
