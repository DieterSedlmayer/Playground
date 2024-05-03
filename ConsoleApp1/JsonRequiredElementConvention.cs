using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using System.Reflection;
using System.Text.Json.Serialization;

namespace ConsoleApp1;

/// <summary>
/// 
/// </summary>
public sealed class JsonRequiredElementConvention() : ConventionBase, IMemberMapConvention
{
    #region Methods

    /// <summary>
    /// 
    /// </summary>
    public void Apply(BsonMemberMap memberMap)
    {
        if ((memberMap ?? throw new ArgumentNullException(nameof(memberMap)))
            .MemberInfo
            .GetCustomAttribute<JsonRequiredAttribute>() is not null)
            memberMap.SetIsRequired(true);
    }

    #endregion Methods
}
