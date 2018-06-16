using System.Globalization;

namespace DerECoach.Util.Holiday.Services
{
    internal interface ILocalizationService
    {
        /// <summary>
        /// Get the localized description of a configuration by the path
        /// </summary>
        /// <param name="hierarchyPath"></param>
        /// <param name="defaultValue"></param>
        /// <returns>the default value if no corresponding resources is found</returns>
        string GetHierarchyDescription(string hierarchyPath, string defaultValue);

        /// <summary>
        /// Get the localized description of a holiday
        /// </summary>
        /// <param name="descriptionKey"></param>
        /// <returns>if found the localized description of a holiday, otherwise the descriptionKey is returned</returns>
        string GetHolidayDescription(string descriptionKey);

        /// <summary>
        /// Get the localized description of a Christian holiday
        /// </summary>
        /// <param name="descriptionKey"></param>
        /// <returns>if found the localized description of a holiday, otherwise the descriptionKey is returned</returns>
        string GetChristianHolidayDescription(string descriptionKey);

        /// <summary>
        /// Get the localized description of an Ethiopian Ortodox holiday
        /// </summary>
        /// <param name="descriptionKey"></param>
        /// <returns>if found the localized description of a holiday, otherwise the descriptionKey is returned</returns>
        string GetEthiopianOrthodoxHolidayDescription(string descriptionKey);

        /// <summary>
        /// Get the localized description of an Islamic holiday
        /// </summary>
        /// <param name="descriptionKey"></param>
        /// <returns>if found the localized description of a holiday, otherwise the descriptionKey is returned</returns>
        string GetIslamicHolidayDescription(string descriptionKey);

        /// <summary>
        /// Set the current culture info
        /// </summary>
        /// <param name="cultureInfo"></param>
        void SetCurrentCulture(CultureInfo cultureInfo);
    }
}