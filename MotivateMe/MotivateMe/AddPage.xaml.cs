using MotivateMe.Models;
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
	public partial class AddPage : ContentPage
	{
		public AddPage ()
		{
			InitializeComponent ();

            this.BindingContext = new Activity();
        }


        private async void Button_Clicked(object sender, EventArgs e)
        {
            Activity activity = (Activity)this.BindingContext;
            Console.WriteLine(activity.ToString());

            await SqliteDataAccess.GetInstance().SaveActivityAsync(activity);

            await AchievementCalculator.GetInstance().CalculateAchievementsAsync();

            this.BindingContext = new Activity();
            // push to listing tab
            var masterPage = this.Parent as TabbedPage;
            masterPage.CurrentPage = masterPage.Children[1];
        }
    }
}