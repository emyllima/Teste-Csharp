using System;
using System.Collections.Generic;

class Program
{
    // Lista para armazenar os fornecedores cadastrados
    static List<Fornecedor> fornecedores = new List<Fornecedor>();

    static void Main(string[] args)
    {

        int opcao;

        do
        {
            Console.Clear();
            Console.WriteLine("Menu de Opções:");
            Console.WriteLine("1 - Cadastrar Fornecedor");
            Console.WriteLine("2 - Pesquisar Fornecedor");
            Console.WriteLine("3 - Sair");
            Console.Write("Escolha uma opção: ");
            opcao = int.Parse(Console.ReadLine());

            switch (opcao)
            {
                case 1:
                    CadastrarFornecedor();
                    break;
                case 2:
                    PesquisarFornecedor();
                    break;
                case 3:
                    Console.WriteLine("Saindo...");
                    break;
                default:
                    Console.WriteLine("Opção inválida! Tente novamente.");
                    break;
            }

            if (opcao != 3)
            {
                Console.WriteLine("\nPressione qualquer tecla para voltar ao menu...");
                Console.ReadKey();
            }

        } while (opcao != 3);
    }

    static void CadastrarFornecedor()
    {
        Console.Clear();
        // Criar uma empresa
        Empresa empresa = new Empresa();
        Console.WriteLine("Digite o estado (UF) da empresa:");
        empresa.Uf = Console.ReadLine();
        Console.WriteLine("Digite o nome fantasia da empresa:");
        empresa.NomeFantasia = Console.ReadLine();
        Console.WriteLine("Digite o CNPJ da empresa:");
        empresa.Cnpj = Console.ReadLine();

        // Criar um fornecedor
        Fornecedor fornecedor = new Fornecedor();
        Console.WriteLine("\nDigite o nome do fornecedor:");
        fornecedor.Nome = Console.ReadLine();
        Console.WriteLine("Digite o CPF ou CNPJ do fornecedor:");
        fornecedor.CpfoCnpj = Console.ReadLine();
        fornecedor.DataHoraCadastro = DateTime.Now;

        // Perguntar se o fornecedor é pessoa física ou jurídica
        Console.WriteLine("O fornecedor é pessoa física (Digite 'f') ou jurídica (Digite 'j')?");
        string tipoPessoa = Console.ReadLine().ToLower();

        if (tipoPessoa == "f")
        {
            // Pessoa física: pedir o RG e a data de nascimento
            Console.WriteLine("Digite o RG do fornecedor:");
            fornecedor.Rg = Console.ReadLine();
            Console.WriteLine("Digite a data de nascimento do fornecedor (Formato: dd/MM/yyyy):");
            fornecedor.DataNascimento = DateTime.Parse(Console.ReadLine());
        }
        else
        {
            // Pessoa jurídica: sem RG nem data de nascimento
            fornecedor.Rg = null;
            fornecedor.DataNascimento = null;
        }

        fornecedor.Empresa = empresa; // Associando a empresa criada

        // Adicionar telefones ao fornecedor
        fornecedor.Telefones = new List<Telefone>(); // Inicializar a lista de telefones
        bool adicionarTelefone = true;
        while (adicionarTelefone)
        {
            Telefone telefone = new Telefone();
            Console.WriteLine("\nDigite o tipo de telefone (ex: Celular, Fixo):");
            telefone.Tipo = Console.ReadLine();
            Console.WriteLine("Digite o número do telefone:");
            telefone.Numero = Console.ReadLine();
            fornecedor.Telefones.Add(telefone);

            Console.WriteLine("Deseja adicionar outro telefone? (Digite 's' para sim, qualquer tecla para não):");
            string resposta = Console.ReadLine();
            if (resposta.ToLower() != "s")
            {
                adicionarTelefone = false;
            }
        }

        // Validar o fornecedor
        try
        {
            fornecedor.Validar(); // Validação das regras de negócios
            fornecedores.Add(fornecedor); // Adiciona o fornecedor à lista
            Console.WriteLine("\nFornecedor cadastrado com sucesso!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao cadastrar fornecedor: {ex.Message}");
        }

        // Exibir os dados cadastrados
        MostrarDadosFornecedor(fornecedor);
    }

