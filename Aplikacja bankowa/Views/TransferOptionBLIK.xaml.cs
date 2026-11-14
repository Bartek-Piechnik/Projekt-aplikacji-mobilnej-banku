using Aplikacja_bankowa.Helpers;

namespace Aplikacja_bankowa
{
    public partial class TransferOptionBLIK : ContentPage
    {
        public TransferOptionBLIK()
        {
            InitializeComponent();
            Random random = new Random();
            var number = random.Next(0, 1000000);
            var codeBlik = number.ToString("D6");
            LabelCode.Text = codeBlik;
        }

        private async void OnButtonCopyCodeClicked(object sender, EventArgs e)
        {
            await ButtonAnimations.AnimateButtonAsync(ButtonCopyCode);

            await Clipboard.Default.SetTextAsync(LabelCode.Text);

            await DisplayAlertAsync("Sukces", $"Kod: {LabelCode.Text} został skopiowany!", "OK");
            await Navigation.PopAsync();
        }
    }
}