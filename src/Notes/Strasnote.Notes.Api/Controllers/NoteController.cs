// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Strasnote.AppBase.Abstracts;
using Strasnote.Notes.Api.Models.Notes;
using Strasnote.Notes.Data.Abstracts;
using Strasnote.Util;

namespace Strasnote.Notes.Api.Controllers
{
	[Authorize]
	[ApiController]
	[Route("[controller]")]
	public class NoteController : Controller
	{
		private readonly IAppContext ctx;

		private readonly INoteRepository notes;

		public NoteController(IAppContext ctx, INoteRepository notes) =>
			(this.ctx, this.notes) = (ctx, notes);

		[HttpPost]
		public Task<long> Create() =>
			Is.AuthenticatedUser(
				ctx,
				userId => notes.CreateAsync(new()
				{
					UserId = userId
				}),
				() => 0
			);

		[HttpPost("{folderId}")]
		public Task<long> Create(long folderId) =>
			Is.AuthenticatedUser(
				ctx,
				userId => notes.CreateAsync(new()
				{
					UserId = userId,
					FolderId = folderId
				}),
				() => 0
			);

		[HttpGet("{noteId}")]
		public Task<GetModel?> GetById(long noteId) =>
			notes.RetrieveAsync<GetModel?>(noteId, ctx.CurrentUserId);

		[HttpPut("{noteId}")]
		public Task<UpdateModel?> Update(long noteId, UpdateModel note) =>
			notes.UpdateAsync<UpdateModel?>(noteId, note, ctx.CurrentUserId);

		[HttpDelete("{noteId}")]
		public Task<int> Delete(long noteId) =>
			notes.DeleteAsync(noteId, ctx.CurrentUserId);
	}
}
