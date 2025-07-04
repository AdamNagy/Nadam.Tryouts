namespace Infrastructure.OpenXML.Tests;

public class CellIndexTests
{
    [Fact]
    public void IncrementColumn1()
    {
        var idx = CellIndex.From("a", 1);
        var idx2 = idx.IncrementRow();

        Assert.Equal((uint)2, idx2.Row);
        Assert.Equal("a", idx2.Column);
    }

    [Fact] public void From1() 
    {
        var idx = CellIndex.From("a1");

        Assert.Equal((uint)1, idx.Row);
        Assert.Equal("a", idx.Column);
    }

    [Fact]
    public void From2()
    {
        var idx = CellIndex.From("d123");

        Assert.Equal((uint)123, idx.Row);
        Assert.Equal("d", idx.Column);
    }
}
