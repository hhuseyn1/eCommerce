using Microsoft.EntityFrameworkCore.Update;
using Source.Models;
using Source.Services;

namespace Source.Middlewares;

public class AuthMiddleware
{
    private RequestDelegate _requestDelegate;

    public AuthMiddleware(RequestDelegate requestDelegate)
    {
        this._requestDelegate = requestDelegate;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        IUserManager _userManager = context.RequestServices.GetService<IUserManager>();

        UserCredentials userCredentials = _userManager.GetCredentials();
        if (userCredentials is not null)
            await _requestDelegate.Invoke(context);
        else
            await context.Response.WriteAsync("UnAuthorized");
    }
}
