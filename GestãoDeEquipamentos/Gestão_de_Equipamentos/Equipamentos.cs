using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestão_de_Equipamentos
{
    class Equipamentos
    {
        private string nome;
        private string fabricante;
        private double precoAquisicao;
        private string numeroDeSerie;
        private int id;
    
    
        public Equipamentos(string nome, string fabricante, string numeroDeSerie, double precoAquisicao, int id )
        {
            this.nome = nome;
            this.fabricante = fabricante;
            this.numeroDeSerie = numeroDeSerie;
            this.precoAquisicao = precoAquisicao;
            this.id = id;
        
        }

        public string getNome()
        {
            return this.nome;
        }

        public void setNome(string nome)
        {
            this.nome = nome;
        }
    
    }



}
