using ChatApp.components;
using ChatApp.iOS.Renderers;
using CoreGraphics;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(StandardEntry), typeof(StandardEntryRenderer))] 
namespace ChatApp.iOS.Renderers {
    public class StandardEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (Control != null&&Element is StandardEntry standardEntry)
            {
                Control.BorderStyle = UITextBorderStyle.None;
                Control.Layer.CornerRadius = standardEntry.CornerRadius;
                Control.Layer.BorderColor = standardEntry.BorderColor.ToCGColor();
                Control.Layer.BorderWidth = standardEntry.BorderThickness;
                if (standardEntry.PaddingLeftAndRight != 0)
                {
                    Control.LeftView = new UIView(new CGRect(0, 0, standardEntry.PaddingLeftAndRight, 0));
                    Control.LeftViewMode = UITextFieldViewMode.Always;
                    Control.RightView = new UIView(new CGRect(0, 0, standardEntry.PaddingLeftAndRight, 0));
                    Control.RightViewMode = UITextFieldViewMode.Always;
                }
            }
        }
    }
}