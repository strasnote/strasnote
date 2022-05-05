// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using FluentValidation;
using Strasnote.AppBase.Abstracts;
using Strasnote.Notes.Data.Abstracts;

namespace Strasnote.Notes.Api.Models.Notes
{
	/// <summary>
	/// See <see cref="Controllers.NoteController.MoveToFolder(NoteIdModel, MoveToFolderModel)"/>
	/// </summary>
	public sealed record MoveToFolderModel(ulong? FolderId);

	/// <summary>
	/// Validator for <see cref="MoveToFolderModel"/>
	/// </summary>
	public sealed class MoveToFolderModelValidator : AbstractValidator<MoveToFolderModel>
	{
		private readonly IAppContext appContext;
		private readonly IFolderRepository folderRepository;

		/// <summary>
		/// <see cref="MoveToFolderModelValidator"/>
		/// </summary>
		public MoveToFolderModelValidator(IAppContext appContext, IFolderRepository folderRepository)
		{
			this.appContext = appContext;
			this.folderRepository = folderRepository;

			RuleFor(x => x.FolderId)
				.MustAsync(async (folderId, _) => await folderRepository.RetrieveAsync<GetByIdModel?>((ulong)folderId!, appContext.CurrentUserId) != null)
				.WithMessage("Folder does not exist")
				.When(x => x.FolderId != null);
		}
	}
}
