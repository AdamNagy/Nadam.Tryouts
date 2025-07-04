using DocumentFormat.OpenXml.Spreadsheet;

using static Infrastructure.OpenXML.Utils;

namespace Infrastructure.OpenXML;

/// <summary>
/// Encapsulates the reading logic for an Excel Sheet.
/// </summary>
public class ExcelReader
{
    private readonly ExcelSheet _excelsheet;

    public ExcelSheet ExcelSheet { get => _excelsheet; }

    public ExcelReader(ExcelSheet worksheet)
    {
        _excelsheet = worksheet;
    }

    public Dictionary<ColumnIndex, object?> ReadSingleRow(uint rowIdx)
    {
        var row = _excelsheet.WorkSheet.Descendants<Row>().FirstOrDefault(r =>
            r.RowIndex is not null &&
            r.RowIndex.Value == rowIdx);

        if(row == null)
        {
            throw new ArgumentException($"No suct row with index {rowIdx}");
        }

        var result = new Dictionary<ColumnIndex, object?>();
        foreach (Cell cell in row)
        {
            ColumnIndex columnName = GetColumnIndexFromCellIndex(cell.CellReference.Value);

            if (cell.DataType is not null)
            {
                if (cell.DataType.Value == CellValues.SharedString)
                {
                    result.Add(columnName, ReadText(int.Parse(cell.CellValue?.Text)));
                }
            }
            else
            {
                result.Add(columnName, cell.CellValue?.Text);
            }
        }        

        return result;
    }

    public IEnumerable<Dictionary<string, object?>> ReadRows()
    {
        foreach (Row row in _excelsheet.WorkSheet.Descendants<Row>().Where(r => r.RowIndex is not null).Skip(1))
        {
            var result = new Dictionary<string, object?>();
            foreach (Cell cell in row)
            {
                var s = cell.DataType;
                string columnName = GetColumnIndexFromCellIndex(cell.CellReference.Value);

                if (cell.DataType is not null)
                {
                    if (cell.DataType.Value == CellValues.SharedString)
                    {
                        result.Add(columnName, ReadText(int.Parse(cell.CellValue?.Text)));
                    }
                }
                else
                {
                    result.Add(columnName, cell.CellValue?.Text);
                }
            }

            yield return result;
        }
    }

    private string? ReadText(int key)
    {
        return _excelsheet.SharedStringTablePart.SharedStringTable.ElementAt(key).InnerText;
    }
}
