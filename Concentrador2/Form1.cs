using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql;
using MySql.Data.MySqlClient;

namespace Concentrador2
{
    public partial class Form1 : Form
    {
        string mCuenta, mOperacion, mFeabono, mhora, mDescuento, mConcepto, mCantidad, mDescribe, mrecibo, mReciboc, mUsuario, mVale, mAutorizo, mCaja, mEfectivo, mFormapago, mCheque, mBanco, mDependencia, mregistro, minteres, mSaldoact, mSaldoant, mtipoAbono, mNotCredito, mFolDevolucion, mCaptura, mSucOrigen, mTimbrado, mDescribeDesc, mFicha, mBancoF, mnumeroF, mParcialidad;

        string CuentaAnt, Cuenta, Nombre, Paterno, Materno, Fenacimiento, Domicilio, Interior, Exterior, Cruza, Colonia, Sector, poblacion, municipio, codpos, ladacasa, telcasa, ladacel, telcel, ladaotros, teltrabajo, Ladatrabajo, telotros, limcred, cobrador, diacobro, tipocli, activo, Restric, fealta, tipcobro, conginteres, fecongelacion, usufecongelacion, firmadigital, ferestric, ife, ctaaval, ctaFactura, estado, usuario, ctarestric, usufecompro, fecompromiso, observaciones, descuento, zona, Registro, Tarjeta, c_interes, NombreAval, DomicilioAval, TelAval, completa, FeUltModificacion, UsuUltmodificacion, FechaVale, UsuarioVale, ContadorRpt1, ContadorRpt2, BloquearLimite, TarjCred, ModificaPedido, CodCancelTarjeta, Correo;
        int Plazo, confallida;
        decimal Importe, Enganche, AbonoMen, Saldoact, Saldoant, Interes, MensPagare;
        string servidor;
        string Cuentaold, cuenta, CtaAdicional, Fecompra, Operacion, Vence, Captura, Ultpago, Liquidacion, Tipo, Usuario, sucorigen, fecha_int, Bonificaciones;


        private void btnmovtos_Click(object sender, EventArgs e)
        {
            limpiarTabla("clientes");
            actualizarClientes(1);
            actualizarClientes(2);
            actualizarClientes(3);
            actualizarClientes(4);
            limpiarTabla("cargos");
            actualizarCargos(1);
            actualizarCargos(2);
            actualizarCargos(3);
            actualizarCargos(4);
            limpiarTabla("movtos");
            actualizarMovtos(2);
            actualizarMovtos(3);
            actualizarMovtos(4);
            actualizarMovtos(1);
        }
        private void btmatq_Click(object sender, EventArgs e)
        {
            actualizarClientes(2);
        }

        private void btmixt_Click(object sender, EventArgs e)
        {
            actualizarClientes(3);
        }

        private void btmtla_Click(object sender, EventArgs e)
        {
            actualizarClientes(4);
        }

        private void btmgdl_Click(object sender, EventArgs e)
        {
            actualizarClientes(1);
            
        }

        private void btnatq_Click(object sender, EventArgs e)
        {
            actualizarCargos(2);
        }

        private void btnixt_Click(object sender, EventArgs e)
        {
            actualizarCargos(3);
        }

        private void btntla_Click(object sender, EventArgs e)
        {
            actualizarCargos(4);
        }

        private void btngdl_Click(object sender, EventArgs e)
        {
            actualizarCargos(1);
        }

        

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Refresh();
            //limpiarTabla("clientes");
            //limpiarTabla("cargos");
            //limpiarTabla("movtos");
            //actualizarClientes(1);
            //actualizarClientes(2);
            //actualizarClientes(3);
            //actualizarClientes(4);
            //actualizarCargos(1);
            //actualizarCargos(2);
            //actualizarCargos(3);
            //actualizarCargos(4);
            //actualizarMovtos(2);
            //actualizarMovtos(3);
            //actualizarMovtos(4);
            //actualizarMovtos(1);
        }

