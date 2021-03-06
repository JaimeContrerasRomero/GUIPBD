using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUIPBD
{
    public partial class fEmpresa : Form
    {
        string Modo = "";

        public fEmpresa()
        {
            InitializeComponent();
        }

        private void empresaBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.empresaBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.pBDDataSet);

        }

        private void fEmpresa_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'pBDDataSet.Empresa' table. You can move, or remove it, as needed.
            this.CargaDatos();

        }

        private void CargaDatos()
        {
            try
            {
                this.empresaTableAdapter.Fill(this.pBDDataSet.Empresa);
                this.ModoEdicion("Lectura");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en la carga de datos: " + ex.Message.ToString());
            }
        }

        private void ModoEdicion(string modo)
        {
            this.Modo = modo;
            switch (modo)
            {
                case "Lectura":
                    this.pnlBontones.Enabled = true;
                    this.pnlDetalle.Enabled = false;
                    this.empresaDataGridView.Enabled = true;
                    this.empresaBindingNavigator.Enabled = true;
                    break;
                case "Insertar":
                    this.pnlBontones.Enabled = false;
                    this.pnlDetalle.Enabled = true;
                    this.empresaDataGridView.Enabled = false;
                    this.empresaBindingNavigator.Enabled = false;
                    break;
                case "Actualizar":
                    this.pnlBontones.Enabled = false;
                    this.pnlDetalle.Enabled = true;
                    this.empresaDataGridView.Enabled = false;
                    this.empresaBindingNavigator.Enabled = false;
                    break;
            }
        }

        private void btnInsertar_Click(object sender, EventArgs e)
        {
            this.ModoEdicion("Insertar");
            this.idEmpresaTextBox.Text = "";
            this.razonSocialTextBox.Text = "";
            this.razonSocialTextBox.Focus();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.CargaDatos();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            this.ModoEdicion("Actualizar");
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr = MessageBox.Show("¿Está seguro de eliminar este registro?", "Eliminar", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    //ejecutar el deleted de tabla
                    int id = int.Parse(this.idEmpresaTextBox.Text);
                    this.empresaTableAdapter.Delete(id);
                    this.CargaDatos();
                }
                else
                {
                    this.CargaDatos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Eliminar: " + ex.Message.ToString());
            }
        }

        private bool Valida()
        {
            this.errorProvider1.Clear();
            bool validado = true;
            if (this.razonSocialTextBox.Text.Trim() == "")
            {
                validado = false;
                this.errorProvider1.SetError(this.razonSocialTextBox, "Campo requerido");
            }
            return validado;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.Valida())
                {
                    switch (this.Modo)
                    {
                        case "Insertar":
                            //Ejecutar el insert de la tabla empresa
                            this.empresaTableAdapter.Insert(this.razonSocialTextBox.Text);
                            break;
                        case "Actualizar":
                            //Ejecutar el update de la tabla empresa
                            int id = int.Parse(this.idEmpresaTextBox.Text);
                            this.empresaTableAdapter.Update(this.razonSocialTextBox.Text, id);
                            break;
                    }
                    this.CargaDatos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message.ToString());
            }
        }
    }
}
