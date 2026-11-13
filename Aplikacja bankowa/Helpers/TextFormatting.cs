using Aplikacja_bankowa.Models;
using BankSQLite;

namespace Aplikacja_bankowa.Helpers;

public static class TextFormatting
{
    public static string FormatName(string name)
    {
        name = name.Trim().ToLower();

        return char.ToUpper(name[0]) + name.Substring(1);
    }

    public static void FormatUserData(UsersItem user)
    {
        user.Name = FormatName(user.Name);
        user.Surname = FormatName(user.Surname);
        user.Email = user.Email.Trim().ToLowerInvariant();
        user.PhoneNumber = user.PhoneNumber.Trim();
    }
}