using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ok.Tech.Domain.Entities
{
    public class Price : Entity
    {

        public Guid PriceListId { get; set; }

        public Guid ProductId { get; set; }

        public decimal Value { get; set; }

        /*EF*/

        public PriceList PriceList { get; set; }

        public Product Product { get; set; }

    }
}