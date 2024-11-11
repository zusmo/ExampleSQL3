using EjemploSQL3.Clases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EjemploSQL3.Mantenimiento
{
    public partial class Nuevo_Empleado : Form
    {
        public Nuevo_Empleado(bool nuevos , int ids)
        {
            InitializeComponent();
            nuevo = nuevos;
            id = ids;
        }

        bool nuevo;
        int id;

        Clases.Conector con = new Clases.Conector();
        #region SQL
        string SQL_INSERT = @"INSERT INTO Empleados(Nombre , ID_Departamento, Sueldo)
                                VALUES('{0}', {1}, {2})";
        string SQL_SELECT = "SELECT ID, NOMBRE FROM DEPARTAMENTOS";
        string SQL_SEL2 = @" SELECT * FROM EMPLEADOS WHERE Codigo = {0}";
        string SQL_UPDT = @"UPDATE EMPLEADOS SET Nombre = '{1}', ID_Departamento = {2}, Sueldo = {3} WHERE Codigo = {0}";
        #endregion

        public Consulta_Empleado consulta;
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            con.Abrir();

            if (nuevo)
            {
                con.insert(string.Format(SQL_INSERT, this.textBox2.Text, this.comboBox1.SelectedValue.ToString(), this.numericUpDown1.Value));
              
            }
            else
            {
                con.insert(string.Format(SQL_UPDT, id, this.textBox2.Text, this.comboBox1.SelectedValue.ToString(), this.numericUpDown1.Value));
                
            }


            con.cerrar();
            consulta.refrescar();
            MessageBox.Show("Empleado Editado con Exito");
            this.Close();
        }

        private void Nuevo_Empleado_Load(object sender, EventArgs e)
        {
            DataSet ds;
            this.comboBox1.ValueMember = "ID";
            this.comboBox1.DisplayMember = "Nombre";
            con.Abrir();
            this.comboBox1.DataSource = con.select(SQL_SELECT).Tables[0];
            con.cerrar();

            if (!nuevo)
            {
                con.Abrir();
                ds = con.select( string.Format(SQL_SEL2, id));
                con.cerrar();
                this.textBox1.Text = ds.Tables[0].Rows[0][0].ToString();
                this.textBox2.Text = ds.Tables[0].Rows[0][1].ToString();
                this.comboBox1.SelectedValue = ds.Tables[0].Rows[0][2].ToString();
                this.numericUpDown1.Value = Convert.ToDecimal(ds.Tables[0].Rows[0][3].ToString());
            
            }

        }
    }
}
