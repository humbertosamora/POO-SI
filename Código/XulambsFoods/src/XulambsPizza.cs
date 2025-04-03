using System.Text;

namespace XulambsFoods_2025_1.src {
    internal class XulambsPizza {
        #region Atributos de Classe
        const int MaxPedidos = 100;
        static Pedido[] _pedidos = new Pedido[MaxPedidos];
        static int _quantPedidos = 0;
        #endregion

        static void Cabecalho() {
            Console.Clear();
            Console.WriteLine("XULAMBS PIZZA v0.3\n==================================");
        }

        static void Pausa() {
            Console.WriteLine("Digite enter para continuar...");
            Console.ReadLine();
        }

        static int ExibirMenuPrincipal() {
            Cabecalho();
            Console.WriteLine("1 - Abrir Pedido");
            Console.WriteLine("2 - Alterar Pedido");
            Console.WriteLine("3 - Relatório do Pedido");
            Console.WriteLine("4 - Fechar Pedido");
            Console.WriteLine("0 - Finalizar");
            Console.Write("Digite sua escolha: ");
            return int.Parse(Console.ReadLine());
        }

        static Pedido AbrirPedido() {
            Pedido novo = EscolherTipoPedido();
            AdicionarPizza(novo);
            return novo;
        }

        static Pedido EscolherTipoPedido()
        {
            int opcao = ExibirMenuTipoPedido();
            return opcao switch {
                2 => CriarPedidoEntrega(),
                1 or _ => CriarPedidoLocal()
            };
        }

        static int ExibirMenuTipoPedido()
        {
            Cabecalho();
            Console.WriteLine("Escolha o tipo de pedido:");
            Console.WriteLine("1 - Local (padrão)");
            Console.WriteLine("2 - Pedido para entrega");
            Console.Write("Sua opção: ");
            return int.Parse(Console.ReadLine());
        }

        static Pedido CriarPedidoLocal() {
            return new Pedido();
        }

        static Pedido CriarPedidoEntrega() {
            Console.WriteLine("Pedido para Entregal");
            Console.Write("Distância: ");
            double distancia = double.Parse(Console.ReadLine());
            return new PedidoEntrega(distancia);
        }

        static void AdicionarPizza(Pedido pedido) {
            string conf;
            do {
                Pizza novaPizza = ComprarPizza();
                pedido.Adicionar(novaPizza);
                Console.Write("\nQuer uma nova pizza (S/N)? ");
                conf = Console.ReadLine().ToUpper();
            } while (conf.Equals("S"));
        }

        static int ExibirMenuIngredientes(Pizza pizza) {
            Cabecalho();
            Console.WriteLine("Personalizar a Pizza\n");
            MostrarNota(pizza);
            Console.WriteLine("\n1 - Acrescentar ingredientes");
            Console.WriteLine("2 - Retirar ingredientes");
            Console.WriteLine("0 - Não quero alterar");
            Console.Write("Digite sua escolha: ");
            return int.Parse(Console.ReadLine());
        }

        static Pizza ComprarPizza() {
            Cabecalho();
            Console.WriteLine("Comprando uma nova pizza:");
            Pizza novaPizza = new Pizza();
            EscolherIngredientes(novaPizza);
            Console.WriteLine();
            MostrarNota(novaPizza);
            return novaPizza;
        }

        static void EscolherIngredientes(Pizza pizza) {
            int opcao = ExibirMenuIngredientes(pizza);
            while(opcao!=0){
                Console.Write("Quantos ingredientes? ");
                int adicionais = int.Parse(Console.ReadLine());
                switch(opcao) {
                    case 1: pizza.AdicionarIngredientes(adicionais);
                        break;
                    case 2: pizza.RetirarIngredientes(adicionais);
                        break;
                };
                Console.WriteLine();
                MostrarNota(pizza);
                Pausa();
                opcao = ExibirMenuIngredientes(pizza);
            } 
            
        }

        static void MostrarNota(Pizza pizza) {
            Console.WriteLine("Comprando: ");
            Console.WriteLine(pizza);

        }

        static void MostrarPedido(Pedido pedido) {
            if (pedido != null) {
                Cabecalho();
                Console.WriteLine(pedido);
            }
        }


        static void ArmazenarPedido(Pedido novo)
        {
            if(_quantPedidos < MaxPedidos) {
                _pedidos[_quantPedidos] = novo;
                _quantPedidos++;
            }
        }

        static Pedido AlterarPedido() {
            Pedido localizado = LocalizarPedido();
            if (localizado == null) {
                Console.WriteLine("Pedido não localizado.");
            }
            else {
                AdicionarPizza(localizado);
            }
            return localizado;
        }

        static Pedido LocalizarPedido() {
            Cabecalho();
            Console.WriteLine("Localizando o pedido");
            Console.Write("Digite o número do pedido: ");
            int idPedido = int.Parse(Console.ReadLine());
            Pedido buscado = null;

            for(int i = 0; i < _quantPedidos && buscado == null; i++) {
              if (_pedidos[i].GetHashCode() == idPedido) {
                buscado = _pedidos[i];
              }
            }
            return buscado;
        }
        
        static void RelatorioDoPedido() {
            Pedido buscado = LocalizarPedido();

            if (buscado == null) {
                Console.WriteLine("Pedido não localizado.");
            }
            else
            {
              MostrarPedido(buscado);
            }
        }

        static void FecharPedido() {
            Pedido buscado = LocalizarPedido();

            if (buscado == null) {
                Console.WriteLine("Pedido não localizado.");
            }
            else {
              buscado.FecharPedido();
              MostrarPedido(buscado);
            }
        }

        static void Main(string[] args) {
            int opcao = -1;
            do {
                opcao = ExibirMenuPrincipal();
                switch (opcao) {
                    case 1:
                        Pedido novo = AbrirPedido();
                        ArmazenarPedido(novo);
                        MostrarPedido(novo);
                        break;
                    case 2:
                        Pedido alterado = AlterarPedido();
                        MostrarPedido(alterado);
                        break;
                    case 3:
                        RelatorioDoPedido();
                        break;
                    case 4:
                        FecharPedido();
                        break;
                    case 0: Console.WriteLine("FLW VLW OBG VLT SMP.");
                        break;
                }
                Console.ReadKey();
            } while (opcao != 0);
        }
    }
}
