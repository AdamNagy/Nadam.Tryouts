using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

using static Infrastructure.OpenXML.Utils;

namespace Infrastructure.OpenXML;

/// <summary>
/// Represents one Excel sheet and its closely related objets.
/// </summary>
public class ExcelSheet
{
    private readonly WorkbookPart _workbook;
    private readonly WorksheetPart _sheet;
    private SharedStringTablePart? _sharedStringTable;

    public Worksheet WorkSheet { get => _sheet.Worksheet; }
    public SharedStringTablePart SharedStringTablePart { 
        get 
        {
            if(_sharedStringTable != null) return _sharedStringTable;

            _sharedStringTable = GetSharedStringTable(_workbook);
            return _sharedStringTable;
        } 
    }

    public string Name { get; private set; }

    public ExcelSheet(string name, WorkbookPart workbook, WorksheetPart sheet)
    {
        _workbook = workbook;
        _sheet = sheet;
        Name = name;
    }
}
