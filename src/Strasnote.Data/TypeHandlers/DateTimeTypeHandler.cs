// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System;
using System.Data;
using Dapper;

namespace Strasnote.Data.TypeHandlers
{
	/// <summary>
	/// Ensure DateTime values are stored as UTC.
	/// </summary>
	public class DateTimeTypeHandler : SqlMapper.TypeHandler<DateTime>
	{
		/// <summary>
		/// Store value as UTC.
		/// </summary>
		/// <param name="parameter">IDbDataParameter</param>
		/// <param name="value">DateTime</param>
		public override void SetValue(IDbDataParameter parameter, DateTime value) =>
			parameter.Value = value.ToUniversalTime();

		/// <summary>
		/// Parse value and specify it is UTC.
		/// </summary>
		/// <param name="value">Database value</param>
		/// <exception cref="InvalidCastException">Thrown if <paramref name="value"/> is not a valid <see cref="DateTime"/></exception>
		public override DateTime Parse(object value) =>
			DateTime.TryParse(value?.ToString(), out DateTime dt) switch
			{
				true =>
					DateTime.SpecifyKind(dt, DateTimeKind.Utc),

				false =>
					throw new InvalidCastException($"'{value}' is not a valid DateTime.")
			};
	}
}
