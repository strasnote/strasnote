// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using FluentValidation;
using Strasnote.AppBase.Abstracts;
using Strasnote.Notes.Api.Models.Tags;
using Strasnote.Notes.Data.Abstracts;

namespace Strasnote.Notes.Api.Models.Notes
{
	/// <summary>
	/// See <see cref="Controllers.NoteController.AddTag(NoteIdModel, AddTagModel)"/>
	/// </summary>
	/// <param name="TagId">Tag ID</param>
	public sealed record AddTagModel(ulong TagId);

	/// <summary>
	/// Validation for <see cref="AddTagModel"/>
	/// </summary>
	public sealed class AddTagModelValidator : AbstractValidator<AddTagModel>
	{
		private readonly IAppContext appContext;
		private readonly ITagRepository tagRepository;

		/// <summary>
		/// <see cref="AddTagModelValidator"/>
		/// </summary>
		public AddTagModelValidator(IAppContext appContext, ITagRepository tagRepository)
		{
			this.appContext = appContext;
			this.tagRepository = tagRepository;

			RuleFor(x => x.TagId)
				.NotEmpty()
				.MustAsync(async (id, _) => await tagRepository.RetrieveAsync<TagIdModel?>(id, appContext.CurrentUserId) != null)
				.WithMessage("Tag does not exist");
		}
	}
}
