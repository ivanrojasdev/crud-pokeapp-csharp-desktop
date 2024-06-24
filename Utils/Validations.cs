using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public class Validations
    {
        //POKEMON
        public static readonly string POKEMON_NAME = @"^[a-zA-ZÁÉÍÓÚÑáéíóúñ\s]{3,28}$";
        public static readonly string POKEMON_TYPE = @"^[a-zA-ZÁÉÍÓÚÑáéíóúñ]{4,9}$";
        public static readonly string POKEMON_LEVEL = @"^([2-9]|[1-9][0-9]|[1][0-9][0-9]|[2][0][0])$";

    }
}
