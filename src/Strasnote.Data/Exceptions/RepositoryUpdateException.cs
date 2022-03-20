// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System;

namespace Strasnote.Data.Exceptions
{
	public class RepositoryUpdateException<T> : Exception
	{
		public RepositoryUpdateException() { }
		public RepositoryUpdateException(ulong id) : base($"Unable to update {typeof(T)} with ID {id}.") { }
		public RepositoryUpdateException(string message) : base(message) { }
		public RepositoryUpdateException(string message, Exception inner) : base(message, inner) { }
	}
}
