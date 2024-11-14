using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TicketMaster.Models;

namespace TicketMaster.Services
{
    public class CartService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<CartService> _logger;

        public CartService(HttpClient httpClient, ILogger<CartService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<IEnumerable<Cart>> GetCartsAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<IEnumerable<Cart>>("https://ticketmasterapi-mugk.onrender.com/api/carts");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching carts.");
                throw;
            }
        }

        public async Task<Cart> GetCartAsync(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<Cart>($"https://ticketmasterapi-mugk.onrender.com/api/carts/{id}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while fetching the cart with ID {id}.");
                throw;
            }
        }

        public async Task<Cart> CreateCartAsync(Cart cart)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("https://ticketmasterapi-mugk.onrender.com/api/carts", cart);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<Cart>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a new cart.");
                throw;
            }
        }

        public async Task UpdateCartAsync(Cart cart)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"https://ticketmasterapi-mugk.onrender.com/api/carts/{cart.Id}", cart);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while updating the cart with ID {cart.Id}.");
                throw;
            }
        }

        public async Task DeleteCartAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"https://ticketmasterapi-mugk.onrender.com/api/carts/{id}");
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while deleting the cart with ID {id}.");
                throw;
            }
        }
    }
}
