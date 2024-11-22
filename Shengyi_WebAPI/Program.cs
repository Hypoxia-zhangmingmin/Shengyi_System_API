using DemoTest.DLL;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Shengyi_WebAPI.Daos.Category;
using Shengyi_WebAPI.Daos.Commondity;
using Shengyi_WebAPI.Daos.Customers;
using Shengyi_WebAPI.Daos.Inventory;
using Shengyi_WebAPI.Daos.Invoices;
using Shengyi_WebAPI.Daos.Order;
using Shengyi_WebAPI.Daos.PaymentStatus;
using Shengyi_WebAPI.Daos.Sales;
using Shengyi_WebAPI.Daos.Storages;
using Shengyi_WebAPI.Daos.Suppliers;
using Shengyi_WebAPI.Daos.User;
using Shengyi_WebAPI.Dll;
using Shengyi_WebAPI.EFCore;
using Shengyi_WebAPI.Services.Category;
using Shengyi_WebAPI.Services.Commondity;
using Shengyi_WebAPI.Services.Customers;
using Shengyi_WebAPI.Services.Inventory;
using Shengyi_WebAPI.Services.Invoices;
using Shengyi_WebAPI.Services.Jwt;
using Shengyi_WebAPI.Services.Orders;
using Shengyi_WebAPI.Services.PaymentStatus;
using Shengyi_WebAPI.Services.Sales;
using Shengyi_WebAPI.Services.Storages;
using Shengyi_WebAPI.Services.Suppliers;
using Shengyi_WebAPI.Services.User;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.ConfigureCors();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var DB_CON_STR = builder.Configuration.GetValue<string>("DBConStr");

builder.Services.AddDbContext<DB>(a =>
{
    a.UseSqlServer(DB_CON_STR);
});


builder.UseRedis();
builder.UseJsonToken();

builder.Services.AddScoped<IUserDao, UserDao>();
builder.Services.AddScoped<IUserService,UserService>();

builder.Services.AddScoped<ICategoryDao, CategoryDao>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

builder.Services.AddScoped<ICommondityDao, CommondityDao>();
builder.Services.AddScoped<ICommondityService, CommondityService>();

builder.Services.AddScoped<ICustomerDao, CustomerDao>();
builder.Services.AddScoped<ICustomerService,CustomerService>();

builder.Services.AddScoped<ISupplierDao, SupplierDao>();
builder.Services.AddScoped<ISupplierService, SupplierService>();

builder.Services.AddScoped<ISaleDao, SalesDao>();
builder.Services.AddScoped<ISaleService,SaleService>();

builder.Services.AddScoped<IInventoryDao,InventioryDao>();
builder.Services.AddScoped<IInventoryService, InventoryService>();

builder.Services.AddScoped<IStorageDao, StorageDao>();
builder.Services.AddScoped<IStorageService, StorageService>();

builder.Services.AddScoped<IPaymentStatusDao, PaymentStatusDao>();
builder.Services.AddScoped<IPaymentStatusService,PaymentStatusService>();

builder.Services.AddScoped<IOrderDao, OrderDao>();
builder.Services.AddScoped<IOrderService, OrderService>();  

builder.Services.AddScoped<IInvoicesDao,InvoicesDao>();
builder.Services.AddScoped<IInvoicesService,InvoicesService>();

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AnyPolicy");
app.UseAuthorization();

app.MapControllers();

app.Run();
