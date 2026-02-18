/*
SQLyog Ultimate v13.1.1 (64 bit)
MySQL - 10.4.32-MariaDB : Database - sql_activities
*********************************************************************
*/

/*!40101 SET NAMES utf8 */;

/*!40101 SET SQL_MODE=''*/;

/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;
CREATE DATABASE /*!32312 IF NOT EXISTS*/`sql_activities` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci */;

USE `sql_activities`;

/*Table structure for table `api_routes` */

DROP TABLE IF EXISTS `api_routes`;

CREATE TABLE `api_routes` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `path` varchar(50) NOT NULL,
  `destination_url` varchar(255) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

/*Data for the table `api_routes` */

insert  into `api_routes`(`id`,`path`,`destination_url`) values 
(1,'users','https://localhost:7218'),
(2,'products','https://localhost:7083'),
(3,'orders','https://localhost:7225');

/*Table structure for table `error_logs` */

DROP TABLE IF EXISTS `error_logs`;

CREATE TABLE `error_logs` (
  `log_id` int(11) NOT NULL AUTO_INCREMENT,
  `trace_id` varchar(50) NOT NULL,
  `error_message` text DEFAULT NULL,
  `stack_trace` text DEFAULT NULL,
  `created_at` datetime DEFAULT current_timestamp(),
  PRIMARY KEY (`log_id`)
) ENGINE=InnoDB AUTO_INCREMENT=14 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

/*Data for the table `error_logs` */

