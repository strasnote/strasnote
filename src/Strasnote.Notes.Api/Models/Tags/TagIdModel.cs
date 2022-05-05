// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using FluentValidation;
using Strasnote.AppBase.Abstracts;
using Strasnote.Notes.Data.Abstracts;

namespace Strasnote.Notes.Api.Models.Tags
{
	/// <summary>
	/// Tag ID (wrapped for fluent validation)
	/// </summary>
	public sealed record class TagIdModel : RouteId;

	/// <summary>
	/// Validation for <see cref="TagIdModel"/>
	/// </summary>
	public sealed class TagIdModelValidator : AbstractValidator<TagIdModel>
	{
		private readonly IAppContext appContext;
		private readonly ITagRepository tagRepository;

		/// <summary>
		/// <see cref="TagIdModelValidator"/>
		/// </summary>
		public TagIdModelValidator(IAppContext appContext, ITagRepository tagRepository)
		{
			this.appContext = appContext;
			this.tagRepository = tagRepository;

			RuleFor(x => x.Value)
				.NotEmpty()
				.MustAsync(async (id, _) => await tagRepository.RetrieveAsync<GetByIdModel?>(id, appContext.CurrentUserId) != null)
				.WithMessage("Tag not found");
		}
	}
}
