using Infrastructure.OpenXML;

var excel = new ExcelDocument(@"G:\git\Nadam.Tryouts\DotNet\OpenXML\TestData\file_example_XLSX_100.xlsx");
excel.Open();

var reader = excel.GetReader();

Console.WriteLine("Sheets in the file");
foreach (var item in excel.SheetNames)
{
    Console.WriteLine(item);
}

Console.WriteLine("First 10 row");
foreach (var row in reader.ReadRows().Take(10))
{
    foreach (var column in row)
    {
        Console.Write($"{column.Key}: {column.Value}; ");
    }

    Console.WriteLine();
}

var writer = excel.GetWriter();

var columnToInsert = writer.GetFirstEmptyColumn(2);
var dataToInsert = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum".Split(' ');

writer.InsertColumn(columnToInsert, dataToInsert.Take(10));

excel.Close();

Console.WriteLine($"{Environment.NewLine}Done, press any key..");
Console.ReadKey();
