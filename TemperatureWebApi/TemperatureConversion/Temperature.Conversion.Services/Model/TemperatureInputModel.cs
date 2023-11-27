using Temperature.Conversion.Services.Model.Enum;

namespace Temperature.Conversion.Services.Model
{
    public class TemperatureInputModel
    {
        public TemperatureType InputType { get; set; }
        public double InputDegree { get; set; }
    }
}
