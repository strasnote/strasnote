﻿// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Strasnote.AppBase.Abstracts;
using Strasnote.Logging;
using Strasnote.Notes.Api.Models.Folders;
using Strasnote.Notes.Data.Abstracts;

namespace Strasnote.Notes.Api.Controllers
{
	/// <summary>
	/// Folder Controller
	/// </summary>
	[Authorize]
	[ApiController]
	[ApiVersion("1.0")]
	[Route("api/v{version:apiVersion}/folder")]
	public class FolderController : Controller
	{
		private readonly IFolderRepository folders;

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="ctx">IAppContext</param>
		/// <param name="log">ILog</param>
		/// <param name="folders">IFolderRepository</param>
		public FolderController(IAppContext ctx, ILog<FolderController> log, IFolderRepository folders) : base(ctx, log) =>
			 this.folders = folders;

		/// <summary>
		/// Creates a Folder.
		/// </summary>
		/// <remarks>
		/// POST /folder
		/// {
		///     "folderName": "...",
		///     "parentId": 42 (nullable)
		/// }
		/// </remarks>
		/// <returns>The ID of the new Folder</returns>
		[HttpPost]
		[ProducesResponseType(typeof(ulong), StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public Task<IActionResult> Create(CreateModel model) => 
			IsAuthenticatedUserAsync(
				then: userId => folders.CreateAsync(new() { FolderName = model.FolderName, FolderParentId = model.ParentId, UserId = userId }),
				result: folderId => Created(nameof(GetById), folderId)
			);

		/// <summary>
		/// Retrieves a Folder by ID.
		/// </summary>
		/// <remarks>
		/// GET /folder/42
		/// </remarks>
		/// <param name="folderId">The Folder ID</param>
		[HttpGet("{folderId}")]
		[ProducesResponseType(typeof(GetByIdModel), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public Task<IActionResult> GetById(FolderIdModel folderId) =>
			IsAuthenticatedUserAsync(
				then: userId => folders.RetrieveAsync<GetByIdModel?>(folderId.Value, userId)
			);

		/// <summary>
		/// Saves Folder name.
		/// </summary>
		/// <remarks>
		/// PUT /folder/42
		/// {
		///     "folderName": "..."
		/// }
		/// </remarks>
		/// <param name="folderId">The Folder ID</param>
		/// <param name="model">Updated Folder values</param>
		[HttpPut("{folderId}")]
		[ProducesResponseType(typeof(SaveNameModel), 200)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public Task<IActionResult> SaveName([FromRoute] FolderIdModel folderId, SaveNameModel model) =>
			IsAuthenticatedUserAsync(
				then: userId => folders.UpdateAsync(folderId.Value, model, userId)
			);

		/// <summary>
		/// Deletes a Folder by ID.
		/// </summary>
		/// <remarks>
		/// DELETE /folder/42
		/// </remarks>
		/// <param name="folderId">The Folder ID</param>
		[HttpDelete("{folderId}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public Task<IActionResult> Delete([FromRoute] FolderIdModel folderId) =>
			IsAuthenticatedUserAsync(
				then: userId => folders.DeleteAsync(folderId.Value, userId),
				result: _ => NoContent()
			);
	}
}
