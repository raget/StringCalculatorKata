using NUnit.Framework;

namespace StringCalculator.Tests;

public class StringCalculatorTests
{
    [TestCase("", 0, TestName = "For empty string - return 0")]
    [TestCase("1", 1, TestName = "For only one number - return that number")]
    [TestCase("1,2", 3, TestName = "For two numbers - return their addition")]
    [TestCase("1,2,3", 6, TestName = "For more numbers - return their addition")]
    [TestCase("1\n2,3", 6, TestName = "New line and comma delimiters")]
    [TestCase("//;\n2;3", 5, TestName = "Custom delimiter")]
    [TestCase("1001,2", 2, TestName = "Numbers bigger than 1000 are ignored")]
    [TestCase("//[***]\n1***2***3", 6, TestName = "Delimiter can be of any length")]
    [TestCase("//[*][%]\n1*2%3", 6, TestName = "Multiple delimiters")]
    [TestCase("//[***][%%]\n1***2%%3", 6, TestName = "Multiple delimiters can be of any lenght")]
    public void CalculatorAdd_PositiveNumbers_ReturnsSum(string input, int expectedResult)
    {
        var result = Calculator.Add(input);

        Assert.That(result, Is.EqualTo(expectedResult));
    }

    [TestCase("1,-2", "-2")]
    [TestCase("1,-2,-3", "-2, -3")]
    [TestCase("//[***][%%]\n1***-2%%-3", "-2, -3")]
    public void CalculatorAdd_NegativeNumbers_ThrowsException(string input, string expectedNegatives)
    {
        var exception = Assert.Throws<NegativesNotAllowedException>(() => Calculator.Add(input));
        Assert.That(exception!.Message, Is.EqualTo($"Negatives not allowed: {expectedNegatives}"));
    }
}