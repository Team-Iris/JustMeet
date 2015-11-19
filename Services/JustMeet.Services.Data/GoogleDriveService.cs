namespace JustMeet.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading;

    using Google.Apis.Auth.OAuth2;
    using Google.Apis.Drive.v2;
    using Google.Apis.Services;
    using JustMeet.Common.Constants;
    using JustMeet.Services.Data.Contracts;
    using System.Collections.Specialized;
    using System.Net;
    using Newtonsoft.Json.Linq;
    using Google.Apis.Auth.OAuth2.Responses;
    using Google.Apis.Auth.OAuth2.Flows;

    public class GoogleDriveService : IGoogleDriveService
    {
        private const string clientId = "1082017868226-oqvt8hcalblolbf76506uvl8g2n96ar3.apps.googleusercontent.com";
        private const string clientSecret = "NDZpYXGx8lHBWjXE1FABNwU2";
        private const string refreshToken = "1/kubYzBrMfd33oW7WLOH6Cp-5n-G0iQ4qo3xMDCHYNKw";


        private readonly DriveService instance;
        private UserCredential credential;
        private string[] scopes;

        public GoogleDriveService()
        {
            this.scopes = new string[] { DriveService.Scope.Drive };

            this.credential = GetCredentialWithAccessToken(refreshToken, clientId, clientSecret, scopes);
            this.credential.Token.Issued = DateTime.Now;

            this.instance = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = this.credential,
                ApplicationName = Assemblies.ApplicationName,
            });
        }

        public IList<string> ListFilesIDs(int maxResults = GoogleDriveConstants.DefaultListFilesIDsCount)
        {
            CheckToken(ref this.credential);
            FilesResource.ListRequest listRequest = this.instance.Files.List();
            listRequest.MaxResults = maxResults;

            IList<Google.Apis.Drive.v2.Data.File> files = listRequest.Execute().Items;
            var result = new List<string>();

            if (files != null && files.Count > 0)
            {
                foreach (var file in files)
                {
                    result.Add(file.Id);
                }
            }
            else
            {
                // Console.WriteLine("No files found.");
            }

            return result;
        }

        /// <summary>
        /// Permanently deletes a file by ID. Skips the trash.
        /// </summary>
        /// <param name="fileID">file id</param>
        /// <returns>Returns true on success</returns>
        public bool DeleteFileByID(string fileID)
        {
            CheckToken(ref this.credential);
            FilesResource.DeleteRequest deleteRequest = this.instance.Files.Delete(fileID);
            var result = deleteRequest.Execute();
            return result.Length == 0;
        }

        /// <summary>
        /// Uploads File into Google Drive.
        /// </summary>
        /// <param name="fileBytes">byte[] of the file</param>
        /// <param name="fileNameWithExtension">File-name with extension or path ending with the filename with extension. For example: picture.jpg </param>
        /// <returns>File ID in Google Drive. On error returns null.</returns>
        public string UploadFile(byte[] fileBytes, string fileNameWithExtension)
        {
            CheckToken(ref this.credential);
            var body = new Google.Apis.Drive.v2.Data.File();
            body.Title = fileNameWithExtension;
            body.Description = "File uploaded by JustMeet";
            body.MimeType = GetMimeType(fileNameWithExtension);
            ////body.Parents = new List<ParentReference>() { new ParentReference() { Id = _parent } };

            string result = null;
            using (var stream = new MemoryStream(fileBytes))
            {
                try
                {
                    FilesResource.InsertMediaUpload request = this.instance.Files.Insert(body, stream, body.MimeType);
                    ////request.Convert = true;   // uncomment this line if you want files to be converted to Drive format
                    request.Upload();
                    result = request.ResponseBody.Id;
                }
                catch (Exception)
                {
                    result = null;
                }
            }

            return result;
        }

        public string GetLinkById(string fileId)
        {
            if (fileId.Length == 0)
            {
                throw new ArgumentException("Error: Empty fileId is passed!");
            }

            return GoogleDriveConstants.PremanentLinkPrefix + fileId;
        }

        public string GetIdByLink(string fileLink)
        {
            if (!fileLink.Contains(GoogleDriveConstants.PremanentLinkPrefix))
            {
                throw new ArgumentException("Error: Bad fileLink is passed!");
            }

            return fileLink.Substring(GoogleDriveConstants.PremanentLinkPrefix.Length);
        }

        private static string GetMimeType(string fileName)
        {
            string mimeType = "application/unknown";
            string ext = System.IO.Path.GetExtension(fileName).ToLower();
            Microsoft.Win32.RegistryKey regKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext);
            if (regKey != null && regKey.GetValue("Content Type") != null)
            {
                mimeType = regKey.GetValue("Content Type").ToString();
            }

            return mimeType;
        }

        private static UserCredential GetCredentialWithAccessToken(string refreshToken, string clientID, string clientSecret, string[] scopes)
        {
            TokenResponse token = new TokenResponse
            {
                RefreshToken = refreshToken
            };

            IAuthorizationCodeFlow flow = new AuthorizationCodeFlow(new AuthorizationCodeFlow.Initializer(Google.Apis.Auth.OAuth2.GoogleAuthConsts.AuthorizationUrl, Google.Apis.Auth.OAuth2.GoogleAuthConsts.TokenUrl)
            {
                ClientSecrets = new ClientSecrets
                {
                    ClientId = clientID,
                    ClientSecret = clientSecret
                },
                Scopes = scopes
            });

            UserCredential credential = new UserCredential(flow, "me", token);
            try
            {
                bool success = credential.RefreshTokenAsync(CancellationToken.None).Result;
            }
            catch
            {
                throw;
            }
            return credential;
        }

        private void CheckToken(ref UserCredential credential)
        {
            if (credential.Token.Issued.AddSeconds(3600) <= DateTime.Now.AddSeconds(-120))
            {
                credential = GetCredentialWithAccessToken(refreshToken, clientId, clientSecret, this.scopes);
                credential.Token.Issued = DateTime.Now;
            }
        }
    }
}
