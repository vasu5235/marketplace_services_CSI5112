using marketplace_services_CSI5112.Services;
using marketplace_services_CSI5112.Models;

var builder = WebApplication.CreateBuilder(args);

//allow cors
//var MyAllowSpecificOrigins = "*";

//var MyAllowSpecificOrigins = "http://localhost:50213";
//var MyAllowSpecificOrigins = "http://localhost:50213";

//var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
//var MyAllowSpecificOrigins = "http://localhost:51845";


// builder.Services.AddCors(options =>
// {
//     options.AddPolicy(name: MyAllowSpecificOrigins,
//                       builder =>
//                       {
//                           builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
//                       });
// });

builder.Services.AddCors();
builder.Services.AddMvc(options => options.EnableEndpointRouting = false);

// builder.Services.AddCors(builder => builder
//      .AllowAnyOrigin()
//      .AllowAnyMethod()
//      .AllowAnyHeader()
//      );

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Changes for integrating MongoDB Atlas
builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);
builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection(nameof(DatabaseSettings)));
DatabaseSettings options = builder.Configuration.GetSection(nameof(DatabaseSettings)).Get<DatabaseSettings>();

// override connection string from environment variables, you can also do the same for the rest
//string connection_string = builder.Configuration.GetValue<string>("CONNECTION_STRING");
//if (!string.IsNullOrEmpty(connection_string))
//{
//    options.ConnectionString = connection_string;
//}

builder.Services.AddSingleton<DatabaseSettings>();

// Services DI
builder.Services.AddSingleton<UserService>();
builder.Services.AddSingleton<CategoryService>();
builder.Services.AddSingleton<ProductService>();
builder.Services.AddSingleton<OrderService>();
builder.Services.AddSingleton<QuestionService>();
builder.Services.AddSingleton<AnswerService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(
        options => options.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod()
    );

app.UseMvc();

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
