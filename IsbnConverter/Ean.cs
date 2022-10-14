namespace DoenaSoft.IsbnConverter
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Text;

/// <summary />
    public class Ean : IConvert, IChecksum
    {
        /// <summary>
        /// Converts EAN to ISBN.
        /// </summary>
        public string Convert(string ean)
        {
            BaseCheck(ean);

            var eanDigits = Calculate(ean.ToCharArray());

            var isbn = Assemble(eanDigits);

            return isbn;
        }

        /// <summary>
        /// Calculates the EAN checksum.
        /// </summary>
        /// <param name="ean" />
        /// <param name="throwOnError">if the checksum is already given and this parameter is true then an <see cref="InvalidDataException"/> is thrown when the calculated checksum differs from the given one</param>
        /// <returns>the checksum</returns>
        public string GetCheckSum(string ean, bool throwOnError)
        {
            BaseCheck(ean);

            var eanDigits = new byte[Constants.EanFullLength];

            Helper.CopyDigits(ean.ToCharArray(), eanDigits, Constants.EanLowerBound, Constants.EanLowerBound, Constants.EanChecksumIndex - 1);

            var checksum = Helper.CalculateEanChecksum(eanDigits).ToString();

            if (throwOnError
                && ean.Length == Constants.EanFullLength
                && ean[Constants.EanChecksumIndex] != checksum[0])
            {
                throw new InvalidDataException($"Given checksum is {ean[Constants.EanChecksumIndex]} when it should be {checksum}!");
            }

            return checksum;
        }

        /// <summary>
        /// Checks if the EAN checksum is correct.
        /// </summary>
        public bool IsValidCheckSum(string ean)
        {
            BaseCheck(ean);

            if (ean.Length != Constants.EanFullLength)
            {
                throw new ArgumentException($"EAN doesn't have correct length ({Constants.EanFullLength})!", nameof(ean));
            }

            var eanDigits = new byte[Constants.EanFullLength];

            Helper.CopyDigits(ean.ToCharArray(), eanDigits, Constants.EanLowerBound, Constants.EanLowerBound, Constants.EanChecksumIndex - 1);

            var checksum = Helper.CalculateEanChecksum(eanDigits).ToString();

            var isValid = ean[Constants.IsbnChecksumIndex] == checksum[0];

            return isValid;
        }

        /// <summary>
        /// Checks the input EAN for basic validity.
        /// </summary>
        protected virtual void BaseCheck(string ean)
        {
            if (ean == null)
            {
                throw new ArgumentNullException(nameof(ean));
            }
            else if (ean.Length != Constants.EanFullLength)
            {
                if (ean.Length != Constants.EanFullLength - 1)
                {
                    throw new ArgumentException($"EAN doesn't have correct length ({Constants.EanFullLength})!", nameof(ean));
                }
            }
            else if (ean.Any(d => d < '0' || d > '9'))
            {
                throw new ArgumentException("EAN contains invalid character!");
            }
        }

        private static byte[] Calculate(char[] eanDigits)
        {
            var isbnDigits = new byte[Constants.IsbnFullLength];

            Helper.CopyDigits(eanDigits, isbnDigits, Constants.IsbnLowerBound, Constants.EanLowerBoundWithoutPrefix, Constants.EanUpperBound);

            isbnDigits[Constants.IsbnChecksumIndex] = Helper.CalculateIsbnChecksum(isbnDigits);

            return isbnDigits;
        }

        private static string Assemble(byte[] isbnDigits)
        {
            var isbn = new StringBuilder();

            for (var isbnIndex = Constants.IsbnLowerBound; isbnIndex <= Constants.IsbnUpperBound; isbnIndex++)
            {
                isbn.Append(isbnDigits[isbnIndex]);
            }

            var checksum = isbnDigits[Constants.IsbnChecksumIndex];

            isbn.Append((checksum == 10)
                ? "X"
                : checksum.ToString());

            return isbn.ToString();
        }
    }
}