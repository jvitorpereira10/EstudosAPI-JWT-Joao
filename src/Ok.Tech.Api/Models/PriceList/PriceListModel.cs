using System;
using System.ComponentModel.DataAnnotations;

namespace Ok.Tech.Api.Models.PriceList
{
  public class PriceListModel
  {
    [Key]
    public Guid Id { get; set; }
    
    [Required(ErrorMessage = "The {0} must be supplied")]
    [StringLength(200, ErrorMessage = "The {0} length must be between {2} and {1}", MinimumLength = 3)]
    public string Name { get; set; }
    
    public bool Active { get; set; }
  }
}
