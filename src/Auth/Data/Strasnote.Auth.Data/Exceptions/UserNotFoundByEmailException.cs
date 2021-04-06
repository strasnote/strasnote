// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System;

namespace Strasnote.Auth.Data.Exceptions
{
	public sealed class UserNotFoundByEmailException : UserNotFoundException
	{
		public UserNotFoundByEmailException() { }
		public UserNotFoundByEmailException(string email) : base($"Email: {email}") { }
		public UserNotFoundByEmailException(string message, Exception inner) : base(message, inner) { }
	}
}
