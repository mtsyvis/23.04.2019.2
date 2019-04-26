using System;
using System.Collections;
using System.Collections.Generic;

namespace PseudoEnumerable
{
    public static class Enumerable
    {
        /// <summary>
        /// Filters a sequence of values based on a predicate.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source</typeparam>
        /// <param name="source">An <see cref="IEnumerable{TSource}"/> to filter.</param>
        /// <param name="predicate">A function to test each source element for a condition</param>
        /// <returns>
        ///     An <see cref="IEnumerable{TSource}"/> that contains elements from the input
        ///     sequence that satisfy the condition.
        /// </returns>
        /// <exception cref="ArgumentNullException">Throws if <paramref name="source"/> is null.</exception>
        /// <exception cref="ArgumentNullException">Throws if <paramref name="predicate"/> is null.</exception>
        public static IEnumerable<TSource> Filter<TSource>(this IEnumerable<TSource> source,
            Func<TSource, bool> predicate)
        {
            ValidateArgument(source, predicate);

            return FilterIterator();

            IEnumerable<TSource> FilterIterator()
            {
                foreach (var item in source)
                {
                    if (predicate(item))
                    {
                        yield return item;
                    }
                }
            }
        }

        /// <summary>
        /// Transforms each element of a sequence into a new form.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <typeparam name="TResult">The type of the value returned by transformer.</typeparam>
        /// <param name="source">A sequence of values to invoke a transform function on.</param>
        /// <param name="transformer">A transform function to apply to each source element.</param>
        /// <returns>
        ///     An <see cref="IEnumerable{TResult}"/> whose elements are the result of
        ///     invoking the transform function on each element of source.
        /// </returns>
        /// <exception cref="ArgumentNullException">Throws if <paramref name="source"/> is null.</exception>
        /// <exception cref="ArgumentNullException">Throws if <paramref name="transformer"/> is null.</exception>
        public static IEnumerable<TResult> Transform<TSource, TResult>(this IEnumerable<TSource> source,
            Func<TSource, TResult> transformer)
        {
            ValidateArgument(source, transformer);

            return TransformIterator();

            IEnumerable<TResult> TransformIterator()
            {
                foreach (var item in source)
                {
                    yield return transformer(item);
                }
            }
        }

        /// <summary>
        /// Sorts the elements of a sequence in ascending order according to a key.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by key.</typeparam>
        /// <param name="source">A sequence of values to order.</param>
        /// <param name="key">A function to extract a key from an element.</param>
        /// <returns>
        ///     An <see cref="IEnumerable{TSource}"/> whose elements are sorted according to a key.
        /// </returns>
        /// <exception cref="ArgumentNullException">Throws if <paramref name="source"/> is null.</exception>
        /// <exception cref="ArgumentNullException">Throws if <paramref name="key"/> is null.</exception>
        public static IEnumerable<TSource> SortBy<TSource, TKey>(this IEnumerable<TSource> source,
            Func<TSource, TKey> key)
        {
            ValidateArgument(source, key);

            return new EnumerableSorter<TSource,TKey>(source, key, null, false).GetSortedIterator();
        }

        /// <summary>
        /// Sorts the elements of a sequence in ascending order according by using a specified comparer for a key .
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by key.</typeparam>
        /// <param name="source">A sequence of values to order.</param>
        /// <param name="key">A function to extract a key from an element.</param>
        /// <param name="comparer">An <see cref="IComparer{T}"/> to compare keys.</param>
        /// <returns>
        ///     An <see cref="IEnumerable{TSource}"/> whose elements are sorted according to a key.
        /// </returns>
        /// <exception cref="ArgumentNullException">Throws if <paramref name="source"/> is null.</exception>
        /// <exception cref="ArgumentNullException">Throws if <paramref name="key"/> is null.</exception>
        public static IEnumerable<TSource> SortBy<TSource, TKey>(this IEnumerable<TSource> source,
            Func<TSource, TKey> key, IComparer<TKey> comparer)
        {
            ValidateArgument(source, key);

            return new EnumerableSorter<TSource, TKey>(source, key, comparer, false).GetSortedIterator();
        }

