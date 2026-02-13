using Microsoft.EntityFrameworkCore;
using TrabukaApi.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TrabukaApi.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Trabuka API",
        Version = "v1",
        Description = "API para conectar jovens profissionais a empresas em Angola",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "Trabuka Team",
            Email = "contato@trabuka.ao"
        }
    });
});

// Adicionar CORS para permitir chamadas do Angular
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200") // URL do frontend
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

builder.Services.AddDbContext<TrabukaApi.Data.TrabukaDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registrar repositórios
builder.Services.AddScoped<TrabukaApi.Interfaces.IUsuarioRepository, TrabukaApi.Services.UsuarioRepository>();
builder.Services.AddScoped<TrabukaApi.Interfaces.IEmpresaRepository, TrabukaApi.Services.EmpresaRepository>();
builder.Services.AddScoped<TrabukaApi.Interfaces.IProjetoRepository, TrabukaApi.Services.ProjetoRepository>();
builder.Services.AddScoped<TrabukaApi.Interfaces.IPagamentoRepository, TrabukaApi.Services.PagamentoRepository>();
builder.Services.AddScoped<TrabukaApi.Interfaces.ITicketRepository, TrabukaApi.Services.TicketRepository>();
builder.Services.AddScoped<TrabukaApi.Interfaces.IPortfolioRepository, TrabukaApi.Services.PortfolioRepository>();
builder.Services.AddScoped<TrabukaApi.Interfaces.IFAQRepository, TrabukaApi.Services.FAQRepository>();
builder.Services.AddScoped<TrabukaApi.Interfaces.INotificacaoRepository, TrabukaApi.Services.NotificacaoRepository>();
builder.Services.AddScoped<TrabukaApi.Interfaces.IRelatorioRepository, TrabukaApi.Services.RelatorioRepository>();
builder.Services.AddScoped<TrabukaApi.Interfaces.ITesteRepository, TrabukaApi.Services.TesteRepository>();
builder.Services.AddScoped<TrabukaApi.Interfaces.IQuestaoRepository, TrabukaApi.Services.QuestaoRepository>();
builder.Services.AddScoped<TrabukaApi.Interfaces.IResultadoTesteRepository, TrabukaApi.Services.ResultadoTesteRepository>();
builder.Services.AddScoped<TrabukaApi.Interfaces.IMentoriaRepository, TrabukaApi.Services.MentoriaRepository>();
builder.Services.AddScoped<TrabukaApi.Interfaces.IEquipeRepository, TrabukaApi.Services.EquipeRepository>();
builder.Services.AddScoped<TrabukaApi.Interfaces.IUsuarioEquipeRepository, TrabukaApi.Services.UsuarioEquipeRepository>();
builder.Services.AddScoped<TrabukaApi.Interfaces.IConfiguracaoService, TrabukaApi.Services.ConfiguracaoService>();
builder.Services.AddScoped<TrabukaApi.Interfaces.ILogService, TrabukaApi.Services.LogService>();
builder.Services.AddScoped<TrabukaApi.Interfaces.ISessaoService, TrabukaApi.Services.SessaoService>();
builder.Services.AddScoped<TrabukaApi.Interfaces.IMetricaService, TrabukaApi.Services.MetricaService>();
builder.Services.AddScoped<TrabukaApi.Interfaces.IServicoService, TrabukaApi.Services.ServicoService>();
builder.Services.AddScoped<TrabukaApi.Interfaces.IServicosRelacionadosService, TrabukaApi.Services.ServicosRelacionadosService>();
builder.Services.AddScoped<TrabukaApi.Interfaces.IRedeSocialService, TrabukaApi.Services.RedeSocialService>();
builder.Services.AddScoped<TrabukaApi.Interfaces.IFuncionalidadeService, TrabukaApi.Services.FuncionalidadeService>();
builder.Services.AddScoped<TrabukaApi.Interfaces.ICertificacaoService, TrabukaApi.Services.CertificacaoService>();
builder.Services.AddScoped<TrabukaApi.Interfaces.IEducacaoService, TrabukaApi.Services.EducacaoService>();
builder.Services.AddScoped<TrabukaApi.Interfaces.IExperienciaService, TrabukaApi.Services.ExperienciaService>();

