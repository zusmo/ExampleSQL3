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
    public partial class Consulta_Empleado : Form
    {
        public Consulta_Empleado()
        {
            InitializeComponent();
        }

        Clases.Conector con = new Clases.Conector();

        #region SQL 
        string SQL_SEL = @" SELECT * FROM EMPLEADOS WHERE Nombre LIKE '{0}%' ORDER BY Codigo DESC";
        string SQL_DEL = "DELETE FROM EMPLEADOS WHERE Codigo = {0} ";
        #endregion

        public void refrescar()
        {
            con.Abrir();
            this.dataGridView1.DataSource = con.select(string.Format(SQL_SEL, this.textBox1.Text)).Tables[0];
            con.cerrar();
        }

        private void Consulta_Empleado_Load(object sender, EventArgs e)
        {
            con.Abrir();
            this.dataGridView1.DataSource = con.select(string.Format(SQL_SEL, "")).Tables[0];
            con.cerrar();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

            con.Abrir();
            this.dataGridView1.DataSource = con.select(string.Format(SQL_SEL, this.textBox1.Text)).Tables[0];
            con.cerrar();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Mantenimiento.Nuevo_Empleado forma = new Mantenimiento.Nuevo_Empleado(true, -1);
            forma.MdiParent = this.MdiParent;
            forma.consulta = this;
            forma.Show();
        }
        
        //Context menu para eliminar
        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int r = this.dataGridView1.CurrentRow.Index;
            if (MessageBox.Show("Estas seguro si o no", "Borrar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                con.Abrir();
                con.insert(string.Format(SQL_DEL, this.dataGridView1.Rows[r].Cells[0].Value.ToString()));
                this.dataGridView1.DataSource = con.select(string.Format(SQL_SEL, this.textBox1.Text)).Tables[0];
                con.cerrar();

            }
        }

        private void Consulta_Empleado_DoubleClick(object sender, EventArgs e)
        {
           
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            int r = this.dataGridView1.CurrentRow.Index;
            int id = Convert.ToInt32(this.dataGridView1.Rows[r].Cells[0].Value.ToString());
            Mantenimiento.Nuevo_Empleado forma = new Mantenimiento.Nuevo_Empleado(false, id);
            forma.MdiParent = this.MdiParent;
            forma.consulta = this;
            forma.Show();
        }
    }
}
