using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerCare.Model
{
    public abstract class BaseEntity : INotifyPropertyChanged
    {
        String _lastUpdatedBy = "system";
        public String LastUpdatedBy
        {
            get => _lastUpdatedBy;
            set
            {
                if (_lastUpdatedBy != value)
                {
                    _lastUpdatedBy = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        DateTime _lastUpdated = DateTime.UtcNow;
        public DateTime LastUpdated
        {
            get => _lastUpdated;
            set
            {
                if (_lastUpdated != value)
                {
                    _lastUpdated = value;
                    this.NotifyPropertyChanged();
                }
            }
        }


        public DateTime Created { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// set a value with reflection. automatically updates metadata (such as: who edited what and when)
        /// </summary>
        /// <param name="pIssuer">name of entity issuing the change of a value</param>
        /// <param name="pTargetProperty">the property to change</param>
        /// <param name="pNewvalue">the new value for the target property</param>
        public void ChangeValue(String pIssuer, String pTargetProperty, object pNewvalue)
        {
            //check input values
            if (String.IsNullOrWhiteSpace(pIssuer))
                throw new ArgumentNullException("issuer cannot be null or whitespace");
            if(String.IsNullOrWhiteSpace(pTargetProperty))
                throw new ArgumentNullException("target property cannot be null or whitespace");
            if(pNewvalue == null)
                throw new ArgumentNullException("a new value must be provided");

            //find property
            var property = this.GetType().GetProperty(pTargetProperty);
            if (property == null)
                throw new ArgumentException($"property {pTargetProperty} not found");
            var targetType = IsNullableType(property.PropertyType) ? Nullable.GetUnderlyingType(property.PropertyType) : property.PropertyType;

            //check for value compability
            if(targetType != pNewvalue.GetType())
                throw new ArgumentException($"provided new value is not of type {targetType}");

            //apply new value
            property.SetValue(this, Convert.ChangeType(pNewvalue, targetType));
            this.LastUpdatedBy = pIssuer;
            this.LastUpdated = DateTime.UtcNow;            
        }

        //http://technico.qnownow.com/how-to-set-value-to-nullable-type-properties-using-reflection-in-c/
        private static bool IsNullableType(Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition().Equals(typeof(Nullable<>));
        }

        #region INotifyPropertyChanged
        /// <summary>
        /// INotifyPropertyChanged event
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// wrapper method for INotifyPropertyChanged
        /// </summary>
        /// <param name="propertyName">property that changed. gets auto-filled if left empty</param>
        protected virtual void NotifyPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] String propertyName = "")
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
        }

        #endregion
    }
}
