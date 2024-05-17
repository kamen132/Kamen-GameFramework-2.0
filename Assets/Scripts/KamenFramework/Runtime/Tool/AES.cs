using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

namespace KamenFramework.Runtime.Tool
{
    /// <summary>
    /// AES加密解密
    /// </summary>
    public class AES
    {

        private static string AESHead = "AESEncrypt";

        /// <summary>
        /// 文件加密 传入文件路径
        /// </summary>
        /// <param name="path"></param>
        /// <param name="EcrptyKey"></param>
        public static void AESFileEncrpty(string path, string EcrptyKey)
        {
            if (!File.Exists(path))
            {
                return;
            }

            try
            {
                using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    if (fs != null)
                    {
                        //第一步  读取字节头  判断是否已经加密过了
                        byte[] headBuff = new byte[10];
                        fs.Read(headBuff, 0, 10);
                        string HeadTag = Encoding.UTF8.GetString(headBuff);
                        if (HeadTag == AESHead)
                        {
#if UNITY_EDITOR
                            Debug.LogError(path + "已经加密过了！！");
#endif
                            return;
                        }

                        fs.Seek(0, SeekOrigin.Begin);
                        //第二部  加密并且写入字节头
                        byte[] buffer = new byte[fs.Length];
                        //把所有的文件 读取buffer里
                        fs.Read(buffer, 0, Convert.ToInt32(fs.Length));
                        //截止到0
                        fs.Seek(0, SeekOrigin.Begin);
                        //文件清空
                        fs.SetLength(0);
                        //头buffer
                        byte[] headBuffer = Encoding.UTF8.GetBytes(AESHead);
                        //写入0-10
                        fs.Write(headBuffer, 0, 10);
                        byte[] EncBuffer = AESEncrypt(buffer, EcrptyKey);
                        fs.Write(EncBuffer, 0, EncBuffer.Length);

                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogError("文件加密 传入文件路径出错：" + e.Source);
                throw;
            }
        }

        /// <summary>
        /// 文件解密，传入文件路径（会改动加密文件，不适合运行时）
        /// </summary>
        /// <param name="path"></param>
        /// <param name="EncrptyKey"></param>
        public static void AESFileDecrypt(string path, string EncrptyKey)
        {
            if (!File.Exists(path))
            {
                return;
            }

            try
            {
                using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    if (fs != null)
                    {
                        byte[] headBuff = new byte[10];
                        fs.Read(headBuff, 0, headBuff.Length);
                        string headTag = Encoding.UTF8.GetString(headBuff);
                        if (headTag == AESHead)
                        {
                            byte[] buffer = new byte[fs.Length - headBuff.Length];
                            fs.Read(buffer, 0, Convert.ToInt32(fs.Length - headBuff.Length));
                            fs.Seek(0, SeekOrigin.Begin);
                            fs.SetLength(0);
                            byte[] DecBuffer = AESDecrypt(buffer, EncrptyKey);
                            fs.Write(DecBuffer, 0, DecBuffer.Length);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }

        /// <summary>
        /// 文件解密， 传入文件路径
        /// </summary>
        /// <returns></returns>
        public static byte[] AESFileByteDecrpty(string path, string EncrptyKey)
        {
            if (!File.Exists(path))
            {
                return null;
            }

            byte[] DecBuffer = null;
            try
            {
                using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    if (fs != null)
                    {
                        byte[] headBuffer = new byte[10];
                        fs.Read(headBuffer, 0, headBuffer.Length);
                        string headTag = Encoding.UTF8.GetString(headBuffer);
                        if (headTag == AESHead)
                        {
                            byte[] buffer = new byte[fs.Length - headBuffer.Length];
                            fs.Read(buffer, 0, Convert.ToInt32(fs.Length - headBuffer.Length));
                            DecBuffer = AESDecrypt(buffer, EncrptyKey);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogError("文件解密出错：" + e.Source);
                throw;
            }

            return DecBuffer;
        }




        #region 加密

        #region 加密字符串

        /// <summary>
        /// AES 加密(高级加密标准，是下一代的加密算法标准，速度快，安全级别高，目前 AES 标准的一个实现是 Rijndael 算法)
        /// </summary>
        /// <param name="EncryptString">待加密密文</param>
        /// <param name="EncryptKey">加密密钥</param>
        public static string AESEncrypt(string EncryptString, string EncryptKey)
        {
            return Convert.ToBase64String(AESEncrypt(Encoding.Default.GetBytes(EncryptString), EncryptKey));
        }

        #endregion

        #region 加密字节数组

        /// <summary>
        /// AES 加密(高级加密标准，是下一代的加密算法标准，速度快，安全级别高，目前 AES 标准的一个实现是 Rijndael 算法)
        /// </summary>
        /// <param name="EncryptString">待加密密文</param>
        /// <param name="EncryptKey">加密密钥</param>
        public static byte[] AESEncrypt(byte[] EncryptByte, string EncryptKey)
        {
            if (EncryptByte.Length == 0)
            {
                throw (new Exception("明文不得为空"));
            }

            if (string.IsNullOrEmpty(EncryptKey))
            {
                throw (new Exception("密钥不得为空"));
            }

            byte[] m_strEncrypt;
            byte[] m_btIV = Convert.FromBase64String("Rkb4jvUy/ye7Cd7k89QQgQ==");
            byte[] m_salt = Convert.FromBase64String("gsf4jvkyhye5/d7k8OrLgM==");
            Rijndael m_AESProvider = Rijndael.Create();
            try
            {
                MemoryStream m_stream = new MemoryStream();
                PasswordDeriveBytes pdb = new PasswordDeriveBytes(EncryptKey, m_salt);
                ICryptoTransform transform = m_AESProvider.CreateEncryptor(pdb.GetBytes(32), m_btIV);
                CryptoStream m_csstream = new CryptoStream(m_stream, transform, CryptoStreamMode.Write);
                m_csstream.Write(EncryptByte, 0, EncryptByte.Length);
                m_csstream.FlushFinalBlock();
                m_strEncrypt = m_stream.ToArray();
                m_stream.Close();
                m_stream.Dispose();
                m_csstream.Close();
                m_csstream.Dispose();
            }
            catch (IOException ex)
            {
                throw ex;
            }
            catch (CryptographicException ex)
            {
                throw ex;
            }
            catch (ArgumentException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                m_AESProvider.Clear();
            }

            return m_strEncrypt;
        }

        #endregion

        #endregion

        #region 解密

        #region 解密字符串

        /// <summary>
        /// AES 解密(高级加密标准，是下一代的加密算法标准，速度快，安全级别高，目前 AES 标准的一个实现是 Rijndael 算法)
        /// </summary>
        /// <param name="DecryptString">待解密密文</param>
        /// <param name="DecryptKey">解密密钥</param>
        public static string AESDecrypt(string DecryptString, string DecryptKey)
        {
            return Convert.ToBase64String(AESDecrypt(Encoding.Default.GetBytes(DecryptString), DecryptKey));
        }

        #endregion

        #region 解密字节数组

        /// <summary>
        /// AES 解密(高级加密标准，是下一代的加密算法标准，速度快，安全级别高，目前 AES 标准的一个实现是 Rijndael 算法)
        /// </summary>
        /// <param name="DecryptString">待解密密文</param>
        /// <param name="DecryptKey">解密密钥</param>
        public static byte[] AESDecrypt(byte[] DecryptByte, string DecryptKey)
        {
            if (DecryptByte.Length == 0)
            {
                throw (new Exception("密文不得为空"));
            }

            if (string.IsNullOrEmpty(DecryptKey))
            {
                throw (new Exception("密钥不得为空"));
            }

            byte[] m_strDecrypt;
            byte[] m_btIV = Convert.FromBase64String("Rkb4jvUy/ye7Cd7k89QQgQ==");
            byte[] m_salt = Convert.FromBase64String("gsf4jvkyhye5/d7k8OrLgM==");
            Rijndael m_AESProvider = Rijndael.Create();
            try
            {
                MemoryStream m_stream = new MemoryStream();
                PasswordDeriveBytes pdb = new PasswordDeriveBytes(DecryptKey, m_salt);
                ICryptoTransform transform = m_AESProvider.CreateDecryptor(pdb.GetBytes(32), m_btIV);
                CryptoStream m_csstream = new CryptoStream(m_stream, transform, CryptoStreamMode.Write);
                m_csstream.Write(DecryptByte, 0, DecryptByte.Length);
                m_csstream.FlushFinalBlock();
                m_strDecrypt = m_stream.ToArray();
                m_stream.Close();
                m_stream.Dispose();
                m_csstream.Close();
                m_csstream.Dispose();
            }
            catch (IOException ex)
            {
                throw ex;
            }
            catch (CryptographicException ex)
            {
                throw ex;
            }
            catch (ArgumentException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                m_AESProvider.Clear();
            }

            return m_strDecrypt;
        }

        #endregion

        #endregion

    }
}