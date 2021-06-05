// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using NSubstitute;
using Strasnote.AppBase.Abstracts;
using Strasnote.Logging;
using Strasnote.Notes.Data.Abstracts;
using Strasnote.Util;

namespace Strasnote.Notes.Api.Controllers.FolderController_Tests
{
	public abstract class FolderController_Tests
	{
		public static (FolderController, Vars) Setup()
		{
			var ctx = Substitute.For<IAppContext>();
			ctx.IsAuthenticated.Returns(true);

			var userId = Rnd.Lng;
			ctx.CurrentUserId.Returns(userId);

			var log = Substitute.For<ILog<FolderController>>();

			var folders = Substitute.For<IFolderRepository>();

			return (new(ctx, log, folders), new(ctx, log, userId, folders));
		}

		public sealed record Vars(
			IAppContext AppContext,
			ILog<FolderController> Log,
			long UserId,
			IFolderRepository Folders
		);
	}
}
