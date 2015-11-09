@echo off
::
:: 

:: args parsing
set SUB_PATH=%~1
echo Saving to path %SUB_PATH%


:: RGB image
set RGB_PATH=%cd%\RunKinectImageContinuous\RunKinectImage\bin\Release
set RGB_EXE=RunKinectImage.exe

:: depth image
set DEPTH_PATH=%cd%\RunKinectContinuous\RunKinect\bin\Release
set DEPTH_EXE=RunKinect.exe

:: current working directory
::set PWD=%cd%\Images


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
	"%RGB_PATH%\%RGB_EXE%" %RGB_FILENAME%
	
	:: run depth image
	"%DEPTH_PATH%\%DEPTH_EXE%" %DEPTH_FILENAME%
	
	:: increase index
	set /a IDX=%IDX% + 1
GOTO begin
:: :loop body

:: -------------------------------------------------------------
:end
@echo on
exit /b