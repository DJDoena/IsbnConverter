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
            var isbnDigits = new byte[Isbn.FullLength];

            Helper.CopyDigits(eanDigits, isbnDigits, Isbn.LowerBound, LowerBoundWithoutPrefix, UpperBound);

            isbnDigits[Isbn.ChecksumIndex] = Isbn.CalculateChecksum(isbnDigits);

            return isbnDigits;
        }

        private static string AssembleIsbn(byte[] isbnDigits)
        {
            var isbn = new StringBuilder();

            for (var isbnIndex = Isbn.LowerBound; isbnIndex <= Isbn.UpperBound; isbnIndex++)
            {
                isbn.Append(isbnDigits[isbnIndex]);
            }

            var checksum = isbnDigits[Isbn.ChecksumIndex];

            isbn.Append(checksum == 10
                ? Isbn.CheckDigitX
                : checksum.ToString());

            return isbn.ToString();
        }
    }
}