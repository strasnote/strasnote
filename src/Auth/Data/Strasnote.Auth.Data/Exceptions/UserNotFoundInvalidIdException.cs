// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System;

namespace Strasnote.Auth.Data.Exceptions
{
	public sealed class UserNotFoundInvalidIdException : UserNotFoundException
	{
		public UserNotFoundInvalidIdException() { }
		public UserNotFoundInvalidIdException(string id) : base($"ID: {id}") { }
		public UserNotFoundInvalidIdException(string message, Exception inner) : base(message, inner) { }
	}
}
