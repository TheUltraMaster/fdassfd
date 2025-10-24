using Bucket;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Minio;
using Proyecto.Data;
using Proyecto.Services;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();



dotenv.net.DotEnv.Load();


builder.Services.AddDbContext<ProyectoContext>(opt => opt.UseMySQL(Environment.GetEnvironmentVariable("conexion")));
// Add BCrypt service
builder.Services.AddScoped<Bycript.IBCryptService, Bycript.BCryptService>();
// Add User service
builder.Services.AddScoped<IUserService, UserService>();
// Add Export service
// Add Treatment service
builder.Services.AddScoped<ITreatmentService, TreatmentService>();
// Add HttpClient service
builder.Services.AddHttpClient();





string direccion = "138.197.199.155";
int puerto = 9000;
bool ssl = false;

builder.Services.AddMinio(configureClient => configureClient
    .WithEndpoint(direccion, puerto)
    .WithCredentials("minioadmin", "minioadmin")
    .WithSSL(ssl));

// 🔥 Inyectar nuestro servicio MinioService
builder.Services.AddScoped<IMinioService>(sp =>
{
    var minioClient = sp.GetRequiredService<IMinioClient>();
    return new MinioService(minioClient, direccion, puerto, ssl);
});









var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// API endpoint for mobile app login
app.MapPost("/api/mobile/login", async (Proyecto.ApiModels.LoginRequest request, Proyecto.Services.IUserService usuarioService) =>
{
    try
    {
        var user = await usuarioService.AuthenticateAsync(request.Username, request.Password);

        if (user != null)
        {
            var response = new Proyecto.ApiModels.LoginResponse
            {
                Success = true,
                Message = "Login exitoso",
                User = new Proyecto.ApiModels.UserData
                {
                    Id = user.Id,
                    Username = user.Username,
                    IsAdmin = user.Isadmin ?? false
                },
                Token = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes($"{user.Id}:{user.Username}:{DateTime.UtcNow:yyyy-MM-ddTHH:mm:ssZ}"))
            };

            return Results.Ok(response);
        }
        else
        {
            var response = new Proyecto.ApiModels.LoginResponse
            {
                Success = false,
                Message = "Usuario o contraseña incorrectos"
            };

            return Results.Unauthorized();
        }
    }
    catch (Exception ex)
    {
        var response = new Proyecto.ApiModels.LoginResponse
        {
            Success = false,
            Message = "Error interno del servidor"
        };

        return Results.Problem("Error interno del servidor");
    }
});


// API endpoint to register disease prediction
app.MapPost("/api/prediccion", async (Proyecto.ApiModels.PredictionRequest request, ProyectoContext context) =>
{
    try
    {
        // Decode and validate token
        int userId;
        try
        {
            var decodedBytes = Convert.FromBase64String(request.Token);
            var decodedToken = System.Text.Encoding.UTF8.GetString(decodedBytes);
            var tokenParts = decodedToken.Split(':');

            if (tokenParts.Length < 3 || !int.TryParse(tokenParts[0], out userId))
            {
                return Results.BadRequest(new Proyecto.ApiModels.PredictionResponse
                {
                    Success = false,
                    Message = "Token inválido"
                });
            }

            // Validate token timestamp (optional - check if token is not too old)
            if (DateTime.TryParse(tokenParts[2], out DateTime tokenTime))
            {
                if (DateTime.UtcNow.Subtract(tokenTime).TotalDays > 7)
                {
                    return Results.Unauthorized();
                }
            }
        }
        catch
        {
            return Results.BadRequest(new Proyecto.ApiModels.PredictionResponse
            {
                Success = false,
                Message = "Token inválido"
            });
        }

        // Verify user exists
        var user = await context.Usuarios.FindAsync(userId);
        if (user == null)
        {
            return Results.Unauthorized();
        }

        // Find disease by classname
        var enfermedad = await context.Enfermedads
            .FirstOrDefaultAsync(e => e.Classname == request.EnfermedadClassname);

        if (enfermedad == null)
        {
            return Results.BadRequest(new Proyecto.ApiModels.PredictionResponse
            {
                Success = false,
                Message = "Enfermedad no encontrada"
            });
        }

        // Verify cultivo exists
        var cultivo = await context.Cultivos.FindAsync(request.CultivoId);
        if (cultivo == null)
        {
            return Results.BadRequest(new Proyecto.ApiModels.PredictionResponse
            {
                Success = false,
                Message = "Cultivo no encontrado"
            });
        }
        
        // Create prediction
        var prediccion = new Proyecto.Models.Prediccion
        {
            Confianza = request.Confianza,
            IdCultvio = request.CultivoId,
            IdEnfermedad = enfermedad.Id,
            IdEtapa = cultivo.IdEtapa // Use the current stage of the crop
        };

        context.Prediccions.Add(prediccion);
        await context.SaveChangesAsync();

        return Results.Ok(new Proyecto.ApiModels.PredictionResponse
        {
            Success = true,
            Message = "Predicción registrada exitosamente",
            PrediccionId = prediccion.Id
        });
    }
    catch (Exception ex)
    {
        return Results.Problem(new Proyecto.ApiModels.PredictionResponse
        {
            Success = false,
            Message = ex.Message
        }.Message);
    }
});


