using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using System;

namespace Pair_of_employees_who_have_worked_together.Helpers
{
    public class CustomDateTimeConverter : DateTimeConverter
    {
        private readonly string[] dateFormats = new[] {"yyyy-MM-dd", "MM/dd/yyyy", "dd/MM/yyyy", "yyyy/MM/dd", "dd-MM-yyyy",
        "MM-dd-yyyy", "yyyyMMdd", "ddMMyyyy", "dd-MMM-yyyy", "yyyy MMM dd", "dd.MM.yyyy", "yyyy.MM.dd", "MM.dd.yyyy" };

        public override object? ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData)
        {
            if (string.IsNullOrEmpty(text) || text.ToLower().Equals("null"))
                return DateTime.Now.Date;

            if (DateTime.TryParseExact(text, dateFormats, System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.None, out DateTime result))
            {
                return result;
            }

            return base.ConvertFromString(text, row, memberMapData);
        }
    }
}
