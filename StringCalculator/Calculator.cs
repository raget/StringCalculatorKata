namespace StringCalculator;

public static class Calculator
{
    /// <exception cref="NegativesNotAllowedException">When input contains any negative number</exception>
    public static int Add(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return 0;
        }

        var delimiters = new List<string> { ",", "\n" };

        if (input.UsesCustomDelimiter())
        {
            delimiters.AddRange(input.GetCustomDelimiters());
            input = input.GetInputAfterCustomDelimiter();
        }

        var numbers = input
            .Split(delimiters.ToArray(), StringSplitOptions.None)
            .Select(int.Parse)
            .ToArray();

        if (numbers.Any(IsNegative))
        {
            throw new NegativesNotAllowedException(numbers.Where(IsNegative));
        }

        return numbers
            .Where(n => n <= 1000)
            .Sum();

        bool IsNegative(int n) => n < 0;
    }

    private static bool UsesCustomDelimiter(this string input) => input.StartsWith("//");

    private static string GetInputAfterCustomDelimiter(this string input) => input.Split('\n').Last();

    private static IEnumerable<string> GetCustomDelimiters(this string input)
    {
        var delimiterSection = input
            .Split('\n')
            .First()
            .Replace("//", "")
            .Trim('[', ']');

        var hasMultipleDelimiters = delimiterSection.Contains("][");
        if (hasMultipleDelimiters)
        {
            return delimiterSection
                .Replace("][", ";")
                .Split(';');
        }

        return new[] { delimiterSection };
    }

}