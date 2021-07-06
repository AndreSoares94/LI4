using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsultaJa
{
    public class Administrador
    {
        /**
         * Variável que guarda os pedidos
         * de inscrição para médicos. Notar
         * que neste Map a chave é o email
         */
        private Dictionary<string, Medico> pedidosInsc;

        /**
         * Construtor para objetos da 
         * classe Administrador
         */
        public Administrador(string email, string password, string nome, DateTime dataNascimento)
        {
            this.pedidosInsc = new Dictionary<string, Medico>();
        }

        /**
         * Método que permite adicionar um pedido 
         * de inscrição por parte de um novo médico
         */
        public void fazerPedido(Medico m)
        {
            Medico test;
            if (this.pedidosInsc.TryGetValue(m.getEmail(), out test))
                throw new Exception("Esse mail já efetuou envio de pedido");
            /* Se não existir o mail na lista de pedidos adicionamos o novo pedido de inscrição */
            this.pedidosInsc.Add(m.getEmail(), m);
        }

        /**
         * Método que permite remover um pedido 
         * de inscrição da lista dos pedidos, 
         * fornecendo o mail do médico em questão
         */
        public Medico removerPedido(string email)
        {
            Medico m;
            if(this.pedidosInsc.TryGetValue(email, out m))
                this.pedidosInsc.Remove(email);
            return m;
        }

        /**
         * Implementação obrigatória do método 
         * getHistorico() para superclasses 
         * da classe Conta
         */
        public Dictionary<int, Consulta> getHistorico()
        {
            return null;
        }

        /**
         * Implementação obrigatória do método 
         * getConsultasAgendadas() para 
         * superclasses da classe Conta
         */
        public Dictionary<int, Consulta> getConsultasAgendadas()
        {
            return null;
        }
    }
}
