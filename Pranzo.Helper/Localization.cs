using System.Globalization;
using System.Threading;

namespace Lazeez.Helper
{
    public class Localization
    {
        public static void SetCulture(Enums.Culture culture)
        {
            string cultureName = culture == Enums.Culture.Ar ? "ar-EG" : "en-US";

            // Modify current thread's cultures   
            var cultureInfo = new CultureInfo(cultureName)
            {
                DateTimeFormat =
                {
                    ShortDatePattern = "dd/MM/yyyy",
                    LongTimePattern = "HH:mm"
                }
            };

            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;

            // Set current language
            if (GlobalSettings.CurrentCulture != culture)
                GlobalSettings.CurrentCulture = culture;
        }
    }
}