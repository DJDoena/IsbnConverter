namespace DoenaSoft.IsbnConverter
{
    using System;
    using System.Text;

    /// <summary>
    /// Represents an EAN from "book land".
    /// </summary>
    public sealed class BooklandEan : Ean, IConvert
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
        /// Checks the input EAN for basic validity.
        /// </summary>
        protected override void BaseCheck(string ean)
        {
            base.BaseCheck(ean);

            if (ean[0] != '9')
            {
                throw new ArgumentException("EAN doesn't start with 9!", nameof(ean));
            }
            else if (ean[1] != '7')
            {
                throw new ArgumentException("EAN doesn't start with 97!", nameof(ean));
            }
            else if (ean[2] != '8')
            {
                throw new ArgumentException("EAN doesn't start with 978!", nameof(ean));
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