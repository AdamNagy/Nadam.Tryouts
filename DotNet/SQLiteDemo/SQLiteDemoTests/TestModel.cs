namespace SQLiteDemoTests
{
    internal class TestModel : IComparable<TestModel>
    {
        public int IntProp { get; set; }
        public DateTime DateTimeProp { get; set; }

        public int CompareTo(TestModel? other)
        {
            if (IntProp == other.IntProp) return 0;

            return IntProp < other.IntProp ? -1 : 1;
        }
    }
}
