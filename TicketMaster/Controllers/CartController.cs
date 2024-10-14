using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TicketMaster.Services;
using TicketMaster.Models;

public class CartController : Controller
{
    private readonly CartService _cartService;

    public CartController(CartService cartService)
    {
        _cartService = cartService; // Inject CartService
    }

    public async Task<IActionResult> Index()
    {
        var carts = await _cartService.GetCartsAsync(); // Call CartService, not HttpClient directly
        return View(carts);
    }

    public async Task<IActionResult> Details(int id)
    {
        var cart = await _cartService.GetCartAsync(id); // Use CartService
        if (cart == null) return NotFound();
        return View(cart);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Cart cart)
    {
        var createdCart = await _cartService.CreateCartAsync(cart); // Use CartService
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var cart = await _cartService.GetCartAsync(id); // Use CartService
        if (cart == null) return NotFound();
        return View(cart);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, Cart cart)
    {
        if (id != cart.Id) return BadRequest();
        await _cartService.UpdateCartAsync(cart); // Use CartService
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var cart = await _cartService.GetCartAsync(id); // Use CartService
        if (cart == null) return NotFound();
        return View(cart);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _cartService.DeleteCartAsync(id); // Use CartService
        return RedirectToAction(nameof(Index));
    }
}
