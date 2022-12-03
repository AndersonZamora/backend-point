using Capa_Datos;
using Capa_Entidad;
using Capa_Negocio;
using Capa_Validacion;
using CoreCinCout;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var SpecificOrigins = "_SpecificOrigins";
builder.Configuration.AddJsonFile("appsettings.json");

var code = builder.Configuration.GetSection("AppSettings").Get<Setting>();
var keyBytes = Encoding.UTF8.GetBytes(code.Jwt);

builder.Services.AddCors(options =>
{
    options.AddPolicy(SpecificOrigins,
        policy =>
        {
            policy.WithOrigins("http://localhost:5173/")
            .AllowAnyHeader()
            .AllowAnyMethod();
        });
});

builder.Services.AddAuthentication(config =>
{
    config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(config =>
{
    config.RequireHttpsMetadata = true;
    config.SaveToken = true;
    config.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.AddControllers();
builder.Services.AddScoped<IBD_CinCout, SBD_CinCout>();
builder.Services.AddScoped<ICreateHash, SCreateHash>();
builder.Services.AddScoped<ILoginCinCout, SLoginCinCout>();
builder.Services.AddScoped<ITokenCreate, STokenCreate>();
builder.Services.AddScoped<IValidarCampos, SValidarCampos>();
builder.Services.AddScoped<IValidarLoginRegister, SValidarLoginRegister>();
builder.Services.AddScoped<IValidateSettings, SValidateSettings>();
builder.Services.AddScoped<ICOSetting, SCOSetting>();
builder.Services.AddScoped<IHandleLogin, SHandleLogin>();
builder.Services.AddScoped<IDB_Usuario, SDB_Usuario>();
builder.Services.AddScoped<ILoginUsuario, SLoginUsuario>();
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();  

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.UseSwagger();
    //app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.UseCors(SpecificOrigins);

app.UseRouting();

app.UseCors();

app.UseAuthentication();

app.UseAuthorization();
//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapRazorPages();
//});

app.MapControllers();

app.Run();
