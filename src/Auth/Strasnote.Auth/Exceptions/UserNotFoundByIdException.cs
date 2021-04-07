// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System;

namespace Strasnote.Auth.Exceptions
{
	public sealed class UserNotFoundByIdException : UserNotFoundException
	{
		public UserNotFoundByIdException() { }
		public UserNotFoundByIdException(long id) : this($"ID: {id}") { }
		public UserNotFoundByIdException(string message) : base(message) { }
		public UserNotFoundByIdException(string message, Exception inner) : base(message, inner) { }
	}
}
