using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace Estacionamento.Repositories
{
    public class ClienteRepository : ICliente
    {
        private readonly MySqlConnection conn;

        public ClienteRepository(MySqlConnection connection)
        {
            conn = connection;
            conn.Open();
        }

        public async Task<string?> Ler(int id)
        {
            var command = new MySqlCommand($"SELECT * FROM tb_usuarios WHERE id_usuario={id}", conn);
            await conn.OpenAsync();
            await using var reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return reader.GetString(1);
            }
            else
            {
                return null;
            }
        }
        public async Task<string> Listar()
        {
            var command = new MySqlCommand("SELECT * FROM tb_usuarios", conn);
            await conn.OpenAsync();
            await using var reader = await command.ExecuteReaderAsync();

            var clientes = new List<string>();
            while (await reader.ReadAsync())
            {
                clientes.Add(reader.GetString(1));
            }

            return string.Join(", ", clientes);
        }
        public async Task<bool> Criar(string nome)
        {
            var command = new MySqlCommand($"INSERT INTO tb_usuarios(nome_usuario) VALUES('{nome}')", conn);
            await conn.OpenAsync();
            await command.ExecuteNonQueryAsync();

            return true;
        }
        public async Task<bool> Editar(int id, string nome)
        {
            var command = new MySqlCommand($"SELECT * FROM tb_usuarios WHERE id_usuario={id}", conn);
            await conn.OpenAsync();

            await using var reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                await reader.CloseAsync();

                var command1 = new MySqlCommand($"UPDATE tb_usuarios SET nome_usuario='{nome}' WHERE id_usuario={id}", conn);

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
            var command = new MySqlCommand($"SELECT * FROM tb_usuarios WHERE id_usuario={id}", conn);
            await conn.OpenAsync();

            await using var reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                await reader.CloseAsync();

                var command1 = new MySqlCommand($"DELETE * FROM tb_usuario WHERE id_usuario={id}", conn);

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
