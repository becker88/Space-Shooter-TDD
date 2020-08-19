// <copyright file="Utils.cs" company="Climate Conserve">
// Copyright (C) 2019 Matrix Becker. All Rights Reserved.
//
//@ Author: Mr. Saikat Patra
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>

using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using UnityEditor;


namespace SpaceShooter
{

    public static class Utils
    {

        private static readonly string logPrefix = "[SpaceShooter] ";
        private static bool isDebugEnable;
        //private static string connectionString;

        public static bool IsDebugEnable { set { isDebugEnable = value; } }

        /// <summary>
        /// Show Log
        /// </summary>
        /// <param name="format"></param>
        /// <param name="list"></param>
        public static void Log(string format, params object[] list)
        {
            if (isDebugEnable)
            {
                Debug.Log(logPrefix + string.Format(format, list));
            }
        }

        /// <summary>
        /// Show Warning
        /// </summary>
        /// <param name="format"></param>
        /// <param name="list"></param>
        public static void Warn(string format, params object[] list)
        {
            if (isDebugEnable)
            {
                Debug.LogWarning(logPrefix + string.Format(format, list));
            }
        }

        /// <summary>
        /// Show Error
        /// </summary>
        /// <param name="format"></param>
        /// <param name="list"></param>
        public static void Error(string format, params object[] list)
        {
            if (isDebugEnable)
            {
                throw new System.Exception(logPrefix + string.Format(format, list));
            }
        }


        /// <summary>
        /// Copy Directory
        /// </summary>
        /// <param name="sourceDirName"></param>
        /// <param name="destDirName"></param>
        /// <param name="copySubDirs"></param>
        public static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);
            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            // If the destination directory doesn't exist, create it.
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Application.persistentDataPath + "/Color Switch/";
                file.CopyTo(temppath, false);
            }

            // If copying subdirectories, copy them and their contents to new location.
            if (copySubDirs)
            {
                DirectoryInfo[] dirs = dir.GetDirectories();
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Application.persistentDataPath + "/Color Switch/";
                    DirectoryCopy(subdir.FullName, temppath, copySubDirs);
                }
            }
        }


        /// <summary>
        /// Create and Write File into Proper Path
        /// </summary>
        /// <param name="pathToSave"></param>
        /// <param name="fileName"></param>
        /// <param name="fileExtension"></param>
        /// <param name="dataToWrite"></param>
        public static void CreateAndSaveFile(string pathToSave, string fileName, string fileExtension, string dataToWrite)
        {

            // If the  directory doesn't exist, create it.
            if (!Directory.Exists(pathToSave))
            {
                Directory.CreateDirectory(pathToSave);

                // If the  File doesn't exist, create and Write it.
                if (!File.Exists(pathToSave + fileName + fileExtension))
                {
                    File.WriteAllText(pathToSave + fileName + fileExtension, dataToWrite);
                }
            }
            else
            {
                File.WriteAllText(pathToSave + fileName + fileExtension, dataToWrite);
            }
        }

        /// <summary>
        /// Load file from Specific Path
        /// </summary>
        /// <param name="pathFromLoad"></param>
        /// <returns></returns>
        public static string LoadFile(string pathFromLoad)
        {
            if (!Directory.Exists(pathFromLoad))
            {
                return null;
            }
            var file = Directory.GetFiles(pathFromLoad, "*.*");
            string jsonItem = "";
            foreach (var item in file)
            {

                if (Regex.IsMatch(item, @".json|.txt$"))
                {

                    jsonItem = File.ReadAllText(item);
                }
            }
            return jsonItem;
        }


        /// <summary>
        /// To Copy a File from Source to Target
        /// </summary>
        /// <param name="_fileName"></param>
        public static void RelocateFile(string _fileName)
        {
            if (!Directory.Exists(Application.persistentDataPath + "/WeatherX Settings/"))
            {
                Directory.CreateDirectory(Application.persistentDataPath + "/WeatherX Settings/");
            }
#if UNITY_EDITOR || UNITY_EDITOR_64 || UNITY_EDITOR_WIN

            string filePath = Application.persistentDataPath + "/WeatherX Settings/" + _fileName;
            if (!File.Exists(filePath))
            {
                FileUtil.CopyFileOrDirectory(Application.dataPath + "/StreamingAssets/" + _fileName, Application.persistentDataPath + "/WeatherX Settings/" + _fileName);
            }

#elif UNITY_ANDROID
            string pathFile = Application.persistentDataPath + "/WeatherX Settings/" + _fileName;

            if (!File.Exists(pathFile))
            {

                Debug.LogWarning("File \"" + pathFile + "\" does not exist. Attempting to create from \"" +
                Application.dataPath + "!/assets/" + _fileName);

                // if it doesn't ->
                // open StreamingAssets directory and load the db -> 

                WWW loadJson = new WWW("jar:file://" + Application.dataPath + "!/assets/" + _fileName);

                while (!loadJson.isDone) { }

                // then save to Application.persistentDataPath

                File.WriteAllText(pathFile, loadJson.text);

            }
#endif
        }

        /// <summary>
        /// Sending an e-Mail
        /// If not send then https://www.google.com/settings/security/lesssecureapps
        /// Check enable and try again
        /// </summary>
        /// <param name="senderMailID"></param>
        /// <param name="receiverMaidID"></param>
        /// <param name="passWord"></param>
        /// <param name="sub"></param>
        /// <param name="mailBody"></param>
        public static void SendMail(string senderMailID, string receiverMaidID, string passWord, string mailSubject, string mailBody)
        {
            MailMessage mail = new MailMessage();

            mail.From = new MailAddress(senderMailID);
            mail.To.Add(receiverMaidID);
            mail.Subject = mailSubject;
            mail.Body = mailBody;

            SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");//Gamil: smtp.gmail.com Yahoo: smtp.mail.yahoo.com
            smtpServer.Port = 587;//587 465

            smtpServer.Credentials = new System.Net.NetworkCredential(senderMailID, passWord) as ICredentialsByHost;
            smtpServer.EnableSsl = true;

            ServicePointManager.ServerCertificateValidationCallback =
                delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
                { return true; };

            smtpServer.Send(mail);
            Utils.Log("Mail send Successfully");
        }
    }
}
