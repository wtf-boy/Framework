﻿namespace WTF.Power.Entity
{
    using System;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;

    [Serializable, DataContract(IsReference=true), EdmComplexType(NamespaceName="ModuleModel", Name="Sys_GetUserPowerModule_Result")]
    public class Sys_GetUserPowerModule_Result : ComplexObject
    {
    }
}

