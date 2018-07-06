namespace Day4
{
    using System.Runtime.InteropServices;
    using System.Text;

    /// <summary>
    /// Contains some extension methods for System.Double type
    /// </summary>
    public static class DoubleHelper
    {
        #region Constants
        /// <summary>
        /// Count of bits in byte
        /// </summary>
        private const int BYTESIZE = 8;

        /// <summary>
        /// Count of bytes in double and long
        /// </summary>
        private const int LONGDOUBLEBITS = 8 * BYTESIZE;
        #endregion

        #region Public API
        /// <summary>
        /// Represents binary form of System.Double as a string using unsafe code
        /// </summary>
        /// <param name="value">number to be represented</param>
        /// <returns>string which contains binary form of a number</returns>
        public static string BinaryUnsafe(this double value)
        {
            long bits = DoubleToLong(value);
            return bits.Binary();
        }

        /// <summary>
        /// Represents binary form of System.Double as a string
        /// </summary>
        /// <param name="value">number to be represented</param>
        /// <returns>string which contains binary form of a number</returns>
        public static string Binary(this double value)
        {
            Union union = new Union(value);
            long bits = union.ToLong();

            return bits.Binary();
        }
        #endregion

        #region Private Section
        /// <summary>
        /// Reinterpret cast of double to long
        /// </summary>
        /// <param name="value">number to be casted</param>
        /// <returns>binary form of System.Double value as long integer</returns>
        private static unsafe long DoubleToLong(double value)
        {
            void* mem = (void*)&value;
            return *(long*)mem;
        }

        /// <summary>
        /// Represents binary form of long integer number as a string
        /// </summary>
        /// <param name="value">number to be represented</param>
        /// <returns>string which contains binary form of a number</returns>
        private static string Binary(this long value)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < LONGDOUBLEBITS; i++)
            {
                if ((value & 1) == 1)
                {
                    sb.Insert(0, "1");
                }
                else
                {
                    sb.Insert(0, "0");
                }

                value >>= 1;
            }

            return sb.ToString();
        }
        
        /// <summary>
        /// long representation of double value
        /// </summary>
        [StructLayout(LayoutKind.Explicit)]
        private struct Union
        {
            /// <summary>
            /// double representation
            /// </summary>
            [FieldOffset(0)]
            private readonly double @double;

            /// <summary>
            /// long representation
            /// </summary>
            [FieldOffset(0)]
            private readonly long @long;

            /// <summary>
            /// Initializes a new instance of the <see cref="Union"/> struct
            /// </summary>
            /// <param name="value">double value to be converted</param>
            public Union(double value) : this()
            {
                @double = value;
            }

            /// <summary>
            /// Casts to long
            /// </summary>
            /// <param name="obj">value to be casted</param>
            public static explicit operator long(Union obj) => obj.@long;

            /// <summary>
            /// Casts to long
            /// </summary>
            /// <returns>long value representation</returns>
            public long ToLong() => @long;
        }
        #endregion
    }
}
