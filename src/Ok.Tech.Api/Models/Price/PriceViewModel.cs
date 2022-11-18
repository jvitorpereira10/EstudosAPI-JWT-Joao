using System;

namespace Ok.Tech.Api.Models.Price
{
    public class PriceViewModel
    {
        public Guid Id { get; set; }
        public Guid PriceListId { get; set; }
        public string PriceListName { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Value { get; set; }
    }
}