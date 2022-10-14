namespace DoenaSoft.IsbnConverter
{
    using System;
    using System.Linq;

    /// <summary />
    public static class Helper
    {
        internal static void CopyDigits(char[] source
            , byte[] target
            , byte leftLowerBound
            , byte rightLowerBound
            , byte rightUpperBound)
        {
            for (byte leftIndex = leftLowerBound, rightIndex = rightLowerBound; rightIndex <= rightUpperBound; leftIndex++, rightIndex++)
            {
                target[leftIndex] = byte.Parse(source[rightIndex].ToString());
            }
        }

        /// <summary>
        /// Calculates the ISBN checksum
        /// </summary>
        /// <param name="isbnDigits">digits of the ISBN with or without the checksum</param>
        public static byte CalculateIsbnChecksum(byte[] isbnDigits)
        {
            CheckIsbnDigits(isbnDigits);

            var checksum = 0;

            for (var isbnIndex = Constants.IsbnLowerBound; isbnIndex <= Constants.IsbnUpperBound; isbnIndex++)
            {
                checksum += isbnDigits[isbnIndex] * (isbnIndex + 1);
            }

            checksum %= 11;

            return (byte)checksum;
        }

        /// <summary>
        /// Calculates the EAN checksum
        /// </summary>
        /// <param name="eanDigits">digits of the EAN with or without the checksum</param>
        public static byte CalculateEanChecksum(byte[] eanDigits)
        {
            CheckEanDigits(eanDigits);

            var evens = Add(eanDigits, 0);

            var odds = Add(eanDigits, 1) * 3;

            var checksum = (evens + odds) % 10;

            checksum = (10 - checksum) % 10; //in case previous modulo is 10

            return (byte)checksum;
        }

        private static void CheckIsbnDigits(byte[] isbnDigits)
        {
            if (isbnDigits == null)
            {
                throw new ArgumentNullException(nameof(isbnDigits));
            }
            else if (isbnDigits.Length != Constants.IsbnFullLength)
            {
                if (isbnDigits.Length != Constants.IsbnFullLength - 1)
                {
                    throw new ArgumentException($"ISBN doesn't have correct length ({Constants.IsbnFullLength})!", nameof(isbnDigits));
                }
            }
            else if (isbnDigits.Any(d => d < 0 || d > 9))
            {
                for (var isbnIndex = Constants.IsbnLowerBound; isbnIndex <= Constants.IsbnUpperBound; isbnIndex++)
                {
                    if (isbnDigits[isbnIndex] < 0 || isbnDigits[isbnIndex] > 9)
                    {
                        throw new ArgumentException("ISBN contains invalid character!");
                    }
                }

                if (isbnDigits.Length == Constants.IsbnFullLength)
                {
                    if (isbnDigits[Constants.IsbnChecksumIndex].ToString().ToUpper() != "X")
                    {
                        throw new ArgumentException("ISBN contains invalid character!");
                    }
                }
            }
        }

        private static void CheckEanDigits(byte[] eanDigits)
        {
            if (eanDigits == null)
            {
                throw new ArgumentNullException(nameof(eanDigits));
            }
            else if (eanDigits.Length != Constants.EanFullLength)
            {
                if (eanDigits.Length != Constants.EanFullLength - 1)
                {
                    throw new ArgumentException($"EAN doesn't have correct length ({Constants.IsbnFullLength})!", nameof(eanDigits));
                }
            }
            else if (eanDigits.Any(d => d < 0 || d > 9))
            {
                throw new ArgumentException("EAN contains invalid character!");
            }
        }

        private static int Add(byte[] ean, byte lowerBound)
        {
            var sum = 0;

            for (var eanIndex = lowerBound; eanIndex <= Constants.EanUpperBound; eanIndex += 2)
            {
                sum += ean[eanIndex];
            }

            return sum;
        }
    }
}