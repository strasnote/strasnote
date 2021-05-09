// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Strasnote.Data.Entities.Notes;
using Strasnote.Notes.Api.Models.Notes;
using Strasnote.Notes.Data.Abstracts;

namespace Strasnote.Notes.Api.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class NoteController : Controller
	{
		private readonly INoteRepository notes;

		public NoteController(INoteRepository notes) =>
			this.notes = notes;

		[HttpPost]
		public Task<long> Create(NoteEntity note) =>
			notes.CreateAsync(note);

		[HttpGet("{id}")]
		public Task<GetModel?> GetById(long id) =>
			notes.RetrieveAsync<GetModel>(id);
	}
}
