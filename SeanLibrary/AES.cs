using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace SeanLibrary
{
    public class AES
    {
        private string _key = "shelovely20160520";
        private string _iv = "0123456789";
        private bool _fault_tolerant = true;
        public List<string> Error;

        public AES()
        {
            Error = new List<string>();
        }
        public AES(string key, string iv)
        {
            Error = new List<string>();
            _key = key;
            _iv = iv;
        }

        /// <summary>
        /// 密钥设置
        /// </summary>
        public string Key
        {
            set
            {
                _key = value;
            }
        }
        /// <summary>
        /// 向量设置
        /// </summary>
        public string IV
        {
            set
            {
                _iv = value;
            }
        }
        /// <summary>
        /// 容错
        /// </summary>
        public bool FaultFolerant
        {
            set
            {
                _fault_tolerant = value;
            }
        }

        #region CBC模式

        /// <summary>
        /// 加密成16进制字符串
        /// </summary>
        /// <param name="toEncrypt"></param>
        /// <returns></returns>
        public string Encrypt2Hex(string toEncrypt)
        {
            try
            {
                var k = _fault_tolerant ? Common.MD5(_key).ToLower() : _key;
                var iv = _fault_tolerant ? Common.MD5(_iv).ToLower() : _iv;
                byte[] keyArray = UTF8Encoding.UTF8.GetBytes(k);
                byte[] ivArray = UTF8Encoding.UTF8.GetBytes(iv.Substring(0, 16));
                byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

                RijndaelManaged rDel = new RijndaelManaged();
                rDel.Key = keyArray;
                rDel.IV = ivArray;
                rDel.Mode = CipherMode.CBC;
                rDel.Padding = PaddingMode.PKCS7;

                ICryptoTransform cTransform = rDel.CreateEncryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

                return byte2hex(resultArray);
            }
            catch (Exception ex)
            {
                Error.Add(ex.Message);
                return toEncrypt;
            }
        }
        /// <summary>
        /// 加密字符串
        /// </summary>
        /// <param name="toEncrypt"></param>
        /// <returns></returns>
        public string Encrypt(string toEncrypt)
        {
            try
            {
                var k = _fault_tolerant ? Common.MD5(_key).ToLower() : _key;
                var iv = _fault_tolerant ? Common.MD5(_iv).ToLower() : _iv;
                byte[] keyArray = UTF8Encoding.UTF8.GetBytes(k);
                byte[] ivArray = UTF8Encoding.UTF8.GetBytes(iv.Substring(0, 16));
                byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

                RijndaelManaged rDel = new RijndaelManaged();
                rDel.Key = keyArray;
                rDel.IV = ivArray;
                rDel.Mode = CipherMode.CBC;
                rDel.Padding = PaddingMode.PKCS7;

                ICryptoTransform cTransform = rDel.CreateEncryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

                return Convert.ToBase64String(resultArray);
            }
            catch (Exception ex)
            {
                Error.Add(ex.Message);
                return toEncrypt;
            }

        }

        /// <summary>
        /// 解密16进制字符串
        /// </summary>
        /// <param name="HextoDecrypt"></param>
        /// <returns></returns>
        public string DecryptByHex(string HextoDecrypt)
        {
            try
            {
                var k = _fault_tolerant ? Common.MD5(_key).ToLower() : _key;
                var iv = _fault_tolerant ? Common.MD5(_iv).ToLower() : _iv;
                byte[] keyArray = UTF8Encoding.UTF8.GetBytes(k);
                byte[] ivArray = UTF8Encoding.UTF8.GetBytes(iv.Substring(0, 16));
                byte[] toEncryptArray = hexToBytes(HextoDecrypt);

                RijndaelManaged rDel = new RijndaelManaged();
                rDel.Key = keyArray;
                rDel.IV = ivArray;
                rDel.Mode = CipherMode.CBC;
                rDel.Padding = PaddingMode.PKCS7;

                ICryptoTransform cTransform = rDel.CreateDecryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

                return UTF8Encoding.UTF8.GetString(resultArray);
            }
            catch (Exception ex)
            {
                Error.Add(ex.Message);
                return HextoDecrypt;
            }
        }
        /// <summary>
        /// 解密字符串
        /// </summary>
        /// <param name="toDecrypt"></param>
        /// <returns></returns>
        public string Decrypt(string toDecrypt)
        {
            try
            {
                var k = _fault_tolerant ? Common.MD5(_key).ToLower() : _key;
                var iv = _fault_tolerant ? Common.MD5(_iv).ToLower() : _iv;
                byte[] keyArray = UTF8Encoding.UTF8.GetBytes(k);
                byte[] ivArray = UTF8Encoding.UTF8.GetBytes(iv.Substring(0, 16));
                byte[] toEncryptArray = Convert.FromBase64String(toDecrypt);

                RijndaelManaged rDel = new RijndaelManaged();
                rDel.Key = keyArray;
                rDel.IV = ivArray;
                rDel.Mode = CipherMode.CBC;
                rDel.Padding = PaddingMode.PKCS7;

                ICryptoTransform cTransform = rDel.CreateDecryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

                return UTF8Encoding.UTF8.GetString(resultArray);
            }
            catch (Exception ex)
            {
                Error.Add(ex.Message);
                return toDecrypt;
            }
        }

        #endregion

        #region ECB模式

        /// <summary>
        /// 加密字符串
        /// </summary>
        /// <param name="toEncrypt"></param>
        /// <returns></returns>
        public string ECB_Encrypt(string toEncrypt)
        {
            try
            {
                var k = _fault_tolerant ? Common.MD5(_key).ToLower() : _key;
                byte[] keyArray = UTF8Encoding.UTF8.GetBytes(k);
                byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

                RijndaelManaged rDel = new RijndaelManaged();
                rDel.Key = keyArray;
                rDel.Mode = CipherMode.ECB;
                rDel.Padding = PaddingMode.PKCS7;

                ICryptoTransform cTransform = rDel.CreateEncryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

                return Convert.ToBase64String(resultArray);
            }
            catch (Exception ex)
            {
                Error.Add(ex.Message);
                return toEncrypt;
            }

        }
        /// <summary>
        /// 加密成16进制字符串
        /// </summary>
        /// <param name="toEncrypt"></param>
        /// <returns></returns>
        public string ECB_Encrypt2Hex(string toEncrypt)
        {
            try
            {
                var k = _fault_tolerant ? Common.MD5(_key).ToLower() : _key;
                byte[] keyArray = UTF8Encoding.UTF8.GetBytes(k);
                byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

                RijndaelManaged rDel = new RijndaelManaged();
                rDel.Key = keyArray;
                rDel.Mode = CipherMode.ECB;
                rDel.Padding = PaddingMode.PKCS7;

                ICryptoTransform cTransform = rDel.CreateEncryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

                return byte2hex(resultArray);
            }
            catch (Exception ex)
            {
                Error.Add(ex.Message);
                return toEncrypt;
            }
        }
        /// <summary>
        /// 解密字符串
        /// </summary>
        /// <param name="toDecrypt"></param>
        /// <returns></returns>
        public string ECB_Decrypt(string toDecrypt)
        {
            try
            {
                var k = _fault_tolerant ? Common.MD5(_key).ToLower() : _key;
                byte[] keyArray = UTF8Encoding.UTF8.GetBytes(k);
                byte[] toEncryptArray = Convert.FromBase64String(toDecrypt);

                RijndaelManaged rDel = new RijndaelManaged();
                rDel.Key = keyArray;
                rDel.Mode = CipherMode.ECB;
                rDel.Padding = PaddingMode.PKCS7;

                ICryptoTransform cTransform = rDel.CreateDecryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

                return UTF8Encoding.UTF8.GetString(resultArray);
            }
            catch (Exception ex)
            {
                Error.Add(ex.Message);
                return toDecrypt;
            }
        }
        /// <summary>
        /// 解密16进制字符串
        /// </summary>
        /// <param name="HextoDecrypt"></param>
        /// <returns></returns>
        public string ECB_DecryptByHex(string HextoDecrypt)
        {
            try
            {
                var k = _fault_tolerant ? Common.MD5(_key).ToLower() : _key;
                byte[] keyArray = UTF8Encoding.UTF8.GetBytes(k);
                byte[] toEncryptArray = hexToBytes(HextoDecrypt);

                RijndaelManaged rDel = new RijndaelManaged();
                rDel.Key = keyArray;
                rDel.Mode = CipherMode.ECB;
                rDel.Padding = PaddingMode.PKCS7;

                ICryptoTransform cTransform = rDel.CreateDecryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

                return UTF8Encoding.UTF8.GetString(resultArray);
            }
            catch (Exception ex)
            {
                Error.Add(ex.Message);
                return HextoDecrypt;
            }
        }

        #endregion

        /// <summary>
        /// 16进制的字符串转换成byte数组
        /// </summary>
        /// <param name="bcdStr"></param>
        /// <returns></returns>
        private byte[] hexToBytes(String bcdStr)
        {
            bcdStr = bcdStr.Replace("   ", " ");
            byte[] buffer = new byte[bcdStr.Length / 2];
            for (int i = 0; i < bcdStr.Length; i += 2)
                buffer[i / 2] = (byte)Convert.ToByte(bcdStr.Substring(i, 2), 16);
            return buffer;
        }

        /// <summary>
        /// 把单字节数组转成16进制字符串
        /// </summary>
        /// <param name="block"></param>
        /// <returns></returns>
        private String byte2hex(byte[] block)
        {
            string returnStr = "";
            if (block != null)
            {
                for (int i = 0; i < block.Length; i++)
                {
                    returnStr += block[i].ToString("X2");
                }
            }
            return returnStr;
        }

    }

}
