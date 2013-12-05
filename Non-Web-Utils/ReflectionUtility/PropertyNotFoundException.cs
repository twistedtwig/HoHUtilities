using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace HoHUtilities.ReflectionUtility
{
    [Serializable]
    public class PropertyNotFoundException : Exception
    {
        private String ObjTypeName;
        private String PropName;

        protected PropertyNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            if (info != null)
            {
                ObjTypeName = info.GetString("objname");
                PropName = info.GetString("propName");
            }
        }
        public PropertyNotFoundException(String message, String objTypeName, String propName) : this(message, null, objTypeName, propName) { }
        public PropertyNotFoundException(String message, Exception inner, String objTypeName, String propName)
            : base(message, inner)
        {
            this.ObjTypeName = objTypeName;
            this.PropName = propName;
        }

        public String ObjectNameWhosPropertyNotFound
        {
            get { return ObjTypeName; }
            set { ObjTypeName = value; }
        }

        public String NameOfPropertyNotFound
        {
            get { return PropName; }
            set { PropName = value; }
        }

        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("objname", ObjTypeName);
            info.AddValue("propName", PropName);
        }
    }
}
