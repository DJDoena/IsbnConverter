namespace DoenaSoft.IsbnConverter
{
    /// <summary />
    public interface IConvert
    {
        /// <summary>
        /// Converts source (e.g. ISBN) to target (e.g. EAN).
        /// </summary>
        string Convert(string source);
    }
}