﻿using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using Android.Content;

namespace GreatQuotes
{
	[Activity]			
	public class QuoteDetailActivity : Activity
	{
		int quoteIndex;
		TextView quoteText;
		TextView authorText;

		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			quoteIndex = Intent.Extras.GetInt("quoteIndex");

			SetContentView(Resource.Layout.Detail);

			quoteText = FindViewById<TextView>(Resource.Id.quoteText);
			authorText = FindViewById<TextView>(Resource.Id.authorText);
		}

		protected override void OnResume()
		{
			base.OnResume();

			var quote = App.Quotes[quoteIndex];
			quoteText.Text = quote.QuoteText;
			authorText.Text = quote.Author;
		}

		protected override void OnPause()
		{
			base.OnPause();
			App.Save();
		}

		public override bool OnCreateOptionsMenu(IMenu menu)
		{
			MenuInflater.Inflate(Resource.Menu.detailOptions, menu);

			return base.OnCreateOptionsMenu(menu);
		}

		public override bool OnOptionsItemSelected(IMenuItem item)
		{
			switch (item.ItemId) {
				case Resource.Id.edit_quote:
					Intent intent = new Intent(this, typeof(EditQuoteActivity));
					intent.PutExtra("quoteIndex", quoteIndex);
					StartActivity(intent);
					break;
				case Resource.Id.delete_quote:
					App.Quotes.RemoveAt(quoteIndex);
					StartActivity(typeof(QuoteListActivity));
					Finish();
					break;
			}

			return base.OnOptionsItemSelected(item);
		}
	}
}

