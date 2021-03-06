// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AssemblyExtensions.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// <auto-generated>
//   Sourced from NuGet package. Will be overwritten with package update except in OBeautifulCode.Type.Recipes source.
// </auto-generated>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type.Recipes
{
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Linq;
    using global::System.Reflection;

    /// <summary>
    /// Type-related extension methods on type <see cref="AssemblyExtensions"/>.
    /// </summary>
#if !OBeautifulCodeTypeSolution
    [global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [global::System.CodeDom.Compiler.GeneratedCode("OBeautifulCode.Type.Recipes", "See package version number")]
    internal
#else
    public
#endif
    static class AssemblyExtensions
    {
        /// <summary>
        /// Gets the enum types defined in the specified assembly.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        /// <returns>
        /// The enum types defined in the specified assembly.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="assembly"/> is null.</exception>
        public static IReadOnlyCollection<Type> GetEnumTypes(
            this Assembly assembly)
        {
            if (assembly == null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            var result = assembly.GetTypes().Where(_ => _.IsEnum).ToList();

            return result;
        }

        /// <summary>
        /// Gets the public enum types defined in the specified assembly.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        /// <returns>
        /// The public enum types defined in the specified assembly.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="assembly"/> is null.</exception>
        public static IReadOnlyCollection<Type> GetPublicEnumTypes(
            this Assembly assembly)
        {
            if (assembly == null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            var result = assembly.GetEnumTypes().Where(_ => _.IsPublic).ToList();

            return result;
        }

        /// <summary>
        /// Gets the interface types defined in the specified assembly.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        /// <returns>
        /// The interface types defined in the specified assembly.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="assembly"/> is null.</exception>
        public static IReadOnlyCollection<Type> GetInterfaceTypes(
            this Assembly assembly)
        {
            if (assembly == null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            var result = assembly.GetTypes().Where(_ => _.IsInterface).ToList();

            return result;
        }

        /// <summary>
        /// Gets the public interface types defined in the specified assembly.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        /// <returns>
        /// The public interface types defined in the specified assembly.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="assembly"/> is null.</exception>
        public static IReadOnlyCollection<Type> GetPublicInterfaceTypes(
            this Assembly assembly)
        {
            if (assembly == null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            var result = assembly.GetInterfaceTypes().Where(_ => _.IsPublic).ToList();

            return result;
        }
    }
}
