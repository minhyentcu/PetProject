using BaseSource.Shared.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BaseSource.Shared.Helpers
{
    public class UploadHelper
    {
        private static string GetUploadDirectoryGen(EUploadDirectoryType type)
        {
            var directoryGen = string.Empty;

            switch (type)
            {
                case EUploadDirectoryType.Project:
                    directoryGen = "image/project";
                    break;
                default:
                    return string.Empty;
            }
            return directoryGen;
        }

        public static bool IsValidFileContent(IFormFile postedFile)
        {
            //-------------------------------------------
            //  Attempt to read the file and check the first bytes
            //-------------------------------------------
            try
            {
                if (!postedFile.OpenReadStream().CanRead)
                {
                    return false;
                }

                if (postedFile.Length <= 0)
                {
                    return false;
                }

                byte[] buffer = new byte[512];
                postedFile.OpenReadStream().Read(buffer, 0, 512);
                string content = System.Text.Encoding.UTF8.GetString(buffer);
                if (Regex.IsMatch(content, @"<?php|<script|<html|<head|<title|<body|<pre|<table|<a\s+href|<img|<plaintext|<cross\-domain\-policy",
                    RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Multiline))
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        } 
        
        public static bool IsValidImage(IFormFile postedFile)
        {
            //-------------------------------------------
            //  Check the mime types
            //-------------------------------------------
            if (postedFile.ContentType.ToLower() != "image/jpg" &&
                postedFile.ContentType.ToLower() != "image/jpeg" &&
                postedFile.ContentType.ToLower() != "image/png" &&
                postedFile.ContentType.ToLower() != "image/gif" &&
                postedFile.ContentType.ToLower() != "image/webp")
            {
                return false;
            }

            //-------------------------------------------
            //  Check the extension
            //-------------------------------------------
            if (Path.GetExtension(postedFile.FileName).ToLower() != ".jpg"
                && Path.GetExtension(postedFile.FileName).ToLower() != ".png"
                && Path.GetExtension(postedFile.FileName).ToLower() != ".jpeg"
                && Path.GetExtension(postedFile.FileName).ToLower() != ".gif"
                && Path.GetExtension(postedFile.FileName).ToLower() != ".webp")
            {
                return false;
            }

            return true;
        }

        public static bool IsValidVideo(IFormFile postedFile)
        {
            //-------------------------------------------
            //  Check the mime types
            //-------------------------------------------
            if (postedFile.ContentType.ToLower() != "video/mp4" &&
                postedFile.ContentType.ToLower() != "video/ogg")
            {
                return false;
            }

            //-------------------------------------------
            //  Check the extension
            //-------------------------------------------
            if (Path.GetExtension(postedFile.FileName).ToLower() != ".mp4"
                && Path.GetExtension(postedFile.FileName).ToLower() != ".ogg")
            {
                return false;
            }

            return true;
        }

        public static bool IsValidDoc(IFormFile postedFile)
        {
            //-------------------------------------------
            //  Check the mime types
            //-------------------------------------------
            if (postedFile.ContentType.ToLower() != "application/pdf" &&
                postedFile.ContentType.ToLower() != "application/vnd.openxmlformats-officedocument.wordprocessingml.document" &&
                postedFile.ContentType.ToLower() != "application/msword" &&
                postedFile.ContentType.ToLower() != "application/octet-stream")
            {
                return false;
            }

            //-------------------------------------------
            //  Check the extension
            //-------------------------------------------
            if (Path.GetExtension(postedFile.FileName).ToLower() != ".pdf"
                && Path.GetExtension(postedFile.FileName).ToLower() != ".rar"
                && Path.GetExtension(postedFile.FileName).ToLower() != ".zip"
                && Path.GetExtension(postedFile.FileName).ToLower() != ".doc"
                && Path.GetExtension(postedFile.FileName).ToLower() != ".docx")
            {
                return false;
            }

            return true;
        }

        public static bool IsValidSize(IFormFile postedFile, int size)
        {
            if (postedFile.Length <= 0 || postedFile.Length > (1024 * 1024 * size))
            {
                return false;
            }
            return true;
        }

        public static async Task<KeyValuePair<bool, string>> Upload(IFormFile sourceFile, EUploadFileType fileType, EUploadDirectoryType dirType, string rootPath)
        {
            if (!IsValidFileContent(sourceFile))
            {
                return new KeyValuePair<bool, string>(false, $"{sourceFile.FileName}: File không hợp lệ");
            }

            switch (fileType)
            {
                case EUploadFileType.All:
                    //if (!IsValidImage(sourceFile) && 
                    //    !IsValidVideo(sourceFile) && 
                    //    !IsValidDoc(sourceFile))
                    //{
                    //    return new KeyValuePair<bool, string>(false, $"{sourceFile.FileName}: Loại file không hợp lệ");
                    //}
                    break;
                case EUploadFileType.Image:
                    if (!IsValidImage(sourceFile))
                    {
                        return new KeyValuePair<bool, string>(false, $"{sourceFile.FileName}: Loại file không hợp lệ");
                    }
                    break;
                case EUploadFileType.Video:
                    if (!IsValidVideo(sourceFile))
                    {
                        return new KeyValuePair<bool, string>(false, $"{sourceFile.FileName}: Loại file không hợp lệ");
                    }
                    break;
                case EUploadFileType.Doc:
                    if (!IsValidDoc(sourceFile))
                    {
                        return new KeyValuePair<bool, string>(false, $"{sourceFile.FileName}: Loại file không hợp lệ");
                    }
                    break;
                default:
                    break;
            }

            try
            {
                var shortFileName = Path.GetFileNameWithoutExtension(sourceFile.FileName);
                shortFileName = shortFileName.Length > 30 ? shortFileName.Substring(0, 30) : shortFileName;

                var fileName = shortFileName + "_" + DateTime.Now.Ticks.ToString() + Path.GetExtension(sourceFile.FileName);
                var directoryGen = GetUploadDirectoryGen(dirType);
                var directory = Path.Combine(rootPath, directoryGen);

                Directory.CreateDirectory(directory);
                var path = Path.Combine(directory, fileName);

                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await sourceFile.CopyToAsync(fileStream);
                }
                //upload done

                return new KeyValuePair<bool, string>(true, $"/{directoryGen}/{fileName}");
            }
            catch (Exception e)
            {
                return new KeyValuePair<bool, string>(false, $"{sourceFile.FileName}: " + e.Message);
            }
        }

        public static KeyValuePair<bool, string> RemoveFileFromServer(string path, string rootPath)
        {
            try //Maybe error could happen like Access denied or Presses Already User used
            {
                var fullPath = rootPath + path;
                if (!File.Exists(fullPath))
                    return new KeyValuePair<bool, string>(false, $"File {path} không tồn tại");

                File.Delete(fullPath);
                return new KeyValuePair<bool, string>(true, $"Delete {path} thành công");
            }
            catch (Exception e)
            {
                //Debug.WriteLine(e.Message);
                return new KeyValuePair<bool, string>(false, $"{path}: " + e.Message);
            }
        }
    }
}
