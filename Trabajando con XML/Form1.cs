using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Trabajando_con_XML
{
    public partial class Form1 : Form
    {

        private List<Contacto> listacontactos = new List<Contacto>();

        public Form1()
        {
            InitializeComponent();
            //Cargar valores del ComboBox segun los tipo de telefonia declarados en la Clase Contacto
            cboTelefonia.DataSource = Enum.GetValues(typeof(Contacto.Telefonia));
            cboTelefonia.SelectedIndex = -1;
        }

        private void GuardarXML(object sender, EventArgs e)
        {
            XDocument xmlDoc = new XDocument
                (
                new XElement("Contactos",
                    listacontactos.Select(c =>
                    new XElement("Contacto",
                    new XElement("Nombre", c.Nombre),
                    new XElement("Apellido", c.Apellido),
                    new XElement("Numero", c.Numero),
                    new XElement("Telefonia", c.TipodeTelefonia.ToString())
                    )
                    )
                    )
                );
            xmlDoc.Save("ListaContactos.xml");
            MessageBox.Show("Contactos guardados en XML", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void LeerXML(object sender, EventArgs e)
        {
            if (System.IO.File.Exists("ListaContactos.xml"))
            {
                XDocument xmlDoc = XDocument.Load("ListaContactos.xml");
                listacontactos = xmlDoc.Root.Elements("Contacto")
                    .Select(x => new Contacto(
                        x.Element("Nombre").Value,
                        x.Element("Apellido").Value,
                        x.Element("Numero").Value,
                        (Contacto.Telefonia)Enum.Parse(typeof(Contacto.Telefonia), x.Element("Telefonia").Value)
                        )).ToList();
                MostrarLista();
            }
            else
            {
                MessageBox.Show("No se encontró el archivo XML.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Agregar(object sender, EventArgs e)
        {
            //Validación de que no esten vacios los campos
            if (!string.IsNullOrWhiteSpace(txtNombre.Text) && !string.IsNullOrWhiteSpace(txtApellido.Text) && !string.IsNullOrWhiteSpace(mtbNumero.Text))
            {
               var nuevocontacto = new Contacto (txtNombre.Text, txtApellido.Text, mtbNumero.Text, (Contacto.Telefonia)cboTelefonia.SelectedItem);
                
                listacontactos.Add (nuevocontacto);
                MostrarLista();
                Limpiar();
                MessageBox.Show("Contacto registrado con exito", "Registro Exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }else { MessageBox.Show("Complete todos los campos requeridos.", "Campos vacios o imcompletos", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void Salir(object sender, EventArgs e)
        {
            DialogResult confirmación = MessageBox.Show("¿Esta seguro que quiere salir?","Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmación == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void LimpiarData(object sender, EventArgs e)
        {
            dtgvLista.Rows.Clear();
        }

        private void MostrarenData(object sender, EventArgs e)
        {
            MostrarLista();
        }

        private void Limpiar()
        {
            txtNombre.Clear();
            txtApellido.Clear();
            mtbNumero.Clear();
            cboTelefonia.SelectedIndex = -1;
        }

        private void MostrarLista()
        {
            dtgvLista.Rows.Clear(); // Limpiar filas antes de registrar
            foreach (var contacto in listacontactos)
            {
                dtgvLista.Rows.Add(contacto.Nombre, contacto.Apellido, contacto.Numero, contacto.TipodeTelefonia.ToString());
            }
        }
    }
}
