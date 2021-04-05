// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using Microsoft.Extensions.Options;
using NSubstitute;
using Strasnote.Data.Config;
using Strasnote.Data.Exceptions;
using Strasnote.Util;
using Xunit;

namespace Strasnote.Data.Clients.MySql.MySqlClient_Tests
{
	public class Constructor_Tests
	{
		[Fact]
		public void With_ConnectionString_Sets_ConnectionString()
		{
			// Arrange
			var value = Rnd.Str;

			// Act
			var result = new MySqlClient(value);

			// Assert
			Assert.Equal(value, result.ConnectionString);
		}

		[Fact]
		public void With_Null_MySql_Config_Throws_DbConfigMissingException()
		{
			// Arrange
			var options = Substitute.For<IOptions<DbConfig>>();
			options.Value.Returns(new DbConfig());

			// Act
			void action() => new MySqlClient(options);

			// Assert
			Assert.Throws<DbConfigMissingException<MySqlDbConfig>>(action);
		}

		[Fact]
		public void With_Invalid_MySql_Config_Throws_DbConfigInvalidException()
		{
			// Arrange
			var options = Substitute.For<IOptions<DbConfig>>();
			var config = new DbConfig { MySql = new() };
			options.Value.Returns(config);

			// Act
			void action() => new MySqlClient(options);

			// Assert
			Assert.Throws<DbConfigInvalidException<MySqlDbConfig>>(action);
		}

		[Fact]
		public void With_Valid_MySql_Config_Creates_ConnectionString()
		{
			// Arrange
			var options = Substitute.For<IOptions<DbConfig>>();
			var host = Rnd.Str;
			var port = Rnd.Int;
			var user = Rnd.Str;
			var pass = Rnd.Str;
			var dbse = Rnd.Str;
			var cstm = Rnd.Str;
			var config = new DbConfig
			{
				MySql = new()
				{
					Host = host,
					Port = port,
					User = user,
					Pass = pass,
					Database = dbse,
					Custom = cstm
				}
			};
			options.Value.Returns(config);

			var expected = string.Format(
				"server={0};port={1};user id={2};password={3};database={4};convert zero datetime=True;{5}",
				host, port, user, pass, dbse, cstm
			);

			// Act
			var result = new MySqlClient(options);

			// Assert
			Assert.Equal(expected, result.ConnectionString);
		}
	}
}
