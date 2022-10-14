using System;

namespace DoenaSoft.IsbnConverter
{
    /// <summary>
    /// Represents an EAN from "book land".
    /// </summary>
    public sealed class BooklandEan : Ean
    {
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
    }
}