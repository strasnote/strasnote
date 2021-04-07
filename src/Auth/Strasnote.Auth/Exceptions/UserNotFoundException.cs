// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System;

namespace Strasnote.Auth.Exceptions
{
	public abstract class UserNotFoundException : Exception
	{
		protected UserNotFoundException() { }
		protected UserNotFoundException(string message) : base(message) { }
		protected UserNotFoundException(string message, Exception inner) : base(message, inner) { }
	}
}
