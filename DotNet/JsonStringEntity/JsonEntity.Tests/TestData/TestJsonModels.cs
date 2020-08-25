using System.Collections.Generic;

namespace TestData
{
    public class TestJsonModel
    {
        public string StringProp { get; set; }
        public int NumberProp { get; set; }
        public TestJsonModel2 ComplexProp { get; set; }

        public IEnumerable<int> NumberArrayProp { get; set; }
        public IEnumerable<string> StringArrayProp { get; set; }
        public IEnumerable<TestJsonModel2> ComplexArrayProp { get; set; }

        public static TestJsonModel GetDefault()
        {
            var model = new TestJsonModel();
            model.StringProp = MockData.TEXTS[0];
            model.NumberProp= MockData.NUMBERS[0];
            model.ComplexProp = TestJsonModel2.GetDefault();
            // bool
            // date

            model.NumberArrayProp = MockData.NUMBERS_ARRAY1;
            model.StringArrayProp = MockData.STRING_ARRAY1;
            model.ComplexArrayProp = new List<TestJsonModel2>(3)
            {
                TestJsonModel2.GetDefault(),TestJsonModel2.GetDefault(),TestJsonModel2.GetDefault()
            };

            return model;
        }
    }

    public class TestJsonModel2
    {
        public string StringProp1 { get; set; }
        public string StringProp2 { get; set; }

        public int IntProp1 { get; set; }
        public int IntProp2 { get; set; }

        public IEnumerable<int> IntArrayProp1 { get; set; }
        public IEnumerable<int> IntArrayProp2{ get; set; }

        public IEnumerable<string> StringArrayProp1 { get; set; }
        public IEnumerable<string> StringArrayProp2 { get; set; }

        public static TestJsonModel2 GetDefault()
        {
            var model = new TestJsonModel2();
            model.StringProp1 = MockData.TEXTS[1];
            model.StringProp2 = MockData.TEXTS[2];

            model.IntProp1 = MockData.NUMBERS[1];
            model.IntProp2 = MockData.NUMBERS[2]; ;

            model.StringArrayProp1 = MockData.STRING_ARRAY2;
            model.StringArrayProp2 = MockData.STRING_ARRAY3;

            model.IntArrayProp1 = MockData.NUMBERS_ARRAY2;
            model.IntArrayProp2 = MockData.NUMBERS_ARRAY3;

            return model;
        }
    }
}
