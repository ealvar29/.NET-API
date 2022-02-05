﻿using System.ComponentModel.DataAnnotations;
using static VueAPI.Models.Trail;

namespace VueAPI.Models.Dtos
{
    public class TrailUpsertDto
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public double Distance { get; set; }

        public DifficultyType Difficulty { get; set; }

        [Required]
        public int NationalParkId { get; set; }
    }
}
