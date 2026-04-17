using Microsoft.OpenApi;

namespace AeonRegistryAPI.Extensions
{
    public static class OpenAPISwaggerExtensions
    {
        public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen( c => 
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Aeon Registry API",
                    Version = "v1",
                    Description = """
                    <img src="/images/AeonRegistryLogoBLK.png" height="120" />

                    ## Aeon Research Division

                    Internal API for managing recovered artifacts and research data.
                    Provides secure access for field researchers and analysis.

                    ### Key Features:
                    - Site and Artifact Catalog
                    - Research record submissions
                    - Secure media storage
                    - User role management

                    """,
                    Contact = new OpenApiContact
                    { 
                        Name = "Mariia-Olena",
                        Url = new Uri("https://www.linkedin.com/in/mariia-olena-stus-378909222/"),
                        Email = "mariia.stus@gmail.com"
                    }
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                { 
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer' [space] and then your valid JWT token."
                });

                c.AddSecurityRequirement((document) => new OpenApiSecurityRequirement()
                {
                    [new OpenApiSecuritySchemeReference("Bearer", document)] = []
                });
            });

            return services;
        }
    }
}
