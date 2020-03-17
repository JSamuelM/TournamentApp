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
using TournamentApp.Models;
using TournamentApp.Utils;
using static TournamentApp.Models.FormValidators;

namespace TournamentApp
{
    public partial class frmjugador : UserControl
    {

        private static frmjugador _instance;
        public static frmjugador Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new frmjugador();
                }
                return _instance;
            }
        }
        private static PlayerController playerController = new PlayerController();
        private static TeamController teamController = new TeamController();
        private List<player> player = new List<player>();
        private List<team> team = new List<team>();
        private player selectedPlayer = null;

        private int[] columnsToChange = { 0, 1, 2, 3, 4 };
        private int[] columnsToHide = { 5 };
        private string[] titlesforColumns = { "ID", "Nombre", "Apellido", "Edad", "team id" };

        private void fillSelectedData(player currentPlayer)
        {
            // Le damos el valor del torneo seleccionado accediendo a su propiedad
            txtnombrejug.Text = currentPlayer.player_name;
            txtapellidojug.Text = currentPlayer.playeR_surname;
            txtedadjug.Text = Convert.ToString(currentPlayer.player_age);
            cmbEquipo.SelectedValue = currentPlayer.team_id;
        }

        // Cargar Tabla
        private void loadTable()
        {
            try
            {
                Operation<player> getPlayerOperation = playerController.getRecords();
                if (getPlayerOperation.State)
                {
                    player = getPlayerOperation.Data;
                    dgvJugadores.DataSource = player;
                    // Arreglo de titulos a cambiar, columnas a cambiar y el contrl datagridview
                    FormUtils.changeTitlesForDgv(titlesforColumns, columnsToChange, dgvJugadores);
                    // Esconder las columnas y datagridview
                    FormUtils.hideColumnsForDgv(columnsToHide, dgvJugadores);
                    return;
                }
                MessageBox.Show("Error al cargar los datos de los super cracks", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                FormUtils.defaultErrorMessage(ex);
            }
        }
        private void fillCombo()
        {
            try
            {
                Operation<team> getteamOperation = teamController.getRecords();
                if (getteamOperation.State)
                {
                    team = getteamOperation.Data;
                   cmbEquipo.DataSource = team;
                    cmbEquipo.DisplayMember = "team_name";
                    cmbEquipo.ValueMember = "id";
                    // Arreglo de titulos a cambiar, columnas a cambiar y el contrl datagridview
                    return;
                }
                MessageBox.Show("Error al cargar los datos de los super cracks", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                FormUtils.defaultErrorMessage(ex);
            }

        }

        // Guardar registro
        private void saveData()
        {
            // Creamos un objeto temporal de tipo de la entidad (torneo)
            player tempPlayer = new player
            {
                player_name = txtnombrejug.Text,
                playeR_surname = txtapellidojug.Text,
                player_age = Convert.ToSByte(txtedadjug.Text),
                team_id = Convert.ToInt32(cmbEquipo.SelectedValue)
            };
            // Realizamos la operación addRecord
            Operation<player> operation = playerController.addRecord(tempPlayer);

            if (operation.State)
            {
                MessageBox.Show("supercrack agregado con éxito", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                loadTable();
                cleanForm();
            }
        }

        // Actualizar registro
        private void updateData(player currentPlayer)
        {
            Operation<player> operation = playerController.updateRecord(currentPlayer);
            if (operation.State)
            {
                MessageBox.Show("supercrack actualizado con éxito", "Éxito",
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
        private void deleteData(player currentPlayer)
        {
            Operation<player> operation = playerController.deleteRecord(currentPlayer);
            if (operation.State)
            {
                MessageBox.Show("supercrack elimnado con éxito", "Éxito",
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
            btnagregar.Text = "Guardar";
            selectedPlayer = null;
            errorProvider.Clear();
            cmbEquipo.SelectedIndex = -1;
        }

        private ToValidate[] getValidators()
        {
            ToValidate[] validators =
            {
                new ToValidate(txtnombrejug, new ControlValidator[] { FormValidators.hasText },
                new string[] { "Ingresa un nombre de supercrack" })
            };
            return validators;
        }

        private Control[] textControls()
        {
            Control[] controls =
            {
                txtnombrejug,
                txtapellidojug,
                txtedadjug
            };
            return controls;
        }
        public frmjugador()
        {
            InitializeComponent();
        }

        private void btnnuevo_Click(object sender, EventArgs e)
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            try
            {
                int index = e.RowIndex;
                if (index >= 0)
                {
                    selectedPlayer = player[index];
                    btnagregar.Text = "Modificar";
                    fillSelectedData(selectedPlayer);
                }
            }
            catch (Exception ex)
            {
                FormUtils.defaultErrorMessage(ex);
            }
        }

        private void btnagregar_Click(object sender, EventArgs e)
        {
            try
            {
                List<ControlErrorProvider> errorProvider = FormValidators.validFormTest(getValidators());
                bool isValid = errorProvider == null;
                if (isValid)
                {
                    if (selectedPlayer == null)
                    {
                        saveData();
                    }
                    else
                    {
                        selectedPlayer.player_name = txtnombrejug.Text;
                        updateData(selectedPlayer);
                    }
                }
                else
                {
                    this.errorProvider.Clear();
                    MessageBox.Show("Algunos datos ingresados son inválidos.\n" +
                        "Pase el puntero sobre los íconos de error para ver los detalles de cada campo.", "Error al ingresar datos",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    foreach (ControlErrorProvider controlErrorProvider in errorProvider)
                    {
                        this.errorProvider.SetError(controlErrorProvider.ControlName,
                            controlErrorProvider.ErrorMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                FormUtils.defaultErrorMessage(ex);
            }
        }

        private void btneliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (selectedPlayer == null)
                {
                    MessageBox.Show("Seleccione un registro primero", "Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
                else
                {
                    selectedPlayer.player_name = txtnombrejug.Text;
                    deleteData(selectedPlayer);
                }
            }
            catch (Exception ex)
            {
                FormUtils.defaultErrorMessage(ex);
            }
        }

        private void frmjugador_Load(object sender, EventArgs e)
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
            try
            {
                fillCombo();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al cargar",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            cleanForm();
           
        }
    }
}
