using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calificaciones
{
    public partial class Consulta : Form
    {
        DataGridView temp = new DataGridView();
        public Consulta(DataGridView nuevo)
        {
            InitializeComponent();
            temp = nuevo;
        }

        /**********************************************************************************************************************/
        //Al recivir el DGV como parametro, llenos los campos de la nueva ventana
        private void buscarPorDni(object sender, EventArgs e)
        {
            String cadena = txtDNI.Text.Trim();

            if (!string.IsNullOrWhiteSpace(cadena))
            {
                for (int i = 0; i < temp.Rows.Count - 1; i++) //El for lo utilizo para recorre el DGV
                {
                    if (txtDNI.Text.Trim().Equals(temp.Rows[i].Cells[3].Value.ToString()))
                    {
                        txtNombre.Text = temp.Rows[i].Cells[0].Value.ToString(); //Obtengo el nombre del DGV y se lo asigno al TextBox Nombre
                        txtApellido.Text = temp.Rows[i].Cells[1].Value.ToString(); //Obtengo el Apellido del DGV y se lo asigno al TextBox Apellido
                        txtDireccion.Text = temp.Rows[i].Cells[2].Value.ToString(); //Obtengo el Direccion del DGV y se lo asigno al TextBox Direccion
                        txtCedula.Text = temp.Rows[i].Cells[3].Value.ToString(); //Obtengo el DNI del DGV y se lo asigno al TextBox Cedula
                        txtNota.Text = temp.Rows[i].Cells[4].Value.ToString(); //Obtengo el Nota del DGV y se lo asigno al TextBox Nota
                        cbCalificacion.SelectedIndex = 0;
                        return;
                    }
                }
                MessageBox.Show("  No existe el DNI ingresado  ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("Por favor ingrese el DNI para realizar la busqueda", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /**********************************************************************************************************************/
        //Al presionar el boton modificar, se modifican los campos en el DGV
        private void modificarPorDni(object sender, EventArgs e)
        {
            String nombre = txtNombre.Text.Trim();
            String apellido = txtApellido.Text.Trim();
            String direccion = txtDireccion.Text.Trim();
            String dni = txtDNI.Text.Trim();
            String nota = txtNota.Text.Trim();

            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(apellido) || string.IsNullOrWhiteSpace(direccion) || string.IsNullOrWhiteSpace(dni) || string.IsNullOrWhiteSpace(nota))
            {
                MessageBox.Show("Todos los campos deben estar llenos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                for (int i = 0; i < temp.Rows.Count - 1; i++)
                {
                    if (txtCedula.Text.Trim().Equals(temp.Rows[i].Cells[3].Value.ToString()))
                    {
                        temp.Rows[i].Cells[0].Value = txtNombre.Text;
                        temp.Rows[i].Cells[1].Value = txtApellido.Text;
                        temp.Rows[i].Cells[2].Value = txtDireccion.Text;
                        temp.Rows[i].Cells[3].Value = txtCedula.Text;
                        temp.Rows[i].Cells[4].Value = txtNota.Text;
                        temp.Rows[i].Cells[5].Value = cbCalificacion.SelectedItem.ToString();
                        MessageBox.Show(" La modificación se realizó exitosamente ", "Ok", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
            }
            
        }


        /**********************************************************************************************************************/
        //Al presionar el boton se elimina en DNI que hemos consultado
        private void eliminarPorDNI(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCedula.Text.ToString().Trim()))
            {
                MessageBox.Show(" Para poder eliminar, primero consulte el DNI ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                for (int i = 0; i < temp.Rows.Count - 1; i++)
                {
                    if (txtCedula.Text.Trim().Equals(temp.Rows[i].Cells[3].Value.ToString()))
                    {
                        DialogResult resultado = MessageBox.Show("¿Esta seguro que desea eliminar al alumno?", "Confirmacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (resultado == DialogResult.Yes)
                        {
                            temp.Rows.RemoveAt(i);
                            MessageBox.Show(" La modificación se realizó exitosamente ", "Ok", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        else
                        {
                            MessageBox.Show(" Se cancelo la eliminación ", "Ok", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
        }

       

    }
}
