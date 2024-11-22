using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketMaster.Models;
using TicketMaster.Services;



namespace TicketMaster.Controllers
{
    public class TicketsController : Controller
    {
        private readonly ApiService _apiService;

        public TicketsController(ApiService apiService)
        {
            _apiService = apiService;
        }


        public async Task<IActionResult> Index()
        {
            var tickets = await _apiService.GetTicketsAsync();
            return View(tickets);
        }

        // GET: Tickets/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tickets/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                await _apiService.CreateTicketAsync(ticket);
                return RedirectToAction(nameof(Index));
            }
            return View(ticket);
        }

        // GET: Tickets/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var ticket = await _apiService.GetTicketAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }
            return View(ticket);
        }

        // POST: Tickets/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                await _apiService.UpdateTicketAsync(ticket);
                return RedirectToAction(nameof(Index));
            }
            return View(ticket);
        }

        // GET: Tickets/Delete/5

        // GET: Tickets/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var ticket = await _apiService.GetTicketAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }
            return View(ticket); // This will return the delete confirmation view with the ticket details
        }

        // This helps prevent CSRF attacks
        // POST: Tickets/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        // This matches the delete action method in your view
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _apiService.DeleteTicketAsync(id); // Call the method directly

            // Since it doesn't return a value, you can redirect after the call
            return RedirectToAction(nameof(Index)); // Redirect to the index action after deletion
        }

        public async Task<IActionResult> TransferTickets(int id)
        {
            var ticket = await _apiService.GetTicketAsync(id);
            if (ticket == null) return NotFound();

            return View(ticket); // Render view with ticket details
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TransferTickets(int id, string seatNumber)
        {
            if (string.IsNullOrWhiteSpace(seatNumber))
            {
                ModelState.AddModelError("", "Seat number is required.");
                var ticket = await _apiService.GetTicketAsync(id);
                return View(ticket);
            }

            try
            {
                await _apiService.TransferTicketAsync(id, seatNumber);
                return RedirectToAction("Index"); // Redirect to ticket listing after successful transfer
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"An error occurred: {ex.Message}");
                var ticket = await _apiService.GetTicketAsync(id);
                return View(ticket);
            }
        }



    }
}

