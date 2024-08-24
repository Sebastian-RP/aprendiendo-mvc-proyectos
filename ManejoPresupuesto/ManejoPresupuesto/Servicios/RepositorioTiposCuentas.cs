using Dapper;
using ManejoPresupuesto.Models;
using Microsoft.Data.SqlClient;

namespace ManejoPresupuesto.Servicios
{
    public interface IRepositorioTiposCuentas
    {
        Task Actualizar(TipoCuenta tipocuenta);
        Task Borrar(int id);
        Task Crear(TipoCuenta tipoCuenta);
        Task<bool> Existe(string nombre, int UsuarioId, int id = 0);
        Task<IEnumerable<TipoCuenta>> Obtener(int usuarioId);
        Task<TipoCuenta> ObtenerPorId(int id, int usuarioId);
        Task Ordenar(IEnumerable<TipoCuenta> tipoCuentasOrdenadas);
    }

    public class RepositorioTiposCuentas(IConfiguration configuration) : IRepositorioTiposCuentas
    {
        private readonly string connectionString = configuration.GetConnectionString("DefaultConnection");

        public async Task Crear(TipoCuenta tipoCuenta)
        {
            using var connection = new SqlConnection(connectionString);
            var id = await connection.QuerySingleAsync<int>
                ("dbo.TiposCuentas_Insertar", 
                new { UsuarioId = tipoCuenta.UsuarioId, Nombre = tipoCuenta.Nombre },
                commandType: System.Data.CommandType.StoredProcedure);
            tipoCuenta.Id = id;
        }

        public async Task<bool> Existe(string nombre, int UsuarioId, int id = 0)
        {
            var existe = 0;

            using var connection = new SqlConnection(connectionString);
            existe = await connection.QueryFirstOrDefaultAsync<int>(
                @"SELECT 1 
                FROM TiposCuentas 
                WHERE Nombre = @Nombre AND UsuarioId = @UsuarioId AND Id <> @id;",
            new { nombre, UsuarioId, id });

            return existe == 1;
        }

        public async Task<IEnumerable<TipoCuenta>> Obtener(int usuarioId)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<TipoCuenta>(
                @"SELECT Id, Nombre, Orden
                FROM TiposCuentas
                WHERE UsuarioId = @UsuarioId
                ORDER BY Orden", new { usuarioId });
        }

        public async Task Actualizar(TipoCuenta tipocuenta)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(@"UPDATE TiposCuentas
                                            SET Nombre = @Nombre
                                            WHERE Id = @Id", tipocuenta);
        }

        public async Task<TipoCuenta> ObtenerPorId(int id, int usuarioId)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryFirstOrDefaultAsync<TipoCuenta>(
                @"SELECT Id, Nombre, Orden
                FROM TiposCuentas
                WHERE Id = @Id AND UsuarioId = @UsuarioId", 
                new { id, usuarioId }
            );
        }

        public async Task Borrar(int id)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(@"DELETE TiposCuentas WHERE Id = @Id", new { id });
        }

        public async Task Ordenar(IEnumerable<TipoCuenta> tipoCuentasOrdenadas)
        {
            //se ejecuta el Query por cada elemento de la lista
            var query = "UPDATE TiposCuentas SET Orden = @Orden WHERE Id = @Id";
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(query, tipoCuentasOrdenadas);
        }
    }
}
