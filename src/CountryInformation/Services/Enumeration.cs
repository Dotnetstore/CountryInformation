using System.Reflection;

namespace CountryInformation.Services;

public abstract class Enumeration<TEnum>(Guid value, string name) : IEquatable<Enumeration<TEnum>>
    where TEnum : Enumeration<TEnum>
{
    private static readonly Dictionary<Guid, TEnum> Enumerations = CreateEnumerations();

    public Guid Value { get; } = value;

    public string Name { get; } = name;

    public static TEnum? FromValue(Guid value)
    {
        return Enumerations.GetValueOrDefault(value);
    }

    public static TEnum? FromName(string name)
    {
        return Enumerations
            .Values
            .SingleOrDefault(q => q.Name == name);
    }

    public bool Equals(Enumeration<TEnum>? other)
    {
        if (other is null)
            return false;

        return GetType() == other.GetType() &&
               Value == other.Value;
    }

    public override bool Equals(object? obj)
    {
        return obj is Enumeration<TEnum> other &&
               Equals(other);
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    private static Dictionary<Guid, TEnum> CreateEnumerations()
    {
        var enumerationType = typeof(TEnum);

        var fieldsForType = enumerationType
            .GetFields(
                BindingFlags.Public |
                BindingFlags.Static |
                BindingFlags.FlattenHierarchy)
            .Where(q => enumerationType.IsAssignableFrom(q.FieldType))
            .Select(q => (TEnum) q.GetValue(default)!);

        return fieldsForType.ToDictionary(q => q.Value);
    }
}