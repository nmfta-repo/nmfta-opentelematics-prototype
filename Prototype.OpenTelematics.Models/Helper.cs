using System;
using System.Collections.Generic;
using System.Text;

namespace Prototype.OpenTelematics.Models
{
    public class Helper
    {
        public delegate bool TryDelegate<T>(string s, out T result);

        public static bool TryParseNullable<T>(string s, out T? result, TryDelegate<T> tryDelegate) where T : struct
        {
            if (s == null)
            {
                result = null;
                return true;
            }

            T temp;
            bool success = tryDelegate(s, out temp);
            result = temp;
            return success;
        }

        public static T? TryParse<T>(string s, TryDelegate<T> tryDelegate) where T : struct
        {
            if (s == null)
            {
                return null;
            }

            T temp;
            return tryDelegate(s, out temp)
                ? (T?)temp
                : null;
        }

        /// <summary>
        /// Location should be of form "latitude longitude", sample 37.4224764 -122.0842499
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static (decimal? latitude, decimal? longitude) TryParseLocation(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return (null, null);

            var coordinates = input.Split(' ');
            if (coordinates.Length != 2)
            {
                return (null, null);
            }

            return (TryParse<decimal>(coordinates[0], decimal.TryParse),
                TryParse<decimal>(coordinates[1], decimal.TryParse));
        }
    }
}
