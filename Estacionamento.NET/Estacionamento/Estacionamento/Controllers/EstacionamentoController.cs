using Estacionamento.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using System.Configuration;
using System.Data.Common;

namespace Estacionamento.Controllers
{
    [Route("Estacionamento/[controller]")]
    [ApiController]
    public class EstacionamentoController : Controller
    {
        private readonly EstacionamentoRepository estRep;

        public EstacionamentoController(MySqlConnection connection)
        {
            estRep = new EstacionamentoRepository(connection);
        }

        [HttpGet("Ler/{id}")]
        public async Task<ActionResult> Ler(int id)
        {
            string? res = await estRep.Ler(id);

            if (res != null)
            {
                return Ok(res);
            } else
            {
                return BadRequest("Vaga Vazia");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Estacionar(int id_carro, int id_vaga)
        {
            bool res = await estRep.Estacionar(id_carro, id_vaga);

            if (res)
            {
                return Ok("Carro estacionado");
            } else
            {
                return BadRequest("Erro ao estacionar carro");
            }
        }

        [HttpDelete]
        public async Task<ActionResult> Desestacionar(int id_vaga)
        {
            string? res = await estRep.Desestacionar(id_vaga);

            if (res != null)
            {
                return Ok(res);
            }
            else
            {
                return BadRequest("Erro ao desestacionar carro");
            }
        }
    }
}
