set rootDir=%1
d:
cd\Builds\SepPortal\%rootDir%\Trunk %rootDir%\wwwroot\
xcopy * "\\server10\Builds\SepPortal\%rootDir%\Trunk %rootDir%\Installs\FTP\" /C /Y /E
xcopy * "\\server10\Builds\SepPortal\%rootDir%\Trunk %rootDir%\Installs\Microsoft App Gallery\SepCity\" /C /Y /E
cd install
cd sql
xcopy * "\\server10\Builds\SepPortal\%rootDir%\Trunk %rootDir%\Installs\Plesk\scripts\" /C /Y /E
cd ..
cd ..
rd install /S /Q
xcopy * "\\server10\Builds\SepPortal\%rootDir%\Trunk %rootDir%\Installs\Plesk\htdocs\" /C /Y /E
cd\Builds\SepPortal\%rootDir%\Trunk %rootDir%\
cd installs
cd "Microsoft App Gallery"
cd SepCity
cd install
cd sql
xcopy install_temp.xml "\\server10\Builds\SepPortal\%rootDir%\Trunk %rootDir%\Installs\Microsoft App Gallery\SepCity\app_data\" /C /Y /E
xcopy system.xml "\\server10\Builds\SepPortal\%rootDir%\Trunk %rootDir%\Installs\Microsoft App Gallery\SepCity\app_data\" /C /Y /E
cd\Builds\SepPortal\%rootDir%\Trunk %rootDir%\wwwroot\
xcopy * "c:\inetpub\demo.sepcity.com\www" /s /y
