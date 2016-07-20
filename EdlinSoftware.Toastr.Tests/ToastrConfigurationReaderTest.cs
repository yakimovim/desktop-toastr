using System.Collections.Generic;
using EdlinSoftware.Toastr.Configuration;
using Xunit;

namespace EdlinSoftware.Toastr.Tests
{
    public class ToastrConfigurationReaderTest
    {
        private readonly ToastrConfigurationReader _reader;

        public ToastrConfigurationReaderTest()
        {
            _reader = new ToastrConfigurationReader();
        }

        [Fact]
        public void Get_ShouldReturnDefaultConfiguration_OnNullDictionary()
        {
            var configuration = _reader.Get(null);

            Assert.False(configuration.CloseButton);
            Assert.False(configuration.NewestOnTop);
            Assert.False(configuration.PreventDuplicates);
            Assert.False(configuration.ProgressBar);
            Assert.Equal(ToastrPositions.TopCenter, configuration.PositionClass);
            Assert.Equal(5000L, configuration.TimeOut);
            Assert.Equal(1000L, configuration.ExtendedTimeOut);
            Assert.Equal(ToastrShowMethods.FadeIn, configuration.ShowMethod);
            Assert.Equal(300L, configuration.ShowDuration);
            Assert.Equal(ToastrEasings.Linear, configuration.ShowEasing);
            Assert.Equal(ToastrHideMethods.FadeOut, configuration.HideMethod);
            Assert.Equal(1000L, configuration.HideDuration);
            Assert.Equal(ToastrEasings.Linear, configuration.HideEasing);
            Assert.Equal(300U, configuration.Width);
            Assert.Equal(20U, configuration.HorizontalOffset);
            Assert.Equal(20U, configuration.VerticalOffset);
        }

        [Fact]
        public void Get_ShouldReturnDefaultConfiguration_OnEmptyDictionary()
        {
            var configuration = _reader.Get(new Dictionary<string, object>());

            Assert.False(configuration.CloseButton);
            Assert.False(configuration.NewestOnTop);
            Assert.False(configuration.PreventDuplicates);
            Assert.False(configuration.ProgressBar);
            Assert.Equal(ToastrPositions.TopCenter, configuration.PositionClass);
            Assert.Equal(5000L, configuration.TimeOut);
            Assert.Equal(1000L, configuration.ExtendedTimeOut);
            Assert.Equal(ToastrShowMethods.FadeIn, configuration.ShowMethod);
            Assert.Equal(300L, configuration.ShowDuration);
            Assert.Equal(ToastrEasings.Linear, configuration.ShowEasing);
            Assert.Equal(ToastrHideMethods.FadeOut, configuration.HideMethod);
            Assert.Equal(1000L, configuration.HideDuration);
            Assert.Equal(ToastrEasings.Linear, configuration.HideEasing);
        }

        [Fact]
        public void Get_ShouldSetCloseButton()
        {
            var configuration = _reader.Get(new Dictionary<string, object>
            {
                { "closeButton", true }
            });

            Assert.True(configuration.CloseButton);
        }

        [Fact]
        public void Get_ShouldRaiseErrorEvent_IfTypeOfCloseButtonIsNotBoolean()
        {
            string errorMessage = null;
            _reader.ReadingError += message => { errorMessage = message; };

            var configuration = _reader.Get(new Dictionary<string, object>
            {
                { "closeButton", 3 }
            });

            Assert.False(configuration.CloseButton);
            Assert.NotNull(errorMessage);
            Assert.Contains("closeButton", errorMessage);
        }

        [Fact]
        public void Get_ShouldSetNewestOnTop()
        {
            var configuration = _reader.Get(new Dictionary<string, object>
            {
                { "newestOnTop", true }
            });

            Assert.True(configuration.NewestOnTop);
        }

