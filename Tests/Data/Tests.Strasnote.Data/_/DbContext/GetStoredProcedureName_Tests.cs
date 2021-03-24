// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.Runtime.CompilerServices;
using NSubstitute;
using Strasnote.Data.Abstracts;
using Strasnote.Logging;
using Strasnote.Util;
using Xunit;

namespace Strasnote.Data.DbContext_Tests
{
	public class GetStoredProcedureName_Tests
	{
		static private TestDbContext GetContext()
		{
			var client = Substitute.For<IDbClient>();
			var log = Substitute.For<ILog>();
			return new TestDbContext(client, log);
		}

		[Fact]
		public void Removes_Entity_From_Type()
		{
			// Arrange
			var method = Rnd.Str;
			var context = GetContext();
			var expected = $"Test_{method}";

			// Act
			var result = context.GetStoredProcedureNameTest(method);

			// Assert
			Assert.Equal(expected, result);
		}

		[Fact]
		public void Removes_Async_From_Method()
		{
			// Arrange
			var method = Rnd.Str;
			var methodWithAsync = $"{method}Async";
			var context = GetContext();
			var expected = $"Test_{method}";

			// Act
			var result = context.GetStoredProcedureNameTest(methodWithAsync);

			// Assert
			Assert.Equal(expected, result);
		}

		[Theory]
		[InlineData(Operation.Create)]
		[InlineData(Operation.Retrieve)]
		[InlineData(Operation.RetrieveById)]
		[InlineData(Operation.Update)]
		[InlineData(Operation.Delete)]
		public void Uses_Operation_Enum_As_String(Operation input)
		{
			// Arrange
			var context = GetContext();
			var expected = $"Test_{input}";

			// Act
			var result = context.GetStoredProcedureNameTest(input);

			// Assert
			Assert.Equal(expected, result);
		}

		public sealed class TestDbContext : DbContext<TestEntity>
		{
			public TestDbContext(IDbClient client, ILog log) : base(client, log) { }
		}

		public sealed record TestEntity(long Id) : IEntity;
	}
}
