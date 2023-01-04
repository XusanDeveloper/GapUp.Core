using System;

namespace GapUp.Api.Models.Products
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime CreatedUserId { get; set; }
        public DateTime UpdatedUserId { get; set; }
    }
}
