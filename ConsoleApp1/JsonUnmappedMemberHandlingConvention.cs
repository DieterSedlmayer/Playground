using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using System.Reflection;
using System.Text.Json.Serialization;

namespace ConsoleApp1;

/// <summary>
/// 
/// </summary>
public sealed class JsonUnmappedMemberHandlingConvention() : ConventionBase, IClassMapConvention
{
    #region Methods

    /// <summary>
    /// 
    /// </summary>
    public void Apply(BsonClassMap classMap)
    {
        switch (GetAttribute(classMap ?? throw new ArgumentNullException(nameof(classMap)))?.UnmappedMemberHandling)
        {
            case JsonUnmappedMemberHandling.Disallow:
                classMap.SetIgnoreExtraElements(false);
                break;
            case JsonUnmappedMemberHandling.Skip:
                classMap.SetIgnoreExtraElements(true);
                break;
            default: break;
        };
    }

    /// <summary>
    /// 
    /// </summary>
    private static JsonUnmappedMemberHandlingAttribute? GetAttribute(BsonClassMap classMap)
    {
        for (var map = classMap; map is not null; map = map.BaseClassMap)
        {
            var attribute = map.ClassType.GetCustomAttribute<JsonUnmappedMemberHandlingAttribute>();
            if (attribute is not null)
                return attribute;
        }

        return null;
    }

    #endregion Methods
}
