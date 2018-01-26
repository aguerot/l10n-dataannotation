using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace l10n_dataannotation
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLocalization(l => l.ResourcesPath = "Ressources");

            services
               .AddMvc()
               .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix, o => { o.ResourcesPath = "Resources"; })
               .AddDataAnnotationsLocalization();

            services.Configure<RequestLocalizationOptions>(o =>
            {
                o.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("en");
                o.SupportedUICultures = o.SupportedCultures = new List<CultureInfo> {
                    new CultureInfo("en-US"),
                    new CultureInfo("fr-FR")
                };
                o.RequestCultureProviders.Insert(0, new QueryStringRequestCultureProvider());
            });

           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            var locOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(locOptions.Value);

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
