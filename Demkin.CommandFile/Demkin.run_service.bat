@echo off
echo ¿ªÊ¼Ö´ÐÐ

::sc create webApi.Service binPath="dotnet D:\\CodeRepositories\BlogAdmin\Demkin.CommandFile\webApi\Demkin.Blog.WebApi.dll --urls=http://*:8090" DisplayName="webApi.Service" start=auto depend= RpcSs/EventSystem

sc create webApi.Service binPath="D:\\CodeRepositories\BlogAdmin\Demkin.CommandFile\webApi\Demkin.Blog.WebApi.exe --urls=http://*:8090" DisplayName="webApi.Service" start=auto depend= RpcSs/EventSystem

sc start webApi.Service


pause