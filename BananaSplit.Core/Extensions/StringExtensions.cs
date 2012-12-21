using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Globalization;

namespace BananaSplit.Core.Extensions
{

	public static class StringExtensions {

		static Regex _scrubSpecial = new Regex(@"[^a-zA-Z0-9]");
		static Regex _scrubSpaces = new Regex(@"([\s])+");

		/// <summary>
		/// Creates a scrubbed and url friendly version of the string. Defaults to using hyphens.
		/// </summary>
		/// <example> string url = myString.ToScrubbedURL(); </example>
		/// <returns>title-to-some-page-on-our-site</returns>
		public static string ToScrubbed(this string input)  {
			return input.ToScrubbed("-");
		}

		/// <summary>
		/// Creates a scrubbed and url friendly version of the string.
		/// </summary>
		/// <example> string url = myString.ToScrubbedURL("-"); </example>
		/// <param name="replacement"></param>
		/// <returns>title-to-some-page-on-our-site</returns>
		public static string ToScrubbed(this string input, string replacement) {
			
			// normalize the string by removing any diacritics, accent marks
			var normalized = input.ToLower().Normalize(NormalizationForm.FormD);

			// handle special cases
			normalized = normalized.Replace("$", "s").Replace("&", "and");

			var sb = new StringBuilder();
			foreach (char c in normalized) {
				var uc = CharUnicodeInfo.GetUnicodeCategory(c);
				if (uc != UnicodeCategory.NonSpacingMark && uc != UnicodeCategory.OtherPunctuation) {
					sb.Append(c);
				}
			}

			var scrubbed = sb.ToString().Normalize(NormalizationForm.FormC);

			// lets remove all the special characters
			scrubbed = _scrubSpecial.Replace(scrubbed, " ");

			// now finally replace all the spaces
			return _scrubSpaces.Replace(scrubbed.Trim(), replacement);
		}


        /// <summary>
        /// Converts a string into a byte array
        /// </summary>
        /// <param name="input">string to convert</param>
        /// <returns>returns an array of bytes</returns>
        /// 
        public static byte[] ToByteArray(this string input)
        {            
            var encoding = new ASCIIEncoding();
            return encoding.GetBytes(input);
        }

        /// <summary>
        /// Converts a byte array into a string
        /// </summary>
        /// <param name="bytes">The bytes to convert</param>
        /// <returns>Returns a string</returns> 
        /// 
        public static String GetString(this byte[] bytes)
        {
            var encoding = new ASCIIEncoding();
            return encoding.GetString(bytes);
        }
	}
}
