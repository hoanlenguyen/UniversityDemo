using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace UniversityDemo.Models
{
    public class FileForm
    {
        public IFormFile RawFile { get; set; }

        public string Filename { get; set; }

        public string Caption { get; set; }
    }
    public static class FileFormExtension
    {
        public static FileModel ToFileModel(this FileForm fileForm)
        {
            if (fileForm.RawFile == null)
                return null;
            var fileBytes = new byte[] { };
            string data;
            using (var ms = new MemoryStream())
            {
                fileForm.RawFile.CopyTo(ms);
                fileBytes = ms.ToArray();
                data = Convert.ToBase64String(fileBytes);
            }

            return new FileModel
            {
                Filename = fileForm.RawFile.FileName,
                Caption = fileForm.Caption,
                FileBytes = fileBytes,
                Data = data
            };
        }
    }
}