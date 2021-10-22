using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PDC60.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewTrees : ContentPage
    {
        public ViewTrees()
        {
            InitializeComponent();
        }
        //public async void OnItemSelected(object sender, ItemTappedEventArgs args)
        //{
        //    var trees = args.Item as Tree;
        //    if (trees == null) return;

        //    await Navigation.PushAsync(new DetailsPage(trees));
        //    lstTrees.SelectedItem = null;

        //}
        private async void btnAddTree_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddTree());
        }
    }
}