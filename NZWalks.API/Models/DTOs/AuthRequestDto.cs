using System;
using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTOs
{
	public class AuthRequestDto
	{
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Password { get; set; }
    }
}

