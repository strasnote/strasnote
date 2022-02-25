﻿// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Strasnote.AppBase.Abstracts;

namespace Strasnote.AppBase.ModelBinding
{
	/// <summary>
	/// Binds a <seealso cref="ulong"/> value to a <see cref="RouteId"/> model
	/// </summary>
	/// <typeparam name="T">RouteId type</typeparam>
	public sealed class RouteIdModelBinder<T> : IModelBinder
		where T : RouteId, new()
	{
		/// <summary>
		/// Get value and attempt to parse as a long
		/// </summary>
		/// <param name="bindingContext">ModelBindingContext</param>
		public Task BindModelAsync(ModelBindingContext bindingContext)
		{
			// Get the value from the context
			var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
			if (valueProviderResult == ValueProviderResult.None)
			{
				return Task.CompletedTask;
			}

			bindingContext.ModelState.SetModelValue(bindingContext.ModelName, valueProviderResult);

			// Get the value and attempt to parse it as a long
			bindingContext.Result = ulong.TryParse(valueProviderResult.FirstValue, out ulong id) switch
			{
				true =>
					success(id),

				false =>
					success(0)
			};

			return Task.CompletedTask;

			// Set the model value using the parsed ID
			static ModelBindingResult success(ulong id) =>
				ModelBindingResult.Success(new T { Id = id });
		}
	}
}
