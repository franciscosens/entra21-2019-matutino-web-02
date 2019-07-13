using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Repository
{
    public class VeiculoRepository
    {
        public int Inserir(Veiculo veiculo)
        {
            SqlCommand comando = Conexao.Conectar();
            comando.CommandText = @"INSERT INTO veiculos
(modelo, id_categoria, valor)
OUTPUT INSERTED.ID VALUES
(@MODELO, @ID_CATEGORIA, @VALOR)";
            comando.Parameters.AddWithValue("@MODELO", veiculo.Modelo);
            comando.Parameters.AddWithValue("@ID_CATEGORIA", veiculo.IdCategoria);
            comando.Parameters.AddWithValue("@VALOR", veiculo.Valor);
            int id = Convert.ToInt32(comando.ExecuteScalar());
            comando.Connection.Close();
            return id;
        }

        public List<Veiculo> ObterTodos()
        {
            SqlCommand comando = Conexao.Conectar();
            comando.CommandText = @"SELECT veiculos.id AS 'VeiculoId',
veiculos.modelo AS 'VeiculoModelo',
veiculos.valor AS 'VeiculoValor',
veiculos.id_categoria AS 'VeiculoIdCategoria',
categorias.nome AS 'CategoriaNome'
FROM veiculos 
INNER JOIN categorias ON (veiculos.id_categoria = categorias.id);";

            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());
            comando.Connection.Close();

            List<Veiculo> veiculos = new List<Veiculo>();
            foreach (DataRow linha in tabela.Rows)
            {
                Veiculo veiculo = new Veiculo();
                veiculo.Id = Convert.ToInt32(linha["VeiculoId"]);
                veiculo.Modelo = linha["VeiculoModelo"].ToString();
                veiculo.Valor = Convert.ToDecimal(linha["VeiculoValor"]);
                veiculo.IdCategoria = Convert.ToInt32(linha["VeiculoIdCategoria"]);
                veiculo.Categoria = new Categoria();
                veiculo.Categoria.Nome = linha["CategoriaNome"].ToString();
                veiculos.Add(veiculo);
            }
            return veiculos;
        }

        public bool Apagar(int id)
        {
            SqlCommand comando = Conexao.Conectar();
            comando.CommandText = "DELETE FROM veiculos WHERE @ID = id";
            comando.Parameters.AddWithValue("@ID", id);
            int quantidadeAfetada = comando.ExecuteNonQuery();
            comando.Connection.Close();
            return quantidadeAfetada == 1;
        }

        public bool Alterar(Veiculo veiculo)

        {
            SqlCommand comando = Conexao.Conectar();
            comando.CommandText = @"UPDATE veiculos SET 
modelo = @MODELO, 
id_categoria = @ID_CATEGORIA, 
valor = @VALOR 
WHERE id = @Id";
            comando.Parameters.AddWithValue("@MODELO", veiculo.Modelo);
            comando.Parameters.AddWithValue("@ID_CATEGORIA", veiculo.IdCategoria);
            comando.Parameters.AddWithValue("@VALOR", veiculo.Valor);
            comando.Parameters.AddWithValue("@ID", veiculo.Id);
            int quantidadeAfetada = comando.ExecuteNonQuery();
            comando.Connection.Close();
            return quantidadeAfetada == 1;
        }

        public Veiculo ObterPeloId(int id)
        {
            SqlCommand comando = Conexao.Conectar();
            comando.CommandText = @"SELECT veiculos.id AS 'VeiculoId',
veiculos.modelo AS 'VeiculoModelo',
veiculos.valor AS 'VeiculoValor',
veiculos.id_categoria AS 'VeiculoIdCategoria',
categorias.nome AS 'CategoriaNome'
FROM veiculos 
INNER JOIN categorias ON (veiculos.id_categoria = categorias.id)
WHERE veiculos.id = @ID";
            comando.Parameters.AddWithValue("@ID", id);

            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());
            comando.Connection.Close();

            List<Veiculo> veiculos = new List<Veiculo>();
            if (tabela.Rows.Count == 0)
            {
                return null;
            }

            DataRow linha = tabela.Rows[0];
            Veiculo veiculo = new Veiculo();
            veiculo.Id = Convert.ToInt32(linha["VeiculoId"]);
            veiculo.Modelo = linha["VeiculoModelo"].ToString();
            veiculo.Valor = Convert.ToDecimal(linha["VeiculoValor"]);
            veiculo.IdCategoria = Convert.ToInt32(linha["VeiculoIdCategoria"]);
            veiculo.Categoria = new Categoria();
            veiculo.Categoria.Nome = linha["CategoriaNome"].ToString();

            return veiculo;
        }
    }
}
