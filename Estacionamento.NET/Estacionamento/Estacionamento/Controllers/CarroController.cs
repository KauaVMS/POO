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
    public class CarroController : Controller
    {
        private readonly CarroRepository carRep;

        public CarroController(MySqlConnection connection)
        {
            carRep = new CarroRepository(connection);
        }

        [HttpGet("Ler/{id}")]
        public async Task<ActionResult> Ler(int id)
        {
            string? res = await carRep.Ler(id);

            if (res != null)
            {
                return Ok(res);
            }
            else
            {
                return BadRequest("Carro não encontrado");
            }
        }

        [HttpGet("Listar")]
        public async Task<ActionResult> Listar()
        {
            string res = await carRep.Listar();

            return Ok(res);
        }

        [HttpPost]
        public async Task<ActionResult> Adicionar(int id_usuario, string desc)
        {
            await carRep.Adicionar(id_usuario, desc);

            return Ok($"Carro adicionado");
        }

        [HttpPut]
        public async Task<ActionResult> Editar(int id, int id_usuario, string desc)
        {
            bool res = await carRep.Editar(id, id_usuario, desc);

            if (res)
            {
                return Ok("Carro editado");
            }
            else
            {
                return BadRequest("Carro não encontrado");
            }
        }

        [HttpDelete]
        public async Task<ActionResult> Apagar(int id)
        {
            bool res = await carRep.Apagar(id);

            if (res)
            {
                return Ok("Carro apagado");
            }
            else
            {
                return BadRequest("Carro não encontrado");
            }
        }
    }
}
