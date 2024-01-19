using CRM.Common.Enums;
using System;

namespace CRM.WebFramework.Filters
{
    public class MenuAttribute : Attribute
    {
        private readonly eMenu eMenu;
        private readonly int order;
        public MenuAttribute(eMenu eMenu, int order)
        {
            this.eMenu = eMenu;
            this.order = order;
        }
        public virtual eMenu Menu
        {
            get { return eMenu; }
        }
        public virtual int Order
        {
            get { return order; }
        }
    }
}