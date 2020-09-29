using ScriptGenerator.services;
using System;

namespace ScriptGenerator
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("--------------ScriptGenerator------------------");
            Console.WriteLine("Qual operacao Seguir? ");
            Console.WriteLine("1 - InserirSucessos ");
            Console.WriteLine("2 - GerarCorrecaoCRZ");
            int opcao = int.Parse(Console.ReadLine());
            switch (opcao)
            {
                case 1:
                    InsertSucesso();
                    break;
                case 2:
                    GerarUpdateTotalizadores();
                    break;
                default:
                    Console.WriteLine("É preciso escolher algumas das opções");
                    break;
            }

        }

        private static void GerarUpdateTotalizadores()
        {
            Console.WriteLine("\n\n");
            Console.Write("Qual o banco: ");
            var dbClient = Console.ReadLine();
            Console.Write("Id do totalizador: ");
            var tgId = int.Parse(Console.ReadLine());
            var Services = new TotalizadoresFileServices(dbClient);

            var resp = Services.Handle(tgId);

            if (resp.IsSucess)
            {
                Console.WriteLine($"\n\nArquivo gerado em: {resp.FilesPath}");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Houve algum erro ao gerar arquivos");
                Console.ReadKey();
            }
        }

        public static void InsertSucesso()
        {
            Console.Write("\n Qual o banco: ");
            var dbClient = Console.ReadLine();
            Console.Write("Ids das reducoes: ");
            var stringIds = Console.ReadLine();
            var idsRz = stringIds.Split(',');
            Console.WriteLine("Confirma gerar script das reducoes abaixo?");
            foreach (var id in idsRz)
            {
                Console.WriteLine($"Reducao id: {id}");
            }
            Console.Write("\n Sim(S) --- Não(N): ");
            var decision = Console.ReadLine();
            if (decision.ToUpper() == "N")
            {
                Environment.Exit(0);
            }

            var services = new RzInfoFileServices(dbClient);

            var resp = services.Handle(idsRz);

            if (resp.IsSucess)
            {
                Console.WriteLine($"\n\nArquivo gerado em: {resp.FilesPath}");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Houve algum erro ao gerar arquivos");
                Console.ReadKey();
            }
        }
    }
}