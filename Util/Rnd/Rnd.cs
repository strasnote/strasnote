/*
 * The MIT License (MIT)
 * 
 * Copyright © 2021 bcg|design <ben@bcgdesign.com>
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy of this software 
 * and associated documentation files (the “Software”), to deal in the Software without 
 * restriction, including without limitation the rights to use, copy, modify, merge, publish, 
 * distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the 
 * Software is furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all copies or 
 * substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING 
 * BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND 
 * NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, 
 * DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING 
 * FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 * 
 * See https://github.com/bencgreen/jeebs/blob/main/Libraries/Jeebs/Functions/_Rnd.cs
 */

namespace Strasnote.Util
{
	public static partial class Rnd
	{
		/// <summary>
		/// Generate a random string 6 characters long, containing uppercase and lowercase letters but no numbers or special characters
		/// </summary>
		public static string Str =>
			RndString.Get(6);

		/// <summary>
		/// Generate a random 32-bit integer between 0 and 1000
		/// </summary>
		public static int Int =>
			RndNumber.GetInt32(max: 1000);

		/// <summary>
		/// Generate a random 64-bit integer between 0 and 1000
		/// </summary>
		public static long Lng =>
			RndNumber.GetInt64(max: 1000L);
	}
}
