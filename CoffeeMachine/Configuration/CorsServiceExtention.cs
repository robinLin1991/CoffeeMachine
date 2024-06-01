namespace CoffeeMachineAPI.Configuration
{
    public static class CorsServiceExtention
    {
        public static void CorsDomainPolicy(this WebApplicationBuilder builder)
        {

            builder.Services.AddCors(option =>
            {
                option.AddPolicy("AllCorsDomainPolicy", corsbuilder =>
                {
                    corsbuilder.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });
        }


        public static void AllCorsDomainPolicy(this WebApplication app) =>
            app.UseCors("AllCorsDomainPolicy");
    }
}
