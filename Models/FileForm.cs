using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel;
using System.IO;

namespace UniversityDemo.Models
{
    public class FileForm
    {
        public IFormFile RawFile { get; set; }

        public string Filename { get; set; }

        public string Caption { get; set; }

        [DefaultValue(true)]
        public bool IsPublic { get; set; }
    }
    public static class FileFormExtension
    {
        public static FileModel ToFileModel(this FileForm fileForm)
        {
            if (fileForm.RawFile == null)
                return null;

            var fileModel = new FileModel();
            fileModel.Filename = fileForm.Filename ?? fileForm.RawFile.FileName;
            fileModel.Caption = fileForm.Caption;
            fileModel.IsPublic = fileForm.IsPublic;
            fileModel.FileType = Path.GetExtension(fileForm.RawFile.FileName).ToLower();
            using (var stream = new MemoryStream())
            {
                fileForm.RawFile.CopyTo(stream);
                fileModel.FileBytes = stream.ToArray();
                var data = Convert.ToBase64String(fileModel.FileBytes);
                var index = data.LastIndexOf("base64", StringComparison.Ordinal);
                fileModel.Data = index == -1 ? data : data.Substring(index + 7);
            }

            return fileModel;
        }

    }

   

    //public static class FormFileExtension
    //{
    //    public static FileModel ToFileModel(this IFormFile formFile)
    //    {
    //        if (formFile == null)
    //            return null;

    //        var fileModel = new FileModel();
    //        fileModel.Filename = formFile.FileName;
    //        fileModel.Caption = fileForm.Caption;
    //        fileModel.FileType = Path.GetExtension(fileForm.RawFile.FileName).ToLower();
    //        using (var stream = new MemoryStream())
    //        {
    //            fileForm.RawFile.CopyTo(stream);
    //            fileModel.FileBytes = stream.ToArray();
    //            fileModel.Data = Convert.ToBase64String(fileModel.FileBytes);
    //        }

    //        return fileModel;
    //    }
    //}
}