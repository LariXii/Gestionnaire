﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.XPath;
using Gestionnaire.manager;
using Gestionnaire.model;

namespace Gestionnaire
{
    public static class MyUtils
    {
        public const string EXTENSION = ".xml";
        public static string CreateFile(string filePath, string fileName, bool isProfilFile)
        {
            try
            {
                //Génération aléatoire d'un nom de fichier
                if (String.IsNullOrEmpty(fileName))
                {
                    fileName = Path.GetRandomFileName();
                    fileName = Path.ChangeExtension(fileName, EXTENSION);
                }
                var pathString = Path.Combine(filePath, fileName);
            
                //Vérification de l'existance du nom de fichier, regénération aléatoire tant qu'il existe.
                while (File.Exists(fileName))
                {
                    fileName = Path.GetRandomFileName();
                    fileName = Path.ChangeExtension(fileName, EXTENSION);
                    pathString = Path.Combine(filePath, fileName);
                }

                XmlWriterSettings settings = new XmlWriterSettings
                {
                    Indent = true, 
                    NewLineOnAttributes = true,
                    ConformanceLevel = ConformanceLevel.Document,
                    Encoding = Encoding.UTF8
                };

                XmlWriter xtw = XmlWriter.Create(pathString, settings);
                
                if (isProfilFile)
                {
                    xtw.WriteDocType("Profils", null, null,
                        "<!ELEMENT Profils     (Profil*)>" +
                        "<!ELEMENT Profil  (Login,IdUsb,PathFileEntries, Password)>" +
                        "<!ELEMENT PathFileEntries (#PCDATA)>" +
                        "<!ELEMENT Login       (#PCDATA)>" +
                        "<!ELEMENT Password    (#PCDATA)>" +
                        "<!ELEMENT IdUsb (#PCDATA)>");
                    xtw.WriteStartElement("Profils");
                }
                else
                {
                    xtw.WriteDocType("Database", null, null, 
                        "<!ELEMENT Database (Entries)>" +
                        "<!ELEMENT Entries  (Entry*)>" +
                        "<!ELEMENT Entry (Name, UserName, Url, Password)>" +
                        "<!ELEMENT Name    (#PCDATA)>" +
                        "<!ELEMENT UserName    (#PCDATA)>" +
                        "<!ELEMENT Url    (#PCDATA)>" +
                        "<!ELEMENT Password (#PCDATA)>");
                    xtw.WriteStartElement("Database");
                    xtw.WriteStartElement("Entries");
                    xtw.WriteEndElement();
                }
                xtw.WriteEndElement();
                xtw.Close();
            }
            
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return fileName;
        }

        public static string GetRandomFileName()
        {
            string fileName = Path.GetRandomFileName();
            fileName = Path.ChangeExtension(fileName, EXTENSION);
            
            while (File.Exists(fileName))
            {
                fileName = Path.GetRandomFileName();
                fileName = Path.ChangeExtension(fileName, EXTENSION);
            }

            return fileName;
        }
        
        public static XmlDocument CreateEntryXmlDocument()
        {
            XmlDocument xmlDoc = new XmlDocument();
            var declaration = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null);
            xmlDoc.AppendChild(declaration);
            var docType = xmlDoc.CreateDocumentType("Entries", null, null,
                "<!ELEMENT Entries  (Entry*)>" +
                "<!ELEMENT Entry (Name, UserName, Url, Password)>" +
                "<!ELEMENT Name    (#PCDATA)>" +
                "<!ELEMENT UserName    (#PCDATA)>" +
                "<!ELEMENT Url    (#PCDATA)>" +
                "<!ELEMENT Password (#PCDATA)>");
            xmlDoc.AppendChild(docType);
            xmlDoc.AppendChild(xmlDoc.CreateElement("Entries"));
            return xmlDoc;
        }
        
