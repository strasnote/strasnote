// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using Microsoft.AspNetCore.Mvc;

namespace Strasnote.Apps.Web.Pages.Components.Sidebar;

public sealed record class SidebarModel(
	Menu[] Menus
);

public sealed class SidebarViewComponent : ViewComponent
{
	public IViewComponentResult Invoke()
	{
		var mainMenu = new Menu(
			Title: "Menu",
			Items: new MenuItem[]
			{
				new("Home", "Index"),
				new("Inbox", "Notes/Inbox"),
				new("Tags", "Tags/Index")
			}
		);

		var accountMenu = new Menu(
			Title: "Account",
			Items: new MenuItem[]
			{
				new("Sign Out", "Auth/SignOut")
			}
		);

		return View(new SidebarModel(new[] { mainMenu, accountMenu }));
	}
}