        [Fact]
        public void Get_ShouldRaiseErrorEvent_IfTypeOfNewestOnTopIsNotBoolean()
        {
            string errorMessage = null;
            _reader.ReadingError += message => { errorMessage = message; };

            var configuration = _reader.Get(new Dictionary<string, object>
            {
                { "newestOnTop", 3 }
            });

            Assert.False(configuration.NewestOnTop);
            Assert.NotNull(errorMessage);
            Assert.Contains("newestOnTop", errorMessage);
        }

        [Fact]
        public void Get_ShouldSetPreventDuplicates()
        {
            var configuration = _reader.Get(new Dictionary<string, object>
            {
                { "preventDuplicates", true }
            });

            Assert.True(configuration.PreventDuplicates);
        }

        [Fact]
        public void Get_ShouldRaiseErrorEvent_IfTypeOfPreventDuplicatesIsNotBoolean()
        {
            string errorMessage = null;
            _reader.ReadingError += message => { errorMessage = message; };

            var configuration = _reader.Get(new Dictionary<string, object>
            {
                { "preventDuplicates", 3 }
            });

            Assert.False(configuration.PreventDuplicates);
            Assert.NotNull(errorMessage);
            Assert.Contains("preventDuplicates", errorMessage);
        }

        [Fact]
        public void Get_ShouldSetProgressBar()
        {
            var configuration = _reader.Get(new Dictionary<string, object>
            {
                { "progressBar", true }
            });

            Assert.True(configuration.ProgressBar);
        }

        [Fact]
        public void Get_ShouldRaiseErrorEvent_IfTypeOfProgressBarIsNotBoolean()
        {
            string errorMessage = null;
            _reader.ReadingError += message => { errorMessage = message; };

            var configuration = _reader.Get(new Dictionary<string, object>
            {
                { "progressBar", 3 }
            });

            Assert.False(configuration.ProgressBar);
            Assert.NotNull(errorMessage);
            Assert.Contains("progressBar", errorMessage);
        }

        [Theory]
        [InlineData("toast-top-right", ToastrPositions.TopRight)]
        [InlineData("toast-top-left", ToastrPositions.TopLeft)]
        [InlineData("toast-top-full-width", ToastrPositions.TopFullWidth)]
        [InlineData("toast-top-center", ToastrPositions.TopCenter)]
        [InlineData("toast-bottom-right", ToastrPositions.BottomRight)]
        [InlineData("toast-bottom-left", ToastrPositions.BottomLeft)]
        [InlineData("toast-bottom-full-width", ToastrPositions.BottomFullWidth)]
        [InlineData("toast-bottom-center", ToastrPositions.BottomCenter)]
        public void Get_ShouldSetPositionClass(string value, ToastrPositions expected)
        {
            var configuration = _reader.Get(new Dictionary<string, object>
            {
                { "positionClass", value }
            });

            Assert.Equal(expected, configuration.PositionClass);
        }

        [Fact]
        public void Get_ShouldRaiseErrorEvent_IfTypeOfPositionClassIsNotString()
        {
            string errorMessage = null;
            _reader.ReadingError += message => { errorMessage = message; };

            var configuration = _reader.Get(new Dictionary<string, object>
            {
                { "positionClass", 3 }
            });

            Assert.Equal(ToastrPositions.TopCenter, configuration.PositionClass);
            Assert.NotNull(errorMessage);
            Assert.Contains("positionClass", errorMessage);
        }

        [Fact]
        public void Get_ShouldRaiseErrorEvent_IfPositionClassIsUnknown()
        {
            string errorMessage = null;
            _reader.ReadingError += message => { errorMessage = message; };

            var configuration = _reader.Get(new Dictionary<string, object>
            {
                { "positionClass", "something" }
            });

            Assert.Equal(ToastrPositions.TopCenter, configuration.PositionClass);
            Assert.NotNull(errorMessage);
            Assert.Contains("something", errorMessage);
        }

        [Theory]
        [InlineData(555)]
        [InlineData(555L)]
        public void Get_ShouldSetTimeOut(object value)
        {
            var configuration = _reader.Get(new Dictionary<string, object>
            {
                { "timeOut", value }
            });

            Assert.Equal(555L, configuration.TimeOut);
        }

