@echo off
@title Deploy FakeGoogleDrive Front-End  

cd ./FGD.Angular

ECHO Installing Angular CLI...
call npm install -g @angular/cli
echo.

PUSHD node_modules && POPD\%NUL% || (
	ECHO Angular dependencies for FGD installing
	call npm i 
	echo.
)
  
ECHO Compiling and opening FGD Client
call ng serve --open
echo.

cmd /k