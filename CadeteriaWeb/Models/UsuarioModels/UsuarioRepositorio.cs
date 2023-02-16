using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data;
using Microsoft.Data.Sqlite;

namespace CadeteriaWeb.Models.UsuarioModels
{
    public class UsuarioRepositorio:IUsuario
    {
       private readonly IConfiguration Configuration;

       public UsuarioRepositorio(IConfiguration configuration)
        {
            Configuration = configuration;
        }
         public Usuario Logear(string usuario, string pass)
        {
            Usuario user = null;
            int idCadete = 0;

            SqliteCommand comando = new();
            SqliteConnection conexion = new SqliteConnection(Configuration["ConnectionStrings:Connection"]);
            conexion.Open();
            comando.Connection = conexion;

            try
            {
                comando.CommandText = "SELECT * FROM usuarios WHERE usuario = $user AND password = $pass";
                comando.Parameters.AddWithValue("$user", usuario);
                comando.Parameters.AddWithValue("$pass", pass);
                var reader = comando.ExecuteReader();

                if(reader.Read() && !reader.IsDBNull(0))
                {
                    if (!reader.IsDBNull(5))
                    {
                        idCadete = reader.GetInt32(5);
                    }
                    if (reader.GetString(4) == "Administrador")
                    {
                        user = new(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), RolUsuario.Administrador, idCadete);
                    }
                    if (reader.GetString(4) == "Cadete")
                    {
                        user = new(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), RolUsuario.Cadete, idCadete);
                    }
                    
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ha ocurrido un error (UsuarioRepo, Logear): " + ex.Message);
            }

            conexion.Close();

            return user;
        }
    }
}
    
    
