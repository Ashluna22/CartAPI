using Microsoft.AspNetCore.Mvc;
using CartAPI.Models;
using CartAPI.Repositories;

[ApiController]
[Route("cart-items")]
public class CartItemsController : ControllerBase
{
    // Example cURL for GET
    // curl http://localhost:5000/cart-items
    [HttpGet]
    public IActionResult GetCartItems([FromQuery] double? maxPrice, [FromQuery] string prefix, [FromQuery] int? pageSize)
    {
        var items = CartItemsRepository.CartItems.AsQueryable();

        if (maxPrice.HasValue)
            items = items.Where(item => item.Price <= maxPrice.Value);

        if (!string.IsNullOrEmpty(prefix))
            items = items.Where(item => item.Product.StartsWith(prefix, StringComparison.OrdinalIgnoreCase));

        if (pageSize.HasValue)
            items = items.Take(pageSize.Value);

        return Ok(items);
    }

    // Example cURL for POST
    // curl -X POST -H "Content-Type: application/json" -d '{"product":"Laptop","price":1200,"quantity":1}' http://localhost:5000/cart-items
    [HttpPost]
    public IActionResult AddCartItem([FromBody] CartItem newItem)
    {
        newItem.Id = CartItemsRepository.CartItems.Count > 0 ? CartItemsRepository.CartItems.Max(i => i.Id) + 1 : 1;
        CartItemsRepository.CartItems.Add(newItem);
        return CreatedAtAction(nameof(GetCartItems), new { id = newItem.Id }, newItem);
    }

    // Example cURL for DELETE
    // curl -X DELETE http://localhost:5000/cart-items/1
    [HttpDelete("{id}")]
    public IActionResult DeleteCartItem(int id)
    {
        var item = CartItemsRepository.CartItems.FirstOrDefault(i => i.Id == id);
        if (item == null) return NotFound("ID Not Found");

        CartItemsRepository.CartItems.Remove(item);
        return NoContent();
    }
}
