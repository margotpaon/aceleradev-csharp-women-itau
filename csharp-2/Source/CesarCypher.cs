using System;
using System.Text.RegularExpressions; //permite usar Regex

namespace Codenation.Challenge
{
    public class CesarCypher : ICrypt, IDecrypt
    {
        public string Crypt(string message)
        {
            //Se a mensagem for em branco retorne uma exception
            if (message == null)
            {
                throw new ArgumentNullException();
            }
            else
            {
                //Caso a mensagem for em branco retorne texto em branco
                if(message == string.Empty)
                {
                    return string.Empty;   
                }
                //Se não criptografe a mensagem
                else
                {
                    return Encipher(message, 3);
                }
            }
        }

        public string Decrypt(string cryptedMessage)
        {   
            //se o texto estive em branco retorne uma exception
            if (cryptedMessage == string.Empty)
            {
                throw new ArgumentNullException();
            }
            else
            {
                //Se o texto for em branco retorne texto em branco
                if(cryptedMessage == string.Empty)
                {
                    return string.Empty;
                }
                //Se não retorne a mensagem descriptografada
                else
                {
                    return Decipher(cryptedMessage, 3);
                }
            }
        }

        private static char Cipher(char ch, int key)
        {
            //Se o caracter não for uma letra retorne o caracter
            if (!char.IsLetter(ch))
                return ch;

            //Se a letra for maiuscula transforme em minuscula
            char offset = char.IsUpper(ch) ? 'A' : 'a';


            return (char)((((ch + key) - offset) % 26) + offset);
        }

        public static string Encipher(string input, int key)
        {
            //Cria uma string vazia
            string output = string.Empty;

            //retira caracteres especiais
            Regex regex = new Regex("^[a-zA-Z0-9 ]*$");

            // Para cada char dentro da string input chame o método Cipher e coloque novos char na string output
            foreach (char ch in input)
                output += Cipher(ch, key);

            if (regex.IsMatch(input))
            {
                //Deixa todas as letras minusculas na string output
                return output.ToLower();
            }
            else
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        public static string Decipher(string input, int key)
        {
            Regex regex = new Regex("^[a-zA-Z0-9 ]*$");
            if(regex.IsMatch(input))
            {
                return Encipher(input, 26 - key).ToLower();
            }
            else
            {
                throw new ArgumentOutOfRangeException();
            }
        }
    }
}
