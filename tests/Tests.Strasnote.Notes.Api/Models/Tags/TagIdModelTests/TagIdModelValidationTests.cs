// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using FluentValidation.TestHelper;
using Strasnote.AppBase.Abstracts;
using Strasnote.Notes.Data.Abstracts;

namespace Strasnote.Notes.Api.Models.Tags.TagIdModelTests
{
	public class TagIdModelValidationTests
	{
		private readonly IAppContext appContext;
		private readonly ITagRepository tagRepository;
		private readonly TagIdModelValidator validator;

		public TagIdModelValidationTests()
		{
			appContext = Substitute.For<IAppContext>();
			tagRepository = Substitute.For<ITagRepository>();
			validator = new TagIdModelValidator(appContext, tagRepository);
		}

		[Fact]
		public void Value_Should_Error_When_0()
		{
			var tagIdModel = new TagIdModel
			{
				Value = 0
			};

			var result = validator.TestValidate(tagIdModel);

			result.ShouldHaveValidationErrorFor(x => x.Value);
		}

		[Fact]
		public void Error_When_FolderRepository_RetrieveAsync_Returns_Null()
		{
			var tagIdModel = new TagIdModel
			{
				Value = Rnd.Ulng
			};

			tagRepository.RetrieveAsync<GetByIdModel?>(Arg.Any<ulong>(), Arg.Any<ulong?>())
				.Returns(Task.FromResult<GetByIdModel?>(null!));

			var result = validator.TestValidate(tagIdModel);

			result.ShouldHaveValidationErrorFor(x => x.Value);
		}

		[Fact]
		public void No_Error_When_Data_Valid()
		{
			var tagIdModel = new TagIdModel
			{
				Value = Rnd.Ulng
			};

			tagRepository.RetrieveAsync<GetByIdModel?>(Arg.Any<ulong>(), Arg.Any<ulong?>())
				.Returns(new GetByIdModel(Rnd.Ulng, DateTime.Now, Rnd.Str, Rnd.Str, DateTime.Now));

			var result = validator.TestValidate(tagIdModel);

			result.ShouldNotHaveAnyValidationErrors();
		}
	}
}