        public static T DeserializeFragment<T>(string xmlFragment)
        {
            // Add a root element using the type name e.g. <Profil>...</Profil>
            var xmlData = string.Format("<{0}>{1}</{0}>", typeof(T).Name, xmlFragment);
            var mySerializer = new XmlSerializer(typeof(T));
            using (var reader = new StringReader(xmlData))
            {
                return (T)mySerializer.Deserialize(reader);
            }
        }

        public static Entries ExtractEntries(XmlDocument xmlDoc)
        {
            Entries myList = new Entries();
            
            XPathNavigator nav = xmlDoc.CreateNavigator();
            var nodes = nav.Select("//Entries");
            if (nodes.MoveNext())
            {
                XPathNavigator myEntries = nodes.Current;
                myList = DeserializeFragment<Entries>(myEntries.InnerXml);
            }
            
            return myList;
        }
        public static XmlDocumentFragment ToXmlDocumentFragment(XmlDocument doc, object o)
        {
            //Initialisation d'un espace de nom vide
            XmlSerializerNamespaces xmlSerializerNamespaces = new XmlSerializerNamespaces(
                new[]
                {
                    XmlQualifiedName.Empty
                });

            //Chaine de charactère de sortie du XML créé à partir de l'objet
            StringBuilder output = new StringBuilder();

            //Initialisation d'un XmlWriter qui omet la déclaration
            XmlWriter writer = XmlWriter.Create(output,
                new XmlWriterSettings
                {
                    OmitXmlDeclaration = true
                });
            //Initialisation du sérialiseur avec le type de l'objet
            var ser = new XmlSerializer(o.GetType());
            //Sérialisation sans déclaration ni espace de nom
            ser.Serialize(writer, o, xmlSerializerNamespaces);
            //Création d'un fragment XML
            XmlDocumentFragment fragmentProfil = doc.CreateDocumentFragment();
            //Ajout du XML généré à l'aide du sérialiseur dans le fragment
            fragmentProfil.InnerXml = output.ToString();
            //Fermeture du writer
            writer.Close();
            
            return fragmentProfil;
        }

        public static void AddFragmentToXmlDocument(XmlDocument xmlDoc, string parent, object o)
        {
            //Récupération du noeud racine dans le fichier des profils
            XmlNode rootNode = xmlDoc.GetElementsByTagName(parent)[0];
            //Sérialisation de l'objet en fragment XML
            XmlDocumentFragment fragmentNode = ToXmlDocumentFragment(xmlDoc, o);
            //Ajout du fragment dans le document
            rootNode.AppendChild(fragmentNode);
        }

        public static void AddProfilToXmlDocument(XmlDocument xmlDoc, Profil p)
        {
            AddFragmentToXmlDocument(xmlDoc, "Profils", p);
        }
        
        public static void AddEntryToXmlDocument(XmlDocument xmlDoc, Entry e)
        {
            AddFragmentToXmlDocument(xmlDoc, "Entries", e);
        }

        public static void SaveXmlDocToFile(string filePath, XmlDocument xmlDoc, string password)
        {
            AESManager.EncryptXmlDocumentToFile(xmlDoc, filePath, password);
            //AESManager.EncryptAesManaged(xmlDoc, password);
        }

        public static void LoadFileToXmlDoc(string filePath, string password, out XmlDocument xmlDoc)
        {
            if (File.Exists(filePath))
            {
                xmlDoc = AESManager.DecryptFileToXmlDocument(filePath, password);
            }
            else
            {
                xmlDoc = CreateEntryXmlDocument();
            }

            Console.WriteLine(xmlDoc.InnerXml);
        }

