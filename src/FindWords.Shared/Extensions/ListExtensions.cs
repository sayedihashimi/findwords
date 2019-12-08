using System;
using System.Collections.Generic;
using System.Linq;

namespace FindWords.Shared.Extensions {
    public static class IEnumerableExtensions {
        public static IEnumerable<T> Randomize<T>(this IEnumerable<T> source) {
            Random rnd = new Random();
            return source.OrderBy<T, int>((item) => rnd.Next());
        }
    }
}
