// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Strasnote.Data.Entities.Auth
{
	/// <inheritdoc cref="IdentityRole{TKey}"/>
	public class UserRoleEntity : IdentityUserRole<long>
	{

	}
}
