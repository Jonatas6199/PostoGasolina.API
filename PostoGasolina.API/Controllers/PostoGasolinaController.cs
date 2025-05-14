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
            return StatusCode(200, BancoDeDados.ListarCombustiveis());
        }

        //Método HttpGet com um parâmetro obrigatório na assinatura do endpoint
        [HttpGet("CombustivelEspecifico/{codigoCombustivel}")]
        public IActionResult CombustivelEspecifico(int codigoCombustivel)
        {
            //Busca um combustivel no banco de dados e o atribui a variavel combustivelEncontrado
            Combustivel? combustivelEncontrado = BancoDeDados.BuscaCombustivelEspecifico(codigoCombustivel);
            //Verifica se foi de fato encontrado, se não foi, ele vai ser nulo
            if (combustivelEncontrado == null)
                //Se não encontrou, devolve um código de erro na requisição
                return StatusCode(400, "Nenhum combustivel com esse código foi encontrado");
            
            //Devolve um código de sucesso junto com o combustivel encontrado
            return StatusCode(200, combustivelEncontrado);
        }

        [HttpPut("AtualizarPreco")]
        public IActionResult AtualizarPreco(int codigoProduto, double novoPreco)
        {
            if (BancoDeDados.BuscaCombustivelEspecifico(codigoProduto) == null)
                return StatusCode(400, "Nenhum código de produto encontrado");
           
            BancoDeDados.AtualizarPreco(codigoProduto, novoPreco);
            return StatusCode(200,"Preço atualizado!");
        }

        [HttpPost("ComprarCombustivel")]
        public IActionResult ComprarCombustivel(int codigoDeCombustivel, double litros)
        {
            Combustivel? combustivel = BancoDeDados.BuscaCombustivelEspecifico(codigoDeCombustivel);
            if (combustivel == null)
                return BadRequest("Nenhum combustível encontrado");

            Compra compra = new Compra();
            compra.ValorTotal = combustivel.PrecoLitro * litros;
            compra.Combustivel = combustivel;
            compra.DataCompra = DateTime.Now;

            BancoDeDados.RealizarCompra(codigoDeCombustivel, compra.ValorTotal);
            return Ok(compra);
        }

        [HttpGet("Extrato")]
        public IActionResult Extrato()
        {
            return StatusCode(200, BancoDeDados.ListarCompras());
        }

    }
}