    static void PesquisarFornecedor()
    {
        Console.Clear();
        Console.WriteLine("Escolha o filtro para pesquisa:");
        Console.WriteLine("1 - Nome");
        Console.WriteLine("2 - CPF/CNPJ");
        Console.WriteLine("3 - Data de Cadastro");
        Console.Write("Escolha uma opção: ");
        int filtro = int.Parse(Console.ReadLine());

        List<Fornecedor> resultados = new List<Fornecedor>();

        switch (filtro)
        {
            case 1:
                Console.WriteLine("Digite o nome do fornecedor para pesquisar:");
                string nomePesquisa = Console.ReadLine().ToLower();
                resultados = fornecedores.Where(f => f.Nome.ToLower().Contains(nomePesquisa)).ToList();
                break;

            case 2:
                Console.WriteLine("Digite o CPF/CNPJ do fornecedor para pesquisar:");
                string cpfCnpjPesquisa = Console.ReadLine();
                resultados = fornecedores.Where(f => f.CpfoCnpj.Contains(cpfCnpjPesquisa)).ToList();
                break;

            case 3:
                Console.WriteLine("Digite a data de cadastro do fornecedor (Formato: dd/MM/yyyy):");
                DateTime dataCadastroPesquisa;
                if (DateTime.TryParse(Console.ReadLine(), out dataCadastroPesquisa))
                {
                    resultados = fornecedores.Where(f => f.DataHoraCadastro.Date == dataCadastroPesquisa.Date).ToList();
                }
                else
                {
                    Console.WriteLine("Data inválida.");
                    return;
                }
                break;

            default:
                Console.WriteLine("Opção inválida.");
                return;
        }

        if (resultados.Any())
        {
            foreach (var fornecedor in resultados)
            {
                MostrarDadosFornecedor(fornecedor);
            }
        }
        else
        {
            Console.WriteLine("Nenhum fornecedor encontrado com esse critério.");
        }
    }

    static void MostrarDadosFornecedor(Fornecedor fornecedor)
    {
        Console.WriteLine("\nDados do Fornecedor:");
        Console.WriteLine($"Nome: {fornecedor.Nome}");
        Console.WriteLine($"CPF/CNPJ: {fornecedor.CpfoCnpj}");
        Console.WriteLine($"Data de Cadastro: {fornecedor.DataHoraCadastro}");
        Console.WriteLine($"RG: {fornecedor.Rg}");
        if (fornecedor.DataNascimento.HasValue)
        {
            Console.WriteLine($"Data de Nascimento: {fornecedor.DataNascimento.Value.ToShortDateString()}");
        }

        Console.WriteLine($"Empresa: {fornecedor.Empresa.NomeFantasia} - CNPJ: {fornecedor.Empresa.Cnpj}");
        Console.WriteLine("Telefones:");
        foreach (var telefone in fornecedor.Telefones)
        {
            Console.WriteLine($"- {telefone.Tipo}: {telefone.Numero}");
        }



        /*
        // Criar uma empresa
        Empresa empresa = new Empresa();
        empresa.Uf = "PR"; // Estado (UF)
        empresa.NomeFantasia = "Fornecedor Paraná LTDA";
        empresa.Cnpj = "12.345.678/0001-99";

        // Criar um fornecedor
        Fornecedor fornecedor = new Fornecedor();
        fornecedor.Nome = "Emy Lima";
        fornecedor.CpfoCnpj = "123.456.789-00"; // CPF
        fornecedor.DataHoraCadastro = DateTime.Now;
        fornecedor.Rg = "12.345.678"; // RG para pessoa física
        fornecedor.DataNascimento = new DateTime(2000, 8, 20); // Data de nascimento
        fornecedor.Empresa = empresa; // Associando a empresa criada

        // Adicionar telefones ao fornecedor
        Telefone telefone1 = new Telefone();
        telefone1.Numero = "11111-1111";
        telefone1.Tipo = "Celular";
        fornecedor.Telefones.Add(telefone1);

        Telefone telefone2 = new Telefone();
        telefone2.Numero = "2222-2222";
        telefone2.Tipo = "Fixo";
        fornecedor.Telefones.Add(telefone2);

        // Validar o fornecedor
        try
        {
            fornecedor.Validar(); // Validação das regras de negócios
            Console.WriteLine("Fornecedor cadastrado com sucesso!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao cadastrar fornecedor: {ex.Message}");
        }

        // Exibir os dados cadastrados
        MostrarDadosFornecedor(fornecedor);
        
    }

    static void MostrarDadosFornecedor(Fornecedor fornecedor)
    {
        Console.WriteLine("\nDados do Fornecedor:");
        Console.WriteLine($"Nome: {fornecedor.Nome}");
        Console.WriteLine($"CPF/CNPJ: {fornecedor.CpfoCnpj}");
        Console.WriteLine($"Data de Cadastro: {fornecedor.DataHoraCadastro}");
        Console.WriteLine($"RG: {fornecedor.Rg}");
        if (fornecedor.DataNascimento.HasValue)
        {
            Console.WriteLine($"Data de Nascimento: {fornecedor.DataNascimento.Value.ToShortDateString()}");
        }

        Console.WriteLine($"Empresa: {fornecedor.Empresa.NomeFantasia} - CNPJ: {fornecedor.Empresa.Cnpj}");
        Console.WriteLine("Telefones:");
        foreach (var telefone in fornecedor.Telefones)
        {
            Console.WriteLine($"- {telefone.Tipo}: {telefone.Numero}");
        }
        */
    }


}
