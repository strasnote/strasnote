// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using FluentValidation.TestHelper;
using Strasnote.AppBase.Abstracts;
using Strasnote.Notes.Data.Abstracts;

namespace Strasnote.Notes.Api.Models.Folders.CreateModelTests
{
	public class CreateModelValidationTests
	{
		private readonly CreateModelValidator validator;
		private readonly IAppContext appContext;
		private readonly IFolderRepository folderRepository;

		public CreateModelValidationTests()
		{
			appContext = Substitute.For<IAppContext>();
			folderRepository = Substitute.For<IFolderRepository>();
			validator = new CreateModelValidator(appContext, folderRepository);
		}

		[Fact]
		public void FolderName_Should_Error_When_Empty()
		{
			var createModel = new CreateModel(" ");

			var result = validator.TestValidate(createModel);

			result.ShouldHaveValidationErrorFor(x => x.FolderName);
		}

		[Fact]
		public void FolderName_Should_Error_When_Null()
		{
			var createModel = new CreateModel(null!);

			var result = validator.TestValidate(createModel);

			result.ShouldHaveValidationErrorFor(x => x.FolderName);
		}

		[Fact]
		public void FolderName_Should_Error_When_Length_Exceeds_128()
		{
			var createModel = new CreateModel(Rnd.RndString.Get(129));

			var result = validator.TestValidate(createModel);

			result.ShouldHaveValidationErrorFor(x => x.FolderName);
		}

		[Fact]
		public void ParentId_Should_Error_When_Parent_Folder_Not_Found()
		{
			var createModel = new CreateModel(Rnd.Str, 9);

			folderRepository.RetrieveAsync<GetByIdModel?>(Arg.Any<ulong>(), Arg.Any<ulong?>())
				.Returns(Task.FromResult<GetByIdModel?>(null));

			var validator = new CreateModelValidator(appContext, folderRepository);

			var result = validator.TestValidate(createModel);

			result.ShouldHaveValidationErrorFor(x => x.ParentId);
		}

		[Fact]
		public void ParentId_Should_Not_Error_When_Null()
		{
			var createModel = new CreateModel(Rnd.Str, null);

			var result = validator.TestValidate(createModel);

			result.ShouldNotHaveValidationErrorFor(x => x.ParentId);
		}

		[Fact]
		public void ParentId_Should_Only_Validate_When_Not_Null()
		{
			var createModel = new CreateModel(Rnd.Str, null);

			validator.TestValidate(createModel);

			folderRepository.DidNotReceive().RetrieveAsync<GetByIdModel?>(Arg.Any<ulong>(), Arg.Any<ulong?>());
		}

		[Fact]
		public void No_Error_When_Data_Valid_Top_Level_Folder()
		{
			var createModel = new CreateModel(Rnd.Str, null);

			var result = validator.TestValidate(createModel);

			result.ShouldNotHaveAnyValidationErrors();
		}

		[Fact]
		public void No_Error_When_Data_Valid_With_Parent_Folder()
		{
			var createModel = new CreateModel(Rnd.Str, Rnd.Ulng);

			folderRepository.RetrieveAsync<GetByIdModel?>(Arg.Any<ulong>(), Arg.Any<ulong?>())
				.Returns(new GetByIdModel(DateTime.Now, Rnd.Str, DateTime.Now, Rnd.Ulng));

			var result = validator.TestValidate(createModel);

			result.ShouldNotHaveAnyValidationErrors();
		}
	}
}
