namespace Nadam.DataServices.Tests.EntityModels
{
    public class OrderingModel
    {
        public int IntProp { get; set; }
        public Double DoubleProp { get; set; }
        public string TextProp { get; set; }
        public DateTime DateTimeProp { get; set; }

        public static IEnumerable<OrderingModel> GenerateData()
            => Enumerable.Range(0, 100).Select(p => new OrderingModel()
            {
                IntProp = p,
                DoubleProp = double.Parse($"{p / 10}.{p % 10}".TrimEnd('0')),
                DateTimeProp = DateTime.Now.AddDays(p * 10 * -1),
                TextProp = $"{p % 5} and some text 4"
            });
    }
}
