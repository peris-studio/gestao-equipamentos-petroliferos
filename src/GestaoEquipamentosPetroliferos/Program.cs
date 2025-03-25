var builder = WebApplication.CreateBuilder(args);

// Adicionando serviços ao contêiner
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "GestaoEquipamentosPetroliferos API", Version = "v1" });
});

// Configurando o DbContext com a string de conexão
builder.Services.AddDbContext<ApplicationDbContext>();

// Adicionando Health Checks
builder.Services.AddHealthChecks();

// Criando a aplicação
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "GestaoEquipamentosPetroliferos API v1");
    c.RoutePrefix = string.Empty; // Acessar documentação na raiz ("/")
});

app.UseHttpsRedirection();
app.UseRouting(); // Adicione esta linha para garantir que o roteamento está configurado

app.MapControllers(); // Mapeia todos os controllers

// Endpoint de Health Check
app.MapHealthChecks("/healthcheck");

// Iniciando a aplicação
app.Run();