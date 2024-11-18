// <auto-generated/>
#pragma warning disable CS0618
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using System.Collections.Generic;
using System.IO;
using System;
namespace Kiota.Builder.SearchProviders.GitHub.GitHubClient.Models
{
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    #pragma warning disable CS1591
    public partial class Repositories : IAdditionalDataHolder, IParsable
    #pragma warning restore CS1591
    {
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>The fragment property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Fragment { get; set; }
#nullable restore
#else
        public string Fragment { get; set; }
#endif
        /// <summary>The matches property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::Kiota.Builder.SearchProviders.GitHub.GitHubClient.Models.Repositories_matches>? Matches { get; set; }
#nullable restore
#else
        public List<global::Kiota.Builder.SearchProviders.GitHub.GitHubClient.Models.Repositories_matches> Matches { get; set; }
#endif
        /// <summary>The object_type property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? ObjectType { get; set; }
#nullable restore
#else
        public string ObjectType { get; set; }
#endif
        /// <summary>The object_url property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? ObjectUrl { get; set; }
#nullable restore
#else
        public string ObjectUrl { get; set; }
#endif
        /// <summary>The property property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Property { get; set; }
#nullable restore
#else
        public string Property { get; set; }
#endif
        /// <summary>
        /// Instantiates a new <see cref="global::Kiota.Builder.SearchProviders.GitHub.GitHubClient.Models.Repositories"/> and sets the default values.
        /// </summary>
        public Repositories()
        {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::Kiota.Builder.SearchProviders.GitHub.GitHubClient.Models.Repositories"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static global::Kiota.Builder.SearchProviders.GitHub.GitHubClient.Models.Repositories CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::Kiota.Builder.SearchProviders.GitHub.GitHubClient.Models.Repositories();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>
            {
                { "fragment", n => { Fragment = n.GetStringValue(); } },
                { "matches", n => { Matches = n.GetCollectionOfObjectValues<global::Kiota.Builder.SearchProviders.GitHub.GitHubClient.Models.Repositories_matches>(global::Kiota.Builder.SearchProviders.GitHub.GitHubClient.Models.Repositories_matches.CreateFromDiscriminatorValue)?.AsList(); } },
                { "object_type", n => { ObjectType = n.GetStringValue(); } },
                { "object_url", n => { ObjectUrl = n.GetStringValue(); } },
                { "property", n => { Property = n.GetStringValue(); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer)
        {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteStringValue("fragment", Fragment);
            writer.WriteCollectionOfObjectValues<global::Kiota.Builder.SearchProviders.GitHub.GitHubClient.Models.Repositories_matches>("matches", Matches);
            writer.WriteStringValue("object_type", ObjectType);
            writer.WriteStringValue("object_url", ObjectUrl);
            writer.WriteStringValue("property", Property);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
#pragma warning restore CS0618
