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
		/// Add a tag to a note
		/// </summary>
		public static string AddTagToNote =>
			nameof(AddTagToNote);

		/// <summary>
		/// Get tag Ids for a note
		/// </summary>
		public static string GetTagIdsForNote =>
			nameof(GetTagIdsForNote);
	}
}
