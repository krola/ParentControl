using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace ParentControl.Service.Configuration
{
    public class ParentControlConfiguration : System.Configuration.ConfigurationSection
    {
        private static string ConfigurationSectionName = "ParentControl";

        /// <summary>
        /// Returns an shiConfiguration instance
        /// </summary>
        public static ParentControlConfiguration GetConfig()
        {

            return (ParentControlConfiguration)System.Configuration.ConfigurationManager.
               GetSection(ParentControlConfiguration.ConfigurationSectionName) ??
               new ParentControlConfiguration();

        }

        [System.Configuration.ConfigurationProperty("Validators")]
        public ValidatorConfigSectionElementCollection Validators
        {
            get
            {
                return (ValidatorConfigSectionElementCollection)this["Validators"] ??
                   new ValidatorConfigSectionElementCollection();
            }
        }

        [System.Configuration.ConfigurationProperty("Initializers")]
        public InitializersConfigSectionElementCollection Processes
        {
            get
            {
                return (InitializersConfigSectionElementCollection)this["Initializers"] ??
                   new InitializersConfigSectionElementCollection();
            }
        }
    }

    [ConfigurationCollection(typeof(TypeElement))]
    public class ValidatorConfigSectionElementCollection : ConfigurationElementCollection
    {
        internal const string PropertyName = "Validator";

        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.BasicMapAlternate;
            }
        }
        protected override string ElementName
        {
            get
            {
                return PropertyName;
            }
        }

        protected override bool IsElementName(string elementName)
        {
            return elementName.Equals(PropertyName,
              StringComparison.InvariantCultureIgnoreCase);
        }

        public override bool IsReadOnly()
        {
            return false;
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new TypeElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((TypeElement)(element)).Type;
        }

        public TypeElement this[int idx]
        {
            get { return (TypeElement)BaseGet(idx); }
        }
    }

    [ConfigurationCollection(typeof(TypeElement))]
    public class InitializersConfigSectionElementCollection : ConfigurationElementCollection
    {
        internal const string PropertyName = "Initializer";

        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.BasicMapAlternate;
            }
        }
        protected override string ElementName
        {
            get
            {
                return PropertyName;
            }
        }

        protected override bool IsElementName(string elementName)
        {
            return elementName.Equals(PropertyName,
              StringComparison.InvariantCultureIgnoreCase);
        }

        public override bool IsReadOnly()
        {
            return false;
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new TypeElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((TypeElement)(element)).Type;
        }

        public TypeElement this[int idx]
        {
            get { return (TypeElement)BaseGet(idx); }
        }
    }

    public class TypeElement : ConfigurationSection
    {
        [ConfigurationProperty("type", IsRequired = true)]
        public string Type
        {
            get { return (string)this["type"]; }
            set { this["type"] = value; }
        }
    }
}
