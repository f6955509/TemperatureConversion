# TemperatureConversion - Web API
This can convert temperature.
1. Web API is under the folder 'TemperatureWebApi/TemperatureConversion', please get files under 'TemperatureConversion' folder.
2. Open solution in Visual Studio since this is developed under .NET 6.
3. Build and Run WebService.
4. WebAPI URL will be displayed 'https://localhost:7272/swagger/index.html'.
5. Customer can access Web API via passing json data like below.

    {

   "inputType":0, //Celsius = 0,
        //Fahrenheit = 1,
        //Kelvin = 2
   
    "inputDegree":55

   }
6. Log will be auto generated under folder 'Temperature.Conversion.Services' and named 'audit.log'
7. Log is generated in middleware, which path is '.\TemperatureWebApi\TemperatureConversion\Temperature.Conversion.Services\Middleware\AuditLogMiddleware.cs'


# TemperatureConversion - Web UI, Angular project
Front-End project is under the folder 'temperature_convertor', please get 'temperature_convertor' folder and follow below instruction to run angular project

This project was generated with [Angular CLI](https://github.com/angular/angular-cli) version 15.0.3.

## Install node modules 
Run `npm install` for install all libraries defined in package.json
## Development server

1. Run `ng serve` for a dev server. Navigate to `http://localhost:4200/`. The application will automatically reload if you change any of the source files.
2. Open website 'http://localhost:4200/' and then you can input temperature number and click button to convert.
