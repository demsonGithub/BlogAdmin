:: 文件保存为 ANSI 格式，不然中文内容乱码

:: 关闭其他所有命令的回显
@echo off

:: 设置本地为延迟扩展
SETLOCAL EnableDelayedExpansion
for /F "tokens=1,2 delims=#" %%a in ('"prompt #$H#$E# & echo on & for %%b in (1) do rem"') do (
  set "DEL=%%a"
)

:: 设置整个窗体的背景色cmd输入 color /? 查看配置
color 07

:: 进入程序的目录
set publishPath=%~dp0

cd ..

cd Demkin.Blog.WebApi

:: Begin以下内容为需要执行的逻辑命令

::设置发布的文件夹名称
set publishFile="webApi"

:: /q 安静模式 /f 强制执行 /s 包含子目录 /a 根据属性
:: 删除当前目录下后缀7z的文件
del "%publishPath%\*.7z*" /a /f /q

:: 删除名为publish目录下的所有文件
del "%publishPath%\%publishFile%\*.*" /a /f /q

:: 删除文件夹
rd  "%publishPath%\%publishFile%" /s /q

:: 发布打包到指定文件夹
dotnet publish -c Release -r win-x64 -o "%publishPath%\%publishFile%" --self-contained false

:: 输出指定颜色的文字，用双引号括起来，后面为指定颜色
call:TextColor 02 "发布程序生成完成，保存至：%publishFile%目录下" 
echo.

:: 删除不需要发布的文件
del "%publishPath%\%publishFile%\*.pdb*" /a /f /q
del "%publishPath%\%publishFile%\appsettings.development.json*" /a /f /q
call:TextColor 01 "已删除不需要发布的文件" 
echo.

:: 使用7z压缩打包
cd /d %~dp0%

rem 7z
"D:\Program Files\7-Zip\7z.exe" a -t7z %publishFile%.7z %publishFile% -m0=BCJ -m1=LZMA:d=21 -ms -mmt

call:TextColor 06 "已压缩，保存至：%publishFile%.7z" 
echo.

echo 执行完成!

pause

:: 函数部分，写在pause之后
goto :eof

:TextColor
echo off
<nul set /p ".=%DEL%" > "%~2"
findstr /v /a:%1 /R "^$" "%~2" nul
del "%~2" > nul 2>&1
goto :eof