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

            var isbnDigits = CalculateIsbnDigits(ean.ToCharArray());

            var isbn = AssembleIsbn(isbnDigits);

            return isbn;
        }

        /// <summary>
        /// Checks the input EAN for basic validity.
        /// </summary>
        protected override void BaseCheck(string ean)
        {
            base.BaseCheck(ean);

            if (ean[0] != '9' || ean[1] != '7' || ean[2] != '8')
            {
                throw new ArgumentException("EAN doesn't start with 978!", nameof(ean));
            }
        }

        private static byte[] CalculateIsbnDigits(char[] eanDigits)
        {
            var isbnDigits = new byte[Constants.IsbnFullLength];

            Helper.CopyDigits(eanDigits, isbnDigits, Constants.IsbnLowerBound, Constants.EanLowerBoundWithoutPrefix, Constants.EanUpperBound);

            isbnDigits[Constants.IsbnChecksumIndex] = Isbn.CalculateChecksum(isbnDigits);

            return isbnDigits;
        }

        private static string AssembleIsbn(byte[] isbnDigits)
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