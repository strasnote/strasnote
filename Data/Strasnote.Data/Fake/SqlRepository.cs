﻿// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Strasnote.Data.Abstracts;
using Strasnote.Logging;
using Strasnote.Util;

namespace Strasnote.Data.Fake
{
	/// <summary>
	/// Fake DbContext
	/// </summary>
	/// <typeparam name="TEntity"></typeparam>
	public abstract class SqlRepository<TEntity> : Data.SqlRepository<TEntity>
		where TEntity : IEntity
	{
		protected SqlRepository(ILog log) : base(new SqlClient(), log, Rnd.Str) { }

		/// <summary>
		/// Force an object to <typeparamref name="TModel"/> - this can only be tested at runtime
		/// so ensure you create the correct model in the first place
		/// </summary>
		/// <typeparam name="TModel">Model type</typeparam>
		/// <param name="obj">Model object</param>
		protected async Task<TModel> ConvertToModel<TModel>(object obj)
		{
			try
			{
				var model = (TModel)obj;
				return await Task.FromResult(model).ConfigureAwait(false);
			}
			catch (Exception)
			{
				Log.Error("Unable to convert {ObjectType} to {ModelType}", obj.GetType(), typeof(TModel));
				throw;
			}
		}

		/// <summary>
		/// Override to send logs to Information log not Trace
		/// </summary>
		/// <param name="message"></param>
		/// <param name="args"></param>
		protected override void LogTrace(string message, params object[] args) =>
			Log.Information(message, args);

		/// <summary>
		/// Return a fake model to use in <see cref="CreateAsync{TModel}(TEntity)"/>
		/// </summary>
		protected abstract object GetFakeModelForCreate();

		/// <inheritdoc/>
		public override Task<TModel> CreateAsync<TModel>(TEntity entity)
		{
			// Log create
			LogOperation(Operation.Create, "{Entity}", entity);

			// Perform create and return created entity
			return ConvertToModel<TModel>(GetFakeModelForCreate());
		}

		/// <summary>
		/// Return a fake model to use in <see cref="GetFakeModelForRetrieve(long)"/>
		/// </summary>
		/// <param name="id">Entity ID</param>
		protected abstract object GetFakeModelForRetrieve(long id);

		/// <inheritdoc/>
		public override Task<TModel> RetrieveAsync<TModel>(long id)
		{
			// Log retrieve
			LogOperation(Operation.RetrieveById, "{Id}", id);

			// Perform retrieve and map to model
			return ConvertToModel<TModel>(GetFakeModelForRetrieve(id));
		}

		/// <summary>
		/// Return a fake model for use in <see cref="UpdateAsync{TModel}(TEntity)"/>
		/// </summary>
		protected abstract object GetFakeModelForUpdate();

		/// <inheritdoc/>
		public override Task<TModel> UpdateAsync<TModel>(TEntity entity)
		{
			// Log update
			LogOperation(Operation.Update, "{Entity}", entity);

			// Perform update and return updated entity
			return ConvertToModel<TModel>(GetFakeModelForUpdate());
		}

		/// <summary>
		/// Return a fake result for use in <see cref="DeleteAsync(long)"/>
		/// </summary>
		protected virtual bool GetFakeResultForDelete() =>
			true;

		/// <inheritdoc/>
		public override Task<bool> DeleteAsync(long id)
		{
			// Log delete
			LogOperation(Operation.Delete, "{Id}", id);

			// Perform retrieve and map to model
			return Task.FromResult(GetFakeResultForDelete());
		}
	}
}