using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XulambsFoods_2025_1.src {
    /// <summary>
    /// Um pedido pode agrupar várias pizzas. Deve exibir um relatório descritivo 
    /// com o detalhamento das pizzas e o valor total a pagar.
    /// </summary>
    public class Pedido {
        /// <summary>
        /// Static para geração do id do pedido seguinte.
        /// </summary>
        private static int s_ultimoPedido = 0;
        /// <summary>
        /// Apenas para controle do vetor de pizzas.
        /// </summary>
        private const int MaxPizzas = 100;
        /// <summary>
        /// Máximo de pizzas permitidas(pelo controle de tamanho)
        /// </summary>
        private int _maxPizzas;
        private int _idPedido;
        private DateOnly _data;
        private Pizza[] _pizzas;
        private int _quantPizzas;
        private bool _aberto;

        public void init(int quantidade) {
            s_ultimoPedido++;
            _idPedido = s_ultimoPedido;
            _data = DateOnly.FromDateTime(DateTime.Now);
            _maxPizzas = quantidade;
            if (_maxPizzas < 1)
            {
              _maxPizzas = 1;
            }
            _pizzas = new Pizza[_maxPizzas];
            _quantPizzas = 0;
            _aberto = true;
        }

        protected Pedido(int quantidade) {
            init(quantidade);
        }

        public Pedido() {
            init(MaxPizzas);
        }

        private bool PodeAdicionar() {
            return _aberto && _quantPizzas < _maxPizzas;
        }

        /// <summary>
        /// Adiciona uma pizza ao pedido, caso ele esteja aberto.
        /// Caso contrário, ignora a operação Retorna a quantidade
        /// de pizzas no pedido ao final da execução.
        /// </summary>
        /// <param name="pizza">A pizza a ser incluída no pedido</param>
        /// <returns>A quantidade de pizzas do pedido</returns>
        public int Adicionar(Pizza pizza) {
            if (PodeAdicionar()) {
                _pizzas[_quantPizzas] = pizza;
                _quantPizzas++;
            }
            return _quantPizzas;
        }

        public void FecharPedido() {
            if(_quantPizzas > 0) {
                _aberto = false;
            }
        }

        protected double ValorItens() {
            double preco = 0d;
            for (int i = 0; i < _quantPizzas; i++) {
                preco += _pizzas[i].ValorFinal();
            }
            return preco;
        }

        public virtual double PrecoAPagar() {
            return ValorItens();
        }

        public virtual string DetalhamentoPedido() {
            StringBuilder relat = new StringBuilder($"nº{_idPedido:D2}\n{_data} - ");
            relat.AppendLine(_aberto ? "ABERTO" : "FECHADO");
            relat.AppendLine("==================================");
            for (int i = 0; i < _quantPizzas; i++) {
                relat.AppendLine($"{(i+1):D2} - {_pizzas[i].NotaDeCompra()}");
            }
            return relat.ToString();
        }

        public virtual string Relatorio() {
            StringBuilder relat = new StringBuilder($"Pedido Local {DetalhamentoPedido()}");
            relat.AppendLine($"\nValor a pagar: {PrecoAPagar():C2}");
            relat.AppendLine("==================================");
            return relat.ToString();
        }

        public int GetID() {
            return _idPedido;
        }
    }
}
