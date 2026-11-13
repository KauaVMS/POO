using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace Estacionamento.Repositories
{
    public class CarroRepository : ICarro
    {
        private readonly MySqlConnection conn;

        public CarroRepository(MySqlConnection connection)
        {
            conn = connection;
            conn.Open();
        }

        public async Task<string?> Ler(int id)
        {
            var command = new MySqlCommand($"SELECT * FROM tb_carros INNER JOIN tb_usuarios ON tb_carros.id_usuario = tb_usuarios.id_usuario WHERE id_carro = {id}", conn);
            await using var reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return $"{reader.GetString(2)} de {reader.GetString(4)}";
            }
            else
            {
                return "Carro não encontrado";
            }
        }
        public async Task<string> Listar()
        {
            var command = new MySqlCommand("SELECT * FROM tb_carros INNER JOIN tb_usuarios ON tb_carros.id_usuario = tb_usuarios.id_usuario", conn);
            await using var reader = await command.ExecuteReaderAsync();

            var clientes = new List<string>();
            while (await reader.ReadAsync())
            {
                clientes.Add($"{reader.GetString(2)} de {reader.GetString(4)}");
            }

            await reader.CloseAsync();

            return string.Join(", ", clientes);
        }
        public async Task<bool> Adicionar(int id_usuario, string desc)
        {
            var command = new MySqlCommand($"SELECT * FROM tb_usuarios WHERE id_usuario={id_usuario}", conn);

            await using var reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                await reader.CloseAsync();

                var command1 = new MySqlCommand($"INSERT INTO tb_carros(id_usuario, desc_carro) VALUES({id_usuario}, '{desc}')", conn);
                await command1.ExecuteNonQueryAsync();

                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<bool> Editar(int id, int id_usuario, string desc)
        {
            var command = new MySqlCommand($"SELECT * FROM tb_carros WHERE id_carro={id}", conn);

            await using var reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                await reader.CloseAsync();

                var command1 = new MySqlCommand($"UPDATE tb_carros SET desc_carro='{desc}', id_usuario={id_usuario} WHERE id_carro={id}", conn);

                await command1.ExecuteNonQueryAsync();

                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<bool> Apagar(int id)
        {
            var command = new MySqlCommand($"SELECT * FROM tb_carros WHERE id_carro={id}", conn);

            await using var reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                await reader.CloseAsync();

                var command1 = new MySqlCommand($"DELETE * FROM tb_carros WHERE id_carro={id}", conn);

                await command1.ExecuteNonQueryAsync();

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
