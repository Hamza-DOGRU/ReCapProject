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
            var directory = Environment.CurrentDirectory + "\\wwwroot\\Image";
            string path = Path.GetExtension(formFile.FileName);
            string newpath = NewGuid(formFile.FileName) + path;

            if (!Directory.Exists(newpath))
            {
                Directory.CreateDirectory(newpath);
            }
            using (FileStream fileStream = File.Create(directory + "\\" + newpath))
            {
                formFile.CopyTo(fileStream);
                fileStream.Flush();
            }
            return newpath;
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
        public static string NewGuid(string FileName)
        {
            
            return Guid.NewGuid() + "_" + DateTime.Now.Day + "_" + DateTime.Now.Month + "_" + DateTime.Now.Year;
        }
    }
}
