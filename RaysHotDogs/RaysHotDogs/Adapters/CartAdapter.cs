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
    public class CartAdapter: BaseAdapter<CartItem>
    {
        List<CartItem> items;
        Activity context;

        public CartAdapter(Activity context, List<CartItem> items): base()
		{
            this.context = context;
            this.items = items;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override CartItem this[int position]
        {
            get
            {
                return items[position];
            }
        }

        public override int Count
        {
            get
            {
                return items.Count;
            }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = items[position];

            var imageBitmap = ImageHelper.GetImageBitmapFromUrl("http://gillcleerenpluralsight.blob.core.windows.net/files/" + item.HotDog.ImagePath + ".jpg");

            if (convertView == null)
            {
                convertView = context.LayoutInflater.Inflate(Resource.Layout.CartRowView, null);
            }

            convertView.FindViewById<TextView>(Resource.Id.hotDogNameTextView).Text = item.HotDog.Name;
            convertView.FindViewById<TextView>(Resource.Id.amountTextView).Text = item.Amount.ToString();
            convertView.FindViewById<ImageView>(Resource.Id.hotDogImageView).SetImageBitmap(imageBitmap);

            return convertView;
        }
    }
}