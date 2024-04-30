﻿using System;
using Microsoft.AspNetCore.Identity;

namespace NZWalks.API.Repositories
{
	public interface ITokenRepository
	{
		public string createJWTToken(IdentityUser user, List<string> roles);
	}
}

