namespace PostoGasolina.API
{
    public class Combustivel
    {
        //Codigo referente ao tipo de combustível
        public int CodigoDoProduto { get; set; }

        //Nome do combustível. Ex: Gasolina Comum, Etanol Comum, etc.
        public string Descricao { get; set; }

        //Vai definir o valor por litro do combustível
        //Exemplo: Gasolina Comum o PrecoLitro é 5.99
        public double PrecoLitro { get; set; }
    }
}
