using System.Text.Json.Serialization;
using ActionFilterValidator;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });
builder.Services.AddScoped<ModelStateValidationFilter>();
builder.Services.AddControllers(options => { options.Filters.Add<ModelStateValidationFilter>(); })
    .AddJsonOptions(opt => { opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); });
builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
// just add one of validation configs here like CreateUserValidation:
builder.Services.AddValidatorsFromAssemblyContaining<CreateUserValidation>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();