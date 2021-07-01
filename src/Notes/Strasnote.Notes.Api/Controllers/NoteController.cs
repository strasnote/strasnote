// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
		[ProducesResponseType(typeof(ulong), 201)]
		[ProducesResponseType(401)]
		[ProducesResponseType(500)]
		public Task<IActionResult> Create() =>
			IsAuthenticatedUserAsync(
				then: userId => notes.CreateAsync(new() { UserId = userId }),
				result: noteId => Created(nameof(GetById), noteId)
			);

		/// <summary>
		/// Creates a Note within a Folder.
		/// </summary>
		/// <remarks>
		/// POST /note/in-folder
		/// {
		///     "folderId": 42
		/// }
		/// </remarks>
		/// <param name="model">CreateInFolderModel</param>
		/// <returns>The ID of the new Note</returns>
		[HttpPost("in-folder")]
		[ProducesResponseType(typeof(ulong), 201)]
		[ProducesResponseType(401)]
		[ProducesResponseType(500)]
		public Task<IActionResult> CreateInFolder([FromBody] CreateInFolderModel model) =>
			IsAuthenticatedUserAsync(
				then: userId => notes.CreateAsync(new() { UserId = userId, FolderId = model.FolderId }),
				result: noteId => Created(nameof(GetById), noteId)
			);

		/// <summary>
		/// Retrieves a Note by ID.
		/// </summary>
		/// <remarks>
		/// GET /note/42
		/// </remarks>
		/// <param name="noteId">The Note ID</param>
		[HttpGet("{noteId}")]
		[ProducesResponseType(typeof(GetByIdModel), 200)]
		[ProducesResponseType(401)]
		[ProducesResponseType(404)]
		[ProducesResponseType(500)]
		public Task<IActionResult> GetById(ulong noteId) =>
			IsAuthenticatedUserAsync(
				then: userId => notes.RetrieveAsync<GetByIdModel?>(noteId, userId)
			);

		/// <summary>
		/// Retrieves the Tags assigned to a Note.
		/// </summary>
		/// <remarks>
		/// GET /note/42/tags
		/// </remarks>
		/// <param name="noteId">The Note ID</param>
		[HttpGet("{noteId}/tags")]
		[ProducesResponseType(typeof(IEnumerable<TagModel>), 200)]
		[ProducesResponseType(401)]
		[ProducesResponseType(404)]
		[ProducesResponseType(500)]
		public Task<IActionResult> GetTags(ulong noteId) =>
			IsAuthenticatedUserAsync(
				then: userId => tags.GetForNote<TagModel>(noteId, userId)
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
		[ProducesResponseType(typeof(SaveContentModel), 200)]
		[ProducesResponseType(401)]
		[ProducesResponseType(404)]
		[ProducesResponseType(500)]
		public Task<IActionResult> SaveContent(ulong noteId, [FromBody] SaveContentModel model) =>
			IsAuthenticatedUserAsync(
				then: userId => notes.UpdateAsync<SaveContentModel?>(noteId, model, userId)
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
		[ProducesResponseType(typeof(bool), 200)]
		[ProducesResponseType(401)]
		[ProducesResponseType(404)]
		[ProducesResponseType(500)]
		public Task<IActionResult> AddTag(ulong noteId, [FromBody] AddTagModel model) =>
			IsAuthenticatedUserAsync(
				then: userId => tags.AddToNote(model.TagId, noteId, userId)
			);

		/// <summary>
		/// Deletes a Note by ID.
		/// </summary>
		/// <remarks>
		/// DELETE /note/42
		/// </remarks>
		/// <param name="noteId">The Note ID</param>
		[HttpDelete("{noteId}")]
		[ProducesResponseType(204)]
		[ProducesResponseType(401)]
		[ProducesResponseType(404)]
		[ProducesResponseType(500)]
		public Task<IActionResult> Delete(ulong noteId) =>
			IsAuthenticatedUserAsync(
				then: userId => notes.DeleteAsync(noteId, userId),
				result: _ => NoContent()
			);
	}
}
