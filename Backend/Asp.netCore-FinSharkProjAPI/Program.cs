using Asp.netCore_FinSharkProjAPI.Data;
using Asp.netCore_FinSharkProjAPI.Interfaces;
using Asp.netCore_FinSharkProjAPI.Models;
using Asp.netCore_FinSharkProjAPI.Repository;
using Asp.netCore_FinSharkProjAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

//NewtonSoft.Json.JsonSerializerSettings
builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        
    });

var provider = builder.Services.BuildServiceProvider();
var config = provider.GetRequiredService<IConfiguration>();

builder.Services.AddDbContext<ApplicationDbContext>(items =>
    items.UseSqlServer(config.GetConnectionString("dbcs")));

// Configure Identity 

builder.Services.AddIdentity<AppUser, IdentityRole>(options =>     //ye sub hum conditions derhy ha ke hamre password ki requirments kia hongi
{
    options.Password.RequireDigit = true;  // Password ma digit hona chahiye
    options.Password.RequireLowercase = true;  // Password ma lowercase letter hona chahiye
    options.Password.RequireUppercase = true;  // Password ma uppercase letter hona chahiye
    options.Password.RequiredLength = 8;  // Password ki minimum length 12 honi chahiye
    options.Password.RequireNonAlphanumeric = true;  // Password ma non-alphanumeric character hona chahiye

}).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();  // IdentityUser aur IdentityRole ko ApplicationDbContext ke sath use karna hai


// Authentication jwt token
// AddAuthentication(): ASP.NET Core ko batata hai ke hum authentication use kar rahe hain.
// DefaultAuthenticateScheme: Ye define karta hai default mechanism jo har request ko verify karega (JWT in this case).
// JwtBearerDefaults.AuthenticationScheme ? iska matlab hai ke hum Bearer Token use kar rahe hain (like Authorization: Bearer <token> in headers).


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = 
    options.DefaultChallengeScheme = 
    options.DefaultForbidScheme = 
    options.DefaultScheme = 
    options.DefaultSignInScheme =
    options.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,    // JWT kisne issue kiya? Check karein.

        ValidIssuer = builder.Configuration["Jwt:Issuer"],     // Ye values aapki appsettings.json file se uthayi ja rahi hain:

        ValidateAudience = true,   //JWT kiske liye bana? (client)

        ValidAudience = builder.Configuration["Jwt:Audience"],  //Ye values aapki appsettings.json file se uthayi ja rahi hain:

        ValidateIssuerSigningKey = true,   //Kya signature valid hai?

        IssuerSigningKey = new SymmetricSecurityKey(    //Ye values aapki appsettings.json file se uthayi ja rahi hain: 
            System.Text.Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SigningKey"])
            ),

    };
}
);


builder.Services.AddScoped<IStockRepository, StockRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<ITokenService, ServiceToken>(); // Token service ko dependency injection ke zariye add karna hai
builder.Services.AddScoped<IPortfolioRepository, PortfolioRepository>(); // Portfolio repository ko dependency injection ke zariye add karna hai
builder.Services.AddScoped<IFMPService, FMPSevice>();
builder.Services.AddHttpClient<IFMPService,FMPSevice>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();
app.UseCors("AllowAll");
app.UseAuthentication(); // Authentication middleware ko use karna hai, taake JWT token ko verify kiya ja sake.
app.UseAuthorization(); // Authorization middleware ko use karna hai, taake user ki permissions check ki ja sake.


app.MapControllers();

app.Run();
