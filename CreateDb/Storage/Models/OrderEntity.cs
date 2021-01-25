using System;
using System.Collections.Generic;

namespace CreateDb.Storage.Models
{
    public class OrderEntity
    {
        public int Id { get; set; }

        public int UserEntityId { get; set; }
        public UserEntity UserOrder { get; set; }

        public List<OrderMenuEntity> Products { get; set; }

        public DateTime CreatTime { get; set; } 
        public string Status { get; set; }

    }
}
