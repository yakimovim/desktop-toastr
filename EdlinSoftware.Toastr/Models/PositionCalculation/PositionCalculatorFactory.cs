using System;
using System.Collections.Generic;
using System.Linq;
using EdlinSoftware.Toastr.Configuration;

namespace EdlinSoftware.Toastr.Models.PositionCalculation
{
    public static class PositionCalculatorFactory
    {
        public static PositionCalculator GetCalculator(uint desktopWidth, uint desktopHeight, uint hOffset, uint vOffset, uint notificationWidth, ToastrPositions position)
        {
            switch (position)
            {
                case ToastrPositions.TopRight:
                    return new TopRightPositionCalculator(desktopWidth, desktopHeight, hOffset, vOffset, notificationWidth);
                case ToastrPositions.TopLeft:
                    return new TopLeftPositionCalculator(desktopWidth, desktopHeight, hOffset, vOffset, notificationWidth);
                case ToastrPositions.TopFullWidth:
                    return new TopFullWidthPositionCalculator(desktopWidth, desktopHeight, hOffset, vOffset, notificationWidth);
                case ToastrPositions.TopCenter:
                    return new TopCenterPositionCalculator(desktopWidth, desktopHeight, hOffset, vOffset, notificationWidth);
                case ToastrPositions.BottomRight:
                    return new BottomRightPositionCalculator(desktopWidth, desktopHeight, hOffset, vOffset, notificationWidth);
                case ToastrPositions.BottomLeft:
                    return new BottomLeftPositionCalculator(desktopWidth, desktopHeight, hOffset, vOffset, notificationWidth);
                case ToastrPositions.BottomFullWidth:
                    return new BottomFullWidthPositionCalculator(desktopWidth, desktopHeight, hOffset, vOffset, notificationWidth);
                case ToastrPositions.BottomCenter:
                    return new BottomCenterPositionCalculator(desktopWidth, desktopHeight, hOffset, vOffset, notificationWidth);
                default:
                    throw new ArgumentOutOfRangeException(nameof(position), position, $"Position calculator for position '{position}' is not implemented.");
            }
        }
    }

    public abstract class PositionCalculator
    {
        protected readonly int Width;
        protected readonly int DesktopWidth;
        protected readonly int DesktopHeight;
        protected readonly int HOffset;
        protected readonly int VOffset;

        protected PositionCalculator(uint desktopWidth, uint desktopHeight, uint hOffset, uint vOffset, uint notificationWidth)
        {
            Width = (int)notificationWidth;
            DesktopWidth = (int)desktopWidth;
            DesktopHeight = (int)desktopHeight;
            HOffset = (int)hOffset;
            VOffset = (int)vOffset;
        }

        public abstract void Recalculate(IReadOnlyCollection<IPosition> windows);
    }

    public class TopLeftPositionCalculator : PositionCalculator
    {
        public TopLeftPositionCalculator(uint desktopWidth, uint desktopHeight, uint hOffset, uint vOffset, uint notificationWidth)
            : base(desktopWidth, desktopHeight, hOffset, vOffset, notificationWidth)
        { }

        public override void Recalculate(IReadOnlyCollection<IPosition> windows)
        {
            var top = 2.0 * VOffset;

            foreach (var window in windows)
            {
                window.Left = 2 * HOffset;
                window.Top = top;
                window.Width = Width;

                top += window.Height + VOffset;
            }
        }
    }

    public class TopRightPositionCalculator : PositionCalculator
    {
        public TopRightPositionCalculator(uint desktopWidth, uint desktopHeight, uint hOffset, uint vOffset, uint notificationWidth)
            : base(desktopWidth, desktopHeight, hOffset, vOffset, notificationWidth)
        { }

        public override void Recalculate(IReadOnlyCollection<IPosition> windows)
        {
            var top = 2.0 * VOffset;

            foreach (var window in windows)
            {
                window.Left = DesktopWidth - Width - 2 * HOffset;
                window.Top = top;
                window.Width = Width;

                top += window.Height + VOffset;
            }
        }
    }

    public class TopFullWidthPositionCalculator : PositionCalculator
    {
        public TopFullWidthPositionCalculator(uint desktopWidth, uint desktopHeight, uint hOffset, uint vOffset, uint notificationWidth)
            : base(desktopWidth, desktopHeight, hOffset, vOffset, notificationWidth)
        { }

