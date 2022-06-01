using Microsoft.EntityFrameworkCore;
using PetShop.Data.Base;
using PetShop.Data.Contexts;
using PetShop.Data.Model;
using PetShop.Service;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

//DbContext configuration
builder.Services.AddDbContext<PetShopDataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("PetShopDataConnection"));
});

////Services configuration
builder.Services.AddScoped<IEntityBaseRepository<Category>, CategoryRepository>();
builder.Services.AddScoped<IEntityBaseRepository<Animal>, AnimalRepository>();
builder.Services.AddScoped<IEntityBaseRepository<Comment>, CommentRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
