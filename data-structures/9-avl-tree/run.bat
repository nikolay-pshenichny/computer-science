@echo off

SET PROGRAM_NAME=avl-tree
CALL :RUN

exit /b




:RUN
SET FRAMEWORK_PATH=C:\Windows\Microsoft.NET\Framework64\v4.0.30319\
"%FRAMEWORK_PATH%\csc.exe" /debug %PROGRAM_NAME%.cs > nul
.\%PROGRAM_NAME%.exe
del .\%PROGRAM_NAME%.exe
del .\%PROGRAM_NAME%.pdb
exit /b


:END