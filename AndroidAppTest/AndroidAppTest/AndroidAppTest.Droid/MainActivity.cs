using Android.App;
using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Locations;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace AndroidAppTest.Droid
{
	[Activity (Label = "AndroidAppTest.Droid", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity, ILocationListener, IOnMapReadyCallback, GoogleMap.IOnMapClickListener, GoogleMap.IInfoWindowAdapter
	{
		int count = 1;
		private LocationManager _locMgr;
		private Location _location;
		private GoogleMap _map;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);
			

			// Get our button from the layout resource,
			// and attach an event to it
			Button button = FindViewById<Button> (Resource.Id.myButton);
			
			button.Click += delegate {
				button.Text = string.Format ("{0} clicks!", count++);
			};

			//var d = FindViewById<ImageView>(Resource.Id.topbar);
			//d.LayoutParameters.Height = 200;
			//d.RequestLayout();

			//var drawable = d.Drawable;
			//drawable.SetVisible(true, false);
			//drawable.Alpha = 255;
			//drawable.SetBounds(0, 0, 32, 32);
			//d.SetBackgroundResource(Resource.Drawable.Star);

			//var star = Resources.GetDrawable(Resource.Drawable.Star);
			//d.SetImageDrawable(star);
			//d.SetMinimumHeight(32);

			_locMgr = GetSystemService(Context.LocationService) as LocationManager;
			_locMgr.RequestSingleUpdate(new Criteria(), this, Looper.MyLooper());
			

			MapFragment mapFrag = (MapFragment)FragmentManager.FindFragmentById(Resource.Id.map);
			mapFrag.GetMapAsync(this);

			//_myMapFragment = MapFragment.NewInstance();
			//FragmentTransaction tx = FragmentManager.BeginTransaction();
			//tx.Add(Resource.Id.map, _myMapFragment);
			//tx.Commit();
		}

		public void OnMapReady(GoogleMap googleMap)
		{
			_map = googleMap;
			_map.SetInfoWindowAdapter(this);

			var provider = _locMgr.GetBestProvider(new Criteria(), true);
			var location = _locMgr.GetLastKnownLocation(provider);
			
			googleMap.SetOnMapClickListener(this);
			googleMap.MyLocationEnabled = true;
		}

		public void OnLocationChanged(Location location)
		{
			

			var marker = new MarkerOptions();
			marker.SetTitle("Blaa");
			marker.SetPosition(new LatLng(location.Latitude - 0.01, location.Longitude - 0.01));
			//marker.SetIcon(BitmapDescriptorFactory.FromResource();

			_location = location;
			_map.AddMarker(marker);
		}

		public void OnProviderDisabled(string provider)
		{
		}

		public void OnProviderEnabled(string provider)
		{
		}

		public void OnStatusChanged(string provider, Availability status, Bundle extras)
		{
		}

		public void OnMapClick(LatLng point)
		{
			//Drawable vectorDrawable = Resources.GetDrawable(Resource.Drawable.Icon);
			//vectorDrawable.SetBounds(0, 0, 32, 32);
			//Bitmap bm = Bitmap.CreateBitmap(32, 32, Bitmap.Config.Argb8888);
			//Canvas canvas = new Canvas(bm);
			//vectorDrawable.Draw(canvas);
			//var dec = BitmapDescriptorFactory.FromBitmap(bm);

			var marker = new MarkerOptions();
			marker.SetTitle("Oooh");
			//marker.SetIcon(dec);
			marker.SetPosition(new LatLng(point.Latitude - 0.01, point.Longitude - 0.01));
			marker.SetSnippet("blaaa blaa heel veel tekst");
			marker.Visible(true);
			_map.AddMarker(marker);
		}

		public View GetInfoContents(Marker marker)
		{
			//View
			//new Context()
			//var x = new 

			var popup = LayoutInflater.Inflate(Resource.Layout.Info, null);
			var textView = popup.FindViewById<TextView>(Resource.Id.textView1);
			textView.Text = marker.Snippet;

			var but = popup.FindViewById<Button>(Resource.Id.button1);
			but.Text = marker.Snippet;
			//var x = textView.RootView;
			return popup;
		}

		public View GetInfoWindow(Marker marker)
		{
			return null;
		}
	}
}


