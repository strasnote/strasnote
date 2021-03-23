// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Strasnote.Data.Abstracts
{
	/// <summary>
	/// Database context for use with queries
	/// </summary>
	/// <typeparam name="TEntity">Entity type</typeparam>
	public interface IDbContextWIthQueries<TEntity> : IDbContext<TEntity>
		where TEntity : IEntity
	{
		/// <summary>
		/// Retrieve entities using the specified predicates (uses AND)
		/// </summary>
		/// <typeparam name="TModel">Return object type</typeparam>
		/// <param name="predicates">List of predicates (uses AND)</param>
		Task<IEnumerable<TModel>> QueryAsync<TModel>(
			params (Expression<Func<TEntity, object>> property, SearchOperator op, object value)[] predicates
		);

		/// <summary>
		/// Retrieve a single entity using the specified predicates (uses AND)
		/// </summary>
		/// <typeparam name="TModel">Return object type</typeparam>
		/// <param name="predicates">List of predicates (uses AND)</param>
		Task<TModel> QuerySingleAsync<TModel>(
			params (Expression<Func<TEntity, object>> property, SearchOperator op, object value)[] predicates
		);
	}
}
