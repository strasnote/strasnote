// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

namespace Strasnote.Apps.Web.Pages.Components.Sidebar;

public sealed record class Menu(
	string Title,
	MenuItem[] Items
);

public sealed record class MenuItem(
	string Text,
	string Page
);
