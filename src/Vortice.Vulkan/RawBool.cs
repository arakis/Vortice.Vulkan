﻿// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Vortice.Vulkan
{
    /// <summary>
    /// A boolean value stored on 4 bytes (instead of 1 in .NET).
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Size = 4)]
    public readonly struct RawBool : IEquatable<RawBool>
    {
        private readonly int _value;

        /// <summary>
        /// Initializes a new instance of the <see cref="RawBool" /> class.
        /// </summary>
        /// <param name="boolValue">if set to <c>true</c> [bool value].</param>
        public RawBool(bool boolValue)
        {
            _value = boolValue ? 1 : 0;
        }

        /// <summary>
        /// Indicates whether this instance and a specified object are equal.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns>true if <paramref name="other" /> and this instance are the same type and represent the same value; otherwise, false.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(RawBool other) => _value == other._value;

        /// <inheritdoc/>
        public override bool Equals(object obj) => obj is RawBool rawBool && Equals(rawBool);

        /// <inheritdoc/>
        public override int GetHashCode() => _value;

        /// <summary>
        /// Implements the ==.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(RawBool left, RawBool right) => left.Equals(right);

        /// <summary>
        /// Implements the !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(RawBool left, RawBool right) => !left.Equals(right);

        /// <summary>
        /// Performs an explicit conversion from <see cref="RawBool"/> to <see cref="bool"/>.
        /// </summary>
        /// <param name="value">The <see cref="RawBool"/> value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator bool(RawBool value) => value._value != 0;

        /// <summary>
        /// Performs an explicit conversion from <see cref="bool"/> to <see cref="RawBool"/>.
        /// </summary>
        /// <param name="boolValue">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator RawBool(bool boolValue)
        {
            return new RawBool(boolValue);
        }

        /// <inheritdoc/>
        public override string ToString() => _value != 0 ? "True" : "False";
    }
}
