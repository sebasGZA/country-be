using Microsoft.AspNetCore.Mvc;
using Functions;
using Helpers;

namespace Country.Controllers;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public class CountryController : ControllerBase
{
    private CountryFn _countryFn;

    private readonly ILogger<CountryController> _logger;

    public CountryController(ILogger<CountryController> logger)
    {
        _logger = logger;
        _countryFn = new CountryFn();
    }

    [HttpGet]
    public async Task<ActionResult<CommonResponse>> GetCountries()
    {
        try
        {
            return Ok(await _countryFn.GetCountries());
        }
        catch (Exception e)
        {
            return StatusCode(
                500,
                new CommonResponse
                {
                    state = false,
                    msg = $"Error GetCountries en CountryController: {e.Message}",
                }
            );
        }
    }

    [HttpGet]
    [Route("region/{region}")]
    public async Task<ActionResult<CommonResponse>> GetCountriesByRegion([FromRoute] string region)
    {
        try
        {
            CommonResponse response = await _countryFn.GetCountriesByRegion(region);
            if (!response.state)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        catch (Exception e)
        {
            return StatusCode(
                500,
                new CommonResponse
                {
                    state = false,
                    msg = $"Error GetCountriesByRegion en CountryController: {e.Message}",
                }
            );
        }
    }

    [HttpGet]
    [Route("name/{name}")]
    public async Task<ActionResult<CommonResponse>> GetCountriesByName([FromRoute] string name)
    {
        try
        {
            CommonResponse response = await _countryFn.GetCountriesByName(name);
            if (!response.state)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        catch (Exception e)
        {
            return StatusCode(
                500,
                new CommonResponse
                {
                    state = false,
                    msg = $"Error GetCountriesByName en CountryController: {e.Message}",
                }
            );
        }
    }
}
