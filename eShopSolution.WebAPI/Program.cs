using eShopSolution.BusinessLayer.Abstract;
using eShopSolution.BusinessLayer.Service;
using eShopSolution.cloudinaryManagerFile;
using eShopSolution.cloudinaryManagerFile.Abstract;
using eShopSolution.cloudinaryManagerFile.Service;
using eShopSolution.CrawlData.Model;
using eShopSolution.CrawlData.Service;
using eShopSolution.DataLayer.Abstract;
using eShopSolution.DataLayer.Context;
using eShopSolution.DataLayer.EntityFramework;
using eShopSolution.DataLayer.Helpers;
using eShopSolution.DtoLayer.Configuration;
using eShopSolution.EmailService.Gmail;
using eShopSolution.EmailService.Model;
using eShopSolution.EmailService.Service;
using eShopSolution.EntityLayer.Data;
using eShopSolution.PayMentService.Config;
using eShopSolution.PayMentService.Service;
using eShopSolution.RealTime.DataService;
using eShopSolution.RealTime.Hubs;
using eShopSolution.WebAPI.CustomPermission;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSignalR();
builder.Services.AddHttpClient();



builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddAutoMapper(typeof(MapperConfig));
builder.Services.AddControllers();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    // C?u hình th?i gian h?t h?n c?a Session (tùy ch?nh)
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Session s? h?t h?n sau 30 phút không ho?t ??ng
    options.Cookie.HttpOnly = true; // Ch? truy c?p Session t? server
    options.Cookie.IsEssential = true; // Cookie này là b?t bu?c ?? ho?t ??ng bình th??ng
});

builder.Services.AddDbContext<ApplicationContext>();
builder.Services.AddHostedService<OrderCleanupService>();

builder.Services.AddScoped<IProductSizeInventoryDal, ProductSizeInventoryDal>();
builder.Services.AddScoped<IProductSizeInventoryService, ProductSizeInventoryService>();

builder.Services.AddScoped<IProductImageDal, ProductImageDal>();
builder.Services.AddScoped<IProductImageService, ProductImageService>();

builder.Services.AddScoped<IProductDal, ProductDal>();
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddScoped<IProductColorDal, ProductColorDal>();
builder.Services.AddScoped<IProductColorService, ProductColorService>();

builder.Services.AddScoped<ICategoryAndBrandDal, CategoryAndBrandDal>();
builder.Services.AddScoped<ICategoryAndBrandService, CategoryAndBrandService>();

builder.Services.AddScoped<IShippingProvidersDal, ShippingProvidersDal>();
builder.Services.AddScoped<IShippingProvidersService, ShippingProvidersService>();

builder.Services.AddScoped<IStatusOrderDal, OrderStatusDal>();
builder.Services.AddScoped<IOrderStatusService, OrderStatusService>();

builder.Services.AddScoped<IPayMentDal, PaymentDal>();
builder.Services.AddScoped<IPaymentService, PaymentService>();

builder.Services.AddScoped<IGenderDal, GenderDal>();
builder.Services.AddScoped<IGenderService, GenderService>();

builder.Services.AddScoped<IColorDal, ColorDal>();
builder.Services.AddScoped<IColorService, ColorService>();

builder.Services.AddScoped<IRefreshTokenDal, RefreshTokenDal>();
builder.Services.AddScoped<IRefreshTokenService, RefreshTokenService>();

builder.Services.AddScoped<ISizeDal, SizeDal>();
builder.Services.AddScoped<ISizeService, SizeService>();

builder.Services.AddScoped<ICategoryDal, CategoryDal>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

builder.Services.AddScoped<IBrandDal, BrandDal>();
builder.Services.AddScoped<IBrandService, BrandService>();

builder.Services.AddScoped<ICartService, CartService>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.AddScoped<ICommentsService, CommentsService>();
builder.Services.AddScoped<ICommentsDal, CommentsDal>();

builder.Services.AddScoped<IAddressDeliveryServive, AddressDeliveryServive>();
builder.Services.AddScoped<IAddressDeliveryDal, AddressDeliveryDal>();

