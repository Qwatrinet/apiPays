using ApiPays.DB;

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

//Ajouter notre contexte
builder.Services.AddDbContext<ContextePays>();

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
}

app.UseHttpsRedirection();
app.UseCors("_localhostOrigins");  // lolol sinon on peut pas fetch depuis localhost

app.UseAuthorization();

app.MapControllers();

app.Run();
