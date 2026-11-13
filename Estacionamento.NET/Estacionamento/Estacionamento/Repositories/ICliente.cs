using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace Estacionamento.Repositories
{
    public interface ICliente
    {
        Task<string?> Ler(int id);

        Task<string> Listar();

        Task<bool> Criar(string nome);

        Task<bool> Editar(int id, string nome);

        Task<bool> Apagar(int id);
    }
}
