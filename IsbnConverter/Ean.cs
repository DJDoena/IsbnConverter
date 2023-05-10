namespace DoenaSoft.IsbnConverter
{
    using System;
    using System.IO;
    using System.Linq;

    /// <summary />
    public class Ean : IChecksum
    {
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

            var isValid = ean[Constants.EanChecksumIndex].ToString() == checksum;

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
    }
}