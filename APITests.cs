namespace seng3020_integration_lab;

public class ApiTests
{
    private HttpClient _client;
    private const string BaseUrl = "YOUR_API_BASE_URL";

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
    public async Task Get_ReturnsSuccessStatusCode()
    {
        var response = await _client.GetAsync($"{BaseUrl}/");
        Assert.That(response.IsSuccessStatusCode, Is.True);
    }
}