// Registrar serviços
builder.Services.AddScoped<TrabukaApi.Interfaces.IUsuarioService, TrabukaApi.Services.UsuarioService>();
builder.Services.AddScoped<TrabukaApi.Interfaces.IEmpresaService, TrabukaApi.Services.EmpresaService>();
builder.Services.AddScoped<TrabukaApi.Interfaces.IProjetoService, TrabukaApi.Services.ProjetoService>();
builder.Services.AddScoped<TrabukaApi.Interfaces.IPagamentoService, TrabukaApi.Services.PagamentoService>();
builder.Services.AddScoped<TrabukaApi.Interfaces.ITicketService, TrabukaApi.Services.TicketService>();
builder.Services.AddScoped<TrabukaApi.Interfaces.IPortfolioService, TrabukaApi.Services.PortfolioService>();
builder.Services.AddScoped<TrabukaApi.Interfaces.IEquipeService, TrabukaApi.Services.EquipeService>();
builder.Services.AddScoped<TrabukaApi.Interfaces.IUsuarioEquipeService, TrabukaApi.Services.UsuarioEquipeService>();
builder.Services.AddScoped<TrabukaApi.Interfaces.IMentoriaService, TrabukaApi.Services.MentoriaService>();
builder.Services.AddScoped<TrabukaApi.Interfaces.IRelatorioService, TrabukaApi.Services.RelatorioService>();
builder.Services.AddScoped<TrabukaApi.Interfaces.IResultadoTesteService, TrabukaApi.Services.ResultadoTesteService>();
builder.Services.AddScoped<TrabukaApi.Interfaces.ITesteService, TrabukaApi.Services.TesteService>();
builder.Services.AddScoped<TrabukaApi.Interfaces.IFAQService, TrabukaApi.Services.FAQService>();
builder.Services.AddScoped<TrabukaApi.Interfaces.INotificacaoService, TrabukaApi.Services.NotificacaoService>();
builder.Services.AddScoped<TrabukaApi.Interfaces.IProjetoPortfolioService, TrabukaApi.Services.ProjetoPortfolioService>();
builder.Services.AddScoped<TrabukaApi.Interfaces.IDashboardService, TrabukaApi.Services.DashboardService>();
builder.Services.AddScoped<TrabukaApi.Interfaces.IHeaderService, TrabukaApi.Services.HeaderService>();

// Configuração JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });
builder.Services.AddScoped<TrabukaApi.Services.JwtService>();

var app = builder.Build();

// Popular o banco com dados iniciais e atualizar caminhos de imagens
using (var scope = app.Services.CreateScope())
{
    try
    {
        var db = scope.ServiceProvider.GetRequiredService<TrabukaApi.Data.TrabukaDbContext>();
        DatabaseSeeder.Seed(db);
        DatabaseUpdater.UpdateImagePaths(db);
        Console.WriteLine("✅ Seed executado com sucesso!");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"❌ Erro ao executar seed: {ex.Message}");
        Console.WriteLine($"Stack trace: {ex.StackTrace}");
        // Não interrompe a aplicação, apenas loga o erro
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Trabuka API v1");
        c.RoutePrefix = string.Empty; // Serve Swagger UI at the app's root
    });
}

// Adicionar middleware de tratamento global de exceções
app.UseGlobalExceptionHandler();

// Usar CORS
app.UseCors("AllowAngularApp");

// Configurar arquivos estáticos para servir imagens
app.UseStaticFiles();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

// Adicionar roteamento de controllers
app.MapControllers();

app.Run();
