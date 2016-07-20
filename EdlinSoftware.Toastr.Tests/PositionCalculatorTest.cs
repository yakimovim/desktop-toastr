using System.Collections.Generic;
using EdlinSoftware.Toastr.Configuration;
using EdlinSoftware.Toastr.Models;
using EdlinSoftware.Toastr.Models.PositionCalculation;
using Xunit;

namespace EdlinSoftware.Toastr.Tests
{
    public class PositionCalculatorTest
    {
        [Fact]
        public void TopRight()
        {
            var windows = GetPositions();

            GetCalculator(ToastrPositions.TopRight).Recalculate(windows);

            Assert.Equal(1024 - 2*20 - 300, windows[0].Left);
            Assert.Equal(20, windows[0].Top);
            Assert.Equal(300, windows[0].Width);
            Assert.Equal(1024 - 2 * 20 - 300, windows[1].Left);
            Assert.Equal(80, windows[1].Top);
            Assert.Equal(300, windows[1].Width);
            Assert.Equal(1024 - 2 * 20 - 300, windows[2].Left);
            Assert.Equal(205, windows[2].Top);
            Assert.Equal(300, windows[2].Width);
        }

        [Fact]
        public void TopLeft()
        {
            var windows = GetPositions();

            GetCalculator(ToastrPositions.TopLeft).Recalculate(windows);

            Assert.Equal(40, windows[0].Left);
            Assert.Equal(20, windows[0].Top);
            Assert.Equal(300, windows[0].Width);
            Assert.Equal(40, windows[1].Left);
            Assert.Equal(80, windows[1].Top);
            Assert.Equal(300, windows[1].Width);
            Assert.Equal(40, windows[2].Left);
            Assert.Equal(205, windows[2].Top);
            Assert.Equal(300, windows[2].Width);
        }

        [Fact]
        public void TopFullWidth()
        {
            var windows = GetPositions();

            GetCalculator(ToastrPositions.TopFullWidth).Recalculate(windows);

            Assert.Equal(40, windows[0].Left);
            Assert.Equal(20, windows[0].Top);
            Assert.Equal(1024 - 80, windows[0].Width);
            Assert.Equal(40, windows[1].Left);
            Assert.Equal(80, windows[1].Top);
            Assert.Equal(1024 - 80, windows[1].Width);
            Assert.Equal(40, windows[2].Left);
            Assert.Equal(205, windows[2].Top);
            Assert.Equal(1024 - 80, windows[2].Width);
        }

        [Fact]
        public void TopCenter()
        {
            var windows = GetPositions();

            GetCalculator(ToastrPositions.TopCenter).Recalculate(windows);

            Assert.Equal((1024 - 300) / 2.0, windows[0].Left);
            Assert.Equal(20, windows[0].Top);
            Assert.Equal(300, windows[0].Width);
            Assert.Equal((1024 - 300) / 2.0, windows[1].Left);
            Assert.Equal(80, windows[1].Top);
            Assert.Equal(300, windows[1].Width);
            Assert.Equal((1024 - 300) / 2.0, windows[2].Left);
            Assert.Equal(205, windows[2].Top);
            Assert.Equal(300, windows[2].Width);
        }

        [Fact]
        public void BottomRight()
        {
            var windows = GetPositions();

            GetCalculator(ToastrPositions.BottomRight).Recalculate(windows);

            Assert.Equal(1024 - 2 * 20 - 300, windows[0].Left);
            Assert.Equal(768 - 235, windows[0].Top);
            Assert.Equal(300, windows[0].Width);
            Assert.Equal(1024 - 2 * 20 - 300, windows[1].Left);
            Assert.Equal(768 - 175, windows[1].Top);
            Assert.Equal(300, windows[1].Width);
            Assert.Equal(1024 - 2 * 20 - 300, windows[2].Left);
            Assert.Equal(768 - 50, windows[2].Top);
            Assert.Equal(300, windows[2].Width);
        }

        [Fact]
        public void BottomLeft()
        {
            var windows = GetPositions();

            GetCalculator(ToastrPositions.BottomLeft).Recalculate(windows);

            Assert.Equal(40, windows[0].Left);
            Assert.Equal(768 - 235, windows[0].Top);
            Assert.Equal(300, windows[0].Width);
            Assert.Equal(40, windows[1].Left);
            Assert.Equal(768 - 175, windows[1].Top);
            Assert.Equal(300, windows[1].Width);
            Assert.Equal(40, windows[2].Left);
            Assert.Equal(768 - 50, windows[2].Top);
            Assert.Equal(300, windows[2].Width);
        }

        [Fact]
        public void BottomFullWidth()
        {
            var windows = GetPositions();

            GetCalculator(ToastrPositions.BottomFullWidth).Recalculate(windows);

            Assert.Equal(40, windows[0].Left);
            Assert.Equal(768 - 235, windows[0].Top);
            Assert.Equal(1024 - 80, windows[0].Width);
            Assert.Equal(40, windows[1].Left);
            Assert.Equal(768 - 175, windows[1].Top);
            Assert.Equal(1024 - 80, windows[1].Width);
            Assert.Equal(40, windows[2].Left);
            Assert.Equal(768 - 50, windows[2].Top);
            Assert.Equal(1024 - 80, windows[2].Width);
        }

        [Fact]
        public void BottomCenter()
        {
            var windows = GetPositions();

            GetCalculator(ToastrPositions.BottomCenter).Recalculate(windows);

            Assert.Equal((1024 - 300) / 2.0, windows[0].Left);
            Assert.Equal(768 - 235, windows[0].Top);
            Assert.Equal(300, windows[0].Width);
            Assert.Equal((1024 - 300) / 2.0, windows[1].Left);
            Assert.Equal(768 - 175, windows[1].Top);
            Assert.Equal(300, windows[1].Width);
            Assert.Equal((1024 - 300) / 2.0, windows[2].Left);
            Assert.Equal(768 - 50, windows[2].Top);
            Assert.Equal(300, windows[2].Width);
        }

        private PositionCalculator GetCalculator(ToastrPositions position)
        {
            return PositionCalculatorFactory.GetCalculator(1024, 768, 20, 10, 300, position);
        }

        private IReadOnlyList<WindowPosition> GetPositions()
        {
            return new List<WindowPosition>
            {
                new WindowPosition { Height = 50 },
                new WindowPosition { Height = 115 },
                new WindowPosition { Height = 30 }
            };
        }

        private class WindowPosition : IPosition
        {
            public double Left { get; set; }
            public double Top { get; set; }
            public double Width { get; set; }
            public double Height { get; set; }
        }
    }
}