        [Fact]
        public void Get_ShouldRaiseErrorEvent_IfTypeOfTimeOutIsNotInteger()
        {
            string errorMessage = null;
            _reader.ReadingError += message => { errorMessage = message; };

            var configuration = _reader.Get(new Dictionary<string, object>
            {
                { "timeOut", "abc" }
            });

            Assert.Equal(5000L, configuration.TimeOut);
            Assert.NotNull(errorMessage);
            Assert.Contains("timeOut", errorMessage);
        }

        [Theory]
        [InlineData(555)]
        [InlineData(555L)]
        public void Get_ShouldSetExtendedTimeOut(object value)
        {
            var configuration = _reader.Get(new Dictionary<string, object>
            {
                { "extendedTimeOut", value }
            });

            Assert.Equal(555L, configuration.ExtendedTimeOut);
        }

        [Fact]
        public void Get_ShouldRaiseErrorEvent_IfTypeOfExtendedTimeOutIsNotInteger()
        {
            string errorMessage = null;
            _reader.ReadingError += message => { errorMessage = message; };

            var configuration = _reader.Get(new Dictionary<string, object>
            {
                { "extendedTimeOut", "abc" }
            });

            Assert.Equal(1000L, configuration.ExtendedTimeOut);
            Assert.NotNull(errorMessage);
            Assert.Contains("extendedTimeOut", errorMessage);
        }

        [Theory]
        [InlineData("fadeIn", ToastrShowMethods.FadeIn)]
        public void Get_ShouldSetShowMethod(string value, ToastrShowMethods expected)
        {
            var configuration = _reader.Get(new Dictionary<string, object>
            {
                { "showMethod", value }
            });

            Assert.Equal(expected, configuration.ShowMethod);
        }

        [Fact]
        public void Get_ShouldRaiseErrorEvent_IfTypeOfShowMethodIsNotString()
        {
            string errorMessage = null;
            _reader.ReadingError += message => { errorMessage = message; };

            var configuration = _reader.Get(new Dictionary<string, object>
            {
                { "showMethod", 3 }
            });

            Assert.Equal(ToastrShowMethods.FadeIn, configuration.ShowMethod);
            Assert.NotNull(errorMessage);
            Assert.Contains("showMethod", errorMessage);
        }

        [Fact]
        public void Get_ShouldRaiseErrorEvent_IfShowMethodIsUnknown()
        {
            string errorMessage = null;
            _reader.ReadingError += message => { errorMessage = message; };

            var configuration = _reader.Get(new Dictionary<string, object>
            {
                { "showMethod", "something" }
            });

            Assert.Equal(ToastrShowMethods.FadeIn, configuration.ShowMethod);
            Assert.NotNull(errorMessage);
            Assert.Contains("something", errorMessage);
        }

        [Theory]
        [InlineData(555)]
        [InlineData(555L)]
        public void Get_ShouldSetShowDuration(object value)
        {
            var configuration = _reader.Get(new Dictionary<string, object>
            {
                { "showDuration", value }
            });

            Assert.Equal(555L, configuration.ShowDuration);
        }

        [Fact]
        public void Get_ShouldRaiseErrorEvent_IfTypeOfShowDurationIsNotInteger()
        {
            string errorMessage = null;
            _reader.ReadingError += message => { errorMessage = message; };

            var configuration = _reader.Get(new Dictionary<string, object>
            {
                { "showDuration", "abc" }
            });

            Assert.Equal(300L, configuration.ShowDuration);
            Assert.NotNull(errorMessage);
            Assert.Contains("showDuration", errorMessage);
        }

        [Theory]
        [InlineData("swing", ToastrEasings.Swing)]
        [InlineData("linear", ToastrEasings.Linear)]
        public void Get_ShouldSetShowEasing(string value, ToastrEasings expected)
        {
            var configuration = _reader.Get(new Dictionary<string, object>
            {
                { "showEasing", value }
            });

            Assert.Equal(expected, configuration.ShowEasing);
        }

