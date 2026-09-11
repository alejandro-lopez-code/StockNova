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

            cboCategoria.DataSource = StockNova.BLL.CategoriaBLL.ObtenerCategorias();
            cboCategoria.DataSource = StockNova.BLL.CategoriaBLL.ObtenerCategorias();
            cboCategoria.DisplayMember = "Nombre"; 
            cboCategoria.ValueMember = "Id";       
            cboCategoria.SelectedIndex = -1; 
        }

        private void CargarGrid()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = ProductoBLL.ObtenerProductos();

            if (dataGridView1.Columns["Precio"] != null)
            {
                dataGridView1.Columns["Precio"].DefaultCellStyle.Format = "C$ #,##0.00";
            }
        }

        private void BloquearCampos(bool bloquear)
        {
            txtCodigo.Enabled = !bloquear;
            txtNombre.Enabled = !bloquear;
            cboCategoria.Enabled = !bloquear;
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
            cboCategoria.SelectedIndex = -1;

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

                if (!decimal.TryParse(txtPrecio.Text.Trim(), out decimal precio))
                {
                    MessageBox.Show("El campo Precio no tiene un formato numérico válido.", "Error de formato", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPrecio.Focus();
                    return;
                }

                if (!int.TryParse(txtStock.Text.Trim(), out int stock))
                {
                    MessageBox.Show("El campo Stock debe ser un número entero válido.", "Error de formato", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtStock.Focus();
                    return;
                }

                Producto nuevo = new Producto
                {
                    Codigo = txtCodigo.Text.Trim(),
                    Nombre = txtNombre.Text.Trim(),
                    Categoria = cboCategoria.Text,
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
            if (dataGridView1.CurrentRow == null || dataGridView1.CurrentRow.Index < 0)
            {
                MessageBox.Show("Por favor, seleccione un producto de la tabla para eliminar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirmacion = MessageBox.Show("¿Está seguro de que desea eliminar este producto?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmacion == DialogResult.Yes)
            {
                try
                {
                    int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["Id"].Value);

                    ProductoBLL.Eliminar(id);

                    CargarGrid();
                    Limpiar();

                    MessageBox.Show("Producto eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al intentar eliminar el producto: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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
