RunKinect and RunKinectImage Builds should be up to date and dependent on relative filepaths. Rebuild solutions
if RunKinect.exe and RunKinectImage.exe aren't found.

Default Code generates a folder with depth text files and image jpegs,in the Images folder.

To take a single depth image navigate to RunKinect\bin\Release and click on RunKinect.exe (or do this in command line)
Input the name of the text file you want to output. 
The depth file will be output in the Images folder

To take a single RGB image navigate to RunKinectImage\bin\Release and click on RunKinectImage.exe (or do this in command line)
Input the name of the .jpg including the extension ex) image.jpg
The RGB file will be output in the Images folder.

Add SamplesConfig.xml and TakeImageandDepEverySecond.bat to same folder with RunKinectContinuous and RunKinectImageContinuous
to run a script which takes and formats an RGB image and DEPTH image every second. (XML file formats depth data in matrix format size of RBG image for easy reconstruction while .bat runs both .exe files to generate and name each RGB and Depth image)

Add recordKinectImageAndDepth.bat to the same folder with RunKinectContinuous and RunKinectImage Continuous
to run a script which takes and formats a single RGB and DEPTH image. Must be in same location as SamplesConfig.xml