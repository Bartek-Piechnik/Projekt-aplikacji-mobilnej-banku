namespace Aplikacja_bankowa.Helpers
{
    public static class PasswordVisibility
    {
        public static void Toggle(Entry entry, Button button)
        {
            if (entry.IsPassword)
            {
                entry.IsPassword = false;
                button.ImageSource = "password_visibility_on.png";
            }
            else
            {
                entry.IsPassword = true;
                button.ImageSource = "password_visibility_off.png";
            }
        }
    }
}
