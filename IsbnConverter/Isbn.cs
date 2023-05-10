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

            var bytes = Calculate(isbn.ToCharArray());

            var result = Assemble(bytes);

            return result;
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

            var checksum = Helper.CalculateIsbnChecksum(isbnDigits).ToString();

            if (throwOnError
                && isbn.Length == Constants.IsbnFullLength
                && isbn[Constants.IsbnChecksumIndex].ToString().ToUpper() != checksum.ToUpper())
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

            var checksum = Helper.CalculateIsbnChecksum(isbnDigits).ToString();

            var isValid = isbn[Constants.IsbnChecksumIndex].ToString().ToUpper() == checksum.ToUpper();

            return isValid;
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

        private static byte[] Calculate(char[] isbnDigits)
        {
            var eanDigits = new byte[Constants.EanFullLength];

            Helper.CopyDigits(isbnDigits, eanDigits, Constants.EanLowerBoundWithoutPrefix, Constants.IsbnLowerBound, Constants.IsbnUpperBound);

            //"Bookland" prefix
            eanDigits[0] = 9;
            eanDigits[1] = 7;
            eanDigits[2] = 8;

            eanDigits[Constants.EanChecksumIndex] = Helper.CalculateEanChecksum(eanDigits);

            return eanDigits;
        }

        private static string Assemble(byte[] eanDigits)
        {
            var ean = new StringBuilder();

            foreach (var eanDigit in eanDigits)
            {
                ean.Append(eanDigit);
            }

            return ean.ToString();
        }
    }
}