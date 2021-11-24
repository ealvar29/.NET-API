using System;
using System.ComponentModel.DataAnnotations;

namespace VueAPI.Models.Dtos
{
    public class NationalParkDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public string State { get; set; }

        public DateTime Created { get; set; }

        public DateTime Established { get; set; }
    }
}
