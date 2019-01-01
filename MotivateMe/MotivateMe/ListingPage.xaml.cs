using Plugin.LocalNotifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MotivateMe
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ListingPage : ContentPage
	{
		public ListingPage ()
		{
			InitializeComponent ();
		}

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            listView.ItemsSource = await SqliteDataAccess.GetInstance().GetActivitiesAsync();
        }

        void OnListItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Console.WriteLine(e.SelectedItem);
            // TODO navigate to details view
            //if (e.SelectedItem != null)
            //{
            //    await Navigation.PushAsync(new TodoItemPage
            //    {
            //        BindingContext = e.SelectedItem as TodoItem
            //    });
            //}
        }

    }
}