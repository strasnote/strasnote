// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using FluentValidation;
using Strasnote.AppBase.Abstracts;
using Strasnote.Notes.Data.Abstracts;

namespace Strasnote.Notes.Api.Models.Folders
{
	/// <summary>
	/// Folder ID (wrapped for fluent validation)
	/// </summary>
	public sealed record class FolderIdModel : RouteId;

	/// <summary>
	/// Validation for <see cref="FolderIdModel"/>
	/// </summary>
	public sealed class FolderIdModelValidator : AbstractValidator<FolderIdModel>
	{
		private readonly IFolderRepository folderRepository;
		private readonly IAppContext appContext;

		/// <summary>
		/// <see cref="FolderIdModelValidator"/>
		/// </summary>
		/// <param name="folderRepository"></param>
		/// <param name="appContext"></param>
		public FolderIdModelValidator(IFolderRepository folderRepository, IAppContext appContext)
		{
			this.folderRepository = folderRepository;
			this.appContext = appContext;

			RuleFor(x => x.Value)
				.NotEmpty()
				.MustAsync(async (id, _) => await folderRepository.RetrieveAsync<GetByIdModel?>(id, appContext.CurrentUserId) != null)
				.WithMessage("Folder not found");
		}
	}
}