        [Fact]
        public void Get_ShouldRaiseErrorEvent_IfTypeOfShowEasingIsNotString()
        {
            string errorMessage = null;
            _reader.ReadingError += message => { errorMessage = message; };

            var configuration = _reader.Get(new Dictionary<string, object>
            {
                { "showEasing", 3 }
            });

            Assert.Equal(ToastrEasings.Linear, configuration.ShowEasing);
            Assert.NotNull(errorMessage);
            Assert.Contains("showEasing", errorMessage);
        }

        [Fact]
        public void Get_ShouldRaiseErrorEvent_IfShowEasingIsUnknown()
        {
            string errorMessage = null;
            _reader.ReadingError += message => { errorMessage = message; };

            var configuration = _reader.Get(new Dictionary<string, object>
            {
                { "showEasing", "something" }
            });

            Assert.Equal(ToastrEasings.Linear, configuration.ShowEasing);
            Assert.NotNull(errorMessage);
            Assert.Contains("something", errorMessage);
        }

        [Theory]
        [InlineData("hideMethod", "fadeOut", ToastrHideMethods.FadeOut)]
        [InlineData("closeMethod", "fadeOut", ToastrHideMethods.FadeOut)]
        public void Get_ShouldSetHideMethod(string key, string value, ToastrHideMethods expected)
        {
            var configuration = _reader.Get(new Dictionary<string, object>
            {
                { key, value }
            });

            Assert.Equal(expected, configuration.HideMethod);
        }

        [Theory]
        [InlineData("hideMethod")]
        [InlineData("closeMethod")]
        public void Get_ShouldRaiseErrorEvent_IfTypeOfHideMethodIsNotString(string key)
        {
            string errorMessage = null;
            _reader.ReadingError += message => { errorMessage = message; };

            var configuration = _reader.Get(new Dictionary<string, object>
            {
                { key, 3 }
            });

            Assert.Equal(ToastrHideMethods.FadeOut, configuration.HideMethod);
            Assert.NotNull(errorMessage);
            Assert.Contains(key, errorMessage);
        }

        [Theory]
        [InlineData("hideMethod")]
        [InlineData("closeMethod")]
        public void Get_ShouldRaiseErrorEvent_IfHideMethodIsUnknown(string key)
        {
            string errorMessage = null;
            _reader.ReadingError += message => { errorMessage = message; };

            var configuration = _reader.Get(new Dictionary<string, object>
            {
                { key, "something" }
            });

            Assert.Equal(ToastrHideMethods.FadeOut, configuration.HideMethod);
            Assert.NotNull(errorMessage);
            Assert.Contains("something", errorMessage);
        }

        [Theory]
        [InlineData("hideDuration", 555)]
        [InlineData("closeDuration", 555)]
        [InlineData("hideDuration", 555L)]
        [InlineData("closeDuration", 555L)]
        public void Get_ShouldSetHideDuration(string key, object value)
        {
            var configuration = _reader.Get(new Dictionary<string, object>
            {
                { key, value }
            });

            Assert.Equal(555L, configuration.HideDuration);
        }

        [Theory]
        [InlineData("hideDuration")]
        [InlineData("closeDuration")]
        public void Get_ShouldRaiseErrorEvent_IfTypeOfHideDurationIsNotInteger(string key)
        {
            string errorMessage = null;
            _reader.ReadingError += message => { errorMessage = message; };

            var configuration = _reader.Get(new Dictionary<string, object>
            {
                { key, "abc" }
            });

            Assert.Equal(1000L, configuration.HideDuration);
            Assert.NotNull(errorMessage);
            Assert.Contains(key, errorMessage);
        }

