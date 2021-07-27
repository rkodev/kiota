using System;
using System.Linq;
using Kiota.Builder.Extensions;

namespace Kiota.Builder.Refiners {
    public class RubyRefiner : CommonLanguageRefiner, ILanguageRefiner
    {
        public RubyRefiner(GenerationConfiguration configuration) : base(configuration) {}
        public override void Refine(CodeNamespace generatedCode)
        {
            AddPropertiesAndMethodTypesImports(generatedCode, false, false, false);
            AddParsableInheritanceForModelClasses(generatedCode);
            AddInheritedAndMethodTypesImports(generatedCode);
            AddDefaultImports(generatedCode, defaultNamespaces, defaultNamespacesForModels, defaultNamespacesForRequestBuilders, defaultSymbolsForApiClient);
            AddGetterAndSetterMethods(generatedCode, new() {
                                                    CodePropertyKind.Custom,
                                                    CodePropertyKind.AdditionalData,
                                                    CodePropertyKind.BackingStore,
                                                }, _configuration.UsesBackingStore, true);
            ReplaceReservedNames(generatedCode, new RubyReservedNamesProvider(), x => $"{x}_escaped");
            ReplaceRelativeImportsByImportPath(generatedCode, '.');
            AddNamespaceModuleImports(generatedCode , _configuration.ClientNamespaceName);
            FixReferencesToEntityType(generatedCode);
            FixInheritedEntityType(generatedCode, null, _configuration.ClientNamespaceName.NormalizeNameSpaceName("::") + "::Users::");
        }
        private static readonly Tuple<string, string>[] defaultNamespacesForRequestBuilders = new Tuple<string, string>[] { 
            new ("HttpCore", "microsoft_kiota_abstractions"),
            new ("HttpMethod", "microsoft_kiota_abstractions"),
            new ("RequestInfo", "microsoft_kiota_abstractions"),
            new ("ResponseHandler", "microsoft_kiota_abstractions"),
            new ("QueryParametersBase", "microsoft_kiota_abstractions"),
            new ("SerializationWriterFactory", "microsoft_kiota_abstractions"),
        };
        private static readonly Tuple<string, string>[] defaultNamespaces = new Tuple<string, string>[] { 
            new ("SerializationWriter", "microsoft_kiota_abstractions"),
        };
        private static readonly Tuple<string, string>[] defaultNamespacesForModels = new Tuple<string, string>[] { 
            new ("ParseNode", "microsoft_kiota_abstractions"),
            new ("Parsable", "microsoft_kiota_abstractions"),
        };
        private static readonly Tuple<string, string>[] defaultSymbolsForApiClient = new Tuple<string, string>[] { 
            new ("ApiClientBuilder", "microsoft_kiota_abstractions"),
            new ("SerializationWriterFactoryRegistry", "microsoft_kiota_abstractions"),
            new ("ParseNodeFactoryRegistry", "microsoft_kiota_abstractions"),
        };
        private static void AddParsableInheritanceForModelClasses(CodeElement currentElement) {
            if(currentElement is CodeClass currentClass && currentClass.IsOfKind(CodeClassKind.Model) 
                && currentClass.StartBlock is CodeClass.Declaration declaration) {
                declaration.Implements.Add(new CodeType(currentClass) {
                    IsExternal = true,
                    Name = $"MicrosoftKiotaAbstractions::Parsable",
                });
            }
            CrawlTree(currentElement, AddParsableInheritanceForModelClasses);
        }
        protected static void AddInheritedAndMethodTypesImports(CodeElement currentElement) {
            if(currentElement is CodeClass currentClass && currentClass.IsOfKind(CodeClassKind.Model) 
                && currentClass.StartBlock is CodeClass.Declaration declaration && declaration.Inherits != null) {
                currentClass.AddUsing(new CodeUsing(currentElement) { Name = declaration.Inherits.Name, Declaration = declaration.Inherits});
            }
            CrawlTree(currentElement, (x) => AddInheritedAndMethodTypesImports(x));
        }

        protected static void FixInheritedEntityType(CodeElement currentElement, CodeClass entityClass = null, string prefix = ""){
            if(currentElement is CodeClass currentClass && currentClass.IsOfKind(CodeClassKind.Model) 
                && currentClass.StartBlock is CodeClass.Declaration declaration && declaration.Inherits != null 
                && "entity".Equals(declaration.Inherits.Name, StringComparison.OrdinalIgnoreCase)) {
                declaration.Inherits.Name = prefix + declaration.Inherits.Name.ToFirstCharacterUpperCase();
            }
            CrawlTree(currentElement, (c) => FixInheritedEntityType(c, entityClass, prefix));
        }
        protected void AddNamespaceModuleImports(CodeElement current, String clientNamespaceName) {
            if(current is CodeClass currentClass) {
                var Module = currentClass.GetImmediateParentOfType<CodeNamespace>();
                if(!String.IsNullOrEmpty(Module.Name)){
                    var modulesProperties = Module.Name.Replace(clientNamespaceName+".", string.Empty).Split(".");
                    for (int i = modulesProperties.Length - 1; i >= 0; i--){
                        var prefix = String.Concat(Enumerable.Repeat("../", modulesProperties.Length -i-1));
                        var nUsing = new CodeUsing(Module) { 
                            Name = modulesProperties[i].ToSnakeCase(), 
                            Declaration = new CodeType(Module) {
                                IsExternal = false,
                            }
                        };
                        nUsing.Declaration.Name = $"{(string.IsNullOrEmpty(prefix) ? "./" + prefix : string.Empty )}{nUsing.Name}";
                        currentClass.AddUsing(nUsing);
                    }
                }
            }
            CrawlTree(current, c => AddNamespaceModuleImports(c, clientNamespaceName));
        }
    }
}
