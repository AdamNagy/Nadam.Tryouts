namespace DimensionalNumberLib
{
    public static class DimensionalNumberFactory
    {
        public static DimensionalNumberPlant Constant { get; } = new DimensionalNumberPlant(Measurement.Constant);
        public static DimensionalNumberPlant Length { get; } = new DimensionalNumberPlant(Measurement.Length);
        public static DimensionalNumberPlant Square { get; } = new DimensionalNumberPlant(Measurement.Square);
        public static DimensionalNumberPlant Kubic { get; } = new DimensionalNumberPlant(Measurement.Kubic);
        public static DimensionalNumberPlant Mass { get; } = new DimensionalNumberPlant(Measurement.Mass);
    }

    public class DimensionalNumberPlant
    {
        private readonly Measurement _mesurement;

        public DimensionalNumberPlant(Measurement ms)
        {
            _mesurement = ms;
        }

        public DimensionalNumber Create(Double value)
            => new DimensionalNumber(value, _mesurement);

        public DimensionalNumber Create(Double value, Prefix prefix)
            => new DimensionalNumber(value, _mesurement, prefix);
    }
}
