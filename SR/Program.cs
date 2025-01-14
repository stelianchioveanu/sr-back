using SR.Services.Implementations;
using SR.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()  // Permite cererile de la orice domeniu (nu este recomandat pentru producție)
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddHttpClient();


// Add the required controllers service
builder.Services.AddControllers(options =>
{
    options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true; // Opțional
}).ConfigureApiBehaviorOptions(options =>
{
    options.SuppressMapClientErrors = true; // Evită mapping-ul `null` -> 204
});

builder.Services.AddSingleton<ITMDBService>(provider =>
    new TMDBService(provider.GetRequiredService<HttpClient>(), "e0986bb8ae7430f895aeb67c0ecf7541")
);

builder.Services.AddSingleton<IRecombeeService>(provider =>
    new RecombeeService(
        "ugabuga-dev", 
        "YV0NqcFoyLfQWGrpLLZYW7mEd9VEBhP0CVak0ahwOubilqR1ImmwNHW2fF1KE65f",
        provider.GetRequiredService<ITMDBService>()
    ));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.UseRouting();
app.UseAuthorization();
app.UseCors("AllowAll");

// Add the endpoints for controllers
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers(); // Use this instead of MapDefaultControllerRoute
});

app.Run();