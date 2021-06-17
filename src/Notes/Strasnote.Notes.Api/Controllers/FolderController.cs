// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Strasnote.AppBase.Abstracts;
using Strasnote.Logging;
using Strasnote.Notes.Api.Models.Folders;
using Strasnote.Notes.Data.Abstracts;
using Swashbuckle.AspNetCore.Annotations;

namespace Strasnote.Notes.Api.Controllers
{
	/// <summary>
	/// Folder Controller
	/// </summary>
	[Authorize]
	[ApiController]
	[Route("[controller]")]
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
		/// POST /Folder
		/// {
		///     "folderName": "..."
		/// }
		/// </remarks>
		/// <returns>The ID of the new Folder</returns>
		[HttpPost]
		[SwaggerResponse(201, "The folder was created.", typeof(long))]
		[SwaggerResponse(401, "The user is not authorised.")]
		[SwaggerResponse(500)]
		public Task<IActionResult> Create([FromBody] CreateModel model) =>
			IsAuthenticatedUserAsync(
				then: userId => folders.CreateAsync(new() { FolderName = model.FolderName, UserId = userId }),
				result: folderId => Created(nameof(GetById), folderId)
			);

		/// <summary>
		/// Creates a Folder within a parent folder.
		/// </summary>
		/// <remarks>
		/// POST /Folder/InFolder
		/// {
		///     "folderName": "...",
		///     "parentId": 42
		/// }
		/// </remarks>
		/// <param name="model">CreateInFolderModel</param>
		/// <returns>The ID of the new Note</returns>
		[HttpPost("InFolder")]
		[SwaggerResponse(201, "The folder was created.", typeof(long))]
		[SwaggerResponse(401, "The user is not authorised.")]
		[SwaggerResponse(500)]
		public Task<IActionResult> CreateInFolder([FromBody] CreateInFolderModel model) =>
			IsAuthenticatedUserAsync(
				then: userId => folders.CreateAsync(new() { FolderName = model.FolderName, FolderParentId = model.ParentId, UserId = userId }),
				result: folderId => Created(nameof(GetById), folderId)
			);

		/// <summary>
		/// Retrieves a Folder by ID.
		/// </summary>
		/// <remarks>
		/// GET /Folder/42
		/// </remarks>
		/// <param name="folderId">The Folder ID</param>
		[HttpGet("{folderId}")]
		[SwaggerResponse(200, "The requested folder.", typeof(GetByIdModel))]
		[SwaggerResponse(401, "The user is not authorised.")]
		[SwaggerResponse(500)]
		public Task<IActionResult> GetById(long folderId) =>
			IsAuthenticatedUserAsync(
				then: userId => folders.RetrieveAsync<GetByIdModel?>(folderId, userId)
			);

		/// <summary>
		/// Saves Folder name.
		/// </summary>
		/// <remarks>
		/// PUT /Folder/42
		/// {
		///     "folderName": "..."
		/// }
		/// </remarks>
		/// <param name="folderId">The Folder ID</param>
		/// <param name="model">Updated Folder values</param>
		[HttpPut("{folderId}")]
		[SwaggerResponse(200, "Updated folder name.", typeof(SaveNameModel))]
		[SwaggerResponse(401, "The user is not authorised.")]
		[SwaggerResponse(500)]
		public Task<IActionResult> SaveName(long folderId, SaveNameModel model) =>
			IsAuthenticatedUserAsync(
				then: userId => folders.UpdateAsync(folderId, model, userId)
			);

		/// <summary>
		/// Deletes a Folder by ID.
		/// </summary>
		/// <remarks>
		/// DELETE /Folder/42
		/// </remarks>
		/// <param name="folderId">The Folder ID</param>
		[HttpDelete("{folderId}")]
		[SwaggerResponse(200, "The folder was successfully deleted.")]
		[SwaggerResponse(401, "The user is not authorised.")]
		[SwaggerResponse(404, "The folder could not be found.")]
		[SwaggerResponse(500)]
		public Task<IActionResult> Delete(long folderId) =>
			IsAuthenticatedUserAsync(
				then: userId => folders.DeleteAsync(folderId, userId),
				result: affected => affected switch
				{
					1 =>
						Ok(),

					_ =>
						NotFound()
				}
			);
	}
}
