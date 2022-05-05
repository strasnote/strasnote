// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using FluentValidation;

namespace Strasnote.Notes.Api.Models.Tags
{
	/// <summary>
	/// See <see cref="Controllers.TagController.Create"/>
	/// </summary>
	public sealed record CreateModel(string TagName);

	/// <summary>
	/// Validation for <see cref="CreateModel"/>
	/// </summary>
	public sealed class CreateModelValidator : AbstractValidator<CreateModel>
	{
		/// <summary>
		/// <see cref="CreateModelValidator"/>
		/// </summary>
		public CreateModelValidator()
		{
			RuleFor(x => x.TagName)
				.NotEmpty()
				.MaximumLength(128);
		}
	}
}
