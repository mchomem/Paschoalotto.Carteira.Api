namespace Paschoalotto.Carteira.Infrastructure.IoC;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureApi(this IServiceCollection services, IConfiguration configuration)
    {
        #region Database

        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(connectionString));

        #endregion

        #region Repositories

        services.AddScoped<IClienteRepository, ClienteRepository>();
        services.AddScoped<IContratoRepository, ContratoRepository>();
        services.AddScoped<IParcelaRepository, ParcelaRepository>();
        services.AddScoped<IAcordoRepository, AcordoRepository>();
        services.AddScoped<IParcelaAcordoRepository, ParcelaAcordoRepository>();
        services.AddScoped<IBoletoRepository, BoletoRepository>();

        #endregion

        #region Services

        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IClienteService, ClienteService>();
        services.AddScoped<IContratoService, ContratoService>();
        services.AddScoped<IDebtCalculationService, DebtCalculationService>();
        services.AddScoped<IAgreementService, AgreementService>();
        services.AddScoped<IParcelaAcordoService, ParcelaAcordoService>();
        services.AddScoped<IBoletoService, BoletoService>();
        services.AddScoped<IDocumentoService, DocumentoService>();

        #endregion

        return services;
    }

    public static IServiceCollection AddInfrastructureJWT(this IServiceCollection services)
    {
        var key = Encoding.ASCII.GetBytes("+WsLhdwMcCnW&cJW4a5hm^jFemE&?V?Y?z9eMdcN_X3DktLE7W9nS#Z2&vpakM6v");
        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(x =>
        {
            x.RequireHttpsMetadata = false;
            x.SaveToken = true;
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });

        return services;
    }

    public static IServiceCollection AddInfrastructureSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Paschoalotto.Carteira.Api",
                Version = "v1",
                Description = "API REST para gestão e negociação de contratos de dívidas de pessoas físicas e jurídicas do Banco Paschoalotto.",
                Contact = new OpenApiContact
                {
                    Name = "Banco Paschoalotto",
                    Email = "contato@bancopaschoalotto.com.br"
                }
            });

            // Incluir comentários XML
            var xmlFile = "Paschoalotto.Carteira.Api.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            if (File.Exists(xmlPath))
            {
                options.IncludeXmlComments(xmlPath);
            }

            // Configurar autenticação JWT no Swagger
            var jwtSecurityScheme = new OpenApiSecurityScheme
            {
                Scheme = "bearer",
                BearerFormat = "JWT",
                Name = "JWT Authentication",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Description = "Insira **APENAS** o token JWT Bearer no campo abaixo!",
                Reference = new OpenApiReference
                {
                    Id = JwtBearerDefaults.AuthenticationScheme,
                    Type = ReferenceType.SecurityScheme
                }
            };

            options.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                { jwtSecurityScheme, Array.Empty<string>() }
            });
        });

        return services;
    }

    public static IServiceCollection AddInfrastructureCORS(this IServiceCollection services, IConfiguration configuration)
    {
        var policyName = configuration["Cors:PolicyName"] ?? "PaschoalottoCarteira";
        var allowedOrigins = configuration["Cors:AllowedOrigins"] ?? "http://localhost:4200";

        services.AddCors(options =>
        {
            options.AddPolicy(policyName, policy =>
                policy.WithOrigins(allowedOrigins.Split(';'))
                      .AllowAnyHeader()
                      .AllowAnyMethod()
                      .AllowCredentials());
        });

        return services;
    }
}
