using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;


namespace ConsultaJa.Backend
{
    public class PasswordHasher
    {
        private const int SaltSize = 8;
        private const int HashSize = 10;
        public static string Hash(string password, int iteracoes)
        {
            /* cria o salt, cria um array the bytes random*/
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[SaltSize]);
            /* cria a hash, Rfc2898DeriveBytes recebe pw, salt, um nº de iteracoes, e gera chaves chamando o metodo GetBytes()*/
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iteracoes);
            var hash = pbkdf2.GetBytes(HashSize);
            /* combina salt e hash no hashBytes*/
            var hashBytes = new byte[SaltSize + HashSize];
            Array.Copy(salt, 0, hashBytes, 0, SaltSize);
            Array.Copy(hash, 0, hashBytes, SaltSize, HashSize);
            /* converte para base64/string - serve para guardar*/
            var base64Hash = Convert.ToBase64String(hashBytes);
            /* formata hash com informacao extra */
            return string.Format("$CJ${0}${1}", iteracoes, base64Hash);
        }
        public static string Hash(string password)
        {
            return Hash(password, 10000);
        }

        /* checka se hash é suportada*/
        public static bool HashSuportada(string hashString)
        {
            return hashString.Contains("$CJ$");
        }
        

        /* Verifica password em relacao a hash */
        public static bool VerificaHash(string password, string hashedPassword)
        {
            //vê se hash começa por $CJ$
            if (!HashSuportada(hashedPassword))
            {
                Console.WriteLine("Hashtype nao suportada");
                throw new NotSupportedException("Hashtype nao suportada");
            }
            //Extrai num de iteracoes e hash
            var splittedHashString = hashedPassword.Replace("$CJ$", "").Split('$');
            var iterations = int.Parse(splittedHashString[0]);
            var base64Hash = splittedHashString[1];
            //passa de string para bytes outra vez
            var hashBytes = Convert.FromBase64String(base64Hash);
            //extrai parte do salt
            var salt = new byte[SaltSize];
            Array.Copy(hashBytes, 0, salt, 0, SaltSize);
            //create hash with given salt
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations);
            byte[] hash = pbkdf2.GetBytes(HashSize);
            //resultado
            for (var i = 0; i < HashSize; i++)
            {
                if (hashBytes[i + SaltSize] != hash[i])
                {
                    return false;
                }
            }
            return true;
        }


    }
}
