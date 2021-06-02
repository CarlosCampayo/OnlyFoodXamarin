using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace OnlyFoodXamarin.Controls
{
    public class CardView : ContentView
    {
        public static readonly BindableProperty CardTitleProperty = BindableProperty.Create(nameof(CardTitle), typeof(string), typeof(CardView), string.Empty);
        public static readonly BindableProperty CardDescriptionProperty = BindableProperty.Create(nameof(CardDescription), typeof(string), typeof(CardView), string.Empty);
        public static readonly BindableProperty BorderColorProperty = BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(CardView), Color.DarkGray);
        public static readonly BindableProperty CardColorProperty = BindableProperty.Create(nameof(CardColor), typeof(Color), typeof(CardView), Color.LightGray);
        public static readonly BindableProperty CardTextColorProperty = BindableProperty.Create(nameof(CardTextColor), typeof(Color), typeof(CardView), Color.White);
        public static readonly BindableProperty IconImageSourceProperty = BindableProperty.Create(nameof(IconImageSource), typeof(String), typeof(CardView), String.Empty);
        public static readonly BindableProperty IconBackgroundColorProperty = BindableProperty.Create(nameof(IconBackgroundColor), typeof(Color), typeof(CardView), Color.LightGray);

        public static readonly BindableProperty CardPriceProperty = BindableProperty.Create(nameof(CardPrice), typeof(double), typeof(CardView), double.MinValue);
        public static readonly BindableProperty CardCodeProperty = BindableProperty.Create(nameof(CardCode), typeof(string), typeof(CardView), string.Empty);
        public static readonly BindableProperty CardUrlProperty = BindableProperty.Create(nameof(CardUrl), typeof(string), typeof(CardView), string.Empty);
        public static readonly BindableProperty CardLineProperty = BindableProperty.Create(nameof(CardLine), typeof(Color), typeof(CardView), Color.Red);

        public string CardTitle
        {
            get => (string)GetValue(CardTitleProperty);
            set => SetValue(CardTitleProperty, value);
        }

        public string CardDescription
        {
            get => (string)GetValue(CardDescriptionProperty);
            set => SetValue(CardDescriptionProperty, value);
        }

        public Color BorderColor
        {
            get => (Color)GetValue(BorderColorProperty);
            set => SetValue(BorderColorProperty, value);
        }

        public Color CardColor
        {
            get => (Color)GetValue(CardColorProperty);
            set => SetValue(CardColorProperty, value);
        }

        public Color CardTextColor
        {
            get => (Color)GetValue(CardTextColorProperty);
            set => SetValue(CardTextColorProperty, value);
        }

        public String IconImageSource
        {
            get => (String)GetValue(IconImageSourceProperty);
            set => SetValue(IconImageSourceProperty, value);
        }

        public Color IconBackgroundColor
        {
            get => (Color)GetValue(IconBackgroundColorProperty);
            set => SetValue(IconBackgroundColorProperty, value);
        }

        public double CardPrice
        {
            get => (double)GetValue(CardPriceProperty);
            set => SetValue(CardPriceProperty, value);
        }

        public String CardCode
        {
            get => (String)GetValue(CardCodeProperty);
            set => SetValue(CardCodeProperty, value);
        }

        public String CardUrl
        {
            get => (String)GetValue(CardUrlProperty);
            set => SetValue(CardUrlProperty, value);
        }

        public Color CardLine
        {
            get => (Color)GetValue(CardLineProperty);
            set => SetValue(CardLineProperty, value);
        }
    }
}
