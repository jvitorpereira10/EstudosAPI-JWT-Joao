using System.Collections.Generic;

namespace Ok.Tech.Domain.Entities
{
    public class PriceList : Entity
    {
        public string Name { get; set; }
        public bool Active { get; set; }

        /*EF*/
        public IEnumerable<Price> Prices { get; set; }
    }
}