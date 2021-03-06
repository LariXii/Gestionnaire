﻿using System;  
using System.IO;  
using System.Security.Cryptography;
using System.Text;
using System.Xml;

namespace Gestionnaire.manager
{

    class AESManager
    {
        public static byte[] GenerateRandomSalt()
        {
            byte[] data = new byte[32];

            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                for (int i = 0; i < 10; i++)
                {
                    rng.GetBytes(data);
                }
            }

            return data;
        }

        private static RijndaelManaged CreateRijndaelManaged(byte[] password, byte[] salt)
        {
            //Set Rijndael symmetric encryption algorithm
            RijndaelManaged AES = new RijndaelManaged();
            AES.KeySize = 256;
            AES.BlockSize = 128;
            AES.Padding = PaddingMode.PKCS7;

            //http://stackoverflow.com/questions/2659214/why-do-i-need-to-use-the-rfc2898derivebytes-class-in-net-instead-of-directly
            //"What it does is repeatedly hash the user password along with the salt." High iteration counts.
            var key = new Rfc2898DeriveBytes(password, salt, 50000);
            AES.Key = key.GetBytes(AES.KeySize / 8);
            AES.IV = key.GetBytes(AES.BlockSize / 8);

            //Cipher modes: http://security.stackexchange.com/questions/52665/which-is-the-best-cipher-mode-and-padding-mode-for-aes-encryption
            //AES.Mode = CipherMode.CFB;

            return AES;
        }
        
        //Get an XML Document then encrypt it with AES Encryption in a file 
        public static void EncryptXmlDocumentToFile(XmlDocument xmlDoc, string filePath, string password)
        {
            //http://stackoverflow.com/questions/27645527/aes-encryption-on-large-files

            //generate random salt
            byte[] salt = GenerateRandomSalt();

            //create output file name
            FileStream fsCrypt = new FileStream(filePath, File.Exists(filePath) ? FileMode.Truncate : FileMode.Create);

            //convert password string to byte arrray
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

            RijndaelManaged AES = CreateRijndaelManaged(passwordBytes, salt);

            // write salt to the begining of the output file, so in this case can be random every time
            fsCrypt.Write(salt, 0, salt.Length);

            CryptoStream cs = new CryptoStream(fsCrypt, AES.CreateEncryptor(), CryptoStreamMode.Write);

            MemoryStream memoryStream = new MemoryStream();
            //Save the XMLDoc into a memoryStream
            xmlDoc.Save(memoryStream);
            //Flush the stream and put the position at 0
            memoryStream.Flush();
            memoryStream.Position = 0;
            
            //create a buffer (1mb) so only this amount will allocate in the memory and not the whole file
            byte[] buffer = new byte[1048576];
            int read;
            //Read the memoruStream's byte and the cryptoStream write them crypted into the fileStream
            try
            {
                while ((read = memoryStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    cs.Write(buffer, 0, read);
                }
                // Close up
                memoryStream.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                cs.Close();
                fsCrypt.Close();
            }
        }
        
        //Decrypt a file into a XML Document
        public static XmlDocument DecryptFileToXmlDocument(string filePath, string password)
        {
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] salt = new byte[32];

            //Open the crypted file 
            FileStream fsCrypt = new FileStream(filePath, FileMode.Open);
            //Read the salt's byte
            fsCrypt.Read(salt, 0, salt.Length);

            RijndaelManaged AES = CreateRijndaelManaged(passwordBytes, salt);
            
            CryptoStream cs = new CryptoStream(fsCrypt, AES.CreateDecryptor(), CryptoStreamMode.Read);

            MemoryStream memoryStream = new MemoryStream();
            memoryStream.Flush();
            memoryStream.Position = 0;
            XmlDocument xmlDocument = new XmlDocument();

            int read;
            byte[] buffer = new byte[1048576];
            //Read the cryptoStream that read the file's byte and write decrypted byte into the memoryStream 
            try
            {
                cs.Read(buffer, 0, 3);
                while ((read = cs.Read(buffer, 0, buffer.Length)) > 0)
                {
                    memoryStream.Write(buffer, 0, read);
                }
                //Save into the xmlDocument the memoryStream's byte converted to string
                xmlDocument.LoadXml(Encoding.Default.GetString(memoryStream.ToArray()));
            }
            catch (CryptographicException ex_CryptographicException)
            {
                Console.WriteLine("CryptographicException error: " + ex_CryptographicException.Message);
            }
            catch (XmlException ex)
            {
                Console.WriteLine("Error: " + ex.Message + " linenumber : "+ ex.LineNumber +" lineposition : " + ex.LinePosition);
            }

            //Close the differents streams
            try
            {
                cs.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error by closing CryptoStream: " + ex.Message);
            }
            finally
            {
                memoryStream.Close();
                fsCrypt.Close();
            }

            return xmlDocument;
        }
    }
}