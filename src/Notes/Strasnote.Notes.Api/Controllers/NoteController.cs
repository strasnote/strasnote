// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Strasnote.AppBase.Abstracts;
using Strasnote.Logging;
using Strasnote.Notes.Api.Models.Notes;
using Strasnote.Notes.Data.Abstracts;
using Swashbuckle.AspNetCore.Annotations;

namespace Strasnote.Notes.Api.Controllers
{
	/// <summary>
	/// Note Controller
	/// </summary>
	[Authorize]
	[ApiController]
	[Route("[controller]")]
	public class NoteController : Controller
	{
		private readonly INoteRepository notes;

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="ctx">IAppContext</param>
		/// <param name="log">ILog</param>
		/// <param name="notes">INoteRepository</param>
		public NoteController(IAppContext ctx, ILog<NoteController> log, INoteRepository notes) : base(ctx, log) =>
			this.notes = notes;

		/// <summary>
		/// Creates a Note.
		/// </summary>
		/// <remarks>
		/// POST /Note
		/// </remarks>
		/// <returns>The ID of the new Note</returns>
		[HttpPost]
		[SwaggerResponse(201, "The note was created.", typeof(long))]
		[SwaggerResponse(401, "The user is not authorised.")]
		[SwaggerResponse(500)]
		public Task<IActionResult> Create() =>
			IsAuthenticatedUserAsync(
				then: userId => notes.CreateAsync(new() { UserId = userId }),
				result: noteId => Created(nameof(GetById), noteId)
			);

		/// <summary>
		/// Creates a Note within a folder.
		/// </summary>
		/// <remarks>
		/// POST /Note/InFolder
		/// {
		///     "folderId": 42
		/// }
		/// </remarks>
		/// <param name="model">CreateInFolderModel</param>
		/// <returns>The ID of the new Note</returns>
		[HttpPost("InFolder")]
		[SwaggerResponse(201, "The note was created.", typeof(long))]
		[SwaggerResponse(401, "The user is not authorised.")]
		[SwaggerResponse(500)]
		public Task<IActionResult> CreateInFolder([FromBody] CreateInFolderModel model) =>
			IsAuthenticatedUserAsync(
				then: userId => notes.CreateAsync(new() { UserId = userId, FolderId = model.FolderId }),
				result: noteId => Created(nameof(GetById), noteId)
			);

		/// <summary>
		/// Retrieves a Note by ID.
		/// </summary>
		/// <remarks>
		/// GET /Note/42
		/// </remarks>
		/// <param name="noteId">The Note ID</param>
		[HttpGet("{noteId}")]
		[SwaggerResponse(200, "The requested note.", typeof(GetByIdModel))]
		[SwaggerResponse(401, "The user is not authorised.")]
		[SwaggerResponse(500)]
		public Task<IActionResult> GetById(long noteId) =>
			IsAuthenticatedUserAsync(
				then: userId => notes.RetrieveAsync<GetByIdModel?>(noteId, userId)
			);

		/// <summary>
		/// Saves Note content.
		/// </summary>
		/// <remarks>
		/// PUT /Note/42
		/// {
		///     "noteContent": "..."
		/// }
		/// </remarks>
		/// <param name="noteId">The Note ID</param>
		/// <param name="model">Updated Note values</param>
		[HttpPut("{noteId}")]
		[SwaggerResponse(200, "Updated note content.", typeof(SaveContentModel))]
		[SwaggerResponse(401, "The user is not authorised.")]
		[SwaggerResponse(500)]
		public Task<IActionResult> SaveContent(long noteId, [FromBody] SaveContentModel model) =>
			IsAuthenticatedUserAsync(
				then: userId => notes.UpdateAsync<SaveContentModel?>(noteId, model, userId)
			);

		/// <summary>
		/// Deletes a Note by ID.
		/// </summary>
		/// <remarks>
		/// DELETE /Note/42
		/// </remarks>
		/// <param name="noteId">The Note ID</param>
		[HttpDelete("{noteId}")]
		[SwaggerResponse(200, "The note was successfully deleted.")]
		[SwaggerResponse(401, "The user is not authorised.")]
		[SwaggerResponse(404, "The note could not be found.")]
		[SwaggerResponse(500)]
		public Task<IActionResult> Delete(long noteId) =>
			IsAuthenticatedUserAsync(
				then: userId => notes.DeleteAsync(noteId, userId),
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