        public String GetServidor(int op)
        {
            switch (op)
            {
                case 1:
                    try
                    {

                        servidor = ConfigurationManager.ConnectionStrings["Concentrador2.Properties.Settings.dbCreditoConexion"].ConnectionString.ToString();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("ERROR DE CONEXIÓN: " + ex.Message, "CASA GUERRERO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;
                case 2:
                    try
                    {
                        servidor = ConfigurationManager.ConnectionStrings["Concentrador2.Properties.Settings.dbCreditoConexionATQ"].ConnectionString.ToString();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("ERROR DE CONEXIÓN: " + ex.Message, "CASA GUERRERO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;
                case 3:
                    try
                    {
                        servidor = ConfigurationManager.ConnectionStrings["Concentrador2.Properties.Settings.dbCreditoConexionIXT"].ConnectionString.ToString();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("ERROR DE CONEXIÓN: " + ex.Message, "CASA GUERRERO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;
                case 4:
                    try
                    {
                        servidor = ConfigurationManager.ConnectionStrings["Concentrador2.Properties.Settings.dbCreditoConexionTLA"].ConnectionString.ToString();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("ERROR DE CONEXIÓN: " + ex.Message, "CASA GUERRERO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;
                case 5:
                    try
                    {
                        servidor = ConfigurationManager.ConnectionStrings["Concentrador2.Properties.Settings.dbCreditoConexionMySQL"].ConnectionString.ToString();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("ERROR DE CONEXIÓN: " + ex.Message, "CASA GUERRERO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;

            }
            return servidor;
        }

        public void limpiarTabla(String tabla_)
        {
            String servidorm = GetServidor(5);
            MySqlConnection conn = new MySqlConnection(servidorm);
            conn.Open();
            MySqlCommand cmdm = conn.CreateCommand();
            cmdm.CommandText = "TRUNCATE "+ tabla_ + "";
            cmdm.ExecuteNonQuery();
            conn.Close();
        }

        public void actualizarCargos(int eleccion)
        {
            int sinsaldo=0, consaldo=0, errores=0;
            //Conexion sql
            String servidor = GetServidor(eleccion);
            SqlConnection cn = new SqlConnection(servidor);
            SqlCommand cmd = cn.CreateCommand();
            cn.Open();
            SqlDataReader lector;
            cmd.CommandText = "SELECT Cuentaold, cuenta, CtaAdicional, Fecompra, Operacion, Vence, Plazo, Importe, Enganche, AbonoMen, Saldoact, Saldoant, Interes, Captura, Ultpago, Liquidacion, Tipo, Usuario, Tarjeta, MensPagare, fecha_int, sucorigen, UsuarioVale, Bonificaciones FROM  cargos where SaldoAct <> '0'";
            lector = cmd.ExecuteReader();

            //Conexion mysql

            String servidorm = GetServidor(5);
            MySqlConnection conn = new MySqlConnection(servidorm);
            conn.Open();
            MySqlCommand cmdm = conn.CreateCommand();

            if (lector.HasRows)
            {
                while (lector.Read())
                {
                    this.Refresh();
                    if(lector.IsDBNull(0) is false){ Cuentaold = lector.GetString(0); }else{ Cuentaold = ""; }
                    if (lector.IsDBNull(1) is false) { cuenta = lector.GetString(1);} else { cuenta = "";}
                    if (lector.IsDBNull(2) is false) { CtaAdicional = lector.GetString(2); } else { CtaAdicional = ""; }
                    if (lector.IsDBNull(3) is false) { Fecompra = lector.GetDateTime(3).ToString(); } else { Fecompra = ""; }
                    if (lector.IsDBNull(4) is false) { Operacion = lector.GetString(4); } else { Operacion = ""; }
                    if (lector.IsDBNull(5) is false) { Vence = lector.GetDateTime(5).ToString(); } else { Vence = ""; }
                    if (lector.IsDBNull(6) is false) { Plazo = lector.GetInt32(6); } else { Plazo = 0; }
                    if (lector.IsDBNull(7) is false) { Importe = lector.GetDecimal(7); } else { Importe = 0; }
                    if (lector.IsDBNull(8) is false) { Enganche = lector.GetDecimal(8); } else { Enganche = 0; }
                    if (lector.IsDBNull(9) is false) { AbonoMen = lector.GetDecimal(9); } else { AbonoMen = 0; }
                    if (lector.IsDBNull(10) is false) { Saldoact = lector.GetDecimal(10); } else { Saldoact = 0; }
                    if (lector.IsDBNull(11) is false) { Saldoant = lector.GetDecimal(11); } else { Saldoant = 0; }
                    if (lector.IsDBNull(12) is false) { Interes = lector.GetDecimal(12); } else { Interes = 0; }
                    if (lector.IsDBNull(13) is false) { Captura = lector.GetDateTime(13).ToString(); } else { Captura = ""; }
                    if (lector.IsDBNull(14) is false) { Ultpago = lector.GetDateTime(14).ToString(); } else { Ultpago = ""; }
                    if (lector.IsDBNull(15) is false) { Liquidacion = lector.GetString(15); } else { Liquidacion = ""; }
                    if (lector.IsDBNull(16) is false) { Tipo = lector.GetString(16); } else { Tipo = ""; }
                    if (lector.IsDBNull(17) is false) { Usuario = lector.GetString(17); } else { Usuario = ""; }
                    if (lector.IsDBNull(18) is false) { Tarjeta = lector.GetString(18); } else { Tarjeta = ""; }
                    if (lector.IsDBNull(19) is false) { MensPagare = lector.GetDecimal(19); } else { MensPagare =0; }
                    if (lector.IsDBNull(20) is false) { fecha_int = lector.GetDateTime(20).ToString(); } else { fecha_int = ""; }
                    if (lector.IsDBNull(21) is false) { sucorigen = lector.GetString(21); } else { sucorigen = ""; }
                    if (lector.IsDBNull(22) is false) { UsuarioVale = lector.GetString(22); } else { UsuarioVale = ""; }
                    if (lector.IsDBNull(23) is false) { Bonificaciones = Convert.ToString(lector.GetDecimal(23)); } else { Bonificaciones = ""; }

                    
                    if (Saldoact == 0)
                    {
                        sinsaldo += 1;
                        labsin.Text = sinsaldo.ToString();
                    }
                    else
                    {
                        try
                        {
                        
                        cmdm.CommandText = "INSERT INTO cargos (cuentaold, cuenta, ctaadicional, fecompra, opearcion, vence, plazo, importe, enganche, abonomen, saldoact, saldoant, interes, captura, ultpago, liquidacion, tipo, usuario, tarjeta, menspagare, fecha_int, sucorigen, usuariovale, bonificaciones) VALUES('" + Cuentaold + "', '" + cuenta + "', '" + CtaAdicional + "', '" + Fecompra + "', '" + Operacion + "', '" + Vence + "', '"+Plazo+ "', '"+Importe + "', '" + Enganche + "', '"+AbonoMen+ "', '"+Saldoact + "', '" + Saldoant + "', '"+Interes+ "', '"+Captura + "', '" + Ultpago + "', '"+Liquidacion+ "', '"+Tipo + "', '" + Usuario + "', '"+Tarjeta+ "', '"+MensPagare + "', '" + fecha_int + "', '"+sucorigen+ "', '"+ UsuarioVale + "', '" + Bonificaciones + "')";
                        cmdm.ExecuteNonQuery();

                        consaldo += 1;
                            labcon.Text = consaldo.ToString();
                        }
                        catch(Exception err)
                        {
                            //MessageBox.Show("Error de insercion mysql:" + err.ToString());
                            errores += 1;
                            laberr.Text = errores.ToString();
                            conn.Open();
                            cmdm = conn.CreateCommand();
                            cmdm.CommandText = "INSERT INTO cargos (cuentaold, cuenta, ctaadicional, fecompra, opearcion, vence, plazo, importe, enganche, abonomen, saldoact, saldoant, interes, captura, ultpago, liquidacion, tipo, usuario, tarjeta, menspagare, fecha_int, sucorigen, usuariovale, bonificaciones) VALUES('" + Cuentaold + "', '" + cuenta + "', '" + CtaAdicional + "', '" + Fecompra + "', '" + Operacion + "', '" + Vence + "', '" + Plazo + "', '" + Importe + "', '" + Enganche + "', '" + AbonoMen + "', '" + Saldoact + "', '" + Saldoant + "', '" + Interes + "', '" + Captura + "', '" + Ultpago + "', '" + Liquidacion + "', '" + Tipo + "', '" + Usuario + "', '" + Tarjeta + "', '" + MensPagare + "', '" + fecha_int + "', '" + sucorigen + "', '" + UsuarioVale + "', '" + Bonificaciones + "')";
                            cmdm.ExecuteNonQuery();
                        }
                    }
                    
                }
               // MessageBox.Show("Sin Saldo: " + sinsaldo + " Con saldo: " + consaldo +" Errores: "+errores );
            }
            conn.Close();
            cn.Close();

        }

        public void actualizarClientes(int eleccion)
        {
            int clientes = 0, errores=0;
            //Conexion sql
            String servidor = GetServidor(eleccion);
            SqlConnection cn = new SqlConnection(servidor);
            SqlCommand cmd = cn.CreateCommand();
            cn.Open();
            SqlDataReader lector;
            cmd.CommandText = "SELECT CuentaAnt, Cuenta, Nombre, Paterno, Materno, Fenacimiento, Domicilio, Interior, Exterior, Cruza, Colonia, Sector, poblacion, municipio, codpos, ladacasa, telcasa, ladacel, telcel, ladaotros, teltrabajo, Ladatrabajo, telotros, limcred, cobrador, diacobro, tipocli, activo, Restric, fealta, tipcobro, conginteres, fecongelacion, usufecongelacion, firmadigital, ferestric, ife, ctaaval, ctaFactura, estado, usuario, ctarestric, usufecompro, fecompromiso, observaciones, descuento,zona, Registro, Tarjeta, c_interes, NombreAval, DomicilioAval, TelAval, completa, FeUltModificacion, UsuUltmodificacion, FechaVale, UsuarioVale, ContadorRpt1, ContadorRpt2, BloquearLimite, TarjCred, ModificaPedido, CodCancelTarjeta, Correo FROM  clientes";
            lector = cmd.ExecuteReader();

            //Conexion mysql

            String servidorm = GetServidor(5);
            MySqlConnection conn = new MySqlConnection(servidorm);
            conn.Open();
            MySqlCommand cmdm = conn.CreateCommand();

            if (lector.HasRows)
            {
                while (lector.Read())
                {
                    try
                    {


                        this.Refresh();
                        if (lector.IsDBNull(0) is false) { CuentaAnt = lector.GetString(0); } else { CuentaAnt = ""; }
                        if (lector.IsDBNull(1) is false) { Cuenta = lector.GetString(1); } else { Cuenta = ""; }
                        if (lector.IsDBNull(2) is false) { Nombre = lector.GetString(2); } else { Nombre = ""; }
                        if (lector.IsDBNull(3) is false) { Paterno = lector.GetString(3); } else { Paterno = ""; }
                        if (lector.IsDBNull(4) is false) { Materno = lector.GetString(4); } else { Materno = ""; }
                        if (lector.IsDBNull(5) is false) { Fenacimiento = lector.GetDateTime(5).ToString(); } else { Fenacimiento = ""; }
                        if (lector.IsDBNull(6) is false) { Domicilio = lector.GetString(6); } else { Domicilio = ""; }
                        if (lector.IsDBNull(7) is false) { Interior = lector.GetString(7); } else { Interior = ""; }
                        if (lector.IsDBNull(8) is false) { Exterior = lector.GetString(8); } else { Exterior = ""; }
                        if (lector.IsDBNull(9) is false) { Cruza = lector.GetString(9); } else { Cruza = ""; }
                        if (lector.IsDBNull(10) is false) { Colonia = lector.GetString(10); } else { Colonia = ""; }
                        if (lector.IsDBNull(11) is false) { Sector = lector.GetString(11); } else { Sector = ""; }
                        if (lector.IsDBNull(12) is false) { poblacion = lector.GetString(12); } else { poblacion = ""; }
                        if (lector.IsDBNull(13) is false) { municipio = lector.GetString(13); } else { municipio = ""; }
                        if (lector.IsDBNull(14) is false) { codpos = lector.GetString(14); } else { codpos = ""; }
                        if (lector.IsDBNull(15) is false) { ladacasa = lector.GetString(15); } else { ladacasa = ""; }
                        if (lector.IsDBNull(16) is false) { telcasa = lector.GetString(16); } else { telcasa = ""; }
                        if (lector.IsDBNull(17) is false) { ladacel = lector.GetString(17); } else { ladacel = ""; }
                        if (lector.IsDBNull(18) is false) { telcel = lector.GetString(18); } else { telcel = ""; }
                        if (lector.IsDBNull(19) is false) { ladaotros = lector.GetString(19); } else { ladaotros = ""; }
                        if (lector.IsDBNull(20) is false) { teltrabajo = lector.GetString(20); } else { teltrabajo = ""; }
                        if (lector.IsDBNull(21) is false) { Ladatrabajo = lector.GetString(21); } else { Ladatrabajo = ""; }
                        if (lector.IsDBNull(22) is false) { telotros = lector.GetString(22); } else { telotros = ""; }
                        if (lector.IsDBNull(23) is false) { limcred = Convert.ToString(lector.GetDecimal(23)); } else { limcred = ""; }
                        if (lector.IsDBNull(24) is false) { cobrador = lector.GetString(24); } else { cobrador = ""; }
                        if (lector.IsDBNull(25) is false) { diacobro = lector.GetString(25); } else { diacobro = ""; }
                        if (lector.IsDBNull(26) is false) { tipocli = lector.GetString(26); } else { tipocli = ""; }
                        if (lector.IsDBNull(27) is false) { activo = lector.GetString(27); } else { activo = ""; }
                        if (lector.IsDBNull(28) is false) { Restric = lector.GetString(28); } else { Restric = ""; }
                        if (lector.IsDBNull(29) is false) { fealta = lector.GetDateTime(29).ToString(); } else { fealta = ""; }
                        if (lector.IsDBNull(30) is false) { tipcobro = lector.GetString(30); } else { tipcobro = ""; }
                        if (lector.IsDBNull(31) is false) { conginteres = lector.GetString(31); } else { conginteres = ""; }
                        if (lector.IsDBNull(32) is false) { fecongelacion = lector.GetDateTime(32).ToString(); } else { fecongelacion = ""; }
                        if (lector.IsDBNull(33) is false) { usufecongelacion = lector.GetString(33); } else { usufecongelacion = ""; }
                        if (lector.IsDBNull(34) is false) { firmadigital = lector.GetString(34); } else { firmadigital = ""; }
                        if (lector.IsDBNull(35) is false) { ferestric = lector.GetDateTime(35).ToString(); } else { ferestric = ""; }
                        if (lector.IsDBNull(36) is false) { ife = lector.GetString(36); } else { ife = ""; }
                        if (lector.IsDBNull(37) is false) { ctaaval = lector.GetString(37); } else { ctaaval = ""; }
                        if (lector.IsDBNull(38) is false) { ctaFactura = lector.GetString(38); } else { ctaFactura = ""; }
                        if (lector.IsDBNull(39) is false) { estado = lector.GetString(39); } else { estado = ""; }
                        if (lector.IsDBNull(40) is false) { usuario = lector.GetString(40); } else { usuario = ""; }
                        if (lector.IsDBNull(41) is false) { ctarestric = lector.GetString(41); } else { ctarestric = ""; }
                        if (lector.IsDBNull(42) is false) { usufecompro = lector.GetString(42); } else { usufecompro = ""; }
                        if (lector.IsDBNull(43) is false) { fecompromiso = lector.GetDateTime(43).ToString(); } else { fecompromiso = ""; }
                        if (lector.IsDBNull(44) is false) { observaciones = lector.GetString(44); } else { observaciones = ""; }
                        if (lector.IsDBNull(45) is false) { descuento = lector.GetString(45); } else { descuento = ""; }
                        if (lector.IsDBNull(46) is false) { zona = lector.GetString(46); } else { zona = ""; }
                        if (lector.IsDBNull(47) is false) { Registro = Convert.ToString(lector.GetDecimal(47)); } else { Registro = ""; }
                        if (lector.IsDBNull(48) is false) { Tarjeta = lector.GetString(48); } else { Tarjeta = ""; }
                        if (lector.IsDBNull(49) is false) { c_interes = Convert.ToString(lector.GetBoolean(49)); } else { c_interes = ""; }
                        if (lector.IsDBNull(50) is false) { NombreAval = lector.GetString(50); } else { NombreAval = ""; }
                        if (lector.IsDBNull(51) is false) { DomicilioAval = lector.GetString(51); } else { DomicilioAval = ""; }
                        if (lector.IsDBNull(52) is false) { TelAval = lector.GetString(52); } else { TelAval = ""; }
                        if (lector.IsDBNull(53) is false) { completa = lector.GetString(53); } else { completa = ""; }
                        if (lector.IsDBNull(54) is false) { FeUltModificacion = lector.GetDateTime(54).ToString(); } else { FeUltModificacion = ""; }
                        if (lector.IsDBNull(55) is false) { UsuUltmodificacion = lector.GetString(55); } else { UsuUltmodificacion = ""; }
                        if (lector.IsDBNull(56) is false) { FechaVale = lector.GetDateTime(56).ToString(); } else { FechaVale = ""; }
                        if (lector.IsDBNull(57) is false) { UsuarioVale = lector.GetString(57); } else { UsuarioVale = ""; }
                        if (lector.IsDBNull(58) is false) { ContadorRpt1 = Convert.ToString(lector.GetInt32(58)); } else { ContadorRpt1 = ""; }
                        if (lector.IsDBNull(59) is false) { ContadorRpt2 = Convert.ToString(lector.GetInt32(59)); } else { ContadorRpt2 = ""; }
                        if (lector.IsDBNull(60) is false) { BloquearLimite = Convert.ToString(lector.GetInt32(60)); } else { BloquearLimite = ""; }
                        if (lector.IsDBNull(61) is false) { TarjCred = Convert.ToString(lector.GetBoolean(61)); } else { TarjCred = ""; }
                        if (lector.IsDBNull(62) is false) { /*ModificaPedido = Convert.ToString(lector.GetInt32(62));*/ ModificaPedido = ""; } else { ModificaPedido = ""; }
                        if (lector.IsDBNull(63) is false) { CodCancelTarjeta = lector.GetString(63); } else { CodCancelTarjeta = ""; }
                        if (lector.IsDBNull(64) is false) { Correo = lector.GetString(64); } else { Correo = ""; }

                        //MessageBox.Show("Cuenta: " + Cuenta + " Nombre:" + Nombre);

                        try
                        {
                            if ((CuentaAnt + Cuenta + Nombre + Paterno + Materno + Fenacimiento + Domicilio + Interior + Exterior + Cruza + Colonia + Sector + poblacion + municipio + codpos + ladacasa + telcasa + ladacel + telcel + ladaotros + teltrabajo + Ladatrabajo + telotros + limcred + cobrador + diacobro + tipocli + activo + Restric + fealta + tipcobro + conginteres + fecongelacion + usufecongelacion + firmadigital + ferestric + ife + ctaaval + ctaFactura + estado + usuario + ctarestric + usufecompro + fecompromiso + observaciones + descuento + zona + Registro + Tarjeta + c_interes + NombreAval + DomicilioAval + TelAval + completa + FeUltModificacion + UsuUltmodificacion + FechaVale + UsuarioVale + ContadorRpt1 + ContadorRpt2 + BloquearLimite + TarjCred + ModificaPedido + CodCancelTarjeta + Correo).Contains("'") is true)
                            {
                                errores += 1;
                                //MessageBox.Show("Cuenta: " + Cuenta);
                            }
                            else
                            {
                                cmdm.CommandText = "INSERT INTO clientes ( CuentaAnt, Cuenta, Nombre, Paterno, Materno, Fenacimiento, Domicilio, Interior, Exterior, Cruza, Colonia, Sector, poblacion, municipio, codpos, ladacasa, telcasa, ladacel, telcel, ladaotros, teltrabajo, Ladatrabajo, telotros, limcred, cobrador, diacobro, tipocli, activo, Restric, fealta, tipcobro, conginteres, fecongelacion, usufecongelacion, firmadigital, ferestric, ife, ctaaval, ctaFactura, estado, usuario, ctarestric, usufecompro, fecompromiso, observaciones, descuento,zona, Registro, Tarjeta, c_interes, NombreAval, DomicilioAval, TelAval, completa, FeUltModificacion, UsuUltmodificacion, FechaVale, UsuarioVale, ContadorRpt1, ContadorRpt2, BloquearLimite, TarjCred, ModificaPedido, CodCancelTarjeta, Correo ) VALUES('" + CuentaAnt + "', '" + Cuenta + "', '" + Nombre + "', '" + Paterno + "', '" + Materno + "', '" + Fenacimiento + "','" + Domicilio + "','" + Interior + "','" + Exterior + "', '" + Cruza + "','" + Colonia + "','" + Sector + "', '" + poblacion + "', '" + municipio + "', '" + codpos + "', '" + ladacasa + "', '" + telcasa + "', '" + ladacel + "', '" + telcel + "', '" + ladaotros + "', '" + teltrabajo + "', '" + Ladatrabajo + "', '" + telotros + "', '" + limcred + "', '" + cobrador + "', '" + diacobro + "', '" + tipocli + "', '" + activo + "', '" + Restric + "', '" + fealta + "', '" + tipcobro + "', '" + conginteres + "', '" + fecongelacion + "', '" + usufecongelacion + "', '" + firmadigital + "', '" + ferestric + "', '" + ife + "', '" + ctaaval + "', '" + ctaFactura + "', '" + estado + "', '" + usuario + "', '" + ctarestric + "', '" + usufecompro + "', '" + fecompromiso + "', '" + observaciones + "', '" + descuento + "', '" + zona + "', '" + Registro + "', '" + Tarjeta + "','" + c_interes + "', '" + NombreAval + "','" + DomicilioAval + "', '" + TelAval + "', '" + completa + "', '" + FeUltModificacion + "', '" + UsuUltmodificacion + "', '" + FechaVale + "', '" + UsuarioVale + "', '" + ContadorRpt1 + "', '" + ContadorRpt2 + "', '" + BloquearLimite + "', '" + TarjCred + "', '" + ModificaPedido + "', '" + CodCancelTarjeta + "', '" + Correo + "' )";
                                cmdm.ExecuteNonQuery();
                            }

                        }
                        catch (Exception err)
                        {
                            //MessageBox.Show("Cuenta: " + Cuenta + "Error de insercion mysql:" + err.ToString());
                            if (err.ToString().Contains("must be valid and open") is true)
                            {
                                conn.Open();
                                cmdm = conn.CreateCommand();
                                try
                                {
                                    cmdm.CommandText = "INSERT INTO clientes ( CuentaAnt, Cuenta, Nombre, Paterno, Materno, Fenacimiento, Domicilio, Interior, Exterior, Cruza, Colonia, Sector, poblacion, municipio, codpos, ladacasa, telcasa, ladacel, telcel, ladaotros, teltrabajo, Ladatrabajo, telotros, limcred, cobrador, diacobro, tipocli, activo, Restric, fealta, tipcobro, conginteres, fecongelacion, usufecongelacion, firmadigital, ferestric, ife, ctaaval, ctaFactura, estado, usuario, ctarestric, usufecompro, fecompromiso, observaciones, descuento,zona, Registro, Tarjeta, c_interes, NombreAval, DomicilioAval, TelAval, completa, FeUltModificacion, UsuUltmodificacion, FechaVale, UsuarioVale, ContadorRpt1, ContadorRpt2, BloquearLimite, TarjCred, ModificaPedido, CodCancelTarjeta, Correo ) VALUES('" + CuentaAnt + "', '" + Cuenta + "', '" + Nombre + "', '" + Paterno + "', '" + Materno + "', '" + Fenacimiento + "','" + Domicilio + "','" + Interior + "','" + Exterior + "', '" + Cruza + "','" + Colonia + "','" + Sector + "', '" + poblacion + "', '" + municipio + "', '" + codpos + "', '" + ladacasa + "', '" + telcasa + "', '" + ladacel + "', '" + telcel + "', '" + ladaotros + "', '" + teltrabajo + "', '" + Ladatrabajo + "', '" + telotros + "', '" + limcred + "', '" + cobrador + "', '" + diacobro + "', '" + tipocli + "', '" + activo + "', '" + Restric + "', '" + fealta + "', '" + tipcobro + "', '" + conginteres + "', '" + fecongelacion + "', '" + usufecongelacion + "', '" + firmadigital + "', '" + ferestric + "', '" + ife + "', '" + ctaaval + "', '" + ctaFactura + "', '" + estado + "', '" + usuario + "', '" + ctarestric + "', '" + usufecompro + "', '" + fecompromiso + "', '" + observaciones + "', '" + descuento + "', '" + zona + "', '" + Registro + "', '" + Tarjeta + "','" + c_interes + "', '" + NombreAval + "','" + DomicilioAval + "', '" + TelAval + "', '" + completa + "', '" + FeUltModificacion + "', '" + UsuUltmodificacion + "', '" + FechaVale + "', '" + UsuarioVale + "', '" + ContadorRpt1 + "', '" + ContadorRpt2 + "', '" + BloquearLimite + "', '" + TarjCred + "', '" + ModificaPedido + "', '" + CodCancelTarjeta + "', '" + Correo + "' )";
                                    cmdm.ExecuteNonQuery();
                                }
                                catch
                                {

                                }
                            }
                            errores += 1;
                        }

                        clientes += 1;
                        labcli.Text = clientes.ToString();
                        laberr.Text = errores.ToString();
                        this.Refresh();
                    }
                    catch
                    {

                    }
                }
                //MessageBox.Show("La migración ha terminado");
            }
            conn.Close();
            cn.Close();

        }

        public void actualizarMovtos(int eleccion)
        {

            int sinsaldo = 0, consaldo = 0, errores = 0;
            //Conexion sql
            String servidor = GetServidor(eleccion);
            SqlConnection cn = new SqlConnection(servidor);
            SqlCommand cmd = cn.CreateCommand();
            cn.Open();
            SqlDataReader lector;
            cmd.CommandText = "SELECT  Cuenta, Operacion, Feabono, hora,Descuento,Concepto,Cantidad,Describe,recibo,Reciboc,Usuario,Vale,Autorizo,Caja,Efectivo,Formapago,Cheque,Banco,Dependencia,registro,interes,Saldoact,Saldoant,tipoAbono,NotCredito,FolDevolucion,Captura,SucOrigen,Timbrado,DescribeDesc,Ficha,BancoF,numeroF,Parcialidad FROM movtos where Feabono > '2019-01-01' order by Feabono desc";

            lector = cmd.ExecuteReader();

            //Conexion mysql

            String servidorm = GetServidor(5);
            MySqlConnection conn = new MySqlConnection(servidorm);
            bool correcto = false;

            while (correcto is false) {
                try
                {
                    Thread.Sleep(TimeSpan.FromMinutes(1));
                    conn.Open();
                    correcto = true;
                } catch (Exception erro)
                {
                    correcto = false;
                    confallida += 1;
                    labconex.Text = confallida.ToString();
                }
            }

            MySqlCommand cmdm = conn.CreateCommand();

            if (lector.HasRows)
            {
                while (lector.Read())
                {

                    //this.Refresh();
                    if (lector.IsDBNull(0) is false) { mCuenta = lector.GetString(0); } else { mCuenta = ""; }
                    if (lector.IsDBNull(1) is false) { mOperacion = lector.GetString(1); } else { mOperacion = ""; }
                    if (lector.IsDBNull(2) is false) { mFeabono = lector.GetDateTime(2).ToString(); } else { mFeabono = ""; }
                    if (lector.IsDBNull(3) is false) { mhora = lector.GetString(3); } else { mhora = ""; }
                    if (lector.IsDBNull(4) is false) { mDescuento = Convert.ToString(lector.GetInt32(4)); } else { mDescuento = ""; }
                    if (lector.IsDBNull(5) is false) { mConcepto = lector.GetString(5); } else { mConcepto = ""; }
                    if (lector.IsDBNull(6) is false) { mCantidad = Convert.ToString(lector.GetDecimal(6)); } else { mCantidad = ""; }
                    if (lector.IsDBNull(7) is false) { mDescribe = lector.GetString(7); } else { mDescribe = ""; }
                    if (lector.IsDBNull(8) is false) { mrecibo = lector.GetString(8); } else { mrecibo = ""; }
                    if (lector.IsDBNull(9) is false) { mReciboc = Convert.ToString(lector.GetDecimal(9)); } else { mReciboc = ""; }
                    if (lector.IsDBNull(10) is false) { mUsuario = lector.GetString(10); } else { mUsuario = ""; }
                    if (lector.IsDBNull(11) is false) { mVale = lector.GetString(11); } else { mVale = ""; }
                    if (lector.IsDBNull(12) is false) { mAutorizo = lector.GetString(12); } else { mAutorizo = ""; }
                    if (lector.IsDBNull(13) is false) { mCaja = lector.GetString(13); } else { mCaja = ""; }
                    if (lector.IsDBNull(14) is false) { mEfectivo = Convert.ToString(lector.GetDecimal(14)); } else { mEfectivo = ""; }
                    if (lector.IsDBNull(15) is false) { mFormapago = lector.GetString(15); } else { mFormapago = ""; }
                    if (lector.IsDBNull(16) is false) { mCheque = lector.GetString(16); } else { mCheque = ""; }
                    if (lector.IsDBNull(17) is false) { mBanco = lector.GetString(17); } else { mBanco = ""; }
                    if (lector.IsDBNull(18) is false) { mDependencia = lector.GetString(18); } else { mDependencia = ""; }
                    if (lector.IsDBNull(19) is false) { mregistro = Convert.ToString(lector.GetDecimal(19)); } else { mregistro = ""; }
                    if (lector.IsDBNull(20) is false) { minteres = Convert.ToString(lector.GetDecimal(20)); } else { minteres = ""; }
                    if (lector.IsDBNull(21) is false) { mSaldoact = Convert.ToString(lector.GetDecimal(21)); } else { mSaldoact = ""; }
                    if (lector.IsDBNull(22) is false) { mSaldoant = Convert.ToString(lector.GetDecimal(22)); } else { mSaldoant = ""; }
                    if (lector.IsDBNull(23) is false) { mtipoAbono = lector.GetString(23); } else { mtipoAbono = ""; }
                    if (lector.IsDBNull(24) is false) { mNotCredito = lector.GetString(24); } else { mNotCredito = ""; }
                    if (lector.IsDBNull(25) is false) { mFolDevolucion = lector.GetString(25); } else { mFolDevolucion = ""; }
                    if (lector.IsDBNull(26) is false) { mCaptura = lector.GetDateTime(26).ToString(); } else { mCaptura = ""; }
                    if (lector.IsDBNull(27) is false) { mSucOrigen = lector.GetString(27); } else { mSucOrigen = ""; }
                    if (lector.IsDBNull(28) is false) { mTimbrado = Convert.ToString(lector.GetBoolean(28)); } else { mTimbrado = ""; }
                    if (lector.IsDBNull(29) is false) { mDescribeDesc = lector.GetString(29); } else { mDescribeDesc = ""; }
                    if (lector.IsDBNull(30) is false) { mFicha = lector.GetDateTime(30).ToString(); } else { mFicha = ""; }
                    if (lector.IsDBNull(31) is false) { mBancoF = lector.GetString(31); } else { mBancoF = ""; }
                    if (lector.IsDBNull(32) is false) { mnumeroF = lector.GetString(32); } else { mnumeroF = ""; }
                    if (lector.IsDBNull(33) is false) { mParcialidad = Convert.ToString(lector.GetInt32(33));  } else { mParcialidad = ""; }



                    if (mSaldoact == "0")
                    {
                        sinsaldo += 1;
                        labsin.Text = sinsaldo.ToString();
                    }
                    else
                    {
                        consaldo += 1;
                        labcon.Text = consaldo.ToString();
                    }
                        try
                        {

                            cmdm.CommandText = "INSERT INTO movtos (Cuenta, Operacion, Feabono, hora,Descuento,Concepto,Cantidad,mDescribe,recibo,Reciboc,Usuario,Vale,Autorizo,Caja,Efectivo,Formapago,Cheque,Banco,Dependencia,registro,interes,Saldoact,Saldoant,tipoAbono,NotCredito,FolDevolucion,Captura,SucOrigen,Timbrado,DescribeDesc,Ficha,BancoF,numeroF,Parcialidad) VALUES('"+mCuenta + "','" + mOperacion + "','" + mFeabono + "','" + mhora + "', '" + mDescuento + "','" + mConcepto + "','" + mCantidad + "', '" + mDescribe + "', '" + mrecibo + "', '" + mReciboc + "', '" + mUsuario + "', '" + mVale + "', '" + mAutorizo + "', '" + mCaja + "', '" + mEfectivo + "', '" + mFormapago + "','" + mCheque + "', '" + mBanco + "', '" + mDependencia + "', '" + mregistro + "', '" + minteres + "', '" + mSaldoact + "','" + mSaldoant + "', '" + mtipoAbono + "', '" + mNotCredito + "','" + mFolDevolucion + "', '" + mCaptura + "', '" + mSucOrigen + "', '" + mTimbrado + "', '" + mDescribeDesc + "', '" + mFicha + "', '" + mBancoF + "', '" + mnumeroF + "','" + mParcialidad + "')";
                            cmdm.ExecuteNonQuery();

                        }
                        catch (Exception err)
                        {
                            //MessageBox.Show("Error de insercion mysql:" + err.ToString());
                            errores += 1;
                            laberr.Text = errores.ToString();
                            conn.Open();
                            cmdm = conn.CreateCommand();
                            cmdm.CommandText = "INSERT INTO movtos(Cuenta, Operacion, Feabono, hora, Descuento, Concepto, Cantidad, mDescribe, recibo, Reciboc, Usuario, Vale, Autorizo, Caja, Efectivo, Formapago, Cheque, Banco, Dependencia, registro, interes, Saldoact, Saldoant, tipoAbono, NotCredito, FolDevolucion, Captura, SucOrigen, Timbrado, DescribeDesc, Ficha, BancoF, numeroF, Parcialidad) VALUES('"+mCuenta + "', '" + mOperacion + "', '" + mFeabono + "', '" + mhora + "', '" + mDescuento + "', '" + mConcepto + "', '" + mCantidad + "', '" + mDescribe + "', '" + mrecibo + "', '" + mReciboc + "', '" + mUsuario + "', '" + mVale + "', '" + mAutorizo + "', '" + mCaja + "', '" + mEfectivo + "', '" + mFormapago + "', '" + mCheque + "', '" + mBanco + "', '" + mDependencia + "', '" + mregistro + "', '" + minteres + "', '" + mSaldoact + "', '" + mSaldoant + "', '" + mtipoAbono + "', '" + mNotCredito + "', '" + mFolDevolucion + "', '" + mCaptura + "', '" + mSucOrigen + "', '" + mTimbrado + "', '" + mDescribeDesc + "', '" + mFicha + "', '" + mBancoF + "', '" + mnumeroF + "', '" + mParcialidad + "')";
                            cmdm.ExecuteNonQuery();
                        }
                    

                }
                this.Refresh();
                // MessageBox.Show("Sin Saldo: " + sinsaldo + " Con saldo: " + consaldo +" Errores: "+errores );
            }
            conn.Close();
            cn.Close();

        }
    }
}
