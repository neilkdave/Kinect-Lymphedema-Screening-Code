/*RunKinect.cs
 * Takes a matrix of depth data to be processed by MATLAB to create a 3D surface
 * Written by Jenna Czeck
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Drawing;
using OpenNI;


namespace Kinect_a
{
    //Class Kinect that can take data in the form of a matrix of depth value
    class Kinect
    {
        //Declaration of class variables
        public static Context context;

        //Constructor Method
        //Starts up necessary files to take data
        Kinect()
        {
            //Sets locations of XML File
            string SAMPLE_XML_FILE = @"C:\Users\CenSISS\Documents\Kinect Project\SamplesConfig.xml";

            //Declares object of ScriptNode and defines context
            ScriptNode scriptNode;
            context = Context.CreateFromXmlFile(SAMPLE_XML_FILE, out scriptNode);

            //Declares the depth generator
            DepthGenerator depth = context.FindExistingNode(NodeType.Depth) as DepthGenerator;
            //If the depth generator does not exist returns error messag
            if (depth == null)
            {
                Console.WriteLine("Sample must have a depth generator!");
                Console.ReadLine();
                return;
            }

        }

        //Creates a text file of the matrix of depth values with no parameters
        static string TakeData()
        {
            //Declares necessary variables and classes to take depth
            DepthGenerator depth = context.FindExistingNode(NodeType.Depth) as DepthGenerator;
            MapOutputMode mapMode = depth.MapOutputMode;
            DepthMetaData depthMD = new DepthMetaData();

            //Waits for the depth to update itself
            context.WaitOneUpdateAll(depth);

            //Gets the  depth values
            depth.GetMetaData(depthMD);

            //Creates a string of the current time and date in format Month Day Year Hour Minute Seconds
            string filename = DateTime.Now.ToString("MMddyyHHmmssfff");
            filename = "a" + filename;

            //Opens the text writer and creates a new file with the previously defined file name
            TextWriter tw = new StreamWriter(@"C:\Users\CenSISS\Documents\Kinect Project\Test Files\" + filename + ".txt");
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
            return filename;
        }

        //Creates a text file of the matrix of depth values taking a string parameter
        static string TakeData(string str)
        {
            //Declares necessary variables and classes to take depth
            DepthGenerator depth = context.FindExistingNode(NodeType.Depth) as DepthGenerator;
            MapOutputMode mapMode = depth.MapOutputMode;
            DepthMetaData depthMD = new DepthMetaData();

            //Waits for the depth to update itself
            context.WaitOneUpdateAll(depth);

            //Gets the  depth values
            depth.GetMetaData(depthMD);

            //Opens the text writer and creates a new file with the inputted string as the filename
            TextWriter tw = new StreamWriter(@"C:\Users\CenSISS\Documents\Kinect Project\Test Files\" + str + ".txt");
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

        //Takes image from Kinect
        //static void TakeImage()
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



        //    //WriteableBitmap imageBitmap = new WriteableBitmap(width, height, 96, 96, PixelFormats.Rgb24, null);
        //    //Bitmap imageBitmap1 = new Bitmap(width, height);

        //    //    width, height, 96, 96, PixelFormats.Rgb24, null);
        //    //imageBitmap.Save(
        //}


        static void Main(string[] args)
        {
            //Calls Start()
            Kinect k = new Kinect();

            TextReader tIn = Console.In;
            TextWriter tOut = Console.Out;

            string output = TakeData();
            tOut.WriteLine(output);

            //Waits for input to take data
            //Console.WriteLine("Press enter to take data");
            //Console.ReadLine();
            ////Defines the string user as an empty string
            //string user = "Test31";
            ////Repeats until the program breaks
            //while (true)
            //{
            //    if (user == "E" || user == "e")
            //        break;
            //    else if (user == "")
            //    {
            //        //Takes data
            //        TakeData();
            //        //Asks for input and defines user as the input
            //        Console.WriteLine("Press enter to take data or type E to exit");
            //        user = Console.ReadLine();
            //    }
            //    else
            //    {
            //        //Takes data defining the name as the user input
            //        TakeData(user);
            //        //Asks for input and defines user as the input
            //        Console.WriteLine("Press enter to take data or type E to exit");
            //        user = Console.ReadLine();
            //    }
            //}
        }
    }
}