        public static bool AddFragment(string filePath, string parent, object o)
        {
            var xmlDoc = new XmlDocument();
            try
            {
                xmlDoc.Load(filePath);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            
            //Récupération du noeud racine dans le fichier des profils
            XmlNode rootNode = xmlDoc.GetElementsByTagName(parent)[0];
            //Sérialisation de l'objet en fragment XML
            XmlDocumentFragment fragmentNode = ToXmlDocumentFragment(xmlDoc, o);
            //Ajout du fragment dans le document
            rootNode.AppendChild(fragmentNode);
            
            //Sauvegarde des modifications dans le fichier
            xmlDoc.Save(filePath);
            return true;
        }
        
        public static bool AddProfil(string filePath, Profil p)
        {
            return AddFragment(filePath, "Profils", p);
        }

        public static bool AddEntry(string filePath, Entry entry)
        {
            return AddFragment(filePath, "Entries", entry);
        }

        public static bool DeleteEntryToXmlDocument(XmlDocument xmlDoc, Entry e)
        {
            XPathNavigator nav = xmlDoc.CreateNavigator();
            var nodeToDel = nav.SelectSingleNode("//Entry[Url ='" + e.Url + "']");
            if (nodeToDel != null)
            {
                try
                {
                    nodeToDel.DeleteSelf();
                }
                catch
                {
                    return false;
                }
            }
            Console.WriteLine("Node supprimée ! "); 
            return true;
        }

        public static bool UpdateEntryToXmlDocument(XmlDocument xmlDoc, Entry e)
        {
            if (DeleteEntryToXmlDocument(xmlDoc, e))
            {
                AddEntryToXmlDocument(xmlDoc, e);
                return true;
            }

            return false;
        }

        public static char RandomizeLowerLetter()
        {
            return (char)new Random().Next('a', 'z');
        }
        
        public static char RandomizeUpperLetter()
        {
            return (char)new Random().Next('A', 'Z');
        }
        
        public static char RandomizeDigit()
        {
            return Char.Parse(new Random().Next(0, 9).ToString());
        }
        
        public static char RandomizeSpecialChar(string specialChar)
        {
            var randIndex = new Random().Next(0, specialChar.Length - 1);
            return specialChar[randIndex];
        }

        public static string GeneratePassword(int length, bool maj, bool digit, string specialChar)
        {
            bool hasSpecialChar = true;
            
            List<int> functionUse = new List<int>();
            functionUse.Add(1);
            if (maj)
                functionUse.Add(2);
            if (digit)
                functionUse.Add(3);
            if (!String.IsNullOrEmpty(specialChar))
            {
                functionUse.Add(4);
                hasSpecialChar = false;
            }
                
            string password = "";
            for (int i = 0; i < length; i++)
            {
                var randomizerIndex = new Random().Next(0, functionUse.Count);
                switch (functionUse[randomizerIndex])
                {
                    case 1:
                    {
                        password += RandomizeLowerLetter();
                    }
                        break;
                    case 2:
                    {
                        password += RandomizeUpperLetter();
                    }
                        break;
                    case 3:
                    {
                        password += RandomizeDigit();
                    }
                        break;
                    case 4:
                    {
                        password += RandomizeSpecialChar(specialChar);
                        hasSpecialChar = true;
                    }
                        break;
                }
            }
            
            //Si l'utilisateur a selectionné des caractères spéciaux et que le générateur n'en a pas mis alors on remplace
            //une lettre au hasard pas un caractère spécial. 
            if (!hasSpecialChar)
            {
                var indexLetterToChange = new Random().Next(0, password.Length - 1);
                StringBuilder str = new StringBuilder(password);
                str[indexLetterToChange] = RandomizeSpecialChar(specialChar);
                password = str.ToString();
            }

            return password;
        }
        
        public static bool ValidHttpURL(string s, out Uri resultURI)
        {
            if (!Regex.IsMatch(s, @"^https?:\/\/", RegexOptions.IgnoreCase))
                s = "http://" + s;

            if (Uri.TryCreate(s, UriKind.Absolute, out resultURI))
            {
                return resultURI.Scheme == Uri.UriSchemeHttp || resultURI.Scheme == Uri.UriSchemeHttps;
            }
            
            return false;
        }
    }
}