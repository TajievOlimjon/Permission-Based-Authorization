namespace Infrastructure
{
    public interface IFileService
    {
        Task<string> AddFileAsync(string folderName, IFormFile file);
        Task<string> UpdateFileAsync(string folderName, IFormFile newFile,string lastFileName);
        Task<bool> DeleteFileAsync(string folderName,string fileName);
    }
}

