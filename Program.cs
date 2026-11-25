using System;
using System.Collections.Generic;

namespace GerenciadorDeTarefas
{
    public class Tarefa
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public bool Concluida { get; set; }

        public Tarefa(int id, string descricao)
        {
            Id = id;
            Descricao = descricao;
            Concluida = false;
        }

        public string MostrarSituacao()
        {
            return Concluida ? "Concluída" : "Pendente";
        }
    }

    class Program
    {
        static List<Tarefa> tarefas = new List<Tarefa>();
        static Stack<int> historicoConclusoes = new Stack<int>();
        static int proximoId = 1;

        static void Main(string[] args)
        {
            int opcao = 0;

            while (opcao != 6)
            {
                Console.Clear();
                Console.WriteLine("================================");
                Console.WriteLine("     GERENCIADOR DE TAREFAS");
                Console.WriteLine("================================");
                Console.WriteLine("1 - Cadastrar tarefa");
                Console.WriteLine("2 - Listar tarefas");
                Console.WriteLine("3 - Buscar tarefa");
                Console.WriteLine("4 - Concluir tarefa");
                Console.WriteLine("5 - Desfazer última conclusão");
                Console.WriteLine("6 - Sair");
                Console.WriteLine("--------------------------------");
                Console.Write("Escolha uma opção: ");

                if (!int.TryParse(Console.ReadLine(), out opcao))
                {
                    Console.WriteLine("\nDigite uma opção numérica válida.");
                    Pausar();
                    continue;
                }

                switch (opcao)
                {
                    case 1: CadastrarTarefa(); break;
                    case 2: ListarTarefas(); break;
                    case 3: BuscarTarefa(); break;
                    case 4: ConcluirTarefa(); break;
                    case 5: DesfazerConclusao(); break;
                    case 6: Console.WriteLine("\nPrograma encerrado. Até mais!"); break;
                    default:
                        Console.WriteLine("\nOpção inválida. Escolha de 1 a 6.");
                        Pausar();
                        break;
                }
            }
        }

        static void CadastrarTarefa()
        {
            Console.Clear();
            Console.WriteLine("=== CADASTRAR TAREFA ===");
            Console.Write("Digite a descrição da tarefa: ");
            string descricao = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(descricao))
            {
                Console.WriteLine("\nA descrição não pode ficar vazia.");
                Pausar();
                return;
            }

            Tarefa novaTarefa = new Tarefa(proximoId, descricao.Trim());
            tarefas.Add(novaTarefa);

            Console.WriteLine("\nTarefa cadastrada com sucesso!");
            Console.WriteLine("Identificador: " + novaTarefa.Id);
            proximoId++;
            Pausar();
        }

        static void ListarTarefas()
        {
            Console.Clear();
            Console.WriteLine("=== LISTA DE TAREFAS ===");

            if (tarefas.Count == 0)
            {
                Console.WriteLine("Ainda não existem tarefas cadastradas.");
                Pausar();
                return;
            }

            foreach (Tarefa tarefa in tarefas)
            {
                Console.WriteLine("--------------------------------");
                Console.WriteLine("ID: " + tarefa.Id);
                Console.WriteLine("Descrição: " + tarefa.Descricao);
                Console.WriteLine("Situação: " + tarefa.MostrarSituacao());
            }

            Console.WriteLine("--------------------------------");
            Console.WriteLine("Total de tarefas: " + tarefas.Count);
            Pausar();
        }

        static void BuscarTarefa()
        {
            Console.Clear();
            Console.WriteLine("=== BUSCAR TAREFA ===");

            int id;
            if (!LerIdentificador(out id))
            {
                Pausar();
                return;
            }

            Tarefa tarefa = EncontrarTarefa(id);
            if (tarefa == null)
                Console.WriteLine("\nNão foi encontrada uma tarefa com esse ID.");
            else
            {
                Console.WriteLine("\nTarefa encontrada:");
                Console.WriteLine("ID: " + tarefa.Id);
                Console.WriteLine("Descrição: " + tarefa.Descricao);
                Console.WriteLine("Situação: " + tarefa.MostrarSituacao());
            }

            Pausar();
        }

        static void ConcluirTarefa()
        {
            Console.Clear();
            Console.WriteLine("=== CONCLUIR TAREFA ===");

            int id;
            if (!LerIdentificador(out id))
            {
                Pausar();
                return;
            }

            Tarefa tarefa = EncontrarTarefa(id);
            if (tarefa == null)
                Console.WriteLine("\nNão foi encontrada uma tarefa com esse ID.");
            else if (tarefa.Concluida)
                Console.WriteLine("\nEssa tarefa já está concluída.");
            else
            {
                tarefa.Concluida = true;
                historicoConclusoes.Push(tarefa.Id);
                Console.WriteLine("\nTarefa marcada como concluída!");
            }

            Pausar();
        }

        static void DesfazerConclusao()
        {
            Console.Clear();
            Console.WriteLine("=== DESFAZER ÚLTIMA CONCLUSÃO ===");

            if (historicoConclusoes.Count == 0)
            {
                Console.WriteLine("Não existe nenhuma conclusão para desfazer.");
                Pausar();
                return;
            }

            int id = historicoConclusoes.Pop();
            Tarefa tarefa = EncontrarTarefa(id);

            if (tarefa != null)
            {
                tarefa.Concluida = false;
                Console.WriteLine("A conclusão da tarefa " + id + " foi desfeita.");
                Console.WriteLine("Ela voltou para a situação pendente.");
            }
            else
                Console.WriteLine("Não foi possível localizar a tarefa do histórico.");

            Pausar();
        }

        static Tarefa EncontrarTarefa(int id)
        {
            foreach (Tarefa tarefa in tarefas)
            {
                if (tarefa.Id == id)
                    return tarefa;
            }

            return null;
        }

        static bool LerIdentificador(out int id)
        {
            Console.Write("Digite o ID da tarefa: ");
            if (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("\nO identificador precisa ser um número inteiro.");
                return false;
            }

            return true;
        }

        static void Pausar()
        {
            Console.WriteLine("\nPressione ENTER para continuar...");
            Console.ReadLine();
        }
    }
}
