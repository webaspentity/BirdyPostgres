using Birdy.Shared.Enums;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Net.Http.Json;
using System.Text.Json;

namespace Birdy.Client.Infrastructure;

public class CartService
{
    public CartService(ILocalStorageService localStorageService, IJSRuntime js, HttpClient http)
    {
        this.localStorageService = localStorageService;
        this.JS = js;
        this.HttpClient = http;
    }

    [Inject] ILocalStorageService localStorageService { get; set; }
    [Inject] HttpClient HttpClient { get; set; }
    [Inject] IJSRuntime JS { get; set; }
    JsonSerializerOptions options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

    public async Task<CartItem?> AddItemAsync(CartItem item)
    {
        var response = await HttpClient.PostAsJsonAsync($"{AppData.BaseUri}/api/cart/add", item);
        
        if (response.IsSuccessStatusCode) {
            var result = await response.Content.ReadAsStringAsync();
            CartItem? newItem = JsonSerializer.Deserialize<CartItem>(result, options);
            return newItem;
        }
        else
        {
            return null;
        }
    }

    public async Task<int> DeleteItemAsync(int id)
    {
        var request = new HttpRequestMessage(HttpMethod.Delete, $"{AppData.BaseUri}/api/cart/delete/{id}");
        var response = await HttpClient.SendAsync(request);
        if (response.IsSuccessStatusCode)
        {
            return 1;
        }
        else
        {
            return 0;
        }
        
    }

    public async Task<int> IncrementQuantityAsync(int id)
    {
        var request = new HttpRequestMessage(HttpMethod.Patch, $"{AppData.BaseUri}/api/cart/increment/{id}");
        var response = await HttpClient.SendAsync(request);

        if (response.IsSuccessStatusCode)
        {
            await UpdateCounter();
            return 1;
        }
        else
        {
            return 0;
        }
    }

    public async Task<int> DecrementQuantityAsync(int id)
    {
        var request = new HttpRequestMessage(HttpMethod.Patch, $"{AppData.BaseUri}/api/cart/decrement/{id}");
        var response = await HttpClient.SendAsync(request);
        
        if (response.IsSuccessStatusCode)
        {
            await UpdateCounter();
            return 1;
        }
        else
        {
            return 0;
        }
    }
    
    public async Task UpdateCounter()
    {
        Token? token = await localStorageService.GetAsync<Token>(nameof(Token));

        if (token.Role.Equals(UserRole.User))
        {
            if (token.Cart?.Items is not null)
            {
                int count = token.Cart.Items.Count;
                await JS.InvokeVoidAsync("setCartCounter", count);

            }
            else
            {
                await JS.InvokeVoidAsync("setCartCounter", 0);
            }
        }
    }

    public async Task<decimal> GetTotalCostAsync(int cartId)
    {
        var response = await HttpClient.GetFromJsonAsync<decimal>($"api/cart/total/{cartId}");
        return response;
    }

    public async Task<List<CartItem>?> GetCartItemsByIdAsync(int cartId)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"{AppData.BaseUri}/api/cart/get/{cartId}");
        var response = await HttpClient.SendAsync(request);
        
        if (response.IsSuccessStatusCode)
        {
            List<CartItem>? items = await response.Content.ReadFromJsonAsync<List<CartItem>>(options);
            return items;
        }
        else
        {
            return new();
        }
    }

    public async Task<int> GetCartItemQuantity(int productId)
    {
        Token? token = await localStorageService.GetAsync<Token>(nameof(Token));

        if (token is not null && token.Role.Equals(UserRole.User))
        {
            if (token.Cart is not null)
            {
                if (token.Cart.Items is not null)
                {
                    CartItem item = token.Cart.Items.FirstOrDefault(ci => ci.ProductId == productId);
                    if (item is not null)
                    {
                        return item.Quantity;
                    }
                    else return 0;
                } else return 0;
            }else return 0;
        }else return 0;
    }
}
