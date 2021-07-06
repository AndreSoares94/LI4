using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsultaJa
{
    public class Prescricao
    {
        /**
         * Variável que guarda o inteiro de ordem 
         * da prescrição na receita total passada 
         * aquando da consulta
         */
        private int seq;

        /**
         * Variável que guarda o id de consulta à 
         * qual a prescrição está associada
         */
        private int idConsulta;

        /**
         * Variável que guarda o nome do farmaco 
         * da prescrição em questão
         */
        private string nomeFarmaco;

        /**
         * Variável que reflete a quantidade de 
         * farmaco associado à prescrição
         */
        private decimal quantidade;

        /**
         * Variável que guarda a posologia associada 
         * ao farmaco em questão
         */
        private string posologia;

        /**
         * Construtor para objetos da classe Prescricao
         */
        public Prescricao(int seq, int idConsulta, string nomeFarmaco, decimal quantidade, string posologia)
        {
            this.seq = seq;
            this.idConsulta = idConsulta;
            this.nomeFarmaco = nomeFarmaco;
            this.quantidade = quantidade;
            this.posologia = posologia;
        }

        public int getSeq()
        {
            return this.seq;
        }

        public int getIdConsulta()
        {
            return this.idConsulta;
        }

        public string getNomeFarmaco()
        {
            return this.nomeFarmaco;
        }

        public decimal getQuantidade()
        {
            return this.quantidade;
        }

        public string getPosologia()
        {
            return this.posologia;
        }

        public override bool Equals(object obj)
        {
            if (this == obj)
                return true;
            if (obj == null || obj.GetType() != this.GetType())
                return false;
            Prescricao presc = (Prescricao)obj;

            return this.getIdConsulta() == presc.getIdConsulta()
                    && this.getSeq() == presc.getSeq();
        }
    }
}
