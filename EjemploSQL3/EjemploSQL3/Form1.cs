namespace EjemploSQL3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void empleadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Mantenimiento.Consulta_Empleado forma = new Mantenimiento.Consulta_Empleado();
            forma.MdiParent = this;
            forma.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
