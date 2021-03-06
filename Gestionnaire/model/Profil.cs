﻿using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.XPath;
using Gestionnaire.manager;

namespace Gestionnaire.model
{
    public class Profil
    {
        private string _login;
        private string _password;
        private string _idUsb;
        private string _fileNameEntries;

        private const string FolderName = @"../../../Data";
        private const string FileName = "profilDataBase.xml";
        private static readonly string Path = System.IO.Path.Combine(FolderName, FileName);
        
        public string Login
        {
            get => _login;
            set
            {
                if (IsValidLogin(value))
                {
                    _login = value;
                }
                else
                {
                    throw new FormatException("Login mal formé");
                }
            }
        }
        
        public string IdUsb
        {
            get => _idUsb;
            set
            {
                _idUsb = value;
            }
        }
        
        public string PathFileEntries
        {
            get => _fileNameEntries;
            set => _fileNameEntries = value ?? throw new ArgumentNullException(nameof(value));
        }

        public string Password
        {
            get => _password;
            set
            {
                if (PasswordManager.IsPasswordValide(value))
                {
                    _password = PasswordManager.EncryptMd5(value);
                }
                else
                {
                    throw new FormatException("Mot de passe mal formé");
                }
            }
        }

        public Profil() {}

        public Profil(string login, string password,string idUSB)
        {
            Login = login;
            Password = password;
            IdUsb = idUSB;
        }

        public void WriteToFile()
        {
            //Vérification de l'existance du fichier des profils
            if (!File.Exists(Path))
            {
                //Création du fichier des profils
                MyUtils.CreateFile(FolderName, FileName, true);
            }
            
            //Génération d'un nom de fichier pour stocker les entrées du profil
            _fileNameEntries = MyUtils.GetRandomFileName();

            //Ajout du profil dans la base de donnée
            MyUtils.AddProfil(Path, this);
        }

        public static bool IsLoginExist(string login)
        {
            if (!File.Exists(Path))
                return false;
            
            using (XmlTextReader xtr = new XmlTextReader(Path))
            {
                while (xtr.Read())
                {
                    if (xtr.NodeType == XmlNodeType.Text)
                    {
                        if (login.Equals(xtr.Value))
                            return true;
                    }
                }
            }

            return false;
        }

        public static Profil Connection(string login, string password)
        {
            if (!File.Exists(Path))
                return null;

            var usbDevices = UsbDevice.GetUSBDevices();

            XPathDocument doc = new XPathDocument(Path);
            XPathNavigator nav = doc.CreateNavigator();
            //Vérification du login
            var nodes = nav.Select("//Profil[Login = '"+login+"' and Password ='"+password+"']");
            
            if (nodes.MoveNext())//Si le login
            {

                //On reprend la navigation à partir du noeud profil correct
                XPathNavigator navGoodLogin = nodes.Current;
                //La clé est initialement non insérée
                bool cleInsert = false;
                
                foreach (var entry in usbDevices)//On parcourt les clé USB branchées à l'ordinateur
                {
                    //On vérifie que la clé USB de connexion est bien branchée à l'ordinateur
                    var nodeUsb = navGoodLogin.Select("./IdUsb[text() = '" + entry.DeviceID + "']");
                    if (nodeUsb.MoveNext())//La clé USB est bien branchée
                    {
                        //On désérialise le XML du profil en objet Profil
                        Profil p = MyUtils.DeserializeFragment<Profil>(navGoodLogin.InnerXml);
                        return p;
                    }
                }

                if (!cleInsert)
                {
                    MessageBox.Show("Clé USB non branchée !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
            }
            MessageBox.Show("Erreur, nom d'utilisateur incorrect !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return null;
        }
        
        public static bool IsValidLoginRegex(string login)
        {
            Match isValidRegex = Regex.Match(login, @"\b[A-Za-z]\w{2,19}$");
            return isValidRegex.Success;
        }
        
        public static bool IsValidLogin(string login)
        {
            return IsValidLoginRegex(login);
        }

        public override string ToString()
        {
            return Login + ";" + FileName + ";" + PathFileEntries;
        }
    }
}