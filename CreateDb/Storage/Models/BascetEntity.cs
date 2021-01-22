using System;
namespace CreateDb.Storage.Models
{
    public class BascetEntity
    {
        public int Id { get; set; }

        public int OrderEntityId { get; set; }
        public OrderEntity Order { get; set; }

        public int MenuEntityId { get; set; }
        public MenuEntity Dish { get; set; }
    }
}
