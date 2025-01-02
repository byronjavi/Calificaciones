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
    public partial class Form1 : Form
    {
        private DataTable contenedor;
        public Form1()
        {
            /**********************************************************************************************************************/
            //Inicializamos todos los componentes y las variables que utilizaremos
            InitializeComponent();
            dgvContenedor.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvContenedor.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvContenedor.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            contenedor = new DataTable(); //Esta DataTable nos ayudara a llenar el DataGridView
            contenedor.Columns.Add("Nombre", typeof(string)); //Definimos la columna Nombre
            contenedor.Columns.Add("Apellido", typeof(string)); //Definimos la columna Apellido
            contenedor.Columns.Add("Direccion", typeof(string)); //Definimos la columna Direccion
            contenedor.Columns.Add("DNI", typeof(string)); //Definimos la columna DNI
            contenedor.Columns.Add("Nota", typeof(float)); // Definimos la columna Nota
            contenedor.Columns.Add("Calificacion", typeof(string)); //Definimos la columna Calificacion
        }

        /**********************************************************************************************************************/
        //Esta funcion la utilizamos para validar si lis campos tienen informacion o estan vacios
        private bool validarCampos(String nombre, String appellido, String direccion, String dni, String nota, String calificacion)
        {
            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(appellido) || string.IsNullOrWhiteSpace(direccion) || string.IsNullOrWhiteSpace(nota) || string.IsNullOrWhiteSpace(dni))
            {
                return false; //Si estan vacios retorna falso
            }
            else
                return true; //Si estan llenos retorna verdadero
        }

        /**********************************************************************************************************************/
        //Esta funcion la utilizo para calcular la calificaion de acuerdo a la nota ingresada, esto de acuerdo a las indicaciones
        private String calcularNota(float numero)
        {
            String cadena = "";
            if (numero < 5)
            {
                cadena = "SS";
            }
            else if (numero >= 5 && numero < 7)
            {
                cadena = "AP";
            }
            else if (numero >= 7 && numero < 9)
            {
                cadena = "NT";
            }
            else
                cadena = "SB";

            return cadena;
        }

        /**********************************************************************************************************************/
        //Esta función es para asegurarme que el usuario solo ingrese un digito entero positivo entre 0 y 9 y si es una letra la borre
        private void validarNumero(object sender, EventArgs e)
        {
            if(System.Text.RegularExpressions.Regex.IsMatch(txtDni.Text,"[^0-9]"))
            {
                txtDni.Text = System.Text.RegularExpressions.Regex.Replace(txtDni.Text, "[^0-9]","");
            }
        }

        /**********************************************************************************************************************/
        //Esta función la utilizo para validar que el DNI no se encuentre registrado
        private bool validarDniEnDGV(String myDni)
        {
            if (dgvContenedor.Rows.Count < 1)
                return true;
            for(int i = 0; i < dgvContenedor.Rows.Count-1;i++) //Con este for recorremos todo el DGV
            {
                if (dgvContenedor.Rows[i].Cells["DNI"].Value.ToString() == myDni) //Verificamos si el DNI ya se encuentra registrado
                    return false;
            }
            return true;
        }

        /**********************************************************************************************************************/
        //Esta funcion la utilizo para aplicar los fultros en el contenedor, SS, AP o MH
        private void filtrarCalificacion(int indicador)
        {
            DataView dataView = new DataView(contenedor);
            if(indicador == 1)
                dataView.RowFilter =  "Calificacion = 'SS'";
            else if(indicador == 2)
                dataView.RowFilter = "Calificacion = 'AP'";
            else
                dataView.RowFilter = "Nota = '10'";
            dgvContenedor.DataSource = dataView;
        }

        /**********************************************************************************************************************/
        //Esta función la utilizó para ingresar los datos, primero en el DataTable y luego en el DataGridView
        private void ingresarDatos(object sender, EventArgs e)
        {
            String nombre = txtNombre.Text.Trim();
            String apellido = txtApellido.Text.Trim();
            String direccion = TxtDireccion.Text.Trim();
            String dni = txtDni.Text.Trim();
            String nota = txtNota.Text.Trim();
            String calificacion = "";

            if (validarCampos(nombre, apellido, direccion, dni, nota, calificacion)) //Valido si los campos esta llenos, con información
            {
                try
                {
                        float mynota = float.Parse(nota); 

                        if (mynota > 0 && mynota <= 10) //ME aseguro que la nota ingresada sea un valor entre 0 y 10
                        {
                            if (float.TryParse(nota, out mynota)) //La convierto al formato deseado
                            {
                                if(validarDniEnDGV(dni)) //Valido que el DNI no exista en el DataGridView
                                {
                                    calificacion = calcularNota(mynota);

                                    /************/
                                    contenedor.Rows.Add(nombre, apellido, direccion, dni, nota, calificacion); //Guardo en el DataTable
                                    dgvContenedor.DataSource = contenedor; //Cargo el DGV con el DataTable
                                    /***********/

                                    //Limpio todos los campos para que el usuario pueda registrar otro alumno
                                    txtNombre.Clear();
                                    txtApellido.Clear();
                                    TxtDireccion.Clear();
                                    txtDni.Clear();
                                    txtNota.Clear();
                                    txtCalificacion.Clear();
                                    txtNombre.Focus(); //Pongo el foco en el campo nombre, es decir el cursor
                                }
                                else
                                {
                                    MessageBox.Show("El DNI ya se encuentra registrado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            else
                                MessageBox.Show("Debe ingresar un dato numérico en nota", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                            MessageBox.Show("Debe ingresar un número > cero y <= a 10", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception e1)
                    {
                        //MessageBox.Show("Debe ingresar un dato numérico en nota", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        MessageBox.Show(e1.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
            }
            else
            {
                MessageBox.Show("Debe llenar todos los campos","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        /**********************************************************************************************************************/
        //Esta funcion llama a una nueva ventana para realizar la consulta por DNI
        private void consultarPorDni(object sender, EventArgs e)
        {
            dgvContenedor.DataSource = contenedor;
            Consulta nueva = new Consulta(dgvContenedor); //Llamo a la nueva ventana y le paso por parametro el DGV
            nueva.Show(); //Este metodo me permite mostrar la nueva ventana
        }

        private void mostrarSuspensos(object sender, EventArgs e)
        {
            filtrarCalificacion(1); //Filtro por suspensos
        }

        private void mostrarAprobados(object sender, EventArgs e)
        {
            filtrarCalificacion(2); //Filtro por aprobados
        }

        private void mostrarMH(object sender, EventArgs e)
        {
            filtrarCalificacion(3); //Filtro por MH
        }

        
        
    }
}
