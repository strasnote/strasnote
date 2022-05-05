// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using FluentValidation;

namespace Strasnote.Notes.Api.Models.Tags
{
	/// <summary>
	/// See <see cref="Controllers.TagController.SaveName(TagIdModel, SaveNameModel)"/>
	/// </summary>
	/// <param name="TagName">Tag name</param>
	public sealed record SaveNameModel(string TagName);

	/// <summary>
	/// Validation for <see cref="SaveNameModel"/>
	/// </summary>
	public sealed class SaveNameModelValidator : AbstractValidator<SaveNameModel>
	{
		/// <summary>
		/// <see cref="SaveNameModelValidator"/>
		/// </summary>
		public SaveNameModelValidator()
		{
			RuleFor(x => x.TagName)
				.NotEmpty()
				.MaximumLength(128);
		}
	}
}
