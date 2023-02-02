using apiServerMS.Models;
//Builder
var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

var server = new Server(builder.Configuration);
server.ConfigureService(builder.Services);
server.Config(builder);
