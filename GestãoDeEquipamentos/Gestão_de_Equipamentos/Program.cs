using System;

namespace Gestao_de_Equipamentos_ConsoleApp
{
    class Program
    {     
        const int CAPACIDADE_REGISTROS = 100;

        #region Equipamentos
        static int[] idsEquipamento = new int[CAPACIDADE_REGISTROS];

        static string[] nomesEquipamento = new string[CAPACIDADE_REGISTROS];
        static double[] precosEquipamento = new double[CAPACIDADE_REGISTROS];
        static string[] numerosSerieEquipamento = new string[CAPACIDADE_REGISTROS];
        static DateTime[] datasFabricacaoEquipamento = new DateTime[CAPACIDADE_REGISTROS];
        static string[] fabricantesEquipamento = new string[CAPACIDADE_REGISTROS];

        private static int IdEquipamento;
        #endregion

        #region Chamados
        static int[] idsChamado = new int[CAPACIDADE_REGISTROS];

        static string[] titulosChamado = new string[CAPACIDADE_REGISTROS];
        static string[] descricoesChamado = new string[CAPACIDADE_REGISTROS];
        static int[] idsEquipamentoChamado = new int[CAPACIDADE_REGISTROS];
        static DateTime[] datasAberturaChamado = new DateTime[CAPACIDADE_REGISTROS];

        private static int IdChamado;
        #endregion

        static void Main(string[] args)
        {
            PopularAplicacao(9);

            while (true)
            {
                string opcao = ObterOpcao();

                if (opcao.Equals("s", StringComparison.OrdinalIgnoreCase))
                    break;

                if (opcao == "1")
                {
                    string opcaoCadastroEquipamentos = ObterOpcaoCadastroEquipamentos();

                    if (opcaoCadastroEquipamentos.Equals("s", StringComparison.OrdinalIgnoreCase))
                        break;

                    if (opcaoCadastroEquipamentos == "1")
                        RegistrarEquipamento(0);

                    else if (opcaoCadastroEquipamentos == "2")
                        VisualizarEquipamentos();

                    else if (opcaoCadastroEquipamentos == "3")
                        EditarEquipamento();

                    else if (opcaoCadastroEquipamentos == "4")
                        ExcluirEquipamento();
                }
                else if (opcao == "2")
                {
                    string opcaoControleChamados = ObterOpcaoControleChamados();

                    if (opcaoControleChamados.Equals("s", StringComparison.OrdinalIgnoreCase))
                        break;

                    if (opcaoControleChamados == "1")
                        RegistrarChamado(0);

                    else if (opcaoControleChamados == "2")
                        VisualizarChamados();

                    else if (opcaoControleChamados == "3")
                        EditarChamado();

                    else if (opcaoControleChamados == "4")
                        ExcluirChamado();

                }

                Console.Clear();
            }
        }

        #region Controle de Chamados
        private static void ExcluirChamado()
        {
            Console.Clear();

            VisualizarChamados();

            Console.WriteLine();

            Console.Write("Digite o número do chamado que deseja excluir: ");
            int idSelecionado = Convert.ToInt32(Console.ReadLine());

            for (int i = 0; i < idsEquipamento.Length; i++)
            {
                if (idsChamado[i] == idSelecionado)
                {
                    idsChamado[i] = 0;
                    idsEquipamentoChamado[i] = 0;
                    titulosChamado[i] = null;
                    descricoesChamado[i] = null;
                    datasAberturaChamado[i] = DateTime.MinValue;

                    break;
                }
            }
        }

        private static void EditarChamado()
        {
            Console.Clear();

            VisualizarChamados();

            Console.WriteLine();

            Console.Write("Digite o número do chamado que deseja editar: ");
            int idSelecionado = Convert.ToInt32(Console.ReadLine());

            RegistrarChamado(idSelecionado);
        }

        private static void VisualizarChamados()
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine("{0,-10} | {1,-30} | {2,-55} | {3,-25}", "Id", "Equipamento", "Título", "Dias em Aberto");

            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------");

            Console.ResetColor();

            int numeroChamadosRegistrados = 0;

