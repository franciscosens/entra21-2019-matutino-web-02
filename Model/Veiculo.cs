using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Veiculo
    {
        public int Id;
        public string Modelo;
        public decimal Valor;

        //Propriedade para a coluna do id_categoria(FK)
        public int IdCategoria;

        /*Objeto da categoria que permitirá acessar as 
         * informações de categoria através do veículo.
         */
        public Categoria Categoria;

    }
}
