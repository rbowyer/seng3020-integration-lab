namespace seng3020_integration_lab;

public class DogApiTests
{
    private HttpClient _client;
    private const string BaseUrl = "https://dog.ceo/api";

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
    public async Task GetRandomDog_ReturnsSuccessStatusCode()
    {
        var response = await _client.GetAsync($"{BaseUrl}/breeds/image/random");
        Assert.That(response.IsSuccessStatusCode, Is.True);
    }

    [Test]
public async Task GetRandomDog_ResponseContainsImageUrl()
{
    var response = await _client.GetAsync($"{BaseUrl}/breeds/image/random");
    var json = await response.Content.ReadAsStringAsync();
    var result = System.Text.Json.JsonSerializer.Deserialize<DogApiResponse>(json, 
        new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true });

    Assert.Multiple(() =>
{
    Assert.That(result, Is.Not.Null);
    Assert.That(result?.Status, Is.EqualTo("success"));
    Assert.That(result?.Message, Does.StartWith("https://"));
});
}
}
public class DogApiResponse
{
    public string? Message { get; set; }
    public string? Status { get; set; }
}