using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NestChecksAndBalances.Filters;
using NestChecksAndBalances.Integrations;
using NestChecksAndBalances.Repositories;
using NestChecksAndBalances.Services;

namespace NestChecksAndBalances
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public static IConfigurationRoot Configuration { get; private set; }
        
        public void ConfigureServices(IServiceCollection services)
        {
            //Services Injection
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ITemperatureService, TemperatureService>();

            //Data Access Injection            
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ICabLogRepository, CabLogRepository>();

            //Integration Injection
            services.AddAWSService<Amazon.S3.IAmazonS3>();
            services.AddAWSService<Amazon.DynamoDBv2.IAmazonDynamoDB>();
            services.AddTransient<INestAPI, NestAPI>();

            //Service Filter Injection
            services.AddTransient<UserAuthFilter>();

            services.AddMvc();
            services.AddDefaultAWSOptions(Configuration.GetAWSOptions());
        }
        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddLambdaLogger(Configuration.GetLambdaLoggerOptions());
            app.UseMvc();
        }
    }
}