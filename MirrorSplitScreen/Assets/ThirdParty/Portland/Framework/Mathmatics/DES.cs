using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Linq;
using System.Text;

using TinyJson;

namespace Portland.Mathmatics
{
	public static class DES
	{
		public static byte[] EncryptString(string key, string obj)
		{
			return Encrypt(key, Encoding.UTF8.GetBytes(obj));
		}

		public static string DecryptString(string key, byte[] message)
		{
			return Encoding.UTF8.GetString(Decrypt(key, message));
		}

		public static byte[] Encrypt<TIn>(string key, TIn obj)
		{
			return Encrypt(key, Encoding.UTF8.GetBytes(JSONWriter.ToJson(obj)));
		}

		public static TOut Decrypt<TOut>(string key, byte[] message)
		{
			var str = Encoding.UTF8.GetString(Decrypt(key, message));
			return JSONParser.FromJson<TOut>(str);
		}

		static byte[] Encrypt(string key, byte[] input)
		{
			var DES = System.Security.Cryptography.DES.Create();

			using (var md5 = MD5.Create())
			{
				DES.Key = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(key));
			}

			DES.Mode = CipherMode.ECB;
			DES.Padding = PaddingMode.PKCS7;

			var des = DES.CreateEncryptor();
			byte[] et = des.TransformFinalBlock(input, 0, input.Length);

			DES.Clear();

			return et;
		}

		static byte[] Decrypt(string key, byte[] input)
		{
			var DES = System.Security.Cryptography.DES.Create();

			using (var md5 = MD5.Create())
			{
				DES.Key = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(key));
			}

			DES.Mode = CipherMode.ECB;
			DES.Padding = PaddingMode.PKCS7;
			var des = DES.CreateDecryptor();

			byte[] ct = des.TransformFinalBlock(input, 0, input.Length);

			DES.Clear();

			return ct;
		}
	}
}
