// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System;

namespace Strasnote.Data.Exceptions
{
	public class SqlRepositoryNoMatchingColumnsException : Exception
	{
		public SqlRepositoryNoMatchingColumnsException() { }
		public SqlRepositoryNoMatchingColumnsException(string message) : base(message) { }
		public SqlRepositoryNoMatchingColumnsException(string message, Exception inner) : base(message, inner) { }
	}
}
