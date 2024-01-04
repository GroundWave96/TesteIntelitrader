/*
Menor distância de dois arrays

João estava participando de uma competição de programação e lhe foi dado um problema em que ele tinha 
que encontrar a menor distância entre dois números de dois arrays.

Um exemplo seria:

    Array 1 -> [-1, 5]
    Array 2 -> [26, 6]

A menor distância seria a combinação do número 5 do array 1 com o número 6 do array 2, que seria 1 
de distância.

Instruções
    Use arrays com tamanho maiores ou iguais a 10 números.

    De preferência, coloque o código em um arquivo único, para que seja possível executar online e 
    coloque o link do site que executa. Ex: playcode.io/javascript
*/

using System;

public class Program
{
    public static void Main()
    {
        int opcao = Menu();

        if (opcao == 1)
        {
            CriarArrayAleatorio();
        }
        else if (opcao == 2)
        {
            DigitarNumerosManualmente();
        }

    }

    static int Menu()
    {
        int opcao = 0;
        do
        {
            Console.WriteLine("Escolha uma opcao:");
            Console.WriteLine("1. Criar 2 arrays com numeros aleatorios.");
            Console.WriteLine("2. Digitar manualmente os numeros para os arrays.");
            Console.Write("Opcao: ");
            string entrada = Console.ReadLine();

            if (!int.TryParse(entrada, out opcao) || (opcao != 1 && opcao != 2))
            {
                Console.WriteLine("\nDigite uma opcao valida.\n");
                opcao = 0;
            }
        } while (opcao == 0);

        return opcao;
    }

    static void CriarArrayAleatorio()
    {
        Console.WriteLine();
        int qntd = 0;
        do
        {
            Console.Write("Digite a quantidade de numeros para os arrays (minimo de 10): ");
            string entrada = Console.ReadLine();

            if (!int.TryParse(entrada, out qntd) || qntd < 10)
            {
                Console.WriteLine("Digite um numero valido.\n");
                qntd = 0;
            }
        } while (qntd < 10);

        int[] array1 = GerarArrayAleatorio(qntd);
        int[] array2 = GerarArrayAleatorio(qntd);

        MostrarArrays(array1, array2);
        MostrarResultado(array1, array2);
    }

    static int[] GerarArrayAleatorio(int tamanho)
    {
        Random random = new Random();
        int[] array = new int[tamanho];

        for (int i = 0; i < tamanho; i++)
        {
            array[i] = random.Next(1, 101);
        }

        return array;
    }

    static void DigitarNumerosManualmente()
    {
        Console.WriteLine("Digite os numeros para o Array 1 separados por espaco (minimo de 10 numeros):");
        int[] array1 = LerArrayDoUsuario();

        Console.WriteLine("Digite os numeros para o Array 2 separados por espaco (minimo de 10 numeros):");
        int[] array2 = LerArrayDoUsuario();

        MostrarArrays(array1, array2);
        MostrarResultado(array1, array2);
    }

    static int[] LerArrayDoUsuario()
    {
        string input = "";
        int count = 0;
        while (count < 10)
        {
            input = Console.ReadLine();
            string[] numeros = input.Split(' ');
            count = numeros.Length;
            if (count < 10)
            {
                Console.WriteLine("Digite pelo menos 10 valores para o array:");
            }
        }
        string[] valores = input.Split(' ');

        int[] array = new int[valores.Length];

        for (int i = 0; i < valores.Length; i++)
        {
            if (int.TryParse(valores[i], out int numero))
            {
                array[i] = numero;
            }
            else
            {
                Console.WriteLine($"Valor invalido: {valores[i]}. Tente novamente.");
                return LerArrayDoUsuario();
            }
        }

        return array;
    }

    static void MostrarArrays(int[] array1, int[] array2)
    {
        Console.WriteLine("\nArray 1:");
        Console.Write(array1[0]);
        for (int i = 1; i < array1.Length; i++)
        {
            Console.Write(", " + array1[i]);
        }
        Console.WriteLine("\nArray 2:");
        Console.Write(array2[0]);
        for (int i = 1; i < array2.Length; i++)
        {
            Console.Write(", " + array2[i]);
        }
    }

    static void MostrarResultado(int[] array1, int[] array2)
    {
        var resultado = EncontrarMenorDistancia(array1, array2);
        Console.WriteLine();
        Console.WriteLine($" \nA menor distancia eh entre {resultado.numArray1} do array1 e {resultado.numArray2} do array2: {resultado.menorDistancia}");
    }

    static (int numArray1, int numArray2, int menorDistancia) EncontrarMenorDistancia(int[] array1, int[] array2)
    {
        int numArray1 = 0;
        int numArray2 = 0;
        int menorDistancia = int.MaxValue;

        foreach (int num1 in array1)
        {
            foreach (int num2 in array2)
            {
                int distanciaAtual = Math.Abs(num1 - num2);

                if (distanciaAtual < menorDistancia)
                {
                    menorDistancia = distanciaAtual;
                    numArray1 = num1;
                    numArray2 = num2;
                }
            }
        }

        return (numArray1, numArray2, menorDistancia);
    }
}
