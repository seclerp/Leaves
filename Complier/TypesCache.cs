using System;
using System.Collections.Generic;
using Mono.Cecil;

namespace LeafS.Complier
{
    public class TypesCache
    {
        private ModuleDefinition _currentModule;
        private Dictionary<string, TypeDefinition> _importedTypes;
        private List<ModuleDefinition> _loadedModules;

        public TypesCache(ModuleDefinition moduleDefinition)
        {
            _currentModule = moduleDefinition;
        }

        public TypeDefinition this[string key]
        {
            get
            {
                if (!_importedTypes.ContainsKey(key))
                {
                    foreach (var moduleDefinition in _loadedModules)
                    foreach (var moduleDefinitionType in moduleDefinition.Types)
                        if (moduleDefinitionType.FullName == key)
                        {
                            _importedTypes.Add(key, moduleDefinitionType);
                            return _importedTypes[key];
                        }
                    throw new Exception("Type not found in any loaded assemblies: " + key);
                }
                return _importedTypes[key];
            }

            set => _importedTypes[key] = value;
        }

        public void LoadAssembly(string path)
        {
            _loadedModules.Add(ModuleDefinition.ReadModule(path));
        }
    }
}