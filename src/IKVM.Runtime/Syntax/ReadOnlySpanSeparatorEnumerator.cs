﻿using System;

namespace IKVM.Runtime.Syntax
{

    /// <summary>
    /// Allows navigation over a Java class name.
    /// </summary>
    ref struct ReadOnlySpanSeparatorEnumerator
    {

        ReadOnlySpan<char> name;
        ReadOnlySpan<char> curr;
        readonly char separator;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="separator"></param>
        public ReadOnlySpanSeparatorEnumerator(ReadOnlySpan<char> name, char separator)
        {
            this.name = name;
            curr = default;
            this.separator = separator;
        }

        /// <summary>
        /// Gets an enumerator.
        /// </summary>
        /// <returns></returns>
        public ReadOnlySpanSeparatorEnumerator GetEnumerator() => this;

        /// <summary>
        /// Gets the current entry.
        /// </summary>
        public ReadOnlySpan<char> Current => curr;

        /// <summary>
        /// Returns whether any more entries exist.
        /// </summary>
        /// <returns></returns>
        public bool MoveNext()
        {
            var span = name;
            if (span.Length == 0)
                return false;

            var index = span.IndexOf(separator);
            if (index == -1)
            {
                name = ReadOnlySpan<char>.Empty;
                curr = span;
                return true;
            }

            curr = span.Slice(0, index);
            name = span.Slice(index + 1);
            return true;
        }

    }
}
