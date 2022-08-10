# JWTTokenAuthentication

* [jwt.io](https://jwt.io)
* [rfc-editor](https://www.rfc-editor.org/rfc/rfc7519)
 
1) HEADER:ALGORITHM & TOKEN TYPE
2) PAYLOAD:DATA
3) VERIFY SIGNATURE

[Authorize]

## UserCredentional - JWTManager -Packages

+ add Microsoft.AspNetCore.Authentication.JwtBearer
+ create interface IJWTAuthenticationManager
+ create class JwtAuthenticationManager
+ add System.IdentityModel.Tokens.Jwt
+ add Microsoft.AspNetCore.Authentication
+ add Microsoft.AspNetCore.Authorization

## Startup.cs
```
 public void ConfigureServices(IServiceCollection services)
        {
            private readonly string key = "KEY_STRING";
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddSingleton<IJWTAuthenticationManager>(new JwtAuthenticationManager(key));
        }
```
