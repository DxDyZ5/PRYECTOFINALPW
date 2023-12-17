using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using PROYECTOFINALPW.Client;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Authorization;
using PROYECTOFINALPW.Client.Extensiones;
using PROYECTOFINALPW.Client.Services;
using CurrieTechnologies.Razor.SweetAlert2;




var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<IAbogadoService, AbogadoService>();
builder.Services.AddScoped<ICasoService, CasoService>();
builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<IEstadoSerivce, EstadoService>();
builder.Services.AddScoped<ItipoDeCaso, TipoDeCasoService>();

builder.Services.AddSweetAlert2();   

builder.Services.AddBlazoredSessionStorage();
builder.Services.AddScoped<AuthenticationStateProvider, Autenticacion>();
builder.Services.AddAuthorizationCore();

await builder.Build().RunAsync();
