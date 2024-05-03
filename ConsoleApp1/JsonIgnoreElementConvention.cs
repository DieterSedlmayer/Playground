using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using System.Reflection;
using System.Text.Json.Serialization;

namespace ConsoleApp1;

/// <summary>
/// 
/// </summary>
public sealed class JsonIgnoreElementConvention() : ConventionBase, IMemberMapConvention
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
            .GetCustomAttribute<JsonIgnoreAttribute>();
        if (attribute is null)
            return;

        if (attribute.Condition.HasFlag(JsonIgnoreCondition.Always))
            memberMap.SetShouldSerializeMethod(_ => false);

        if (attribute.Condition.HasFlag(JsonIgnoreCondition.WhenWritingDefault))
            memberMap.SetIgnoreIfDefault(true);

        if (attribute.Condition.HasFlag(JsonIgnoreCondition.WhenWritingNull))
            memberMap.SetIgnoreIfNull(true);
    }

    #endregion Methods
}
