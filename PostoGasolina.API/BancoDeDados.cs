using MySql.Data.MySqlClient;
using Org.BouncyCastle.Tls;

namespace PostoGasolina.API
{
    public static class BancoDeDados
    {
        private static string stringDeConexao = "Server=localhost;Port=3306;User ID=root;Database=ti_113_uc13";
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

        public static List<Combustivel> ListarCombustiveis()
        {
            //Abrindo uma conexão com o meu banco de dados
            MySqlConnection connection = new MySqlConnection(stringDeConexao);
            connection.Open();

            //Definindo a query que será executada
            string query = "select * from Combustivel";

            //Estou criando um comando MySQL com a query e a string de conexão
            MySqlCommand command = new MySqlCommand(query, connection);

            //Estou executando o comando com o método de leitura de execução
            //Vai me trazer o resultado da minha busca
            MySqlDataReader reader = command.ExecuteReader();

            //Lista para armazenar os valores buscados no meu banco de dados
            List<Combustivel> combustiveis = new List<Combustivel>();

            //Enquanto tiver itens no reader...
            while (reader.Read()) 
            {
                //Adiciona esse item na lista de combustíveis
                combustiveis.Add(new Combustivel
                {
                    //Preenche os campos do combustivel com os valores do banco de dados
                    CodigoDoProduto = reader.GetInt32("IdCodigoProduto"),
                    Descricao = reader.GetString("Descricao"),
                    PrecoLitro = reader.GetDouble("Preco")
                });
            }

            //Retorna a lista de combustiveis encontrados no banco de dados
            return combustiveis;

        }

        public static Combustivel? BuscaCombustivelEspecifico(int codigoProduto)
        {
            //Abrindo uma conexão com o meu banco de dados
            MySqlConnection connection = new MySqlConnection(stringDeConexao);
            connection.Open();

            //Definindo a query que será executada
            string query = "select * from Combustivel where IdCodigoProduto = @codigoProduto";

            //Estou criando um comando MySQL com a query e a string de conexão
            MySqlCommand command = new MySqlCommand(query, connection);
            //adiciona o valor do codigodeproduto no parametro da query sql
            command.Parameters.AddWithValue("@codigoProduto", codigoProduto);

            //Estou executando o comando com o método de leitura de execução
            //Vai me trazer o resultado da minha busca
            MySqlDataReader reader = command.ExecuteReader();

            //variavel do tipo Combustivel para armazenar o valor buscado no banco de dados
            Combustivel? combustivel = null;

            //Enquanto tiver itens no reader...
            while (reader.Read())
            {
                //Cria um objeto do tipo combustivel
                combustivel = new Combustivel
                {
                    //preenche os valores desse combustivel com a consulta que vem no banco de dados
                    CodigoDoProduto = reader.GetInt32("IdCodigoProduto"),
                    Descricao = reader.GetString("Descricao"),
                    PrecoLitro = reader.GetDouble("Preco")
                };
            }

            //Retorna o combustivel encontrado no banco de dados
            return combustivel;
        }

        public static void AtualizarPreco(int codigoProduto, double novoPreco)
        {
            //Abrindo uma conexão com o meu banco de dados
            MySqlConnection connection = new MySqlConnection(stringDeConexao);
            connection.Open();

            //Definindo a query que será executada
            string query = "update Combustivel set Preco = @novoPreco where IdCodigoProduto = @codigoProduto";

            //Estou criando um comando MySQL com a query e a string de conexão
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@novoPreco", novoPreco);
            command.Parameters.AddWithValue("@codigoProduto", codigoProduto);

            //Estou executando o comando 
            command.ExecuteNonQuery();
        }

        public static void RealizarCompra(int codigoCombustivel, double valorCompra)
        {
            MySqlConnection connection = new MySqlConnection(stringDeConexao);
            connection.Open();

            string query = "insert into Compra (Valor,DataCompra,CodigoCombustivel) " +
                "values(@valorCompra, @dataCompra, @codigoProduto)";
            MySqlCommand command = new MySqlCommand(query,connection);
            command.Parameters.AddWithValue("@valorCompra", valorCompra);
            command.Parameters.AddWithValue("@dataCompra", DateTime.Now);
            command.Parameters.AddWithValue("@codigoProduto", codigoCombustivel);

            command.ExecuteNonQuery();
        }

        public static List<Compra> ListarCompras()
        {
            MySqlConnection connection = new MySqlConnection(stringDeConexao);
            connection.Open();

            string query = "SELECT Compra.Valor, Compra.DataCompra, " +
                "Combustivel.IdCodigoProduto," +
                " Combustivel.Descricao " +
                "FROM Compra INNER JOIN Combustivel ON" +
                " Compra.CodigoCombustivel = Combustivel.IdCodigoProduto "+
                "order by Compra.DataCompra";

            MySqlCommand command = new MySqlCommand(query, connection);

            MySqlDataReader reader = command.ExecuteReader();

            List<Compra> compras = new List<Compra>();  
            while (reader.Read()) 
            {
                Combustivel combustivel = new Combustivel();
                combustivel.CodigoDoProduto = reader.GetInt32("IdCodigoProduto");
                combustivel.Descricao = reader.GetString("Descricao");

                Compra compra = new Compra();
                compra.DataCompra = reader.GetDateTime("DataCompra");
                compra.ValorTotal = reader.GetDouble("Valor");
                compra.Combustivel = combustivel;

                compras.Add(compra);
            }
            return compras;

        }
    }
}
