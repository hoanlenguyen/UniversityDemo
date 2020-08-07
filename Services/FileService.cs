using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UniversityDemo.Models;

namespace UniversityDemo.Services
{
    public class FileService
    {
        public FileService()
        {
        }

        public async Task<FileModel> UploadAsync(FileForm fileForm)
        {
            var file = fileForm.ToFileModel();
            return file;
        }

        public async Task<List<FileModel>> UploadAsync(HttpRequest request)
        {
            return await GetFiles(request);
        }

        static async Task<List<FileModel>> GetFiles(HttpRequest request)
        {
            if (request.ContentType.Contains("application/json") || request.ContentType.Contains("text/plain"))
                using (var reader = new StreamReader(request.Body))
                {
                    return new List<FileModel>
                    {
                        JsonConvert.DeserializeObject<FileModel>(await reader.ReadToEndAsync())
                    };
                }

            if (request.HasFormContentType && request.Form?.Files != null && request.Form.Files.Count > 0)
            {
                string caption = null;

                if (request.Form.Files.Count == 1 && request.Form.TryGetValue("Caption", out var captionValue))
                    caption = captionValue.ToString();

                var result = new List<FileModel>();
                foreach (var f in request.Form.Files)
                    await using (var m = new MemoryStream())
                    {
                        await f.CopyToAsync(m);

                        var file = new FileModel
                        {
                            Caption = caption,
                            Filename = f.FileName,
                            FileType = Path.GetExtension(f.FileName).ToLower(),
                            FileBytes = m.ToArray()
                        };

                        result.Add(file);
                    }

                return result;
            }

            return new List<FileModel>();
        }
    }
}
