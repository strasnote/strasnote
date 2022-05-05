// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using FluentValidation.TestHelper;

namespace Strasnote.Notes.Api.Models.Tags.SaveNameModelTests
{
	public class CreateModelValidationTests
	{
		private readonly CreateModelValidator validator;

		public CreateModelValidationTests() => 
			validator = new CreateModelValidator();

		[Fact]
		public void TagName_Should_Error_When_Null()
		{
			var createModel = new CreateModel(null!);

			var result = validator.TestValidate(createModel);

			result.ShouldHaveValidationErrorFor(x => x.TagName);
		}

		[Fact]
		public void TagName_Should_Error_When_Empty()
		{
			var createModel = new CreateModel(string.Empty);

			var result = validator.TestValidate(createModel);

			result.ShouldHaveValidationErrorFor(x => x.TagName);
		}

		[Fact]
		public void TagName_Should_Error_When_Whitespace()
		{
			var createModel = new CreateModel(" ");

			var result = validator.TestValidate(createModel);

			result.ShouldHaveValidationErrorFor(x => x.TagName);
		}

		[Fact]
		public void TagName_Should_Error_When_Length_Exceeds_128()
		{
			var createModel = new CreateModel(Rnd.RndString.Get(129));

			var result = validator.TestValidate(createModel);

			result.ShouldHaveValidationErrorFor(x => x.TagName);
		}

		[Fact]
		public void No_Errors_When_TagName_Valid()
		{
			var createModel = new CreateModel(Rnd.Str);

			var result = validator.TestValidate(createModel);

			result.ShouldNotHaveAnyValidationErrors();
		}
	}
}
