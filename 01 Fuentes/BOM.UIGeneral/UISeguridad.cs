using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Security.Cryptography;
using TSHAK.Components;
namespace BOM.UIGeneral
{
    public class UISeguridad
    {
        private readonly byte[] Key = new byte[] {
	0,
	1,
	2,
	3,
	4,
	5,
	6,
	7,
	8,
	9,
	1,
	2,
	3,
	4,
	5,
	6

};
        public string f_Encriptar(string texto)
        {
            string functionReturnValue = null;
            SecureQueryString qs = default(SecureQueryString);
            functionReturnValue = "";
            try
            {
                qs = new SecureQueryString(Key);
                qs["Texto"] = texto;
                qs.ExpireTime = System.DateTime.Now.AddYears(1);
                functionReturnValue = HttpUtility.UrlEncode(qs.ToString());

            }
            catch (Exception Exp)
            {

            }
            return functionReturnValue;
        }

        public string f_Desencriptar(string texto)
        {
            string functionReturnValue = null;
            SecureQueryString qs = default(SecureQueryString);
            functionReturnValue = "";


            try
            {
                qs = new SecureQueryString(Key, HttpUtility.UrlDecode(texto).Replace(" ", "+"));
                functionReturnValue = qs["Texto"];

            }
            catch (Exception Exp)
            {

            }
            return functionReturnValue;
        }
        //string key = "mikey";
        //public string f_Encriptar(string texto)
        //{
        //    //arreglo de bytes donde guardaremos la llave
        //    byte[] keyArray;
        //    //arreglo de bytes donde guardaremos el texto
        //    //que vamos a encriptar
        //    byte[] Arreglo_a_Cifrar =
        //    UTF8Encoding.UTF8.GetBytes(texto);

        //    //se utilizan las clases de encriptación
        //    //provistas por el Framework
        //    //Algoritmo MD5
        //    MD5CryptoServiceProvider hashmd5 =
        //    new MD5CryptoServiceProvider();
        //    //se guarda la llave para que se le realice
        //    //hashing
        //    keyArray = hashmd5.ComputeHash(
        //    UTF8Encoding.UTF8.GetBytes(key));

        //    hashmd5.Clear();

        //    //Algoritmo 3DAS
        //    TripleDESCryptoServiceProvider tdes =
        //    new TripleDESCryptoServiceProvider();

        //    tdes.Key = keyArray;
        //    tdes.Mode = CipherMode.ECB;
        //    tdes.Padding = PaddingMode.PKCS7;

        //    //se empieza con la transformación de la cadena
        //    ICryptoTransform cTransform =
        //    tdes.CreateEncryptor();

        //    //arreglo de bytes donde se guarda la
        //    //cadena cifrada
        //    byte[] ArrayResultado =
        //    cTransform.TransformFinalBlock(Arreglo_a_Cifrar,
        //    0, Arreglo_a_Cifrar.Length);

        //    tdes.Clear();

        //    //se regresa el resultado en forma de una cadena
        //    return Convert.ToBase64String(ArrayResultado,
        //    0, ArrayResultado.Length);
        //}
        //public string f_Desencriptar(string textoEncriptado)
        //{
        //    byte[] keyArray;
        //    //convierte el texto en una secuencia de bytes
        //    byte[] Array_a_Descifrar =
        //    Convert.FromBase64String(textoEncriptado);

        //    //se llama a las clases que tienen los algoritmos
        //    //de encriptación se le aplica hashing
        //    //algoritmo MD5
        //    MD5CryptoServiceProvider hashmd5 =
        //    new MD5CryptoServiceProvider();

        //    keyArray = hashmd5.ComputeHash(
        //    UTF8Encoding.UTF8.GetBytes(key));

        //    hashmd5.Clear();

        //    TripleDESCryptoServiceProvider tdes =
        //    new TripleDESCryptoServiceProvider();

        //    tdes.Key = keyArray;
        //    tdes.Mode = CipherMode.ECB;
        //    tdes.Padding = PaddingMode.PKCS7;

        //    ICryptoTransform cTransform =
        //    tdes.CreateDecryptor();

        //    byte[] resultArray =
        //    cTransform.TransformFinalBlock(Array_a_Descifrar,
        //    0, Array_a_Descifrar.Length);

        //    tdes.Clear();
        //    //se regresa en forma de cadena
        //    return UTF8Encoding.UTF8.GetString(resultArray);
        //}
    }
}