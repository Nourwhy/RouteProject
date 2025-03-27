using Microsoft.Identity.Client;

namespace RouteProject.PL.Helper
{
    public  static class DocumentSettings
    {
        //1.Upload
    


        public static string UploadFile(IFormFile file, string folderName)
        {
            string uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", folderName);

           
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            string fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            string filePath = Path.Combine(uploadPath, fileName);

            using var fileStream = new FileStream(filePath, FileMode.Create);
            file.CopyTo(fileStream);

            return fileName;
        }

        //2.Delete

        public static void DeleteFile(string fileName, string folderName)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files", folderName, fileName);

            if (File.Exists(filePath))
                File.Delete(filePath);
        }

    }
}

