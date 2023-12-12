using System.ComponentModel.DataAnnotations;

namespace SQLiteDemoTests
{
    public class TestModel : IComparable<TestModel>
    {
        [Key]
        public int Key { get; set; }
        public int GreaterThanSome { get; set; }
        public int GreaterThanAll { get; set; }

        public DateTime DateTimeProp { get; set; }
        public Double DoubleProp { get; set; }

        public string StartsWithSomeProp { get; set; }
        public string StartsWithAllProp { get; set; }

        public string EndsWithSomeProp { get; set; }
        public string EndsWithAllProp { get; set; }

        public string ContainsSomeProp { get; set; }
        public string ContainsAllProp { get; set; }

        public int CompareTo(TestModel? other)
        {
            if (GreaterThanSome == other.GreaterThanSome) return 0;

            return GreaterThanSome < other.GreaterThanSome ? -1 : 1;
        }

        public static IEnumerable<TestModel> GenerateData()
             => Enumerable.Range(0, 100).Select(p => new TestModel()
             {
                 GreaterThanSome = p,
                 GreaterThanAll = p + 51,
                 DateTimeProp = DateTime.Now.AddDays(p * 10 * -1),
                 DoubleProp = Double.Parse($"{(p / 10)}.{p % 10}".TrimEnd('0')),
                 StartsWithSomeProp = $"{p % 5} and some text 4",
                 StartsWithAllProp = $"4 and some text 4",
                 ContainsAllProp = $"{p % 5} and some text 4 some other text",
                 ContainsSomeProp = $"before text {p % 5} and some after text",
                 EndsWithAllProp = $"some text before 4",
                 EndsWithSomeProp = $"before text {p % 5}"
             });
    }
}