            for (int indiceChamados = 0; indiceChamados < idsChamado.Length; indiceChamados++)
            {
                if (idsChamado[indiceChamados] > 0)
                {
                    string nomeEquipamento = "";

                    for (int indiceEquipamentos = 0; indiceEquipamentos < idsEquipamento.Length; indiceEquipamentos++)
                    {
                        if (idsEquipamento[indiceEquipamentos] == idsEquipamentoChamado[indiceChamados])
                        {
                            nomeEquipamento = nomesEquipamento[indiceEquipamentos];
                            break;
                        }
                    }

                    TimeSpan diasEmAberto = DateTime.Now - datasAberturaChamado[indiceChamados];

                    Console.Write("{0,-10} | {1,-30} | {2,-55} | {3,-25}",
                       idsChamado[indiceChamados],
                       nomeEquipamento,
                       titulosChamado[indiceChamados],
                       diasEmAberto.ToString("dd"));

                    Console.WriteLine();

                    numeroChamadosRegistrados++;
                }
            }

            if (numeroChamadosRegistrados == 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine("Nenhum chamado registrado!");
                Console.ResetColor();
            }

            Console.ReadLine();
        }

        private static void RegistrarChamado(int idChamadoSelecionado)
        {
            Console.Clear();

            int posicao = ObterPosicaoParaChamados(idChamadoSelecionado);

            VisualizarEquipamentos();

            Console.Write("Digite o Id do equipamento para manutenção: ");
            int idEquipamentoChamado = Convert.ToInt32(Console.ReadLine());

            Console.Write("Digite o titulo do chamado: ");
            string titulo = Console.ReadLine();

            Console.Write("Digite a descricao do chamado: ");
            string descricao = Console.ReadLine();

            Console.Write("Digite a data de abertura do chamado: ");
            DateTime dataFabricacao = Convert.ToDateTime(Console.ReadLine());

            idsChamado[posicao] = IdChamado;
            idsEquipamentoChamado[posicao] = idEquipamentoChamado;
            titulosChamado[posicao] = titulo;
            descricoesChamado[posicao] = descricao;

        }

        private static int ObterPosicaoParaChamados(int idChamadoSelecionado)
        {
            int posicao = 0;

            for (int i = 0; i < idsChamado.Length; i++)
            {
                if (idChamadoSelecionado == 0 && idsChamado[i] == 0) 
                {
                    IdChamado++;
                    posicao = i;
                    break;
                }
                else if (idChamadoSelecionado == idsChamado[i]) 
                {
                    posicao = i;
                    break;
                }
            }

            return posicao;
        }

        private static string ObterOpcaoControleChamados()
        {
            Console.WriteLine("Digite 1 para inserir novo chamadp");
            Console.WriteLine("Digite 2 para visualizar chamados");
            Console.WriteLine("Digite 3 para editar um chamado");
            Console.WriteLine("Digite 4 para excluir um chamado");

            Console.WriteLine("Digite S para sair");

            string opcao = Console.ReadLine();

            return opcao;
        }

        #endregion

        #region Cadastro de Equipamentos
        private static string ObterOpcaoCadastroEquipamentos()
        {
            Console.WriteLine("Digite 1 para inserir novo equipamento");
            Console.WriteLine("Digite 2 para visualizar equipamentos");
            Console.WriteLine("Digite 3 para editar um equipamento");
            Console.WriteLine("Digite 4 para excluir um equipamento");

            Console.WriteLine("Digite S para sair");

            string opcao = Console.ReadLine();

            return opcao;
        }

        private static void EditarEquipamento()
        {
            Console.Clear();

            VisualizarEquipamentos();

            Console.WriteLine();

            Console.Write("Digite o número do equipamento que deseja editar: ");
            int idSelecionado = Convert.ToInt32(Console.ReadLine());

            RegistrarEquipamento(idSelecionado);
        }

        private static void ExcluirEquipamento()
        {
            Console.Clear();

            VisualizarEquipamentos();

            Console.WriteLine();

            Console.Write("Digite o número do equipamento que deseja excluir: ");
            int idSelecionado = Convert.ToInt32(Console.ReadLine());

            for (int i = 0; i < idsEquipamento.Length; i++)
            {
                if (idsEquipamento[i] == idSelecionado)
                {
                    idsEquipamento[i] = 0;
                    nomesEquipamento[i] = null;
                    precosEquipamento[i] = 0;
                    numerosSerieEquipamento[i] = null;
                    datasFabricacaoEquipamento[i] = DateTime.MinValue;
                    fabricantesEquipamento[i] = null;

                    break;
                }
            }
        }

        private static void VisualizarEquipamentos()
        {
            Console.Clear();
                      
            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine("{0,-10} | {1,-55} | {2,-35}", "Id", "Nome", "Fabricante");

            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------");

            Console.ResetColor();

            int numeroEquipamentosCadastrados = 0;

            for (int i = 0; i < idsEquipamento.Length; i++)
            {
                if (idsEquipamento[i] > 0)
                {
                    Console.Write("{0,-10} | {1,-55} | {2,-35}",
                       idsEquipamento[i], nomesEquipamento[i], fabricantesEquipamento[i]);

                    Console.WriteLine();

                    numeroEquipamentosCadastrados++;
                }
            }

            if (numeroEquipamentosCadastrados == 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine("Nenhum equipmaneto cadastrado!");
                Console.ResetColor();
            }

            Console.ReadLine();
        }

