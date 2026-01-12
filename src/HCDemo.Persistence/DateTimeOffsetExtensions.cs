namespace HCDemo.Persistence;

public static class DateTimeOffsetExtensions
{
  extension(DateTimeOffset dateTimeOffset)
  {
    public DateTimeOffset WithTimeOfDay(TimeSpan timeOfDay)
    {
      return new DateTimeOffset(dateTimeOffset.Date.Add(timeOfDay), dateTimeOffset.Offset);
    }

    public DateTimeOffset WithStartTimeOfDay()
    {
      return new DateTimeOffset(dateTimeOffset.Date, dateTimeOffset.Offset);
    }

    public DateTimeOffset WithEndTimeOfDay()
    {
      var dateTime = dateTimeOffset
        .Date
        .AddHours(23)
        .AddMinutes(59)
        .AddSeconds(59);

      return new DateTimeOffset(dateTime, dateTimeOffset.Offset);
    }

    public DateTimeOffset WithDate(DateTime dateValue)
    {
      var dateTime = dateValue.Date + dateTimeOffset.TimeOfDay;

      return new DateTimeOffset(dateTime, dateTimeOffset.Offset);
    }

    public DateTimeOffset WithOffset(TimeSpan offset)
    {
      return new DateTimeOffset(dateTimeOffset.Ticks, offset);
    }

    public DateTimeOffset WithOffset(TimeZoneInfo tz)
    {
      var offset = tz.GetUtcOffset(dateTimeOffset);

      return WithOffset(dateTimeOffset, offset);
    }
  }
}
