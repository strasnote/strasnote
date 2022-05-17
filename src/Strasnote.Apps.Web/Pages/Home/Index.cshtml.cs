// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Strasnote.Apps.Web.Pages.Home;

public sealed class IndexModel : PageModel
{
	public IActionResult OnGetTime() =>
		Content(DateTime.Now.ToString(Formats.DateTimeFull));
}
