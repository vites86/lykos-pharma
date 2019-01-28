@echo off
	setlocal
	set "CURRENT_DIR=%~dp0"
	cd "%CURRENT_DIR%"
	call xcopy "E:\Database\LykosPharmaFiles\*.*" "%CURRENT_DIR%"  /H /Y /C /R /S /I
:end