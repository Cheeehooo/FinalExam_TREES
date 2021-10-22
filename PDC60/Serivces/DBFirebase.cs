using PDC60.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;
using System.Linq;

namespace PDC60.Serivces
{
    public class DBFirebase
    {
        FirebaseClient client;
        public DBFirebase()
        {
            client = new FirebaseClient("https://pdc60final-default-rtdb.firebaseio.com/");
        }

        public ObservableCollection<Tree> getTree()
        {
            var TreeData = client
                .Child("Tree")
                .AsObservable<Tree>()
                .AsObservableCollection();

            return TreeData;
        }

        internal Task DeleteTree(int v, object text1, object text2, object text3, object text4, object text5, object text6, object text7, object text8, object text9, object text10, object text11)
        {
            throw new NotImplementedException();
        }

        public async Task AddTree(int Id, string Name, string TreeCode, string Identification, string Notes,
                                  string Landmark, string TrunkFlare, string Height, string SurfaceRoots, string Canopy,
                                  string GPSCoordinates)
        {
            Tree em = new Tree()
            {
                Id = Id,
                Name = Name,
                TreeCode = TreeCode,
                Identification = Identification,
                Notes = Notes,
                Landmark = Landmark,
                TrunkFlare = TrunkFlare,
                Height = Height,
                SurfaceRoots = SurfaceRoots,
                Canopy = Canopy,
                GPSCoordinates = GPSCoordinates
            };
            await client
                .Child("Tree")
                .PostAsync(em);
        }
        public async Task DeleteTree(int Id, string name, string treeCode, string Identification, string Notes,
                                     string Landmark, string TrunkFlare, string Height, string SurfaceRoots, string Canopy,
                                     string gpsCoordinates)
        {
            var toDeleteTree = (await client
                .Child("Tree")
                .OnceAsync<Tree>()).FirstOrDefault
                (a => a.Object.Name == name || a.Object.TreeCode == treeCode);

            await client.Child("Tree").Child(toDeleteTree.Key).DeleteAsync();
        }
        public async Task UpdateTree(int Id, string name, string treeCode, string Identification, string Notes,
                                     string Landmark, string TrunkFlare, string Height, string SurfaceRoots, string Canopy,
                                     string gpsCoordinates)
        {
            var toUpdateTree = (await client
                .Child("Tree")
                .OnceAsync<Tree>()).FirstOrDefault
                (a => a.Object.Name == name);

            Tree t = new Tree()
            {
                Id = Id,
                Name = name,
                TreeCode = treeCode,
                Identification = Identification,
                Notes = Notes,
                Landmark = Landmark,
                TrunkFlare = TrunkFlare,
                Height = Height,
                SurfaceRoots = SurfaceRoots,
                Canopy = Canopy,
                GPSCoordinates = gpsCoordinates
            };
            await client
                .Child("Tree")
                .Child(toUpdateTree.Key)
                .PutAsync(t);
        }
    }
}
