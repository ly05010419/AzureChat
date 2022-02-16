using Xamarin.Forms;

namespace ChatApp.Components
{
    public partial class StandardEntry : Entry
    {
        public static readonly BindableProperty BorderThicknessProperty
            = BindableProperty.Create(nameof(BorderThickness), typeof(int), typeof(StandardEntry));

        public static readonly BindableProperty BorderColorProperty
            = BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(StandardEntry));

        public static readonly BindableProperty CornerRadiusProperty
            = BindableProperty.Create(nameof(CornerRadius), typeof(int), typeof(StandardEntry));

        public static readonly BindableProperty PaddingLeftAndRightProperty
            = BindableProperty.Create(nameof(PaddingLeftAndRight), typeof(int), typeof(StandardEntry));

        public int BorderThickness
        {
            get => (int)GetValue(BorderThicknessProperty);
            set => SetValue(BorderThicknessProperty, value);
        }

        public Color BorderColor
        {
            get => (Color)GetValue(BorderColorProperty);
            set => SetValue(BorderColorProperty, value);
        }

        public int CornerRadius
        {
            get => (int)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        public int PaddingLeftAndRight
        {
            get => (int)GetValue(PaddingLeftAndRightProperty);
            set => SetValue(PaddingLeftAndRightProperty, value);
        }
    }
}