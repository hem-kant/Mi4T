using System;
using System.Configuration;

namespace MI4T.Common.Configuration
{
    public class ConfigurationSectionHandler : IConfigurationSectionHandler
    {
        #region Public instance constructors

        /// <summary>
        /// Default class constructor.
        /// </summary>
        public ConfigurationSectionHandler()
        {
        }

        #endregion

        #region Implementation of IConfigurationSectionHandler methods

        /// <summary>
        /// Returns the XML node 
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="configContext"></param>
        /// <param name="section"></param>
        /// <returns></returns>
        public object Create(object parent, object configContext, System.Xml.XmlNode section)
        {
            return section;
        }

        #endregion
    }
}
