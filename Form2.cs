using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TournamentApp.Controllers;
using TournamentApp.Entity_Framework;
using TournamentApp.Model;
using TournamentApp.Utils;

namespace TournamentApp
{
    public partial class Form2 : Form
    {

        private static TeamController teamController = new TeamController();
        private List<team> team = new List<team>();

        private team selectedTeam = null;

        private int[] columnsToChange = { 0, 1 };
        private int[] columnsToHide = { 2 };
        private string[] titlesforColumns = { "ID", "Nombre de equipo" };
        public Form2()
        {
            InitializeComponent();

        }
        // Llenar los datos seleccionados
        private void fillSelectedData(team currentTeam)
        {
            // Le damos el valor del equipo seleccionado accediendo a su propiedad
            txtName.Text = currentTeam.team_name;
        }

        // Cargar Tabla
        private void loadTable()
        {
            Operation<team> getTeamOperation = teamController.getRecords();
            if (getTeamOperation.State)
            {
                team = getTeamOperation.Data;
                dgvTeam.DataSource = team;
                // Arreglo de titulos a cambiar, columnas a cambiar y el contrl datagridview
                FormUtils.changeTitlesForDgv(titlesforColumns, columnsToChange, dgvTeam);
                // Esconder las columnas y datagridview
                FormUtils.hideColumnsForDgv(columnsToHide, dgvTeam);
                return;
            }
            MessageBox.Show("Error al cargar los datos de los equipos", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        // Guardar registro
        private void saveData()
        {
            // Creamos un objeto temporal de tipo de la entidad (equipo)
            team tempTeam = new team
            {
                team_name = txtName.Text
            };
            // Realizamos la operación addRecord
            Operation<team> operation = teamController.addRecord(tempTeam);
            if (operation.State)
            {
                MessageBox.Show("Equipo agregado con éxito", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                loadTable();
                cleanForm();
            }
        }

        // Actualizar registro
        private void updateData(team currentTeam)
        {
            Operation<team> operation = teamController.updateRecord(currentTeam);
            if (operation.State)
            {
                MessageBox.Show("Equipo actualizado con éxito", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                loadTable();
                cleanForm();
            }
            else
            {
                MessageBox.Show(operation.Error, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Eliminar un registro
        private void deleteData(team currentTeam)
        {
            Operation<team> operation = teamController.deleteRecord(currentTeam);
            if (operation.State)
            {
                MessageBox.Show("Equipo elimnado con éxito", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                loadTable();
                cleanForm();
            }
            else
            {
                MessageBox.Show(operation.Error, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Limpiar formulario
        private void cleanForm()
        {
            FormUtils.clearTextbox(textControls());
            btnAgregar.Text = "Guardar";
        }

        private Control[] textControls()
        {
            Control[] controls =
            {
                txtName
            };
            return controls;
        }
        

        private void Form2_Load(object sender, EventArgs e)
        {
            try
            {
                loadTable();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al cargar",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvTeam_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int index = e.RowIndex;
                if (index >= 0)
                {
                    selectedTeam = team[index];
                    btnAgregar.Text = "Modificar";
                    fillSelectedData(selectedTeam);
                }
            }
            catch (Exception ex)
            {
                FormUtils.defaultErrorMessage(ex);
            }
        }

        private void btnEliminar_Click_1(object sender, EventArgs e)
        {
            if (selectedTeam == null)
            {
                MessageBox.Show("Seleccione un registro primero", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            else
            {
                selectedTeam.team_name = txtName.Text;
                deleteData(selectedTeam);
            }
        }

        private void btnNuevo_Click_1(object sender, EventArgs e)
        {
            try
            {
                cleanForm();
            }
            catch (Exception ex)
            {
                FormUtils.defaultErrorMessage(ex);
            }
        }

        private void btnAgregar_Click_1(object sender, EventArgs e)
        {
            try
            {
              
                if (selectedTeam == null)
                {
                    saveData();
                }
                else
                {
                    selectedTeam.team_name = txtName.Text;
                    updateData(selectedTeam);
                }
              
            }
            catch (Exception ex)
            {
                FormUtils.defaultErrorMessage(ex);
            }
        }
    }
}
