﻿// Copyright (c) Strasnote
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
		/// Override base to log as Information (not Trace)
		/// </summary>
		/// <param name="operation"></param>
		/// <param name="detail"></param>
		/// <param name="args"></param>
		protected override void LogOperation(string operation, string detail, params object[] args) =>
		   Log.Information($"Fake {operation} {typeof(TEntity)} {detail}", args);

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
		/// Return a fake model to use in <see cref="GetFakeModelForRetrieve"/>
		/// </summary>
		protected abstract object GetFakeModelForRetrieve();

		/// <inheritdoc/>
		public override Task<IEnumerable<TModel>> RetrieveAsync<TModel>(string query, object parameters, CommandType commandType)
		{
			// Log retrieve
			LogOperation(Operation.Retrieve, "{CommandType} {Query} - {@Parameters}", commandType, query, parameters);

			// Perform retrieve and map to TModel
			return ConvertToModel<IEnumerable<TModel>>(GetFakeModelForRetrieve());
		}

		/// <summary>
		/// Return a fake model to use in <see cref="GetFakeModelForRetrieveById(long)"/>
		/// </summary>
		/// <param name="id">Entity ID</param>
		protected abstract object GetFakeModelForRetrieveById(long id);

		/// <inheritdoc/>
		public override Task<TModel> RetrieveByIdAsync<TModel>(long id)
		{
			// Log retrieve
			LogOperation(Operation.RetrieveById, "{Id}", id);

			// Perform retrieve and map to model
			return ConvertToModel<TModel>(GetFakeModelForRetrieveById(id));
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