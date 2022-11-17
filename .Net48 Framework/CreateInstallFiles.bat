set rootDir=%1
c:
cd\
cd "Program Files"
cd WinRAR
winrar.exe a -r -ep1 "\\server10\Builds\SepPortal\%rootDir%\Trunk %rootDir%\SepCity-2.7.zip" "\\server10\Builds\SepPortal\%rootDir%\Trunk %rootDir%\FTP\*"
winrar.exe a -r -x.project -x"APS Package" -ep1 "\\server10\Builds\SepPortal\%rootDir%\Trunk %rootDir%\SepCity-2.7-1.app.zip" "\\server10\Builds\SepPortal\%rootDir%\Trunk %rootDir%\Plesk\*"
winrar.exe a -r -ep1 "\\server10\Builds\SepPortal\%rootDir%\Trunk %rootDir%\SepCity-2.7-IIS.zip" "\\server10\Builds\SepPortal\%rootDir%\Trunk %rootDir%\Microsoft App Gallery\*"
d:
cd\Builds\SepPortal\%rootDir%\Trunk %rootDir%\
rmdir wwwroot /S /Q
rmdir installs /S /Q
del *.exe
del *.sln
del *.vssscc
del *.bat
