using BUS.Services;
using BUS.Services.IServices;
using DBA;
using DBA.Entities;
using DBA.Repo;
using DBA.Repo.IRepo;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);




builder.Services.AddDbContext<clsDbContext>(op =>
    op.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionStr")));

builder.Services.AddIdentity<Users, IdentityRole>(op=>
{
    op.Password.RequireDigit = true;
    op.Password.RequireUppercase = true;
    op.Password.RequireLowercase = true;
    op.Password.RequiredLength = 6;

}
).AddEntityFrameworkStores<clsDbContext>().AddDefaultTokenProviders();

builder.Services.AddAuthentication(auth =>
{
    auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(op =>
{
    op.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["AuthSettings:Audience"],
        ValidIssuer = builder.Configuration["AuthSettings:Issuer"] ,
        RequireExpirationTime=true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["AuthSettings:Key"]!)),
        ValidateIssuerSigningKey =true



    };

});

//repo
builder.Services.AddScoped<IUserRepo, UserRepo>();

//service
builder.Services.AddScoped<IUserService, UserService>();



builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();
