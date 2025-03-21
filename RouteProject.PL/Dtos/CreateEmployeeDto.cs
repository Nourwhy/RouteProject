﻿using Newtonsoft.Json.Serialization;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using RouteProject.DAL.Models;  

namespace RouteProject.PL.Dtos
{
    public class CreateEmployeeDto 
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is Requied !")]
        public string Name { get; set; }
        [Range(22, 60, ErrorMessage = "Age Must Be Between 22 ans 60")]
        public int? Age { get; set; }
        [Required(ErrorMessage = "Email is valid !")]
        public string Email { get; set; }
        [RegularExpression(@"^\d{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{4,10}-[a-zA-Z]{5,10}$",
            ErrorMessage = "Address must be like 123-street-city-country")]
        public string Address { get; set; }


        [Phone]
        public string Phone { get; set; }
        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }

        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        [DisplayName("Hiring Date")]
        public DateTime HiringDate { get; set; }

        [DisplayName("Date Of Create")]
        public DateTime CreateAt { get; set; }
    }
}
