namespace Infrastructure.OpenXML;

/// <summary>
/// Represent one column in the table.
/// </summary>
/// <param name="Index">The alphabetical index of the column. Eg.: A, B, C etc...</param>
/// <param name="NumericalIndex">The indexes index in the alphabet. Eg.: A => 1, B => 2 C => 3 etc...</param>
public record ColumnIndex(string Index, int NumericalIndex) : IComparable<ColumnIndex>
{
    public override string ToString() => Index;

    /// <summary>
    /// Utility factory method to be able to create 'ColumnIndex' from string which contains one or later two letters.
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static ColumnIndex From(string index)
    {
        var letterIdx = Utils.ALphabet.IndexOf(index.ToLower());

        if (letterIdx < 0)
        {
            throw new ArgumentException("Wrong index. Where did you get that?");
        }

        if (letterIdx == Utils.ALphabet.Count)
        {
            throw new ArgumentException("You have reached the end of everithing...");
        }

        return new ColumnIndex(Utils.ALphabet[letterIdx], letterIdx);

    }

    public int CompareTo(ColumnIndex? other)
    {
        if(other == null)
        {
            throw new ArgumentException($"{nameof(other)} is null.");
        }

        if (NumericalIndex > other.NumericalIndex) return 1;
        if (NumericalIndex < other.NumericalIndex) return -1;

        return 0;
    }

    public static ColumnIndex operator ++(ColumnIndex a)
    {
        var letterIdx = Utils.ALphabet.IndexOf(a.Index.ToLower());

        if(letterIdx < 0 )
        {
            throw new ArgumentException("Wrong index. Where did you get that?");
        }

        if(letterIdx == Utils.ALphabet.Count - 1)
        {
            throw new InvalidOperationException("You have reached the end of everithing...");
        }

        var idx = ++letterIdx;
        return  new ColumnIndex(Utils.ALphabet[idx], idx);
    }

    public static ColumnIndex operator +(ColumnIndex a, int b)
    {
        if(b < 1)
        {
            throw new ArgumentException("Negative number is not supported.");
        }

        var letterIdx = Utils.ALphabet.IndexOf(a.Index.ToLower());

        var newIdx = letterIdx + b;

        if (newIdx >= Utils.ALphabet.Count)
        {
            throw new ArgumentException("You have reached the end of everithing...");
        }

        return new ColumnIndex(Utils.ALphabet[newIdx], newIdx);
    }

    public static implicit operator string(ColumnIndex d) => d.Index;
    public static explicit operator ColumnIndex(string b) => From(b);
}
