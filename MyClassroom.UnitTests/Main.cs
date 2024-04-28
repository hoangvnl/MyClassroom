using System;

var id = "America/New_York";
var estTimeZone = TimeZoneInfo.FindSystemTimeZoneById(id);

var dateTime = DateTimeOffset.Now.AddMonths(-3).AddDays(-1).AddHours(-10);
var test2 = TimeZoneInfo.ConvertTimeFromUtc(dateTime.ToUniversalTime().DateTime, estTimeZone);

Console.WriteLine("Hoang sau " + dateTime);
Console.WriteLine(estTimeZone.IsDaylightSavingTime(dateTime));
Console.WriteLine("Hoang sau " + test2);
Console.WriteLine(estTimeZone.IsDaylightSavingTime(test2));

var isDst = estTimeZone.IsDaylightSavingTime(test2);

TimeSpan targetOffset = isDst ? estTimeZone.BaseUtcOffset.Add(TimeSpan.FromHours(1)) : estTimeZone.BaseUtcOffset;

var time = TimeZoneInfo.ConvertTimeFromUtc(dateTime.ToUniversalTime().DateTime, estTimeZone);
var time2 = TimeZoneInfo.ConvertTimeFromUtc(dateTime.UtcDateTime, estTimeZone);
var time3 = TimeZoneInfo.ConvertTime(dateTime, estTimeZone);
Console.WriteLine(time);
Console.WriteLine(time2);
Console.WriteLine(time3);
DateTimeOffset test = new DateTimeOffset(time, targetOffset);
Console.WriteLine("Hoang sau " + test);
Console.WriteLine();



var estDateTime = TimeZoneInfo.ConvertTime(dateTime, estTimeZone);

Console.WriteLine(estDateTime);
