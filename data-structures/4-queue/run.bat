@echo off

SET PROGRAM_NAME=based-on-linked-list
CALL :RUN

SET PROGRAM_NAME=based-on-array
CALL :RUN

SET PROGRAM_NAME=priority-queue
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