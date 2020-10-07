﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class User : BaseModel
    {
        [Required]
        public string Name { get; set; }

        [MaxLength(30)]
        [Index("usernameIndex", IsUnique = true, Order = 2)]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public string Address { get; set; }

        [ForeignKey(nameof(Role))]
        public int RoleId { get; set; }
        public Role Role { get; set; }

        public IList<Order> Orders { get; set; }
    }
}