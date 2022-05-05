// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using FluentValidation.TestHelper;
using Strasnote.AppBase.Abstracts;
using Strasnote.Data.Entities.Notes;
using Strasnote.Notes.Data.Abstracts;

namespace Strasnote.Notes.Api.Models.Folders.FolderIdModelTests
{
	public sealed class FolderIdModelValidationTests
	{
		private readonly FolderIdModelValidator validator;
		private readonly IAppContext appContext;
		private readonly IFolderRepository folderRepository;

		public FolderIdModelValidationTests()
		{
			appContext = Substitute.For<IAppContext>();
			folderRepository = Substitute.For<IFolderRepository>();
			validator = new FolderIdModelValidator(folderRepository, appContext);
		}

		[Fact]
		public void Value_Should_Error_When_0()
		{
			var folderIdModel = new FolderIdModel
			{
				Value = 0
			};

			var result = validator.TestValidate(folderIdModel);

			result.ShouldHaveValidationErrorFor(x => x.Value);
		}

		[Fact]
		public void Error_When_FolderRepository_RetrieveAsync_Returns_Null()
		{
			var folderIdModel = new FolderIdModel
			{
				Value = Rnd.Ulng
			};

			folderRepository.RetrieveAsync<FolderEntity>(Arg.Any<ulong>(), Arg.Any<ulong?>())
				.Returns(Task.FromResult<FolderEntity>(null!));

			var result = validator.TestValidate(folderIdModel);

			result.ShouldHaveValidationErrorFor(x => x.Value);
		}

		[Fact]
		public void No_Error_When_Data_Valid()
		{
			var folderIdModel = new FolderIdModel
			{
				Value = Rnd.Ulng
			};

			folderRepository.RetrieveAsync<GetByIdModel?>(Arg.Any<ulong>(), Arg.Any<ulong?>())
				.Returns(new GetByIdModel(DateTime.Now, Rnd.Str, DateTime.Now, Rnd.Ulng));

			var result = validator.TestValidate(folderIdModel);

			result.ShouldNotHaveAnyValidationErrors();
		}
	}
}
