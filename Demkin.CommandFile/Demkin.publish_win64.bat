:: �ļ�����Ϊ ANSI ��ʽ����Ȼ������������

:: �ر�������������Ļ���
@echo off

:: ���ñ���Ϊ�ӳ���չ
SETLOCAL EnableDelayedExpansion
for /F "tokens=1,2 delims=#" %%a in ('"prompt #$H#$E# & echo on & for %%b in (1) do rem"') do (
  set "DEL=%%a"
)

:: ������������ı���ɫcmd���� color /? �鿴����
color 07

:: ��������Ŀ¼
set publishPath=%~dp0

cd ..

cd Demkin.Blog.WebApi

:: Begin��������Ϊ��Ҫִ�е��߼�����

::���÷������ļ�������
set publishFile="webApi"

:: /q ����ģʽ /f ǿ��ִ�� /s ������Ŀ¼ /a ��������
:: ɾ����ǰĿ¼�º�׺7z���ļ�
del "%publishPath%\*.7z*" /a /f /q

:: ɾ����ΪpublishĿ¼�µ������ļ�
del "%publishPath%\%publishFile%\*.*" /a /f /q

:: ɾ���ļ���
rd  "%publishPath%\%publishFile%" /s /q

:: ���������ָ���ļ���
dotnet publish -c Release -r win-x64 -o "%publishPath%\%publishFile%" --self-contained false

:: ���ָ����ɫ�����֣���˫����������������Ϊָ����ɫ
call:TextColor 02 "��������������ɣ���������%publishFile%Ŀ¼��" 
echo.

:: ɾ������Ҫ�������ļ�
del "%publishPath%\%publishFile%\*.pdb*" /a /f /q
del "%publishPath%\%publishFile%\appsettings.development.json*" /a /f /q
call:TextColor 01 "��ɾ������Ҫ�������ļ�" 
echo.

:: ʹ��7zѹ�����
cd /d %~dp0%

rem 7z
"D:\Program Files\7-Zip\7z.exe" a -t7z %publishFile%.7z %publishFile% -m0=BCJ -m1=LZMA:d=21 -ms -mmt

call:TextColor 06 "��ѹ������������%publishFile%.7z" 
echo.

echo ִ�����!

pause

:: �������֣�д��pause֮��
goto :eof

:TextColor
echo off
<nul set /p ".=%DEL%" > "%~2"
findstr /v /a:%1 /R "^$" "%~2" nul
del "%~2" > nul 2>&1
goto :eof