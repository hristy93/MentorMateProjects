using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using RaysHotDogs.Core.Model;
using RaysHotDogs.Core.Service;
using RaysHotDogs.Utility;

namespace RaysHotDogs
{
    [Activity(Label = "Hotdog details")]
    public class HotDogDetailActivity : Activity
    {
        public const string IMAGE_BITMAP_URL = "http://gillcleerenpluralsight.blob.core.windows.net/files/";

        private ImageView _hotDogImageView;
        private TextView _hotDogNameTextView;
        private TextView _shortDescriptionTextView;
        private TextView _descriptionTextView;
        private TextView _priceTextView;
        private EditText _amountEditText;
        private Button _cancelButton;
        private Button _orderButton;
        private HotDog _selectedHotDog;
        //private HotDogDataService _dataService;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.HotDogDetailView);

            HotDogDataService dataService = new HotDogDataService();
            var selectedHotDogId = Intent.Extras.GetInt("selectedHotDogId");
            _selectedHotDog = dataService.GetHotDogById(selectedHotDogId );
            FindViews();
            BindData();
            HandleEvents();
        }

        private void FindViews()
        {
            _hotDogImageView = FindViewById<ImageView>(Resource.Id.hotDogImageView);
            _hotDogNameTextView = FindViewById<TextView>(Resource.Id.hotDogNameTextView);
            _shortDescriptionTextView = FindViewById<TextView>(Resource.Id.shortDescriptionTextView);
            _descriptionTextView = FindViewById<TextView>(Resource.Id.descriptionTextView);
            _priceTextView = FindViewById<TextView>(Resource.Id.priceTextView);
            _amountEditText = FindViewById<EditText>(Resource.Id.amountEditText);
            _cancelButton = FindViewById<Button>(Resource.Id.cancelButton);
            _orderButton = FindViewById<Button>(Resource.Id.orderButton);
        }

        private void BindData()
        {
            _hotDogNameTextView.Text = _selectedHotDog.Name;
            _shortDescriptionTextView.Text = _selectedHotDog.ShortDescription;
            _descriptionTextView.Text = _selectedHotDog.Description;
            _priceTextView.Text = "Price: " + _selectedHotDog.Price;
            var imageBitmap = ImageHelper.GetImageBitmapFromUrl(IMAGE_BITMAP_URL + _selectedHotDog.ImagePath + ".jpg");
            _hotDogImageView.SetImageBitmap(imageBitmap);
        }

        private void HandleEvents()
        {
            _orderButton.Click += OrderButton_Click;
            _cancelButton.Click += CancelButton_Click;
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            //TODO
        }

        private void OrderButton_Click(object sender, EventArgs e)
        {
            var amount = Int32.Parse(_amountEditText.Text);

            //var dialog = new AlertDialog.Builder(this);
            //dialog.SetTitle("Confirmation");
            //dialog.SetMessage("Your hot dog has been added to your cart!");
            //dialog.Show();

            var intent = new Intent();
            intent.PutExtra("selectedHotDogId", _selectedHotDog.HotDogId);
            intent.PutExtra("amount", amount);
            SetResult(Result.Ok, intent);
            this.Finish();
        }
    }
}