using Invoice.Data;
using Invoice.Repositories;
using Invoice.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
/*builder.Services.AddSingleton<DbContextOptionsFactory>();*/

builder.Services.AddDbContext<ApplicationDbContext>();

Dictionary<string, string> connStrs = new Dictionary<string, string>();
connStrs.Add("DB1", builder.Configuration["ConnectionStrings:EInvoice"]);
connStrs.Add("DB2", builder.Configuration["ConnectionStrings:EwayBillOfficer"]);
connStrs.Add("DB3", builder.Configuration["ConnectionStrings:DefaultConnect"]);
DbContextOptionsFactory.SetConnectionString(connStrs);


/*
builder.Services.AddDbContext<ApplicationDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("EInvoice"));
}
);

builder.Services.AddDbContext<ApplicationDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("EwayBillOfficer"));
}
);*/

builder.Services.AddHttpClient();

builder.Services.AddScoped(typeof(DatabaseOperationsInterface), typeof(DatabaseOperations));
builder.Services.AddScoped(typeof(HeaderVerificationInterface), typeof(HeaderVerification));

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
