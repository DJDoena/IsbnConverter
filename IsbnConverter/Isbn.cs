namespace DoenaSoft.IsbnConverter
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Text;

    /// <summary />
    public sealed class Isbn : IConvert, IChecksum
    {
        internal const byte LowerBound = 0;

        internal const byte UpperBound = 8;

        internal const byte ChecksumIndex = UpperBound + 1;

        internal const byte FullLength = ChecksumIndex + 1;

        internal const string CheckDigitX = "X";

        /// <summary>
        /// Converts ISBN to EAN.
        /// </summary>
        public string Convert(string isbn)
        {
            BaseCheck(isbn);

            var eanDigits = ConvertToEanDigits(isbn.ToCharArray());

            var ean = AssembleEan(eanDigits);

            return ean;
        }

        /// <summary>
        /// Calculates the ISBN checksum.
        /// </summary>
        /// <param name="isbn" />
        /// <param name="throwOnError">if the checksum is already given and this parameter is true then an <see cref="InvalidDataException"/> is thrown when the calculated checksum differs from the given one</param>
        /// <returns>the checksum</returns>
        public string GetCheckSum(string isbn, bool throwOnError)
        {
            BaseCheck(isbn);

            var isbnDigits = new byte[FullLength];

            Helper.CopyDigits(isbn.ToCharArray(), isbnDigits, LowerBound, LowerBound, ChecksumIndex - 1);

            var checksum = CalculateChecksum(isbnDigits).ToString();

            if (throwOnError
                && isbn.Length == FullLength
                && !IsValid(isbn, checksum))
            {
                throw new InvalidDataException($"Given checksum is {isbn[ChecksumIndex]} when it should be {checksum}!");
            }

            return checksum;
        }

        /// <summary>
        /// Checks if the ISBN checksum is correct.
        /// </summary>
        public bool IsValidCheckSum(string isbn)
        {
            BaseCheck(isbn);

            if (isbn.Length != FullLength)
            {
                throw new ArgumentException($"ISBN doesn't have correct length ({FullLength})!", nameof(isbn));
            }

            var isbnDigits = new byte[FullLength];

            Helper.CopyDigits(isbn.ToCharArray(), isbnDigits, LowerBound, LowerBound, ChecksumIndex - 1);

            var checksum = CalculateChecksum(isbnDigits).ToString();

            var isValid = IsValid(isbn, checksum);

            return isValid;
        }

        /// <summary>
        /// Calculates the ISBN checksum
        /// </summary>
        /// <param name="isbnDigits">digits of the ISBN with or without the checksum</param>
        internal static byte CalculateChecksum(byte[] isbnDigits)
        {
            CheckDigits(isbnDigits);

            var checksum = 0;

            for (var isbnIndex = LowerBound; isbnIndex <= UpperBound; isbnIndex++)
            {
                checksum += isbnDigits[isbnIndex] * (isbnIndex + 1);
            }

            checksum %= 11;

            return (byte)checksum;
        }

        private static void CheckDigits(byte[] isbnDigits)
        {
            if (isbnDigits == null)
            {
                throw new ArgumentNullException(nameof(isbnDigits));
            }
            else if (isbnDigits.Length != FullLength)
            {
                if (isbnDigits.Length != FullLength - 1)
                {
                    throw new ArgumentException($"ISBN doesn't have correct length ({FullLength})!", nameof(isbnDigits));
                }
            }
            else if (isbnDigits.Any(d => d < 0 || d > 9))
            {
                for (var isbnIndex = LowerBound; isbnIndex <= UpperBound; isbnIndex++)
                {
                    if (isbnDigits[isbnIndex] < 0 || isbnDigits[isbnIndex] > 9)
                    {
                        throw new ArgumentException("ISBN contains invalid character!");
                    }
                }

                if (isbnDigits.Length == FullLength)
                {
                    if (isbnDigits[ChecksumIndex].ToString().ToUpper() != CheckDigitX)
                    {
                        throw new ArgumentException("ISBN contains invalid character!");
                    }
                }
            }
        }

        private static void BaseCheck(string isbn)
        {
            if (isbn == null)
            {
                throw new ArgumentNullException(nameof(isbn));
            }

            var bytes = Encoding.ASCII.GetBytes(isbn);

            CheckDigits(bytes);
        }

        private static byte[] ConvertToEanDigits(char[] isbnDigits)
        {
            var eanDigits = new byte[Ean.FullLength];

            Helper.CopyDigits(isbnDigits, eanDigits, Ean.LowerBoundWithoutPrefix, LowerBound, UpperBound);

            //"Bookland" prefix
            eanDigits[0] = 9;
            eanDigits[1] = 7;
            eanDigits[2] = 8;

            eanDigits[Ean.ChecksumIndex] = Ean.CalculateChecksum(eanDigits);

            return eanDigits;
        }

        private static string AssembleEan(byte[] eanDigits)
        {
            var ean = new StringBuilder();

            foreach (var eanDigit in eanDigits)
            {
                ean.Append(eanDigit);
            }

            return ean.ToString();
        }

        private static bool IsValid(string isbn, string checksum)
            => isbn[ChecksumIndex].ToString().ToUpper() == checksum.ToUpper();
    }
}