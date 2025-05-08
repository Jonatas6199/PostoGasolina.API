namespace PostoGasolina.API
{
    public class Compra
    {
        //Qual combustível foi utilizado na compra
        public Combustivel Combustivel { get; set; }
        //Qual a data da compra
        public DateTime DataCompra {  get; set; }
        //Qual o valor total da compra
        public double ValorTotal { get; set; }
    }
}
