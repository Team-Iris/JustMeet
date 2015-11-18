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

    public class GoogleDriveService : IGoogleDriveService
    {
        private readonly UserCredential credential;
        private readonly DriveService instance;
        private string[] scopes;

        public GoogleDriveService()
        {
            this.scopes = new string[] { DriveService.Scope.Drive };
            using (var stream = new FileStream(@"GoogleDriveServiceFiles\\client_secret.json", FileMode.Open, FileAccess.Read))
            {
                this.credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                                    GoogleClientSecrets.Load(stream).Secrets,
                                    this.scopes,
                                    "user",
                                    CancellationToken.None).Result;
            }

            this.instance = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = this.credential,
                ApplicationName = Assemblies.ApplicationName,
            });
        }

        public IList<string> ListFilesIDs(int maxResults = GoogleDriveConstants.DefaultListFilesIDsCount)
        {
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
            FilesResource.DeleteRequest deleteRequest = this.instance.Files.Delete(fileID);
            var result = deleteRequest.Execute();
            return result.Length == 0;
        }

        /// <summary>
        /// Uploads File into google drive.
        /// </summary>
        /// <param name="fileBytes">byte[] of the file</param>
        /// <param name="fileNameWithExtension">File-name with extension or path ending with the filename with extension. For example: picture.jpg </param>
        /// <returns>File ID in google drive. On error returns null.</returns>
        public string UploadFile(byte[] fileBytes, string fileNameWithExtension)
        {
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
    }
}
