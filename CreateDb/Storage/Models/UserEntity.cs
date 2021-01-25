﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CreateDb.Storage.Models
{
    public class UserEntity
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string LastName { get; set; }        
        public List<AddressEntity> Address { get; set; }
        [Required]
        public string Phone { get; set; }
        public int Discount { get; set; }

        public List<OrderEntity> Orders { get; set; }
    }
}
