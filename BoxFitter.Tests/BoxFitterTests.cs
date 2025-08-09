using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using Newtonsoft.Json.Linq;

namespace BoxFitter.Tests;

public class BoxFitterTests
{
    private const string Endpoint = "/pack-box";
    private const string Port = "5001";
    private const string ApplicationUrl = "http://localhost";
    private readonly HttpClient BoxFitterClient;

    public BoxFitterTests()
    {
        BoxFitterClient = new HttpClient();
        BoxFitterClient.BaseAddress = new Uri($"{ApplicationUrl}:{Port}");
    }
    
    [Fact]
    void ShouldPackBoxes()
    {
        // GIVEN an order of books
        // WHEN the order is submitted
        // THEN the orderer should receive a list of boxes
        // AND the contents of each box should be enumerated
    }
    
    [Fact]
    async Task ShouldPackABoxAsync()
    {
        // GIVEN an order with one book
        var order = CreateOrder("this book");

        // WHEN the order is submitted
        var packingList = await SubmitOrder(order);

        // THEN the orderer should receive a list with one box
        // AND the contents of that box should be that book
        PackingListShouldContain(order, packingList);
    }

    private static List<string> CreateOrder(string book)
    {
        var order = new List<string> { book };
        return order;
    }

    private async Task<List<string>> SubmitOrder(List<string> order)
    {
        using StringContent requestBody = new(JsonSerializer.Serialize(order), Encoding.UTF8, "application/json");
        using var response = await BoxFitterClient.PostAsync(Endpoint, requestBody);
        var book = await response.Content.ReadAsStringAsync();
        var packingList = new List<string> { book };
        return packingList;
    }

    private static void PackingListShouldContain(List<string> order, List<string> packingList)
    {
        Assert.Equal(order, packingList);
    }
}