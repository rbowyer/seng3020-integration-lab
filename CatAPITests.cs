namespace seng3020_integration_lab;

public class CatApiTests
{
    private HttpClient _client;
    private const string BaseUrl = "https://api.thecatapi.com/v1";
    private const string ApiKey = "live_Cwl8NLYUMa6Hsm3JIVmY4dw4yQR0UEt3nXzsJB0UsbdbU6J2pHVvH2h6zX5oIDhZ"; 

    [SetUp]
    public void Setup()
    {
        _client = new HttpClient();
    }

    [TearDown]
    public void TearDown()
    {
        _client.Dispose();
    }

    [Test]
    public async Task GetCatImage_WithApiKey_ReturnsImageUrl()
    {
        _client.DefaultRequestHeaders.Add("x-api-key", ApiKey);

        var response = await _client.GetAsync($"{BaseUrl}/images/search");
        var json = await response.Content.ReadAsStringAsync();
        var results = System.Text.Json.JsonSerializer.Deserialize<List<CatApiResponse>>(json,
            new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        Assert.Multiple(() =>
        {
            Assert.That(response.IsSuccessStatusCode, Is.True);
            Assert.That(results, Is.Not.Null);
            Assert.That(results?[0].Url, Does.StartWith("https://"));
        });
    }

    [Test]
public async Task GetVotes_WithoutApiKey_ReturnsUnauthorized()
{
    var response = await _client.GetAsync($"{BaseUrl}/votes");
    Assert.That(response.StatusCode, 
        Is.EqualTo(System.Net.HttpStatusCode.Unauthorized));
}
}

public class CatApiResponse
{
    public string? Id { get; set; }
    public string? Url { get; set; }
}