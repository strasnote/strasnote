// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System;
using Strasnote.Data.Abstracts;

namespace Strasnote.Data.Exceptions
{
	public class DbConfigInvalidException<T> : Exception
		where T : IDbConfig
	{
		public DbConfigInvalidException() { }
		public DbConfigInvalidException(string message) : base(message) { }
		public DbConfigInvalidException(string message, Exception inner) : base(message, inner) { }
	}
}
