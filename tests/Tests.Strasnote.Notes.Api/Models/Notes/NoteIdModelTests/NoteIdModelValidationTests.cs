// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using FluentValidation.TestHelper;
using Strasnote.AppBase.Abstracts;
using Strasnote.Data.Entities.Notes;
using Strasnote.Notes.Data.Abstracts;

namespace Strasnote.Notes.Api.Models.Notes.NoteIdModelTests
{
	public sealed class NoteIdModelValidationTests
	{
		private readonly NoteIdModelValidator validator;
		private readonly IAppContext appContext;
		private readonly INoteRepository noteRepository;

		public NoteIdModelValidationTests()
		{
			appContext = Substitute.For<IAppContext>();
			noteRepository = Substitute.For<INoteRepository>();
			validator = new NoteIdModelValidator(appContext, noteRepository);
		}

		[Fact]
		public void Value_Should_Error_When_0()
		{
			var noteIdModel = new NoteIdModel
			{
				Value = 0
			};

			var result = validator.TestValidate(noteIdModel);

			result.ShouldHaveValidationErrorFor(x => x.Value);
		}

		[Fact]
		public void Error_When_FolderRepository_RetrieveAsync_Returns_Null()
		{
			var noteIdModel = new NoteIdModel
			{
				Value = Rnd.Ulng
			};

			noteRepository.RetrieveAsync<FolderEntity>(Arg.Any<ulong>(), Arg.Any<ulong?>())
				.Returns(Task.FromResult<FolderEntity>(null!));

			var result = validator.TestValidate(noteIdModel);

			result.ShouldHaveValidationErrorFor(x => x.Value);
		}

		[Fact]
		public void No_Error_When_Data_Valid()
		{
			var noteIdModel = new NoteIdModel
			{
				Value = Rnd.Ulng
			};

			noteRepository.RetrieveAsync<GetByIdModel?>(Arg.Any<ulong>(), Arg.Any<ulong?>())
				.Returns(new GetByIdModel(Rnd.Ulng, Rnd.Str, DateTime.Now, DateTime.Now));

			var result = validator.TestValidate(noteIdModel);

			result.ShouldNotHaveAnyValidationErrors();
		}
	}
}
