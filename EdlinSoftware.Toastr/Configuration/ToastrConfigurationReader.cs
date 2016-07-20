using System;
using System.Collections.Generic;
using CSharpFunctionalExtensions;

namespace EdlinSoftware.Toastr.Configuration
{
    public class ToastrConfigurationReader
    {
        public event Action<string> ReadingError;

        public IToastrConfiguration Get(IDictionary<string, object> configurationDictionary)
        {
            configurationDictionary = configurationDictionary ?? new Dictionary<string, object>();

            var configuration = new ToastrConfiguration
            {
                CloseButton = GetValue(configurationDictionary, "closeButton", false),
                NewestOnTop = GetValue(configurationDictionary, "newestOnTop", false),
                ProgressBar = GetValue(configurationDictionary, "progressBar", false),
                PositionClass = GetPositionClass(GetValue(configurationDictionary, "positionClass", "toast-top-center")),
                PreventDuplicates = GetValue(configurationDictionary, "preventDuplicates", false),
                TimeOut = GetValue(configurationDictionary, "timeOut", 5000L, Convert.ToInt64),
                ExtendedTimeOut = GetValue(configurationDictionary, "extendedTimeOut", 1000L, Convert.ToInt64),
                ShowMethod = GetShowMethod(GetValue(configurationDictionary, "showMethod", "fadeIn")),
                ShowDuration = GetValue(configurationDictionary, "showDuration", 300L, Convert.ToInt64),
                ShowEasing = GetEasing(GetValue(configurationDictionary, "showEasing", "linear")),
                HideMethod = GetHideMethod(GetValue(configurationDictionary, new[] { "hideMethod", "closeMethod" }, "fadeOut")),
                HideDuration = GetValue(configurationDictionary, new[] { "hideDuration", "closeDuration" }, 1000L, Convert.ToInt64),
                HideEasing = GetEasing(GetValue(configurationDictionary, new[] { "hideEasing", "closeEasing" }, "linear")),
                Width = GetValue(configurationDictionary, "width", 300U, Convert.ToUInt32),
                HorizontalOffset = GetValue(configurationDictionary, "hOffset", 20U, Convert.ToUInt32),
                VerticalOffset = GetValue(configurationDictionary, "vOffset", 20U, Convert.ToUInt32)
            };

            return configuration;
        }

        private ToastrHideMethods GetHideMethod(string value)
        {
            value = value ?? "";

            switch (value.ToLowerInvariant())
            {
                case "fadeout":
                    return ToastrHideMethods.FadeOut;
                default:
                    ReadingError?.Invoke($"Unknown value '{value}' for hide method.");
                    return ToastrHideMethods.FadeOut;
            }
        }

        private ToastrEasings GetEasing(string value)
        {
            value = value ?? "";

            switch (value.ToLowerInvariant())
            {
                case "swing":
                    return ToastrEasings.Swing;
                case "linear":
                    return ToastrEasings.Linear;
                default:
                    ReadingError?.Invoke($"Unknown value '{value}' for easing.");
                    return ToastrEasings.Linear;
            }
        }

        private ToastrShowMethods GetShowMethod(string value)
        {
            value = value ?? "";

            switch (value.ToLowerInvariant())
            {
                case "fadein":
                    return ToastrShowMethods.FadeIn;
                default:
                    ReadingError?.Invoke($"Unknown value '{value}' for show method.");
                    return ToastrShowMethods.FadeIn;
            }
        }

        private ToastrPositions GetPositionClass(string value)
        {
            value = value ?? "";

            switch (value.ToLowerInvariant())
            {
                case "toast-top-right":
                    return ToastrPositions.TopRight;
                case "toast-top-left":
                    return ToastrPositions.TopLeft;
                case "toast-top-full-width":
                    return ToastrPositions.TopFullWidth;
                case "toast-top-center":
                    return ToastrPositions.TopCenter;
                case "toast-bottom-right":
                    return ToastrPositions.BottomRight;
                case "toast-bottom-left":
                    return ToastrPositions.BottomLeft;
                case "toast-bottom-full-width":
                    return ToastrPositions.BottomFullWidth;
                case "toast-bottom-center":
                    return ToastrPositions.BottomCenter;
                default:
                    ReadingError?.Invoke($"Unknown value '{value}' for position class.");
                    return ToastrPositions.TopCenter;
            }
        }
        private T GetValue<T>(
            IDictionary<string, object> configurationDictionary,
            string[] keys,
            T defaultValue,
            Func<object, T> converter = null)
        {
            foreach (var key in keys)
            {
                if (!configurationDictionary.ContainsKey(key))
                    continue;

                var value = configurationDictionary[key];

                if (converter != null)
                {
                    var conversionResult = TryConvert(value, converter);

                    if (conversionResult.IsSuccess)
                    {
                        return conversionResult.Value;
                    }

                    ReadingError?.Invoke($"Key '{key}' contains value of incorrect type '{value?.GetType()}'. Type '{typeof(T)}' is expected.");
                }
                else
                {
                    if (value is T)
                        return (T)value;

                    ReadingError?.Invoke($"Key '{key}' contains value of incorrect type '{value?.GetType()}'. Type '{typeof(T)}' is expected.");
                }
            }

            return defaultValue;
        }

        private Result<T> TryConvert<T>(object value, Func<object, T> converter)
        {
            try
            {
                return Result.Ok(converter(value));
            }
            catch (Exception)
            {
                return Result.Fail<T>($"Unable to convert '{value}' to type '{typeof(T)}'");
            }
        }

        private T GetValue<T>(
            IDictionary<string, object> configurationDictionary,
            string key,
            T defaultValue,
            Func<object, T> converter = null)
        {
            return GetValue(configurationDictionary, new[] { key }, defaultValue, converter);
        }
    }
}