        public override void Recalculate(IReadOnlyCollection<IPosition> windows)
        {
            var top = 2.0 * VOffset;

            foreach (var window in windows)
            {
                window.Left = 2 * HOffset;
                window.Top = top;
                window.Width = DesktopWidth - 4 * HOffset;

                top += window.Height + VOffset;
            }
        }
    }

    public class TopCenterPositionCalculator : PositionCalculator
    {
        public TopCenterPositionCalculator(uint desktopWidth, uint desktopHeight, uint hOffset, uint vOffset, uint notificationWidth)
            : base(desktopWidth, desktopHeight, hOffset, vOffset, notificationWidth)
        { }

        public override void Recalculate(IReadOnlyCollection<IPosition> windows)
        {
            var top = 2.0 * VOffset;

            foreach (var window in windows)
            {
                window.Left = (DesktopWidth - Width) / 2.0;
                window.Top = top;
                window.Width = Width;

                top += window.Height + VOffset;
            }
        }
    }

    public class BottomLeftPositionCalculator : PositionCalculator
    {
        public BottomLeftPositionCalculator(uint desktopWidth, uint desktopHeight, uint hOffset, uint vOffset, uint notificationWidth)
            : base(desktopWidth, desktopHeight, hOffset, vOffset, notificationWidth)
        { }

        public override void Recalculate(IReadOnlyCollection<IPosition> windows)
        {
            if (windows.Count == 0)
                return;

            var top = DesktopHeight - (2 * VOffset + windows.Sum(w => w.Height) + (windows.Count - 1) * VOffset);

            foreach (var window in windows)
            {
                window.Left = 2 * HOffset;
                window.Top = top;
                window.Width = Width;

                top += window.Height + VOffset;
            }
        }
    }

    public class BottomRightPositionCalculator : PositionCalculator
    {
        public BottomRightPositionCalculator(uint desktopWidth, uint desktopHeight, uint hOffset, uint vOffset, uint notificationWidth)
            : base(desktopWidth, desktopHeight, hOffset, vOffset, notificationWidth)
        { }

        public override void Recalculate(IReadOnlyCollection<IPosition> windows)
        {
            if (windows.Count == 0)
                return;

            var top = DesktopHeight - (2 * VOffset + windows.Sum(w => w.Height) + (windows.Count - 1) * VOffset);

            foreach (var window in windows)
            {
                window.Left = DesktopWidth - Width - 2 * HOffset;
                window.Top = top;
                window.Width = Width;

                top += window.Height + VOffset;
            }
        }
    }

    public class BottomFullWidthPositionCalculator : PositionCalculator
    {
        public BottomFullWidthPositionCalculator(uint desktopWidth, uint desktopHeight, uint hOffset, uint vOffset, uint notificationWidth)
            : base(desktopWidth, desktopHeight, hOffset, vOffset, notificationWidth)
        { }

        public override void Recalculate(IReadOnlyCollection<IPosition> windows)
        {
            if (windows.Count == 0)
                return;

            var top = DesktopHeight - (2 * VOffset + windows.Sum(w => w.Height) + (windows.Count - 1) * VOffset);

            foreach (var window in windows)
            {
                window.Left = 2 * HOffset;
                window.Top = top;
                window.Width = DesktopWidth - 4 * HOffset;

                top += window.Height + VOffset;
            }
        }
    }

    public class BottomCenterPositionCalculator : PositionCalculator
    {
        public BottomCenterPositionCalculator(uint desktopWidth, uint desktopHeight, uint hOffset, uint vOffset, uint notificationWidth)
            : base(desktopWidth, desktopHeight, hOffset, vOffset, notificationWidth)
        { }

        public override void Recalculate(IReadOnlyCollection<IPosition> windows)
        {
            if (windows.Count == 0)
                return;

            var top = DesktopHeight - (2 * VOffset + windows.Sum(w => w.Height) + (windows.Count - 1) * VOffset);

            foreach (var window in windows)
            {
                window.Left = (DesktopWidth - Width) / 2.0;
                window.Top = top;
                window.Width = Width;

                top += window.Height + VOffset;
            }
        }
    }
}