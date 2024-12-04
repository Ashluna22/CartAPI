using System.Collections.Generic;
using CartAPI.Models;

namespace CartAPI.Repositories
{
    public static class CartItemsRepository
    {
        public static List<CartItem> CartItems { get; } = new List<CartItem>
        {
            new CartItem { Id = 1, Product = "Laptop", Price = 999.99, Quantity = 1 },
            new CartItem { Id = 2, Product = "Mouse", Price = 19.99, Quantity = 2 },
            new CartItem { Id = 3, Product = "Keyboard", Price = 49.99, Quantity = 1 }
        };
    }
}
