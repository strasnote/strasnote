// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Strasnote.AppBase.Abstracts;
using Strasnote.Logging;
using Strasnote.Notes.Api.Models.Folders;
using Strasnote.Notes.Data.Abstracts;
using Strasnote.Util;

namespace Strasnote.Notes.Api.Controllers
{
	[Authorize]
	[ApiController]
	[Route("[controller]")]
	public class FolderController : Controller
	{
		private IAppContext ctx => Context;

		private readonly IFolderRepository folders;

		public FolderController(IAppContext ctx, ILog<FolderController> log, IFolderRepository folders) : base(ctx, log) =>
			 this.folders = folders;

		[HttpPost]
		public Task<long> Create([FromBody] string folderName) =>
			Is.AuthenticatedUser(
				ctx,
				userId => folders.CreateAsync(new()
				{
					FolderName = folderName,
					UserId = userId
				}),
				() => 0
			);

		[HttpPost("{parentFolderId}")]
		public Task<long> Create(long parentFolderId, [FromBody] string folderName) =>
			Is.AuthenticatedUser(
				ctx,
				userId =>
				folders.CreateAsync(new()
				{
					FolderName = folderName,
					FolderParentId = parentFolderId,
					UserId = userId
				}),
				() => 0
			);

		[HttpGet("{folderId}")]
		public Task<GetModel?> GetById(long folderId) =>
			folders.RetrieveAsync<GetModel?>(folderId, ctx.CurrentUserId);

		[HttpPut("{folderId}")]
		public Task<UpdateModel?> Update(long folderId, UpdateModel folder) =>
			folders.UpdateAsync<UpdateModel?>(folderId, folder, ctx.CurrentUserId);

		[HttpDelete("{folderId}")]
		public Task<int> Delete(long folderId) =>
			folders.DeleteAsync(folderId, ctx.CurrentUserId);
	}
}
