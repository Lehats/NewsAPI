namespace NewsAPI.Helpers;

public static class StringHelper
{
    public static string CombineKeyWords(this string commaSeparatedString)
    {
        var splitString = new List<string>();
        foreach (var s in commaSeparatedString.Split(","))
        {
            splitString.Add($"\"{s}\"");
        }
        return string.Join(" OR ",splitString);
    }

    public static string RemoveQuotes(this string stringWithQuotes)
    {
        // If string does not end or start with \" return input string.
        if (!stringWithQuotes.StartsWith("\"") || !stringWithQuotes.EndsWith("\"")) return stringWithQuotes;

        var cleanString = stringWithQuotes.Substring(1);
        return cleanString.Substring(0, cleanString.Length-1);
    }
}