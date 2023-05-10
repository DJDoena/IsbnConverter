namespace DoenaSoft.IsbnConverter
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Text;

    /// <summary />
    public sealed class Isbn : IConvert, IChecksum
    {
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

            var isbnDigits = new byte[Constants.IsbnFullLength];

            Helper.CopyDigits(isbn.ToCharArray(), isbnDigits, Constants.IsbnLowerBound, Constants.IsbnLowerBound, Constants.IsbnChecksumIndex - 1);

            var checksum = CalculateChecksum(isbnDigits).ToString();

            if (throwOnError
                && isbn.Length == Constants.IsbnFullLength
                && !IsValid(isbn, checksum))
            {
                throw new InvalidDataException($"Given checksum is {isbn[Constants.IsbnChecksumIndex]} when it should be {checksum}!");
            }

            return checksum;
        }

        /// <summary>
        /// Checks if the ISBN checksum is correct.
        /// </summary>
        public bool IsValidCheckSum(string isbn)
        {
            BaseCheck(isbn);

            if (isbn.Length != Constants.IsbnFullLength)
            {
                throw new ArgumentException($"ISBN doesn't have correct length ({Constants.IsbnFullLength})!", nameof(isbn));
            }

            var isbnDigits = new byte[Constants.IsbnFullLength];

            Helper.CopyDigits(isbn.ToCharArray(), isbnDigits, Constants.IsbnLowerBound, Constants.IsbnLowerBound, Constants.IsbnChecksumIndex - 1);

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

            for (var isbnIndex = Constants.IsbnLowerBound; isbnIndex <= Constants.IsbnUpperBound; isbnIndex++)
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

        private static void BaseCheck(string isbn)
        {
            if (isbn == null)
            {
                throw new ArgumentNullException(nameof(isbn));
            }
            else if (isbn.Length != Constants.IsbnFullLength)
            {
                if (isbn.Length != Constants.IsbnFullLength - 1)
                {
                    throw new ArgumentException($"ISBN doesn't have correct length ({Constants.IsbnFullLength})!", nameof(isbn));
                }
            }
            else if (isbn.Any(d => d < '0' || d > '9'))
            {
                for (var isbnIndex = Constants.IsbnLowerBound; isbnIndex <= Constants.IsbnUpperBound; isbnIndex++)
                {
                    if (isbn[isbnIndex] < '0' || isbn[isbnIndex] > '9')
                    {
                        throw new ArgumentException("ISBN contains invalid character!");
                    }
                }

                if (isbn.Length == Constants.IsbnFullLength)
                {
                    if (isbn[Constants.IsbnChecksumIndex].ToString().ToUpper() != "X")
                    {
                        throw new ArgumentException("ISBN contains invalid character!");
                    }
                }
            }
        }

        private static byte[] ConvertToEanDigits(char[] isbnDigits)
        {
            var eanDigits = new byte[Constants.EanFullLength];

            Helper.CopyDigits(isbnDigits, eanDigits, Constants.EanLowerBoundWithoutPrefix, Constants.IsbnLowerBound, Constants.IsbnUpperBound);

            //"Bookland" prefix
            eanDigits[0] = 9;
            eanDigits[1] = 7;
            eanDigits[2] = 8;

            eanDigits[Constants.EanChecksumIndex] = Ean.CalculateChecksum(eanDigits);

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
            => isbn[Constants.IsbnChecksumIndex].ToString().ToUpper() == checksum.ToUpper();
    }
}