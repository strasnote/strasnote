// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Strasnote.AppBase.Abstracts;
using Strasnote.Logging;
using Strasnote.Notes.Api.Models.Notes;
using Strasnote.Notes.Data.Abstracts;
using Strasnote.Util;
using Swashbuckle.AspNetCore.Annotations;

namespace Strasnote.Notes.Api.Controllers
{
	[Authorize]
	[ApiController]
	[Route("[controller]")]
	public class NoteController : Controller
	{
		private readonly INoteRepository notes;

		public NoteController(IAppContext ctx, ILog<NoteController> log, INoteRepository notes) : base(ctx, log) =>
			this.notes = notes;

		/// <summary>
		/// Creates a Note
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
		/// Creates a Note within a folder
		/// </summary>
		/// <remarks>
		/// POST /Note/folderId
		/// </remarks>
		/// <param name="folderId">Folder ID</param>
		/// <returns>The ID of the new Note</returns>
		[HttpPost("{folderId}")]
		[SwaggerResponse(201, "The note was created.", typeof(long))]
		[SwaggerResponse(401, "The user is not authorised.")]
		[SwaggerResponse(500)]
		public Task<IActionResult> Create(long folderId) =>
			IsAuthenticatedUserAsync(
				then: userId => notes.CreateAsync(new() { UserId = userId, FolderId = folderId }),
				result: noteId => Created(nameof(GetById), noteId)
			);

		[HttpGet("{noteId}")]
		public Task<IActionResult> GetById(long noteId) =>
			IsAuthenticatedUserAsync(
				then: userId => notes.RetrieveAsync<GetModel?>(noteId, userId)
			);

		[HttpPut("{noteId}")]
		public Task<IActionResult> Update(long noteId, [FromBody] UpdateModel note) =>
			IsAuthenticatedUserAsync(
				then: userId => notes.UpdateAsync<UpdateModel?>(noteId, note, userId)
			);


		[HttpDelete("{noteId}")]
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
