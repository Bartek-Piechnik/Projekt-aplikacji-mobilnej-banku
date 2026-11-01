using System.Text.RegularExpressions;

namespace BankSQLite;

public static class RegexValidator
{
    private const string NamePattern = @"^[A-ZĄĆĘŁŃÓŚŹŻ][a-ząćęłńóśźż]+([ -][A-ZĄĆĘŁŃÓŚŹŻ][a-ząćęłńóśźż]+)?$";
    private const string SurnamePattern = @"^[A-ZĄĆĘŁŃÓŚŹŻ][a-ząćęłńóśźż]+([ -][A-ZĄĆĘŁŃÓŚŹŻ][a-ząćęłńóśźż]+)*$";
    private const string EmailPattern = @"^[a-zA-Z0-9._-]+@[a-zA-Z0-9]+\.[a-zA-Z]{2,}$";
    private const string PhoneNumberPattern = @"^(?:\+\d{2})?\d{9}$";
    private const string PasswordPattern = @"^[a-ząćęłńóśźżA-ZĄĆĘŁŃÓŚŹŻ0-9!@#$%^&*()_+\-=\[\]{};':""\\|,.<>/? ]{8,}$";
    private const string RecipientsAccountPattern = @"^[0-9]{26}$";

    public static bool IsValidName(string name)
    {
        return !string.IsNullOrWhiteSpace(name) && Regex.IsMatch(name, NamePattern);
    }
    
    public static bool IsValidSurname(string surname)
    {
        return !string.IsNullOrWhiteSpace(surname) && Regex.IsMatch(surname, SurnamePattern);
    }
    
    public static bool IsValidEmail(string email)
    {
        return !string.IsNullOrWhiteSpace(email) && Regex.IsMatch(email, EmailPattern);
    }
    
    public static bool IsValidPhoneNumber(string phoneNumber)
    {
        return !string.IsNullOrWhiteSpace(phoneNumber) && Regex.IsMatch(phoneNumber, PhoneNumberPattern);
    }
    
    public static bool IsValidPassword(string password)
    {
        return !string.IsNullOrEmpty(password) && Regex.IsMatch(password, PasswordPattern);
    }
    
    public static bool IsValidRecipientsAccount(string recipientsAccount)
    {
        return !string.IsNullOrWhiteSpace(recipientsAccount) && Regex.IsMatch(recipientsAccount, RecipientsAccountPattern);
    }
}