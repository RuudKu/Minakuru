@echo off
REM Enable recursive directory traversal
setlocal
 
REM Define the directory to start the search from (current directory in this case)
set "startDir=%cd%"
 
REM Traverse all subdirectories and look for 'obj' directories
for /r "%startDir%" %%d in (.) do (
    if exist "%%d\obj" (
        echo Deleting directory: %%d\obj
        rmdir /s /q "%%d\obj"
    )
    if exist "%%d\bin" (
        echo Deleting directory: %%d\bin
        rmdir /s /q "%%d\bin"
    )	
)
 
echo All obj and bin directories have been removed, if they existed.
endlocal
