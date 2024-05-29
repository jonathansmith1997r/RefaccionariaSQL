using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;


namespace Refaccionariav2
{
    class Conexion
    {
        private static string cadenaConexion = ("SERVER=DESKTOP-665NNR5;DATABASE=refaccionaria2;integrated security = true;");
        private static SqlConnection cn;
        public static SqlConnection Conectar()
        {
            //Se pone el valor de un punto ya que todos manejamos el Localhost de la Lap.
            //SqlConnection cn = new SqlConnection("SERVER=DESKTOP-UIK66RL;DATABASE=Palmusica;integrated security=true;");
            cn = new SqlConnection(cadenaConexion);
            cn.Open();
            return cn;

        }

    }
}
