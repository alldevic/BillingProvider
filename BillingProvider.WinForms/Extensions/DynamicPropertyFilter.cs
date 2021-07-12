using System;
using System.ComponentModel;

namespace BillingProvider.WinForms.Extensions
{
    /// <summary>
    /// Атрибут для поддержки динамически показываемых свойств
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = true)]
    class DynamicPropertyFilterAttribute : Attribute
    {
        string _propertyName;

        /// <summary>
        /// Название свойства, от которого будет зависить видимость
        /// </summary>
        public string PropertyName => _propertyName;

        string _showOn;

        /// <summary>
        /// Значения свойства, от которого зависит видимость (через запятую, если несколько), при котором свойство, к
        /// которому применен атрибут, будет видимо.
        /// </summary>
        public string ShowOn => _showOn;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="propertyName">
        /// Название свойства, от которого будет зависеть видимость
        /// </param>
        /// <param name="value">Значения свойства (через запятую, если несколько),
        /// при котором свойство, к которому применен атрибут, будет видимо.
        /// </param>
        public DynamicPropertyFilterAttribute(string propertyName, string value)
        {
            _propertyName = propertyName;
            _showOn = value;
        }
    }

    /// <summary>
    /// Базовый класс для объектов, поддерживающих динамическое
    /// отображение свойств в PropertyGrid
    /// </summary>
    public class FilterablePropertyBase : ICustomTypeDescriptor
    {
        protected PropertyDescriptorCollection
            GetFilteredProperties(Attribute[] attributes)
        {
            var pdc = TypeDescriptor.GetProperties(this, attributes, true);

            var finalProps = new PropertyDescriptorCollection(new PropertyDescriptor[0]);

            foreach (PropertyDescriptor pd in pdc)
            {
                var include = false;
                var dynamic = false;

                foreach (Attribute a in pd.Attributes)
                {
                    if (!(a is DynamicPropertyFilterAttribute dpf))
                    {
                        continue;
                    }

                    dynamic = true;

                    var temp = pdc[dpf.PropertyName];

                    if (dpf.ShowOn.IndexOf(temp.GetValue(this)?.ToString() ?? string.Empty, StringComparison.Ordinal) >
                        -1)
                    {
                        include = true;
                    }
                }

                if (!dynamic || include)
                {
                    finalProps.Add(pd);
                }
            }

            return finalProps;
        }

        #region ICustomTypeDescriptor Members

        public TypeConverter GetConverter() => TypeDescriptor.GetConverter(this, true);

        public EventDescriptorCollection GetEvents(Attribute[] attributes) =>
            TypeDescriptor.GetEvents(this, attributes, true);

        EventDescriptorCollection ICustomTypeDescriptor.GetEvents() => TypeDescriptor.GetEvents(this, true);

        public string GetComponentName() => TypeDescriptor.GetComponentName(this, true);

        public object GetPropertyOwner(PropertyDescriptor pd) => this;

        public AttributeCollection GetAttributes() => TypeDescriptor.GetAttributes(this, true);

        public PropertyDescriptorCollection GetProperties(Attribute[] attributes) => GetFilteredProperties(attributes);

        PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties() => GetFilteredProperties(new Attribute[0]);

        public object GetEditor(Type editorBaseType) => TypeDescriptor.GetEditor(this, editorBaseType, true);

        public PropertyDescriptor GetDefaultProperty() => TypeDescriptor.GetDefaultProperty(this, true);

        public EventDescriptor GetDefaultEvent() => TypeDescriptor.GetDefaultEvent(this, true);

        public string GetClassName() => TypeDescriptor.GetClassName(this, true);

        #endregion
    }
}