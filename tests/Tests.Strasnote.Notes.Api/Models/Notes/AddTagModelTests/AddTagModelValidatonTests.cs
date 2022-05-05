// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using FluentValidation.TestHelper;
using Strasnote.AppBase.Abstracts;
using Strasnote.Notes.Data.Abstracts;

namespace Strasnote.Notes.Api.Models.Notes.AddTagModelTests
{
	public class AddTagModelValidatonTests
	{
		private readonly IAppContext appContext;
		private readonly ITagRepository tagRepository;
		private readonly AddTagModelValidator validator;

		public AddTagModelValidatonTests()
		{
			appContext = Substitute.For<IAppContext>();
			tagRepository = Substitute.For<ITagRepository>();
			validator = new AddTagModelValidator(appContext, tagRepository);
		}

		[Fact]
		public void TagId_Should_Error_When_0()
		{
			var addTagModel = new AddTagModel(0);

			var result = validator.TestValidate(addTagModel);

			result.ShouldHaveValidationErrorFor(x => x.TagId);
		}

		[Fact]
		public void TagId_Should_Error_When_TagRepository_Retrieve_Null()
		{
			var addTagModel = new AddTagModel(Rnd.Ulng);

			tagRepository.RetrieveAsync<Tags.TagIdModel?>(Arg.Any<ulong>(), Arg.Any<ulong?>())
				.Returns(Task.FromResult<Tags.TagIdModel?>(null));

			var result = validator.TestValidate(addTagModel);

			result.ShouldHaveValidationErrorFor(x => x.TagId);
		}

		[Fact]
		public void TagId_Should_Not_Error_When_TagRepository_Not_Null()
		{
			var addTagModel = new AddTagModel(Rnd.Ulng);

			tagRepository.RetrieveAsync<Tags.TagIdModel?>(Arg.Any<ulong>(), Arg.Any<ulong?>())
				.Returns(new Tags.TagIdModel());

			var result = validator.TestValidate(addTagModel);

			result.ShouldNotHaveAnyValidationErrors();
		}
	}
}
