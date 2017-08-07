using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Mono.Cecil;
using Mono.Cecil.Cil;

namespace LeafS.Complier
{
    public class Compiler
    {
        public void Compile(string outputFileName, params string[] inputFileNames)
        {
            foreach (var inputFileName in inputFileNames)
            {
                var inputCode = File.ReadAllText(inputFileName);
                var lexer = new Lexer.Lexer();
                var parser = new Parser.Parser();

                lexer.InitializeTokenDefinitions();
                var tokens = lexer.Tokenize(inputCode).ToArray();
                var statements = parser.Parse(tokens);

                var name = new AssemblyNameDefinition("Leafs Application", new Version(1, 0, 0, 0));
                var asm = AssemblyDefinition.CreateAssembly(name, outputFileName, ModuleKind.Console);

                // импортируем в библиотеку типы string и void
                var stringImport = asm.MainModule.Import(typeof(String));
                var voidImport = asm.MainModule.Import(typeof(void));
                var type = new TypeDefinition("LeafSTest", "Program", TypeAttributes.AutoClass | TypeAttributes.Public | TypeAttributes.AnsiClass | TypeAttributes.BeforeFieldInit, asm.MainModule.Import(typeof(object)));
                var method = new MethodDefinition("Main", MethodAttributes.Static | MethodAttributes.Private | MethodAttributes.HideBySig, voidImport);
                // сохраняем короткую ссылку на генератор кода
                var ip = method.Body.GetILProcessor();

                var mainContext = new Context
                {
                    Processor = ip,
                    Module = new ModuleContext
                    {
                        Types = new TypesCache(asm.MainModule),
                        Def = asm.MainModule
                    },
                    TypeDef = type,
                    Method = new MethodContext
                    {
                        Def = method,
                        NameMappings = new Dictionary<string, int>(),
                        VariablesCount = 0
                    }
                };

                foreach (var statement in statements)
                    statement.Emit(mainContext);

                ip.Emit(OpCodes.Ret);

                // добавляем тип в сборку
                asm.MainModule.Types.Add(type);
                // привязываем метод к типу
                type.Methods.Add(method);

                // указываем точку входа для исполняемого файла
                asm.EntryPoint = method;

                // сохраняем сборку на диск
                asm.Write(outputFileName);
            }
        }
    }
}