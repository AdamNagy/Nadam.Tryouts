using System.Text.RegularExpressions;

using DocumentFormat.OpenXml.Packaging;

namespace Infrastructure.OpenXML;

public static class Utils
{
    public static readonly List<string> ALphabet = ["a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z"];

    // Given a cell name, parses the specified cell to get the row index.
    public static uint GetRowIndexFromCellIndex(string cellIndex)
    {
        // Create a regular expression to match the row index portion the cell name.
        Regex regex = new Regex(@"\d+");
        Match match = regex.Match(cellIndex);

        return uint.Parse(match.Value);
    }

    // Given a cell name, parses the specified cell to get the column name.
    public static ColumnIndex GetColumnIndexFromCellIndex(string cellIndex)
    {
        // Create a regular expression to match the column name portion of the cell name.
        Regex regex = new Regex("[A-Za-z]+");
        Match match = regex.Match(cellIndex);

        return ColumnIndex.From(match.Value);
    }

    // Given two column names (A, B C, etc..), compares the columns.
    public static int CompareColumn(string column1, string column2)
    {
        if (string.IsNullOrWhiteSpace(column1)) throw new ArgumentNullException(nameof(column1));
        if (string.IsNullOrWhiteSpace(column2)) throw new ArgumentNullException(nameof(column2));

        if (column1.Length > column2.Length)
        {
            return 1;
        }
        else if (column1.Length < column2.Length)
        {
            return -1;
        }
        else
        {
            return string.Compare(column1, column2, true);
        }
    }

    public static SharedStringTablePart GetSharedStringTable(WorkbookPart workbook)
    {
        if (workbook.GetPartsOfType<SharedStringTablePart>().Count() > 0)
        {
            return workbook.GetPartsOfType<SharedStringTablePart>().First();
        }
        else
        {
            return workbook.AddNewPart<SharedStringTablePart>();
        }
    }
}
