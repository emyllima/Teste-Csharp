public class Fornecedor
    {
    #region Atributos Privados
    private string nome;
    private string cpfoCnpj;
    private DateTime dataHoraCadastro;
    private string rg;
    private DateTime? dataNascimento;
    private Empresa empresa;
    private List<Telefone> telefones;
    #endregion

     #region Métodos Acessores e Modificadores
    public string Nome
    {
        get { return nome; }
        set { nome = value; }
    }

    public string CpfoCnpj
    {
        get { return cpfoCnpj; }
        set { cpfoCnpj = value; }
    }

    public DateTime DataHoraCadastro
    {
        get { return dataHoraCadastro; }
        set { dataHoraCadastro = value; }
    }

    public string Rg
    {
        get { return rg; }
        set { rg = value; }
    }

    public DateTime? DataNascimento
    {
        get { return dataNascimento; }
        set { dataNascimento = value; }
    }

    public Empresa Empresa
    {
        get { return empresa; }
        set { empresa = value; }
    }

    public List<Telefone> Telefones
    {
        get { return telefones; }
        set { telefones = value; }
    }
    #endregion    

     #region Construtores
    public Fornecedor()
    {
        Telefones = new List<Telefone>();
    }
    #endregion

    #region Métodos
    public void Validar()
    {
        if (string.IsNullOrWhiteSpace(cpfoCnpj))
            throw new Exception("CPF ou CNPJ é obrigatório.");

        if (empresa.Uf == "PR" && dataNascimento.HasValue)
        {
            int idade = CalcularIdade(dataNascimento.Value);
            if (idade < 18)
                throw new Exception("Fornecedor menor de idade não é permitido no Paraná.");
        }

        if (!string.IsNullOrWhiteSpace(rg) && !dataNascimento.HasValue)
            throw new Exception("Data de nascimento é obrigatória para pessoa física.");
    }

    private int CalcularIdade(DateTime dataNascimento)
    {
        int idade = DateTime.Now.Year - dataNascimento.Year;
        if (DateTime.Now.Date < dataNascimento.Date.AddYears(idade))
            idade--;
        return idade;
    }
    #endregion

    }