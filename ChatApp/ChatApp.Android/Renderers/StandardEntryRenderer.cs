using System.ComponentModel;
using Android.Content;
using Android.Graphics.Drawables;
using ChatApp.Components;
using ChatApp.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(StandardEntry), typeof(StandardEntryRenderer))]
namespace ChatApp.Droid.Renderers
{
    public class StandardEntryRenderer : EntryRenderer
    {
        public StandardEntryRenderer(Context context) : base(context) { }
        
        private StandardEntry Entry => Element as StandardEntry;

        protected override FormsEditText CreateNativeControl()
        {
            var control = base.CreateNativeControl();
            UpdateBackground(control);
            return control;
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == StandardEntry.BorderColorProperty.PropertyName)
            {
                UpdateBackground();
            }
            else if (e.PropertyName == StandardEntry.BorderThicknessProperty.PropertyName)
            {
                UpdateBackground();
            }

            Control?.SetTextColor(Android.Graphics.Color.Black);

            base.OnElementPropertyChanged(sender, e);
        }

        private void UpdateBackground(FormsEditText control)
        {
            if (control == null) return;
            
            var gd = new GradientDrawable();
            gd.SetColor(Element.BackgroundColor.ToAndroid());
            gd.SetCornerRadius(Context.ToPixels(Entry.CornerRadius));
            gd.SetStroke((int) Context.ToPixels(Entry.BorderThickness), Entry.BorderColor.ToAndroid());
            control.SetBackground(gd);

            var padding = (int) Context.ToPixels(Entry.PaddingLeftAndRight);
            control.SetPadding(padding, 0, padding, 0);
        }
    }
}