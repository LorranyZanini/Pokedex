using Microsoft.EntityFrameworkCore;
using Pokedex.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Configuração do Serviço de Contexto (conexão com o banco)
string conexao = builder.Configuration.GetConnectionString("Conexao");
//linha acima busca quem é o conectionstring
var versao = ServerVersion.AutoDetect(conexao); //descobre a versão do banco
builder.Services.AddDbContext<AppDbContext>(
    options => options.UseMySql(conexao, versao)
);
//criado contexto, configurada a string de conexão e o serviço
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
