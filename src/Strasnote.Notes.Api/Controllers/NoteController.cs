// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Strasnote.AppBase.Abstracts;
using Strasnote.Logging;
using Strasnote.Notes.Api.Models.Notes;
using Strasnote.Notes.Data.Abstracts;

namespace Strasnote.Notes.Api.Controllers
{
	/// <summary>
	/// Note Controller
	/// </summary>
	[Authorize]
	[ApiController]
	[ApiVersion("1.0")]
	[Route("api/v{version:apiVersion}/note")]
	public class NoteController : Controller
	{
		private readonly INoteRepository notes;

		private readonly ITagRepository tags;

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="ctx">IAppContext</param>
		/// <param name="log">ILog</param>
		/// <param name="notes">INoteRepository</param>
		/// <param name="tags">ITagRepository</param>
		public NoteController(IAppContext ctx, ILog<NoteController> log, INoteRepository notes, ITagRepository tags) : base(ctx, log) =>
			(this.notes, this.tags) = (notes, tags);

		/// <summary>
		/// Creates a Note.
		/// </summary>
		/// <remarks>
		/// POST /note
		/// </remarks>
		/// <returns>The ID of the new Note</returns>
		[HttpPost]
		[ProducesResponseType(typeof(ulong), StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public Task<IActionResult> Create() =>
			IsAuthenticatedUserAsync(
				then: userId => notes.CreateAsync(new() { UserId = userId }),
				result: noteId => Created(nameof(GetById), noteId)
			);

		/// <summary>
		/// Creates a Note within a Folder.
		/// </summary>
		/// <remarks>
		/// POST /note/folder/42
		/// </remarks>
		/// <param name="folderId"></param>
		/// <returns>The ID of the new Note</returns>
		[HttpPost("folder/{folderId}")]
		[ProducesResponseType(typeof(ulong), StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public Task<IActionResult> CreateInFolder([FromRoute] Models.Folders.FolderIdModel folderId) =>
			IsAuthenticatedUserAsync(
				then: userId => notes.CreateAsync(new() { UserId = userId, FolderId = folderId.Value }),
				result: noteId => Created(nameof(GetById), noteId)
			);

		/// <summary>
		/// Moves a note to a folder. Set FolderId to null to remove the note from all folders.
		/// </summary>
		/// <remarks>
		/// {
		///		"FolderId": 42
		///	}
		/// </remarks>
		/// <param name="noteId">The ID of the note we're working with</param>
		/// <param name="model">MoveToFolderModel</param>
		/// <returns>The ID of the new folder</returns>
		[HttpPut("{noteId}/move")]
		[ProducesResponseType(typeof(ulong?), StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public Task<IActionResult> MoveToFolder([FromRoute] NoteIdModel noteId, MoveToFolderModel model) =>
			IsAuthenticatedUserAsync(
				then: userId => notes.UpdateAsync(noteId.Value, model, userId)
			);

		/// <summary>
		/// Retrieves a Note by ID.
		/// </summary>
		/// <remarks>
		/// GET /note/42
		/// </remarks>
		/// <param name="noteId">The Note ID</param>
		[HttpGet("{noteId}")]
		[ProducesResponseType(typeof(GetByIdModel), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public Task<IActionResult> GetById([FromRoute] NoteIdModel noteId) =>
			IsAuthenticatedUserAsync(
				then: userId => notes.RetrieveAsync<GetByIdModel?>(noteId.Value, userId)
			);

		/// <summary>
		/// Retrieves the Tags assigned to a Note.
		/// </summary>
		/// <remarks>
		/// GET /note/42/tags
		/// </remarks>
		/// <param name="noteId">The Note ID</param>
		[HttpGet("{noteId}/tags")]
		[ProducesResponseType(typeof(IEnumerable<TagModel>), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public Task<IActionResult> GetTags([FromRoute] NoteIdModel noteId) =>
			IsAuthenticatedUserAsync(
				then: userId => tags.GetForNote<TagModel>(noteId.Value, userId)
			);

		/// <summary>
		/// Saves Note content.
		/// </summary>
		/// <remarks>
		/// PUT /note/42
		/// {
		///     "noteContent": "..."
		/// }
		/// </remarks>
		/// <param name="noteId">The Note ID</param>
		/// <param name="model">Updated Note model</param>
		[HttpPut("{noteId}")]
		[ProducesResponseType(typeof(SaveContentModel), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public Task<IActionResult> SaveContent([FromRoute] NoteIdModel noteId, SaveContentModel model) =>
			IsAuthenticatedUserAsync(
				then: userId => notes.UpdateAsync<SaveContentModel?>(noteId.Value, model, userId)
			);

		/// <summary>
		/// Adds a Tag to a Note.
		/// </summary>
		/// <remarks>
		/// POST /note/42/tag
		/// {
		///     "tagId": 42
		/// }
		/// </remarks>
		/// <param name="noteId">The Note ID</param>
		/// <param name="model">Tag model</param>
		[HttpPost("{noteId}/tag")]
		[ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public Task<IActionResult> AddTag([FromRoute] NoteIdModel noteId, AddTagModel model) =>
			IsAuthenticatedUserAsync(
				then: userId => tags.AddToNote(model.TagId, noteId.Value, userId)
			);

		/// <summary>
		/// Removes a Tag from a Note.
		/// </summary>
		/// <remarks>
		/// DELETE /note/42/tag/42
		/// </remarks>
		/// <param name="noteId">The Note ID</param>
		/// <param name="tagId">The Tag ID</param>
		[HttpDelete("{noteId}/tag/{tagId}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public Task<IActionResult> RemoveTag([FromRoute] NoteIdModel noteId, [FromRoute] Models.Tags.TagIdModel tagId) =>
			IsAuthenticatedUserAsync(
				then: userId => tags.RemoveFromNote(tagId.Value, noteId.Value, userId),
				result: _ => NoContent()
			);

		/// <summary>
		/// Deletes a Note by ID.
		/// </summary>
		/// <remarks>
		/// DELETE /note/42
		/// </remarks>
		/// <param name="noteId">The Note ID</param>
		[HttpDelete("{noteId}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public Task<IActionResult> Delete([FromRoute] NoteIdModel noteId) =>
			IsAuthenticatedUserAsync(
				then: userId => notes.DeleteAsync(noteId.Value, userId),
				result: _ => NoContent()
			);
	}
}
