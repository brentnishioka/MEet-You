namespace Pentaskilled.MEetAndYou.DataAccess.Contracts
{
    public interface IWeatherDAO
    {
        string GeoCoderAPI(string cityName, string countryName);

        string OneCallAPI(string latitude, string longitude);
    }
}
