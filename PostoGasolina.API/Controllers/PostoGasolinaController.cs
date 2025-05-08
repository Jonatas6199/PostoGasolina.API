using Microsoft.AspNetCore.Mvc;

namespace PostoGasolina.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostoGasolinaController : Controller
    {
        //Método HttpGet, que somente retorna informações
        [HttpGet("ListaCombustiveis")]
        public IActionResult ListaCombustiveis()
        {
            //Devolve um status code de sucesso juntamente com a lista de combustiveis
            return StatusCode(200, BancoDeDados.Combustiveis);
        }

        [HttpPost("ComprarCombustivel")]
        public IActionResult ComprarCombustivel (int codigoDeCombustivel, double litros)
        {
            //Guardar o combustivel escolhido
            Combustivel? combustivelEscolhido = null;

            //Para cada combustivel dentro da lista de combustiveis
            foreach (Combustivel combustivel in BancoDeDados.Combustiveis)
            {
                if(combustivel.CodigoDoProduto == codigoDeCombustivel)
                {
                    //variavel de nome combustivel 
                    //é a variável que temos no banco de dados
                    //estou copiando o valor dela pra variável combustivelEscolhido
                    combustivelEscolhido = combustivel;
                    break;
                }
            }
            /* Outra forma de fazer a busca
            combustivelEscolhido = 
            BancoDeDados.Combustiveis.Find(c => c.CodigoDoProduto == codigoDeCombustivel);
            */
            if (combustivelEscolhido == null)
                return StatusCode(400,"Nenhum código associado foi encontrado");

            
            Compra compra = new Compra();
            compra.Combustivel = combustivelEscolhido;
            compra.DataCompra = DateTime.Now;
            compra.ValorTotal = combustivelEscolhido.PrecoLitro * litros;

            BancoDeDados.Compras.Add(compra);

            return StatusCode(200, compra);
        }

        [HttpGet("Extrato")]
        public IActionResult Extrato()
        {
            return StatusCode(200, BancoDeDados.Compras);
        }

    }
}
