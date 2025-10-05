using Microsoft.EntityFrameworkCore;
using razor_pages.Data.DataAnnotations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// By default AddDbContext is scoped life time.

//builder.Services.AddDbContext<razor_pages.Data.Default.StudentContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("StudentContext") ?? throw new InvalidOperationException("Connection string 'StudentContext' not found.")));

//builder.Services.AddDbContext<razor_pages.Data.DataAnnotations.StudentContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("StudentContext") ?? throw new InvalidOperationException("Connection string 'StudentContext' not found.")));

//builder.Services.AddDbContext<razor_pages.Data.FluentValidation.StudentContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("StudentContext") ?? throw new InvalidOperationException("Connection string 'StudentContext' not found.")));

//builder.Services.AddDbContext<razor_pages.Data.DataAnnotations.StudentContext>();

// Change life time scope for the AddDbContext (scoped --> Transient)
// always keeping as scoped is recommented

// studentcontext
// dbcontext options
builder.Services.AddDbContext<razor_pages.Data.DataAnnotations.StudentContext>(
    options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("StudentContext") ?? throw new InvalidOperationException("Connection string 'StudentContext' not found."));
});

// context factory singltonclass
// StudentContext is used as scoped here.
builder.Services.AddDbContext<razor_pages.Data.DataAnnotations.StudentContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("StudentContext") ?? throw new InvalidOperationException("Connection string 'StudentContext' not found."));
});

// we can't change the dbcontext scope with pool
// it's always scoped
// when to use pool means, you are interacting with only single database and you are using most often db context means, better to use pool
// it will create instance and reuse the instances, 
// it can hold upto 1024 bydefault, but you can change the value also.

//builder.Services.AddDbContextPool<razor_pages.Data.DataAnnotations.StudentContext>(options =>
//{
//    options.UseSqlServer(builder.Configuration.GetConnectionString("StudentContext") ?? throw new InvalidOperationException("Connection string 'StudentContext' not found."));
//}, 1024);

// registering by own without using db context extension method
//builder.Services.AddScoped<StudentContext>(options =>
//{
//    var configuration = options.GetRequiredService<IConfiguration>();
//    return new StudentContext(configuration);
//});

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
    app.UseMigrationsEndPoint();
}

// ensuring database created. if no database found then it will create it otherwise no action.
using(var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<razor_pages.Data.DataAnnotations.StudentContext>();
    // it will create just empty database, nothing else
    context.Database.EnsureCreated();
    // it's creating basic data
    razor_pages.Data.DataAnnotations.DbInitializer.Initialize(context);
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
