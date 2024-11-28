using PusokAlexLab7.Models;

namespace PusokAlexLab7;

public partial class ListPage : ContentPage
{
	public ListPage()
	{
		InitializeComponent();
	}
    async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        var slist = (Shoplist)BindingContext;
        slist.Date = DateTime.UtcNow;
        await App.Database.SaveShopListAsync(slist);
        await Navigation.PopAsync();
    }
    async void OnDeleteButtonClicked(object sender, EventArgs e)
    {
        var slist = (Shoplist)BindingContext;
        await App.Database.DeleteShopListAsync(slist);
        await Navigation.PopAsync();
    }
    async void OnChooseButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ProductPage((Shoplist)
       this.BindingContext)
        {
            BindingContext = new Product()
        });

    }
    async void OnDeleteItemButtonClicked(object sender, EventArgs e)
    {
        var product = listView.SelectedItem as Product;
        var shop1 = (Shoplist)BindingContext;
        
        if (product != null)
        {
            await App.Database.DeleteProductAsync(product, shop1);
            listView.ItemsSource = await App.Database.GetListProductsAsync(shop1.ID);
        }
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        var shopl = (Shoplist)BindingContext;

        listView.ItemsSource = await App.Database.GetListProductsAsync(shopl.ID);
    }
}