using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage(
    "Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores",
    Justification = "External API requires parameter name as is",
    Scope = "member",
    Target = "Facade.Logic.BingMapsAndOpenWeather.WeatherForecastApi.WeatherForecastForMoment.wind_speed")]