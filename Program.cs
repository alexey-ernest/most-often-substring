using System;
using System.Collections.Generic;
using System.Linq;

namespace Crossover
{
    internal class Program
    {
        private static void Main()
        {
            Console.WriteLine("Most Often Subscription Application.\n");
            Console.WriteLine("Input parameters in format: ");
            Console.WriteLine("N");
            Console.WriteLine("K L M");
            Console.WriteLine("S");
            Console.WriteLine("\n* where 2 <= N <= 100,000");
            Console.WriteLine("   2 <= K <= L <= 26, L < N");
            Console.WriteLine("   2 <= M <= 26");
            Console.WriteLine("   S - string of length N");

            try
            {
                // Read input parameters
                var n = int.Parse(Console.ReadLine() ?? string.Empty);
                var klm = (Console.ReadLine() ?? string.Empty).Split();
                var k = int.Parse(klm[0]);
                var l = int.Parse(klm[1]);
                var m = int.Parse(klm[2]);
                var str = Console.ReadLine();
                if (string.IsNullOrEmpty(str) || str.Length != n)
                {
                    throw new ApplicationException(string.Format("Specified string '{0}' should be of length {1}", str, n));
                }

                str = str.ToLower();

                // Validate input parameters
                if (!(n >= 2 && n <= 100000))
                {
                    throw new ApplicationException(string.Format("Invalid N parameter specified: {0}", n));
                }
                if (!(k >= 2 && k <= 26) || !(l >= 2 && l <= 26) || !(k <= l) && !(l < n))
                {
                    throw new ApplicationException(string.Format("Invalid K & L parameters specified: {0} & {1}", k, l));   
                }
                if (!(m >= 2 && m <= 26))
                {
                    throw new ApplicationException(string.Format("Invalid M parameter specified: {0}", m));
                }

                // Searching for substrings
                var substrings = new Dictionary<string, int>();
                for (var i = 0; i < n; ++i)
                {
                    // building subscrings of length between k and l
                    for (var j = k; j <= l && (i + j - 1 < n); j++)
                    {
                        var substr = str.Substring(i, j);
                        if (CountUniqueChars(substr) > m)
                        {
                            continue;
                        }

                        if (substrings.ContainsKey(substr))
                        {
                            substrings[substr]++;
                        }
                        else
                        {
                            substrings.Add(substr, 1);
                        }
                    }
                }

                if (substrings.Any())
                {
                    var mostOftenSubstringCount = substrings.Max(s => s.Value);
                    Console.WriteLine(mostOftenSubstringCount);
                }
                else
                {
                    Console.WriteLine(0);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e);
            }

            Console.ReadKey();
        }

        private static int CountUniqueChars(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return 0;
            }

            var chars = new HashSet<char>();
            foreach (var chr in str)
            {
                chars.Add(chr);
            }

            return chars.Count;
        }
    }
}