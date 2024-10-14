namespace TicketMaster.Services
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;
    using TicketMaster.Models;

    public class ApiService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ApiService> _logger;

        public ApiService(HttpClient httpClient, ILogger<ApiService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<IEnumerable<Ticket>> GetTicketsAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<IEnumerable<Ticket>>("api/tickets");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching tickets.");
                throw;
            }
        }

        public async Task<Ticket> GetTicketAsync(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<Ticket>($"api/tickets/{id}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while fetching the ticket with ID {id}.");
                throw;
            }
        }

        public async Task<Ticket> CreateTicketAsync(Ticket ticket)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/tickets", ticket);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<Ticket>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a new ticket.");
                throw;
            }
        }

        public async Task UpdateTicketAsync(Ticket ticket)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/tickets/{ticket.Id}", ticket);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while updating the ticket with ID {ticket.Id}.");
                throw;
            }
        }

        public async Task DeleteTicketAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/tickets/{id}");
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while deleting the ticket with ID {id}.");
                throw;
            }
        }
    }
}
