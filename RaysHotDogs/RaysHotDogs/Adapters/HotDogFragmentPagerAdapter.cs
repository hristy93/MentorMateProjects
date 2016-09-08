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
using Android.Support.V4.App;
using Java.Lang;
using Android.Graphics.Drawables;
using Android.Text;
using Android.Text.Style;
using Android.Support.V4.Content;
using RaysHotDogs.Fragments;

namespace RaysHotDogs.Adapters
{
    class HotDogFragmentPagerAdapter : FragmentPagerAdapter
    {
        private const int PAGE_COUNT = 3;
        private string[] _tabTitles = new string[] { "Favorites", "Meat Lovers", "Veggie Lovers" };

        public override int Count => PAGE_COUNT;

        public HotDogFragmentPagerAdapter(Android.Support.V4.App.FragmentManager fm) : base(fm)
        {

        }

        public override ICharSequence GetPageTitleFormatted(int position)
        {
            //return tabTitles[position];
            Drawable image = null;
            image = GetImageByPosition(position);
            image.SetBounds(0, 0, image.IntrinsicWidth, image.IntrinsicHeight);
            SpannableString spannableString = new SpannableString("   " + _tabTitles[position]);
            ImageSpan imageSpan = new ImageSpan(image, SpanAlign.Bottom);
            spannableString.SetSpan(imageSpan, 0, 1, SpanTypes.ExclusiveExclusive);
            return spannableString;
        }

        private static Drawable GetImageByPosition(int position)
        {
            Drawable image;
            if (position == 0)
            {
                image = ContextCompat.GetDrawable(Application.Context, Resource.Drawable.FavoritesIcon);
            }
            else if (position == 1)
            {
                image = ContextCompat.GetDrawable(Application.Context, Resource.Drawable.MeatLoversIcon);
            }
            else
            {
                image = ContextCompat.GetDrawable(Application.Context, Resource.Drawable.VeggieLoversIcon);
            }

            return image;
        }

        public override Android.Support.V4.App.Fragment GetItem(int position)
        {
            if (position == 0)
            {
                return new FavoriteHotDogFragment();
            }
            else if (position == 1)
            {
                return new MeatLoversFragment();
            }
            else
            {
                return new VeggieLoversFragment();
            }
        }
    }
}