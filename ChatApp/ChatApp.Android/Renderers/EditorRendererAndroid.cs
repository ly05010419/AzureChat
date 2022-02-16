using Android.Content;
using ChatApp.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Editor), typeof(EditorRendererAndroid), new[] { typeof(VisualMarker.DefaultVisual) })]
namespace ChatApp.Droid.Renderers
{
     public class EditorRendererAndroid : EditorRenderer
    {
        public EditorRendererAndroid(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.Background = null;
                Control.SetBackgroundColor(Android.Graphics.Color.Transparent);
            }
        }
    }
}