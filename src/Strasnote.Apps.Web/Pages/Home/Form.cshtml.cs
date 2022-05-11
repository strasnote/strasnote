// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Strasnote.Apps.Web.Pages.Home;

public sealed partial class FormModel : PageModel
{
	public Person Person { get; init; } = new();
}