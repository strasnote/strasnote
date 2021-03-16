// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Strasnote.Data.Abstracts;
using Strasnote.Logging;

namespace Strasnote.Data.Fake
{
	public abstract class DbContext<TEntity> : Data.DbContext<TEntity>
		where TEntity : IEntity
	{
		protected DbContext(ILog log) : base(new DbClient(), string.Empty, log) { }

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
		protected override void LogOperation(string operation, string detail, params object[] args) =>
		   Log.Information($"Fake {operation} {typeof(TEntity)} {detail}", args);

		protected abstract object GetFakeModelForCreate();

		/// <inheritdoc/>
		public override Task<TModel> CreateAsync<TModel>(TEntity entity)
		{
			// Log create
			LogOperation(Operation.Create, "{Entity}", entity);

			// Perform create and return created entity
			return ConvertToModel<TModel>(GetFakeModelForCreate());
		}

		protected abstract object GetFakeModelForRetrieve();

		/// <inheritdoc/>
		public override Task<IEnumerable<TModel>> RetrieveAsync<TModel>(string query, object parameters, CommandType commandType)
		{
			// Log retrieve
			LogOperation(Operation.Retrieve, "{CommandType} {Query} - {@Parameters}", commandType, query, parameters);

			// Perform retrieve and map to TModel
			return ConvertToModel<IEnumerable<TModel>>(GetFakeModelForRetrieve());
		}

		protected abstract object GetFakeModelForRetrieveById(long id);

		/// <inheritdoc/>
		public override Task<TModel> RetrieveByIdAsync<TModel>(long id)
		{
			// Log retrieve
			LogOperation(Operation.RetrieveById, "{Id}", id);

			// Perform retrieve and map to model
			return ConvertToModel<TModel>(GetFakeModelForRetrieveById(id));
		}

		protected abstract object GetFakeModelForUpdate();

		/// <inheritdoc/>
		public override Task<TModel> UpdateAsync<TModel>(TEntity entity)
		{
			// Log update
			LogOperation(Operation.Update, "{Entity}", entity);

			// Perform update and return updated entity
			return ConvertToModel<TModel>(GetFakeModelForUpdate());
		}

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
