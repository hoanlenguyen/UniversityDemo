using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Threading.Tasks;
using UniversityDemo.Authentication;
using UniversityDemo.Data;
using UniversityDemo.DataContext.Cosmos;
using UniversityDemo.Extensions;
using UniversityDemo.Identity;

namespace UniversityDemo
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Env = env;
        }

        public IConfiguration Configuration { get; }

        private IWebHostEnvironment Env { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            if (Env.IsDevelopment())
            {
                services.AddDbContext<DemoDbContext>(options =>
                options.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]));

                services.AddSingleton<CosmosDbContext>(
                InitializeCosmosClientInstanceAsync(
                    Configuration.GetSection("CosmosDb")).GetAwaiter().GetResult());
            }
            else
            {
                services.AddDbContext<DemoDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

                services.AddSingleton<CosmosDbContext>(
                InitializeCosmosClientInstanceAsync(
                    Configuration.GetSection("CosmosDb")).GetAwaiter().GetResult());
            }

            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<DemoDbContext>();

            //services.AddCors(opts =>
            //{
            //    //opts.AddPolicy("AllowAllOrigins",
            //    //builder =>
            //    //{
            //    //    builder.AllowAnyOrigin()
            //    //    .AllowAnyMethod()
            //    //    .AllowCredentials();

            //    //});
            //});

            services.AddHttpContextAccessor();

            services.Configure<JWTSettings>(Configuration.GetSection(nameof(JWTSettings)));

            //services.AddAuthentication("Basic");
            //https://www.zehntec.com/blog/permission-based-authorization-in-asp-net-core/
            //https://stackoverflow.com/questions/46938248/asp-net-core-2-0-combining-cookies-and-bearer-authorization-for-the-same-endpoin/46942760#46942760

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
             {
                 options.RequireHttpsMetadata = false;
                 options.SaveToken = true;
                 options.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuer = false,
                     ValidateAudience = false,
                     ValidateLifetime = true,
                     ValidateIssuerSigningKey = true,
                     ValidIssuer = Configuration["JWTSettings:Issuer"],
                     ValidAudience = Configuration["JWTSettings:Audience"],
                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWTSettings:SecretKey"]))
                 };
             });

            services.AddAutoMapper(typeof(Startup));

            services.AddBusinessServices();

            services.AddAuthorizations();

            services.AddTestInjectionServices();

            services.AddControllers();

            services.AddRouting(options => options.LowercaseUrls = true);

            services.AddSwaggerDocumentation();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseSwaggerDocumentation();

            app.UseRouting();

            //// global cors policy
            //app.UseCors(x => x
            //    .AllowAnyOrigin()
            //    .AllowAnyMethod()
            //    .AllowAnyHeader());

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static async Task<CosmosDbContext> InitializeCosmosClientInstanceAsync(IConfigurationSection configurationSection)
        {
            string databaseName = configurationSection.GetSection("DatabaseName").Value;
            string containerName = configurationSection.GetSection("ContainerName").Value;
            string account = configurationSection.GetSection("Account").Value;
            string key = configurationSection.GetSection("Key").Value;
            CosmosClient client = new CosmosClient(account, key);
            CosmosDbContext cosmosDbContext = new CosmosDbContext(client, databaseName, containerName);
            DatabaseResponse database = await client.CreateDatabaseIfNotExistsAsync(databaseName);
            await database.Database.CreateContainerIfNotExistsAsync(containerName, "/type");

            return cosmosDbContext;
        }
    }
}