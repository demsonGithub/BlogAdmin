# 开发环境和技术栈

1. 开发工具
   Visual Studio 2022、VsCode 、SqlServer

2. 前端
   Vue3、SCSS、ElementPlus、Router、Axios、Vuex

3. 后端

   .Net 3.1、Automapper、Autofac、Sqlsugar、Jwt、Serilog
   
   

# 	项目工程结构

1. Infrastructure 基础设施

   - Demkin.Blog.Utils：公用的类库，所有的项目都可以引用
   - Demkin.Blog.Enum: 项目中所用的枚举
2. Entity 实体模型

   - Demkin.Blog.Entity: 项目的实体，对应数据库 （需引用：Utils）
   - Demkin.Blog.DTO: 包含VO，请求参数使用VO后缀，返回使用DTO后缀，方便理解
3. DataAccess 数据持久层

   - Demkin.Blog.DbAccess ：访问数据的上下文，具体内容与采用的ORM有关 （需引用：Utils）
   - Demkin.Blog.Repository :  具体的数据库操作类库，包含具体数据库的读写 （需引用：DbAccess）

   - Demkin.Blog.CodeFirst ：代码先行，不需要可以去掉这一层 （需引用：DbAccess，Entity）
4. Business 业务层

   - Demkin.Blog.IService : 业务拆分的接口 （需引用：Entity）
   - Demkin.Blog.Service : 业务的逻辑实现  （需引用：IService ，Repository）
5. Extension 扩展层

   - Demkin.Blog.Extension : 服务注册类，中间件等的扩展，为避免api层臃肿所以提出来 
6. Application 应用层

   - Demkin.Blog.WebApi :Api接口  （需引用：IService，Extension）
   - Demkin.Blog.Web : 前端页面，MVC等等
7. Test 测试层
   - Demkin.Blog.Tests 单元测试层
