﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SerializationConfigurationTypes.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// <auto-generated>
//   Sourced from NuGet package OBeautifulCode.Build.Conventions.VisualStudioProjectTemplates.Domain.Test (1.1.136)
// </auto-generated>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure.Test
{
    using System;
    using System.CodeDom.Compiler;
    using System.Diagnostics.CodeAnalysis;

    using OBeautifulCode.Serialization.Bson;
    using OBeautifulCode.Serialization.Json;

    using OBeautifulCode.DataStructure.Serialization.Bson;
    using OBeautifulCode.DataStructure.Serialization.Json;

    [ExcludeFromCodeCoverage]
    [GeneratedCode("OBeautifulCode.Build.Conventions.VisualStudioProjectTemplates.Domain.Test", "1.1.136")]
    public static class SerializationConfigurationTypes
    {
        public static BsonSerializationConfigurationType BsonSerializationConfigurationType => typeof(DataStructureBsonSerializationConfiguration).ToBsonSerializationConfigurationType();

        public static JsonSerializationConfigurationType JsonSerializationConfigurationType => typeof(DataStructureJsonSerializationConfiguration).ToJsonSerializationConfigurationType();
    }
}