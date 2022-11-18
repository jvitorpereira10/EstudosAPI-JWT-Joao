using System;
using System.ComponentModel.DataAnnotations;

namespace Ok.Tech.Api.Models.Product
{
  public class ProductModel
  {
    [Key]
    public Guid Id { get; set; }
    
    [Required(ErrorMessage = "The {0} must be supplied")]
    [StringLength(200, ErrorMessage = "The {0} length must be between {2} and {1}", MinimumLength = 3)]
    public string Name { get; set; }
    
    [Required(ErrorMessage = "The {0} must be supplied")]
    [StringLength(1000, ErrorMessage = "The {0} length must be between {2} and {1}", MinimumLength = 3)]
    
    public string Description { get; set; }
    
    public bool Active { get; set; }
  }
}
