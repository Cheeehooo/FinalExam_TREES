using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PDC60.Serivces;
using PDC60.Models;

namespace PDC60.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditTree : ContentPage
    {
        DBFirebase services;
        public EditTree(Tree tree)
        {
            InitializeComponent();
            BindingContext = tree;
            services = new DBFirebase();
        }
    }
}