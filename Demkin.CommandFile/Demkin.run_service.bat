@echo off
echo 开始执行

sc create webApi.Service binPath="dotnet D:\\CodeRepositories\BlogAdmin\Demkin.CommandFile\webApi\Demkin.Blog.WebApi.dll --urls=http://*:5000" DisplayName="webApi.Service" start=auto depend= RpcSs/EventSystem

sc start webApi.Service

echo 关闭窗体没有影响

pause