using System;
using System.Collections.Generic;

namespace CreateDb.Storage.Models
{
    public class MenuEntity
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public int Price { get; set; }

        public List<OrderMenuEntity> Orders { get; set; }

    }
}
