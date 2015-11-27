@echo off

cd web
echo installing npm packages...
CALL npm install 
echo installing bower packages...
CALL bower install
echo installing grunt packages...
CALL grunt build

cd..

set PROJ=Desktop\BankA.Wpf\BankA.Wpf.csproj

.nuget\nuget restore BankA.sln

SET MSBUILDEXEDIR=C:\Program Files (x86)\MSBuild\14.0\Bin
SET MSBUILDEXE=%MSBUILDEXEDIR%\MSBuild.exe
SET MSBUILDOPT=/verbosity:minimal
SET CONFIGURATION=Debug
SET SolutionPath=%~dp0
SET ReleasePath=%SolutionPath%\Desktop\Release

"%MSBUILDEXE%" /p:Configuration=%CONFIGURATION%;Platform=x86;SolutionDir=%SolutionPath%;OutputPath=%ReleasePath% "%PROJ%"

cd %ReleasePath%
CALL BankA.Wpf.exe