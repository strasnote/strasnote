// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using Microsoft.AspNetCore.Mvc;

namespace Strasnote.Apps.Web.Pages.Components.Footer;

public sealed record class FooterModel
{
	public string Years { get; init; } = string.Empty;
}

public sealed class FooterViewComponent : ViewComponent
{
	public IViewComponentResult Invoke() =>
		View(new FooterModel { Years = $"2021 - {DateTime.Now.Year}" });
}
