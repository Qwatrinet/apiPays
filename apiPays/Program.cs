using ApiPays.DB;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.EntityFrameworkCore;

//using (var context = new ContextePays())
//{
//    //creates db if not exists 
//    context.Database.EnsureCreated();
//    //create entity objects

//    HttpClient client = new HttpClient();


//    var tmp = client.GetFromJsonAsAsyncEnumerable<PaysLike>("https://arfp.github.io/tp/web/api/api-countries/countries.json");

//    await foreach (PaysLike? paysLike in tmp)
//    {
//        context.Pays.Add(new Pays { NomPays = paysLike.country_name, CodePays = paysLike.country_code });
//    }
//    //save data to the database tables
//    context.SaveChanges();
//}


//building the app
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// .AddJsonOptions(o => o.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Ajouter notre contexte si connection fixe
//builder.Services.AddDbContext<ContextePays>();
//Personnaliser la chaine de connection dans appsettings.json
var connectionstring = builder.Configuration.GetConnectionString("ConnectionSQLServer");
builder.Services.AddDbContext<ContextePays>(options => options.UseSqlServer(connectionstring));

builder.Services.AddHttpLogging(options =>
{
    options.LoggingFields = HttpLoggingFields.All;
    options.CombineLogs = true;
});

builder.Services.AddCors(o =>
{
    o.AddPolicy(name: "_localhostOrigins",
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:5173")
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                      });
}); //lolol sinon on peut pas fetch depuis localhost

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    //RewriteOptions options = new RewriteOptions()
    //    .AddRedirect("\\/", "swagger/index.html");
    //app.UseRewriter(options);
}

app.UseHttpsRedirection();
app.UseCors("_localhostOrigins");  // lolol sinon on peut pas fetch depuis localhost

app.UseAuthorization();

app.UseHttpLogging();

app.MapControllers().WithHttpLogging(HttpLoggingFields.All);

//using (var scope = app.Services.CreateScope())
//{
//    scope.ServiceProvider.GetRequiredService<ContextePays>().Database.Migrate();
//}

app.Run();
