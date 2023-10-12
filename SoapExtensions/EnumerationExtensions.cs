/// <summary>
/// Extensions methods for enumerating over ranges and collections
/// </summary>

using System.Runtime.InteropServices;

namespace SoapExtensions
{
    public static class EnumerationExtensions
    {
        #region Ranges

        /// <summary>
        /// Returns a custom int enumerator for the given range
        /// </summary>
        /// <param name="range">The range to enumerate</param>
        /// <returns>A custom int enumerator</returns>
        public static CustomIntEnumerator GetEnumerator(this Range range)
        {
            return new CustomIntEnumerator(range);
        }

        /// <summary>
        /// Returns a custom int enumerator with step size for the given tuple
        /// </summary>
        /// <param name="tuple">The tuple containing start, end and step size</param>
        /// <returns>A custom int enumerator with step size</returns>
        public static CustomIntWithStepSizeEnumerator GetEnumerator(this (int start, int end, int stepSize) tuple)
        {
            return new CustomIntWithStepSizeEnumerator(tuple.start..tuple.end, tuple.stepSize);
        }

        /// <summary>
        /// Returns a custom int enumerator with step size for the given tuple
        /// </summary>
        /// <param name="tuple">The tuple containing a range and step size</param>
        /// <returns>A custom int enumerator with step size</returns>
        public static CustomIntWithStepSizeEnumerator GetEnumerator(this (Range range, int stepSize) tuple)
        {
            return new CustomIntWithStepSizeEnumerator(tuple.range, tuple.stepSize);
        }

        /// <summary>
        /// Executes an action for each value in the range
        /// </summary>
        /// <param name="range">The range to enumerate</param>
        /// <param name="a">The action to perform per value</param>
        public static void ForEach(this Range range, Action<int> a)
        {
            foreach (var i in range)
            {
                a(i);
            }
        }

        #endregion Ranges

        #region IEnumerable

        /// <summary>
        /// Executes an action for each element in the enumerable
        /// </summary>
        /// <typeparam name="T">The type of the enumerable</typeparam>
        /// <param name="enumerable">The enumerable to enumerate</param>
        /// <param name="a">The action to perform per element</param>
        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> a)
        {
            foreach (var i in enumerable)
            {
                a(i);
            }
        }

        /// <summary>
        /// Executes an action with index for each element in the enumerable
        /// </summary>
        /// <typeparam name="T">The type of the enumerable</typeparam>
        /// <param name="enumerable">The enumerable to enumerate</param>
        /// <param name="a">The action to perform per element</param>
        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T, int> a)
        {
            var counter = 0;
            foreach (var i in enumerable)
            {
                a(i, counter++);
            }
        }

        #endregion IEnumerable

        #region ForEachRef

        public delegate void RefAction<T>(ref T value) where T : struct;

        /// <summary>
        /// Executes an action on each element passed by reference
        /// </summary>
        /// <typeparam name="T">The type of the list</typeparam>
        /// <param name="list">The list to enumerate</param>
        /// <param name="action">The action to perform per element</param>
        public static void ForEachRef<T>(this List<T> list, RefAction<T> action) where T : struct
        {
            if (list is null) throw new ArgumentNullException(nameof(list));
            if (action is null) throw new ArgumentNullException(nameof(action));
            var span = System.Runtime.InteropServices.CollectionsMarshal.AsSpan(list);
            foreach (ref T item in span)
            {
                action(ref item);
            }
        }

        #endregion ForEachRef

        #region FirstRef

        public static ref T FirstRef<T>(this List<T> list, Func<T, bool> predicate)
        {
            if (list is null) throw new ArgumentNullException(nameof(list));
            if (predicate is null) throw new ArgumentNullException(nameof(predicate));
            var span = CollectionsMarshal.AsSpan(list);

            for (var i = 0; i < span.Length; i++)
            {
                if (predicate(span[i]))
                {
                    return ref span[i];
                }
            }

            throw new KeyNotFoundException("No element found");
        }

        #endregion
    }

    public struct CustomIntWithStepSizeEnumerator
    {
        private int _current;
        private readonly bool _descending;
        private readonly int _end;
        private readonly int _stepsize;

        public CustomIntWithStepSizeEnumerator(Range range, int stepSize)
        {
            if (range.End.IsFromEnd || range.Start.IsFromEnd)
                throw new NotSupportedException();
            if (range.Start.Value > range.End.Value)
            {
                _descending = true;
                _current = range.Start.Value + stepSize;
            }
            else
            {
                _descending = false;
                _current = range.Start.Value - stepSize;
            }

            _end = range.End.Value;
            _stepsize = stepSize;
        }

        public readonly int Current => _current;

        public bool MoveNext()
        {
            if (_descending)
            {
                _current -= _stepsize;
                return _current >= _end;
            }
            else
            {
                _current += _stepsize;
                return _current <= _end;
            }
        }
    }

    public struct CustomIntEnumerator
    {
        private int _current;
        private readonly bool _descending;
        private readonly int _end;

        public CustomIntEnumerator(Range range)
        {
            if (range.End.IsFromEnd || range.Start.IsFromEnd)
                throw new NotSupportedException();
            if (range.Start.Value > range.End.Value)
            {
                _descending = true;
                _current = range.Start.Value + 1;
            }
            else
            {
                _descending = false;
                _current = range.Start.Value - 1;
            }

            _end = range.End.Value;
        }

        public int Current => _current;

        public bool MoveNext()
        {
            if (_descending)
            {
                _current--;
                return _current >= _end;
            }
            else
            {
                _current++;
                return _current <= _end;
            }
        }
    }
}