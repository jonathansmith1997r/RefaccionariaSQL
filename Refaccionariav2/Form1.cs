using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Refaccionariav2
{
    public partial class Form1 : Form
    {
        //Variables que se usan para tomar los id de dicha tabla y poder ejecutar acciones como modificar y eliminar registros de las tablas
        int id_refaccion;
        int id_proveedor;
        int id_empleado;
        int id_compra;
        int id_venta;
        int id_categoria;
        
        public Form1()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Carga todos los grids que se van a mostrar con los datos de la BD
        /// al igual que actualiza los combobox donde se hace referencia a alguna tabla.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            Conexion.Conectar();

            grid_refaccion.DataSource = mostrar_gridRefaccion();
            grid_vehiculo.DataSource = Mostrar_grid_vehiculo();
            grid_persona.DataSource = Mostrar_grid_persona();
            grid_categoria.DataSource = Mostrar_grid_categoria();
            gridProveedor.DataSource = Mostrar_grid_proveedor();
            gridEmpleado.DataSource = Mostrar_grid_empleado();
            gridCompra.DataSource = Mostrar_grid_Compra();
            gridVenta.DataSource = Mostrar_grid_venta();
            gridDetalleCompra.DataSource = Mostrar_grid_detalleCompra();
            gridDetalleVentas.DataSource = Mostrar_grid_detalleventa();
            actualizaListaVehiculo();
            actualizaListaCategoria();
            actualizaListaPersonas();
            actualizaListaProveedores();
            actualizaListaEmpleados();
            actualizaListaCompras();
            actualizaListaRefacciones();
            actualizaListaVentas();
            //modicar encabezados de algunos encabezados de las tablas cuando se concatenan campos
            grid_refaccion.Columns[1].HeaderText = "idVehiculo";
            grid_refaccion.Columns[2].HeaderText = "idCategoria";
            gridProveedor.Columns[1].HeaderText = "idPersona";
            gridEmpleado.Columns[1].HeaderText = "idPersona";
            gridCompra.Columns[1].HeaderText = "idProveedor";
            gridVenta.Columns[1].HeaderText = "idEmpleado";
            gridDetalleCompra.Columns[1].HeaderText = "idPieza";
            gridDetalleVentas.Columns[1].HeaderText = "idPieza";



        }
        /// <summary>
        /// actualiza el combobox con la lista de vehiculos ejecutando un query, mandandolo a un registro y llenando la lista
        /// </summary>
        public void actualizaListaVehiculo()
        {
            comboBox1.Items.Clear();
            Conexion.Conectar();
            SqlCommand comando2 = new SqlCommand("SELECT idVehiculo,numSerie,ano,nombreMarca,nombreModelo FROM ADMINREFACCIONARIA.Vehiculo", Conexion.Conectar());
            SqlDataReader reg2 = comando2.ExecuteReader();
            while (reg2.Read())
            {

                comboBox1.Items.Add(reg2["idVehiculo"].ToString()+"-"+ reg2["ano"].ToString()+"-"+ reg2["numSerie"].ToString() + "-" + reg2["nombreMarca"].ToString() + "-" + reg2["nombreModelo"].ToString());
            }
        }
        /// <summary>
        /// actualiza el combobox con la lista de categorias para refacción ejecutando un query, mandandolo a un registro y llenando la lista.
        /// </summary>
        public void actualizaListaCategoria()
        {
            comboBox2.Items.Clear();
            Conexion.Conectar();
            SqlCommand comando = new SqlCommand("SELECT idCategoria,nombre FROM ADMINREFACCIONARIA.Categoria", Conexion.Conectar());
            SqlDataReader reg = comando.ExecuteReader();
            while (reg.Read())
            {

                comboBox2.Items.Add(reg["idCategoria"].ToString() + "-" + reg["nombre"].ToString());
            }
        }
        /// <summary>
        /// actualiza la lista de personas para los combobox de empleado y proveedor ejecutando un query, mandandolo a un registro y llenando la lista
        /// </summary>
        public void actualizaListaPersonas()
        {
            comboBox3.Items.Clear();
            cBPersona2.Items.Clear();
            Conexion.Conectar();
            SqlCommand comando = new SqlCommand("SELECT idPersona,nombre FROM ADMINREFACCIONARIA.Persona", Conexion.Conectar());
            SqlDataReader reg = comando.ExecuteReader();
            
            while (reg.Read())
            {
                comboBox3.Items.Add(reg["idPersona"].ToString() + "-" + reg["nombre"].ToString());
                cBPersona2.Items.Add(reg["idPersona"].ToString() + "-" + reg["nombre"].ToString());
            }
            
        }
        /// <summary>
        /// actualiza los proveedores para las compras en el combobox ejecutando un query, mandandolo a un registro y llenando la lista
        /// </summary>
        public void actualizaListaProveedores()
        {
            cBProveedores.Items.Clear();
            Conexion.Conectar();
            SqlCommand comando = new SqlCommand("SELECT idProveedor,nombre,empresa FROM ADMINREFACCIONARIA.proveedor INNER JOIN ADMINREFACCIONARIA.persona ON ADMINREFACCIONARIA.proveedor.idpersona = ADMINREFACCIONARIA.persona.idpersona", Conexion.Conectar());
            SqlDataReader reg = comando.ExecuteReader();
            while (reg.Read())
            {
                cBProveedores.Items.Add(reg["idProveedor"].ToString() + "-" + reg["nombre"].ToString() + "-" + reg["empresa"].ToString());
            }
        }
        /// <summary>
        /// actualiza la lista de empleados para ventas en el combobox ejecutando un query, mandandolo a un registro y llenando la lista
        /// </summary>
        public void actualizaListaEmpleados()
        {
            cBEmpleados.Items.Clear();
            Conexion.Conectar();
            SqlCommand comando = new SqlCommand("SELECT idEmpleado,nombre FROM ADMINREFACCIONARIA.empleado INNER JOIN ADMINREFACCIONARIA.persona ON ADMINREFACCIONARIA.empleado.idpersona = ADMINREFACCIONARIA.persona.idpersona", Conexion.Conectar());
            SqlDataReader reg = comando.ExecuteReader();
            while (reg.Read())
            {
                cBEmpleados.Items.Add(reg["idEmpleado"].ToString() + "-" + reg["nombre"].ToString());
            }
        }
        /// <summary>
        /// actualiza las compras para el combobox de ventas en detalle de venta ejecutando un query, mandandolo a un registro y llenando la lista
        /// </summary>
        public void actualizaListaCompras()
        {
            cBCompras.Items.Clear();
            Conexion.Conectar();
            SqlCommand comando = new SqlCommand("SELECT idCompra FROM ADMINREFACCIONARIA.compra", Conexion.Conectar());
            SqlDataReader reg = comando.ExecuteReader();
            while (reg.Read())
            {
                cBCompras.Items.Add(reg["idCompra"].ToString());
            }
        }
        /// <summary>
        /// actualiza la lista de refacciones para los combobox de detalle de venta y detalle de compra
        /// ejecutando un query, mandandolo a un registro y llenando la lista.
        /// </summary>
        public void actualizaListaRefacciones()
        {
            cBRefacciones.Items.Clear();
            cBRefacciones2.Items.Clear();
            Conexion.Conectar();
            SqlCommand comando = new SqlCommand("SELECT idPieza,nombre FROM ADMINREFACCIONARIA.refaccion", Conexion.Conectar());
            SqlDataReader reg = comando.ExecuteReader();
            while (reg.Read())
            {

                cBRefacciones2.Items.Add(reg["idPieza"].ToString()+"-"+ reg["nombre"].ToString());
                cBRefacciones.Items.Add(reg["idPieza"].ToString() + "-" + reg["nombre"].ToString());
            }
        }
        /// <summary>
        /// actualiza la lista de compras en el combobox de detalle de venta
        /// ejecutando un query, mandandolo a un registro y llenando la lista.
        /// </summary>
        public void actualizaListaVentas()
        {
            cBVentas.Items.Clear();
            Conexion.Conectar();
            //query de selección 
            SqlCommand comando = new SqlCommand("SELECT idVenta FROM ADMINREFACCIONARIA.venta", Conexion.Conectar());
            SqlDataReader reg = comando.ExecuteReader();
            //lectura de registros
            while (reg.Read())
            {
                cBVentas.Items.Add(reg["idVenta"].ToString());
            }

        }
        /// <summary>
        /// boton para agregar refacciones siempre y cuando esten llenos todos los campos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnaddpieza_Click(object sender, EventArgs e)
        {
            using (Conexion.Conectar()) { }
            Conexion.Conectar();

            string insertar = "INSERT INTO ADMINREFACCIONARIA.refaccion(idVehiculo,idCategoria,nombre,descripcion,precioCompra,precioVenta,stock) VALUES (@idvehiculo,@idcategoria,@nombre,@descripcion,@preciocompra,@precioventa,@stock)";
            SqlCommand con1 = new SqlCommand(insertar, Conexion.Conectar());

            //con1.Parameters.AddWithValue("@id",tbidPieza.Text);
            con1.Parameters.AddWithValue("@idvehiculo", comboBox1.Text.Split('-')[0].Trim());
            
            con1.Parameters.AddWithValue("@idcategoria", comboBox2.Text.Split('-')[0].Trim());
        
            con1.Parameters.AddWithValue("@nombre",tbnombrerefaccion.Text);
            con1.Parameters.AddWithValue("@descripcion",rbdesc.Text);
            con1.Parameters.AddWithValue("@preciocompra",tbPreciocp.Text);
            con1.Parameters.AddWithValue("@precioventa",0);
            con1.Parameters.AddWithValue("@stock",0);
            con1.ExecuteNonQuery();
            limpiar();
            actualizaListaRefacciones();
            
            grid_refaccion.DataSource = mostrar_gridRefaccion();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// funcion que se encarga de limpiar los textbox de la pestaña de refacciones 
        /// mediante Clear.
        /// </summary>
        public void limpiar()
        {
            
            tbidecatfk.Clear();
            tbidvehiculofk.Clear();
            tbnombrerefaccion.Clear();
            tbPreciocp.Clear();
            //tbPreciovt.Clear();
            rbdesc.Clear();
            tbStock.Clear();
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
        }
        /// <summary>
        /// ejecuta un query con los campos que se quieren mostrar en la tabla para despues pasarlo a un datatable
        /// y llenarlo con todos los registros.
        /// </summary>
        /// <returns></returns>
        public DataTable mostrar_gridRefaccion()
        {
            using (Conexion.Conectar())
            {
                DataTable dt = new DataTable();
                string qry = "SELECT idPieza,CAST(ADMINREFACCIONARIA.vehiculo.idVehiculo as varchar)+'_'+numSerie+'_'+CAST(ano as varchar),CAST(ADMINREFACCIONARIA.categoria.idCategoria as varchar)+'-'+ADMINREFACCIONARIA.categoria.nombre,ADMINREFACCIONARIA.refaccion.nombre,descripcion,precioCompra,PrecioVenta,stock FROM ADMINREFACCIONARIA.refaccion INNER JOIN ADMINREFACCIONARIA.vehiculo ON ADMINREFACCIONARIA.vehiculo.idvehiculo=ADMINREFACCIONARIA.refaccion.idvehiculo INNER JOIN ADMINREFACCIONARIA.Categoria ON ADMINREFACCIONARIA.categoria.idcategoria = ADMINREFACCIONARIA.refaccion.idcategoria";
                
                SqlCommand com = new SqlCommand(qry, Conexion.Conectar());

                SqlDataAdapter dat = new SqlDataAdapter(com);

                dat.Fill(dt);
                
                return dt;
            }
        }
        /// <summary>
        /// ejecuta un query con los campos que se quieren mostrar en la tabla para despues pasarlo a un datatable
        /// y llenarlo con todos los registros.
        /// </summary>
        /// <returns></returns>
        public DataTable Mostrar_grid_Compra()
        {
            DataTable dt = new DataTable();
            string qry = "SELECT idCompra,CAST(ADMINREFACCIONARIA.proveedor.idProveedor as varchar)+'-'+ CAST(ADMINREFACCIONARIA.persona.nombre as varchar) +'-' + CAST(ADMINREFACCIONARIA.proveedor.empresa as varchar), fechaCompra, total FROM ADMINREFACCIONARIA.compra INNER JOIN ADMINREFACCIONARIA.proveedor ON ADMINREFACCIONARIA.compra.idProveedor = ADMINREFACCIONARIA.proveedor.idProveedor INNER JOIN ADMINREFACCIONARIA.persona ON ADMINREFACCIONARIA.proveedor.idpersona = ADMINREFACCIONARIA.persona.idpersona ";
            SqlCommand com = new SqlCommand(qry, Conexion.Conectar());
            SqlDataAdapter dat = new SqlDataAdapter(com);
            dat.Fill(dt);

            return dt;
        }
        /// <summary>
        /// ejecuta un query con los campos que se quieren mostrar en la tabla para despues pasarlo a un datatable
        /// y llenarlo con todos los registros.
        /// </summary>
        /// <returns></returns>
        public DataTable Mostrar_grid_empleado()
        {
            DataTable dt = new DataTable();
            string qry = "SELECT idEmpleado,CAST(ADMINREFACCIONARIA.empleado.idPersona as varchar)+'-'+CAST(nombre as varchar),fechaInicio,Antiguedad,rol FROM ADMINREFACCIONARIA.empleado INNER JOIN ADMINREFACCIONARIA.persona ON ADMINREFACCIONARIA.empleado.idpersona = ADMINREFACCIONARIA.persona.idpersona";
            SqlCommand com = new SqlCommand(qry, Conexion.Conectar());
            SqlDataAdapter dat = new SqlDataAdapter(com);
            dat.Fill(dt);

            return dt;
        }
        /// <summary>
        /// ejecuta un query con los campos que se quieren mostrar en la tabla para despues pasarlo a un datatable
        /// y llenarlo con todos los registros.
        /// </summary>
        /// <returns></returns>
        public DataTable Mostrar_grid_venta()
        {
            DataTable dt = new DataTable();
            string qry = "SELECT idVenta,CAST(ADMINREFACCIONARIA.empleado.idEmpleado as varchar)+'-'+ CAST(ADMINREFACCIONARIA.persona.nombre as varchar), estadoDeVenta, total FROM ADMINREFACCIONARIA.Venta INNER JOIN ADMINREFACCIONARIA.empleado ON ADMINREFACCIONARIA.venta.idEmpleado = ADMINREFACCIONARIA.empleado.idEmpleado INNER JOIN ADMINREFACCIONARIA.persona ON ADMINREFACCIONARIA.empleado.idpersona = ADMINREFACCIONARIA.persona.idpersona";
            SqlCommand com = new SqlCommand(qry, Conexion.Conectar());
            SqlDataAdapter dat = new SqlDataAdapter(com);
            dat.Fill(dt);
            return dt;
        }
        /// <summary>
        /// ejecuta un query con los campos que se quieren mostrar en la tabla para despues pasarlo a un datatable
        /// y llenarlo con todos los registros.
        /// </summary>
        /// <returns></returns>
        public DataTable Mostrar_grid_detalleventa()
        {
            DataTable dt = new DataTable();
            string qry = "SELECT idventa,CAST(ADMINREFACCIONARIA.refaccion.idPieza as varchar)+'-'+CAST(ADMINREFACCIONARIA.refaccion.nombre as varchar),cantidad,descuento,subtotal FROM ADMINREFACCIONARIA.detalleventa INNER JOIN ADMINREFACCIONARIA.refaccion ON ADMINREFACCIONARIA.detalleventa.idPieza = ADMINREFACCIONARIA.refaccion.idPieza";
            SqlCommand com = new SqlCommand(qry, Conexion.Conectar());
            SqlDataAdapter dat = new SqlDataAdapter(com);
            dat.Fill(dt);
            return dt;
        }

        private void grid_refaccion_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }
        /// <summary>
        /// botón para eliminar el registro seleccionado mediante el indice que se toma con el cellContentClick 
        /// en este caso es id_refaccion
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btndelpieza_Click(object sender, EventArgs e)
        {
            using (Conexion.Conectar()) { }
            Conexion.Conectar();
            string eliminar = "DELETE FROM ADMINREFACCIONARIA.refaccion WHERE idPieza = @id_refaccion";
            SqlCommand con3 = new SqlCommand(eliminar, Conexion.Conectar());
            con3.Parameters.AddWithValue("@id_refaccion", id_refaccion);
            con3.ExecuteNonQuery();
            limpiar();
            grid_refaccion.DataSource = mostrar_gridRefaccion();
            actualizaListaRefacciones();
        }
        /// <summary>
        /// Botón para modificar la refacción 
        /// seleccionando el registro este toma el indice y se pasa como clave primaria
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnmod_Click(object sender, EventArgs e)
        {
            using (Conexion.Conectar())
            {
                Conexion.Conectar();
                string actualizar = " UPDATE ADMINREFACCIONARIA.refaccion SET nombre = @nombre,descripcion = @descripcion,precioCompra = @precioCompra,precioVenta = @precioVenta,stock = @stock WHERE idPieza = @id_refaccion";

                SqlCommand con2 = new SqlCommand(actualizar, Conexion.Conectar());
                con2.Parameters.AddWithValue("@id_refaccion", id_refaccion);
                con2.Parameters.AddWithValue("@nombre", tbnombrerefaccion.Text);
                con2.Parameters.AddWithValue("@descripcion", rbdesc.Text);
                con2.Parameters.AddWithValue("@preciocompra", tbPreciocp.Text);
                con2.Parameters.AddWithValue("@precioventa",(Int32.Parse(tbPreciocp.Text) * 0.2) + Int32.Parse(tbPreciocp.Text));
                con2.Parameters.AddWithValue("@stock", 0);

                con2.ExecuteNonQuery();
                limpiar();

            }
            grid_refaccion.DataSource = mostrar_gridRefaccion();
            actualizaListaRefacciones();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// Acción para el botón de insertar categoria toma el nombre del textbox y 
        /// lo pasa al query
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            Conexion.Conectar();
            String agregar = "INSERT INTO ADMINREFACCIONARIA.Categoria(nombre)VALUES(@nombre)";
            SqlCommand cmdo1 = new SqlCommand(agregar, Conexion.Conectar());
            cmdo1.Parameters.AddWithValue("@nombre", tbNomCategoria.Text);
            cmdo1.ExecuteNonQuery();
            grid_categoria.DataSource = Mostrar_grid_categoria();
            actualizaListaCategoria();
        }

        //Vehiculo
        //Mostrar tabla de VEHICULO
        public DataTable Mostrar_grid_vehiculo()
        {
            DataTable dt = new DataTable();
            string qry = "SELECT * FROM ADMINREFACCIONARIA.Vehiculo";
            SqlCommand com = new SqlCommand(qry, Conexion.Conectar());

            SqlDataAdapter dat = new SqlDataAdapter(com);

            dat.Fill(dt);
            return dt;
        }
        /// <summary>
        /// Mostrar la tabla categoria con sus respectivos registros
        /// </summary>
        /// <returns></returns>
        public DataTable Mostrar_grid_categoria()
        {
            DataTable dt = new DataTable();
            string qry = "SELECT * FROM ADMINREFACCIONARIA.Categoria";
            SqlCommand com = new SqlCommand(qry, Conexion.Conectar());
            SqlDataAdapter dat = new SqlDataAdapter(com);
            dat.Fill(dt);
            return dt;
        }
        /// <summary>
        /// mostrar la lista de proveedores mediante un query de select
        /// ademas se agregan campos concatenados de la tabla persona para la
        /// clave foranea
        /// </summary>
        /// <returns></returns>
        public DataTable Mostrar_grid_proveedor()
        {
            DataTable dt = new DataTable();
            
            string qry = "SELECT idProveedor,CAST(ADMINREFACCIONARIA.proveedor.idPersona as varchar)+'-'+CAST(nombre as varchar),empresa,giro,gerente FROM ADMINREFACCIONARIA.proveedor INNER JOIN ADMINREFACCIONARIA.persona ON ADMINREFACCIONARIA.proveedor.idpersona = ADMINREFACCIONARIA.persona.idpersona";
            SqlCommand com = new SqlCommand(qry, Conexion.Conectar());
            SqlDataAdapter dat = new SqlDataAdapter(com);
            dat.Fill(dt);
            
            return dt;
            
        }

        //Agregar VEHICULO
        private void BtAgregar_vehiculo_Click(object sender, EventArgs e)
        {
            Conexion.Conectar();
            String agregar = "INSERT INTO ADMINREFACCIONARIA.vehiculo(numSerie,nombreMarca,nombreModelo,ano,descTecnica)VALUES(@numSerie,@nombreMarca,@nombreModelo,@ano,@descTecnica)";
            SqlCommand cmdo1 = new SqlCommand(agregar, Conexion.Conectar());

            cmdo1.Parameters.AddWithValue("@numSerie", TxtNumSerie.Text);
            cmdo1.Parameters.AddWithValue("@nombreMarca", TxtNombreMarca.Text);
            cmdo1.Parameters.AddWithValue("@nombreModelo", TxtNombreModelo.Text);
            cmdo1.Parameters.AddWithValue("@ano", TxtAno.Text);
            cmdo1.Parameters.AddWithValue("@descTecnica", TxtDescTecnica.Text);

            cmdo1.ExecuteNonQuery();

            //MessageBox.Show("Se agrego");

            grid_vehiculo.DataSource = Mostrar_grid_vehiculo();
            actualizaListaVehiculo();
            actualizaListaRefacciones();
            TxtNumSerie.Text = "";
            TxtNombreMarca.Text = "";
            TxtNombreModelo.Text = "";
            TxtAno.Text = "";
            TxtDescTecnica.Text = "";
        }
        //Llenar campos al hacer click VEHICULO
        private void grid_vehiculo_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                TxtNumSerie.Text = grid_vehiculo.CurrentRow.Cells[1].Value.ToString();
                TxtNombreMarca.Text = grid_vehiculo.CurrentRow.Cells[2].Value.ToString();
                TxtNombreModelo.Text = grid_vehiculo.CurrentRow.Cells[3].Value.ToString();
                TxtAno.Text = grid_vehiculo.CurrentRow.Cells[4].Value.ToString();
                TxtDescTecnica.Text = grid_vehiculo.CurrentRow.Cells[5].Value.ToString();

            }
            catch
            {
            }
        }
        //Modificar VEHICULO
        private void BtModificarVehiculo_Click(object sender, EventArgs e)
        {
            Conexion.Conectar();
            String modificar = "UPDATE ADMINREFACCIONARIA.vehiculo SET numSerie=@numSerie, nombreMarca=@nombreMarca, nombreModelo=@nombreModelo, ano=@ano, descTecnica=@descTecnica WHERE numSerie=@numSerie";
            SqlCommand cmdo2 = new SqlCommand(modificar, Conexion.Conectar());

            cmdo2.Parameters.AddWithValue("@numSerie", TxtNumSerie.Text);
            cmdo2.Parameters.AddWithValue("@nombreMarca", TxtNombreMarca.Text);
            cmdo2.Parameters.AddWithValue("@nombreModelo", TxtNombreModelo.Text);
            cmdo2.Parameters.AddWithValue("@ano", TxtAno.Text);
            cmdo2.Parameters.AddWithValue("@descTecnica", TxtDescTecnica.Text);

            cmdo2.ExecuteNonQuery();

            //MessageBox.Show("Se actualizaron los datos");
            grid_vehiculo.DataSource = Mostrar_grid_vehiculo();
            actualizaListaVehiculo();

            TxtNumSerie.Text = "";
            TxtNombreMarca.Text = "";
            TxtNombreModelo.Text = "";
            TxtAno.Text = "";
            TxtDescTecnica.Text = "";
        }
        //Eliminar VEHICULO
        private void BtEliminarVehiculo_Click(object sender, EventArgs e)
        {
            Conexion.Conectar();
            String eliminar = "DELETE FROM ADMINREFACCIONARIA.vehiculo WHERE numSerie=@numSerie";
            SqlCommand cmdo3 = new SqlCommand(eliminar, Conexion.Conectar());

            cmdo3.Parameters.AddWithValue("@numSerie", TxtNumSerie.Text);

            cmdo3.ExecuteNonQuery();

            //MessageBox.Show("Eliminado");

            grid_vehiculo.DataSource = Mostrar_grid_vehiculo();
            actualizaListaVehiculo();

            TxtNumSerie.Text = "";
            TxtNombreMarca.Text = "";
            TxtNombreModelo.Text = "";
            TxtAno.Text = "";
            TxtDescTecnica.Text = "";
        }

        //Persona
        //Llenar campos al hacer click PERSONA
        private void grid_persona_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                TxtRFCPersona.Text = grid_persona.CurrentRow.Cells[1].Value.ToString();
                TxtNombrePersona.Text = grid_persona.CurrentRow.Cells[2].Value.ToString();
                TxtCorreoPersona.Text = grid_persona.CurrentRow.Cells[3].Value.ToString();
                TxtTelefonoPersona.Text = grid_persona.CurrentRow.Cells[4].Value.ToString();
                TxtDireccionPersona.Text = grid_persona.CurrentRow.Cells[5].Value.ToString();
                TxtCPPersona.Text = grid_persona.CurrentRow.Cells[6].Value.ToString();
                TxtEstadoPersona.Text = grid_persona.CurrentRow.Cells[7].Value.ToString();
            }
            catch
            {
            }
        }
        //mostrar tabla de PERSONA
        public DataTable Mostrar_grid_persona()
        {
            DataTable dt = new DataTable();
            string qry = "SELECT * FROM ADMINREFACCIONARIA.persona";
            SqlCommand com = new SqlCommand(qry, Conexion.Conectar());

            SqlDataAdapter dat = new SqlDataAdapter(com);

            dat.Fill(dt);
            return dt;
        }
        /// <summary>
        /// Muestra todos los detalles de compra en el Datagrid de la pestaña detalle
        /// de compra.
        /// </summary>
        /// <returns></returns>
        public DataTable Mostrar_grid_detalleCompra()
        {
            DataTable dt = new DataTable();
            string qry = "SELECT idCompra,CAST(ADMINREFACCIONARIA.refaccion.idPieza as varchar)+'-'+CAST(ADMINREFACCIONARIA.refaccion.nombre as varchar),cantidad,subtotal FROM ADMINREFACCIONARIA.detallecompra INNER JOIN ADMINREFACCIONARIA.refaccion ON ADMINREFACCIONARIA.detallecompra.idPieza = ADMINREFACCIONARIA.refaccion.idPieza"; 
            SqlCommand com = new SqlCommand(qry, Conexion.Conectar());
            SqlDataAdapter dat = new SqlDataAdapter(com);
            dat.Fill(dt);
            return dt;
        }
        //Agregar PERSONA
        private void BtAgregarPersona_Click(object sender, EventArgs e)
        {
            Conexion.Conectar();
            String agregar = "INSERT INTO ADMINREFACCIONARIA.persona(rfc,nombre,correo,telefono,direccion,cp,estado)VALUES(@rfc,@nombre,@correo,@telefono,@direccion,@cp,@estado)";
            SqlCommand cmdo4 = new SqlCommand(agregar, Conexion.Conectar());
            //parametros a insertar
            cmdo4.Parameters.AddWithValue("@rfc", TxtRFCPersona.Text);
            cmdo4.Parameters.AddWithValue("@nombre", TxtNombrePersona.Text);
            cmdo4.Parameters.AddWithValue("@correo", TxtCorreoPersona.Text);
            cmdo4.Parameters.AddWithValue("@telefono", TxtTelefonoPersona.Text);
            cmdo4.Parameters.AddWithValue("@direccion", TxtDireccionPersona.Text);
            cmdo4.Parameters.AddWithValue("@cp", TxtCPPersona.Text);
            cmdo4.Parameters.AddWithValue("@estado", TxtEstadoPersona.Text);

            cmdo4.ExecuteNonQuery();

           

            grid_persona.DataSource = Mostrar_grid_persona();

            TxtRFCPersona.Text = "";
            TxtNombrePersona.Text = "";
            TxtCorreoPersona.Text = "";
            TxtTelefonoPersona.Text = "";
            TxtDireccionPersona.Text = "";
            TxtCPPersona.Text = "";
            TxtEstadoPersona.Text = "";

            actualizaListaPersonas();

        }
        //Modificar PERSONA
        private void BtModificarPersona_Click(object sender, EventArgs e)
        {
            Conexion.Conectar();
            //(rfc,nombre,correo,telefono,direccion,cp,estado)VALUES(@rfc,@nombre,@correo,@telefono,@direccion,@cp,@estado)";
            String modificar = "UPDATE ADMINREFACCIONARIA.persona SET rfc=@rfc, nombre=@nombre, correo=@correo, telefono=@telefono, direccion=@direccion, cp=@cp, estado=@estado WHERE rfc=@rfc";
            SqlCommand cmdo5 = new SqlCommand(modificar, Conexion.Conectar());

            cmdo5.Parameters.AddWithValue("@rfc", TxtRFCPersona.Text);
            cmdo5.Parameters.AddWithValue("@nombre", TxtNombrePersona.Text);
            cmdo5.Parameters.AddWithValue("@correo", TxtCorreoPersona.Text);
            cmdo5.Parameters.AddWithValue("@telefono", TxtTelefonoPersona.Text);
            cmdo5.Parameters.AddWithValue("@direccion", TxtDireccionPersona.Text);
            cmdo5.Parameters.AddWithValue("@cp", TxtCPPersona.Text);
            cmdo5.Parameters.AddWithValue("@estado", TxtEstadoPersona.Text);

            cmdo5.ExecuteNonQuery();

            MessageBox.Show("Se actualizaron los datos");
            grid_persona.DataSource = Mostrar_grid_persona();

            TxtRFCPersona.Text = "";
            TxtNombrePersona.Text = "";
            TxtCorreoPersona.Text = "";
            TxtTelefonoPersona.Text = "";
            TxtDireccionPersona.Text = "";
            TxtCPPersona.Text = "";
            TxtEstadoPersona.Text = "";

            actualizaListaPersonas();

        }
        //Eliminar PERSONA
        private void BtEliminarPersona_Click(object sender, EventArgs e)
        {
            Conexion.Conectar();
            String eliminar = "DELETE FROM ADMINREFACCIONARIA.persona WHERE rfc=@rfc";
            SqlCommand cmdo6 = new SqlCommand(eliminar, Conexion.Conectar());

            cmdo6.Parameters.AddWithValue("@rfc", TxtRFCPersona.Text);

            cmdo6.ExecuteNonQuery();

            MessageBox.Show("Eliminado");

            grid_persona.DataSource = Mostrar_grid_persona();

            TxtRFCPersona.Text = "";
            TxtNombrePersona.Text = "";
            TxtCorreoPersona.Text = "";
            TxtTelefonoPersona.Text = "";
            TxtDireccionPersona.Text = "";
            TxtCPPersona.Text = "";
            TxtEstadoPersona.Text = "";

            actualizaListaPersonas();
        }
        /// <summary>
        /// Al hacer click 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grid_refaccion_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                id_refaccion = Convert.ToInt32(grid_refaccion.CurrentRow.Cells[0].Value);

                //tbidvehiculofk.Text = grid_refaccion.CurrentRow.Cells[1].Value.ToString();
                //tbidecatfk.Text = grid_refaccion.CurrentRow.Cells[2].Value.ToString();
                tbnombrerefaccion.Text = grid_refaccion.CurrentRow.Cells[3].Value.ToString();
                rbdesc.Text = grid_refaccion.CurrentRow.Cells[4].Value.ToString();
                tbPreciocp.Text = grid_refaccion.CurrentRow.Cells[5].Value.ToString();
                //tbPreciovt.Text = grid_refaccion.CurrentRow.Cells[6].Value.ToString();
                //tbStock.Text = grid_refaccion.CurrentRow.Cells[9].Value.ToString();
            }
            catch
            {

            }
        }

        private void grid_vehiculo_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void grid_refaccion_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {

        }

        private void btnModCat_Click(object sender, EventArgs e)
        {

            actualizaListaCategoria();
        }
        /// <summary>
        /// borrar la categoria seleccionada
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelCat_Click(object sender, EventArgs e)
        {
            Conexion.Conectar();
            String eliminar = "DELETE FROM ADMINREFACCIONARIA.categoria WHERE idcategoria=@idcategoria";
            SqlCommand cmdo3 = new SqlCommand(eliminar, Conexion.Conectar());

            cmdo3.Parameters.AddWithValue("@idcategoria", id_categoria);

            cmdo3.ExecuteNonQuery();
            actualizaListaCategoria();
        }

        private void tabPage6_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// insertar nuevo proveedor
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click_1(object sender, EventArgs e)
        {
            Conexion.Conectar();
            //query insertar
            String agregar = "INSERT INTO ADMINREFACCIONARIA.proveedor(idPersona,empresa,giro,gerente)VALUES(@idPersona,@empresa,@giro,@gerente)";
            SqlCommand cmdo1 = new SqlCommand(agregar, Conexion.Conectar());
            //agregar parametros
            cmdo1.Parameters.AddWithValue("@idPersona", comboBox3.Text.Split('-')[0].Trim());
            
            cmdo1.Parameters.AddWithValue("@empresa", tBEmpresa.Text);
            cmdo1.Parameters.AddWithValue("@giro", tBGiro.Text);
            cmdo1.Parameters.AddWithValue("@gerente", tBGerente.Text);
            cmdo1.ExecuteNonQuery();
            actualizaListaProveedores();
            gridProveedor.DataSource = Mostrar_grid_proveedor();   
        }
        /// <summary>
        /// actualizar campo de proveedor
        /// requiere selección en el datagrid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            Conexion.Conectar();
            //query actualizar
            String modificar = "UPDATE ADMINREFACCIONARIA.proveedor SET  idPersona=@idPersona, nombre=@nombre, empresa=@empresa,giro=@giro,gerente=@gerente WHERE idProveedor=@idProveedor";
            SqlCommand cmdo2 = new SqlCommand(modificar, Conexion.Conectar());
            //parametros a insertar
            cmdo2.Parameters.AddWithValue("@idProveedor", id_proveedor);
            cmdo2.Parameters.AddWithValue("@idPersona", comboBox3.Text.Split('-')[0].Trim());
            cmdo2.Parameters.AddWithValue("@empresa", tBEmpresa.Text);
            cmdo2.Parameters.AddWithValue("@giro", tBGiro.Text);
            cmdo2.Parameters.AddWithValue("@gerente", tBGerente.Text);

            cmdo2.ExecuteNonQuery();
            actualizaListaProveedores();
            //MessageBox.Show("Se actualizaron los datos");
            gridProveedor.DataSource = Mostrar_grid_proveedor();
            actualizaListaVehiculo();
            tBEmpresa.Text = "";
            tBGerente.Text = "";
            tBGiro.Text = "";
        }
        /// <summary>
        /// eliminar proveedor
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {

            Conexion.Conectar();
            //query para eliminar
            string eliminar = "DELETE FROM ADMINREFACCIONARIA.proveedor WHERE idProveedor = @id_proveedor";
            SqlCommand con3 = new SqlCommand(eliminar, Conexion.Conectar());
            //indice como parametro
            con3.Parameters.AddWithValue("@id_proveedor", id_proveedor);
            con3.ExecuteNonQuery();
            limpiar(); 
            //actualizar combobox
            actualizaListaProveedores();
            gridProveedor.DataSource = Mostrar_grid_proveedor();
        }
        /// <summary>
        /// llenar campos con registros seleccionado y tomar el indice
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridProveedor_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                id_proveedor = Convert.ToInt32(gridProveedor.CurrentRow.Cells[0].Value);
                tBEmpresa.Text = gridProveedor.CurrentRow.Cells[2].Value.ToString();
                tBGiro.Text = gridProveedor.CurrentRow.Cells[3].Value.ToString();
                tBGerente.Text = gridProveedor.CurrentRow.Cells[4].Value.ToString();
            }
            catch
            {

            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// agregar nuevo empleado presionando botón y llenando 
        /// completamente los campos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAgregarEmpleado_Click(object sender, EventArgs e)
        {   
            Conexion.Conectar();
            String agregar = "INSERT INTO ADMINREFACCIONARIA.empleado(idPersona,fechaInicio,Antiguedad,rol)VALUES(@idPersona,@fechaInicio,@Antiguedad,@rol)";
            SqlCommand cmdo1 = new SqlCommand(agregar, Conexion.Conectar());
            cmdo1.Parameters.AddWithValue("@idPersona", cBPersona2.Text.Split('-')[0].Trim());
            
            cmdo1.Parameters.AddWithValue("@fechaInicio", dTEmpleado.Value);
            cmdo1.Parameters.AddWithValue("@Antiguedad", 0);
            cmdo1.Parameters.AddWithValue("@Rol", cBRol.Text);
            cmdo1.ExecuteNonQuery();
            actualizaListaEmpleados();
            gridEmpleado.DataSource = Mostrar_grid_empleado();
            
        }
        /// <summary>
        /// llenar campos de empleado si se selecciona en el grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridEmpleado_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            id_empleado = Convert.ToInt32(gridEmpleado.CurrentRow.Cells[0].Value);
            cBPersona2.Text = gridEmpleado.CurrentRow.Cells[1].Value.ToString();
            cBRol.Text = gridEmpleado.CurrentRow.Cells[4].Value.ToString();
            
        }
        /// <summary>
        /// eliminar empleado seleccionado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEliminarEmpleado_Click(object sender, EventArgs e)
        {
            Conexion.Conectar();
            try
            {
                string eliminar = "DELETE FROM ADMINREFACCIONARIA.empleado WHERE idEmpleado = @id_empleado";
                SqlCommand con3 = new SqlCommand(eliminar, Conexion.Conectar());
                con3.Parameters.AddWithValue("@id_empleado", id_empleado);
                con3.ExecuteNonQuery();
                actualizaListaEmpleados();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
            
            gridEmpleado.DataSource = Mostrar_grid_empleado();
        }
        /// <summary>
        /// modificación de empleado seleccionado en el
        /// datagrid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnModificarEmpleado_Click(object sender, EventArgs e)
        {
            Conexion.Conectar();
            //query actualizar
            String modificar = "UPDATE ADMINREFACCIONARIA.empleado SET  idPersona=@idPersona, fechaInicio=@fechaInicio,Antiguedad=@Antiguedad,rol=@Rol WHERE idEmpleado=@idEmpleado";
            SqlCommand cmdo2 = new SqlCommand(modificar, Conexion.Conectar());
            //parametros a insertar
            cmdo2.Parameters.AddWithValue("@idEmpleado", id_empleado);
            cmdo2.Parameters.AddWithValue("@idPersona", cBPersona2.Text.Split('-')[0].Trim());
            cmdo2.Parameters.AddWithValue("@fechaInicio", dTEmpleado.Value);
            cmdo2.Parameters.AddWithValue("@Antiguedad", 1);
            cmdo2.Parameters.AddWithValue("@Rol", cBRol.Text);
            actualizaListaEmpleados();
            cmdo2.ExecuteNonQuery();

            //MessageBox.Show("Se actualizaron los datos");
            //actualizar grid y listas en combobox
            gridEmpleado.DataSource = Mostrar_grid_empleado();
            actualizaListaEmpleados();
        }
        /// <summary>
        /// insertar una compra
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            Conexion.Conectar();
            String agregar = "INSERT INTO ADMINREFACCIONARIA.Compra(idProveedor,fechaCompra,total)VALUES(@idProveedor,@fechaCompra,0000)";
            SqlCommand cmdo1 = new SqlCommand(agregar, Conexion.Conectar());
            //parametros a insertar
            cmdo1.Parameters.AddWithValue("@idProveedor", cBProveedores.Text.Split('-')[0].Trim());
            cmdo1.Parameters.AddWithValue("@fechaCompra", dtCompra.Value);
            cmdo1.ExecuteNonQuery();
            actualizaListaCompras();
            gridCompra.DataSource = Mostrar_grid_Compra();
        }
        /// <summary>
        /// eliminar compra
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                string eliminar = "DELETE FROM ADMINREFACCIONARIA.compra WHERE idCompra = @id_compra";
                SqlCommand con3 = new SqlCommand(eliminar, Conexion.Conectar());
                con3.Parameters.AddWithValue("@id_compra", id_compra);
                con3.ExecuteNonQuery();
                actualizaListaCompras();
                gridCompra.DataSource = Mostrar_grid_Compra();
            }catch(SqlException ex)
            {
                MessageBox.Show("Hay registros asociados a este contenido");
            }
           
        }
        /// <summary>
        /// llenar textbox en la pestaña de compra 
        /// al seleccionar un registro en el datagrid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridCompra_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                id_compra = Convert.ToInt32(gridCompra.CurrentRow.Cells[0].Value);
                cBProveedores.Text = gridCompra.CurrentRow.Cells[1].Value.ToString();
            }
            catch
            {

            }
        }
        /// <summary>
        /// actualizar una compra seleccionada 
        /// en el data grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button6_Click(object sender, EventArgs e)
        {
            
            String modificar = "UPDATE ADMINREFACCIONARIA.compra SET  idProveedor=@idProveedor, fechaCompra=@fechaCompra WHERE idCompra=@idCompra";
            SqlCommand cmdo2 = new SqlCommand(modificar, Conexion.Conectar());
            //parametros a insertar
            cmdo2.Parameters.AddWithValue("@idCompra", id_compra);
            cmdo2.Parameters.AddWithValue("@idProveedor", cBProveedores.Text.Split('-')[0].Trim());           
            cmdo2.Parameters.AddWithValue("@fechaCompra", dtCompra.Value);
            
            cmdo2.ExecuteNonQuery();
            //actualizar combobox y datagrid
            actualizaListaCompras();
            gridCompra.DataSource = Mostrar_grid_Compra();
            
        }
        /// <summary>
        /// insertar venta nueva en la base de datos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button7_Click(object sender, EventArgs e)
        {
            Conexion.Conectar();
            String agregar = "INSERT INTO ADMINREFACCIONARIA.Venta(idEmpleado,estadoDeVenta,total)VALUES(@idEmpleado,@estadoDeVenta,@total)";
            SqlCommand cmdo1 = new SqlCommand(agregar, Conexion.Conectar());
            //parametros a insertar en la consulta
            cmdo1.Parameters.AddWithValue("@idEmpleado", cBEmpleados.Text.Split('-')[0].Trim());
            cmdo1.Parameters.AddWithValue("@estadoDeVenta", cBEstado.Text);
            cmdo1.Parameters.AddWithValue("@total",0);
            cmdo1.ExecuteNonQuery();
            actualizaListaVentas();
            gridVenta.DataSource = Mostrar_grid_venta();
        }
        /// <summary>
        /// tomar campos de el grid de ventas y el indice que se va a tomar como 
        /// clave primaria ya sea para eliminar o modificar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridVenta_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                id_venta = Convert.ToInt32(gridVenta.CurrentRow.Cells[0].Value);
                cBEmpleados.Text = gridVenta.CurrentRow.Cells[1].Value.ToString();
                cBEstado.Text = gridVenta.CurrentRow.Cells[2].Value.ToString();
            }
            catch
            {

            }
        }
        /// <summary>
        /// eliminar venta
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                //query borrar
                string eliminar = "DELETE FROM ADMINREFACCIONARIA.venta WHERE idVenta = @id_venta";
                SqlCommand con3 = new SqlCommand(eliminar, Conexion.Conectar());
                //parametro de busqueda
                con3.Parameters.AddWithValue("@id_venta", id_venta);
                con3.ExecuteNonQuery();
                actualizaListaVentas();
                gridVenta.DataSource = Mostrar_grid_venta();
            }catch(SqlException ex)
            {
                MessageBox.Show("Hay registros asociados");
            }
           
        }
        /// <summary>
        /// actualizar venta
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button9_Click(object sender, EventArgs e)
        {
            //query actualizar
            String modificar = "UPDATE ADMINREFACCIONARIA.venta SET  idEmpleado=@idEmpleado, estadoDeVenta=@estadoDeVenta WHERE idVenta=@idVenta";
            SqlCommand cmdo2 = new SqlCommand(modificar, Conexion.Conectar());
            //parametros a insertar en la consulta
            cmdo2.Parameters.AddWithValue("@idVenta", id_venta);
            cmdo2.Parameters.AddWithValue("@idEmpleado", cBEmpleados.Text.Split('-')[0].Trim());
            cmdo2.Parameters.AddWithValue("@estadoDeVenta", cBEstado.Text);
            

            cmdo2.ExecuteNonQuery();
            actualizaListaVentas();
            gridVenta.DataSource = Mostrar_grid_venta();
        }

        private void tabPage10_Click(object sender, EventArgs e)
        {

        }

        private void tabPage9_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// actualizar detalle de venta - seleccionar registro en datagrid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button12_Click(object sender, EventArgs e)
        {
            String modificar = "UPDATE ADMINREFACCIONARIA.detalleVenta SET idPieza= @idPieza, idVenta=@idVenta, cantidad = @cantidad, descuento=@descuento WHERE idPieza = @idPieza and idVenta=@idVenta";
            SqlCommand con3 = new SqlCommand(modificar, Conexion.Conectar());
            //parametros a insertar en la consulta
            con3.Parameters.AddWithValue("@idVenta", cBVentas.Text);
            con3.Parameters.AddWithValue("@idPieza", cBRefacciones.Text.Split('-')[0].Trim());
            con3.Parameters.AddWithValue("@cantidad", Int32.Parse(tbCantidad.Text));
            con3.Parameters.AddWithValue("@descuento", Int32.Parse(tbDescuento.Text));

            con3.ExecuteNonQuery();
            gridDetalleVentas.DataSource = Mostrar_grid_detalleventa();
            gridVenta.DataSource = Mostrar_grid_venta();
            grid_refaccion.DataSource = mostrar_gridRefaccion();

        }
        /// <summary>
        /// insertar nuevo detalle de venta
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                Conexion.Conectar();
                String agregar = "INSERT INTO ADMINREFACCIONARIA.detalleVenta(idVenta,idPieza,cantidad,descuento)VALUES(@idVenta,@idPieza,@cantidad,@descuento)";
                SqlCommand cmdo1 = new SqlCommand(agregar, Conexion.Conectar());
                //parametros para insertar en un detalled de venta
                cmdo1.Parameters.AddWithValue("@idVenta", cBVentas.Text.Split('-')[0].Trim());
                cmdo1.Parameters.AddWithValue("@idPieza", cBRefacciones.Text.Split('-')[0].Trim());
                cmdo1.Parameters.AddWithValue("@cantidad", tbCantidad.Text);
                cmdo1.Parameters.AddWithValue("@descuento", tbDescuento.Text);
                cmdo1.ExecuteNonQuery();
                actualizaListaVentas();
                gridDetalleVentas.DataSource = Mostrar_grid_detalleventa();
                gridVenta.DataSource = Mostrar_grid_venta();
                grid_refaccion.DataSource = mostrar_gridRefaccion();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("No hay suficiente stock");
            
            }

        }
        /// <summary>
        /// agrega un nuevo registro 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button13_Click(object sender, EventArgs e)
        {
            Conexion.Conectar();
            String agregar = "INSERT INTO ADMINREFACCIONARIA.detalleCompra(idCompra,idPieza,cantidad)VALUES(@idCompra,@idPieza,@cantidad)";
            SqlCommand cmdo1 = new SqlCommand(agregar, Conexion.Conectar());
            //agregar parametros
            cmdo1.Parameters.AddWithValue("@idCompra", cBCompras.Text.Split('-')[0].Trim());
            cmdo1.Parameters.AddWithValue("@idPieza", cBRefacciones2.Text.Split('-')[0].Trim());
            cmdo1.Parameters.AddWithValue("@cantidad", tBCantidad2.Text);
            cmdo1.ExecuteNonQuery();
            //actualizar tablas y combobox
            gridDetalleCompra.DataSource = Mostrar_grid_detalleCompra();
            gridCompra.DataSource = Mostrar_grid_Compra();
            grid_refaccion.DataSource = mostrar_gridRefaccion();

        }
        /// <summary>
        /// al seleccionar una celda se carga los textbox con el registro seleccionado
        ///    ya sea para modificar o en caso de eliminar se toma el id del registro.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridDetalleCompra_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            cBRefacciones2.Text = gridDetalleCompra.CurrentRow.Cells[1].Value.ToString();
            cBCompras.Text = gridDetalleCompra.CurrentRow.Cells[0].Value.ToString();
            tBCantidad2.Text = gridDetalleCompra.CurrentRow.Cells[2].Value.ToString();
        }
        /// <summary>
        /// borra el registro seleccionado en el datagrid y presionando el botón 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button14_Click(object sender, EventArgs e)
        {
            string eliminar = "DELETE FROM ADMINREFACCIONARIA.detalleCompra WHERE idPieza = @idPieza AND idCompra = @idCompra";
            SqlCommand con3 = new SqlCommand(eliminar, Conexion.Conectar());  
            con3.Parameters.AddWithValue("@idPieza", cBRefacciones2.Text.Split('-')[0].Trim());
            con3.Parameters.AddWithValue("@idCompra", cBCompras.Text);
            con3.ExecuteNonQuery();
            gridDetalleCompra.DataSource = Mostrar_grid_detalleCompra();
            gridCompra.DataSource = Mostrar_grid_Compra();
        }
        /// <summary>
        /// actualizar el detalle de venta al presionar botón 
        /// actualiza campos idcompra idpieza y cantidad
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button15_Click(object sender, EventArgs e)
        {
            String modificar = "UPDATE ADMINREFACCIONARIA.detalleCompra SET idPieza= @idPieza, idCompra=@idCompra, Cantidad = @Cantidad WHERE idPieza = @idPieza and idCompra=@idCompra";
            SqlCommand con3 = new SqlCommand(modificar, Conexion.Conectar());

            con3.Parameters.AddWithValue("@idCompra", cBCompras.Text);
            con3.Parameters.AddWithValue("@idPieza", cBRefacciones2.Text.Split('-')[0].Trim());
            con3.Parameters.AddWithValue("@cantidad", Int32.Parse(tBCantidad2.Text));

            con3.ExecuteNonQuery();
            actualizaListaCompras();
            gridDetalleCompra.DataSource = Mostrar_grid_detalleCompra();
            gridCompra.DataSource = Mostrar_grid_Compra();
        }
        /// <summary>
        /// eliminar el registro seleccionado en detalle de venta al presionar el botón de 
        /// eliminar ejecuta una sentencia delete tomando como valor el id de la refacción 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button11_Click(object sender, EventArgs e)
        {
            string eliminar = "DELETE FROM ADMINREFACCIONARIA.detalleVenta WHERE idPieza = @idPieza AND idVenta = @idVenta ";
            SqlCommand con3 = new SqlCommand(eliminar, Conexion.Conectar());
            con3.Parameters.AddWithValue("@idPieza", cBRefacciones.Text.Split('-')[0].Trim());
            con3.Parameters.AddWithValue("@idVenta", cBVentas.Text);
            con3.ExecuteNonQuery();
            
            gridDetalleVentas.DataSource = Mostrar_grid_detalleventa();
            gridVenta.DataSource = Mostrar_grid_venta();
            grid_refaccion.DataSource = mostrar_gridRefaccion();

        }
        /// <summary>
        /// al seleccionar una celda se carga los textbox con el registro seleccionado 
        /// ya sea para modificar o en caso de eliminar se toma el id del registro.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridDetalleVentas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            cBRefacciones.Text = gridDetalleVentas.CurrentRow.Cells[1].Value.ToString();
            cBVentas.Text = gridDetalleVentas.CurrentRow.Cells[0].Value.ToString();
            tbCantidad.Text = gridDetalleVentas.CurrentRow.Cells[2].Value.ToString();
            tbDescuento.Text = gridDetalleVentas.CurrentRow.Cells[3].Value.ToString();
        }
        /// <summary>
        /// llenar campos y tomar indice para categoria
        /// según el registro seleccionado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grid_categoria_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            id_categoria = Convert.ToInt32(grid_categoria.CurrentRow.Cells[0].Value);
            tbNomCategoria.Text =grid_categoria.CurrentRow.Cells[1].Value.ToString();
        }
    }
}
