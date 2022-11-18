using System;
using System.ComponentModel.DataAnnotations;

namespace Ok.Tech.Api.Models.Price
{
    public class PriceModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "The {0} must be supplied")]
        public Guid PriceListId { get; set; }

        [Required(ErrorMessage = "The {0} must be supplied")]
        public Guid ProductId { get; set; }

        [Required(ErrorMessage = "The {0} must be supplied")]
        public decimal Value { get; set; }
    }
}