@echo off

sc stop webApi.Service

sc delete webApi.Service

echo 已成功删除服务

echo  .

pause