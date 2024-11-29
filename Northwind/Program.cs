using Northwind;
using ServiceStack;

ServiceStack.Licensing.RegisterLicense("OSS GPL-3.0 2024 https://github.com/NetCoreApps/Northwind MdY/zx8fYPPaKBGBHM6gALunMI2yWijyGGB5ydV1FlpWFM0ZbTV/QwbncEWl01pTKKvaTSbr9dLBJdDJe6gZJxaeX5OfaJkXmtc7/HqNPk8yr1gU289EpaXKpwt9URSz5MfGvqK2F9knr4BCugwDJYug+KUGt7UnhNLGCs/BG/A=");

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    app.UseHttpsRedirection();
}

app.UseServiceStack(new AppHost());

app.Run();
