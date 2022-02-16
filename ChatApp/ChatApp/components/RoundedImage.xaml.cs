
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Shapes;
using Xamarin.Forms.Xaml;

namespace ChatApp.components
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RoundedImage : Image
      { 
        public RoundedImage()
        {
            InitializeComponent();
        }
        
        public static readonly BindableProperty ImageSourceProperty = BindableProperty.Create(
            nameof(ImageSource),
            typeof(string),
            typeof(RoundedImage),
            propertyChanged: OnImageSourcePropertyChanged
        );
        
        public string ImageSource
        {
            get => (string) GetValue(ImageSourceProperty);
            set => SetValue(ImageSourceProperty, value);
        }
        
        private static void OnImageSourcePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((RoundedImage)bindable).OnImageSourcePropertyChangedExecute((string)oldValue, (string)newValue);
        }

        private async Task OnImageSourcePropertyChangedExecute(string oldValue, string newValue)
        {
            Source = Xamarin.Forms.ImageSource.FromUri(new Uri(newValue));
        }

        protected override void OnParentSet()
        {
            base.OnParentSet();

            var height = IsSet(HeightRequestProperty) ? HeightRequest : 0;
            var width = IsSet(WidthRequestProperty) ? WidthRequest : 0;
            
            Clip = new EllipseGeometry
            {
                Center = new Point(height / 2, width / 2), 
                RadiusX = height / 2, 
                RadiusY = width / 2
            };
        }
    }
}