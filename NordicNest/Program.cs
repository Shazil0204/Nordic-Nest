using Microsoft.AspNetCore.Builder;
using NordicNest.Controller.EmailVerficationClasses;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// Add in-memory cache service
builder.Services.AddDistributedMemoryCache();

// Singleton service for EmailSender
builder.Services.AddSingleton<EmailSender>();

// Configure session
builder.Services.AddSession(options =>
{
	// Set session timeout to 30 minutes
	options.IdleTimeout = TimeSpan.FromMinutes(30);

	// Make the session cookie HTTP-only and essential
	options.Cookie.HttpOnly = true;
	options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	// Use exception handler for non-development environments
	app.UseExceptionHandler("/Error");

	// Enable HTTP Strict Transport Security (HSTS) for enhanced security
	// The default HSTS value is 30 days
	// You may want to change this for production scenarios
	// See https://aka.ms/aspnetcore-hsts
	app.UseHsts();
}

// Redirect HTTP requests to HTTPS
app.UseHttpsRedirection();

// Serve static files like CSS, JavaScript, and images
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

// Enable session middleware
app.UseSession();

// Map Razor Pages
app.MapRazorPages();

// Run the application
app.Run();
