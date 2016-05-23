using MI4T.Common.Configuration.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MI4T.Common.Configuration.Type
{
    /// <summary>
    /// Type handler for the <em>System.Boolean</em> data-type.
    /// </summary>
    public class BooleanTypeHandler : ITypeHandler
    {
        /// <summary>
        /// Default class constructor.
        /// </summary>
        public BooleanTypeHandler()
        {
        }

        #region Implementation of ITypeHandler methods

        /// <summary>
        /// Converts a string to its equivalent <em>System.Boolean</em> value.
        /// </summary>
        /// <param name="propertyValue">String to be converted.</param>
        /// <returns>Boolean equivalent of the string value.</returns>
        public Object ToObject(String propertyValue)
        {
            if (propertyValue == null)
            {
                throw new ArgumentNullException("propertyValue",
                                                "The parameter [propertyValue] is null.  Cannot convert to a Boolean.");
            }

            return Boolean.Parse(propertyValue);
        }

        /// <summary>
        /// Converts a <em>System.Boolean</em> value to its string equivalent.
        /// </summary>
        /// <param name="propertyValue">Boolean to be converted.</param>
        /// <returns>String equivalent of the Boolean value.</returns>
        public String ToString(Object propertyValue)
        {
            if (propertyValue == null)
            {
                throw new ArgumentNullException("propertyValue",
                                                "The parameter [propertyValue] is null.  Cannot convert to a Boolean.");
            }

            return propertyValue.ToString();
        }

        #endregion
    }
}
