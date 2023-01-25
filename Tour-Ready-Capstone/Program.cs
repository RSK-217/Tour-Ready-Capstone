using Tour_Ready_Capstone.Interfaces;
using Tour_Ready_Capstone.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;

var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

//FirebaseApp.Create(new AppOptions
//{
//    Credential = GoogleCredential.FromFile("")
//});

var firebaseProjectId = builder.Configuration.GetValue<string>("Authentication:Firebase:ProjectId");
var googleTokenUrl = $"https://securetoken.google.com/{firebaseProjectId}";
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = googleTokenUrl;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = googleTokenUrl,
            ValidateAudience = true,
            ValidAudience = firebaseProjectId,
            ValidateLifetime = true
        };
    });
// Add services to the container.
builder.Services.AddTransient<IUser, UserRepository>();
builder.Services.AddTransient<IGroup, GroupRepository>();
builder.Services.AddTransient<IGroupMember, GroupMemberRepository>();
builder.Services.AddTransient<IShow, ShowRepository>();
builder.Services.AddTransient<ICity, CityRepository>();
builder.Services.AddTransient<IPeople, PeopleRepository>();
builder.Services.AddTransient<IPlace, PlaceRepository>();
builder.Services.AddTransient<INotes, NotesRepository>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                      });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(MyAllowSpecificOrigins);
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
