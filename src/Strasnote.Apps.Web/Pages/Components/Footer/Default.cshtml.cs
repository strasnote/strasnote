// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.Reflection;
using Microsoft.AspNetCore.Mvc;

namespace Strasnote.Apps.Web.Pages.Components.Footer;

public sealed record class FooterModel(
	string Build,
	DateTime LastModified
);

public sealed class FooterViewComponent : ViewComponent
{
	public IViewComponentResult Invoke() =>
		View(new FooterModel(
			Build: "v0.8",
			LastModified: File.GetLastWriteTime(Assembly.GetExecutingAssembly().Location)
		));
}
