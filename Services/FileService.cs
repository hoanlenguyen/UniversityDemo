using System;
using System.Collections.Generic;
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
            var valid = file.IsValid();
            return file;
        }

    }
}
