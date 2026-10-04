namespace Aplikacja_bankowa
{
    public partial class MyDataPage : ContentPage
    {
        public MyDataPage()
        {
            InitializeComponent();
        }
        
        private async void OnButtonGoToMenuPageClicked(Object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MenuPage());
        }
    }
}
