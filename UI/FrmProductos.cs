using StockNova.BLL;
using StockNova.Entities;
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
    public partial class FrmProductos : Form
    {
        public FrmProductos()
        {
            InitializeComponent();
        }

        private void FrmProductos_Load(object sender, EventArgs e)
        {
            CargarGrid();
            BloquearCampos(true);
        }

        private void CargarGrid()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = ProductoBLL.ObtenerProductos();
        }

        private void BloquearCampos(bool bloquear)
        {
            txtCodigo.Enabled = !bloquear;
            txtNombre.Enabled = !bloquear;
            txtCategoria.Enabled = !bloquear;
            txtPrecio.Enabled = !bloquear;
            txtStock.Enabled = !bloquear;

            btnGuardar.Enabled = !bloquear;
            btnCancelar.Enabled = !bloquear;
            btnNuevo.Enabled = bloquear;
        }

        private void Limpiar()
        {
            txtCodigo.Clear();
            txtNombre.Clear();
            txtCategoria.Clear();
            txtPrecio.Clear();
            txtStock.Clear();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Limpiar();
            BloquearCampos(false);
            txtCodigo.Focus();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtNombre.Text) || string.IsNullOrWhiteSpace(txtCodigo.Text))
                {
                    MessageBox.Show("Por favor, rellene al menos el Código y el Nombre.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Producto nuevo = new Producto
                {
                    Codigo = txtCodigo.Text.Trim(),
                    Nombre = txtNombre.Text.Trim(),
                    Categoria = txtCategoria.Text.Trim(),
                    Precio = decimal.Parse(txtPrecio.Text),
                    Stock = int.Parse(txtStock.Text)
                };

                ProductoBLL.Agregar(nuevo);
                CargarGrid();
                Limpiar();
                BloquearCampos(true);

                MessageBox.Show("¡Producto guardado exitosamente en memoria!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Verifica que Precio y Stock sean números válidos.\nDetalle: " + ex.Message, "Error de formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["Id"].Value);
                ProductoBLL.Eliminar(id);
                CargarGrid();
                MessageBox.Show("Producto eliminado de la lista.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Seleccione una fila completa en la tabla para eliminar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Limpiar();
            BloquearCampos(true);
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
