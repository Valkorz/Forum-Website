@echo off
:: Initialize the C# API and get it's process token to also shut down when finished using.
:: use "%~dp0" to find the relative path for the batch file.

::start cmd and go to api folder, use dotnet run
::start cmd /k "cd ForumApi\api && dotnet run"
start /b cmd /k "cd ForumInterface && npm start"
start /b cmd /k "cd ForumApi/api && dotnet run --urls http://1.1.1.1:5000/"
start http://localhost:5000

::get task token and kill the specific task
pause
::for /F "tokens=2 delims=," %%f in ('tasklist /NH /FI "IMAGENAME eq dotnet.exe" /FO CSV') do set dotnetPID=%%~f
for /F "tokens=2 delims=," %%f in ('tasklist /NH /FI "IMAGENAME eq node.exe" /FO CSV') do set nodePID=%%~f
for /F "tokens=2 delims=," %%f in ('tasklist /NH /FI "IMAGENAME eq dotnet.exe" /FO CSV') do set dotnetPID=%%~f
echo "Finishing process..."
echo %nodePID%
echo %dotnetPID%
taskkill /F /PID %dotnetPID%
taskkill /F /PID %nodePID%