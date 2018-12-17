using System;
using System.Collections.Generic;
using System.Configuration;

namespace wslyvh.Core.Interfaces.Configuration
{
    public interface IConfigurationSource
    {
        /// <summary>
        /// Determines whether this <see cref="IConfigurationSource" /> contains the specified <paramref name="sectionName" />.
        /// </summary>
        /// <param name="sectionName">The name of the <see cref="ConfigurationSection" /> within this <see cref="IConfigurationSource" />.</param>
        /// <returns><c>true</c> if this <see cref="IConfigurationSource" /> contains the specified <paramref name="sectionName" /> otherwise; <c>false</c>.</returns>
        /// <exception cref="System.ArgumentNullException">The <paramref name="sectionName" /> parameter is <c>null</c>.</exception>
        /// <exception cref="System.ArgumentException">The <paramref name="sectionName" /> parameter is an empty string.</exception>
        bool ContainsSection(string sectionName);

        /// <summary>
        /// Determines whether this <see cref="IConfigurationSource" /> contains the specified <typeparamref name="TSection" />.
        /// The provided <typeparamref name="TSection" /> must be a <see cref="ConfigurationSection" />
        /// </summary>
        /// <typeparam name="TSection">The type of the <see cref="ConfigurationSection" />.</typeparam>
        /// <param name="sectionName">The name of the <see cref="ConfigurationSection" /> within this <see cref="IConfigurationSource" />.</param>
        /// <returns><c>true</c> if this <see cref="IConfigurationSource" /> contains the specified <typeparamref name="TSection" /> otherwise; <c>false</c>.</returns>
        /// <exception cref="System.ArgumentNullException">The <paramref name="sectionName" /> parameter is <c>null</c>.</exception>
        /// <exception cref="System.ArgumentException">The <paramref name="sectionName" /> parameter is an empty string.</exception>
        bool ContainsSection<TSection>(string sectionName) where TSection : ConfigurationSection;

        /// <summary>
        /// Gets the specified <paramref name="sectionName" /> from this <see cref="IConfigurationSource" /> .
        /// </summary>
        /// <param name="sectionName">The name of the <see cref="ConfigurationSection" /> within this <see cref="IConfigurationSource" />.</param>
        /// <returns>An instance of <see cref="ConfigurationSection" /> if the specified <paramref name="sectionName" /> exist within this <see cref="IConfigurationSource" /> otherwise; <c>null</c>.</returns>
        /// <exception cref="System.ArgumentNullException">The <paramref name="sectionName" /> parameter is <c>null</c>.</exception>
        /// <exception cref="System.ArgumentException">The <paramref name="sectionName" /> parameter is an empty string.</exception>
        ConfigurationSection GetSection(string sectionName);

        /// <summary>
        /// Gets the specified <typeparamref name="TSection" /> which matches the specified <paramref name="sectionName" />.
        /// The provided <typeparamref name="TSection" /> must be a <see cref="ConfigurationSection" /> located in this <see cref="IConfigurationSource" /> instance.
        /// </summary>
        /// <typeparam name="TSection">The type of the <see cref="ConfigurationSection" />.</typeparam>
        /// <param name="sectionName">The name of the <see cref="ConfigurationSection" /> within this <see cref="IConfigurationSource" />.</param>
        /// <returns>An instance of <typeparamref name="TSection" /> if the specified <paramref name="sectionName" /> exist within this <see cref="IConfigurationSource" /> otherwise; <c>null</c>.</returns>
        /// <exception cref="System.ArgumentNullException">The <paramref name="sectionName" /> parameter is <c>null</c>.</exception>
        /// <exception cref="System.ArgumentException">The <paramref name="sectionName" /> parameter is an empty string.</exception>
        TSection GetSection<TSection>(string sectionName) where TSection : ConfigurationSection;

        /// <summary>
        /// Gets a sequence of all <see cref="ConfigurationSection" /> of type <paramref name="sectionType" /> defined in this <see cref="IConfigurationSource" />.
        /// The provided <paramref name="sectionType" /> must be a <see cref="ConfigurationSection" />.
        /// </summary>
        /// <param name="sectionType">The type of the <see cref="ConfigurationSection" />.</param>
        /// <returns>A sequence of <see cref="ConfigurationSection" /> of type <paramref name="sectionType" /> if any exist within this <see cref="IConfigurationSource" /> otherwise; an empty sequence.</returns>
        /// <exception cref="System.ArgumentNullException">The <paramref name="sectionType" /> parameter is <c>null</c>.</exception>
        /// <exception cref="System.ArgumentException">The <paramref name="sectionType" /> parameter is not assignable to <see cref="ConfigurationSection" />.</exception>
        IEnumerable<ConfigurationSection> GetSections(Type sectionType);

        /// <summary>
        /// Gets a sequence of all <typeparamref name="TSection" /> defined in this <see cref="IConfigurationSource" />.
        /// The provided <typeparamref name="TSection" /> must be a <see cref="ConfigurationSection" />.
        /// </summary>
        /// <typeparam name="TSection">The type of the <see cref="ConfigurationSection" />.</typeparam>
        /// <returns>A sequence of <typeparamref name="TSection" /> if any exist within this <see cref="IConfigurationSource" /> otherwise; an empty sequence.</returns>
        IEnumerable<TSection> GetSections<TSection>() where TSection : ConfigurationSection;
    }
}
