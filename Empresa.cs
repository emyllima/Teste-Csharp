public class Empresa
    {
    #region Atributos Privados
    private string uf;
    private string nomeFantasia;
    private string cnpj;
    #endregion

    #region Métodos Acessores e Modificadores
    public string Uf
    {
        get { return uf; }
        set { uf = value; }
    }

    public string NomeFantasia
    {
        get { return nomeFantasia; }
        set { nomeFantasia = value; }
    }

    public string Cnpj
    {
        get { return cnpj; }
        set { cnpj = value; }
    }
    #endregion

    }
