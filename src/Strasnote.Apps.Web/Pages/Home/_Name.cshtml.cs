// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using Microsoft.AspNetCore.Mvc;

namespace Strasnote.Apps.Web.Pages.Home;

public sealed record class Person
{
	public string Text { get; init; } = "I don't know who you are - please tell me!";

	public string? Name { get; init; }
}

public sealed partial class FormModel
{
	public PartialViewResult OnPostName(Person person) =>
		Partial("_Name", person.Name switch
		{
			string s =>
					new Person { Text = $"Hello, {s}!" },

			_ =>
				new Person { Text = "I still don't know who you are :-p!" }
		});
}
