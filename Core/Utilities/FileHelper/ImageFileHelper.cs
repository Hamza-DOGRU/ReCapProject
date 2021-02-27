using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Core.Utilities.FileHelper
{
    public class ImageFileHelper
    {
        public static string Add(IFormFile formFile)
        {
            string extension = Path.GetExtension(formFile.FileName).ToUpper();
            string newguid = NewGuid() + extension;

            var path = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName+@"\Images");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string Path2;
            using (FileStream fileStream=File.Create(path+"\\"+newguid))
            {
                formFile.CopyTo(fileStream);
                Path2 = path + "\\" + newguid;
                fileStream.Flush();
            }
            return Path2;
        }
        public static void Update(IFormFile formFile,string path)
        {
            string extension = Path.GetExtension(formFile.FileName);
            using (FileStream fileStream=File.Open(path,FileMode.Open))
            {
                formFile.CopyTo(fileStream);
                fileStream.Flush();
            }

        }
        public static void Delete(string path)
        {
            File.Delete(path);
        }
        public static string NewGuid()
        {
            return Guid.NewGuid().ToString() + "." + DateTime.Now.Day + "." + DateTime.Now.Month + "." + DateTime.Now.Year;
        }
    }
}
