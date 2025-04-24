using System.Net.Http.Headers;
using Blazored.LocalStorage;
using PuzzleLab.Web;
using PuzzleLab.Web.Services.Api.Client;
using PuzzleLab.Web.Services.Api.Core.Implementations;
using PuzzleLab.Web.Services.Api.Core.Interfaces;
using PuzzleLab.Web.Services.Api.Handlers;
using PuzzleLab.Web.Services.Api.Security;
using PuzzleLab.Web.Services.State;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

var apiBaseUrl = builder.Configuration.GetValue<string>("ApiSettings:BaseUrl");
if (string.IsNullOrEmpty(apiBaseUrl))
{
    throw new InvalidOperationException("API Base URL 'ApiSettings:BaseUrl' is not configured in appsettings.json");
}

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddSingleton<ITokenProvider, InMemoryAuthTokenService>();

// builder.Services.AddScoped<ITokenProvider>(sp =>
// {
//     var protectedSessionStorage = sp.GetRequiredService<ILocalStorageService>();
//     return new AuthTokenService(protectedSessionStorage, "authorization-token");
// });

builder.Services.AddTransient<AuthHeaderHandler>();
builder.Services.AddHttpClient<IApiClient, ApiClient>(client =>
    {
        client.BaseAddress = new Uri(apiBaseUrl);
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    })
    .AddHttpMessageHandler<AuthHeaderHandler>();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IQuestionPackageService, QuestionPackageService>();
builder.Services.AddScoped<IQuestionService, QuestionService>();
builder.Services.AddScoped<IAnswerService, AnswerService>();
builder.Services.AddScoped<IQuizAnswerService, QuizAnswerService>();
builder.Services.AddScoped<IQuizScheduleService, QuizScheduleService>();
builder.Services.AddScoped<IQuizParticipantsService, QuizParticipantsService>();
builder.Services.AddScoped<IQuizSessionService, QuizSessionService>();
builder.Services.AddScoped<UserStateService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();