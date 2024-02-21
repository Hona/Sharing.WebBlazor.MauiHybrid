using System.Net.Http.Headers;

namespace MyShop.UI.Mobile.Authentication;

public class TokenDelegatingHandler(TokenAccessor tokenAccessor) : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        request.Headers.Authorization =
            new AuthenticationHeaderValue("Bearer", tokenAccessor.AccessToken);
        return await base.SendAsync(request, cancellationToken);
    }
}