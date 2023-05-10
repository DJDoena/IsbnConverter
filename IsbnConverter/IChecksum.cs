namespace DoenaSoft.IsbnConverter
{
    using System.IO;

    /// <summary />
    public interface IChecksum
    {
        /// <summary>
        /// Calculates the checksum.
        /// </summary>
        /// <param name="source" />
        /// <param name="throwOnError">if the checksum is already given and this parameter is true then an <see cref="InvalidDataException"/> is thrown when the calculated checksum differs from the given one</param>
        /// <returns>the checksum</returns>
        string GetCheckSum(string source, bool throwOnError = false);

        /// <summary>
        /// Checks if the checksum is correct.
        /// </summary>
        bool IsValidCheckSum(string source);
    }
}