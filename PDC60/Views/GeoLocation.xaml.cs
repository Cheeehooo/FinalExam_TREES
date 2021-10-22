using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PDC60.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GeoLocation : ContentPage
    {
        public GeoLocation()
        {
            InitializeComponent();
            GetAddress = new Command(async () => await OnGetAddress());
            GetLocation = new Command(async () => await OnGetPosition());
            BindingContext = this;
        }
        string lat = "47.673988";
        string lon = "-122.121513";
        string Glocation = "227b MacArthur Hwy, Angeles, Pampanga, Philippines";
        string geocodeAddress;
        string geocodeLocation;

        public ICommand GetAddress { get; }

        public ICommand GetLocation { get; }

        public string Latitude
        {
            get => lat;
            set => SetProperty(ref lat, value);
        }

        public string Longitude
        {
            get => lon;
            set => SetProperty(ref lon, value);
        }

        public string GeocodeAddress
        {
            get => geocodeAddress;
            set => SetProperty(ref geocodeAddress, value);
        }

        public string Location
        {
            get => Glocation;
            set => SetProperty(ref Glocation, value);
        }

        public string GeocodeLocation
        {
            get => geocodeLocation;
            set => SetProperty(ref geocodeLocation, value);
        }

        async Task OnGetPosition()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            try
            {

                var loc = await Geocoding.GetLocationsAsync(Glocation);
                Location location = loc.FirstOrDefault();
                if (location == null)
                {
                    GeocodeLocation = "Unable to detect locations";
                }
                else
                {
                    GeocodeLocation =
                        $"{nameof(location.Latitude)}: {location.Latitude}\n" +
                        $"{nameof(location.Longitude)}: {location.Longitude}\n";
                }
            }
            catch (Exception ex)
            {
                GeocodeLocation = $"Unable to detect locations: {ex.Message}";
            }
            finally
            {
                IsBusy = false;
            }
        }

        async Task OnGetAddress()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            try
            {
                double.TryParse(lat, out var lt);
                double.TryParse(lon, out var ln);

                var placemarks = await Geocoding.GetPlacemarksAsync(lt, ln);
                Placemark placemark = placemarks.FirstOrDefault();
                if (placemark == null)
                {
                    GeocodeAddress = "Unable to detect placemarks.";
                }
                else
                {
                    GeocodeAddress =
                        $"{nameof(placemark.AdminArea)}: {placemark.AdminArea}\n" +
                        $"{nameof(placemark.CountryCode)}: {placemark.CountryCode}\n" +
                        $"{nameof(placemark.CountryName)}: {placemark.CountryName}\n" +
                        $"{nameof(placemark.FeatureName)}: {placemark.FeatureName}\n" +
                        $"{nameof(placemark.Locality)}: {placemark.Locality}\n" +
                        $"{nameof(placemark.PostalCode)}: {placemark.PostalCode}\n" +
                        $"{nameof(placemark.SubAdminArea)}: {placemark.SubAdminArea}\n" +
                        $"{nameof(placemark.SubLocality)}: {placemark.SubLocality}\n" +
                        $"{nameof(placemark.SubThoroughfare)}: {placemark.SubThoroughfare}\n" +
                        $"{nameof(placemark.Thoroughfare)}: {placemark.Thoroughfare}\n";
                }
            }
            catch (Exception ex)
            {
                GeocodeAddress = $"Unable to detect placemarks: {ex.Message}";
            }
            finally
            {
                IsBusy = false;
            }
        }

        protected virtual bool SetProperty<T>(ref T backingStore, T value, [CallerMemberName] string propertyName = "", Action onChanged = null, Func<T, T, bool> validateValue = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            if (validateValue != null && !validateValue(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }
        bool isGettingLocation;
        async void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            isGettingLocation = true;
            while (isGettingLocation)
            {
                var result = await Geolocation.GetLocationAsync(new GeolocationRequest(GeolocationAccuracy.Default, TimeSpan.FromMinutes(1)));
                RLocation.Text = $"lat: {result.Latitude}, lng: {result.Longitude}{Environment.NewLine}";
                await Task.Delay(1000);
            }
        }

        private void Button_Stop(System.Object sender, System.EventArgs e)
        {
            isGettingLocation = false;
        }
    }
}