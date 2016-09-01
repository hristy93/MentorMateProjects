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
using RaysHotDogs.Utility;
using Android.Graphics;

namespace RaysHotDogs.Adapters
{
    public class HotDogListAdapter: BaseAdapter<HotDog>
    {
        private List<HotDog> _hotDogItems;
        private Activity _context;

        public HotDogListAdapter(Activity context, List<HotDog> items) : base()
        {
            _context = context;
            _hotDogItems = items;
        }

        public override long GetItemId(int position) => position;
        public override HotDog this[int position] => _hotDogItems[position];
        public override int Count => _hotDogItems.Count;
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = _hotDogItems[position];
            var imageBitmap = ImageHelper.GetImageBitmapFromUrl(HotDogDetailActivity.IMAGE_BITMAP_URL + item.ImagePath + ".jpg");
            if (convertView == null)
            {
                convertView = _context.LayoutInflater.Inflate(Resource.Layout.HotDogRowView, null);
            }

            FintViews(convertView, item, imageBitmap);
            return convertView;
        }

        private static void FintViews(View convertView, HotDog item, Bitmap imageBitmap)
        {
            convertView.FindViewById<TextView>(Resource.Id.hotDogNameTextView).Text = item.Name;
            convertView.FindViewById<TextView>(Resource.Id.shortDescriptionTextView).Text = item.ShortDescription;
            convertView.FindViewById<TextView>(Resource.Id.priceTextView).Text = "$ " + item.Price;
            convertView.FindViewById<ImageView>(Resource.Id.hotDogImageView).SetImageBitmap(imageBitmap);
        }
    }
}