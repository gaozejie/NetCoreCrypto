using NetCoreCrypto.RSA;
using NetCoreCrypto.SM2;
using NetCoreCrypto.SM3;
using NetCoreCrypto.SM4;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IRSAEncryptionService, RSAEncryptionService>();
builder.Services.AddScoped<ISm2EncryptionService, Sm2EncryptionService>();
builder.Services.AddScoped<ISm3EncryptionService, Sm3EncryptionService>();
builder.Services.AddScoped<ISm4EncryptionService, Sm4EncryptionService>();

// Sm2EncryptionOptions ≈‰÷√
builder.Services.Configure<Sm2EncryptionOptions>(options =>
{
    options.DefaultCurve = Sm2EncryptionNames.CurveSm2p256v1;
});
// Sm4EncryptionOptions ≈‰÷√
builder.Services.Configure<Sm4EncryptionOptions>(options =>
{
    // 16Œª
    options.DefaultIv = Encoding.UTF8.GetBytes("8888888888888888");
    options.DefaultMode = Sm4EncryptionNames.ModeECB;
    options.DefaultPadding = Sm4EncryptionNames.NoPadding;
});


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
