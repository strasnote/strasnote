// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.Threading.Tasks;
using Strasnote.Data.Abstracts;
using Strasnote.Data.Entities.Auth;

namespace Strasnote.Auth.Data.Abstracts
{
	/// <summary>
	/// Abstraction for interacting with the user / authentication / identity database
	/// </summary>
	public interface IUserContext : IDbContext<UserEntity>
	{
		/// <summary>
		/// Retrieve a User by username
		/// </summary>
		/// <param name="name">Username</param>
		Task<TModel> RetrieveAsync<TModel>(string name);
	}
}
