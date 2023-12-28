using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using WEB_API;
using WEB_API.Data;
using WEB_API.Models;
using WEB_API.Models.DTO;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(MappingConfig));
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapGet("/api/product", ()=> {
    APIResponse response = new();

    response.Result = ProductStore.productList;
    response.IsSucces = true;
    response.StatusCode = HttpStatusCode.OK;    

    return Results.Ok(response);

}).WithName("GetProducts").Produces<APIResponse>(200);



app.MapGet("/api/product/{id:int}", (int id) => {
    APIResponse response = new();

    response.Result = ProductStore.productList.FirstOrDefault(i => i.Id == id);
    response.IsSucces = true;
    response.StatusCode = HttpStatusCode.OK;

    Results.Ok(response);

}).WithName("GetProduct").Produces<APIResponse>(200);




app.MapPost("/api/product", async (IMapper _mapper,
    IValidator <ProductCreateDTO> _validation, [FromBody] ProductCreateDTO product_C_DTO) => {

    APIResponse response = new() { IsSucces = false, StatusCode = HttpStatusCode.BadRequest };

    var validationResult = await _validation.ValidateAsync(product_C_DTO);

    if(!validationResult.IsValid){
        response.ErrorMessages.Add(validationResult.Errors.FirstOrDefault().ToString());
        return Results.BadRequest(response);
    }
    if (ProductStore.productList.FirstOrDefault(u => u.Name.ToLower() == product_C_DTO.Name.ToLower()) != null)
    {
            response.ErrorMessages.Add("Product name exist");
            return Results.BadRequest(response);
    }

    Product product = _mapper.Map<Product>(product_C_DTO);

    product.Id = ProductStore.productList.OrderByDescending(i => i.Id).FirstOrDefault().Id + 1;
    ProductStore.productList.Add(product);

    Product_DTO product_DTO = _mapper.Map<Product_DTO>(product);


    response.Result = product_DTO;
    response.IsSucces = true;
    response.StatusCode = HttpStatusCode.OK;

    return Results.Ok(response);
        //return Results.CreatedAtRoute("GetProduct", new { id = product.Id }, product_DTO);


    }).WithName("CreateProduct").Accepts<ProductCreateDTO>("aplication/json").Produces<APIResponse>(201).Produces(400);



app.MapPut("/api/product", async (IMapper _mapper,
    IValidator<ProductUpdateDTO> _validation, [FromBody] ProductUpdateDTO product_U_DTO) => {
    APIResponse response = new() { IsSucces = false, StatusCode = HttpStatusCode.BadRequest };

    var validationResult = await _validation.ValidateAsync(product_U_DTO);

    if (!validationResult.IsValid)
    {
        response.ErrorMessages.Add(validationResult.Errors.FirstOrDefault().ToString());
        return Results.BadRequest(response);
    }

    Product productFromStore = ProductStore.productList.FirstOrDefault(i => i.Id == product_U_DTO.Id);
    productFromStore.IsActive = product_U_DTO.IsActive;
    productFromStore.Name = product_U_DTO.Name;
    productFromStore.Updated = DateTime.Now;



    response.Result = _mapper.Map<Product>(productFromStore);
    response.IsSucces = true;
    response.StatusCode = HttpStatusCode.OK;

    return Results.Ok(response);
    }).WithName("UpdateProduct").Accepts<ProductUpdateDTO>("aplication/json").Produces<APIResponse>(200).Produces(400);




app.MapDelete("/api/product/{id:int}", (int id) => {

    APIResponse response = new() { IsSucces = false, StatusCode = HttpStatusCode.BadRequest };



    Product productFromStore = ProductStore.productList.FirstOrDefault(i => i.Id == id);
    if(productFromStore != null)
    {
        ProductStore.productList.Remove(productFromStore);
        response.IsSucces = true;
        response.StatusCode = HttpStatusCode.NoContent;
        return Results.Ok(response);
    }
    else
    {
        response.ErrorMessages.Add("Invalid id");
        return Results.BadRequest(response);
    }

});






app.Run();
