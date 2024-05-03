using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using System.Reflection;
using System.Text.Json.Serialization;

namespace ConsoleApp1;

/// <summary>
/// 
/// </summary>
public sealed class JsonPropertyNameConvention() : ConventionBase, IMemberMapConvention
{
    #region Methods

    /// <summary>
    /// 
    /// </summary>
    public void Apply(BsonMemberMap memberMap)
    {
        var attribute
            = (memberMap ?? throw new ArgumentNullException(nameof(memberMap)))
            .MemberInfo
            .GetCustomAttribute<JsonPropertyNameAttribute>();
        if (!string.IsNullOrWhiteSpace(attribute?.Name))
            memberMap.SetElementName(attribute.Name);
    }

    #endregion Methods
}
