using Entities;
using Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Utils;

namespace UI
{
    public partial class FrmPokemonList : Form
    {
        private readonly PokemonService _pokemonService;

        public FrmPokemonList()
        {
            InitializeComponent();
            _pokemonService = new PokemonService();
        }

        private void FrmPokemonList_Load(object sender, EventArgs e)
        {
            LoadPokemons();
        }

        private void btnNewPokemon_Click(object sender, EventArgs e)
        {
            FrmPokemon frmPokemon = new FrmPokemon();
            if (frmPokemon.ShowDialog() == DialogResult.OK)
            {
                LoadPokemons();
            }
        }

        private void LoadPokemons()
        {
            dgvPokemoms.DataSource = _pokemonService.GetAllPokemons();
            ConfigureDataGridView();
        }

        private void ConfigureDataGridView()
        {
            dgvPokemoms.Columns["PokemonId"].Visible = false;
            dgvPokemoms.Columns["UrlImage"].Visible = false;

            // dgvPokemoms.Columns["Name"].Width = 200;
            dgvPokemoms.Columns["Edit"].Width = 33;
            dgvPokemoms.Columns["Delete"].Width = 33;

            dgvPokemoms.Columns["Delete"].DisplayIndex = dgvPokemoms.ColumnCount - 1;
            dgvPokemoms.Columns["Edit"].DisplayIndex = dgvPokemoms.ColumnCount - 2;

            dgvPokemoms.Columns["Edit"].Width = 33;
            dgvPokemoms.Columns["Delete"].Width = 33;

        }

        private void dgvPokemoms_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvPokemoms.Columns["Edit"].Index)
            {
                Pokemon selectedPokemon = dgvPokemoms.CurrentRow.DataBoundItem as Pokemon;
                FrmPokemon frmPokemon = new FrmPokemon() { Pokemon = selectedPokemon };
                if (frmPokemon.ShowDialog() == DialogResult.OK)
                {
                    LoadPokemons();
                }
            }
            else if (e.ColumnIndex == dgvPokemoms.Columns["Delete"].Index)
            {
                DialogResult result = Messages.ConfirmMessage("Estas seguro de eliminar este registro");
                if (result == DialogResult.Yes)
                {
                    int id = (int)dgvPokemoms.CurrentRow.Cells["PokemonId"].Value;
                    string urlImage = dgvPokemoms.CurrentRow.Cells["UrlImage"].Value.ToString();
                    if (File.Exists(urlImage) && urlImage != "D:\\DesktopApp\\PokeApp\\UI\\Resources\\default-image.jpg")
                    {
                        File.Delete(urlImage);
                    }
                    _pokemonService.DeletePokemon(id);
                    Messages.SuccessMessage("Pokemon eliminado correctamente");
                    LoadPokemons();
                }
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            List<Pokemon> list = _pokemonService.GetAllPokemons();
            string searchText = txtSearch.Text.Trim();
            var filteredList = list.FindAll(p => p.Name.ToLower().Contains(searchText.ToLower()) || p.Type.ToLower().Contains(searchText.ToLower()));
            dgvPokemoms.DataSource = filteredList;
            ConfigureDataGridView();
        }

        private void FrmPokemonList_Resize(object sender, EventArgs e)
        {
            ConfigureDataGridView();
        }
    }
}
