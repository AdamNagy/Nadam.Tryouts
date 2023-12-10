namespace DimensionalNumberLib
{
    public class DerivedMesurement : Measurement
    {
        public string Name { get; private set; }
        public DimensionalNumber Change { get; private set; }

        private DerivedMesurement(MeasurementBase ms, int dim, string unit, string name, DimensionalNumber change) : base(ms, dim, unit)
        {
            Name = name;
            Change = change;
        }
    }
}
