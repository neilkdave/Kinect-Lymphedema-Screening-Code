@echo off
::
:: 

:: args parsing
set SUB_PATH=%~1
echo Saving to path %SUB_PATH%


:: RGB image
set RGB_PATH=C:\Users\thearchitect\Desktop\KinectBuild_SetupGuide\RunKinect\bin\Release
set RGB_EXE=RunKinectImage.exe

:: depth image
set DEPTH_PATH=C:\Users\thearchitect\Desktop\KinectBuild_SetupGuide\RunKinectImage\bin\Release
set DEPTH_EXE=RunKinect.exe

:: current workig directory
set PWD=%cd%


if exist %SUB_PATH%\NUL (
echo Directory already exists. Permission denied. Exit.
GOTO end
) else (
	mkdir %SUB_PATH%
	echo Created directory %SUB_PATH%
)

:: loop body
set IDX=0
:begin

	:: create filenames
	set IDX_FORMATTED=000%IDX%
	set IDX_FORMATTED=%IDX_FORMATTED:~-4%
	set RGB_FILENAME=%SUB_PATH%\%IDX_FORMATTED%.jpg
	set DEPTH_FILENAME=%SUB_PATH%\%IDX_FORMATTED%.txt
	::echo rgb: %RGB_FILENAME% :: depth=%DEPTH_FILENAME%

	:: run RGB image
	RunKinectImage %RGB_FILENAME%
	
	:: run depth image
	::"%DEPTH_PATH%\%DEPTH_EXE%" %DEPTH_FILENAME%
	
	:: increase index
	set /a IDX=%IDX% + 1
GOTO begin
:: :loop body

:: -------------------------------------------------------------
:end
@echo on
exit /b