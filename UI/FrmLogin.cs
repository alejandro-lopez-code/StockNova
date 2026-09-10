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

            if (usuario == "admin" && password == "1234")
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
    }
}
