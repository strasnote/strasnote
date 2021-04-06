// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System;

namespace Strasnote.Auth.Data.Exceptions
{
	public sealed class UserNotFoundByUsernameException : UserNotFoundException
	{
		public UserNotFoundByUsernameException() { }
		public UserNotFoundByUsernameException(string username) : base($"Username: {username}") { }
		public UserNotFoundByUsernameException(string message, Exception inner) : base(message, inner) { }
	}
}
