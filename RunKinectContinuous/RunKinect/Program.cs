using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Drawing;
using System.Drawing.Imaging;
using OpenNI;

namespace Kinect_a
{

    //Creates a Kinect that can take data
    class Kinect
    {
        //unsafe
        //Declaration of class variables
        public static Context context;
        public static DepthGenerator depth;
        public static DepthMetaData depthMD;
        public static MapOutputMode mapMode;

        //Starts up necessary files to take data
        //Must run before TakeData()
        Kinect()
        {
            //Sets locations of XML File
            string SAMPLE_XML_FILE = @"SamplesConfig.xml";

            //Declares object of ScriptNode and defines context
            ScriptNode scriptNode;
            context = Context.CreateFromXmlFile(SAMPLE_XML_FILE, out scriptNode);

            //Declares the depth generator
            depth = context.FindExistingNode(NodeType.Depth) as DepthGenerator;
            //If the depth generator does not exist returns error messag
            if (depth == null)
            {
                Console.WriteLine("Sample must have a depth generator!");
                Console.ReadLine();
                return;
            }
            //Declares necessary variables and classes to take depth
            //DepthGenerator depth = context.FindExistingNode(NodeType.Depth) as DepthGenerator;
            mapMode = depth.MapOutputMode;
            depthMD = new DepthMetaData();

        }

