﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DataStructureDummyFactory.designer.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// <auto-generated>
//   Sourced from NuGet package OBeautifulCode.Build.Conventions.VisualStudioProjectTemplates.Domain.Test (1.1.136)
// </auto-generated>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.DataStructure.Test
{
    using global::System;

    using FakeItEasy;

    /// <summary>
    /// DO NOT EDIT.  
    /// THIS CLASS EXISTS SO THAT THE DUMMY FACTORY CAN INHERIT FROM IT AND THE PROJECT CAN COMPILE.
    /// THIS WILL BE REPLACED BY A CODE GENERATED DEFAULT DUMMY FACTORY.
    /// </summary>
    [global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [global::System.CodeDom.Compiler.GeneratedCode("OBeautifulCode.Build.Conventions.VisualStudioProjectTemplates.Domain.Test", "1.1.136")]
#if !OBeautifulCodeDataStructureSolution
    internal
#else
    public
#endif
    abstract class DefaultDataStructureDummyFactory : IDummyFactory
    {
        /// <inheritdoc />
        public Priority Priority => new FakeItEasy.Priority(1);

        /// <inheritdoc />
        public bool CanCreate(Type type)
        {
            return false;
        }

        /// <inheritdoc />
        public object Create(Type type)
        {
            return null;
        }
    }
}