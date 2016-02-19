@echo off
SET PROGRAM_NAME=program
SET FRAMEWORK_PATH=C:\Windows\Microsoft.NET\Framework64\v4.0.30319\

REM Roslyn
REM SET FRAMEWORK_PATH=C:\Program Files (x86)\MSBuild\14.0\Bin


"%FRAMEWORK_PATH%\csc.exe" /debug %PROGRAM_NAME%.cs > nul
.\%PROGRAM_NAME%.exe
del .\%PROGRAM_NAME%.exe
del .\%PROGRAM_NAME%.pdb