// API endpoint to get tomato diseases
app.MapGet("/api/enfermedades", async (ProyectoContext context) =>
{
    try
    {
        var enfermedades = await context.Enfermedads
            .Select(e => new
            {
                e.Id,
                e.Nombre,
                e.Classname
            })
            .ToListAsync();

        return Results.Ok(enfermedades);
    }
    catch (Exception ex)
    {
        return Results.Problem("Error al obtener las enfermedades");
    }
});

// API endpoint to add leaf with image upload
app.MapPost("/api/hoja", async (Proyecto.ApiModels.LeafRequest request, ProyectoContext context, IMinioService minioService) =>
{
    try
    {
        // Verify prediction exists
        var prediccion = await context.Prediccions.FindAsync(request.PrediccionId);
        if (prediccion == null)
        {
            return Results.BadRequest(new Proyecto.ApiModels.LeafResponse
            {
                Success = false,
                Message = "Predicción no encontrada"
            });
        }

        // Find disease by classname
        var enfermedad = await context.Enfermedads
            .FirstOrDefaultAsync(e => e.Classname == request.EnfermedadClassname);

        if (enfermedad == null)
        {
            return Results.BadRequest(new Proyecto.ApiModels.LeafResponse
            {
                Success = false,
                Message = "Enfermedad no encontrada"
            });
        }

        // Generate unique filename with GUID
        var nombreArchivo = Guid.NewGuid() + ".jpg";

        // Upload image to Minio
        string imageUrl;
        using (var stream = new MemoryStream(request.ImageData))
        {
            var ruta = await minioService.UploadImageAsync(stream, nombreArchivo, "image/jpeg");
            imageUrl = await minioService.GetImageUrlAsync(nombreArchivo);
        }

        // Create new leaf entry
        var hoja = new Proyecto.Models.Hoja
        {
            Confianza = request.Confianza,
            Luminosidad = request.Luminosidad,
            Rojo = request.Rojo,
            Verde = request.Verde,
            Azul = request.Azul,
            IdEnfermedad = enfermedad.Id,
            Url = imageUrl,
            Anchopx = request.Ancho,
            Altopx = request.Alto
        };

        context.Hojas.Add(hoja);
        await context.SaveChangesAsync();

        return Results.Ok(new Proyecto.ApiModels.LeafResponse
        {
            Success = true,
            Message = "Hoja registrada exitosamente",
            HojaId = hoja.Id,
            ImageUrl = imageUrl
        });
    }
    catch (Exception ex)
    {
        return Results.Problem(new Proyecto.ApiModels.LeafResponse
        {
            Success = false,
            Message = "Error interno del servidor"
        }.Message);
    }
});

