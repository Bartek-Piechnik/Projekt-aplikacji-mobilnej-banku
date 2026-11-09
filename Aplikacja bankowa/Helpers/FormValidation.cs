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
            errors.Add("Imię nie może być puste!");
        }
        else if (!RegexValidate.IsValidName(user.Name))
        {
            errors.Add("Imię musi zaczynać się wielką literą. Możliwe jest imię dwuczłonowe!");
        }

        if (string.IsNullOrWhiteSpace(user.Surname))
        {
            errors.Add("Nazwisko nie może być puste!");
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
            errors.Add("Email nie może być pusty!");
        }
        else if (!RegexValidate.IsValidEmail(user.Email))
        {
            errors.Add("Podaj poprawny adres email np. przyklad@gmail.com!");
        }

        if (string.IsNullOrWhiteSpace(user.PhoneNumber))
        {
            errors.Add("Numer telefonu nie może być pusty!");
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
            errors.Add("Hasło nie może być puste!");
        }
        else if (!RegexValidate.IsValidPassword(password))
        {
            errors.Add("Hasło musi zawierać co najmniej 8 znaków!");
        }

        if (string.IsNullOrWhiteSpace(repeatedPassword))
        {
            errors.Add("Powtórzone hasło nie może być puste!");
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
            errors.Add("Stare hasło nie może być puste!");
        }

        if (string.IsNullOrWhiteSpace(newPassword))
        {
            errors.Add("Nowe hasło nie może być puste!");
        }
        else if (!RegexValidate.IsValidPassword(newPassword))
        {
            errors.Add("Hasło musi zawierać co najmniej 8 znaków!");
        }

        if (string.IsNullOrWhiteSpace(repeatedNewPassword))
        {
            errors.Add("Powtórzone nowe hasło nie może być puste!");
        }

        if (!string.IsNullOrWhiteSpace(newPassword) && !string.IsNullOrWhiteSpace(repeatedNewPassword) && newPassword != repeatedNewPassword)
        {
            errors.Add("Nowe hasło i powtórzone hasło muszą być identyczne!");
        }

        return errors;
    }

    public static List<string> ValidateTransaction(TransactionsItem transaction)
    {
        List<string> errors = new List<string>();

        if (string.IsNullOrWhiteSpace(transaction.TransferTitle))
        {
            errors.Add("Tytuł przelewu nie może być pusty!");
        }
        else if (!RegexValidate.IsValidTransferTitle(transaction.TransferTitle))
        {
            errors.Add("Tytuł przelewu może zawierać maksymalnie 30 znaków!");
        }

        if (string.IsNullOrWhiteSpace(transaction.TransferType))
        {
            errors.Add("Typ przelewu musi być wybrany!");
        }

        return errors;
    }
}