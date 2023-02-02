namespace Functions;
using Helpers;
using Country;


public class CountryFn
{
    private string[] regions = new string[] {
        "Americas",
        "Europe",
        "Asia",
        "Africa",
        "Oceania"
    };
    private HttpClient _http = new HttpClient();
    private string path = "https://restcountries.com/v3.1/";
    private string fields = "name,region,population,languages,flags";
    public async Task<CommonResponse> GetCountries()
    {
        try
        {
            HttpResponseMessage response = await _http.GetAsync($"{path}all?fields={fields}");
            if (!response.IsSuccessStatusCode)
            {
                return new CommonResponse
                {
                    state = false,
                    data = response.StatusCode,
                    msg = "Error: Not Success",
                };
            }

            var data = await response.Content.ReadFromJsonAsync<List<object>>();

            return new CommonResponse
            {
                state = true,
                data = data,
                msg = "Success"
            };
        }
        catch (Exception e)
        {
            return new CommonResponse
            {
                state = false,
                data = e.Message.ToString(),
                msg = "Error: " + e.InnerException,
            };
        }
    }

    public async Task<CommonResponse> GetCountriesByRegion(string region)
    {
        try
        {
            if (string.IsNullOrEmpty(region))
            {
                return (
                    new CommonResponse
                    {
                        state = false,
                        data = "Bad Request",
                        msg = "Error: region is required"
                    }
                );
            }

            if (!ValidateRegion(region))
            {
                return new CommonResponse
                {
                    state = false,
                    data = "BadRequest",
                    msg = "Error: region is not valid"
                };
            }

            HttpResponseMessage response = await _http.GetAsync($"{path}region/{region}?fields={fields}");
            if (!response.IsSuccessStatusCode)
            {
                return new CommonResponse
                {
                    state = false,
                    data = response.StatusCode,
                    msg = "Error: Not Success",
                };
            }

            var data = await response.Content.ReadFromJsonAsync<List<object>>();

            return new CommonResponse
            {
                state = true,
                data = data,
                msg = "Success"
            };
        }
        catch (Exception e)
        {
            return new CommonResponse
            {
                state = false,
                data = e.Message.ToString(),
                msg = "Error: " + e.InnerException,
            };
        }
    }

    public async Task<CommonResponse> GetCountriesByName(string name)
    {
        try
        {
            if (string.IsNullOrEmpty(name))
            {
                return (
                    new CommonResponse
                    {
                        state = false,
                        data = "Bad Request",
                        msg = "Error: name is required"
                    }
                );
            }

            HttpResponseMessage response = await _http.GetAsync($"{path}name/{name}?fields={fields}");
            if (!response.IsSuccessStatusCode)
            {
                return new CommonResponse
                {
                    state = false,
                    data = response.StatusCode,
                    msg = "Error: Not Success",
                };
            }

            var data = await response.Content.ReadFromJsonAsync<List<object>>();

            return new CommonResponse
            {
                state = true,
                data = data,
                msg = "Success"
            };
        }
        catch (Exception e)
        {
            return new CommonResponse
            {
                state = false,
                data = e.Message.ToString(),
                msg = "Error: " + e.InnerException,
            };
        }
    }

    private bool ValidateRegion(string region)
    {
        return regions.Contains(region) ? true : false;
    }
}