        [Theory]
        [InlineData("hideEasing", "swing", ToastrEasings.Swing)]
        [InlineData("closeEasing", "swing", ToastrEasings.Swing)]
        [InlineData("hideEasing", "linear", ToastrEasings.Linear)]
        [InlineData("closeEasing", "linear", ToastrEasings.Linear)]
        public void Get_ShouldSetHideEasing(string key, string value, ToastrEasings expected)
        {
            var configuration = _reader.Get(new Dictionary<string, object>
            {
                { key, value }
            });

            Assert.Equal(expected, configuration.HideEasing);
        }

        [Theory]
        [InlineData("hideEasing")]
        [InlineData("closeEasing")]
        public void Get_ShouldRaiseErrorEvent_IfTypeOfHideEasingIsNotString(string key)
        {
            string errorMessage = null;
            _reader.ReadingError += message => { errorMessage = message; };

            var configuration = _reader.Get(new Dictionary<string, object>
            {
                { key, 3 }
            });

            Assert.Equal(ToastrEasings.Linear, configuration.HideEasing);
            Assert.NotNull(errorMessage);
            Assert.Contains(key, errorMessage);
        }

        [Theory]
        [InlineData("hideEasing")]
        [InlineData("closeEasing")]
        public void Get_ShouldRaiseErrorEvent_IfHideEasingIsUnknown(string key)
        {
            string errorMessage = null;
            _reader.ReadingError += message => { errorMessage = message; };

            var configuration = _reader.Get(new Dictionary<string, object>
            {
                { key, "something" }
            });

            Assert.Equal(ToastrEasings.Linear, configuration.HideEasing);
            Assert.NotNull(errorMessage);
            Assert.Contains("something", errorMessage);
        }

        [Theory]
        [InlineData(500)]
        [InlineData(500L)]
        [InlineData(500U)]
        public void Get_ShouldSetWidth(object value)
        {
            var configuration = _reader.Get(new Dictionary<string, object>
            {
                { "width", value }
            });

            Assert.Equal(500U, configuration.Width);
        }

        [Fact]
        public void Get_ShouldRaiseErrorEvent_IfTypeOfWidthIsNotInteger()
        {
            string errorMessage = null;
            _reader.ReadingError += message => { errorMessage = message; };

            var configuration = _reader.Get(new Dictionary<string, object>
            {
                { "width", "hello" }
            });

            Assert.Equal(300U, configuration.Width);
            Assert.NotNull(errorMessage);
            Assert.Contains("width", errorMessage);
        }

        [Theory]
        [InlineData(50)]
        [InlineData(50L)]
        [InlineData(50U)]
        public void Get_ShouldSetHorizontalOffset(object value)
        {
            var configuration = _reader.Get(new Dictionary<string, object>
            {
                { "hOffset", value }
            });

            Assert.Equal(50U, configuration.HorizontalOffset);
        }

        [Fact]
        public void Get_ShouldRaiseErrorEvent_IfTypeOfHorizontalOffsetIsNotInteger()
        {
            string errorMessage = null;
            _reader.ReadingError += message => { errorMessage = message; };

            var configuration = _reader.Get(new Dictionary<string, object>
            {
                { "hOffset", "hello" }
            });

            Assert.Equal(20U, configuration.HorizontalOffset);
            Assert.NotNull(errorMessage);
            Assert.Contains("hOffset", errorMessage);
        }

        [Theory]
        [InlineData(50)]
        [InlineData(50L)]
        [InlineData(50U)]
        public void Get_ShouldSetVerticalOffset(object value)
        {
            var configuration = _reader.Get(new Dictionary<string, object>
            {
                { "vOffset", value }
            });

            Assert.Equal(50U, configuration.VerticalOffset);
        }

        [Fact]
        public void Get_ShouldRaiseErrorEvent_IfTypeOfVerticalOffsetIsNotInteger()
        {
            string errorMessage = null;
            _reader.ReadingError += message => { errorMessage = message; };

            var configuration = _reader.Get(new Dictionary<string, object>
            {
                { "vOffset", "hello" }
            });

            Assert.Equal(20U, configuration.VerticalOffset);
            Assert.NotNull(errorMessage);
            Assert.Contains("vOffset", errorMessage);
        }
    }
}