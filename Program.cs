using Microsoft.AspNetCore.Http;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Step 3: Error-handling middleware (first)
app.Use(async (context, next) =>
{
    try
    {
        await next();
    }
    catch (Exception)
    {
        context.Response.StatusCode = 500;
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync("{\"error\": \"Internal server error.\"}");
    }
});

// Step 4: Authentication middleware (next)
app.Use(async (context, next) =>
{
    var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Replace("Bearer ", "");
    if (string.IsNullOrEmpty(token))
    {
        context.Response.StatusCode = 401;
        await context.Response.WriteAsync("{\"error\": \"Unauthorized.\"}");
        return;
    }

    try
    {
        // Simpler token check: just check if token equals a hardcoded value
        if (token != "mysecrettoken")
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("{\"error\": \"Unauthorized.\"}");
            return;
        }
        await next();
    }
    catch
    {
        context.Response.StatusCode = 401;
        await context.Response.WriteAsync("{\"error\": \"Unauthorized.\"}");
    }
});

// Step 2: Logging middleware (last)
app.Use(async (context, next) =>
{
    var method = context.Request.Method;
    var path = context.Request.Path;
    await next();
    var statusCode = context.Response.StatusCode;
    Console.WriteLine($"[{DateTime.Now}] {method} {path} => {statusCode}");
});

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();