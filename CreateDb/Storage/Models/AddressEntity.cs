using System;
using System.ComponentModel.DataAnnotations;

namespace CreateDb.Storage.Models
{
    public class AddressEntity
    {
        public int Id { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public string NumberOfBuild { get; set; }
        public int NumberOfEntrance { get; set; }
        public int Apartment { get; set; }

        public int UserEntityId { get; set; }
        public UserEntity UserAddress { get; set; }


    }
}
