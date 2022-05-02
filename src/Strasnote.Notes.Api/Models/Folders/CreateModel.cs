// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using FluentValidation;
using Strasnote.AppBase.Abstracts;
using Strasnote.Notes.Data.Abstracts;

namespace Strasnote.Notes.Api.Models.Folders
{
	/// <summary>
	/// See <see cref="Controllers.FolderController.Create(CreateModel)"/>
	/// </summary>
	/// <param name="FolderName">New folder name</param>
	/// <param name="ParentId">Parent folder ID</param>
	public sealed record CreateModel(string FolderName, ulong? ParentId = null);

	/// <summary>
	/// Validation for <see cref="CreateModel"/>
	/// </summary>
	public sealed class CreateModelValidator : AbstractValidator<CreateModel>
	{
		private readonly IAppContext appContext;
		private readonly IFolderRepository folderRepository;

		/// <summary>
		/// See <see cref="CreateModelValidator"/>
		/// </summary>
		/// <param name="appContext"></param>
		/// <param name="folderRepository"></param>
		public CreateModelValidator(IAppContext appContext, IFolderRepository folderRepository)
		{
			this.appContext = appContext;
			this.folderRepository = folderRepository;

			RuleFor(x => x.FolderName)
				.NotEmpty()
				.MaximumLength(128);

			// ToDo: to consider: should we be creating IFolderValidator.Exists() (for example), which this would call instead of calling the folder repository
			// Doesn't matter too much for checks like this, but if/when we write something more complex, it would need to be unit tested which would be easier
			// if it was in its own validator service. Therefore, we should put all validators in a service, even simple ones.
			RuleFor(x => x.ParentId)
				.MustAsync(async (id, _) => await folderRepository.RetrieveAsync<GetByIdModel?>((ulong)id!, appContext.CurrentUserId) != null)
				.WithMessage("Parent folder doesn't exist")
				.When(x => x.ParentId != null);
		}
	}
}
