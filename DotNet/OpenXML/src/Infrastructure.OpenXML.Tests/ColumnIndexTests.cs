namespace Infrastructure.OpenXML.Tests;

public class ColumnIndexTests
{
    [Fact]
    public void Valid1()
    {
        var idx = ColumnIndex.From("a");

        Assert.Equal("a", idx);
    }

    [Fact]
    public void Valid2()
    {
        var idx = ColumnIndex.From("z");

        Assert.Equal("z", idx);
    }

    [Fact]
    public void Valid3()
    {
        var idx = ColumnIndex.From("g");

        Assert.Equal("g", idx);
    }

    [Fact]
    public void Invalid1()
    {
        Assert.Throws<ArgumentException>(() => ColumnIndex.From("1"));
    }

    [Fact]
    public void Invalid2()
    {
        Assert.Throws<ArgumentException>(() => ColumnIndex.From("ab"));
    }

    [Fact]
    public void ValidIncrement1()
    {
        var idx = ColumnIndex.From("g");

        Assert.Equal("h", ++idx);
    }

    [Fact]
    public void InvalidIncrement1()
    {
        var idx = ColumnIndex.From("z");

        Assert.Throws<InvalidOperationException>(() => ++idx);
    }
}
