using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HoHUtilities.SimpleIoCContainer.SimpleResolver
{
    public class SimpleDependencyResolverConfigurationManager
    {
        //TODO use new way of getting config out to hook up all the possible settings

//        public class IoCAppSettingsManager : IConfigurationSectionHandler
//        {
//            private Dictionary<string, string> iocSettings = new Dictionary<string, string>();
//
//
//            public object Create(object parent, object configContext, XmlNode section)
//            {
                //Get each item node
//                foreach (XmlNode node in section.ChildNodes)
//                {
//                    if (!node.Name.Equals("item", StringComparison.OrdinalIgnoreCase))
//                        throw new SettingsPropertyWrongTypeException(String.Format(CultureInfo.InvariantCulture, "Unkown XmlNode name '{0}', path is probably incorrect: '{1}'", node.Name, section.BaseURI));
//
//                    string typeval = null;
//                    string implementationval = null;
//                    foreach (XmlNode itemNode in node.ChildNodes)
//                    {
//                        switch (itemNode.Name.Trim().ToUpperInvariant())
//                        {
//                            case "TYPE":
//                                if (!itemNode.InnerText.IsNullOrEmptyOrJustSpaces())
//                                    typeval = itemNode.InnerText.Trim();
//                                break;
//                            case "IMPLEMENTATION":
//                                if (!itemNode.InnerText.IsNullOrEmptyOrJustSpaces())
//                                    implementationval = itemNode.InnerText.Trim();
//                                break;
//                            default:
//                                throw new ArgumentException(String.Format(CultureInfo.InvariantCulture, "Unknown XmlNode inside item Node: {0}", itemNode.Name));
//                        }
//                    }
//
//                    if (typeval == null || implementationval == null)
//                        throw new SettingsPropertyNotFoundException("Type or Implementation was null during IoC AppSettings loading.");
//
//                    iocSettings.Add(typeval, implementationval);
//                }
//
//                return iocSettings;
//            }
//        }

    }
}
