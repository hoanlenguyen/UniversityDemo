using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.IO;
using System.Text.RegularExpressions;

namespace UniversityDemo.Models
{
    public class FileModel
    {
        public string Data { get; set; }

        public string Filename { get; set; }

        public string FileType { get; set; }

        public string Directory { get; set; }

        public string Caption { get; set; }

        [DefaultValue(true)]
        public bool IsPublic { get; set; }

        [JsonIgnore]
        public byte[] FileBytes { get; set; }

        public int ResourceId { get; set; }

        //public string GetBase64()
        //{
        //    if (string.IsNullOrWhiteSpace(Data))
        //        return null;
        //    var index = Data.LastIndexOf("base64", StringComparison.Ordinal);
        //    if (index == -1)
        //        return Data;
        //    return Data.Substring(index + 7);
        //}

        //public byte[] GetByteArray()
        //{
        //    try
        //    {
        //        if (FileBytes != null)
        //            return FileBytes;

        //        var base64 = GetBase64();
        //        if (string.IsNullOrWhiteSpace(base64))
        //            return null;
        //        return Convert.FromBase64String(base64);
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}

        //public string GetResourceName()
        //{
        //    var index = FileType.LastIndexOf("/", StringComparison.Ordinal);
        //    return $"{UniqueId.GenerateNewId()}.{FileType.Substring(index + 1)}";
        //}

        public bool IsValid()
        {
            if (string.IsNullOrEmpty(FileType))
            {
                //if (!string.IsNullOrEmpty(Data))
                //{
                //    var m = Regex.Match(Data, "data:(.*?);");
                //    if (!m.Success)
                //        return false;

                //    FileType = m.Groups[1].Value;
                //}
                //else
                //{
                    
                //}

                if (!string.IsNullOrEmpty(Filename))
                {
                    var ext = Path.GetExtension(Filename).ToLower();
                    //if (ext.EndsWith("jpg") || ext.EndsWith("jpeg"))
                    //    FileType = "image/jpeg";
                    //else if (ext.EndsWith("png"))
                    //    FileType = "image/png";
                    FileType = ext;
                }
            }

            if (string.IsNullOrEmpty(FileType))
                return false;

            return true;
        }

        public static byte[] ReadBytes(Stream input)
        {
            var buffer = new byte[16 * 1024];
            using (var ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                    ms.Write(buffer, 0, read);
                return ms.ToArray();
            }
        }
    }
}