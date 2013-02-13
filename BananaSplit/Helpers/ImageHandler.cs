using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.Text;
using System.IO;
using System.Configuration;
using Kaliko.ImageLibrary;

namespace BananaSplit.Helpers
{
    public class ImageHandler
    {
        private const int DEFAULT_THUMB_SIZE = 100;
        private const string slash = @"_";

        public static ImagePaths SaveThumbnail(string originalImagePath, int thumbSize = DEFAULT_THUMB_SIZE, int imgQuality = 95)
        {
            var origImg = new KalikoImage(originalImagePath);

            //Create thumbnail name from original image name
            //var ext = Path.GetExtension(originalImagePath);
            //var imgWOExt = Path.GetFileNameWithoutExtension(originalImagePath);
            var newThumbName = GetUniqueFileName(originalImagePath, "thumb");// String.Format("{0}_thumb.{1}", imgWOExt, ext);

            //var origFileName = Path.GetFileName(originalImagePath);

            var newThumbPath = GetNewFullPath(originalImagePath, newThumbName); // originalImagePath.Replace(origFileName, newThumbName);

            var splitPath = originalImagePath.Split(@"\".ToCharArray());
            var unixStyleFolderPortion = new StringBuilder();
            for (var i = 0; i < splitPath.Length; i++)
            {
                if (i == 0) { continue; } //We don't want the drive letter
                if (i == splitPath.Length - 1) { break; } //we don't want the fileName either
                unixStyleFolderPortion.AppendFormat("{0}/", splitPath[i]);  //we just need the folder paths
            }

            var dateFolder = splitPath[splitPath.Length - 2];

            origImg.GetThumbnailImage(thumbSize, thumbSize, ThumbnailMethod.Crop).SavePng(newThumbPath, imgQuality);
            //var webAddress = String.Concat(ConfigurationManager.AppSettings["BaseDomain"], unixStyleFolderPortion.ToString(), newThumbName);
            //var webAddress = String.Concat(ConfigurationManager.AppSettings["BaseDomain"], "Uploads/", dateFolder, slash, newThumbName);
            var webAddress = String.Concat(ConfigurationManager.AppSettings["BaseDomain"], "Uploads/", newThumbName);
            //var sqlAddress = String.Format("{0}{1}", unixStyleFolderPortion.ToString(), newThumbName);
            //var sqlAddress = String.Format("Uploads/{0}{1}{2}", dateFolder, slash, newThumbName);
            var sqlAddress = String.Format("Uploads/{0}", newThumbName);

            var paths = new ImagePaths(webAddress, sqlAddress, newThumbPath);

            return paths;

        }

        public static Boolean DeleteImage(string filepath)
        {
            try
            {
                File.Delete(filepath);
                return true;
            }
            catch (Exception e)
            {
                //Log error in future
            }
            return false;
        }

        public static String ConvertPartialWebPathToFilePath(string webPath)
        {
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var path = webPath.Replace("/", @"\");
            var filePath = String.Concat(baseDirectory, path);
            return filePath;
        }


        private static String GetUniqueFileName(string originalPath, string uniquePortion)
        {
            var ext = ".png"; //Path.GetExtension(originalPath);
            var fileNameWOExt = Path.GetFileNameWithoutExtension(originalPath);
            var newName = String.Format("{0}_{1}{2}", fileNameWOExt, uniquePortion, ext);
            return newName;
        }

        private static String GetNewFullPath(string originalFullPath, string newName)
        {
            var origFileName = Path.GetFileName(originalFullPath);
            var pathWOFileName = originalFullPath.Replace(origFileName, "");
            if (!Directory.Exists(pathWOFileName))
            {
                Directory.CreateDirectory(pathWOFileName);
            }
            var newPath = originalFullPath.Replace(origFileName, newName);
            return newPath;
        }

        private static Image cropImage(Image img, Rectangle cropArea)
        {
            Bitmap bmpImage = new Bitmap(img);
            Bitmap bmpCrop = bmpImage.Clone(cropArea,
            bmpImage.PixelFormat);
            return (Image)(bmpCrop);
        }

        public static ImagePaths SaveCroppedImage(string originalImageFilePath, int xCoord, int yCoord, int width, int height)
        {
            ImagePaths paths = null;
            var cropRect = new Rectangle(xCoord, yCoord, width, height);
            using (var bmpImage = new Bitmap(originalImageFilePath))
            {
                var bmpCrop = bmpImage.Clone(cropRect, bmpImage.PixelFormat);
                //Now save
                var uniqueCropFileName = GetUniqueFileName(originalImageFilePath, "crop");
                var cropPath = GetNewFullPath(originalImageFilePath, uniqueCropFileName);

                paths = SaveImage(cropPath, uniqueCropFileName, bmpImage);
            }

            return paths;
        }

        private static ImagePaths SaveImage(string fullFilePath, string uniqueFileName, Bitmap image)
        {
            var uploadFolder = GetFolderPathWithoutDriveLetter(fullFilePath);

            if (File.Exists(fullFilePath))
            {
                //try
                //{
                File.Delete(fullFilePath);

                //}catch(Exception e)
                //{
                //    System.GC.Collect();
                //    System.GC.WaitForPendingFinalizers();
                //}
                //if (File.Exists(fullFilePath))
                //{
                //    try
                //    {
                //        File.Delete(fullFilePath);
                //    }
                //    catch (Exception)
                //    {

                //    }
                //}
            }

            image.Save(fullFilePath); //Save Image

            //Use Web Address later
            var webAddress = String.Concat(ConfigurationManager.AppSettings["BaseDomain"], uploadFolder, uniqueFileName);
            var sqlAddress = String.Format("{0}{1}", uploadFolder, uniqueFileName);

            var paths = new ImagePaths(webAddress, sqlAddress, fullFilePath);
            return paths;
        }

        private static String GetFolderPathWithoutDriveLetter(string folderPath, string startFolder = "Uploads")
        {
            var splitPath = folderPath.Split(@"\".ToCharArray());
            var unixStyleFolderPortion = new StringBuilder();
            var foundStart = false;

            for (var i = 0; i < splitPath.Length; i++)
            {
                if (i == 0) { continue; } //We don't want the drive letter   
                if (splitPath[i].Equals(startFolder, StringComparison.OrdinalIgnoreCase))
                {
                    foundStart = true;
                }
                if (foundStart)
                {
                    unixStyleFolderPortion.AppendFormat("{0}/", splitPath[i]);  //we just need the folder paths
                }
            }
            return unixStyleFolderPortion.ToString();
        }

        public static String GetDatedFolderPath()
        {
            var now = DateTime.Now;
            var datedFolder = String.Format("{0}_{1}_{2}", now.Month, now.Day, now.Year);
            return datedFolder;
        }

        public static ImageDimensions GetImageDimensions(string imageFilePath)
        {
            ImageDimensions imgDimensions = null;
            try
            {
                using (var img = new Bitmap(imageFilePath))
                {
                    imgDimensions = new ImageDimensions(img.Width, img.Height);
                }
            }
            catch (Exception e)
            {
                //Do nothing
            }
            return imgDimensions;
        }
    }
}