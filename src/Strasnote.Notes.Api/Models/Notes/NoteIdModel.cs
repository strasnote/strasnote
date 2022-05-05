// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using FluentValidation;
using Strasnote.AppBase.Abstracts;
using Strasnote.Notes.Data.Abstracts;

namespace Strasnote.Notes.Api.Models.Notes
{
	/// <summary>
	/// Note ID (wrapped for fluent validation)
	/// </summary>
	public sealed record class NoteIdModel : RouteId;

	/// <summary>
	/// Validation for <see cref="NoteIdModel"/>
	/// </summary>
	public sealed class NoteIdModelValidator : AbstractValidator<NoteIdModel>
	{
		private readonly IAppContext appContext;
		private readonly INoteRepository noteRepository;

		/// <summary>
		/// <see cref="NoteIdModelValidator"/>
		/// </summary>
		public NoteIdModelValidator(IAppContext appContext, INoteRepository noteRepository)
		{
			this.appContext = appContext;
			this.noteRepository = noteRepository;

			RuleFor(x => x.Value)
				.NotEmpty()
				.MustAsync(async (id, _) => await noteRepository.RetrieveAsync<GetByIdModel?>(id, appContext.CurrentUserId) != null)
				.WithMessage("Note not found");
		}
	}
}
