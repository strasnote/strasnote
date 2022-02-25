// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using Microsoft.AspNetCore.Mvc.ModelBinding;
using Strasnote.AppBase.Abstracts;
using Xunit;

namespace Strasnote.AppBase.ModelBinding.RouteIdModelBinderProvider_Tests
{
	public class GetBinderFromModelType_Tests
	{
		[Fact]
		public void ModelType_Does_Not_Inherit_RouteId_Returns_Null()
		{
			// Arrange
			var type = typeof(RandomType);

			// Act
			var result = RouteIdModelBinderProvider.GetBinderFromModelType(type);

			// Assert
			Assert.Null(result);
		}

		[Fact]
		public void ModelType_Inherits_RouteId_Returns_RouteIdModelBinder()
		{
			// Arrange
			var type = typeof(IdType);

			// Act
			var result = RouteIdModelBinderProvider.GetBinderFromModelType(type);

			// Assert
			Assert.IsAssignableFrom<IModelBinder>(result);
			Assert.IsType<RouteIdModelBinder<IdType>>(result);
		}

		public sealed record class RandomType;

		public sealed record class IdType : RouteId;
	}
}
