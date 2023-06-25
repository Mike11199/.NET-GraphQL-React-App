using API.GraphQL;
using GraphQL.Server.Ui.Voyager;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;




var AllowSpecificOrigins = "_allowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContextFactory<OMAContext>(options =>
{
    options.UseInMemoryDatabase("InMemoryDb");  //from EFC
});
//builder.services.addcontrollers();


//graphql service

builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddFiltering();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//so front end can access back end without issues
app.UseCors(AllowSpecificOrigins);  

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();

app.MapGraphQL();

app.UseGraphQLVoyager("/graphql-voyager", new VoyagerOptions { GraphQLEndPoint = "/graphql"} );

app.Run();



