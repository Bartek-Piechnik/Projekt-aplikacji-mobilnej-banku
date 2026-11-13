using Aplikacja_bankowa.Models;
using BankSQLite;

namespace Aplikacja_bankowa.Helpers;

public static class TextFormatting
{
    public static string FormatName(string name)
    {
        name = name.Trim().ToLower();

        string result = "";

        for (int i = 0; i < name.Length; i++)
        {
            if (i == 0 || name[i - 1] == ' ' || name[i - 1] == '-')
            {
                result += char.ToUpper(name[i]);
            }
            else
            {
                result += name[i];
            }
        }

        return result;
    }

    public static void FormatUserData(UsersItem user)
    {
        user.Name = FormatName(user.Name);
        user.Surname = FormatName(user.Surname);
        user.Email = user.Email.Trim().ToLowerInvariant();
        user.PhoneNumber = user.PhoneNumber.Trim();
    }
}