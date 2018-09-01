﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Milibreria;

namespace GestorInformatico
{
    public partial class AbmBarrio : Form
    {
        public AbmBarrio()
        {
            InitializeComponent();
        }
        int a = 0;
        private void AbmBarrio_Load(object sender, EventArgs e)
        {

            cmbProvincia.DataSource = Milibreria.Utilidades.Ejecutar("Select * from Provincia");
            cmbProvincia.DisplayMember = "Descripcion";
            cmbProvincia.ValueMember = "idProvincia";
            cmbProvincia.SelectedIndex = -1;
            a = 1;
            LlenarGrilla();
         
        }


        private void EstaSeguro(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Desea salir sin guardar?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void cmbProvincia_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (a == 1 && cmbProvincia.SelectedValue != null)
            {
                a = 0;
                cmbDepartamento.DataSource = Milibreria.Utilidades.Ejecutar("Select * from Departamento"
                + " where IdProvincia = " + cmbProvincia.SelectedValue.ToString());
                cmbDepartamento.DisplayMember = "Descripcion";
                cmbDepartamento.ValueMember = "IdDepartamento";
                cmbDepartamento.SelectedIndex = -1;
                a = 1;
            }
            cmbDepartamento.SelectedIndex = -1;
            cmbLocalidad.SelectedIndex = -1;
        }
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (txtBarrio.Text != "")
            {
                DataTable table;
                table = Milibreria.Utilidades.Ejecutar("Select l.Descripcion as Localidad,D.Descripcion as Departamento,P.Descripcion as Provincia,* from barrio b"
                + " join Localidad l on b.IdLocalidad = l.IdLocalidad"
                + " join Deptartamento d on d.Localidad = l.IdLocalidad "
                + " join Provincia p on p.idDepartamento = p.IdDepartamento"
                + " where Descripcion Like '" + txtBarrio.Text + "'");

                cmbLocalidad.Text = table.Rows[0]["Localidad"].ToString();
                cmbDepartamento.Text = table.Rows[0]["Departamento"].ToString();
                cmbProvincia.Text = table.Rows[0]["Provincia"].ToString();



            }
        }

        private void cmbDepartamento_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (a == 1 && cmbDepartamento.SelectedValue != null)
            {
                a = 0;
                cmbLocalidad.DataSource = Milibreria.Utilidades.Ejecutar("Select * from Localidad"
                + " where IdDepartamento = " + cmbDepartamento.SelectedValue.ToString());
                cmbLocalidad.DisplayMember = "Descripcion";
                cmbLocalidad.ValueMember = "IdLocalidad";
                cmbLocalidad.SelectedIndex = -1;
                a = 1;
            }
            cmbLocalidad.SelectedIndex = -1;
        }

        private void LlenarGrilla()
        {
            DataTable table;
            table = Milibreria.Utilidades.Ejecutar("Select b.Descripcion,l.Descripcion as Localidad,d.Descripcion as Departamento,P.Descripcion as Provincia"
            +" from barrio b "
            +"  join Localidad l on b.IdLocalidad = l.IdLocalidad" 
            +" join Departamento d on d.IdDepartamento = l.IdDepartamento"  
               +" join Provincia p on p.IdProvincia =d.IdProvincia");
            dataGridView1.Rows.Clear();
            if (table.Rows.Count > 0)
            {
                
                for ( int i = 0; i < table.Rows.Count; i++)
                {
                    table.Rows.Add(table.Rows[i]["Descripcion"].ToString()
                                   ,table.Rows[i]["Localidad"].ToString()
                                   ,table.Rows[i]["Departamento"].ToString()
                                   ,table.Rows[i]["Provincia"].ToString());
                }
            }
        }
    }

}
