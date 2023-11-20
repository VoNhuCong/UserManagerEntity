global using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using UserManagerEntity.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using UserManagerEntity.Models.Entities;
using UserManagerEntity.Models;
using UserManagerEntity.Configurations;
using Microsoft.OpenApi.Models;
using System.Reflection;
using Google.Protobuf.WellKnownTypes;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureServices();

// JWT Token
var jwtSettings = new JwtSettings()
{
    Key = builder.Configuration["Jwt:Key"],
    Issuer = builder.Configuration["Jwt:Issuer"],
    Audience = builder.Configuration["Jwt:Audience"],
    Lifetime = TimeSpan.Parse(builder.Configuration["Jwt:Lifetime"] ?? "00:00:45"),
    RefreshTokenTTL = int.Parse(builder.Configuration["Jwt:RefreshTokenTTL"] ?? "6"),
    ClockSkew = TimeSpan.Parse(builder.Configuration["Jwt:ClockSkew"] ?? "00:00:00"),
};
builder.Services.AddSingleton(jwtSettings);

// Khóa tài khoản nếu đăng nhập nhiều lần
var identitySettings = new IdentitySettings();
builder.Configuration.GetSection("IdentitySettings").Bind(identitySettings);
if (identitySettings.DefaultUserName.IsNullOrEmpty() || identitySettings.DefaultPassword.IsNullOrEmpty())
{
    throw new ApplicationException("Missing DefaultUserName or DefaultPassword ENV");
}
builder.Services.AddSingleton(identitySettings);

// Add authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
                .AddJwtBearer(opt =>
                {
                    opt.SaveToken = true;
                    opt.RequireHttpsMetadata = false;
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        //tự cấp token
                        ValidateIssuer = false,
                        ValidateAudience = false,

                        //ký vào token
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key)),

                        ValidateLifetime = true,
                        ValidIssuer = jwtSettings.Issuer,
                        ValidAudience = jwtSettings.Audience,
                        ClockSkew = TimeSpan.Zero
                    };
                });


// connect db
var mysqlOptions = new MySqlOptions();
var MysqlOptionsSection = builder.Configuration.GetSection(MySqlOptions.SectionName);
MysqlOptionsSection.Bind(mysqlOptions);
builder.Services.Configure<MySqlOptions>(MysqlOptionsSection);

builder.Services.AddDbContextPool<DataContext>(
    options => options.UseMySQL(mysqlOptions.GetConnectionString())
);

// identity
builder.Services.AddIdentity<User, Role>()
    .AddRoles<Role>()
    .AddEntityFrameworkStores<DataContext>()
    .AddDefaultTokenProviders();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "USER_MANAGER", Version = "V1" });
    var filePath = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    c.IncludeXmlComments(filePath, includeControllerXmlComments: true);
    c.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT containing userid claim",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    var security =
        new OpenApiSecurityRequirement
        {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Id = "Bearer",
                                    Type = ReferenceType.SecurityScheme
                                },
                                UnresolvedReference = true
                            },
                            new List<string>()
                        }
        };

    c.AddSecurityRequirement(security);
});

// don't edit
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.CreateDbIfNotExists();
app.Run();
