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
            var directory = Environment.CurrentDirectory + "\\wwwroot\\Image"; //kayıt yapacağı yol
            string path = Path.GetExtension(formFile.FileName); //uzantısını al
            string newpath = NewGuid(formFile.FileName) + path; //yani uzantı oluştur

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
        public static string Update(IFormFile formFile)
        {
            var directory = Environment.CurrentDirectory + "\\wwwroot\\Image"; //kayıt yapacağı yol
            string path = Path.GetExtension(formFile.FileName); //uzantısını al
            string newpath = NewGuid(formFile.FileName) + path; //yani uzantı oluştur

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
        public static void Delete(IFormFile formFile)
        {
            File.Delete(formFile);
        }
        public static string NewGuid(string FileName)
        {
            
            return Guid.NewGuid() + "_" + DateTime.Now.Day + "_" + DateTime.Now.Month + "_" + DateTime.Now.Year;
        }
    }
}
