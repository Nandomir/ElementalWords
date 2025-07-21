using ElementalWords.Data;

namespace ElementalWords.Services;

public static class ElementWordFinder
{
    private static readonly Dictionary<string, string> UppercaseToOriginalSymbol =
           ElementsData.Elements.Keys.ToDictionary(
               symbol => symbol.ToUpperInvariant(),
               symbol => symbol
           );

    public static string[][] ElementalForms(string word)
    {
        if (string.IsNullOrEmpty(word))
        {
            return [];
        }

        var results = new List<string[]>();
        var currentPath = new List<string>();

        FindCombinations(word.ToUpperInvariant(), 0, currentPath, results);

        return [.. results];
    }

    private static void FindCombinations(
        string word,
        int startIndex,
        List<string> currentPath,
        List<string[]> results)
    {
        if (startIndex == word.Length)
        {
            results.Add([.. currentPath]);
            return;
        }

        for (int length = 1; length <= 3 && startIndex + length <= word.Length; length++)
        {
            string potentialSymbol = word.Substring(startIndex, length);

            if (UppercaseToOriginalSymbol.TryGetValue(potentialSymbol, out string originalSymbol))
            {
                string elementName = ElementsData.Elements[originalSymbol];
                string formatted = $"{elementName} ({originalSymbol})";

                currentPath.Add(formatted);
                FindCombinations(word, startIndex + length, currentPath, results);
                currentPath.RemoveAt(currentPath.Count - 1);
            }
        }
    }
}
