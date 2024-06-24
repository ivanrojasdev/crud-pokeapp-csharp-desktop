using Entities;
using Services;
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Utils;

namespace UI
{
    public partial class FrmPokemon : Form
    {
        private readonly PokemonService _pokemonService;
        public Pokemon Pokemon { get; set; }
        private string _urlImage;

        public FrmPokemon()
        {
            InitializeComponent();
            _pokemonService = new PokemonService();
        }

        private void FrmPokemon_Load(object sender, System.EventArgs e)
        {
            LoadPokemon();
        }

        private void btnSavePokemon_Click(object sender, System.EventArgs e)
        {
            SavePokemon();
        }

        private void pbxImage_Click(object sender, EventArgs e)
        {
            LoadImage();
        }

        private void lblUploadImage_Click(object sender, System.EventArgs e)
        {
            LoadImage();
        }

        private void SavePokemon()
        {
            try
            {
                if (!ValidateFields())
                {
                    return;
                }

                if (Pokemon == null)
                {
                    Pokemon = new Pokemon();
                }

                bool imageChanged = _urlImage != null && _urlImage != Pokemon.UrlImage;

                if (imageChanged)
                {
                    if (File.Exists(Pokemon.UrlImage) && Pokemon.UrlImage != "D:\\DesktopApp\\PokeApp\\UI\\Resources\\default-image.jpg")
                    {
                        File.Delete(Pokemon.UrlImage);
                    }
                    Pokemon.UrlImage = Images.SaveImage(_urlImage, "pokemons");
                }

                Pokemon.Name = txtName.Text.Trim();
                Pokemon.Type = txtType.Text.Trim();
                Pokemon.Level = int.Parse(txtLevel.Text.Trim());


                if (_pokemonService.SavePokemon(Pokemon))
                {
                    Messages.SuccessMessage("Pokemon guardado correctamente");
                    DialogResult = DialogResult.OK;
                }
                else
                {
                    Messages.ErrorMessage("Error al guardar el pokemon");
                }
            }
            catch (Exception ex)
            {
                Messages.ErrorMessage(ex.Message);
            }
        }

        private void LoadPokemon()
        {
            if (Pokemon != null)
            {
                txtName.Text = Pokemon.Name;
                txtType.Text = Pokemon.Type;
                txtLevel.Text = Pokemon.Level.ToString();
                pbxImage.Load(Pokemon.UrlImage);
                lblTitlePokemon.Text = "Editar Pokemon";
                lblUploadImage.Text = "Cambiar imagen";
            }
        }

        private void LoadImage()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Seleccionar imagen";
            ofd.Filter = "Archivos de imagen|*.jpg;*.jpeg;*.png";
            ofd.FileName = String.Empty;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                _urlImage = ofd.FileName;
                pbxImage.Load(_urlImage);
            }
        }

        public bool ValidateFields()
        {
            string name = txtName.Text.Trim();
            string type = txtType.Text.Trim();
            string level = txtLevel.Text.Trim();

            if (name == string.Empty)
            {
                Messages.ErrorMessage("El campo nombre es obligatorio");
                return false;
            }

            if (!Regex.IsMatch(name, Validations.POKEMON_NAME))
            {
                Messages.ErrorMessage("El nombre del pokemon debe tener entre 3 y 28 caracteres y solo puede contener letras y espacios");
                return false;
            }

            if (type == string.Empty)
            {
                Messages.ErrorMessage("El campo tipo es obligatorio");
                return false;
            }

            if (!Regex.IsMatch(type, Validations.POKEMON_TYPE))
            {
                Messages.ErrorMessage("El tipo de pokemon debe tener entre 4 y 9 caracteres y solo puede contener letras");
                return false;
            }

            if (level == string.Empty)
            {
                Messages.ErrorMessage("El campo nivel es obligatorio");
                return false;
            }

            if (!Regex.IsMatch(level, Validations.POKEMON_LEVEL))
            {
                Messages.ErrorMessage("El nivel del Pokémon debe estar entre 2 y 200");
                return false;
            }
            return true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
