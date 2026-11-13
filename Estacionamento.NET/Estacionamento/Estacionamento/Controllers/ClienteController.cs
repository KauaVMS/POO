using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data.Common;
using Estacionamento.Repositories;

namespace Estacionamento.Controllers
{
    [Route("Estacionamento/[controller]")]
    [ApiController]
    public class ClienteController : Controller
    {
        private readonly ClienteRepository cliRep;

        public ClienteController(MySqlConnection connection)
        {
            cliRep = new ClienteRepository(connection);
        }

        [HttpGet("Ler/{id}")]
        public async Task<ActionResult> Ler(int id)
        {
            string? res = await cliRep.Ler(id);

            if (res != null)
            {
                return Ok(res);
            } 
            else
            {
                return BadRequest("Usuário não encontrado");
            }
        }

        [HttpGet("Listar")]
        public async Task<ActionResult> Listar()
        {
            string res = await cliRep.Listar();

            return Ok(res);
        }

        [HttpPost]
        public async Task<ActionResult> Criar(string nome)
        {
            await cliRep.Criar(nome);

            return Ok($"Usuário {nome} criado");
        }

        [HttpPut]
        public async Task<ActionResult> Editar(int id, string nome)
        {
            bool res = await cliRep.Editar(id, nome);

            if (res)
            {
                return Ok("Usuário editado");
            }
            else
            {
                return BadRequest("Usuário não encontrado");
            }
        }

        [HttpDelete]
        public async Task<ActionResult> Apagar(int id)
        {
            bool res = await cliRep.Apagar(id);

            if (res)
            {
                return Ok("Usuário apagado");
            }
            else
            {
                return BadRequest("Usuário não encontrado");
            }
        }
    }
}
