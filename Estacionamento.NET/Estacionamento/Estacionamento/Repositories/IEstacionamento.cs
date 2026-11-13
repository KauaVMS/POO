using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace Estacionamento.Repositories
{
    public interface IEstacionamento
    {
        Task<string?> Ler(int id);

        Task<bool> Estacionar(int id_carro, int id_vaga);

        Task<string?> Desestacionar(int id_vaga);
    }
}
