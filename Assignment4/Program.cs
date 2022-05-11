using Assignment4.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<MongoService>();
builder.Services.AddSingleton<CardService>();
builder.Services.AddSingleton<MetaService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	//app.UseSwagger();
	//app.UseSwaggerUI();
}

using (var serviceScope = app.Services.CreateScope())
{
	new MongoService();
}


app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
