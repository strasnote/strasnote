// The MIT License (MIT)
//
// Copyright © 2021 bfren.uk
//
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software 
// and associated documentation files (the “Software”), to deal in the Software without 
// restriction, including without limitation the rights to use, copy, modify, merge, publish, 
// distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the 
// Software is furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all copies or 
// substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING 
// BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND 
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, 
// DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING 
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//
// See https://github.com/bencgreen/jeebs/blob/main/Libraries/Jeebs/Functions

using System;
using System.Security.Cryptography;

namespace Strasnote.Util
{
	public static partial class Rnd
	{
		public static class RndNumber
		{
			private const string MinimumMustBeLessThanMaximum = "Minimium value must be less than the maximum value.";

			private const string MinimumMustBeAtLeastZero = "Minimum value must be at least 0.";

			/// <summary>
			/// Returns a random number between 0 and 1
			/// </summary>
			/// <remarks>
			/// Thanks to https://stackoverflow.com/users/11178549/theodor-zoulias for comments and suggested improvements
			/// - see https://stackoverflow.com/a/64264895/8199362
			/// </remarks>
			/// <param name="generator">[Optional] Random Number Generator - if null will use <see cref="RandomNumberGenerator"/></param>
			public static double Get(RandomNumberGenerator? generator = null)
			{
				// Get 8 random bytes to convert into a 64-bit integer
				var lng = BitConverter.ToInt64(RndBytes.Get(8, generator), 0);
				var dbl = (double)(lng < 0 ? ~lng : lng);

				// Convert to a random number between 0 and 1
				return dbl / long.MaxValue;
			}

			/// <summary>
			/// Returns a random integer between <paramref name="min"/> and <paramref name="max"/> inclusive
			/// </summary>
			/// <remarks>
			/// Don't share code with <see cref="GetInt64(long, long, RandomNumberGenerator?)"/> for memory allocation reasons
			/// </remarks>
			/// <param name="min">Minimum acceptable value</param>
			/// <param name="max">Maximum acceptable value</param>
			/// <param name="generator">[Optional] Random Number Generator - if null will use <see cref="RandomNumberGenerator"/></param>
			public static int GetInt32(int min = 0, int max = int.MaxValue, RandomNumberGenerator? generator = null)
			{
				// Check arguments
				if (min >= max)
				{
					throw new ArgumentOutOfRangeException(nameof(min), min, MinimumMustBeLessThanMaximum);
				}

				if (min < 0)
				{
					throw new ArgumentException(MinimumMustBeAtLeastZero, nameof(min));
				}

				// Get the range between the specified minimum and maximum values
				var range = max - min;

				// Now add a random amount of the range to the minimum value - it will never exceed maximum value
				var add = Math.Round(range * Get(generator));
				return (int)(min + add);
			}

			/// <summary>
			/// Returns a random integer between <paramref name="min"/> and <paramref name="max"/> inclusive
			/// </summary>
			/// <remarks>
			/// Don't share code with <see cref="GetUInt64(ulong, ulong, RandomNumberGenerator?)"/> for memory allocation reasons
			/// </remarks>
			/// <param name="min">Minimum acceptable value</param>
			/// <param name="max">Maximum acceptable value</param>
			/// <param name="generator">[Optional] Random Number Generator - if null will use <see cref="RandomNumberGenerator"/></param>
			public static uint GetUInt32(uint min = 0, uint max = uint.MaxValue, RandomNumberGenerator? generator = null)
			{
				// Check arguments
				if (min >= max)
				{
					throw new ArgumentOutOfRangeException(nameof(min), min, MinimumMustBeLessThanMaximum);
				}

				// Get the range between the specified minimum and maximum values
				var range = max - min;

				// Now add a random amount of the range to the minimum value - it will never exceed maximum value
				var add = Math.Round(range * Get(generator));
				return (uint)(min + add);
			}

			/// <summary>
			/// Returns a random integer between <paramref name="min"/> and <paramref name="max"/> inclusive
			/// </summary>
			/// <remarks>
			/// Don't share code with <see cref="GetInt32(int, int, RandomNumberGenerator?)"/> for memory allocation reasons
			/// </remarks>
			/// <param name="min">Minimum acceptable value</param>
			/// <param name="max">Maximum acceptable value</param>
			/// <param name="generator">[Optional] Random Number Generator - if null will use <see cref="RandomNumberGenerator"/></param>
			public static long GetInt64(long min = 0, long max = long.MaxValue, RandomNumberGenerator? generator = null)
			{
				// Check arguments
				if (min >= max)
				{
					throw new ArgumentOutOfRangeException(nameof(min), min, MinimumMustBeLessThanMaximum);
				}

				if (min < 0)
				{
					throw new ArgumentException(MinimumMustBeAtLeastZero, nameof(min));
				}

				// Get the range between the specified minimum and maximum values
				var range = max - min;

				// Now add a random amount of the range to the minimum value - it will never exceed maximum value
				var add = Math.Round(range * Get(generator));
				return (long)(min + add);
			}

			/// <summary>
			/// Returns a random integer between <paramref name="min"/> and <paramref name="max"/> inclusive
			/// </summary>
			/// <remarks>
			/// Don't share code with <see cref="GetUInt32(uint, uint, RandomNumberGenerator?)"/> for memory allocation reasons
			/// </remarks>
			/// <param name="min">Minimum acceptable value</param>
			/// <param name="max">Maximum acceptable value</param>
			/// <param name="generator">[Optional] Random Number Generator - if null will use <see cref="RandomNumberGenerator"/></param>
			public static ulong GetUInt64(ulong min = 0, ulong max = ulong.MaxValue, RandomNumberGenerator? generator = null)
			{
				// Check arguments
				if (min >= max)
				{
					throw new ArgumentOutOfRangeException(nameof(min), min, MinimumMustBeLessThanMaximum);
				}

				// Get the range between the specified minimum and maximum values
				var range = max - min;

				// Now add a random amount of the range to the minimum value - it will never exceed maximum value
				var add = Math.Round(range * Get(generator));
				return (ulong)(min + add);
			}
		}
	}
}
