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
            await ButtonGoToMenuPage.ScaleToAsync(1.1, 150);
            await ButtonGoToMenuPage.ScaleToAsync(1.0, 150);

            await Navigation.PushAsync(new MenuPage());
        }
    }
}
