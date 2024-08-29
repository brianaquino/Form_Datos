using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;

namespace Form_Datos
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();


            // agregar controladores de eventos TextChanged a los campos
            tbNombre.TextChanged += validarNombre;
            tbApellidos.TextChanged += validarApellidos;
            tbEdad.TextChanged += validarEdad;
            tbEstatura.TextChanged += validarEstatura;
            tbTelefono.Leave += validarTelefono;
          
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string nombres = tbNombre.Text;
            string apellidos = tbApellidos.Text;
            string edad = tbEdad.Text;
            string estatura = tbEstatura.Text;
            string telefono = tbTelefono.Text;
            string genero = "";

            if (rbHombre.Checked)
            {
                genero = "Hombre";
            }
            else if (rbMujer.Checked)
            {
                genero = "Mujer";
            }
               

            string datos = $"Nombres: {nombres}\r\nApellidos: {apellidos}\r\nEdad: {edad}\r\nEstatura: {estatura}\r\nTelefono: {telefono}\r\nGenero: {genero}";

            string rutaArchivo = "C:/Users/briaq/OneDrive/Documents/datos.txt";
            bool archivoExiste = File.Exists(rutaArchivo);

            using (StreamWriter writer = new StreamWriter(rutaArchivo, true))
            {
                if (archivoExiste)
                {
                    writer.WriteLine();
                }

                writer.WriteLine(datos);
            }

            MessageBox.Show("Datos guardados con exito:\n\n" + datos, "Informacion",
                MessageBoxButtons.OK, MessageBoxIcon.Information);  
        }
        private bool EsEnteroValido(string valor)
        {
            int resultado;
            return int.TryParse(valor, out resultado);

        }

        private bool EsDecimalValido(string valor)
        {
            decimal resultado;
            return decimal.TryParse(valor, out resultado);
        }

        private bool EsEnteroValido10Digitos(string valor)
        {
            long resultado;
            return long.TryParse(valor, out resultado)&&valor.Length == 10;
        }

        private bool EsTextoValido(string valor)
        {
            return Regex.IsMatch(valor, @"^[a-zA-Z\s]+$");
        }

        private void validarEdad(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (!EsEnteroValido(textBox.Text))
            {
                MessageBox.Show("Por favor, ingrese una edad valida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox.Clear();
            }
        }

        private void validarEstatura(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (!EsDecimalValido(textBox.Text))
            {
                MessageBox.Show("Por favor, ingrese una Estatura valida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox.Clear();
            }
        }
        private void validarTelefono (object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text.Length == 10 && EsEnteroValido10Digitos(textBox.Text))
            {
                textBox.BackColor = Color.Blue;
            }
            else
            {
                textBox.BackColor = Color.Gray;
                MessageBox.Show("Por favor, ingrese un número de telefono valido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox.Clear();
            }
            /*  if (!EsEnteroValido10Digitos(textBox.Text))
               {
                   MessageBox.Show("Por favor, ingrese un número de telefono valido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                   //textBox.Clear();
               }*/
        }

        private void validarNombre (object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (!EsTextoValido(textBox.Text))
            {
                MessageBox.Show("Por favor, ingrese una nombre valido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox.Clear();
            }

        }
        private void validarApellidos(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (!EsTextoValido(textBox.Text))
            {
                MessageBox.Show("Por favor, ingrese una apellido valido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox.Clear();
            } 
        }

        private void btnLimpiar_Click_1(object sender, EventArgs e)
        {
            tbNombre.Clear();
            tbApellidos.Clear();
            tbTelefono.Clear();
            tbEdad.Clear();
            tbEstatura.Clear();
            rbHombre.Checked = false;
            rbMujer.Checked = false;
        }

      
    }
}
