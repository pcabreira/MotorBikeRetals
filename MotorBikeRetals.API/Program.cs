using Azure.Storage.Blobs;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MotorBikeRetals.API.Filters;
using MotorBikeRetals.Application.Commands.CreateBike;
using MotorBikeRetals.Application.Commands.CreateContract;
using MotorBikeRetals.Application.Commands.CreateUser;
using MotorBikeRetals.Application.Commands.CreateUserImage;
using MotorBikeRetals.Application.Consumers;
using MotorBikeRetals.Application.Validators;
using MotorBikeRetals.Infrastructure;
using Serilog;
using System;
using System.Text;

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddHostedService<BikeNotificationConsumer>();
    
    builder.Services.AddFluentValidationAutoValidation();
    builder.Services.AddFluentValidationClientsideAdapters();
    builder.Services.AddControllers(options => options.Filters.Add(typeof(ValidationFilter)));
    builder.Services.AddValidatorsFromAssemblyContaining<CreateUserCommandValidator>()
                    .AddValidatorsFromAssemblyContaining<CreateUserImageCommandValidator>()
                    .AddValidatorsFromAssemblyContaining<CreateBikeCommandValidator>()
                    .AddValidatorsFromAssemblyContaining<CreateContractCommandValidator>()
                    .AddValidatorsFromAssemblyContaining<UpdatePlateCommandValidator>();

    builder.Services
        .AddMediatR(typeof(CreateUserCommand))
        .AddMediatR(typeof(CreateUserImageCommand))
        .AddMediatR(typeof(CreateBikeCommand))
        .AddMediatR(typeof(CreateContractCommand))
        .AddMongo()
        .AddRepositories()
        .AddSingleton(x => new BlobServiceClient(builder.Configuration.GetConnectionString("StorageAccount")));

    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "MotorBikeRetals.API", Version = "v1" });

        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "JWT Authorization header usando o esquema Bearer."
        });

        c.AddSecurityRequirement(new OpenApiSecurityRequirement
                 {
                     {
                           new OpenApiSecurityScheme
                             {
                                 Reference = new OpenApiReference
                                 {
                                     Type = ReferenceType.SecurityScheme,
                                     Id = "Bearer"
                                 }
                             },
                             new string[] {}
                     }
                 });
    });

    builder.Services
      .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
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
              IssuerSigningKey = new SymmetricSecurityKey
            (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
          };
      });

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MotorBikeRetals.API v1"));
    }
    else
    {
        app.UseExceptionHandler("/Error");
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseRouting();
    app.UseCors("AllowAll");
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();
    app.Run();

    Log.Information("Application Starting");
}
catch (Exception ex)
{
    Log.Warning(ex, "An error occurred starting the application");
}
finally
{
    Log.CloseAndFlush();
}