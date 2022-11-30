using Tour_Ready_Capstone.Interfaces;
using Tour_Ready_Capstone.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<IUser, UserRepository>();
builder.Services.AddTransient<IGroup, GroupRepository>();
builder.Services.AddTransient<IGroupMember, GroupMemberRepository>();
builder.Services.AddTransient<IShow, ShowRepository>();
builder.Services.AddTransient<ICity, CityRepository>();
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
