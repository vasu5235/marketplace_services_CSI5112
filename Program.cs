using marketplace_services_CSI5112.Services;

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

// Services DI
builder.Services.AddSingleton<UserService>();
builder.Services.AddSingleton<CategoryService>();
builder.Services.AddSingleton<ProductService>();
builder.Services.AddSingleton<OrderService>();

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
