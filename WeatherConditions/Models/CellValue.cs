using NPOI.SS.UserModel;
using System.Globalization;

namespace WeatherConditions.Models
{
    public static class CellValue
    {
        public static DateTime? ConvertDateTimeValue(this ICell cell)
        {
            if (cell == null)
            {
                return null;
            }

            return DateTime.TryParse(cell.StringCellValue.
                ToString(CultureInfo.InvariantCulture), out DateTime date) ?
                date : null;
        }

        public static int? ConvertIntValue(this ICell cell)
        {
            if (cell == null)
            {
                return null;
            }

            return cell.CellType == CellType.Numeric ?
                (int)cell.NumericCellValue : null;
        }


        public static float? ConvertFloatValue(this ICell cell)
        {
            if (cell == null)
            {
                return null;
            }

            return cell.CellType == CellType.Numeric ?
                (float)cell.NumericCellValue : null;
        }

        public static string? ConvertStringValue(this ICell cell)
        {
            if (cell == null)
            {
                return null;
            }

            return cell.CellType == CellType.String ?
                cell.StringCellValue : null;
        }

        public static TimeSpan? ConvertTimeSpan(this ICell cell)
        {
            if (cell == null)
            {
                return null;
            }

            return DateTime.TryParse(cell.StringCellValue.
                ToString(CultureInfo.InvariantCulture), out DateTime date) ?
                date.TimeOfDay : null;
        }
    }

}
