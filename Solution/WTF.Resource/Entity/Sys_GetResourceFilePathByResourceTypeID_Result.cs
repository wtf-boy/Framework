namespace WTF.Resource.Entity
{
    using System;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;

    [Serializable, DataContract(IsReference=true), EdmComplexType(NamespaceName="ResourceModel", Name="Sys_GetResourceFilePathByResourceTypeID_Result")]
    public class Sys_GetResourceFilePathByResourceTypeID_Result : ComplexObject
    {
    }
}

