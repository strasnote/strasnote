// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System;

namespace Strasnote.Data
{
	/// <summary>
	/// Mark property as to be ignored when mapping entity properties to table columns
	/// </summary>
	[AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
	public sealed class IgnoreAttribute : Attribute { }
}
