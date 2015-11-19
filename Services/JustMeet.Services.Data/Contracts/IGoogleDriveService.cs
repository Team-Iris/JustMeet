namespace JustMeet.Services.Data.Contracts
{
    using System.Collections.Generic;

    public interface IGoogleDriveService
    {
        bool DeleteFileByID(string fileID);

        string GetIdByLink(string fileLink);

        string GetLinkById(string fileId);

        IList<string> ListFilesIDs(int maxResults = 1000);

        string UploadFile(byte[] fileBytes, string fileNameWithExtension);
    }
}