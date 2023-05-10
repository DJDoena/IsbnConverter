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

            var checksum = CalculateChecksum(eanDigits).ToString();

            if (throwOnError
                && ean.Length == Constants.EanFullLength
                && !IsValid(ean, checksum))
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

            var checksum = CalculateChecksum(eanDigits).ToString();

            var isValid = IsValid(ean, checksum);

            return isValid;
        }

        /// <summary>
        /// Calculates the EAN checksum
        /// </summary>
        /// <param name="eanDigits">digits of the EAN with or without the checksum</param>
        internal static byte CalculateChecksum(byte[] eanDigits)
        {
            CheckDigits(eanDigits);

            var evens = AddDigits(eanDigits, 0);

            var odds = AddDigits(eanDigits, 1) * 3;

            var checksum = (evens + odds) % 10;

            checksum = (10 - checksum) % 10; //in case previous modulo is 10

            return (byte)checksum;
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

        private static void CheckDigits(byte[] eanDigits)
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

        private static int AddDigits(byte[] ean, byte lowerBound)
        {
            var sum = 0;

            for (var eanIndex = lowerBound; eanIndex <= Constants.EanUpperBound; eanIndex += 2)
            {
                sum += ean[eanIndex];
            }

            return sum;
        }

        private static bool IsValid(string ean, string checksum)
            => ean[Constants.EanChecksumIndex].ToString() == checksum;
    }
}