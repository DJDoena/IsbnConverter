namespace DoenaSoft.IsbnConverter
{
    /// <summary />
    internal static class Helper
    {
        internal static void CopyDigits(char[] source
            , byte[] target
            , byte leftLowerBound
            , byte rightLowerBound
            , byte rightUpperBound)
        {
            for (byte leftIndex = leftLowerBound, rightIndex = rightLowerBound; rightIndex <= rightUpperBound; leftIndex++, rightIndex++)
            {
                target[leftIndex] = byte.Parse(source[rightIndex].ToString());
            }
        }
    }
}