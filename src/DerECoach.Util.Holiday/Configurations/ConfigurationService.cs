using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using DerECoach.Util.Holiday.Services;

namespace DerECoach.Util.Holiday.Configurations
{

    internal interface IConfigurationService
    {
        IEnumerable<ILocation> GetSupportedLocations(CultureInfo cultureInfo);
        Dictionary<string,Holidays> GetHolidays(string hierarchyPath);
    }

    /// <summary>
    /// The configuration provider
    /// </summary>
    internal class ConfigurationService: IConfigurationService
    {
        #region fields --------------------------------------------------------
        private readonly Dictionary<string, Configuration> _configurations = new Dictionary<string, Configuration>();
        private readonly ILocalizationService _localizationService;
        #endregion

        #region constructor ---------------------------------------------------

        public ConfigurationService(ILocalizationService localizationService)
        {
            _localizationService = localizationService;
            LoadHierarchies();
        }

        private void LoadHierarchies()
        {
            Directory.GetFiles(@".\\Data", "*.xml").ToList().ForEach(dataFile =>
            {
                try
                {
                    var configuration = Configuration.LoadFromFile(dataFile);
                    _configurations.Add(configuration.hierarchy, configuration);
                }
                // ReSharper disable EmptyGeneralCatchClause
                catch (Exception)
                {
                    // do nothing, the file is invalid anyway
                }
                // ReSharper restore EmptyGeneralCatchClause
            });
            
        }


        #endregion

        #region ILocationService members --------------------------------------
        public IEnumerable<ILocation> GetSupportedLocations(CultureInfo cultureInfo)
        {
            _localizationService.SetCurrentCulture(cultureInfo);

            var supportedLocations =  _configurations.Values.Select(configuration =>
            {
                var description = _localizationService.GetHierarchyDescription(configuration.hierarchy, configuration.description);
                var result = Location.CreateRootLocation(configuration.hierarchy, description);
                ProcessHierarchy(result, configuration);
                return result;
            });
            return supportedLocations;
        }

        public Dictionary<string,Holidays> GetHolidays(string hierarchyPath)
        {
            var splittedPath = hierarchyPath.Split(new[] {'/'});
            var result = new Dictionary<string, Holidays>();
            if (splittedPath.Length == 0)
                throw new ArgumentException("hierarchyPath");

            var configuration = _configurations[splittedPath[0]];
            var currentPath = splittedPath[0];
            result.Add(currentPath, configuration.Holidays);

            for (var i = 1; i < splittedPath.Length; i++)
            {
                currentPath = string.Format(@"{0}/{1}", currentPath, splittedPath[i]);
                configuration = configuration[splittedPath[i]];
                result.Add(currentPath, configuration.Holidays);
            }

            return result;
        }

        #endregion

        #region helper methods ------------------------------------------------
        private void ProcessHierarchy(Location location, Configuration configuration)
        {
            configuration.SubConfigurations.ForEach(sub =>
            {
                var description =
                    _localizationService.GetHierarchyDescription(
                        string.Format(@"{0}/{1}", location.Path, sub.hierarchy), sub.description);
                var subLocation = location.AddChild(sub.hierarchy, description);
                ProcessHierarchy(subLocation, sub);
            });
        }
        #endregion
        
    }
}
