// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System;
using Strasnote.Data.Abstracts;

namespace Strasnote.Data.Exceptions
{
	public class DbConfigMissingException<T> : Exception
		where T : IDbConfig
	{
		public DbConfigMissingException() { }
		public DbConfigMissingException(string message) : base(message) { }
		public DbConfigMissingException(string message, Exception inner) : base(message, inner) { }
	}
}
