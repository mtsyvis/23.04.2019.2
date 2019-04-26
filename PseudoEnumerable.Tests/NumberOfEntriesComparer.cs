using System.Collections.Generic;

namespace PseudoEnumerable.Tests
{
    public class NumberOfEntriesComparer : IComparer<string>
    {
        /// <summary>
        /// The symbol
        /// </summary>
        private readonly char symbol;

        /// <summary>
        /// Initializes a new instance of the <see cref="NumberOfEntriesComparer"/> class.
        /// </summary>
        /// <param name="symbol">The symbol.</param>
        public NumberOfEntriesComparer(char symbol)
        {
            this.symbol = symbol;
        }

        /// <summary>
        /// Compares two objects and returns a value indicating whether one is less than, equal to, or greater than the other.
        /// </summary>
        /// <param name="x">The first object to compare.</param>
        /// <param name="y">The second object to compare.</param>
        /// <returns>
        /// A signed integer that indicates the relative values of <paramref name="x" /> and <paramref name="y" />, as shown in the following table.Value Meaning Less than zero
        /// <paramref name="x" /> is less than <paramref name="y" />.Zero
        /// <paramref name="x" /> equals <paramref name="y" />.Greater than zero
        /// <paramref name="x" /> is greater than <paramref name="y" />.
        /// </returns>
        public int Compare(string x, string y)
        {
            return GetNumberOfEntriesSymbol(x) - GetNumberOfEntriesSymbol(y);
        }

        private int GetNumberOfEntriesSymbol(string str)
        {
            int count = 0;
            foreach (var s in str)
            {
                if (s == this.symbol)
                {
                    count++;
                }
            }

            return count;
        }
    }
}
