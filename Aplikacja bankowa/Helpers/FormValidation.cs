using Aplikacja_bankowa.Models;
using BankSQLite;

namespace Aplikacja_bankowa.Helpers;

public static class FormValidation
{
    public static List<string> ValidateUserData(UsersItem user)
    {
        List<string> errors = new List<string>();

        if (string.IsNullOrWhiteSpace(user.Name))
        {
            errors.Add("Pole imię nie może być puste!");
        }
        else if (!RegexValidate.IsValidName(user.Name))
        {
            errors.Add("Imię musi zaczynać się wielką literą. Możliwe jest imię dwuczłonowe!");
        }

        if (string.IsNullOrWhiteSpace(user.Surname))
        {
            errors.Add("Pole nazwisko nie może być puste!");
        }
        else if (!RegexValidate.IsValidSurname(user.Surname))
        {
            errors.Add("Nazwisko musi zaczynać się wielką literą. Możliwe są nazwiska dwuczłonowe!");
        }

        if (user.DateOfBirth == null)
        {
            errors.Add("Wybierz datę urodzenia!");
        }

        if (string.IsNullOrWhiteSpace(user.Email))
        {
            errors.Add("Pole email nie może być puste!");
        }
        else if (!RegexValidate.IsValidEmail(user.Email))
        {
            errors.Add("Podaj poprawny adres email np. przyklad@gmail.com!");
        }

        if (string.IsNullOrWhiteSpace(user.PhoneNumber))
        {
            errors.Add("Pole numer telefonu nie może być puste!");
        }
        else if (!RegexValidate.IsValidPhoneNumber(user.PhoneNumber))
        {
            errors.Add("Numer telefonu musi zawierać 9 cyfr. Możesz również podać numer z prefiksem kraju, np. +48123456789!");
        }

        return errors;
    }
    public static List<string> ValidateRegistration(UsersItem user, string password, string repeatedPassword)
    {
        List<string> errors = new List<string>();

        errors.AddRange(ValidateUserData(user));

        if (string.IsNullOrWhiteSpace(password))
        {
            errors.Add("Pole hasło nie może być puste!");
        }
        else if (!RegexValidate.IsValidPassword(password))
        {
            errors.Add("Hasło musi zawierać co najmniej 8 znaków!");
        }

        if (string.IsNullOrWhiteSpace(repeatedPassword))
        {
            errors.Add("Pole powtórzone hasło nie może być puste!");
        }
        else if (password != repeatedPassword)
        {
            errors.Add("Hasło i powtórzone hasło muszą być identyczne!");
        }

        return errors;
    }

    public static List<string> ValidatePasswordChange(string oldPassword, string newPassword, string repeatedNewPassword)
    {
        List<string> errors = new List<string>();

        if (string.IsNullOrWhiteSpace(oldPassword))
        {
            errors.Add("Pole stare hasło nie może być puste!");
        }

        if (string.IsNullOrWhiteSpace(newPassword))
        {
            errors.Add("Pole nowe hasło nie może być puste!");
        }
        else if (!RegexValidate.IsValidPassword(newPassword))
        {
            errors.Add("Hasło musi zawierać co najmniej 8 znaków!");
        }

        if (string.IsNullOrWhiteSpace(repeatedNewPassword))
        {
            errors.Add("Pole powtórzone nowe hasło nie może być puste!");
        }

        if (!string.IsNullOrWhiteSpace(newPassword) && !string.IsNullOrWhiteSpace(repeatedNewPassword) && newPassword != repeatedNewPassword)
        {
            errors.Add("Hasło i powtórzone hasło muszą być identyczne!");
        }

        return errors;
    }
}