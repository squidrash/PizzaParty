using System;
using System.ComponentModel.DataAnnotations;

namespace CreateDb.Storage.Models
{
    public class OrderMenuEntity
    {
        public int Id { get; set; }

        public int OrderEntityId { get; set; }
        public OrderEntity Order { get; set; }

        public int MenuEntityId { get; set; }
        public MenuEntity Dish { get; set; }

        public int CountDish { get; set; }
    }
}
