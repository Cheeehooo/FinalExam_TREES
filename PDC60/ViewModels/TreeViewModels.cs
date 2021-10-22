using System;
using System.Collections.Generic;
using System.Text;


using System.Threading.Tasks;
using PDC60.Models;
using PDC60.Serivces;
using MvvmHelpers;
using Xamarin.Forms;
using System.Collections.ObjectModel;

namespace PDC60.ViewModels
{
    class TreeViewModels : BaseViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string TreeCode { get; set; }
        public string Identification { get; set; }
        public string Notes { get; set; }
        public string GPSCoordinates { get; set; }
        public string Landmark { get; set; }
        public string TrunkFlare { get; set; }
        public string Height { get; set; }
        public string SurfaceRoots { get; set; }
        public string Canopy { get; set; }

        private DBFirebase services;

        public Command AddTree { get; }

        public ObservableCollection<Tree> _trees = new ObservableCollection<Tree>();

        public ObservableCollection<Tree> Trees
        {
            get { return _trees; }
            set
            {
                _trees = value;
                OnPropertyChanged();
            }
        }

        public TreeViewModels()
        {
            services = new DBFirebase();
            Trees = services.getTree();
            AddTree = new Command(async () => await addTreeAsync
            (Id, Name, TreeCode, Identification, Notes, Landmark, TrunkFlare, Height, SurfaceRoots, Canopy, GPSCoordinates));
        }

        public async Task addTreeAsync(int Id, string Name, string TreeCode, string Identification, string Notes,
                                       string Landmark, string TrunkFlare, string Height, string SurfaceRoots, string Canopy,
                                       string GPSCoordinates)
        {
            await services.AddTree(Id, Name, TreeCode, Identification, Notes, Landmark, TrunkFlare, Height, SurfaceRoots, Canopy, GPSCoordinates);
        }
    }
}
