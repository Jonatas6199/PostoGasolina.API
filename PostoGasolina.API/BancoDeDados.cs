namespace PostoGasolina.API
{
    public static class BancoDeDados
    {
        //Isso é como se fosse uma tabela no banco, que vai ter todos os combustíveis
        public static List<Combustivel> Combustiveis = new List<Combustivel>()
        {
            //Cada registro aqui é um combustível
            new Combustivel
            {
                CodigoDoProduto = 1,
                Descricao = "Gasolina Comum",
                PrecoLitro = 5.99
            },
            new Combustivel
            {
                CodigoDoProduto = 2,
                Descricao = "Etanol Comum",
                PrecoLitro = 3.99
            },
            new Combustivel
            {
                CodigoDoProduto = 3,
                Descricao = "Diesel",
                PrecoLitro = 6.99
            },
            new Combustivel
            {
                CodigoDoProduto=4,
                Descricao = "Gasolina Aditivada",
                PrecoLitro = 6.49
            }
        };

        //Essa lista é como se fosse uma tabela no banco pra salvar as compras
        public static List<Compra> Compras = new List<Compra>();
    }
}
