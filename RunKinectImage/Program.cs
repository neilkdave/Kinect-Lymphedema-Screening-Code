using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Drawing;
using System.Drawing.Imaging;
using OpenNI;
using std;

namespace RunKinectImage
{
    class Kinect
    {
        //Starts up necessary files to take data
        //Must run before TakeData()
        unsafe static void TakeImage(string filename)
        {
            //Sets locations of XML File
            string SAMPLE_XML_FILE = @"..\\..\\..\\SamplesConfig.xml";

            //Declares object of ScriptNode and defines context
            ScriptNode scriptNode;
            Context context = Context.CreateFromXmlFile(SAMPLE_XML_FILE, out scriptNode);

            //Declares variables to take a color image
            ImageGenerator image = context.FindExistingNode(NodeType.Image) as ImageGenerator;
            ImageMetaData imageMD = new ImageMetaData();
            MapOutputMode mapMode = image.MapOutputMode;

            //Waits for the Kinect to updage
            context.WaitOneUpdateAll(image);

            // Take current image 
            image.GetMetaData(imageMD);
            byte[] RGB = new byte[imageMD.DataSize];

            //Sets widt
            //int width = (int)mapMode.XRes;
            //int height = (int)mapMode.YRes;

            //Takes the image map
            image.GetRGB24ImageMap();

            unsafe
            {
                //Declares bitmaps and rectangles to take the image data
                Bitmap g_TexMap = new Bitmap((int)mapMode.XRes, (int)mapMode.YRes, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                Bitmap imageBitmap = new Bitmap((int)mapMode.XRes, (int)mapMode.YRes, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                Rectangle rectIma = new Rectangle(0, 0, imageBitmap.Width, imageBitmap.Height);
                System.Drawing.Imaging.BitmapData dataIma = imageBitmap.LockBits(rectIma, ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

                //Creates a pointer for the image
                byte* pIma = (byte*)image.ImageMapPtr;

                // set pixels 
                for (int y = 0; y < imageMD.YRes; ++y)
                {
                    //Declares the pointer for the destination pointers
                    byte* pDest = (byte*)dataIma.Scan0.ToPointer() + y * dataIma.Stride;

                    //Puts the different colored pixels into the destination
                    for (int x = 0; x < imageMD.XRes; ++x)
                    {
                        *((pDest++)+2) = *(pIma++); // blue 
                        *(pDest++) = *(pIma++); // green 
                        *((pDest++)-2) = *(pIma++); // red 
                    }
                }
                //Flips image: If Kinect is mounted rightside up, take this line out
                //imageBitmap.RotateFlip(RotateFlipType.Rotate180FlipX);
                //Saves the image as a jpg
                imageBitmap.Save(filename, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
        }

        static void Main(string[] args)
        {
            //Creates a text reader and writer to interact with the console
            TextReader tIn = Console.In;
            TextWriter tOut = Console.Out;
            //Gets the filenmae from the console
            string filename = tIn.ReadLine();
            filename = "..\\..\\..\\Images\\" + filename;
            //Takes image with the filename
            TakeImage(filename);
        }
    }
}

// RGB24Pixel* pDepth = (RGB24Pixel*)this.img.ImageMapPtr.ToPointer();
//RGB24Pixel* pDepth = (RGB24Pixel*)imageMD.ImageMapPtr.ToPointer();
//Rectangle rectIma = new Rectangle(0, 0, imageBitmap.Width,imageBitmap.Height);
//System.Drawing.Imaging.BitmapData dataIma = imageBitmap.LockBits(rectIma, ImageLockMode.WriteOnly,
//                 System.Drawing.Imaging.PixelFormat.Format24bppRgb); 
//imageBitmap.Save(filename, System.Drawing.Imaging.ImageFormat.Jpeg);
//for (int y = 0; y < imageMD.YRes; ++y)
//{
//    byte* pDest = (byte*)data.Scan0.ToPointer() + y * data.Stride;
//    for (int x = 0; x < imageMD.XRes; ++x, ++pDepth, pDest += 3)
//    {
//        byte pixel = (byte)histogram[*pDepth];
//        pDest[0] = 0;
//        pDest[1] = pixel;
//        pDest[2] = pixel;
//    }
//} 