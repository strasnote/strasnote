// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using NSubstitute;
using Strasnote.AppBase.Abstracts;
using Strasnote.Notes.Data.Abstracts;
using Strasnote.Util;

namespace Strasnote.Notes.Api.Controllers.NoteController_Tests
{
	public abstract class NoteController_Tests
	{
		public static (NoteController, Vars) Setup()
		{
			var ctx = Substitute.For<IAppContext>();
			ctx.IsAuthenticated.Returns(true);

			var userId = Rnd.Lng;
			ctx.CurrentUserId.Returns(userId);

			var notes = Substitute.For<INoteRepository>();

			return (new(ctx, notes), new(ctx, userId, notes));
		}

		public sealed record Vars(
			IAppContext AppContext,
			long UserId,
			INoteRepository Notes
		);
	}
}
