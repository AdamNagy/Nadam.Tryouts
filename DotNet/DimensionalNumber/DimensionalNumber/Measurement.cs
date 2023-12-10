namespace DimensionalNumberLib
{
    public enum MeasurementBase
    {
        Length, Square, Kubic, Mass, Quantity,
        Constant
    }

    public class Measurement
    {
        public MeasurementBase MesurementBase { get; private set; }
        public int Dimension { get; private set; }
        public string Unit { get; private set; }

        protected Measurement(MeasurementBase ms, int dim, string unit)
        {
            MesurementBase = ms;
            Dimension = dim;
            Unit = unit;
        }

        public static Measurement Constant { get; } = new Measurement(MeasurementBase.Constant, 1, "");

        public static Measurement Length { get; } = new Measurement(MeasurementBase.Length, 1, "m");
        public static Measurement Square { get; } = new Measurement(MeasurementBase.Square, 2, "m2");
        public static Measurement Kubic { get; } = new Measurement(MeasurementBase.Kubic, 3, "m3");
        public static Measurement Mass { get; } = new Measurement(MeasurementBase.Length, 1, "g");

        public static Measurement Quantity { get; } = new Measurement(MeasurementBase.Quantity, 1, "#");
    }
}
