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
using RaysHotDogs.Core;
using RaysHotDogs.Utility;

namespace RaysHotDogs.Adapters
{
    public class CartItemsListAdapter: BaseAdapter<CartItem>
    {
        private List<CartItem> _items;
        private Activity _context;

        public CartItemsListAdapter(Activity context, List<CartItem> items): base()
		{
            _context = context;
            _items = items;
        }

        public override long GetItemId(int position) => position;

        public override CartItem this[int position] =>_items[position];

        public override int Count => _items.Count;

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = _items[position];
            var imageBitmap = ImageHelper.GetImageBitmapFromUrl(HotDogDetailActivity.IMAGE_BITMAP_URL + item.HotDog.ImagePath + ".jpg");
            if (convertView == null)
            {
                convertView = _context.LayoutInflater.Inflate(Resource.Layout.CartRowView, null);
            }

            FindViews(convertView, item, imageBitmap);
            return convertView;
        }

        private static void FindViews(View convertView, CartItem item, Android.Graphics.Bitmap imageBitmap)
        {
            convertView.FindViewById<TextView>(Resource.Id.hotDogNameTextView).Text = item.HotDog.Name;
            convertView.FindViewById<TextView>(Resource.Id.amountTextView).Text = item.Amount.ToString();
            convertView.FindViewById<TextView>(Resource.Id.allPriceTextView).Text = "$ " + (item.Amount * item.HotDog.Price).ToString();
            convertView.FindViewById<ImageView>(Resource.Id.hotDogImageView).SetImageBitmap(imageBitmap);
        }
    }
}