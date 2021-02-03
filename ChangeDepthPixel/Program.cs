using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace ChangePixelFormat
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Directory Path: ");
            string dirPath = Console.ReadLine();
            if (Directory.Exists(dirPath))
            {
                Console.WriteLine("Processing images from directory...");
                DirectoryInfo dirIn = new DirectoryInfo(dirPath);
                ProcessDirectory(dirIn);
                Console.WriteLine("!!!Complete!!!");
            }
            else
                Console.WriteLine("Directory not found");
            Console.ReadLine();
        }

        private static void ProcessDirectory(DirectoryInfo dirIn)
        {
            foreach (var file in dirIn.GetFiles())
            {
                if (!Directory.Exists("out/"))
                    Directory.CreateDirectory("out/");
                saveImage("out/", file, PixelFormat.Format32bppArgb);
            }
            foreach (var dir in dirIn.GetDirectories())
                ProcessDirectory(dir);
        }

        static void saveImage(string dirPath, FileInfo fileImage, PixelFormat format)
        {
            try
            {
                Bitmap myBitmap = new Bitmap(fileImage.FullName);
                Bitmap clone = myBitmap.Clone(new Rectangle(0, 0, myBitmap.Width, myBitmap.Height), format);
                clone.Save(dirPath + fileImage.Name);
            }
            catch
            {
                Console.WriteLine("Error processing file \"" + fileImage.FullName + "\"");
            }
        }
    }
}
