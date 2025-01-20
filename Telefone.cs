public class Telefone
{
    #region Atributos Privados
    private string numero;
    private string tipo;
    #endregion

    #region Métodos Acessores e Modificadores
    public string Numero
    {
        get { return numero; }
        set { numero = value; }
    }

    public string Tipo
    {
        get { return tipo; }
        set { tipo = value; }
    }
    #endregion
}