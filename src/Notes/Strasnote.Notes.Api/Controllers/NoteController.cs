// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Strasnote.Data.Entities.Notes;
using Strasnote.Notes.Api.Models.Notes;
using Strasnote.Notes.Data.Abstracts;

namespace Strasnote.Notes.Api.Controllers
{
	[Authorize]
	[ApiController]
	[Route("[controller]")]
	public class NoteController : Controller
	{
		private readonly INoteRepository notes;

		public NoteController(INoteRepository notes) =>
			this.notes = notes;

		[HttpPost]
		public Task<long> Create(CreateModel note)
		{
			// ToDo: we should consider some sort of mapping system, even if it's just simple extension methods
			var noteEntity = new NoteEntity
			{
				FolderId = note.FolderId
			};

			return notes.CreateAsync(noteEntity);
		}

		[HttpGet("{id}")]
		public Task<GetModel?> GetById(long id) =>
			notes.RetrieveAsync<GetModel?>(id);

		[HttpPut("{id}")]
		public Task<UpdateModel?> Update(long id, UpdateModel note) =>
			notes.UpdateAsync<UpdateModel?>(id, note);

		[HttpDelete("{id}")]
		public Task<int> Delete(long id) =>
			notes.DeleteAsync(id);
	}
}