insert  into `error_logs`(`log_id`,`trace_id`,`error_message`,`stack_trace`,`created_at`) values 
(1,'0e0fcf7f-819a-4adb-9599-b53839ce9225','Unknown column \'p_seed\' in \'field list\'','MySql.Data.MySqlClient.MySqlException (0x80004005): Unknown column \'p_seed\' in \'field list\'\r\n   at MySql.Data.MySqlClient.MySqlStream.ReadPacketAsync(Boolean execAsync)\r\n   at MySql.Data.MySqlClient.NativeDriver.GetResultAsync(Int32 affectedRow, Int64 insertedId, Boolean execAsync)\r\n   at MySql.Data.MySqlClient.Driver.GetResultAsync(Int32 statementId, Int32 affectedRows, Int64 insertedId, Boolean execAsync)\r\n   at MySql.Data.MySqlClient.Driver.NextResultAsync(Int32 statementId, Boolean force, Boolean execAsync)\r\n   at MySql.Data.MySqlClient.MySqlDataReader.NextResultAsync(Boolean execAsync, CancellationToken cancellationToken)\r\n   at MySql.Data.MySqlClient.MySqlDataReader.NextResultAsync(Boolean execAsync, CancellationToken cancellationToken)\r\n   at MySql.Data.MySqlClient.MySqlCommand.ExecuteReaderAsync(CommandBehavior behavior, Boolean execAsync, CancellationToken cancellationToken)\r\n   at MySql.Data.MySqlClient.MySqlCommand.ExecuteReaderAsync(CommandBehavior behavior, Boolean execAsync, CancellationToken cancellationToken)\r\n   at MySql.Data.MySqlClient.MySqlCommand.ExecuteReaderAsync(CommandBehavior behavior, Boolean execAsync, CancellationToken cancellationToken)\r\n   at MySql.Data.MySqlClient.MySqlCommand.ExecuteNonQueryAsync(Boolean execAsync, CancellationToken cancellationToken)\r\n   at OrderingAPI.Repositories.UserRepository.AddUser(AddUserDTO user) in E:\\C# Projects\\SimpleOrderingAPI\\OrderingAPI\\OrderingAPI\\Repositories\\UserRepository.cs:line 139\r\n   at OrderingAPI.Controllers.UserController.PostUser(AddUserDTO user) in E:\\C# Projects\\SimpleOrderingAPI\\OrderingAPI\\OrderingAPI\\Controllers\\UserController.cs:line 122\r\n   at lambda_method6(Closure, Object)\r\n   at Microsoft.AspNetCore.Mvc.Infrastructure.ActionMethodExecutor.AwaitableObjectResultExecutor.Execute(ActionContext actionContext, IActionResultTypeMapper mapper, ObjectMethodExecutor executor, Object controller, Object[] arguments)\r\n   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.<InvokeActionMethodAsync>g__Awaited|12_0(ControllerActionInvoker invoker, ValueTask`1 actionResultValueTask)\r\n   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.<InvokeNextActionFilterAsync>g__Awaited|10_0(ControllerActionInvoker invoker, Task lastTask, State next, Scope scope, Object state, Boolean isCompleted)\r\n   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.Rethrow(ActionExecutedContextSealed context)\r\n   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.Next(State& next, Scope& scope, Object& state, Boolean& isCompleted)\r\n   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.<InvokeInnerFilterAsync>g__Awaited|13_0(ControllerActionInvoker invoker, Task lastTask, State next, Scope scope, Object state, Boolean isCompleted)\r\n   at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.<InvokeFilterPipelineAsync>g__Awaited|20_0(ResourceInvoker invoker, Task lastTask, State next, Scope scope, Object state, Boolean isCompleted)\r\n   at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.<InvokeAsync>g__Awaited|17_0(ResourceInvoker invoker, Task task, IDisposable scope)\r\n   at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.<InvokeAsync>g__Awaited|17_0(ResourceInvoker invoker, Task task, IDisposable scope)\r\n   at Microsoft.AspNetCore.Authorization.AuthorizationMiddleware.Invoke(HttpContext context)\r\n   at Swashbuckle.AspNetCore.SwaggerUI.SwaggerUIMiddleware.Invoke(HttpContext httpContext)\r\n   at Swashbuckle.AspNetCore.Swagger.SwaggerMiddleware.Invoke(HttpContext httpContext, ISwaggerProvider swaggerProvider)\r\n   at OrderingAPI.MiddleWare.GlobalExceptionMiddleWare.InvokeAsync(HttpContext context, IServiceProvider serviceProvider) in E:\\C# Projects\\SimpleOrderingAPI\\OrderingAPI\\OrderingAPI\\MiddleWare\\GlobalExceptionMiddleWare.cs:line 21','2026-02-11 19:13:45'),
(2,'2f137688-bf8e-47d4-8082-d6b21a05ab45','Unable to resolve service for type \'OrderingAPI.Interfaces.IAuthRepository\' while attempting to activate \'OrderingAPI.Controllers.AuthController\'.','System.InvalidOperationException: Unable to resolve service for type \'OrderingAPI.Interfaces.IAuthRepository\' while attempting to activate \'OrderingAPI.Controllers.AuthController\'.\r\n   at Microsoft.Extensions.DependencyInjection.ActivatorUtilities.ThrowHelperUnableToResolveService(Type type, Type requiredBy)\r\n   at lambda_method18(Closure, IServiceProvider, Object[])\r\n   at Microsoft.AspNetCore.Mvc.Controllers.ControllerFactoryProvider.<>c__DisplayClass6_0.<CreateControllerFactory>g__CreateController|0(ControllerContext controllerContext)\r\n   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.Next(State& next, Scope& scope, Object& state, Boolean& isCompleted)\r\n   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.InvokeInnerFilterAsync()\r\n--- End of stack trace from previous location ---\r\n   at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.<InvokeFilterPipelineAsync>g__Awaited|20_0(ResourceInvoker invoker, Task lastTask, State next, Scope scope, Object state, Boolean isCompleted)\r\n   at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.<InvokeAsync>g__Awaited|17_0(ResourceInvoker invoker, Task task, IDisposable scope)\r\n   at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.<InvokeAsync>g__Awaited|17_0(ResourceInvoker invoker, Task task, IDisposable scope)\r\n   at Microsoft.AspNetCore.Authorization.AuthorizationMiddleware.Invoke(HttpContext context)\r\n   at Swashbuckle.AspNetCore.SwaggerUI.SwaggerUIMiddleware.Invoke(HttpContext httpContext)\r\n   at Swashbuckle.AspNetCore.Swagger.SwaggerMiddleware.Invoke(HttpContext httpContext, ISwaggerProvider swaggerProvider)\r\n   at OrderingAPI.MiddleWare.GlobalExceptionMiddleWare.InvokeAsync(HttpContext context, IServiceProvider serviceProvider) in E:\\C# Projects\\SimpleOrderingAPI\\OrderingAPI\\OrderingAPI\\MiddleWare\\GlobalExceptionMiddleWare.cs:line 21','2026-02-11 19:46:14'),
(3,'4bcbc272-5b7f-4f97-9cf3-58c5311d443e','Unable to resolve service for type \'OrderingAPI.Interfaces.IAuthRepository\' while attempting to activate \'OrderingAPI.Controllers.AuthController\'.','System.InvalidOperationException: Unable to resolve service for type \'OrderingAPI.Interfaces.IAuthRepository\' while attempting to activate \'OrderingAPI.Controllers.AuthController\'.\r\n   at Microsoft.Extensions.DependencyInjection.ActivatorUtilities.ThrowHelperUnableToResolveService(Type type, Type requiredBy)\r\n   at lambda_method10(Closure, IServiceProvider, Object[])\r\n   at Microsoft.AspNetCore.Mvc.Controllers.ControllerFactoryProvider.<>c__DisplayClass6_0.<CreateControllerFactory>g__CreateController|0(ControllerContext controllerContext)\r\n   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.Next(State& next, Scope& scope, Object& state, Boolean& isCompleted)\r\n   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.InvokeInnerFilterAsync()\r\n--- End of stack trace from previous location ---\r\n   at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.<InvokeFilterPipelineAsync>g__Awaited|20_0(ResourceInvoker invoker, Task lastTask, State next, Scope scope, Object state, Boolean isCompleted)\r\n   at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.<InvokeAsync>g__Awaited|17_0(ResourceInvoker invoker, Task task, IDisposable scope)\r\n   at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.<InvokeAsync>g__Awaited|17_0(ResourceInvoker invoker, Task task, IDisposable scope)\r\n   at Microsoft.AspNetCore.Authorization.AuthorizationMiddleware.Invoke(HttpContext context)\r\n   at Swashbuckle.AspNetCore.SwaggerUI.SwaggerUIMiddleware.Invoke(HttpContext httpContext)\r\n   at Swashbuckle.AspNetCore.Swagger.SwaggerMiddleware.Invoke(HttpContext httpContext, ISwaggerProvider swaggerProvider)\r\n   at OrderingAPI.MiddleWare.GlobalExceptionMiddleWare.InvokeAsync(HttpContext context, IServiceProvider serviceProvider) in E:\\C# Projects\\SimpleOrderingAPI\\OrderingAPI\\OrderingAPI\\MiddleWare\\GlobalExceptionMiddleWare.cs:line 21','2026-02-11 19:47:55'),
(4,'9d11a12c-662a-49a0-bd06-4d0d2b2f4cb5','Unable to resolve service for type \'OrderingAPI.Interfaces.IAuthRepository\' while attempting to activate \'OrderingAPI.Controllers.AuthController\'.','System.InvalidOperationException: Unable to resolve service for type \'OrderingAPI.Interfaces.IAuthRepository\' while attempting to activate \'OrderingAPI.Controllers.AuthController\'.\r\n   at Microsoft.Extensions.DependencyInjection.ActivatorUtilities.ThrowHelperUnableToResolveService(Type type, Type requiredBy)\r\n   at lambda_method10(Closure, IServiceProvider, Object[])\r\n   at Microsoft.AspNetCore.Mvc.Controllers.ControllerFactoryProvider.<>c__DisplayClass6_0.<CreateControllerFactory>g__CreateController|0(ControllerContext controllerContext)\r\n   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.Next(State& next, Scope& scope, Object& state, Boolean& isCompleted)\r\n   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.InvokeInnerFilterAsync()\r\n--- End of stack trace from previous location ---\r\n   at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.<InvokeFilterPipelineAsync>g__Awaited|20_0(ResourceInvoker invoker, Task lastTask, State next, Scope scope, Object state, Boolean isCompleted)\r\n   at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.<InvokeAsync>g__Awaited|17_0(ResourceInvoker invoker, Task task, IDisposable scope)\r\n   at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.<InvokeAsync>g__Awaited|17_0(ResourceInvoker invoker, Task task, IDisposable scope)\r\n   at Microsoft.AspNetCore.Authorization.AuthorizationMiddleware.Invoke(HttpContext context)\r\n   at Swashbuckle.AspNetCore.SwaggerUI.SwaggerUIMiddleware.Invoke(HttpContext httpContext)\r\n   at Swashbuckle.AspNetCore.Swagger.SwaggerMiddleware.Invoke(HttpContext httpContext, ISwaggerProvider swaggerProvider)\r\n   at OrderingAPI.MiddleWare.GlobalExceptionMiddleWare.InvokeAsync(HttpContext context, IServiceProvider serviceProvider) in E:\\C# Projects\\SimpleOrderingAPI\\OrderingAPI\\OrderingAPI\\MiddleWare\\GlobalExceptionMiddleWare.cs:line 21','2026-02-11 19:54:41'),
(5,'b731f587-8516-45da-94ce-43e9b303c23c','Unable to resolve service for type \'OrderingAPI.Interfaces.IAuthRepository\' while attempting to activate \'OrderingAPI.Controllers.AuthController\'.','System.InvalidOperationException: Unable to resolve service for type \'OrderingAPI.Interfaces.IAuthRepository\' while attempting to activate \'OrderingAPI.Controllers.AuthController\'.\r\n   at Microsoft.Extensions.DependencyInjection.ActivatorUtilities.ThrowHelperUnableToResolveService(Type type, Type requiredBy)\r\n   at lambda_method10(Closure, IServiceProvider, Object[])\r\n   at Microsoft.AspNetCore.Mvc.Controllers.ControllerFactoryProvider.<>c__DisplayClass6_0.<CreateControllerFactory>g__CreateController|0(ControllerContext controllerContext)\r\n   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.Next(State& next, Scope& scope, Object& state, Boolean& isCompleted)\r\n   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.InvokeInnerFilterAsync()\r\n--- End of stack trace from previous location ---\r\n   at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.<InvokeFilterPipelineAsync>g__Awaited|20_0(ResourceInvoker invoker, Task lastTask, State next, Scope scope, Object state, Boolean isCompleted)\r\n   at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.<InvokeAsync>g__Awaited|17_0(ResourceInvoker invoker, Task task, IDisposable scope)\r\n   at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.<InvokeAsync>g__Awaited|17_0(ResourceInvoker invoker, Task task, IDisposable scope)\r\n   at Microsoft.AspNetCore.Authorization.AuthorizationMiddleware.Invoke(HttpContext context)\r\n   at Swashbuckle.AspNetCore.SwaggerUI.SwaggerUIMiddleware.Invoke(HttpContext httpContext)\r\n   at Swashbuckle.AspNetCore.Swagger.SwaggerMiddleware.Invoke(HttpContext httpContext, ISwaggerProvider swaggerProvider)\r\n   at OrderingAPI.MiddleWare.GlobalExceptionMiddleWare.InvokeAsync(HttpContext context, IServiceProvider serviceProvider) in E:\\C# Projects\\SimpleOrderingAPI\\OrderingAPI\\OrderingAPI\\MiddleWare\\GlobalExceptionMiddleWare.cs:line 21','2026-02-11 19:56:19'),
(6,'24d70759-a832-476a-a5cf-fff24e89c5a2','Unable to resolve service for type \'OrderingAPI.Interfaces.IAuthRepository\' while attempting to activate \'OrderingAPI.Controllers.AuthController\'.','System.InvalidOperationException: Unable to resolve service for type \'OrderingAPI.Interfaces.IAuthRepository\' while attempting to activate \'OrderingAPI.Controllers.AuthController\'.\r\n   at Microsoft.Extensions.DependencyInjection.ActivatorUtilities.ThrowHelperUnableToResolveService(Type type, Type requiredBy)\r\n   at lambda_method10(Closure, IServiceProvider, Object[])\r\n   at Microsoft.AspNetCore.Mvc.Controllers.ControllerFactoryProvider.<>c__DisplayClass6_0.<CreateControllerFactory>g__CreateController|0(ControllerContext controllerContext)\r\n   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.Next(State& next, Scope& scope, Object& state, Boolean& isCompleted)\r\n   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.InvokeInnerFilterAsync()\r\n--- End of stack trace from previous location ---\r\n   at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.<InvokeFilterPipelineAsync>g__Awaited|20_0(ResourceInvoker invoker, Task lastTask, State next, Scope scope, Object state, Boolean isCompleted)\r\n   at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.<InvokeAsync>g__Awaited|17_0(ResourceInvoker invoker, Task task, IDisposable scope)\r\n   at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.<InvokeAsync>g__Awaited|17_0(ResourceInvoker invoker, Task task, IDisposable scope)\r\n   at Microsoft.AspNetCore.Authorization.AuthorizationMiddleware.Invoke(HttpContext context)\r\n   at Swashbuckle.AspNetCore.SwaggerUI.SwaggerUIMiddleware.Invoke(HttpContext httpContext)\r\n   at Swashbuckle.AspNetCore.Swagger.SwaggerMiddleware.Invoke(HttpContext httpContext, ISwaggerProvider swaggerProvider)\r\n   at OrderingAPI.MiddleWare.GlobalExceptionMiddleWare.InvokeAsync(HttpContext context, IServiceProvider serviceProvider) in E:\\C# Projects\\SimpleOrderingAPI\\OrderingAPI\\OrderingAPI\\MiddleWare\\GlobalExceptionMiddleWare.cs:line 21','2026-02-11 19:57:41'),
(7,'9657fd20-d371-49a9-a069-fe660eca839a','IDX10720: Unable to create KeyedHashAlgorithm for algorithm \'http://www.w3.org/2001/04/xmldsig-more#hmac-sha512\', the key size must be greater than: \'512\' bits, key has \'240\' bits. (Parameter \'keyBytes\')','System.ArgumentOutOfRangeException: IDX10720: Unable to create KeyedHashAlgorithm for algorithm \'http://www.w3.org/2001/04/xmldsig-more#hmac-sha512\', the key size must be greater than: \'512\' bits, key has \'240\' bits. (Parameter \'keyBytes\')\r\n   at Microsoft.IdentityModel.Tokens.CryptoProviderFactory.ValidateKeySize(Byte[] keyBytes, String algorithm, Int32 expectedNumberOfBytes)\r\n   at Microsoft.IdentityModel.Tokens.CryptoProviderFactory.CreateKeyedHashAlgorithm(Byte[] keyBytes, String algorithm)\r\n   at Microsoft.IdentityModel.Tokens.SymmetricSignatureProvider.CreateKeyedHashAlgorithm()\r\n   at Microsoft.IdentityModel.Tokens.DisposableObjectPool`1.CreateInstance()\r\n   at Microsoft.IdentityModel.Tokens.DisposableObjectPool`1.Allocate()\r\n   at Microsoft.IdentityModel.Tokens.SymmetricSignatureProvider.GetKeyedHashAlgorithm(Byte[] keyBytes, String algorithm)\r\n   at Microsoft.IdentityModel.Tokens.SymmetricSignatureProvider.Sign(Byte[] input)\r\n   at Microsoft.IdentityModel.JsonWebTokens.JwtTokenUtilities.CreateEncodedSignature(String input, SigningCredentials signingCredentials)\r\n   at System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler.CreateJwtSecurityTokenPrivate(SecurityTokenDescriptor tokenDescriptor)\r\n   at System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler.CreateToken(SecurityTokenDescriptor tokenDescriptor)\r\n   at OrderingAPI.Repositories.AuthRepository.CreateToken(String userID, String email, String role) in E:\\C# Projects\\SimpleOrderingAPI\\OrderingAPI\\OrderingAPI\\Repositories\\AuthRepository.cs:line 45\r\n   at OrderingAPI.Repositories.AuthRepository.Login(LoginRequestDTO login) in E:\\C# Projects\\SimpleOrderingAPI\\OrderingAPI\\OrderingAPI\\Repositories\\AuthRepository.cs:line 64\r\n   at OrderingAPI.Controllers.AuthController.Login(LoginRequestDTO login) in E:\\C# Projects\\SimpleOrderingAPI\\OrderingAPI\\OrderingAPI\\Controllers\\AuthController.cs:line 34\r\n   at lambda_method6(Closure, Object)\r\n   at Microsoft.AspNetCore.Mvc.Infrastructure.ActionMethodExecutor.AwaitableObjectResultExecutor.Execute(ActionContext actionContext, IActionResultTypeMapper mapper, ObjectMethodExecutor executor, Object controller, Object[] arguments)\r\n   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.<InvokeActionMethodAsync>g__Awaited|12_0(ControllerActionInvoker invoker, ValueTask`1 actionResultValueTask)\r\n   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.<InvokeNextActionFilterAsync>g__Awaited|10_0(ControllerActionInvoker invoker, Task lastTask, State next, Scope scope, Object state, Boolean isCompleted)\r\n   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.Rethrow(ActionExecutedContextSealed context)\r\n   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.Next(State& next, Scope& scope, Object& state, Boolean& isCompleted)\r\n   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.<InvokeInnerFilterAsync>g__Awaited|13_0(ControllerActionInvoker invoker, Task lastTask, State next, Scope scope, Object state, Boolean isCompleted)\r\n   at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.<InvokeFilterPipelineAsync>g__Awaited|20_0(ResourceInvoker invoker, Task lastTask, State next, Scope scope, Object state, Boolean isCompleted)\r\n   at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.<InvokeAsync>g__Awaited|17_0(ResourceInvoker invoker, Task task, IDisposable scope)\r\n   at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.<InvokeAsync>g__Awaited|17_0(ResourceInvoker invoker, Task task, IDisposable scope)\r\n   at Microsoft.AspNetCore.Authorization.AuthorizationMiddleware.Invoke(HttpContext context)\r\n   at Swashbuckle.AspNetCore.SwaggerUI.SwaggerUIMiddleware.Invoke(HttpContext httpContext)\r\n   at Swashbuckle.AspNetCore.Swagger.SwaggerMiddleware.Invoke(HttpContext httpContext, ISwaggerProvider swaggerProvider)\r\n   at OrderingAPI.MiddleWare.GlobalExceptionMiddleWare.InvokeAsync(HttpContext context, IServiceProvider serviceProvider) in E:\\C# Projects\\SimpleOrderingAPI\\OrderingAPI\\OrderingAPI\\MiddleWare\\GlobalExceptionMiddleWare.cs:line 21','2026-02-12 02:32:56'),
(8,'395d297a-4037-4728-a183-c8ace36879b7','Parameter \'p_role\' not found in the collection.','System.ArgumentException: Parameter \'p_role\' not found in the collection.\r\n   at MySql.Data.MySqlClient.MySqlParameterCollection.GetParameterFlexible(String parameterName, Boolean throwOnNotFound)\r\n   at MySql.Data.MySqlClient.StoredProcedure.GetAndFixParameter(String spName, MySqlSchemaRow param, Boolean realAsFloat, MySqlParameter returnParameter)\r\n   at MySql.Data.MySqlClient.StoredProcedure.CheckParametersAsync(String spName, Boolean execAsync)\r\n   at MySql.Data.MySqlClient.StoredProcedure.Resolve(Boolean preparing)\r\n   at MySql.Data.MySqlClient.MySqlCommand.ExecuteReaderAsync(CommandBehavior behavior, Boolean execAsync, CancellationToken cancellationToken)\r\n   at MySql.Data.MySqlClient.MySqlCommand.ExecuteNonQueryAsync(Boolean execAsync, CancellationToken cancellationToken)\r\n   at OrderingAPI.Repositories.UserRepository.UpdateUser(Int32 id, UpdateUserDTO user) in E:\\C# Projects\\SimpleOrderingAPI\\OrderingAPI\\OrderingAPI\\Repositories\\UserRepository.cs:line 166\r\n   at OrderingAPI.Controllers.UserController.UpdateUser(Int32 id, UpdateUserDTO user) in E:\\C# Projects\\SimpleOrderingAPI\\OrderingAPI\\OrderingAPI\\Controllers\\UserController.cs:line 172\r\n   at lambda_method14(Closure, Object)\r\n   at Microsoft.AspNetCore.Mvc.Infrastructure.ActionMethodExecutor.AwaitableObjectResultExecutor.Execute(ActionContext actionContext, IActionResultTypeMapper mapper, ObjectMethodExecutor executor, Object controller, Object[] arguments)\r\n   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.<InvokeActionMethodAsync>g__Awaited|12_0(ControllerActionInvoker invoker, ValueTask`1 actionResultValueTask)\r\n   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.<InvokeNextActionFilterAsync>g__Awaited|10_0(ControllerActionInvoker invoker, Task lastTask, State next, Scope scope, Object state, Boolean isCompleted)\r\n   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.Rethrow(ActionExecutedContextSealed context)\r\n   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.Next(State& next, Scope& scope, Object& state, Boolean& isCompleted)\r\n   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.<InvokeInnerFilterAsync>g__Awaited|13_0(ControllerActionInvoker invoker, Task lastTask, State next, Scope scope, Object state, Boolean isCompleted)\r\n   at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.<InvokeFilterPipelineAsync>g__Awaited|20_0(ResourceInvoker invoker, Task lastTask, State next, Scope scope, Object state, Boolean isCompleted)\r\n   at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.<InvokeAsync>g__Awaited|17_0(ResourceInvoker invoker, Task task, IDisposable scope)\r\n   at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.<InvokeAsync>g__Awaited|17_0(ResourceInvoker invoker, Task task, IDisposable scope)\r\n   at Microsoft.AspNetCore.Authorization.AuthorizationMiddleware.Invoke(HttpContext context)\r\n   at Microsoft.AspNetCore.Authentication.AuthenticationMiddleware.Invoke(HttpContext context)\r\n   at Swashbuckle.AspNetCore.SwaggerUI.SwaggerUIMiddleware.Invoke(HttpContext httpContext)\r\n   at Swashbuckle.AspNetCore.Swagger.SwaggerMiddleware.Invoke(HttpContext httpContext, ISwaggerProvider swaggerProvider)\r\n   at OrderingAPI.MiddleWare.GlobalExceptionMiddleWare.InvokeAsync(HttpContext context, IServiceProvider serviceProvider) in E:\\C# Projects\\SimpleOrderingAPI\\OrderingAPI\\OrderingAPI\\MiddleWare\\GlobalExceptionMiddleWare.cs:line 21','2026-02-13 00:12:05'),
(9,'4bc01c0c-a442-465f-a6cc-c9b66c7a38ca','Connection must be valid and open.','System.InvalidOperationException: Connection must be valid and open.\r\n   at MySql.Data.MySqlClient.MySqlCommand.Throw(Exception ex)\r\n   at MySql.Data.MySqlClient.MySqlCommand.CheckState()\r\n   at MySql.Data.MySqlClient.MySqlCommand.ExecuteReaderAsync(CommandBehavior behavior, Boolean execAsync, CancellationToken cancellationToken)\r\n   at MySql.Data.MySqlClient.MySqlCommand.ExecuteNonQueryAsync(Boolean execAsync, CancellationToken cancellationToken)\r\n   at OrderingAPI.Repositories.AuthRepository.GenerateRefreshToken(Int32 userID) in E:\\C# Projects\\SimpleOrderingAPI\\OrderingAPI\\OrderingAPI\\Repositories\\AuthRepository.cs:line 43\r\n   at OrderingAPI.Repositories.AuthRepository.Login(LoginRequestDTO login) in E:\\C# Projects\\SimpleOrderingAPI\\OrderingAPI\\OrderingAPI\\Repositories\\AuthRepository.cs:line 94\r\n   at OrderingAPI.Controllers.AuthController.Login(LoginRequestDTO login) in E:\\C# Projects\\SimpleOrderingAPI\\OrderingAPI\\OrderingAPI\\Controllers\\AuthController.cs:line 34\r\n   at lambda_method6(Closure, Object)\r\n   at Microsoft.AspNetCore.Mvc.Infrastructure.ActionMethodExecutor.AwaitableObjectResultExecutor.Execute(ActionContext actionContext, IActionResultTypeMapper mapper, ObjectMethodExecutor executor, Object controller, Object[] arguments)\r\n   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.<InvokeActionMethodAsync>g__Awaited|12_0(ControllerActionInvoker invoker, ValueTask`1 actionResultValueTask)\r\n   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.<InvokeNextActionFilterAsync>g__Awaited|10_0(ControllerActionInvoker invoker, Task lastTask, State next, Scope scope, Object state, Boolean isCompleted)\r\n   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.Rethrow(ActionExecutedContextSealed context)\r\n   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.Next(State& next, Scope& scope, Object& state, Boolean& isCompleted)\r\n   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.<InvokeInnerFilterAsync>g__Awaited|13_0(ControllerActionInvoker invoker, Task lastTask, State next, Scope scope, Object state, Boolean isCompleted)\r\n   at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.<InvokeFilterPipelineAsync>g__Awaited|20_0(ResourceInvoker invoker, Task lastTask, State next, Scope scope, Object state, Boolean isCompleted)\r\n   at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.<InvokeAsync>g__Awaited|17_0(ResourceInvoker invoker, Task task, IDisposable scope)\r\n   at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.<InvokeAsync>g__Awaited|17_0(ResourceInvoker invoker, Task task, IDisposable scope)\r\n   at Microsoft.AspNetCore.Authorization.AuthorizationMiddleware.Invoke(HttpContext context)\r\n   at Microsoft.AspNetCore.Authentication.AuthenticationMiddleware.Invoke(HttpContext context)\r\n   at Swashbuckle.AspNetCore.SwaggerUI.SwaggerUIMiddleware.Invoke(HttpContext httpContext)\r\n   at Swashbuckle.AspNetCore.Swagger.SwaggerMiddleware.Invoke(HttpContext httpContext, ISwaggerProvider swaggerProvider)\r\n   at OrderingAPI.MiddleWare.GlobalExceptionMiddleWare.InvokeAsync(HttpContext context, IServiceProvider serviceProvider) in E:\\C# Projects\\SimpleOrderingAPI\\OrderingAPI\\OrderingAPI\\MiddleWare\\GlobalExceptionMiddleWare.cs:line 21','2026-02-13 00:47:18'),
(10,'66ae30ff-9874-408c-a928-a541862f1475','Could not find specified column in results: email','System.IndexOutOfRangeException: Could not find specified column in results: email\r\n   at MySql.Data.MySqlClient.ResultSet.GetOrdinal(String name)\r\n   at MySql.Data.MySqlClient.MySqlDataReader.GetOrdinal(String name)\r\n   at MySql.Data.MySqlClient.MySqlDataReader.get_Item(String name)\r\n   at OrderingAPI.Repositories.AuthRepository.Login(LoginRequestDTO login) in E:\\C# Projects\\SimpleOrderingAPI\\OrderingAPI\\OrderingAPI\\Repositories\\AuthRepository.cs:line 152\r\n   at OrderingAPI.Controllers.AuthController.Login(LoginRequestDTO login) in E:\\C# Projects\\SimpleOrderingAPI\\OrderingAPI\\OrderingAPI\\Controllers\\AuthController.cs:line 34\r\n   at lambda_method6(Closure, Object)\r\n   at Microsoft.AspNetCore.Mvc.Infrastructure.ActionMethodExecutor.AwaitableObjectResultExecutor.Execute(ActionContext actionContext, IActionResultTypeMapper mapper, ObjectMethodExecutor executor, Object controller, Object[] arguments)\r\n   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.<InvokeActionMethodAsync>g__Awaited|12_0(ControllerActionInvoker invoker, ValueTask`1 actionResultValueTask)\r\n   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.<InvokeNextActionFilterAsync>g__Awaited|10_0(ControllerActionInvoker invoker, Task lastTask, State next, Scope scope, Object state, Boolean isCompleted)\r\n   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.Rethrow(ActionExecutedContextSealed context)\r\n   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.Next(State& next, Scope& scope, Object& state, Boolean& isCompleted)\r\n   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.<InvokeInnerFilterAsync>g__Awaited|13_0(ControllerActionInvoker invoker, Task lastTask, State next, Scope scope, Object state, Boolean isCompleted)\r\n   at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.<InvokeFilterPipelineAsync>g__Awaited|20_0(ResourceInvoker invoker, Task lastTask, State next, Scope scope, Object state, Boolean isCompleted)\r\n   at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.<InvokeAsync>g__Awaited|17_0(ResourceInvoker invoker, Task task, IDisposable scope)\r\n   at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.<InvokeAsync>g__Awaited|17_0(ResourceInvoker invoker, Task task, IDisposable scope)\r\n   at Microsoft.AspNetCore.Authorization.AuthorizationMiddleware.Invoke(HttpContext context)\r\n   at Microsoft.AspNetCore.Authentication.AuthenticationMiddleware.Invoke(HttpContext context)\r\n   at Swashbuckle.AspNetCore.SwaggerUI.SwaggerUIMiddleware.Invoke(HttpContext httpContext)\r\n   at Swashbuckle.AspNetCore.Swagger.SwaggerMiddleware.Invoke(HttpContext httpContext, ISwaggerProvider swaggerProvider)\r\n   at OrderingAPI.MiddleWare.GlobalExceptionMiddleWare.InvokeAsync(HttpContext context, IServiceProvider serviceProvider) in E:\\C# Projects\\SimpleOrderingAPI\\OrderingAPI\\OrderingAPI\\MiddleWare\\GlobalExceptionMiddleWare.cs:line 21','2026-02-13 01:48:33'),
(11,'ff88966c-29ad-4c21-8d95-a8d2b1e7f04f','You have an error in your SQL syntax; check the manual that corresponds to your MariaDB server version for the right syntax to use near \'SET revoked = NOW() WHERE token = \'w4YkRhvSZS5tQ42hQpE8cWQsFYjT18rL9zzp8qpcj5...\' at line 1','MySql.Data.MySqlClient.MySqlException (0x80004005): You have an error in your SQL syntax; check the manual that corresponds to your MariaDB server version for the right syntax to use near \'SET revoked = NOW() WHERE token = \'w4YkRhvSZS5tQ42hQpE8cWQsFYjT18rL9zzp8qpcj5...\' at line 1\r\n   at MySql.Data.MySqlClient.MySqlStream.ReadPacketAsync(Boolean execAsync)\r\n   at MySql.Data.MySqlClient.NativeDriver.GetResultAsync(Int32 affectedRow, Int64 insertedId, Boolean execAsync)\r\n   at MySql.Data.MySqlClient.Driver.GetResultAsync(Int32 statementId, Int32 affectedRows, Int64 insertedId, Boolean execAsync)\r\n   at MySql.Data.MySqlClient.Driver.NextResultAsync(Int32 statementId, Boolean force, Boolean execAsync)\r\n   at MySql.Data.MySqlClient.MySqlDataReader.NextResultAsync(Boolean execAsync, CancellationToken cancellationToken)\r\n   at MySql.Data.MySqlClient.MySqlDataReader.NextResultAsync(Boolean execAsync, CancellationToken cancellationToken)\r\n   at MySql.Data.MySqlClient.MySqlCommand.ExecuteReaderAsync(CommandBehavior behavior, Boolean execAsync, CancellationToken cancellationToken)\r\n   at MySql.Data.MySqlClient.MySqlCommand.ExecuteReaderAsync(CommandBehavior behavior, Boolean execAsync, CancellationToken cancellationToken)\r\n   at MySql.Data.MySqlClient.MySqlCommand.ExecuteReaderAsync(CommandBehavior behavior, Boolean execAsync, CancellationToken cancellationToken)\r\n   at MySql.Data.MySqlClient.MySqlCommand.ExecuteNonQueryAsync(Boolean execAsync, CancellationToken cancellationToken)\r\n   at OrderingAPI.Repositories.AuthRepository.RevokeRefreshToken(String token) in E:\\C# Projects\\SimpleOrderingAPI\\OrderingAPI\\OrderingAPI\\Repositories\\AuthRepository.cs:line 136\r\n   at OrderingAPI.Repositories.AuthRepository.RefreshToken(RefreshTokenRequestDTO request) in E:\\C# Projects\\SimpleOrderingAPI\\OrderingAPI\\OrderingAPI\\Repositories\\AuthRepository.cs:line 111\r\n   at OrderingAPI.Controllers.AuthController.RefreshToken(RefreshTokenRequestDTO request) in E:\\C# Projects\\SimpleOrderingAPI\\OrderingAPI\\OrderingAPI\\Controllers\\AuthController.cs:line 66\r\n   at lambda_method14(Closure, Object)\r\n   at Microsoft.AspNetCore.Mvc.Infrastructure.ActionMethodExecutor.AwaitableObjectResultExecutor.Execute(ActionContext actionContext, IActionResultTypeMapper mapper, ObjectMethodExecutor executor, Object controller, Object[] arguments)\r\n   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.<InvokeActionMethodAsync>g__Awaited|12_0(ControllerActionInvoker invoker, ValueTask`1 actionResultValueTask)\r\n   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.<InvokeNextActionFilterAsync>g__Awaited|10_0(ControllerActionInvoker invoker, Task lastTask, State next, Scope scope, Object state, Boolean isCompleted)\r\n   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.Rethrow(ActionExecutedContextSealed context)\r\n   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.Next(State& next, Scope& scope, Object& state, Boolean& isCompleted)\r\n   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.<InvokeInnerFilterAsync>g__Awaited|13_0(ControllerActionInvoker invoker, Task lastTask, State next, Scope scope, Object state, Boolean isCompleted)\r\n   at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.<InvokeFilterPipelineAsync>g__Awaited|20_0(ResourceInvoker invoker, Task lastTask, State next, Scope scope, Object state, Boolean isCompleted)\r\n   at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.<InvokeAsync>g__Awaited|17_0(ResourceInvoker invoker, Task task, IDisposable scope)\r\n   at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.<InvokeAsync>g__Awaited|17_0(ResourceInvoker invoker, Task task, IDisposable scope)\r\n   at Microsoft.AspNetCore.Authorization.AuthorizationMiddleware.Invoke(HttpContext context)\r\n   at Microsoft.AspNetCore.Authentication.AuthenticationMiddleware.Invoke(HttpContext context)\r\n   at Swashbuckle.AspNetCore.SwaggerUI.SwaggerUIMiddleware.Invoke(HttpContext httpContext)\r\n   at Swashbuckle.AspNetCore.Swagger.SwaggerMiddleware.Invoke(HttpContext httpContext, ISwaggerProvider swaggerProvider)\r\n   at OrderingAPI.MiddleWare.GlobalExceptionMiddleWare.InvokeAsync(HttpContext context, IServiceProvider serviceProvider) in E:\\C# Projects\\SimpleOrderingAPI\\OrderingAPI\\OrderingAPI\\MiddleWare\\GlobalExceptionMiddleWare.cs:line 21','2026-02-14 00:34:20'),
(12,'a57175e7-04c7-47d0-8e3b-e095481e3976','Conflicting method/path combination \"POST api/auth/login\" for actions - UserService.Controllers.AuthController.Login (UserService),OrderingAPI.Controllers.AuthController.Login (OrderingAPI). Actions require a unique method/path combination for Swagger/OpenAPI 3.0. Use ConflictingActionsResolver as a workaround','Swashbuckle.AspNetCore.SwaggerGen.SwaggerGeneratorException: Conflicting method/path combination \"POST api/auth/login\" for actions - UserService.Controllers.AuthController.Login (UserService),OrderingAPI.Controllers.AuthController.Login (OrderingAPI). Actions require a unique method/path combination for Swagger/OpenAPI 3.0. Use ConflictingActionsResolver as a workaround\r\n   at Swashbuckle.AspNetCore.SwaggerGen.SwaggerGenerator.GenerateOperations(IEnumerable`1 apiDescriptions, SchemaRepository schemaRepository)\r\n   at Swashbuckle.AspNetCore.SwaggerGen.SwaggerGenerator.GeneratePaths(IEnumerable`1 apiDescriptions, SchemaRepository schemaRepository)\r\n   at Swashbuckle.AspNetCore.SwaggerGen.SwaggerGenerator.GetSwaggerDocumentWithoutFilters(String documentName, String host, String basePath)\r\n   at Swashbuckle.AspNetCore.SwaggerGen.SwaggerGenerator.GetSwaggerAsync(String documentName, String host, String basePath)\r\n   at Swashbuckle.AspNetCore.Swagger.SwaggerMiddleware.Invoke(HttpContext httpContext, ISwaggerProvider swaggerProvider)\r\n   at OrderingAPI.MiddleWare.GlobalExceptionMiddleWare.InvokeAsync(HttpContext context, IServiceProvider serviceProvider) in E:\\C# Projects\\SimpleOrderingAPI\\OrderingAPI\\OrderingAPI\\MiddleWare\\GlobalExceptionMiddleWare.cs:line 21','2026-02-17 00:21:18'),
(13,'993840a7-b555-473d-a47b-8feb7cbafea4','No authenticationScheme was specified, and there was no DefaultChallengeScheme found. The default schemes can be set using either AddAuthentication(string defaultScheme) or AddAuthentication(Action<AuthenticationOptions> configureOptions).','System.InvalidOperationException: No authenticationScheme was specified, and there was no DefaultChallengeScheme found. The default schemes can be set using either AddAuthentication(string defaultScheme) or AddAuthentication(Action<AuthenticationOptions> configureOptions).\r\n   at Microsoft.AspNetCore.Authentication.AuthenticationService.ChallengeAsync(HttpContext context, String scheme, AuthenticationProperties properties)\r\n   at Microsoft.AspNetCore.Authorization.Policy.AuthorizationMiddlewareResultHandler.<>c__DisplayClass0_0.<<HandleAsync>g__Handle|0>d.MoveNext()\r\n--- End of stack trace from previous location ---\r\n   at Microsoft.AspNetCore.Authorization.AuthorizationMiddleware.Invoke(HttpContext context)\r\n   at Microsoft.AspNetCore.Authentication.AuthenticationMiddleware.Invoke(HttpContext context)\r\n   at Swashbuckle.AspNetCore.SwaggerUI.SwaggerUIMiddleware.Invoke(HttpContext httpContext)\r\n   at Swashbuckle.AspNetCore.Swagger.SwaggerMiddleware.Invoke(HttpContext httpContext, ISwaggerProvider swaggerProvider)\r\n   at OrderingAPI.MiddleWare.GlobalExceptionMiddleWare.InvokeAsync(HttpContext context, IServiceProvider serviceProvider) in E:\\C# Projects\\SimpleOrderingAPI\\OrderingAPI\\OrderingAPI\\MiddleWare\\GlobalExceptionMiddleWare.cs:line 21','2026-02-17 02:04:37');