        //Creates a text file of the matrix of depth values with no parameters
        static void TakeData()
        {

            //Waits for the depth to update itself
            context.WaitOneUpdateAll(depth);

            //Gets the  depth values
            depth.GetMetaData(depthMD);

            //Creates a string of the current time and date in format Month Day Year Hour Minute Seconds
            string filename = DateTime.Now.ToString("MMddyyHHmmssfff");
            filename = "a" + filename;

            //Opens the text writer and creates a new file with the a and the time as the file name
            TextWriter tw = new StreamWriter(@"D:\Users\CenSSIS\Documents\Kinect_Project\Test Files\" + filename + ".txt");
            //Sets height and width to the X and Y resolutions
            int height = mapMode.YRes;
            int width = mapMode.XRes;
            //For each pixel, writes a line of text: x y depth
            for (int h = 0; h < height; ++h)
            {
                for (int w = 0; w < width; ++w)
                {
                    tw.WriteLine(w + " " + h + " " + depthMD[w, h]);
                }
            }
            //Closes the text writer
            tw.Close();
        }


        //Creates a text file of the matrix of depth values with no parameters
        static UInt16MapData TakeData(int r)
        {

            //Waits for the depth to update itself
            context.WaitOneUpdateAll(depth);

            //Gets the  depth values
            //depth.GetMetaData(depthMD);
            UInt16MapData depthmap = depth.GetDepthMap();

            return depthmap;
        }

        //Creates a text file of the matrix of depth values taking a string parameter
        static string TakeData(string str)
        {
            ////Declares necessary variables and classes to take depth
            //DepthGenerator depth = context.FindExistingNode(NodeType.Depth) as DepthGenerator;
            //MapOutputMode mapMode = depth.MapOutputMode;
            //DepthMetaData depthMD = new DepthMetaData();

            //Waits for the depth to update itself
            context.WaitOneUpdateAll(depth);

            //Gets the  depth values
            depth.GetMetaData(depthMD);

            //Opens the text writer and creates a new file with the inputted string as the filename
            //TextWriter tw = new StreamWriter(@"D:\Users\CenSSIS\Documents\Kinect_Project\Test Files\" + str + ".txt");
            TextWriter tw = new StreamWriter(@str);
            //Sets height and width to the X and Y resolutions
            int height = mapMode.YRes;
            int width = mapMode.XRes;
            //For each pixel, writes a line of text: x y depth
            for (int h = 0; h < height; ++h)
            {
                for (int w = 0; w < width; ++w)
                {
                    tw.WriteLine(w + " " + h + " " + depthMD[w, h]);
                }
            }
            //Closes the text writer
            tw.Close();
            return str;
        }

        static void SplitData(string inputFile)
        {
            string baseName = "C:\\Users\\CenSISS\\Documents\\Kinect Project\\Test Files\\b" + DateTime.Now.ToString("MMddyyHHmmss");

            StreamWriter writer = null;
            try
            {
                using (StreamReader inputfile = new System.IO.StreamReader(inputFile))
                {
                    int count = 0;
                    string line;
                    while ((line = inputfile.ReadLine()) != null)
                    {

                        if (writer == null || line.StartsWith("0 0"))
                        {
                            if (writer != null)
                            {
                                writer.Close();
                                writer = null;
                            }

                            writer = new System.IO.StreamWriter(baseName + count.ToString() + ".txt", true);
                            count++;
                        }

                        writer.WriteLine(line.ToLower());
                    }
                }
            }
            finally
            {
                if (writer != null)
                    writer.Close();
            }
        }

        ////Takes image from Kinect
        //static void TakeImage(string filename)
        //{
        //    //Declares variables to take a color image
        //    ImageGenerator image = context.FindExistingNode(NodeType.Image) as ImageGenerator;
        //    ImageMetaData imageMD = new ImageMetaData();
        //    MapOutputMode mapMode = image.MapOutputMode;

        //    context.WaitOneUpdateAll(image);

        //    // Take current image 
        //    image.GetMetaData(imageMD);
        //    byte[] RGB = new byte[imageMD.DataSize];

        //    int width = (int)mapMode.XRes;
        //    int height = (int)mapMode.YRes;

        //    image.GetRGB24ImageMap();

        //   // WriteableBitmap imageBitmap1 = new WriteableBitmap(width, height, 96, 96, PixelFormats.Rgb24, null);
        //   // Bitmap imageBitmap = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

        //    unsafe
        //    {
        //        MapOutputMode mapImaMode = image.MapOutputMode;
        //        Bitmap g_TexMap = new Bitmap((int)mapMode.XRes, (int)mapMode.YRes, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
        //        Bitmap imageBitmap = new Bitmap((int)mapImaMode.XRes, (int)mapImaMode.YRes, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
        //        Rectangle rectIma = new Rectangle(0, 0, imageBitmap.Width, imageBitmap.Height);
        //        System.Drawing.Imaging.BitmapData dataIma = imageBitmap.LockBits(rectIma, ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

        //        byte* pIma = (byte*)image.ImageMapPtr;

        //        // set pixels 
        //        for (int y = 0; y < imageMD.YRes; ++y)
        //        {
        //            byte* pDest = (byte*)dataIma.Scan0.ToPointer() + y * dataIma.Stride;

        //            for (int x = 0; x < imageMD.XRes; ++x)
        //            {
        //                *(pDest++) = *(pIma++); // blue 
        //                *(pDest++) = *(pIma++); // green 
        //                *(pDest++) = *(pIma++); // red 
        //            }
        //        } 

        //        // RGB24Pixel* pDepth = (RGB24Pixel*)this.img.ImageMapPtr.ToPointer();
        //            //RGB24Pixel* pDepth = (RGB24Pixel*)imageMD.ImageMapPtr.ToPointer();
        //            //Rectangle rectIma = new Rectangle(0, 0, imageBitmap.Width,imageBitmap.Height);
        //            //System.Drawing.Imaging.BitmapData dataIma = imageBitmap.LockBits(rectIma, ImageLockMode.WriteOnly,
        //            //                 System.Drawing.Imaging.PixelFormat.Format24bppRgb); 
        //            //imageBitmap.Save(filename, System.Drawing.Imaging.ImageFormat.Jpeg);
        //            //for (int y = 0; y < imageMD.YRes; ++y)
        //            //{
        //            //    byte* pDest = (byte*)data.Scan0.ToPointer() + y * data.Stride;
        //            //    for (int x = 0; x < imageMD.XRes; ++x, ++pDepth, pDest += 3)
        //            //    {
        //            //        byte pixel = (byte)histogram[*pDepth];
        //            //        pDest[0] = 0;
        //            //        pDest[1] = pixel;
        //            //        pDest[2] = pixel;
        //            //    }
        //            //} 

        //        imageBitmap.Save(filename, System.Drawing.Imaging.ImageFormat.Jpeg);
        //    }
        //}

        static void Main(string[] args)
        {
            //Calls Start()
            Kinect k = new Kinect();
            ////Creates a text reader and writer to interact with the console
            //TextReader tIn = Console.In;
            //TextWriter tOut = Console.Out;

            ////Writes line
            //tOut.WriteLine("Data Collecting: Press any key to stop");
            ////Creates an array list to store the depth data
            //ArrayList depths = new ArrayList();
            ////Creates count to track how many times data is taken
            //int count = 0;
            ////Until a key is pressed, takes data after every update of depth information and adds it to the list
            //while (!Console.KeyAvailable)
            //{
            //    depths.Add(TakeData());
            //    count++;
            //}
            ////Displays the count and shows a waiting message
            //tOut.WriteLine(count);
            //tOut.WriteLine("Data Collection Stopped: Please Wait");
            ////Creates a string of the current time and date in format Month Day Year Hour Minute Seconds
            //string filename = "b" + DateTime.Now.ToString("MMddyyHHmmss");
            //int fcount = 1;
            ////Writes each set of to a text file
            //foreach (UInt16MapData d in depths)
            //{

            //    //Opens the text writer and creates a new file with the a and the time as the file name
            //    TextWriter tw = new StreamWriter(@"D:\Users\CenSSIS\Documents\Kinect_Project\Test Files\Shaw XBS 8 1\" + filename + fcount.ToString() + ".txt");
            //    //Sets height and width to the X and Y resolutions
            //    int height = mapMode.YRes;
            //    int width = mapMode.XRes;
            //    //For each pixel, writes a line of text: x y depth
            //    for (int h = 0; h < height; ++h)
            //    {
            //        for (int w = 0; w < width; ++w)
            //        {
            //            tw.WriteLine(w + " " + h + " " + d[w, h]);
            //        }
            //    }
            //    //Closes the text writer
            //    tw.Close();
            //    fcount++;
            //}

            //Creates a text reader and writer to interact with the console
            TextReader tIn = Console.In;
            TextWriter tOut = Console.Out;

            // get filename 
            string filename;
            if (args.Length == 0)
            {
                //Gets the filename from the console
                Console.WriteLine("Enter filename:");
                filename = tIn.ReadLine();
            }
            else // if (args.Length == 1)
            {
                // take 1st arg as the filename; note, don't use spaces or if you do, use ""
                filename = args[0];
            }
            //else
            {
                // @todo should throw an error here or write usage ...
            }

            //Takes data with the filename
            if (filename == "run")
            {
                Console.WriteLine("'run' mode...");
                while (true)
                {
                    TakeData();
                }
            }
            else
            {
                Console.WriteLine("Record image to file '" + filename + "'...");
                TakeData(filename);
            }
            string output = TakeData(filename);
        }
    }
}

//string filename = DateTime.Now.ToString("MMddyyHHmmss");
//filename = "C:\\Users\\CenSISS\\Documents\\Kinect Project\\Test Files\\a" + filename + ".txt";
////Opens the text writer and creates a new file with the a and the time as the file name
//TextWriter tw = new StreamWriter(@filename);
//while (!Console.KeyAvailable)
//{
//    DepthMetaData depthMD2 = TakeData();
//    //Sets height and width to the X and Y resolutions
//    int height = mapMode.YRes;
//    int width = mapMode.XRes;
//    for (int h = 0; h < height; ++h)
//    {
//        for (int w = 0; w < width; ++w)
//        {
//            tw.WriteLine(w + " " + h + " " + depthMD2[w, h]);
//        }
//    }
//}
//tw.Close();
//TextReader tIn = Console.In;
//string temp = tIn.ReadLine();
//SplitData(filename);