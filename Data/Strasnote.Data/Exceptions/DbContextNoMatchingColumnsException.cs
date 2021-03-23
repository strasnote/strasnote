// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System;

namespace Strasnote.Data.Exceptions
{

	public class DbContextNoMatchingColumnsException : Exception
	{
		public DbContextNoMatchingColumnsException() { }
		public DbContextNoMatchingColumnsException(string message) : base(message) { }
		public DbContextNoMatchingColumnsException(string message, Exception inner) : base(message, inner) { }
	}
}
