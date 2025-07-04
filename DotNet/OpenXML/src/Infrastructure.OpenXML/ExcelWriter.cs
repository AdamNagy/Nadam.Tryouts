using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

namespace Infrastructure.OpenXML;

/// <summary>
/// Encapsulates the writing logic for an Excel Sheet.
/// </summary>
public class ExcelWriter
{
    private readonly ExcelSheet _excelSheet;
    private readonly ExcelReader _excelReader;

    public ExcelWriter(ExcelSheet excelSheet)
    {
        _excelSheet = excelSheet;
        _excelReader = new ExcelReader(excelSheet);
    }

    public bool Insert(string columnIndex, uint rowIndex, string value)
    {
        try
        {
            var cell = GetOrInsertCell(columnIndex, rowIndex, _excelSheet.WorkSheet);

            var sharedStringIndex = InsertSharedString(value, _excelSheet.SharedStringTablePart);

            cell.CellValue = new CellValue(sharedStringIndex.ToString());
            cell.DataType = new EnumValue<CellValues>(CellValues.SharedString);

            return true;
        }
        catch
        {
            return false;
        }
    }

    public void InsertColumn(string columnIndex, IEnumerable<string> values, uint rowIndex = 1)
    {
        foreach (var item in values)
        {
            Insert(columnIndex, rowIndex, item);
            ++rowIndex;
        }
    }

    public void InsertRow(uint rowIndex, IEnumerable<string> values, string columnIndex = "A")
    {
        var cellIdx = CellIndex.From(columnIndex, rowIndex);
        foreach (var item in values)
        {
            Insert(cellIdx.Column, cellIdx.Row, item);
            cellIdx = cellIdx.IncrementColumn();
        }
    }

    public ColumnIndex GetFirstEmptyColumn(uint rowIndex)
    {
        var columnIndex = _excelReader.ReadSingleRow(rowIndex)?.Max(p => p.Key);

        if(columnIndex == null)
        {
            throw new ArgumentException($"No such row with index {rowIndex}");
        }

        // TODO: EDGE CASE: check if the columnIndex is already the biggest
        return ++columnIndex;
    }

    // Given text and a SharedStringTablePart, creates a SharedStringItem with the specified text
    // and inserts it into the SharedStringTablePart. If the item already exists, returns its index.
    private int InsertSharedString(string text, SharedStringTablePart shareStringPart)
    {
        // If the part does not contain a SharedStringTable, create it.
        if (shareStringPart.SharedStringTable is null)
        {
            shareStringPart.SharedStringTable = new SharedStringTable();
        }

        int i = 0;
        foreach (SharedStringItem item in shareStringPart.SharedStringTable.Elements<SharedStringItem>())
        {
            if (item.InnerText == text)
            {
                // The text already exists in the part. Return its index.
                return i;
            }

            i++;
        }

        // The text does not exist in the part. Create the SharedStringItem.
        shareStringPart.SharedStringTable.AppendChild(new SharedStringItem(new DocumentFormat.OpenXml.Spreadsheet.Text(text)));

        return i;
    }

    /// <summary>
    /// Given a column name, a row index, and a WorksheetPart, inserts a cell into the worksheet.
    /// If the cell already exists, returns it.
    /// </summary>
    /// <param name="columnIndex">Column names are letters from english alphabet like A, B, C etc.</param>
    /// <param name="rowIndex">Row index is unsigned int like 1, 2, 3 etc.</param>
    /// <param name="worksheetPart">The worksheet part the cell will be inserted.</param>
    /// <returns></returns>
    private Cell GetOrInsertCell(string columnIndex, uint rowIndex, Worksheet worksheet)
    {
        SheetData sheetData = worksheet.GetFirstChild<SheetData>() ?? worksheet.AppendChild(new SheetData());
        string cellIndex = columnIndex + rowIndex;

        // If the worksheet does not contain the row with the specified index, insert it.
        Row row;
        if (sheetData.Elements<Row>().Where(r => r.RowIndex is not null && r.RowIndex == rowIndex).Count() != 0)
        {
            row = sheetData.Elements<Row>().Where(r => r.RowIndex is not null && r.RowIndex == rowIndex).First();
        }
        else
        {
            row = new Row() { RowIndex = rowIndex };
            sheetData.Append(row);
        }

        // If there is no cell with the specified column index, insert it.
        if (row.Elements<Cell>().Where(c => c.CellReference is not null && c.CellReference.Value == columnIndex + rowIndex).Count() > 0)
        {
            return row.Elements<Cell>().Where(c => c.CellReference is not null && c.CellReference.Value == cellIndex).First();
        }
        else
        {
            // Cells must be in sequential order according to CellReference. Determine where to insert the new cell.
            Cell? refCell = null;

            foreach (Cell cell in row.Elements<Cell>())
            {
                if (string.Compare(cell.CellReference?.Value, cellIndex, true) > 0)
                {
                    refCell = cell;
                    break;
                }
            }

            Cell newCell = new Cell() { CellReference = cellIndex };
            row.InsertBefore(newCell, refCell);

            return newCell;
        }
    }
}
