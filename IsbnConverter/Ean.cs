namespace DoenaSoft.IsbnConverter
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Text;

    /// <summary />
    public class Ean : IChecksum
    {
        internal const byte LowerBound = 0;
        
        internal const byte LowerBoundWithoutPrefix = 3;

        internal const byte UpperBound = 11;
        
        internal const byte ChecksumIndex = UpperBound + 1;
        
        internal const byte FullLength = ChecksumIndex + 1;

        /// <summary>
        /// Calculates the EAN checksum.
        /// </summary>
        /// <param name="ean" />
        /// <param name="throwOnError">if the checksum is already given and this parameter is true then an <see cref="InvalidDataException"/> is thrown when the calculated checksum differs from the given one</param>
        /// <returns>the checksum</returns>
        public string GetCheckSum(string ean, bool throwOnError)
        {
            BaseCheck(ean);

            var eanDigits = new byte[FullLength];

            Helper.CopyDigits(ean.ToCharArray(), eanDigits, LowerBound, LowerBound, ChecksumIndex - 1);

            var checksum = CalculateChecksum(eanDigits).ToString();

            if (throwOnError
                && ean.Length == FullLength
                && !IsValid(ean, checksum))
            {
                throw new InvalidDataException($"Given checksum is {ean[ChecksumIndex]} when it should be {checksum}!");
            }

            return checksum;
        }

        /// <summary>
        /// Checks if the EAN checksum is correct.
        /// </summary>
        public bool IsValidCheckSum(string ean)
        {
            BaseCheck(ean);

            if (ean.Length != FullLength)
            {
                throw new ArgumentException($"EAN doesn't have correct length ({FullLength})!", nameof(ean));
            }

            var eanDigits = new byte[FullLength];

            Helper.CopyDigits(ean.ToCharArray(), eanDigits, LowerBound, LowerBound, ChecksumIndex - 1);

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

            var bytes = Encoding.ASCII.GetBytes(ean);

            CheckDigits(bytes);
        }

        private static void CheckDigits(byte[] eanDigits)
        {
            if (eanDigits == null)
            {
                throw new ArgumentNullException(nameof(eanDigits));
            }
            else if (eanDigits.Length != FullLength)
            {
                if (eanDigits.Length != FullLength - 1)
                {
                    throw new ArgumentException($"EAN doesn't have correct length ({Isbn.FullLength})!", nameof(eanDigits));
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

            for (var eanIndex = lowerBound; eanIndex <= UpperBound; eanIndex += 2)
            {
                sum += ean[eanIndex];
            }

            return sum;
        }

        private static bool IsValid(string ean, string checksum)
            => ean[ChecksumIndex].ToString() == checksum;
    }
}