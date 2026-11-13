using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace Estacionamento.Repositories
{
    public class EstacionamentoRepository : IEstacionamento
    {
        private readonly MySqlConnection conn;

        public EstacionamentoRepository(MySqlConnection connection)
        {
            conn = connection;
            conn.Open();
        }

        public async Task<string?> Ler(int id)
        {
            var command = new MySqlCommand($"SELECT * FROM tb_estacionamento WHERE id_vaga = {id} ORDER BY data DESC LIMIT 1", conn);
            await using (var reader = command.ExecuteReader())
            {
                if (await reader.ReadAsync())
                {
                    if (reader.GetInt32("id_carro") == -1)
                    {
                        return null;
                    }
                    else
                    {
                        return $"Vaga ocupada por {reader.GetInt32("id_carro")}";
                    }
                }
                else
                {
                    return null;
                }
            }
        }
        public async Task<bool> Estacionar(int id_carro, int id_vaga)
        {
            var command = new MySqlCommand($"SELECT * FROM tb_estacionamento WHERE id_vaga = {id_vaga} ORDER BY data DESC LIMIT 1", conn);

            await using (var reader = command.ExecuteReader())
            {
                if (await reader.ReadAsync() && reader.GetInt32("id_carro") != -1)
                {
                    return false;
                }
                else
                {
                    await reader.CloseAsync();
                }
            }

            command = new MySqlCommand($"SELECT * FROM tb_carros WHERE id_carro = {id_carro}", conn);

            await using (var reader = command.ExecuteReader())
            {
                if (await reader.ReadAsync())
                {
                    await reader.CloseAsync();
                }
                else
                {
                    return false;
                }
            }

            command = new MySqlCommand($"SELECT * FROM tb_estacionamento WHERE id_carro = {id_carro} ORDER BY data DESC LIMIT 1", conn);

            await using (var reader = command.ExecuteReader())
            {
                if (await reader.ReadAsync() && reader.GetInt32("vaga") != -1)
                {
                    return false;
                }
                else
                {
                    await reader.CloseAsync();
                }
            }

            var command1 = new MySqlCommand($"INSERT INTO tb_estacionamento(id_carro, id_vaga) VALUES({id_carro}, '{id_vaga}')", conn);
            await command1.ExecuteNonQueryAsync();

            return true;
        }
        public async Task<string?> Desestacionar(int id_vaga)
        {
            var command = new MySqlCommand($"SELECT * FROM tb_estacionamento WHERE id_vaga = {id_vaga} ORDER BY data DESC LIMIT 1", conn);
            int preco;

            await using (var reader = command.ExecuteReader())
            {
                if (await reader.ReadAsync() && reader.GetInt32("id_carro") == -1)
                {
                    return null;
                }
                else
                {
                    preco = 20 + DateTime.Now.Subtract(reader.GetDateTime("data")).Minutes / 2;
                    await reader.CloseAsync();
                }
            }


            var command1 = new MySqlCommand($"INSERT INTO tb_estacionamento(id_carro, id_vaga) VALUES({-1}, '{id_vaga}')", conn);
            await command1.ExecuteNonQueryAsync();

            return "Carro desestacionado; Preço: " + preco.ToString("C");
        }
    }
}
