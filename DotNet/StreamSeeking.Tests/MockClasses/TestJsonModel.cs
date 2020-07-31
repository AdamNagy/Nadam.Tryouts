using System.Collections.Generic;

namespace StreamSeeking.Tests.MockClasses
{
    public class TestJsonModel
    {
        public string StringProp { get; set; }
        public int NumberProp { get; set; }
        public ComplexJsonType ComplexProp { get; set; }

        public IEnumerable<int> NumberArrayProp { get; set; }
        public IEnumerable<string> StringArrayProp { get; set; }
        public IEnumerable<ComplexJsonType> ComplexArrayProp { get; set; }

        public static TestJsonModel GetDefault()
        {
            var model = new TestJsonModel();
            model.StringProp = MockData.MOCK_TEXT[0];
            model.NumberProp= MockData.MOCK_NUMBERS[0];
            model.ComplexProp = ComplexJsonType.GetDefault();

            model.NumberArrayProp = MockData.MOCK_NUMBERS_ARRAY1;
            model.StringArrayProp = MockData.MOCK_STRING_ARRAY1;
            model.ComplexArrayProp = new List<ComplexJsonType>(3)
            {
                ComplexJsonType.GetDefault(),ComplexJsonType.GetDefault(),ComplexJsonType.GetDefault()
            };

            return model;
        }
    }

    public class ComplexJsonType
    {
        public string StringProp1 { get; set; }
        public string StringProp2 { get; set; }

        public int IntProp1 { get; set; }
        public int IntProp2 { get; set; }

        public IEnumerable<int> IntArrayProp1 { get; set; }
        public IEnumerable<int> IntArrayProp2{ get; set; }

        public IEnumerable<string> StringArrayProp1 { get; set; }
        public IEnumerable<string> StringArrayProp2 { get; set; }

        public static ComplexJsonType GetDefault()
        {
            var model = new ComplexJsonType();
            model.StringProp1 = MockData.MOCK_TEXT[1];
            model.StringProp2 = MockData.MOCK_TEXT[2];

            model.IntProp1 = MockData.MOCK_NUMBERS[1];
            model.IntProp2 = MockData.MOCK_NUMBERS[2]; ;

            model.StringArrayProp1 = MockData.MOCK_STRING_ARRAY2;
            model.StringArrayProp2 = MockData.MOCK_STRING_ARRAY3;

            model.IntArrayProp1 = MockData.MOCK_NUMBERS_ARRAY2;
            model.IntArrayProp2 = MockData.MOCK_NUMBERS_ARRAY3;

            return model;
        }
    }
}
