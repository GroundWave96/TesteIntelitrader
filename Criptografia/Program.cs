/*
Criptografia na rede do navio

A intelitrader foi contratada para traduzir uma mensagem capturada na rede de um navio e ela está criptografada.
Não sabemos que tipo de criptografia eles usaram, a única coisa que sabemos até agora, é que, a cada 8 bits,
os dois últimos estão invertidos e a cada 4 bits, os 4 bits foram trocados com os próximos 4.

Mensagem criptografada:
10010110 11110111 01010110 00000001 00010111 00100110 01010111 00000001 
00010111 01110110 01010111 00110110 11110111 11010111 01010111 00000011

Instruções:
    De preferência, coloque o código em um arquivo único, para que seja possível executar online e coloque o
    link do site que executa. Ex: playcode.io/javascript

    Existe mais de uma maneira de resolver este problema, mas aqui está alguns conceitos que podem te ajudar:
        Tabela ASCII
        Operadores lógicos
        Números binários e hexadecimais

    Você saberá se conseguiu descriptografar a mensagem se ela for legível e em inglês. Pois, qualquer bit 
    errado, você terá uma mensagem cheia de símbolos.
*/
using System;
public class Program
{
    public static void Main()
    {
        string mensagem = "10010110 11110111 01010110 00000001 00010111 00100110 01010111 00000001 " +
                          "00010111 01110110 01010111 00110110 11110111 11010111 01010111 00000011";

        Console.WriteLine(Descriptografa(mensagem));
        Console.ReadLine();
    }

    static string InverteOsUltimosDoisBits(string byteStr)
    {
        string seisPrimeirosBits = byteStr.Substring(0, 6);
        string ultimosDoisInvertidos = byteStr[6] == '0' ? "1" : "0";
        ultimosDoisInvertidos += byteStr[7] == '0' ? "1" : "0";
        return seisPrimeirosBits + ultimosDoisInvertidos;
    }

    static string TrocaACadaQuatroBits(string byteStr)
    {
        return byteStr.Substring(4, 4) + byteStr.Substring(0, 4);
    }

    static string BinarioParaTexto(string[] binario)
    {
        string texto = "";
        foreach (string letra in binario)
        {
            char caractere = (char)Convert.ToInt32(letra, 2);
            texto += caractere;
        }
        return texto;
    }

    static string Descriptografa(string mensagem)
    {
        string[] byteStr = mensagem.Split(' ');
        for (int i = 0; i < byteStr.Length; i++)
        {
            byteStr[i] = InverteOsUltimosDoisBits(byteStr[i]);
            byteStr[i] = TrocaACadaQuatroBits(byteStr[i]);
        }
        return BinarioParaTexto(byteStr);
    }
}