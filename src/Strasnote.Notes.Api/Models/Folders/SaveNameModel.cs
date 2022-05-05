// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using FluentValidation;

namespace Strasnote.Notes.Api.Models.Folders
{
	/// <summary>
	/// See <see cref="Controllers.FolderController.SaveName(FolderIdModel, SaveNameModel)"/>
	/// </summary>
	/// <param name="FolderName">Folder Name</param>
	public sealed record SaveNameModel(string FolderName);

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
			RuleFor(x => x.FolderName)
				.NotEmpty()
				.MaximumLength(128);
		}
	}
}
