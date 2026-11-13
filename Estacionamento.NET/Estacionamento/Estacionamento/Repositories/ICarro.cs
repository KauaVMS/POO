using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace Estacionamento.Repositories
{
    public interface ICarro
    {
        Task<string?> Ler(int id);

        Task<string> Listar();

        Task<bool> Adicionar(int id_usuario, string desc);

        Task<bool> Editar(int id, int id_usuario, string desc);

        Task<bool> Apagar(int id);
    }
}
