using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StockNova.UI
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            string usuario = txtUsuario.Text.Trim();
            string password = txtPassword.Text.Trim();

            UsuarioBLL usuarioBLL = new UsuarioBLL();
            bool esValido = usuarioBLL.ValidarLogin(usuario, password);

            if (esValido)
            {
                MessageBox.Show("¡Bienvenido al sistema StockNova!", "Acceso Concedido", MessageBoxButtons.OK, MessageBoxIcon.Information);

                FrmMenuPrincipal menu = new FrmMenuPrincipal();
                menu.Show();
                this.Hide(); 
            }
            else
            {
                MessageBox.Show("Usuario o contraseña incorrectos.", "Error de Autenticación", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            DialogResult respuesta = MessageBox.Show("¿Está seguro de que desea salir de la aplicación?", "Confirmar Salida", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (respuesta == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}
