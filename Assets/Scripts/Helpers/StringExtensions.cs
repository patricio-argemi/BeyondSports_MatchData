using System;
using System.Text.RegularExpressions;

public static class StringExtensions
{
    public static bool ContainsLetters(this String str)
    {
        return Regex.Matches(str, @"[a-zA-Z]").Count > 0;
    }
}
