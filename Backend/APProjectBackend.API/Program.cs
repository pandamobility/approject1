using APProjectBackend.Model.Entities;
using APProjectBackend.Model.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();

builder.Services.AddScoped<AuthorRepository, AuthorRepository>();
builder.Services.AddScoped<BookRepository, BookRepository>();
builder.Services.AddScoped<GenreRepository, GenreRepository>();
builder.Services.AddScoped<PublisherRepository, PublisherRepository>();
builder.Services.AddScoped<UsersRepository, UsersRepository>();

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
//    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
