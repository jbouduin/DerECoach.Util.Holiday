using System.Globalization;
using System.Resources;

namespace DerECoach.Util.Holiday.Services
{
    internal class LocalizationService : ILocalizationService
    {
        #region fields --------------------------------------------------------
        private CultureInfo _cultureInfo;
        private ResourceManager _resourceManager;
        #endregion

        #region properties ----------------------------------------------------
        internal ResourceManager ResourceManager
        {
            get
            {
                if (ReferenceEquals(_resourceManager, null))
                {
                    var temp = new ResourceManager(@"DerECoach.Util.Holiday.Resources.Descriptions", typeof(LocalizationService).Assembly);
                    _resourceManager = temp;
                }
                return _resourceManager;
            }
        }
        #endregion

        #region ILocalizationService members ----------------------------------
        public string GetHierarchyDescription(string hierarchyPath, string defaultValue)
        {
            var descriptionKey = string.Format(@"country_description_{0}", hierarchyPath.Replace('/', '_'));
            return ResourceManager.GetString(descriptionKey, _cultureInfo) ?? defaultValue;
        }

        public string GetHolidayDescription(string descriptionKey)
        {
            var resourceKey = string.Format(@"holiday_description_{0}", descriptionKey);
            return ResourceManager.GetString(resourceKey, _cultureInfo) ?? descriptionKey;
        }

        public string GetChristianHolidayDescription(string descriptionKey)
        {
            var resourceKey = string.Format(@"holiday_description_christian_{0}", descriptionKey);
            return ResourceManager.GetString(resourceKey, _cultureInfo) ?? descriptionKey;
        }

        public string GetEthiopianOrthodoxHolidayDescription(string descriptionKey)
        {
            var resourceKey = string.Format(@"holiday_description_ethiopian_orthodox_{0}", descriptionKey);
            return ResourceManager.GetString(resourceKey, _cultureInfo) ?? descriptionKey;
        }

        public string GetIslamicHolidayDescription(string descriptionKey)
        {
            var resourceKey = string.Format(@"holiday_description_islamic_{0}", descriptionKey);
            return ResourceManager.GetString(resourceKey, _cultureInfo) ?? descriptionKey;
        }

        public void SetCurrentCulture(CultureInfo cultureInfo)
        {
            _cultureInfo = cultureInfo;
        }
        #endregion

        #region constructor ---------------------------------------------------

        public LocalizationService(CultureInfo cultureInfo = null)
        {
            _cultureInfo = cultureInfo;
        }
        #endregion
        
    }
}
