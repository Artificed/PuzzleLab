using System.Net.Http.Headers;
using PuzzleLab.Web.Services.Api.Client;
using Microsoft.JSInterop;
using PuzzleLab.Web.Services.Api.Security;

namespace PuzzleLab.Web.Services.Api.Handlers;

public class AuthHeaderHandler(ITokenProvider tokenProvider) : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        string? token = null;
        try
        {
            token = await tokenProvider.GetTokenAsync();
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains(
                                                       "JavaScript interop calls cannot be issued at this time"))
        {
            Console.WriteLine(
                "AuthHeaderHandler: Cannot get token during prerendering. Proceeding without Authorization header.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"AuthHeaderHandler: Error retrieving token: {ex.Message}");
        }

        if (!string.IsNullOrEmpty(token))
        {
            if (request.Headers.Authorization == null)
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }

        return await base.SendAsync(request, cancellationToken);
    }
}