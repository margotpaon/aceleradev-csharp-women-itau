using System;
using System.Collections.Generic;

namespace Codenation.Challenge
{
    public class Math
    {
        //criando uma lista de inteiro privada para ser usada somente na classe
        private List<int> fibonacci = new List<int>();

        public List<int> Fibonacci()
        {
            //Limpa a lista
            fibonacci.Clear();
            //Adicionando os dois primeiro números fibonacci na lista
            fibonacci.Add(0);
            fibonacci.Add(1);

            //Loop para criar a sequência de fibonacci desejada
            for (int i = 0; i <=350 ; i++)
            {

                //Aplicando a formula de Fibonnaci pegando o ultimo e penultimo itens da lista 
                int sequenciaFibo = fibonacci[fibonacci.Count - 2] + fibonacci[fibonacci.Count - 1];

                //delimitando a sequência
                if(sequenciaFibo <= 350)
                {
                    fibonacci.Add(sequenciaFibo);
                }
                
            }
            //retornando a lista
            return fibonacci;
        }

        public bool IsFibonacci(int numberToTest)
        {
            //Chamando o método que cria a sequência
            Fibonacci();
            //vericando se o número pertence a Fibonacci
            return fibonacci.Contains(numberToTest);
        }
    }
}
