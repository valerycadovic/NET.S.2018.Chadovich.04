namespace Day4
{
    using System.Text;

    /// <summary>
    /// Contains some extension methods for System.Double type
    /// </summary>
    public static class DoubleHelper
    {
        /// <summary>
        /// Represents binary form of System.Double as a string
        /// </summary>
        /// <param name="value">number to be represented</param>
        /// <returns>string which contains binary form of a number</returns>
        public static string Binary(this double value)
        {
            long bits = DoubleToLong(value);
            return bits.Binary();
        }

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

            for (int i = 0; i < 64; i++)
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
    }
}
