namespace WordParser.Core.Models
{
    internal class WordItemModel
    {
        /// <summary>
        /// Слово.
        /// </summary>
        public string? Word { get; set; }
        /// <summary>
        /// Уникальность слова (то, сколько оно встречается раз на странице).
        /// </summary>
        public int WordUniqueness { get; set; }

        /// <summary>
        /// Модель хранения слова и его уникальности.
        /// </summary>
        /// <param name="word"></param>
        /// <param name="wordUniqueness"></param>
        internal WordItemModel(string word, int wordUniqueness)
        {
            Word = word;
            WordUniqueness = wordUniqueness;
        }
    }
}
