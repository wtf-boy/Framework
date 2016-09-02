namespace WTF.Resource.Entity
{
    using System;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;

    [Serializable, EdmComplexType(NamespaceName="ResourceModel", Name="Sys_GetResourceVerByResourceTypeID_Result"), DataContract(IsReference=true)]
    public class Sys_GetResourceVerByResourceTypeID_Result : ComplexObject
    {
    }
}