// API endpoint to get user's linked cultivos
app.MapGet("/api/cultivos", async (HttpContext context, ProyectoContext dbContext) =>
{
    try
    {
        // Get token from Authorization header
        string? token = null;
        if (context.Request.Headers.ContainsKey("Authorization"))
        {
            var authHeader = context.Request.Headers["Authorization"].ToString();
            if (authHeader.StartsWith("Bearer "))
            {
                token = authHeader.Substring(7);
            }
        }

        if (string.IsNullOrEmpty(token))
        {
            return Results.BadRequest(new Proyecto.ApiModels.CultivosResponse
            {
                Success = false,
                Message = "Token requerido"
            });
        }

        // Decode and validate token
        int userId;
        try
        {
            var decodedBytes = Convert.FromBase64String(token);
            var decodedToken = System.Text.Encoding.UTF8.GetString(decodedBytes);
            var tokenParts = decodedToken.Split(':');

            if (tokenParts.Length < 3 || !int.TryParse(tokenParts[0], out userId))
            {
                return Results.BadRequest(new Proyecto.ApiModels.CultivosResponse
                {
                    Success = false,
                    Message = "Token inválido"
                });
            }

            // Validate token timestamp (check if token is not too old)
            if (DateTime.TryParse(tokenParts[2], out DateTime tokenTime))
            {
                if (DateTime.UtcNow.Subtract(tokenTime).TotalDays > 7)
                {
                    return Results.Unauthorized();
                }
            }
        }
        catch
        {
            return Results.BadRequest(new Proyecto.ApiModels.CultivosResponse
            {
                Success = false,
                Message = "Token inválido"
            });
        }

        // Verify user exists
        var user = await dbContext.Usuarios.FindAsync(userId);
        if (user == null)
        {
            return Results.Unauthorized();
        }

        // Get cultivos linked to this user with joins
        var cultivos = await dbContext.UsuariosCultivos
            .Where(uc => uc.IdUsuario == userId)
            .Join(dbContext.Cultivos, uc => uc.IdCultivo, c => c.Id, (uc, c) => c)
            .Join(dbContext.VariedadTomates, c => c.IdVariedadTomate, vt => vt.Id, (c, vt) => new { Cultivo = c, VariedadTomate = vt })
            .Join(dbContext.Etapas, cvt => cvt.Cultivo.IdEtapa, e => e.Id, (cvt, e) => new Proyecto.ApiModels.CultivoData
            {
                Id = cvt.Cultivo.Id,
                IdAgronomo = cvt.Cultivo.IdAgronomo,
                FechaCommit = cvt.Cultivo.FechaCommit,
                CantidadPlantas = cvt.Cultivo.CantidadPlantas,
                Latitud = cvt.Cultivo.Latitud,
                Longitud = cvt.Cultivo.Longitud,
                IdVariedadTomate = cvt.Cultivo.IdVariedadTomate,
                IdEtapa = cvt.Cultivo.IdEtapa,
                VariedadTomate = cvt.VariedadTomate.Nombre,
                Etapa = e.Nombre
            })
            .ToListAsync();

        return Results.Ok(new Proyecto.ApiModels.CultivosResponse
        {
            Success = true,
            Message = "Cultivos obtenidos exitosamente",
            Cultivos = cultivos
        });
    }
    catch (Exception ex)
    {
        return Results.Problem(new Proyecto.ApiModels.CultivosResponse
        {
            Success = false,
            Message = "Error interno del servidor"
        }.Message);
    }
});

// API endpoint to get treatments by disease classname and crop ID
app.MapGet("/api/tratamientos/{diseaseClassname}/{cultivoId:int}", async (string diseaseClassname, int cultivoId, ITreatmentService treatmentService) =>
{
    try
    {
        var treatments = await treatmentService.GetTreatmentsByDiseaseAndCropAsync(diseaseClassname, cultivoId);

        var result = treatments.Select(t => new
        {
            t.Id,
            t.Nombre,
            t.Descripcion,
            t.CantidadPorPlanta,
            Enfermedad = t.IdEnfermedadNavigation?.Nombre,
            EnfermedadClassname = t.IdEnfermedadNavigation?.Classname,
            Etapa = t.IdEtapaNavigation?.Nombre
        }).ToList();

        return Results.Ok(new
        {
            Success = true,
            Message = "Tratamientos obtenidos exitosamente",
            Tratamientos = result
        });
    }
    catch (Exception ex)
    {
        return Results.Problem(new
        {
            Success = false,
            Message = "Error interno del servidor"
        }.ToString());
    }
});


app.Run();


