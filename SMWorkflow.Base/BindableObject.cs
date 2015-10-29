// -------------------------------------------------------------------------
// <copyright file="BindableObject.cs" company="AGI, Novomatic Group.">
//     Copyright © 2015 Stanley Omoregie. All Rights Reserved.
// </copyright>
// -------------------------------------------------------------------------
namespace SMWorkflow.Base
{
    using System;
    using System.ComponentModel;
    using System.Linq.Expressions;
    using System.Reflection;

    public abstract class BindableObject : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged( string propertyName )
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        protected void OnPropertyChanged<T>( Expression<Func<T>> propertyExpression )
        {
            var propertyName = this.ExtractPropertyName(propertyExpression);
            this.RaisePropertyChanged(propertyName);
        }

        private string ExtractPropertyName<T>( Expression<Func<T>> propertyExpression )
        {
            if (propertyExpression == null)
            {
                throw new ArgumentNullException(nameof(propertyExpression));
            }
            var memberExpression = propertyExpression.Body as MemberExpression;
            if (memberExpression == null)
            {
                throw new ArgumentException("propertyExpression");
            }
            var propertyInfo = memberExpression.Member as PropertyInfo;
            if (propertyInfo == null)
            {
                throw new ArgumentException("propertyExpression");
            }
            var getMethod = propertyInfo.GetMethod;
            if (getMethod.IsStatic)
            {
                throw new ArgumentException("propertyExpression");
            }
            return memberExpression.Member.Name;
        }
    }
}
