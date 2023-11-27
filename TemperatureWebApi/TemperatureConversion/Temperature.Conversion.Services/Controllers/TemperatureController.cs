using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Temperature.Conversion.Services.Model;
using Temperature.Conversion.Services.Model.Enum;

namespace Temperature.Conversion.Services.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class TemperatureController : ControllerBase
    {

        private readonly ILogger<TemperatureController> _logger;

        public TemperatureController(ILogger<TemperatureController> logger)
        {
            _logger = logger;
        }

        [Route("Convert")]
        [HttpPost]
        public async Task<IActionResult> Convert([FromBody] TemperatureInputModel inputModel)
        {
            var result = new TemperatureResult();

            switch (inputModel.InputType)
            {
                case TemperatureType.Celsius:
                    result.TemperatureC = inputModel.InputDegree;
                    result.TemperatureF = GetFahrenheitFromCelsius(inputModel.InputDegree);
                    result.TemperatureK = GetKelvinFromCelsius(inputModel.InputDegree);
                    break;
                case TemperatureType.Fahrenheit:
                    result.TemperatureC = ((inputModel.InputDegree - 32) * 5) / 9;
                    result.TemperatureF = inputModel.InputDegree;
                    result.TemperatureK = GetKelvinFromCelsius(result.TemperatureC);
                    break;
                case TemperatureType.Kelvin:
                    result.TemperatureC = inputModel.InputDegree - 273.15;
                    result.TemperatureF = GetFahrenheitFromCelsius(result.TemperatureC);
                    result.TemperatureK = inputModel.InputDegree;
                    break;
                default:
                    return StatusCode((int)HttpStatusCode.UnprocessableEntity);
                    _logger.Log(LogLevel.Information, "Temperature/Convert has been called from user");
            }

            return Ok(result);

        }

        private double GetFahrenheitFromCelsius(double celsius)
        {
            return 32 + ((celsius * 9) / 5);
        }

        private double GetKelvinFromCelsius(double celsius)
        {
            return celsius + 273.15;
        }

    }
}
