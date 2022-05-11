// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using Microsoft.AspNetCore.Mvc;

namespace Strasnote.Apps.Web.Pages.Home;

public sealed record class Form
{
	public string Text { get; init; } = "I don't know who you are - please tell me!";

	public bool Reset { get; init; }

	public string? Name { get; init; }
}

public sealed partial class FormModel
{
	public IActionResult OnPostName(Form form)
	{
		if (form.Reset)
		{
			return new JsonResult(new { redirect = "refresh" });
		}

		return Partial("_Name", form.Name switch
		{
			string s =>
					new Form { Text = $"Hello, {s}!" },

			_ =>
				new Form { Text = "I still don't know who you are :-p!" }
		});
	}
}
