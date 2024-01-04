/*
Livro de ofertas

Parabéns!! você foi contratado por uma corretora para montar um sistema de livro de ofertas de vendas e compras.
Um livro de ofertas nada mais é que uma lista de um mesmo produto organizadas pelo seu preço.

A cada negocicão de compra ou venda os livros de ofertas são atualizados, podendo inserir , remover ou modifcar 
as ofertas presentes no livro. Cada operação na livro gera uma nova mensagem que é composta por 4 paramêtros, 
o primeiro valor é a posição da atualização, já segunda é o tipo de ação a ser tomada, em terceiro temos o valor 
e por último a quantidade. Cada propriedade é descrita na tabela abaixo:

PROPRIEDADE	    TIPO	    VALORES
POSIÇÃO	        INTEIRO	    valores positivos diferente de 0
ACÃO	        INTEIRO	    0 = INSERIR, 1 = MODIFICAR, 2 = DELETAR**
VALORES	        DOUBLE	    Qualquer valor positivo diferente de 0
QUANTIDADE	    INTEIRO	    Qualquer valor positivo diferente de 0

Exemplo:
LIVRO DE OFERTA

POSICAO	VALOR	QUANTIDADE
1	    15.4	50
2	    15.4	10
3	    15.9	20
4	    16.1	100
5	    16.20	20
6	    16.43	30
7	    17.20	70
8	    17.35	80
9	    17.50	200

Seu objetivo é fazer um programa que receba e processe todas as notificações de atualizações de um livro de 
ofertas e imprima seu resultado.

Input:
A primeira linha é composta por um inteiro informando o número de notificações e as linhas subsequences contem 
as notificações no seguinte formato: posição,ação,valor,quantidade.

exemplo:
12
1,0,15.4,50
2,0,15.5,50
2,2,0,0
2,0,15.4,10
3,0,15.9,30
3,1,0,20
4,0,16.50,200
5,0,17.00,100
5,0,16.59,20
6,2,0,0
1,2,0,0
2,1,15.6,0

Output:
O output deve seguir o seguinte formato onde cada linha representa uma posição.

1,15.4,10
2,15.6,20
3,16.50,200
4,16.59,20\

representação do resultado

POSICAO	VALOR	QUANTIDADE
1	    15.4	10
2	    15.6	20
3	    16.50	200
4	    16.59	20
*/

using System;
using System.Globalization;

public class Program
{
    public static void Main()
    {
        int numNotificacoes = int.Parse(Console.ReadLine());
        Livro[] livroDeOfertas = new Livro[0];

        for (int i = 0; i < numNotificacoes; i++)
        {
            string[] notificacao = Console.ReadLine().Split(',');
            int posicao = int.Parse(notificacao[0]);
            int acao = int.Parse(notificacao[1]);
            double valor = double.Parse(notificacao[2], CultureInfo.InvariantCulture);
            int quantidade = int.Parse(notificacao[3]);

            livroDeOfertas = ProcessarNotificacao(livroDeOfertas, posicao, acao, valor, quantidade);
        }

        ImprimirLivroDeOfertas(livroDeOfertas);
    }

    static Livro[] ProcessarNotificacao(Livro[] livroDeOfertas, int posicao, int acao, double valor, int quantidade)
    {
        if (acao == 0)
        {
            return InserirOferta(livroDeOfertas, posicao, valor, quantidade);
        }
        else if (acao == 1)
        {
            return ModificarOferta(livroDeOfertas, posicao, valor, quantidade);
        }
        else if (acao == 2)
        {
            return RemoverOferta(livroDeOfertas, posicao);
        }
        else
        {
            Console.WriteLine("Ação inválida.");
            return livroDeOfertas;
        }

    }

    static Livro[] InserirOferta(Livro[] livroDeOfertas, int posicao, double valor, int quantidade)
    {
        if (posicao != 1 && posicao == livroDeOfertas[livroDeOfertas.Length - 1].Posicao)
        {
            ModificarOferta(livroDeOfertas, posicao, valor, quantidade);
            return livroDeOfertas;
        }

        Livro novaOferta = new Livro(posicao, valor, quantidade);

        Array.Resize(ref livroDeOfertas, livroDeOfertas.Length + 1);
        livroDeOfertas[livroDeOfertas.Length - 1] = novaOferta;

        return livroDeOfertas;
    }

    static Livro[] ModificarOferta(Livro[] livroDeOfertas, int posicao, double valor, int quantidade)
    {
        int index = Array.FindIndex(livroDeOfertas, oferta => oferta.Posicao == posicao);

        if (index != -1)
        {
            if (valor != 0)
                livroDeOfertas[index].Valor = valor;
            if (quantidade != 0)
                livroDeOfertas[index].Quantidade = quantidade;
        }

        return livroDeOfertas;
    }

    static Livro[] RemoverOferta(Livro[] livroDeOfertas, int posicao)
    {
        if (posicao != 1 && posicao > livroDeOfertas[livroDeOfertas.Length - 1].Posicao) //ignora se for uma posiçao mais alta do q tem
        {
            return livroDeOfertas;
        }

        Livro[] novoVetor = new Livro[livroDeOfertas.Length - 1];

        if (posicao == 1) //se for a posiçao 1 ele adiciona todos depois
        {
            for (int i = 1; i <= livroDeOfertas.Length - 1; i++)
            {
                Livro novaOferta = new Livro(i, livroDeOfertas[i].Valor, livroDeOfertas[i].Quantidade);
                novoVetor[i - 1] = novaOferta;
            }
        }
        else //se for qualquer outra posiçao ele copia todos antes do q for deletado e depois copia todos apos o deletado
        {
            for (int i = 0; i <= posicao - 2; i++)
            {
                Livro novaOferta = new Livro(i, livroDeOfertas[i].Valor, livroDeOfertas[i].Quantidade);
                novoVetor[i] = novaOferta;
            }
            for (int i = posicao; i < posicao; i++)
            {
                Livro novaOferta = new Livro(i, livroDeOfertas[i].Valor, livroDeOfertas[i].Quantidade);
                novoVetor[i] = novaOferta;
            }
        }

        livroDeOfertas = novoVetor;

        return livroDeOfertas;
    }


    static void ImprimirLivroDeOfertas(Livro[] livroDeOfertas)
    {
        Console.WriteLine();

        for (int i = 0;i <= livroDeOfertas.Length-1;i++)
        {
            if (i < livroDeOfertas.Length-1)
            {
                Console.WriteLine($"{livroDeOfertas[i].Posicao},{livroDeOfertas[i].Valor.ToString("F2", CultureInfo.InvariantCulture)},{livroDeOfertas[i].Quantidade}");
            }
            else
            {
                Console.Write($"{livroDeOfertas[i].Posicao},{livroDeOfertas[i].Valor.ToString("F2", CultureInfo.InvariantCulture)},{livroDeOfertas[i].Quantidade}\\\n");
            }
        }

        
    }
}

class Livro
{
    public int Posicao { get; set; }
    public double Valor { get; set; }
    public int Quantidade { get; set; }

    public Livro(int posicao, double valor, int quantidade)
    {
        Posicao = posicao;
        Valor = valor;
        Quantidade = quantidade;
    }
}
