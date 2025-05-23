using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

namespace Infrastructure.OpenXML;

/// <summary>
/// Represents one Excel file.
/// </summary>
public class ExcelDocument
{
    private readonly string _path;
    private readonly List<ExcelSheet> _sheets = new List<ExcelSheet>();

    private SpreadsheetDocument _document;

    public IEnumerable<ExcelSheet> Sheets { get => _sheets.AsReadOnly(); }
    public IEnumerable<string> SheetNames { get => _sheets.Select(p => p.Name); }

    public ExcelDocument(string path)
    {
        _path = path;
    }

    public void Open()
    {
        // Structure of the Excel: https://learn.microsoft.com/en-us/office/open-xml/spreadsheet/structure-of-a-spreadsheetml-document?tabs=cs
        _document = SpreadsheetDocument.Open(_path, true);
        
        var workbookPart = _document.WorkbookPart ?? _document.AddWorkbookPart();
        // var sharedStringTablePart = GetSharedStringTable(workbookPart);

        var sheets = workbookPart.Workbook.Descendants<Sheet>();

        if(sheets == null || !sheets.Any())
        {
            throw new Exception("Excel file is empty.");
        }

        foreach (var sheet in sheets)
        {
            var firstId = sheet.Id;
            if (firstId is null)
            {
                continue;
            }

            WorksheetPart worksheetPart = (WorksheetPart)workbookPart.GetPartById(firstId!);
            _sheets.Add(new ExcelSheet(sheet.Name, workbookPart, worksheetPart));
        }        
    }

    public void Close()
    {
        _document.Dispose();
    }

    public ExcelReader GetReader()
    {
        return new ExcelReader(_sheets.First());
    }

    public ExcelWriter GetWriter()
    {
        return new ExcelWriter(_sheets.First());
    }
}
