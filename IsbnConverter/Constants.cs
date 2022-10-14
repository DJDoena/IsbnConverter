namespace DoenaSoft.IsbnConverter
{
    internal static class Constants
    {
        internal const byte IsbnLowerBound = 0;
        internal const byte IsbnUpperBound = 8;
        internal const byte IsbnChecksumIndex = IsbnUpperBound + 1;
        internal const byte IsbnFullLength = IsbnChecksumIndex + 1;

        internal const byte EanLowerBound = 0;
        internal const byte EanLowerBoundWithoutPrefix = 3;
        internal const byte EanUpperBound = 11;
        internal const byte EanChecksumIndex = EanUpperBound + 1;
        internal const byte EanFullLength = EanChecksumIndex + 1;
    }
}