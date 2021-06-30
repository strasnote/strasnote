// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Strasnote.AppBase.Abstracts;
using Strasnote.Logging;
using Strasnote.Notes.Api.Models.Tags;
using Strasnote.Notes.Data.Abstracts;

namespace Strasnote.Notes.Api.Controllers
{
	/// <summary>
	/// Tag Controller
	/// </summary>
	[Authorize]
	[ApiController]
	[ApiVersion("1.0")]
	[Route("api/v{version:apiVersion}/[controller]")]
	public class TagController : Controller
	{
		private readonly ITagRepository tags;

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="ctx">IAppContext</param>
		/// <param name="log">ILog</param>
		/// <param name="tags">ITagRepository</param>
		public TagController(IAppContext ctx, ILog<NoteController> log, ITagRepository tags) : base(ctx, log) =>
			this.tags = tags;

		/// <summary>
		/// Creates a Tag.
		/// </summary>
		/// <remarks>
		/// POST /tag
		/// </remarks>
		/// <returns>The ID of the new Tag</returns>
		[HttpPost]
		[ProducesResponseType(typeof(ulong), 201)]
		[ProducesResponseType(401)]
		[ProducesResponseType(500)]
		public Task<IActionResult> Create([FromBody] CreateModel model) =>
			IsAuthenticatedUserAsync(
				then: userId => tags.CreateAsync(new() { TagName = model.TagName, UserId = userId }),
				result: noteId => Created(nameof(GetById), noteId)
			);

		/// <summary>
		/// Retrieves a Tag by ID.
		/// </summary>
		/// <remarks>
		/// GET /tag/42
		/// </remarks>
		/// <param name="tagId">The Tag ID</param>
		[HttpGet("{tagId}")]
		[ProducesResponseType(typeof(GetByIdModel), 200)]
		[ProducesResponseType(401)]
		[ProducesResponseType(404)]
		[ProducesResponseType(500)]
		public Task<IActionResult> GetById(ulong tagId) =>
			IsAuthenticatedUserAsync(
				then: userId => tags.RetrieveAsync<GetByIdModel?>(tagId, userId)
			);

		/// <summary>
		/// Saves Tag content.
		/// </summary>
		/// <remarks>
		/// PUT /tag/42
		/// {
		///     "tagName": "..."
		/// }
		/// </remarks>
		/// <param name="tagId">The Tag ID</param>
		/// <param name="model">Updated Tag values</param>
		[HttpPut("{tagId}")]
		[ProducesResponseType(typeof(SaveNameModel), 200)]
		[ProducesResponseType(401)]
		[ProducesResponseType(404)]
		[ProducesResponseType(500)]
		public Task<IActionResult> SaveContent(ulong tagId, [FromBody] SaveNameModel model) =>
			IsAuthenticatedUserAsync(
				then: userId => tags.UpdateAsync<SaveNameModel?>(tagId, model, userId)
			);

		/// <summary>
		/// Deletes a Tag by ID.
		/// </summary>
		/// <remarks>
		/// DELETE /tag/42
		/// </remarks>
		/// <param name="tagId">The Tag ID</param>
		[HttpDelete("{tagId}")]
		[ProducesResponseType(200)]
		[ProducesResponseType(401)]
		[ProducesResponseType(404)]
		[ProducesResponseType(500)]
		public Task<IActionResult> Delete(ulong tagId) =>
			IsAuthenticatedUserAsync(
				then: userId => tags.DeleteAsync(tagId, userId),
				result: affected => affected switch
				{
					1 =>
						Ok(true),

					_ =>
						NotFound()
				}
			);
	}
}
