@echo off
echo ��ʼִ��

sc create webApi.Service binPath="dotnet D:\\CodeRepositories\BlogAdmin\Demkin.CommandFile\webApi\Demkin.Blog.WebApi.dll --urls=http://*:5000" DisplayName="webApi.Service" start=auto depend= RpcSs/EventSystem

sc start webApi.Service

echo �رմ���û��Ӱ��

pause