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
            builder.Services.AddTransient<IRepositorioGeral<Aluno>, RepositorioAluno>();
            builder.Services.AddTransient<IRepositorioGeral<Cidade>, RepositorioCidade>();

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
                name: "editarCidade",
                pattern: "AdministradorCidade/CadastroCidade/{id}",
                defaults: new { controller = "AdministradorCidade", action = "CadastroCidade" });

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=AdministradorAluno}/{action=Index}/{id?}");

            app.MapControllerRoute(
                name: "cadastroAluno",
                pattern: "AdministradorAluno/Cadastro",
                defaults: new { controller = "AdministradorAluno", action = "CadastroAluno" });

            app.MapControllerRoute(
                name: "removerAluno",
                pattern: "AdministradorAluno/RemoverAluno/{id}",
                defaults: new { controller = "AdministradorAluno", action = "Index" });

            app.MapControllerRoute(
                name: "gerarPdf",
                pattern: "Reports/GerarPdf",
                defaults: new { controller = "Reports", action = "GerarPdf" });


            app.Run();
        }
    }
}