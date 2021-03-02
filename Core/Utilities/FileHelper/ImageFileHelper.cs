using Core.Utilities.Results;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Core.Utilities.FileHelper
{
    public class ImageFileHelper
    {
        private static string _currentDirectory = Environment.CurrentDirectory + @"\wwwroot";
        private static string _folder = @"\Image\";
        public static string Add(IFormFile formFile)
        {
            string path = Path.GetExtension(formFile.FileName); //uzantısını al
            string newpath = _currentDirectory + _folder + NewGuid(formFile.FileName) + path; //yani uzantı oluştur

            using (FileStream fileStream = File.Create(newpath))
            {
                formFile.CopyTo(fileStream);
                fileStream.Flush();
            }
            return newpath;
        }
        public static string Update(string uimagePath,IFormFile formFile)
        {
            var result = (uimagePath.Replace(@"\", "\\"));
            if (File.Exists(result)==true)
            {
                File.Delete(result);
                string path = Path.GetExtension(formFile.FileName); //uzantısını al
                string newpath = _currentDirectory + _folder + NewGuid(formFile.FileName) + path; //yani uzantı oluştur

                using (FileStream fileStream = File.Create(newpath))
                {
                    formFile.CopyTo(fileStream);
                    fileStream.Flush();
                }
                uimagePath = newpath; 
            }
            return uimagePath;

        }
        public static void Delete(string imagePath)
        {
            var result = (imagePath.Replace(@"\","\\"));
            if (File.Exists(result)==true)
            {
                File.Delete(result);
            }
        }
        private static string NewGuid(string FileName)
        {
            
            return Guid.NewGuid() + "-" + DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year;
        }

    }
}
