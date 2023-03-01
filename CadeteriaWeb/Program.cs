using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//AÑADIENDO LOS MAPPERS
builder.Services.AddControllersWithViews();
var automapper = new MapperConfiguration(item => item.AddProfile(new CadeteriaWeb.MappingProfile())); 
IMapper mapper = automapper.CreateMapper();

//CREANDO LAS DEPENDECIAS
builder.Services.AddTransient<CadeteriaWeb.Models.CadetesModels.ICadetesRepositorio, CadeteriaWeb.Models.CadetesModels.CadetesRepositorio>();
builder.Services.AddTransient<CadeteriaWeb.Models.PedidosModels.IPedidosRepositorio, CadeteriaWeb.Models.PedidosModels.PedidosRepositorio>();
builder.Services.AddTransient<CadeteriaWeb.Models.ClientesModels.IClientesRepositorio, CadeteriaWeb.Models.ClientesModels.ClientesRepositorio>();
builder.Services.AddTransient<CadeteriaWeb.Models.UsuarioModels.IUsuarioRepositorio, CadeteriaWeb.Models.UsuarioModels.UsuarioRepositorio>();

builder.Services.AddSingleton(mapper);
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(99999999999);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
 

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseSession();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
