using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TicketMaster.Models;
using Microsoft.Extensions.Configuration;

namespace TicketMaster.Services
{
    public class BeverageService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<BeverageService> _logger;

        public BeverageService(HttpClient httpClient, ILogger<BeverageService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<IEnumerable<Beverage>> GetBeveragesAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<IEnumerable<Beverage>>("api/beverages");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching beverages.");
                throw;
            }
        }

        public async Task<Beverage> GetBeverageAsync(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<Beverage>($"api/beverages/{id}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while fetching the beverage with ID {id}.");
                throw;
            }
        }

        public async Task<Beverage> CreateBeverageAsync(Beverage beverage)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/beverages", beverage);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<Beverage>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a new beverage.");
                throw;
            }
        }

        public async Task UpdateBeverageAsync(Beverage beverage)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/beverages/{beverage.Id}", beverage);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while updating the beverage with ID {beverage.Id}.");
                throw;
            }
        }

        public async Task DeleteBeverageAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/beverages/{id}");
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while deleting the beverage with ID {id}.");
                throw;
            }
        }
    }
}
