// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Strasnote.AppBase.Abstracts;

namespace Strasnote.AppBase.ModelBinding
{
	/// <summary>
	/// Creates <see cref="RouteIdModelBinder{T}"/> if model is of type <see cref="RouteId"/>
	/// </summary>
	public class RouteIdModelBinderProvider : IModelBinderProvider
	{
		/// <summary>
		/// If the model type implements <see cref="RouteId"/>, create <see cref="RouteIdModelBinder{T}"/>
		/// </summary>
		/// <param name="context">ModelBinderProviderContext</param>
		public IModelBinder? GetBinder(ModelBinderProviderContext context)
		{
			// Return null if this is the wrong type
			if (!typeof(RouteId).IsAssignableFrom(context.Metadata.ModelType))
			{
				return null;
			}

			// The context ModelType is the RouteId type, which we pass to the binder as a generic constraint
			try
			{
				var binderType = typeof(RouteIdModelBinder<>).MakeGenericType(context.Metadata.ModelType);
				return Activator.CreateInstance(binderType) switch
				{
					IModelBinder binder =>
						binder,

					_ =>
						null
				};
			}
			catch (Exception)
			{
				return null;
			}
		}
	}
