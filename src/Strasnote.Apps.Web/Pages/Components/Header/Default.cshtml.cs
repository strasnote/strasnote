// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using Microsoft.AspNetCore.Mvc;

namespace Strasnote.Apps.Web.Pages.Components.Header;

public sealed record class HeaderModel;

public sealed class HeaderViewComponent : ViewComponent
{
	public IViewComponentResult Invoke() =>
		View(new HeaderModel());
}
