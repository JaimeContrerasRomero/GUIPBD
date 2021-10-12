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
    public partial class fAlumno : Form
    {
        string Modo = "";

        public fAlumno()
        {
            InitializeComponent();
        }

        private void alumnoBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.alumnoBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.pBDDataSet);

        }

        private void fAlumno_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'pBDDataSet.Alumno' table. You can move, or remove it, as needed.
            this.CargaDatos();

        }

        private void CargaDatos()
        {
            try
            {
                this.alumnoTableAdapter.Fill(this.pBDDataSet.Alumno);
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
                    this.alumnoDataGridView.Enabled = true;
                    this.alumnoBindingNavigator.Enabled = true;
                    break;
                case "Insertar":
                    this.pnlBontones.Enabled = false;
                    this.pnlDetalle.Enabled = true;
                    this.alumnoDataGridView.Enabled = false;
                    this.alumnoBindingNavigator.Enabled = false;
                    break;
                case "Actualizar":
                    this.pnlBontones.Enabled = false;
                    this.pnlDetalle.Enabled = true;
                    this.alumnoDataGridView.Enabled = false;
                    this.alumnoBindingNavigator.Enabled = false;
                    break;
            }
        }

        private void btnInsertar_Click(object sender, EventArgs e)
        {
            this.ModoEdicion("Insertar");
            this.idAlumnoTextBox.Text = "";
            this.nombreTextBox.Text = "";
            this.nombreTextBox.Focus();
            this.primerApellidoTextBox.Text = "";
            this.segundoApellidoTextBox.Text = "";
            this.noControlTextBox.Text = "";
            this.emailTextBox.Text = "";
            this.telefonoTextBox.Text = "";
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
                    int id = int.Parse(this.idAlumnoTextBox.Text);
                    this.alumnoTableAdapter.Delete(id);
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
            if (this.nombreTextBox.Text.Trim() == "")
            {
                validado = false;
                this.errorProvider1.SetError(this.nombreTextBox, "Campo requerido");
            }
            if (this.primerApellidoTextBox.Text.Trim() == "")
            {
                validado = false;
                this.errorProvider1.SetError(this.primerApellidoTextBox, "Campo requerido");
            }
            if (this.segundoApellidoTextBox.Text.Trim() == "")
            {
                validado = false;
                this.errorProvider1.SetError(this.segundoApellidoTextBox, "Campo requerido");
            }
            if (this.noControlTextBox.Text.Trim() == "")
            {
                validado = false;
                this.errorProvider1.SetError(this.noControlTextBox, "Campo requerido");
            }
            if (this.emailTextBox.Text.Trim() == "")
            {
                validado = false;
                this.errorProvider1.SetError(this.emailTextBox, "Campo requerido");
            }
            if (this.telefonoTextBox.Text.Trim() == "")
            {
                validado = false;
                this.errorProvider1.SetError(this.telefonoTextBox, "Campo requerido");
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
                            this.alumnoTableAdapter.Insert(this.nombreTextBox.Text, this.primerApellidoTextBox.Text, this.segundoApellidoTextBox.Text, this.noControlTextBox.Text, this.emailTextBox.Text, this.telefonoTextBox.Text);
                            break;
                        case "Actualizar":
                            //Ejecutar el update de la tabla empresa
                            int id = int.Parse(this.idAlumnoTextBox.Text);
                            this.alumnoTableAdapter.Update(this.nombreTextBox.Text, this.primerApellidoTextBox.Text, this.segundoApellidoTextBox.Text, this.noControlTextBox.Text, this.emailTextBox.Text, this.telefonoTextBox.Text, id);
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
