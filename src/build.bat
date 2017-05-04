@echo off

cd web
echo installing npm packages...
CALL npm install 
echo installing bower packages...
CALL bower install
echo installing grunt packages...
CALL grunt build

