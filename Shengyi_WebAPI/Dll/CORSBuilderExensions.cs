namespace Shengyi_WebAPI.Dll
{
    public static class CORSBuilderExensions
    {
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddControllers();

            services.AddCors(options =>
            {
                options.AddPolicy("AnyPolicy",
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:5173").AllowAnyHeader().AllowAnyMethod().WithExposedHeaders("Content-Disposition"); ;
                    });
            });
        }
    }
}