builder.Services.AddScoped<ISenderEmail, EmailSender>();

builder.Services.AddScoped<ICloudinaryService, CloudinaryService>();

builder.Services.AddScoped<IVnPayService,VnPayService>();

builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderDal, OrderDal>();

builder.Services.AddScoped<IDetailOrderService, DetailOrderService>();
builder.Services.AddScoped<IDetailOrderDal, DetailOrderDal>();

builder.Services.AddScoped<IProductReviewService, ProductReviewService>();
builder.Services.AddScoped<IProductReviewDal, ProductReviewDal>();

builder.Services.AddScoped<IPermissionService, PermissionService>();
builder.Services.AddScoped<IPermissionDal, PermissionDal>();

builder.Services.AddScoped<IPermissionMenuService, PermissionMenuService>();
builder.Services.AddScoped<IPermissionMenuDal, PermissionMenuDal>();

builder.Services.AddScoped<IMenuService, MenuService>();
builder.Services.AddScoped<IMenuDal, MenuDal>();

builder.Services.AddScoped<IAspNetRoleAccessService,AspNetRoleAccessService>();
builder.Services.AddScoped<IAspNetRoleAccessDal, AspNetRoleAccessDal>();

builder.Services.AddScoped<IInfoPaymentService, InfoService>();
builder.Services.AddScoped<IInfoDal, InfoDal>();

builder.Services.AddScoped<ZaloPayService>();

builder.Services.AddScoped<ReadFileJson>();

builder.Services.AddMemoryCache();
builder.Services.AddSingleton(typeof(ICustomCache<>), typeof(CustomCache<>));
builder.Services.AddSingleton<ShareDb>();

builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod();
           
    });
});
/*policy.WithOrigins("http://localhost:3000", "http://127.0.0.1:5500")
             .AllowAnyHeader()
             .AllowAnyMethod()
             .AllowCredentials();*/
builder.Services.Configure<CustomTokenOption>(builder.Configuration.GetSection("TokenOption"));
builder.Services.Configure<VnPayConfig>(builder.Configuration.GetSection("VnPay"));
builder.Services.Configure<ShippingProvidersConfiguration>(builder.Configuration.GetSection("ShippingProvidersConfiguration"));
builder.Services.Configure<EmailConfiguration>(builder.Configuration.GetSection("EmailConfiguration"));
builder.Services.Configure<CloudinarySetting>(builder.Configuration.GetSection("CloudinarySetting"));

builder.Services.AddSingleton<EmailConfiguration>();
builder.Services.AddSingleton<CloudinarySetting>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "QLBH API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});


/*builder.Services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
builder.Services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();
*/

builder.Services.AddIdentity<AppUser, AppRole>(opt =>
{
    opt.Password.RequiredLength = 7;
    opt.Password.RequireDigit = true;
    opt.Password.RequireUppercase = false;
    opt.SignIn.RequireConfirmedEmail = true;
})
.AddEntityFrameworkStores<ApplicationContext>()
.AddDefaultTokenProviders();

builder.Services.Configure<DataProtectionTokenProviderOptions>(options =>
{
    options.TokenLifespan = TimeSpan.FromMinutes(3);
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    var tokenOptions = builder.Configuration.GetSection("TokenOption").Get<CustomTokenOption>();
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidIssuer = tokenOptions.Issuer,
        ValidAudience = tokenOptions.Audience[0],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions.SecurityKey)),
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
   /* options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            var accessToken = context.Request.Query["access_token"];
            var path = context.HttpContext.Request.Path;
            if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/Chat"))
            {
                context.Token = accessToken;
            }
            return Task.CompletedTask;
        }
    };*/

});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors();
app.UseHttpsRedirection();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(builder.Environment.ContentRootPath, "Image")),
    RequestPath = "/Image"
});
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();
app.MapHub<ChatHub>("/Chat");
app.MapHub<ProductHub>("/ProductHub");
app.MapHub<CommentHub>("/CommentHub");

app.MapControllers();

app.Run();
