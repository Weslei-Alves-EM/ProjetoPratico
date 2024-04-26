using EM.Domain;
using EM.Repository;
namespace EM.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddTransient<IRepositorioAluno<Aluno>, RepositorioAluno>();
            builder.Services.AddTransient<IRepositorioCidade<Cidade>, RepositorioCidade>();

            builder.Services.AddControllersWithViews();

            var app = builder.Build();
            
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}