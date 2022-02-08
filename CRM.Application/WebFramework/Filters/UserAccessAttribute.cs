using Common.Enums;
using CRM.Domain.Common.Enums;
using System;

namespace CRM.Application.WebFramework.Filters
{
    public class UserAccessAttribute : Attribute
    {
        private readonly eAccessControl _userAccessControl;
        private readonly eAccessType _userAccessType;
        private readonly int _orderIndex;
        private readonly bool _anonymouseAccess;
        public UserAccessAttribute(eAccessControl userAccessControl, eAccessType userAccessType, int orderIndex, bool anonymouseAccess = false)
        {
            _userAccessControl = userAccessControl;
            _userAccessType = userAccessType;
            _orderIndex = orderIndex;
            _anonymouseAccess = anonymouseAccess;
        }
        public virtual eAccessControl UserAccessControl
        {
            get { return _userAccessControl; }
        }
        public virtual eAccessType UserAccessType
        {
            get { return _userAccessType; }
        }
        public virtual int OrderIndex
        {
            get { return _orderIndex; }
        }
        public virtual bool AnonymousAccess
        {
            get { return _anonymouseAccess; }
        }
    }
}