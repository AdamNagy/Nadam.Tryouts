// See https://aka.ms/new-console-template for more information
using System.Globalization;

Console.WriteLine("Hello, World!");

var sumPayment = 0;
var value = 4184050;

var hungarian = new CultureInfo("hu-HU");

foreach (var line in File.ReadAllLines(@"H:\Documents\Pénzügyek\Allianz_lifeInsurance.txt"))
{
    if (line == "Díjrendezett") continue;

    if(DateTime.TryParse(line, out var date))
    {
        //Console.Write(date);
        continue;
    }

    if(line.Trim().StartsWith("-"))
    {
        var payment = line.TrimStart('-').Replace(" ", "");
        payment = payment.Substring(0, payment.Length - 2);
        //Console.WriteLine($" {payment}");

        sumPayment += int.Parse(payment);
    }
}

Console.WriteLine($"Sum payment: {sumPayment.ToString(hungarian.NumberFormat)}{Environment.NewLine}" +
    $"Value: {value}{Environment.NewLine}" +
    $"Diff: {value-sumPayment}");
