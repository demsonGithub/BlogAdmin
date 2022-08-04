@echo off

sc stop webApi.Service

sc start webApi.Service

echo 已重启服务

echo  .

pause