/*Table structure for table `order_items` */

DROP TABLE IF EXISTS `order_items`;

CREATE TABLE `order_items` (
  `order_item_id` int(11) NOT NULL AUTO_INCREMENT,
  `order_id` int(11) DEFAULT NULL,
  `product_id` int(11) DEFAULT NULL,
  `quantity` int(11) DEFAULT NULL,
  `price` decimal(10,2) DEFAULT NULL,
  PRIMARY KEY (`order_item_id`),
  KEY `order_id` (`order_id`),
  KEY `product_id` (`product_id`),
  CONSTRAINT `order_items_ibfk_1` FOREIGN KEY (`order_id`) REFERENCES `orders` (`order_id`),
  CONSTRAINT `order_items_ibfk_2` FOREIGN KEY (`product_id`) REFERENCES `products` (`product_id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

/*Data for the table `order_items` */

insert  into `order_items`(`order_item_id`,`order_id`,`product_id`,`quantity`,`price`) values 
(1,1,1,1,40000.00),
(2,1,2,2,800.00),
(3,2,3,1,1500.00),
(4,2,4,1,12000.00),
(5,1,1,4,55.00);

/*Table structure for table `orders` */

DROP TABLE IF EXISTS `orders`;

CREATE TABLE `orders` (
  `order_id` int(11) NOT NULL AUTO_INCREMENT,
  `user_id` int(11) DEFAULT NULL,
  `order_date` datetime DEFAULT NULL,
  `status` varchar(20) DEFAULT NULL,
  PRIMARY KEY (`order_id`),
  KEY `user_id` (`user_id`),
  CONSTRAINT `orders_ibfk_1` FOREIGN KEY (`user_id`) REFERENCES `users` (`user_id`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

/*Data for the table `orders` */

insert  into `orders`(`order_id`,`user_id`,`order_date`,`status`) values 
(1,1,'2026-02-05 03:25:31','COMPLETED'),
(2,2,'2026-02-05 03:25:31','PENDING'),
(3,2,'2024-02-02 00:00:00','completed'),
(4,1,'2026-02-05 03:25:31','CANCELLED'),
(5,1,'2026-02-09 21:33:10','PENDING'),
(6,1,'2026-02-09 21:38:55','PENDING');

/*Table structure for table `payments` */

DROP TABLE IF EXISTS `payments`;

CREATE TABLE `payments` (
  `payment_id` int(11) NOT NULL AUTO_INCREMENT,
  `order_id` int(11) DEFAULT NULL,
  `payment_method` varchar(30) DEFAULT NULL,
  `amount` decimal(10,2) DEFAULT NULL,
  `payment_date` datetime DEFAULT NULL,
  PRIMARY KEY (`payment_id`),
  KEY `order_id` (`order_id`),
  CONSTRAINT `payments_ibfk_1` FOREIGN KEY (`order_id`) REFERENCES `orders` (`order_id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

/*Data for the table `payments` */

insert  into `payments`(`payment_id`,`order_id`,`payment_method`,`amount`,`payment_date`) values 
(1,1,'Credit Card',38000.00,'2024-02-01 00:00:00'),
(2,2,'GCash',13500.00,'2026-02-05 03:26:18');

/*Table structure for table `price_history` */

DROP TABLE IF EXISTS `price_history`;

CREATE TABLE `price_history` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `product_id` int(11) NOT NULL,
  `old_price` decimal(10,2) NOT NULL,
  `new_price` decimal(10,2) NOT NULL,
  `changed_at` datetime DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `price_history->products` (`product_id`),
  CONSTRAINT `price_history->products` FOREIGN KEY (`product_id`) REFERENCES `products` (`product_id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

/*Data for the table `price_history` */

insert  into `price_history`(`id`,`product_id`,`old_price`,`new_price`,`changed_at`) values 
(1,1,0.00,26000.00,'2026-02-10 19:58:31');

/*Table structure for table `products` */

DROP TABLE IF EXISTS `products`;

CREATE TABLE `products` (
  `product_id` int(11) NOT NULL AUTO_INCREMENT,
  `product_name` varchar(100) DEFAULT NULL,
  `price` decimal(10,2) DEFAULT NULL,
  `stock` int(11) DEFAULT NULL,
  PRIMARY KEY (`product_id`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

/*Data for the table `products` */

insert  into `products`(`product_id`,`product_name`,`price`,`stock`) values 
(1,'Laptops',26000.00,0),
(2,'Wireless Mouse',800.00,50),
(3,'Mouse',0.00,100),
(4,'Keyboard',1500.00,-2),
(5,'Monitor',12000.00,0),
(6,'Pc Case',2000.00,10),
(7,'GPU',239.40,5),
(8,'Web Cam',23.00,4),
(9,'string',0.00,0),
(10,'Sample',0.00,0);

/*Table structure for table `user_sessions` */

DROP TABLE IF EXISTS `user_sessions`;

CREATE TABLE `user_sessions` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `user_id` int(11) NOT NULL,
  `token` varchar(255) NOT NULL,
  `expires` datetime NOT NULL,
  `created` datetime DEFAULT current_timestamp(),
  PRIMARY KEY (`id`),
  KEY `user_sessions->users` (`user_id`),
  CONSTRAINT `user_sessions->users` FOREIGN KEY (`user_id`) REFERENCES `users` (`user_id`)
) ENGINE=InnoDB AUTO_INCREMENT=35 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

/*Data for the table `user_sessions` */

insert  into `user_sessions`(`id`,`user_id`,`token`,`expires`,`created`) values 
(34,7,'HPn1QBA3peoYmnecQGlZ1Pg7X3P/cDx1Dp9EeUjExieTvYSIH4tqFLAKm5+xf3iXO3Bu08Gm/6k/jw7n/YZ2sw==','2026-02-18 03:03:58','2026-02-18 02:43:58');

/*Table structure for table `users` */

DROP TABLE IF EXISTS `users`;

CREATE TABLE `users` (
  `user_id` int(11) NOT NULL AUTO_INCREMENT,
  `full_name` varchar(100) DEFAULT NULL,
  `email` varchar(100) DEFAULT NULL,
  `password` varchar(255) NOT NULL,
  `salt` varchar(255) NOT NULL,
  `role` varchar(25) NOT NULL,
  `created_at` datetime DEFAULT NULL,
  `status` int(11) NOT NULL DEFAULT 1,
  PRIMARY KEY (`user_id`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

/*Data for the table `users` */

insert  into `users`(`user_id`,`full_name`,`email`,`password`,`salt`,`role`,`created_at`,`status`) values 
(1,'Jun Dela Cruz','juan@email.com','5a8384aeec10f47df0d94c924b5e40f65de0a44f17e2bb5d66ed33be0a966cc7','Fzntsi0s+0aVFLE4BUeLqQ==','Customer','2026-02-05 03:25:25',1),
(2,'Mari S. Santos','marisantos@gmail.com','5a8384aeec10f47df0d94c924b5e40f65de0a44f17e2bb5d66ed33be0a966cc7','Fzntsi0s+0aVFLE4BUeLqQ==','Customer','2026-02-05 03:25:25',1),
(3,'Pedro Reyes','pedro@email.com','5a8384aeec10f47df0d94c924b5e40f65de0a44f17e2bb5d66ed33be0a966cc7','Fzntsi0s+0aVFLE4BUeLqQ==','Customer','2026-02-05 03:25:25',1),
(4,'juan dela cruz','juan@email.com','5a8384aeec10f47df0d94c924b5e40f65de0a44f17e2bb5d66ed33be0a966cc7','Fzntsi0s+0aVFLE4BUeLqQ==','Customer','2024-01-11 00:00:00',1),
(5,'Maria Santos',NULL,'5a8384aeec10f47df0d94c924b5e40f65de0a44f17e2bb5d66ed33be0a966cc7','Fzntsi0s+0aVFLE4BUeLqQ==','Customer','2024-01-12 00:00:00',1),
(6,'Pedro Reyes','PEDRO@EMAIL.COM','5a8384aeec10f47df0d94c924b5e40f65de0a44f17e2bb5d66ed33be0a966cc7','Fzntsi0s+0aVFLE4BUeLqQ==','Customer','2024-01-13 00:00:00',1),
(7,'Jc','jcbcalayag@gmail.com','99aa503404293169208a89b7d6a2a9a313af30bb70c9d91271efa8b141e246a0','4zNqzh0uNtdB473dzIQYog==','Admin','2026-02-09 15:53:45',1),
(8,'Mari Nicole Medel','medel@gmail.com','5a8384aeec10f47df0d94c924b5e40f65de0a44f17e2bb5d66ed33be0a966cc7','Fzntsi0s+0aVFLE4BUeLqQ==','Admin','2026-02-09 21:38:05',1),
(9,'Reb Cruz','reb123@gmail.com','5a8384aeec10f47df0d94c924b5e40f65de0a44f17e2bb5d66ed33be0a966cc7','Fzntsi0s+0aVFLE4BUeLqQ==','Admin','2026-02-11 19:16:29',1);

/* Trigger structure for table `products` */

DELIMITER $$

/*!50003 DROP TRIGGER*//*!50032 IF EXISTS */ /*!50003 `trg_products_before_update_log_price` */$$

/*!50003 CREATE */ /*!50017 DEFINER = 'root'@'localhost' */ /*!50003 TRIGGER `trg_products_before_update_log_price` BEFORE UPDATE ON `products` FOR EACH ROW 
BEGIN
	IF OLD.price <> NEW.price THEN
		INSERT INTO price_history(
			product_id,
			old_price,
			new_price,
			changed_at)
		VALUES(OLD.product_id,
			OLD.price,
			NEW.price,
			NOW());
	END IF;
END */$$


DELIMITER ;

/* Procedure structure for procedure `AddOrder` */

/*!50003 DROP PROCEDURE IF EXISTS  `AddOrder` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `AddOrder`(
	IN p_user_id INT,
	IN p_status VARCHAR(20),
	OUT p_result INT)
BEGIN
	IF NOT EXISTS(SELECT 1 FROM users WHERE user_id = p_user_id) THEN
		SET p_result = 0;
	ELSE
		INSERT INTO orders(user_id, order_date, status)
		VALUES(p_user_id, NOW(), p_status);
		SET p_result = 1;
	END IF;
END */$$
DELIMITER ;

/* Procedure structure for procedure `AddOrderItem` */

/*!50003 DROP PROCEDURE IF EXISTS  `AddOrderItem` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `AddOrderItem`(
	IN p_order_id INT,
	IN p_product_id INT,
	IN p_quantity INT,
	IN p_price DECIMAL(10,2),
	OUT p_result INT)
BEGIN
	IF NOT EXISTS (SELECT 1 FROM orders WHERE order_id = p_order_id) THEN
		SET p_result = 0;
	ELSEIF NOT EXISTS (SELECT 1 FROM products WHERE product_id = p_product_id) THEN
		SET p_result = 0;
	ELSE 
		INSERT INTO order_items(order_id, product_id, quantity, price)
		VALUES(p_order_id, p_product_id, p_quantity, p_price);
		SET p_result = 1;
	END IF;
END */$$
DELIMITER ;

/* Procedure structure for procedure `AddPayment` */

/*!50003 DROP PROCEDURE IF EXISTS  `AddPayment` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `AddPayment`(
	IN p_order_id INT,
	IN p_payment_method VARCHAR(30),
	IN p_amount DECIMAL(10,2),
	OUT p_result INT)
BEGIN
	IF NOT EXISTS (SELECT 1 FROM orders WHERE order_id = p_order_id) THEN
		SET p_result = 0;
	ELSE
		INSERT INTO payments(order_id, payment_method, amount, payment_date)
		VALUES(p_order_id, p_payment_method, p_amount, NOW());
		SET p_result = 1;
	END IF;
END */$$
DELIMITER ;

/* Procedure structure for procedure `AddProduct` */

/*!50003 DROP PROCEDURE IF EXISTS  `AddProduct` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `AddProduct`(
	IN p_product_name VARCHAR(100),
	IN p_price DECIMAL(10,2),
	IN p_stock INT(11))
BEGIN
	INSERT INTO products(product_name, price, stock)
	VALUES(p_product_name, p_price, p_stock);
END */$$
DELIMITER ;

/* Procedure structure for procedure `AddUser` */

/*!50003 DROP PROCEDURE IF EXISTS  `AddUser` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `AddUser`(
	IN p_full_name VARCHAR(100),
	IN p_email VARCHAR(100),
	IN p_password VARCHAR(255),
	IN p_salt VARCHAR(255),
	IN p_role VARCHAR(25))
BEGIN
	INSERT INTO users(full_name, email, PASSWORD, salt, role, created_at)
	VALUES(p_full_name, p_email, p_password, p_salt, p_role, NOW());
END */$$
DELIMITER ;

/* Procedure structure for procedure `GenerateRefreshToken` */

/*!50003 DROP PROCEDURE IF EXISTS  `GenerateRefreshToken` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `GenerateRefreshToken`(
	IN p_user_id INT,
	IN p_token VARCHAR(255),
	IN p_expires DATETIME)
BEGIN
	INSERT INTO user_sessions(user_id, token, expires, created)
	VALUES(p_user_id, p_token, p_expires, NOW());
END */$$
DELIMITER ;

/* Procedure structure for procedure `LogError` */

/*!50003 DROP PROCEDURE IF EXISTS  `LogError` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `LogError`(
	IN p_trace_id VARCHAR(50),
	IN p_message TEXT,
	IN p_stack_trace TEXT)
BEGIN
	INSERT INTO error_logs(trace_id, error_message, stack_trace)
	VALUES(p_trace_id, p_message, p_stack_trace);
END */$$
DELIMITER ;

/* Procedure structure for procedure `UpdateProduct` */

/*!50003 DROP PROCEDURE IF EXISTS  `UpdateProduct` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `UpdateProduct`(
	IN p_product_id INT,
	IN p_product_name VARCHAR(100),
	IN p_price DECIMAL(10,2),
	IN p_stock INT)
BEGIN
	UPDATE products
	SET
		product_name = COALESCE(p_product_name, product_name),
		price = COALESCE(p_price, price),
		stock = COALESCE(p_stock, stock)
	WHERE product_id = p_product_id;
END */$$
DELIMITER ;

/* Procedure structure for procedure `UpdateUser` */

/*!50003 DROP PROCEDURE IF EXISTS  `UpdateUser` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `UpdateUser`(
	IN p_user_id INT,
	IN p_full_name VARCHAR(100),
	IN p_email VARCHAR(100),
	IN p_password VARCHAR(255),
	IN p_salt VARCHAR(255),
	IN p_role VARCHAR(25),
	IN p_status INT)
BEGIN
	UPDATE users
	SET
		full_name = COALESCE(p_full_name, full_name),
		email = COALESCE(p_email, email),
		PASSWORD = COALESCE(p_password, PASSWORD),
		salt = COALESCE(p_salt, salt),
		role = COALESCE(p_role, role),
		STATUS = COALESCE(p_status, STATUS)
	WHERE user_id = p_user_id;
END */$$
DELIMITER ;

/*Table structure for table `orders_with_user_info` */

DROP TABLE IF EXISTS `orders_with_user_info`;

/*!50001 DROP VIEW IF EXISTS `orders_with_user_info` */;
/*!50001 DROP TABLE IF EXISTS `orders_with_user_info` */;

/*!50001 CREATE TABLE  `orders_with_user_info`(
 `Order ID` int(11) ,
 `User ID` int(11) ,
 `User Name` varchar(100) ,
 `Email` varchar(100) 
)*/;

/*Table structure for table `order_items_details` */

DROP TABLE IF EXISTS `order_items_details`;

/*!50001 DROP VIEW IF EXISTS `order_items_details` */;
/*!50001 DROP TABLE IF EXISTS `order_items_details` */;

/*!50001 CREATE TABLE  `order_items_details`(
 `Order ID` int(11) ,
 `Product ID` int(11) ,
 `Product Name` varchar(100) ,
 `Order Quantity` int(11) ,
 `Order Amount` decimal(10,2) 
)*/;

/*Table structure for table `user_total_spending` */

DROP TABLE IF EXISTS `user_total_spending`;

/*!50001 DROP VIEW IF EXISTS `user_total_spending` */;
/*!50001 DROP TABLE IF EXISTS `user_total_spending` */;

/*!50001 CREATE TABLE  `user_total_spending`(
 `User ID` int(11) ,
 `Name` varchar(100) ,
 `Total Spending` decimal(32,2) 
)*/;

/*View structure for view orders_with_user_info */

/*!50001 DROP TABLE IF EXISTS `orders_with_user_info` */;
/*!50001 DROP VIEW IF EXISTS `orders_with_user_info` */;

/*!50001 CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `orders_with_user_info` AS select `o`.`order_id` AS `Order ID`,`u`.`user_id` AS `User ID`,`u`.`full_name` AS `User Name`,`u`.`email` AS `Email` from (`orders` `o` join `users` `u` on(`o`.`user_id` = `u`.`user_id`)) */;

/*View structure for view order_items_details */

/*!50001 DROP TABLE IF EXISTS `order_items_details` */;
/*!50001 DROP VIEW IF EXISTS `order_items_details` */;

/*!50001 CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `order_items_details` AS select `o`.`order_id` AS `Order ID`,`p`.`product_id` AS `Product ID`,`p`.`product_name` AS `Product Name`,`oi`.`quantity` AS `Order Quantity`,`oi`.`price` AS `Order Amount` from ((`order_items` `oi` join `orders` `o` on(`oi`.`order_id` = `o`.`order_id`)) join `products` `p` on(`oi`.`product_id` = `p`.`product_id`)) */;

/*View structure for view user_total_spending */

/*!50001 DROP TABLE IF EXISTS `user_total_spending` */;
/*!50001 DROP VIEW IF EXISTS `user_total_spending` */;

/*!50001 CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `user_total_spending` AS select `u`.`user_id` AS `User ID`,`u`.`full_name` AS `Name`,sum(`p`.`amount`) AS `Total Spending` from ((`payments` `p` join `orders` `o` on(`o`.`order_id` = `p`.`order_id`)) join `users` `u` on(`u`.`user_id` = `o`.`user_id`)) group by `u`.`user_id` */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;
