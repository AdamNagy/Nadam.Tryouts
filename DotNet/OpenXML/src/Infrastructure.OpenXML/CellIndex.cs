namespace Infrastructure.OpenXML;

/// <summary>
/// Represents an index for one single cell wit its column and row index.
/// </summary>
/// <param name="Column">The index of the column.</param>
/// <param name="Row">The index of the row.</param>
public record CellIndex(ColumnIndex Column, uint Row)
{
    public static CellIndex From(string column, uint row)
        => new CellIndex(ColumnIndex.From(column), row);

    public static CellIndex From(string index)
    {
        var column = Utils.GetColumnIndexFromCellIndex(index);
        var row = Utils.GetRowIndexFromCellIndex(index); 
        
        return new CellIndex(column, row);
    }

    public CellIndex IncrementRow()
        => new CellIndex(Column, Row + 1);

    public CellIndex IncrementColumn()
        => new CellIndex(Column + 1, Row);
}

