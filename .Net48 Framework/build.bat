rem set project so its not in debug mode
cd\sepcity\SepPortal\Trunk
fart -r -c -- "C:\sepcity\SepPortal\Trunk\SepCommon\functions.cs" "public bool DebugMode = true;" "public bool DebugMode = false;"
rem build the project
cd\
cd "Program Files (x86)"
cd "Microsoft Visual Studio"
cd "2022"
cd "BuildTools"
cd "MSBuild"
cd "Current"
cd Bin
msbuild "C:\sepcity\SepPortal\Trunk\SepPortal.sln" /detailedsummary /t:rebuild
rem delete old install files
cd\sepcity\SepPortal\Trunk\installs
del SepCity-3.1.zip
del SepCity-3.1-1.app.zip
del SepCity-3.1-IIS.zip
rd FTP /S /Q
cd "Microsoft App Gallery"
rd "SepCity" /S /Q
cd..
cd plesk
rd htdocs /S /Q
cd..
rem start copying files to the compiles folder
cd..
rd compiled /S /Q
mkdir compiled
cd wwwroot
xcopy * "C:\sepcity\SepPortal\Trunk\compiled" /C /Y /E
cd\inetpub\SepPortal\bin
xcopy * "C:\sepcity\SepPortal\Trunk\compiled\bin" /C /Y /E /i
cd\sepcity\SepPortal\components\yuicompressor\build
rem minify js and css files
java -jar yuicompressor-2.4.7.jar "c:\sepcity\SepPortal\Trunk\compiled\js\main.js" -o "c:\sepcity\SepPortal\Trunk\compiled\js\main.js"
java -jar yuicompressor-2.4.7.jar "c:\sepcity\SepPortal\Trunk\compiled\js\CategorySelection.js" -o "c:\sepcity\SepPortal\Trunk\compiled\js\CategorySelection.js"
java -jar yuicompressor-2.4.7.jar "c:\sepcity\SepPortal\Trunk\compiled\js\country.js" -o "c:\sepcity\SepPortal\Trunk\compiled\js\country.js"
java -jar yuicompressor-2.4.7.jar "c:\sepcity\SepPortal\Trunk\compiled\js\custom_dropdowns.js" -o "c:\sepcity\SepPortal\Trunk\compiled\js\custom_dropdowns.js"
java -jar yuicompressor-2.4.7.jar "c:\sepcity\SepPortal\Trunk\compiled\js\filters.js" -o "c:\sepcity\SepPortal\Trunk\compiled\js\filters.js"
java -jar yuicompressor-2.4.7.jar "c:\sepcity\SepPortal\Trunk\compiled\js\management.js" -o "c:\sepcity\SepPortal\Trunk\compiled\js\management.js"
java -jar yuicompressor-2.4.7.jar "c:\sepcity\SepPortal\Trunk\compiled\js\customfields_modify.js" -o "c:\sepcity\SepPortal\Trunk\compiled\js\customfields_modify.js"
java -jar yuicompressor-2.4.7.jar "c:\sepcity\SepPortal\Trunk\compiled\js\facebook.js" -o "c:\sepcity\SepPortal\Trunk\compiled\js\facebook.js"
java -jar yuicompressor-2.4.7.jar "c:\sepcity\SepPortal\Trunk\compiled\js\gridview.js" -o "c:\sepcity\SepPortal\Trunk\compiled\js\gridview.js"
java -jar yuicompressor-2.4.7.jar "c:\sepcity\SepPortal\Trunk\compiled\js\my_feeds.js" -o "c:\sepcity\SepPortal\Trunk\compiled\js\my_feeds.js"
java -jar yuicompressor-2.4.7.jar "c:\sepcity\SepPortal\Trunk\compiled\js\site_template.js" -o "c:\sepcity\SepPortal\Trunk\compiled\js\site_template.js"
java -jar yuicompressor-2.4.7.jar "c:\sepcity\SepPortal\Trunk\compiled\js\swfupload.js" -o "c:\sepcity\SepPortal\Trunk\compiled\js\swfupload.js"
java -jar yuicompressor-2.4.7.jar "c:\sepcity\SepPortal\Trunk\compiled\skins\public\styles\prettyPhoto.css" -o "c:\sepcity\SepPortal\Trunk\compiled\skins\public\styles\prettyPhoto.css"
java -jar yuicompressor-2.4.7.jar "c:\sepcity\SepPortal\Trunk\compiled\skins\public\styles\public.css" -o "c:\sepcity\SepPortal\Trunk\compiled\skins\public\styles\public.css"
java -jar yuicompressor-2.4.7.jar "c:\sepcity\SepPortal\Trunk\compiled\skins\BusinessCasual\styles\layout.css" -o "c:\sepcity\SepPortal\Trunk\compiled\skins\BusinessCasual\styles\layout.css"
java -jar yuicompressor-2.4.7.jar "c:\sepcity\SepPortal\Trunk\compiled\skins\BusinessWay\styles\layout.css" -o "c:\sepcity\SepPortal\Trunk\compiled\skins\BusinessWay\styles\layout.css"
java -jar yuicompressor-2.4.7.jar "c:\sepcity\SepPortal\Trunk\compiled\skins\BusinessWebDesign\styles\layout.css" -o "c:\sepcity\SepPortal\Trunk\compiled\skins\BusinessWebDesign\styles\layout.css"
java -jar yuicompressor-2.4.7.jar "c:\sepcity\SepPortal\Trunk\compiled\skins\BusinessWorld\styles\layout.css" -o "c:\sepcity\SepPortal\Trunk\compiled\skins\BusinessWorld\styles\layout.css"
java -jar yuicompressor-2.4.7.jar "c:\sepcity\SepPortal\Trunk\compiled\skins\CreamBurn\styles\layout.css" -o "c:\sepcity\SepPortal\Trunk\compiled\skins\CreamBurn\styles\layout.css"
java -jar yuicompressor-2.4.7.jar "c:\sepcity\SepPortal\Trunk\compiled\skins\CyberArray\styles\layout.css" -o "c:\sepcity\SepPortal\Trunk\compiled\skins\CyberArray\styles\layout.css"
java -jar yuicompressor-2.4.7.jar "c:\sepcity\SepPortal\Trunk\compiled\skins\DuetPlasma\styles\layout.css" -o "c:\sepcity\SepPortal\Trunk\compiled\skins\DuetPlasma\styles\layout.css"
java -jar yuicompressor-2.4.7.jar "c:\sepcity\SepPortal\Trunk\compiled\skins\GlobalHouse\styles\layout.css" -o "c:\sepcity\SepPortal\Trunk\compiled\skins\GlobalHouse\styles\layout.css"
java -jar yuicompressor-2.4.7.jar "c:\sepcity\SepPortal\Trunk\compiled\skins\IdeaLab\styles\layout.css" -o "c:\sepcity\SepPortal\Trunk\compiled\skins\IdeaLab\styles\layout.css"
java -jar yuicompressor-2.4.7.jar "c:\sepcity\SepPortal\Trunk\compiled\skins\ITDesk\styles\layout.css" -o "c:\sepcity\SepPortal\Trunk\compiled\skins\ITDesk\styles\layout.css"
java -jar yuicompressor-2.4.7.jar "c:\sepcity\SepPortal\Trunk\compiled\skins\LinkLine\styles\layout.css" -o "c:\sepcity\SepPortal\Trunk\compiled\skins\LinkLine\styles\layout.css"
java -jar yuicompressor-2.4.7.jar "c:\sepcity\SepPortal\Trunk\compiled\skins\ProIndustries\styles\layout.css" -o "c:\sepcity\SepPortal\Trunk\compiled\skins\ProIndustries\styles\layout.css"
java -jar yuicompressor-2.4.7.jar "c:\sepcity\SepPortal\Trunk\compiled\skins\SmartDesign\styles\layout.css" -o "c:\sepcity\SepPortal\Trunk\compiled\skins\SmartDesign\styles\layout.css"
java -jar yuicompressor-2.4.7.jar "c:\sepcity\SepPortal\Trunk\compiled\skins\SocialNet\styles\layout.css" -o "c:\sepcity\SepPortal\Trunk\compiled\skins\SocialNet\styles\layout.css"
java -jar yuicompressor-2.4.7.jar "c:\sepcity\SepPortal\Trunk\compiled\skins\WideCommerce\styles\layout.css" -o "c:\sepcity\SepPortal\Trunk\compiled\skins\WideCommerce\styles\layout.css"
java -jar yuicompressor-2.4.7.jar "c:\sepcity\SepPortal\Trunk\compiled\skins\WinGlobal\styles\layout.css" -o "c:\sepcity\SepPortal\Trunk\compiled\skins\WinGlobal\styles\layout.css"
java -jar yuicompressor-2.4.7.jar "c:\sepcity\SepPortal\Trunk\compiled\skins\YellowStone\styles\layout.css" -o "c:\sepcity\SepPortal\Trunk\compiled\skins\YellowStone\styles\layout.css"
cd\sepcity\SepPortal\
cd "Trunk"
cd compiled
del *.cs
del celebrities*
del wwwroot.csproj
del wwwroot.csproj.user
del wwwroot.Publish.xml
del Web.Debug.config
del Web.Release.config
cd downloads
del *.zip
del *.gif
del *.jpg
del *.png
del *.wav
del *.mp3
del *.avi
del *.flv
del *.doc
del *.dovx
del *.pdf
del *.rtf
del *.txt
cd ..
cd spadmin
del *.cs
cd ..
cd twilio
del *.cs
cd ..
cd dashboard
del *.cs
cd ..
cd install
del *.cs
cd ..
cd js
del *.cs
cd ..
cd help
del *.cs
cd ..
cd spadmin
cd imagemanager
del *.cs
cd ..
cd ..
cd skins
del *.cs
cd BusinessCasual
del config.xml
cd..
cd BusinessWay
del config.xml
cd..
cd BusinessWebDesign
del config.xml
cd..
cd BusinessWorld
del config.xml
cd..
cd CreamBurn
del config.xml
cd..
cd CyberArray
del config.xml
cd..
cd DuetPlasma
del config.xml
cd..
cd GlobalHouse
del config.xml
cd..
cd IdeaLab
del config.xml
cd..
cd ITDesk
del config.xml
cd..
cd LinkLine
del config.xml
cd..
cd ProIndustries
del config.xml
cd..
cd SmartDesign
del config.xml
cd..
cd SocialNet
del config.xml
cd..
cd WideCommerce
del config.xml
cd..
cd WinGlobal
del config.xml
cd..
cd YellowStone
del config.xml
cd..
rd "images" /S /Q
cd ..
cd bin
del *.pdb
del *.xml
del ICU4NET.dll
del icudt55.dll
del icuin55.dll
del icuio55.dll
del icule55.dll
del iculx55.dll
del icutu55.dll
del icuuc55.dll
cd ..
del .refsignored
del packages.config
del wwwroot.csproj.vspscc
del wwwroot.suppress
del wwwroot.xml
rd Controllers /S /Q
rd ApiTypes /S /Q
rd Properties /S /Q
rd Constants /S /Q
rd App_Start /S /Q
rd BusinessObjects /S /Q
rd Models /S /Q
rd DAL /S /Q
rd "My Project" /S /Q
rd obj /S /Q
rd "Service References" /S /Q
cd app_data
del *.xml
del *.csv
del version.txt
del debug.log
cd cache
del *.cache
cd ..
rd lucene /S /Q
cd ..
cd images
del affiliate_totals.png
del daily_signups.png
del daily_site_statistics.png
del monthly_activities.png
del monthly_invoices.png
del monthly_signups.png
del monthly_site_statistics.png
rem set main.js so its not in debug mode
cd ..
cd ..
fart -r -c -- "C:\sepcity\SepPortal\Trunk\compiled\js\main.js" "debugMode = true;" "debugMode = false;"
rem copy files to installs folder
cd compiled
xcopy * "C:\sepcity\SepPortal\Trunk\Installs\FTP\" /C /Y /E
xcopy * "C:\sepcity\SepPortal\Trunk\Installs\Microsoft App Gallery\SepCity\" /C /Y /E
cd install
cd sql
xcopy * "C:\sepcity\SepPortal\Trunk\Installs\Plesk\scripts\" /C /Y /E
cd ..
cd ..
rd install /S /Q
xcopy * "C:\sepcity\SepPortal\Trunk\Installs\Plesk\htdocs\" /C /Y /E
cd ..
rd compiled /S /Q
cd installs
cd "Microsoft App Gallery"
cd SepCity
cd install
cd sql
xcopy install_temp.xml "C:\sepcity\SepPortal\Trunk\Installs\Microsoft App Gallery\SepCity\app_data\" /C /Y /E
xcopy system.xml "C:\sepcity\SepPortal\Trunk\Installs\Microsoft App Gallery\SepCity\app_data\" /C /Y /E
cd..
cd..
cd..
cd..
cd..
buildplesk
rem put project back in debug mode
cd\sepcity\SepPortal\Trunk
fart -r -c -- "C:\sepcity\SepPortal\Trunk\SepCommon\functions.cs" "public bool DebugMode = false;" "public bool DebugMode = true;"
rem ZIP up install folders
cd\
cd "Program Files"
cd WinRAR
winrar.exe a -r -ep1 "C:\sepcity\SepPortal\Trunk\Installs\SepCity-3.1.zip" "C:\sepcity\SepPortal\Trunk\Installs\FTP\*"
winrar.exe a -r -x.project -x"APS Package" -ep1 "C:\sepcity\SepPortal\Trunk\Installs\SepCity-3.1-1.app.zip" "C:\sepcity\SepPortal\Trunk\Installs\Plesk\*"
winrar.exe a -r -ep1 "C:\sepcity\SepPortal\Trunk\Installs\SepCity-3.1-IIS.zip" "C:\sepcity\SepPortal\Trunk\Installs\Microsoft App Gallery\*"
