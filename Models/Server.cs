namespace apiServerMS.Models;

using System.Text.Json.Serialization;

public class Server
{
    private readonly string allowSpecificOrigin = "_allowSpecificOrigin";
    public IConfiguration _configuration { get; }
    public Server(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void ConfigureService(IServiceCollection services)
    {
        services.AddControllers();
        //.ConfigureApiBehaviorOptions(options =>
        // {
        //     options.SuppressConsumesConstraintForFormFileParameters = true;
        //     options.SuppressInferBindingSourcesForParameters = true;
        //     options.SuppressModelStateInvalidFilter = true;
        //     options.SuppressMapClientErrors = true;
        //     // options.ClientErrorMapping[StatusCodes.Status404BadRequest].Link =
        //     //     "https://httpstatuses.com/404";
        // });


        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        //ENABLE CORS
        services.AddCors(
            options =>
            {
                options.AddPolicy
                (
                    name: allowSpecificOrigin,
                    builder =>
                        {
                            builder.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                        }
                );
            }
        );
        // Permite retornar objetos aun cuando hay referencias circulares
        services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
    }

    public void Config(WebApplicationBuilder builder)
    {
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        //Cors
        app.UseCors(allowSpecificOrigin);

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}