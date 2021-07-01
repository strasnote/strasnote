// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

namespace Strasnote.Data
{
	/// <summary>
	/// Stored Procedure names
	/// </summary>
	public static class StoredProcedure
	{
		/// <summary>
		/// Add a Tag to a Note
		/// </summary>
		public static string AddTagToNote =>
			nameof(AddTagToNote);

		/// <summary>
		/// Get Tag Ids for a Note
		/// </summary>
		public static string GetTagIdsForNote =>
			nameof(GetTagIdsForNote);

		/// <summary>
		/// Remove tag from a Note
		/// </summary>
		public static string RemoveTagFromNote =>
			nameof(RemoveTagFromNote);

		/// <summary>
		/// Remove tag from all Notes
		/// </summary>
		public static string RemoveTagFromNotes =>
			nameof(RemoveTagFromNotes);
	}
}