        private static void RegistrarEquipamento(int idEquipamentoSelecionado)
        {
            Console.Clear();

            int posicao = ObterPosicaoParaEquipamentos(idEquipamentoSelecionado);

            string nome = "";
            bool nomeInvalido = false;
            do
            {
                Console.Write("Digite o nome do equipamento: ");
                nome = Console.ReadLine();
                if (nome.Length < 6)
                {
                    nomeInvalido = true;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Nome inválido. No mínimo 6 caracteres");
                    Console.ResetColor(); ;
                }

            } while (nomeInvalido);


            Console.Write("Digite o preço do equipamento: ");
            double preco = Convert.ToDouble(Console.ReadLine());

            Console.Write("Digite o número do equipamento: ");
            string numeroSerie = Console.ReadLine();

            Console.Write("Digite a data de fabricação do equipamento: ");
            DateTime dataFabricacao = Convert.ToDateTime(Console.ReadLine());

            Console.Write("Digite o fabricante do equipamento: ");
            string fabricante = Console.ReadLine();

            idsEquipamento[posicao] = IdEquipamento;
            nomesEquipamento[posicao] = nome;
            precosEquipamento[posicao] = preco;
            numerosSerieEquipamento[posicao] = numeroSerie;
            datasFabricacaoEquipamento[posicao] = dataFabricacao;
            fabricantesEquipamento[posicao] = fabricante;
        }

        private static int ObterPosicaoParaEquipamentos(int idEquipamentoSelecionado)
        {
            int posicao = 0;

            for (int i = 0; i < idsEquipamento.Length; i++)
            {
                if (idEquipamentoSelecionado == 0 && idsEquipamento[i] == 0) 
                {
                    IdEquipamento++;
                    posicao = i;
                    break;
                }
                else if (idEquipamentoSelecionado == idsEquipamento[i]) 
                {
                    posicao = i;
                    break;
                }
            }

            return posicao;
        }

        #endregion

        private static string ObterOpcao()
        {           
            Console.WriteLine("Digite 1 para o Cadastro de Equipamentos");
            Console.WriteLine("Digite 2 para o Controle de Chamados");

            Console.WriteLine("Digite S para Sair");

            string opcao = Console.ReadLine();
            return opcao;
        }

        private static void PopularAplicacao(int quantidadeRegistros)
        {
            string[] nomesEquipamentoFake = new string[]
            {
                "Impressora", "Notebook", "Estabilizador"
            };

            double[] precosEquipamentoFake = new double[]
            {
                1500, 3500, 2000
            };

            DateTime[] datasFabricacaoFake = new DateTime[]
            {
                DateTime.Now.AddYears(-2), DateTime.Now.AddDays(-4), DateTime.Now.AddDays(-6)
            };

            string[] fabricantesEquipamentoFake = new string[]
            {
                "HP", "ACER", "Samsung"
            };

            string[] titulosChamadoFake = new string[]
            {
                "Não liga", "Desliga sozinho", "Está travando"
            };

            string[] descricoesChamadoFake = new string[]
            {
                "Não liga pela manhã", "Desliga sozinho quando abre o netbeans", "Muito tempo ligado começa travar"
            };

            DateTime[] datasAberturaFake = new DateTime[]
            {
                DateTime.Now.AddDays(-2), DateTime.Now.AddDays(-4), DateTime.Now.AddDays(-6)
            };

            for (int i = 0; i < quantidadeRegistros; i++)
            {
                idsEquipamento[i] = i + 1;
                nomesEquipamento[i] = nomesEquipamentoFake[i % 3];
                precosEquipamento[i] = precosEquipamentoFake[i % 3];
                datasFabricacaoEquipamento[i] = datasFabricacaoFake[i % 3];
                fabricantesEquipamento[i] = fabricantesEquipamentoFake[i % 3];

                idsChamado[i] = i + 1;
                idsEquipamentoChamado[i] = idsEquipamento[i % 3];
                titulosChamado[i] = titulosChamadoFake[i % 3];
                descricoesChamado[i] = descricoesChamadoFake[i % 3];
                datasAberturaChamado[i] = datasAberturaFake[i % 3];

            }
        }
    }
}