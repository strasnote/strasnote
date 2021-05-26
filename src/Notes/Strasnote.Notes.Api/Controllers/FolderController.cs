// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Strasnote.Data.Entities.Notes;
using Strasnote.Notes.Api.Models.Folders;
using Strasnote.Notes.Data.Abstracts;

namespace Strasnote.Notes.Api.Controllers
{
	[Authorize]
	[ApiController]
	[Route("[controller]")]
	public class FolderController : Controller
	{
		private readonly IFolderRepository folders;

		public FolderController(IFolderRepository folders) =>
			this.folders = folders;

		[HttpPost]
		public Task<long> Create(FolderEntity folder) =>
			folders.CreateAsync(folder);

		[HttpGet("{id}")]
		public Task<GetModel?> GetById(long id) =>
			folders.RetrieveAsync<GetModel?>(id);

		[HttpPut("{id}")]
		public Task<UpdateModel?> Update(long id, UpdateModel folder) =>
			folders.UpdateAsync<UpdateModel?>(id, folder);

		[HttpDelete("{id}")]
		public Task<int> Delete(long id) =>
			folders.DeleteAsync(id);
	}
}
