using Temperature.Conversion.Services.Model.Enum;

namespace Temperature.Conversion.Services.Model
{
    public class TemperatureResult
    {
        private double _temperatureC;
        private double _temperatureF;
        private double _temperatureK;
        public double TemperatureC {
            get { return _temperatureC; }
            set {
                _temperatureC = Math.Round(value, 2);
            } 
        }

        public double TemperatureF {
            get
            {
                return _temperatureF;
            }
            set
            {
                _temperatureF = Math.Round(value, 2);
            }
        }

        public double TemperatureK {
            get
            {
                return _temperatureK;
            }
            set
            {
                _temperatureK = Math.Round(value, 2); ;
            }
        }

    }
}
