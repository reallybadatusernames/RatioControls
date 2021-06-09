using System;
using System.Linq;
using System.ComponentModel;

using UIKit;
using Foundation;
using CoreGraphics;

namespace RatioControls
{
    [DesignTimeVisible(true), Register("RatioContainer"), Category("Percentage Controls")]
    public class RatioContainer : UIView, IComponent
    {
        #region IComponent Implementation

        public ISite Site { get; set; }

        public event EventHandler Disposed;

        #endregion

        #region Constructors and Overrides

        public RatioContainer() : base() { }

        public RatioContainer(CGRect frame) : base(frame) { }

        public RatioContainer(IntPtr handle) : base(handle) { }

        public RatioContainer(NSCoder coder) : base(coder) { }

        public RatioContainer(NSObjectFlag t) : base(t) { }

        public override void AddSubview(UIView view)
        {
            if (view.GetType() != typeof(RatioElement))
                return;

            base.AddSubview(view);
        }

        public override void InsertSubview(UIView view, nint atIndex)
        {
            if (view.GetType() != typeof(RatioElement))
                return;

            base.InsertSubview(view, atIndex);
        }

        public override void InsertSubviewAbove(UIView view, UIView siblingSubview)
        {
            if (view.GetType() != typeof(RatioElement))
                return;

            base.InsertSubviewAbove(view, siblingSubview);
        }

        public override void InsertSubviewBelow(UIView view, UIView siblingSubview)
        {
            if (view.GetType() != typeof(RatioElement))
                return;

            base.InsertSubviewBelow(view, siblingSubview);
        }

        public override void LayoutSubviews()
        {
            nfloat parentHeight = Frame.Height;
            nfloat cursor = 0f;

            var views = Subviews.OrderBy(x => ((RatioElement)x).Order).ToList();

            for (int i = 0; i < views.Count; i++)
            {
                var view = views[i] as RatioElement;
                var percent = view.Percent;
                var height = parentHeight * percent;
                view.Frame = new CGRect(0, cursor + (view.MarginTop), Frame.Width, height);

                cursor = cursor + height;
                view.Layer.ZPosition = view.ZIndex;
            }

            base.LayoutSubviews();
        }

        #endregion
    }

    [DesignTimeVisible(true), Register("RatioElement"), Category("Percentage Controls")]
    public class RatioElement : UIView, IComponent
    {
        #region Properties

        [DisplayName("Percent Of Parent"), Export("Percent"), Browsable(true)]
        public nfloat Percent { get; set; } = .1f;

        [DisplayName("Margin Top"), Export("MarginTop"), Browsable(true)]
        public nfloat MarginTop { get; set; } = 0f;

        [DisplayName("ZIndex"), Export("ZIndex"), Browsable(true)]
        public int ZIndex { get; set; } = 0;

        [DisplayName("Order In Parent"), Export("OrderInParent"), Browsable(true)]
        public int Order { get; set; } = 0;

        #endregion

        #region IComponent Implementation

        public ISite Site { get; set; }

        public event EventHandler Disposed;

        #endregion

        #region Constructors and Overrides

        public RatioElement() : base() { }

        public RatioElement(CGRect frame) : base(frame) { }

        public RatioElement(IntPtr handle) : base(handle) { }

        public RatioElement(NSCoder coder) : base(coder) { }

        public RatioElement(NSObjectFlag t) : base(t) { }

        #endregion
    }
}