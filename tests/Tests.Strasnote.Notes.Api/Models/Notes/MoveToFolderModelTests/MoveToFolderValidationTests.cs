// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using FluentValidation.TestHelper;
using Strasnote.AppBase.Abstracts;
using Strasnote.Notes.Data.Abstracts;

namespace Strasnote.Notes.Api.Models.Notes.MoveToFolderModelTests
{
	public class MoveToFolderValidationTests
	{
		private readonly MoveToFolderModelValidator validator;
		private readonly IAppContext appContext;
		private readonly IFolderRepository folderRepository;

		public MoveToFolderValidationTests()
		{
			appContext = Substitute.For<IAppContext>();
			folderRepository = Substitute.For<IFolderRepository>();
			validator = new MoveToFolderModelValidator(appContext, folderRepository);
		}

		[Fact]
		public void FolderId_Should_Not_Error_When_Null()
		{
			var moveToFolderModel = new MoveToFolderModel(null);

			var result = validator.TestValidate(moveToFolderModel);

			result.ShouldNotHaveValidationErrorFor(x => x.FolderId);
		}

		[Fact]
		public void FolderId_Should_Only_Validate_When_Not_Null()
		{
			var moveToFolderModel = new MoveToFolderModel(null);

			validator.TestValidate(moveToFolderModel);

			folderRepository.DidNotReceive().RetrieveAsync<GetByIdModel?>(Arg.Any<ulong>(), Arg.Any<ulong?>());
		}

		[Fact]
		public void FolderId_Should_Error_When_Folder_Not_Found()
		{
			var moveToFolderModel = new MoveToFolderModel(Rnd.Ulng);

			folderRepository.RetrieveAsync<GetByIdModel?>(Arg.Any<ulong>(), Arg.Any<ulong?>())
				.Returns(Task.FromResult<GetByIdModel?>(null));

			var result = validator.TestValidate(moveToFolderModel);

			result.ShouldHaveValidationErrorFor(x => x.FolderId);
		}
	}
}
