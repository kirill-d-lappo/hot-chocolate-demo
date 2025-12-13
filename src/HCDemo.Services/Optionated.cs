using HotChocolate;

namespace HCDemo.Services;

public readonly struct Optionated<T>
{
  public Optionated(T value)
  {
    Value = value;
    HasValue = true;
  }

  public T Value { get; }

  public bool HasValue { get; }

  public T GetValueOrDefault(T defaultValue = default)
  {
    return GetValueOrDefault(this, defaultValue);
  }

  private static T GetValueOrDefault(Optionated<T> optionated, T defaultValue = default)
  {
    return optionated.HasValue
      ? optionated.Value
      : defaultValue;
  }

  public static implicit operator T(Optionated<T> optionated)
  {
    if (optionated.HasValue)
    {
      return optionated.Value;
    }

    throw new InvalidCastException("Optionated value does not have a value");
  }

  public static implicit operator Optionated<T>(T value)
  {
    return new Optionated<T>(value);
  }

  // Note [2024-12-05 klappo] hot chocolate conversion here
  public static implicit operator Optionated<T>(Optional<T> optional)
  {
    return optional.HasValue
      ? new Optionated<T>(optional.Value)
      : default;
  }

  public static implicit operator Optional<T>(Optionated<T> optionated)
  {
    return optionated.HasValue
      ? new Optional<T>(optionated.Value)
      : default;
  }
}
