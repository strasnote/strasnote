// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using FluentValidation.TestHelper;

namespace Strasnote.Notes.Api.Models.Folders.SaveNameModelTests
{
	public sealed class SaveNameModelValidationTests
	{
		private readonly SaveNameModelValidator validator;

		public SaveNameModelValidationTests()
		{
			validator = new SaveNameModelValidator();
		}

		[Fact]
		public void FolderName_Should_Error_When_Empty()
		{
			var saveNameModel = new SaveNameModel(" ");

			var result = validator.TestValidate(saveNameModel);

			result.ShouldHaveValidationErrorFor(x => x.FolderName);
		}

		[Fact]
		public void FolderName_Should_Error_When_Null()
		{
			var saveNameModel = new SaveNameModel(null!);

			var result = validator.TestValidate(saveNameModel);

			result.ShouldHaveValidationErrorFor(x => x.FolderName);
		}

		[Fact]
		public void FolderName_Should_Error_When_Length_Exceeds_128()
		{
			var saveNameModel = new SaveNameModel(Rnd.RndString.Get(129));

			var result = validator.TestValidate(saveNameModel);

			result.ShouldHaveValidationErrorFor(x => x.FolderName);
		}

		[Fact]
		public void No_Error_When_FolderName_Valid()
		{
			var saveNameModel = new SaveNameModel(Rnd.Str);

			var result = validator.TestValidate(saveNameModel);

			result.ShouldNotHaveAnyValidationErrors();
		}
	}
}
