using API;
using API.Customize;
using Microsoft.AspNetCore.Builder;
using System.IO;

if (!Directory.Exists("/src/wwwroot/"))
    Directory.CreateDirectory("/src/wwwroot/");

var builder = WebApplication.CreateBuilder(args);

builder.UseStartup<Startup>();