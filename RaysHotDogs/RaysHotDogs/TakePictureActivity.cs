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
using Java.IO;
using Android.Provider;
using RaysHotDogs.Utility;
using Android.Graphics;

namespace RaysHotDogs
{
    [Activity(Label = "Take a picture with Ray")]
    public class TakePictureActivity : Activity
    {
        private ImageView _rayPictureImageView;
        private Button _takePictureButton;
        private File _imageDirectory;
        private File _imageFile;
        private Bitmap _imageBitmap;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.TakePictureView);

            FindViews();
            HandleEvents();
            CreateImageDirectory();
        }

        private void CreateImageDirectory()
        {
            _imageDirectory = new File(Android.OS.Environment.GetExternalStoragePublicDirectory(
                Android.OS.Environment.DirectoryPictures), "RaysHotDogs");
            if (!_imageDirectory.Exists())
            {
                _imageDirectory.Mkdirs();
            }
        }

        private void FindViews()
        {
            _rayPictureImageView = FindViewById<ImageView>(Resource.Id.rayPictureImageView);
            _takePictureButton = FindViewById<Button>(Resource.Id.takePictureButton);
        }

        private void HandleEvents()
        {
            _takePictureButton.Click += TakePictureButton_Click;
        }

        private void TakePictureButton_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(MediaStore.ActionImageCapture);
            _imageFile = new File(_imageDirectory, $"PhotoWithRay_{Guid.NewGuid()}.jpg");
            intent.PutExtra(MediaStore.ExtraOutput, Android.Net.Uri.FromFile(_imageFile));
            StartActivityForResult(intent, 0);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            int height = _rayPictureImageView.Height;
            int width = _rayPictureImageView.Width;
            _imageBitmap = ImageHelper.GetImageBitmapFromFilePath(_imageFile.Path, width, height);

            if (_imageBitmap != null)
            {
                _rayPictureImageView.SetImageBitmap(_imageBitmap);
                _imageBitmap = null;
            }

            GC.Collect();
        }
    }
}