        /// <summary>
        /// Sorts the elements of a sequence in descending order according to a key.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by key.</typeparam>
        /// <param name="source">A sequence of values to order.</param>
        /// <param name="key">A function to extract a key from an element.</param>
        /// <returns>
        ///     An <see cref="IEnumerable{TSource}"/> whose elements are sorted according to a key.
        /// </returns>
        /// <exception cref="ArgumentNullException">Throws if <paramref name="source"/> is null.</exception>
        /// <exception cref="ArgumentNullException">Throws if <paramref name="key"/> is null.</exception>
        public static IEnumerable<TSource> SortByDescending<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> key)
        {
            ValidateArgument(source, key);

            return new EnumerableSorter<TSource, TKey>(source, key, null, true).GetSortedIterator();
        }

        /// <summary>
        /// Sorts the elements of a sequence in descending order according by using a specified comparer for a key .
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by key.</typeparam>
        /// <param name="source">A sequence of values to order.</param>
        /// <param name="key">A function to extract a key from an element.</param>
        /// <param name="comparer">An <see cref="IComparer{T}"/> to compare keys.</param>
        /// <returns>
        ///     An <see cref="IEnumerable{TSource}"/> whose elements are sorted according to a key.
        /// </returns>
        /// <exception cref="ArgumentNullException">Throws if <paramref name="source"/> is null.</exception>
        /// <exception cref="ArgumentNullException">Throws if <paramref name="key"/> is null.</exception>
        public static IEnumerable<TSource> SortByDescending<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> key, IComparer<TKey> comparer)
        {
            ValidateArgument(source, key);

            return new EnumerableSorter<TSource, TKey>(source, key, comparer, true).GetSortedIterator();
        }

        /// <summary>
        /// Casts the elements of an IEnumerable to the specified type.
        /// </summary>
        /// <typeparam name="TResult">The type to cast the elements of source to.</typeparam>
        /// <param name="source">The <see cref="IEnumerable"/> that contains the elements to be cast to type TResult.</param>
        /// <returns>
        ///     An <see cref="IEnumerable{T}"/> that contains each element of the source sequence cast to the specified type.
        /// </returns>
        /// <exception cref="ArgumentNullException">Throws if <paramref name="source"/> is null.</exception>
        /// <exception cref="InvalidCastException">An element in the sequence cannot be cast to type TResult.</exception>
        public static IEnumerable<TResult> CastTo<TResult>(this IEnumerable source)
        {
            if (source is IEnumerable<TResult> resultSource)
            {
                return resultSource;
            }

            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return CastToIterator();

            IEnumerable<TResult> CastToIterator()
            {
                foreach (var item in source)
                {
                    yield return (TResult)item;
                }
            }
        }

        /// <summary>
        /// Determines whether all elements of a sequence satisfy a condition.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <param name="source">A sequence of values.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>
        ///     true if every element of the source sequence passes the test in the specified predicate,
        ///     or if the sequence is empty; otherwise, false
        /// </returns>
        /// <exception cref="ArgumentNullException">Throws if <paramref name="source"/> is null.</exception>
        /// <exception cref="ArgumentNullException">Throws if <paramref name="predicate"/> is null.</exception>
        public static bool ForAll<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            ValidateArgument(source, predicate);
            foreach (var item in source)
            {
                if (!predicate.Invoke(item))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Generates the sequence.
        /// </summary>
        /// <param name="start">The start.</param>
        /// <param name="count">The count.</param>
        /// <returns>Generated sequence</returns>
        /// <exception cref="ArgumentOutOfRangeException">count or start less then 0</exception>
        public static IEnumerable<int> GenerateSequence(int start, int count)
        {
            if (start < 0 || count < 0)
            {
                throw new ArgumentOutOfRangeException($"{nameof(count)} or {nameof(start)} less then 0");
            }

            return GenerateSequenceIterator();

            IEnumerable<int> GenerateSequenceIterator()
            {
                for (int i = 0; i < count; i++)
                {
                    yield return start++;
                }
            }
        }

        private class EnumerableSorter<TSource, TKey>
        {
            private IEnumerable<TSource> source;
            private Func<TSource, TKey> keySelector;
            private IComparer<TKey> comparer;
            private bool descending;
            private TKey[] keys;

            public EnumerableSorter(
                IEnumerable<TSource> source,
                Func<TSource, TKey> keySelector,
                IComparer<TKey> comparer,
                bool descending)
            {
                this.source = source;
                this.keySelector = keySelector;
                this.comparer = comparer ?? Comparer<TKey>.Default;
                this.descending = descending;
            }

            public IEnumerable<TSource> GetSortedIterator()
            {
                TSource[] arraySource = this.source.ToArray();
                this.ComputeKeys(arraySource);
                int[] map = this.GetSortedMap(arraySource);
                for (int i = 0; i < arraySource.Length; i++)
                {
                    yield return arraySource[map[i]];
                }
            }

            private void ComputeKeys(TSource[] source)
            {
                this.keys = new TKey[source.Length];
                for (int i = 0; i < source.Length; i++)
                {
                    this.keys[i] = this.keySelector.Invoke(source[i]);
                }
            }

            private int CompareKeys(int index1, int index2)
            {
                int num = comparer.Compare(keys[index1], keys[index2]);

                if (!this.descending)
                {
                    return num;
                }

                return -num;
            }

            private void SwapKeys(int index1, int index2)
            {
                TKey temp = this.keys[index1];
                this.keys[index1] = this.keys[index2];
                this.keys[index2] = temp;
            }

            private int[] GetSortedMap(TSource[] source)
            {
                var map = new int[source.Length];
                for (int i = 0; i < map.Length; i++)
                {
                    map[i] = i;
                }

                int temp;
                for (int i = 0; i < map.Length - 1; i++)
                {
                    for (int j = i + 1; j < map.Length; j++)
                    {
                        if (this.CompareKeys(i, j) > 0)
                        {
                            SwapKeys(i, j);
                            temp = map[i];
                            map[i] = map[j];
                            map[j] = temp;
                        }
                    }
                }

                return map;
            }
        }

        private static void ValidateArgument(IEnumerable source, Delegate @delegate)
        {
            if (source is null)
            {
                throw new ArgumentNullException($"{nameof(source)} is null");
            }

            if (@delegate is null)
            {
                throw new ArgumentNullException($"{nameof(@delegate)} is null");
            }
        }

        private static TKey[] GetKeys<TSource, TKey>(
            this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector)
        {
            var keyList = new List<TKey>(source.Count());
            foreach (var item in source)
            {
                keyList.Add(keySelector(item));
            }

            return keyList.ToArray();
        }

        private static TElement[] ToArray<TElement>(this IEnumerable<TElement> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException($"{nameof(source)}");
            }

            TElement[] array = null;

            if (source is ICollection<TElement> elements)
            {
                array = new TElement[elements.Count];
                elements.CopyTo(array, 0);
                return array;
            }
            else
            {
                array = new TElement[source.Count()];
                int i = 0;
                foreach (var element in source)
                {
                    array[i] = element;
                    i++;
                }

                return array;
            }
        }

        private static int Count<TSource>(this IEnumerable<TSource> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException($"{nameof(source)}");
            }

            if (source is ICollection collection)
            {
                return collection.Count;
            }

            int count = 0;
            using (IEnumerator<TSource> iterator = source.GetEnumerator())
            {
                while (iterator.MoveNext())
                {
                    count++;
                }
            }

            return count;
        }
    }
}