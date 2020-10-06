using System;
using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    public class BaseModel
    {
        [Key]
        public int Id { get; set; }
    }
}
