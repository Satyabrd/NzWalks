using System;
namespace NZWalks.API.Models.DTOs
{
	public class UpdateRegionRequestDto
	{
        //Here we are allowing client to update only below 2.
        public string Code { get; set; }
        public string Name { get; set; }
    }
}

