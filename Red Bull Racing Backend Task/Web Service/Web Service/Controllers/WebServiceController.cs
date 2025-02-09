using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[Route("api/standings")]
[ApiController]
public class StandingsController : ControllerBase
{
    private static readonly HttpClient _httpClient = new HttpClient();
    private const string ApiUrl = "https://pitwall.redbullracing.com/api/standings/drivers/";
    private const string ApiKey = "7303c8ef-d91a-4964-a7e7-78c26ee17ec4";

    [HttpGet("drivers/{season}")]
    public async Task<IActionResult> GetDriverStandings(int season = 2024)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"{ApiUrl}{season}");
        request.Headers.Add("x-api-key", ApiKey);

        var response = await _httpClient.SendAsync(request);
        if (!response.IsSuccessStatusCode)
        {
            return StatusCode((int)response.StatusCode, "Failed to fetch data");
        }

        return Content(await response.Content.ReadAsStringAsync(), "application/json");
    }
}
