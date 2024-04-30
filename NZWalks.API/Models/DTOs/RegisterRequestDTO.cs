using System;
using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTOs
{
	public class RegisterRequestDTO
	{
		[Required]
		[DataType(DataType.EmailAddress)]
		public string Username { get; set; }
		[Required]
        [DataType(DataType.EmailAddress)]
        public string Password { get; set; }
		public string[] Roles { get; set; }
	}
}

