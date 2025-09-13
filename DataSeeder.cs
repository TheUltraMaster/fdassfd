using Proyecto.Data;
using Proyecto.Models;
using Bycript;

namespace Proyecto;

public static class DataSeeder
{
    public static void SeedUsers(ProyectoContext context, BCryptService bcryptService)
    {
        if (context.Usuarios.Any())
        {
            return; // Ya existen usuarios
        }

        // Crear usuarios semilla
        var usuarios = new List<Usuario>
        {
            new Usuario
            {
                Username = "admin",
                Password = bcryptService.HashText("admin123"),
                Isadmin = true
            },
            new Usuario
            {
                Username = "agronomo1",
                Password = bcryptService.HashText("agronomo123"),
                Isadmin = false
            },
            new Usuario
            {
                Username = "agronomo2", 
                Password = bcryptService.HashText("agronomo456"),
                Isadmin = false
            },
            new Usuario
            {
                Username = "usuario1",
                Password = bcryptService.HashText("usuario123"),
                Isadmin = false
            }
        };

        context.Usuarios.AddRange(usuarios);
        context.SaveChanges();

        // Crear agrónomos asociados a usuarios no-admin
        var agronomos = new List<Agronomo>
        {
            new Agronomo
            {
                PrimerNombre = "Carlos",
                SegundoNombre = "Alberto",
                PrimerApellido = "González",
                SegundoApellido = "Pérez",
                UsuarioId = usuarios[1].Id // agronomo1
            },
            new Agronomo
            {
                PrimerNombre = "María",
                SegundoNombre = "Elena",
                PrimerApellido = "Rodríguez", 
                SegundoApellido = "López",
                UsuarioId = usuarios[2].Id // agronomo2
            }
        };

        context.Agronomos.AddRange(agronomos);
        context.SaveChanges();

        Console.WriteLine("Datos semilla de usuarios creados exitosamente:");
        Console.WriteLine("- admin / admin123 (administrador)");
        Console.WriteLine("- agronomo1 / agronomo123 (Carlos González)");
        Console.WriteLine("- agronomo2 / agronomo456 (María Rodríguez)");
        Console.WriteLine("- usuario1 / usuario123");
    }

    public static void SeedEnfermedades(ProyectoContext context)
    {
        if (context.Enfermedads.Any())
        {
            return; // Ya existen enfermedades
        }

        var enfermedades = new List<Enfermedad>
        {
            new Enfermedad
            {
                Id = 1,
                Classname = "healthy",
                Nombre = "Sana"
            },
            new Enfermedad
            {
                Id = 2,
                Classname = "early_blight",
                Nombre = "Tizón Temprano"
            },
            new Enfermedad
            {
                Id = 3,
                Classname = "late_blight",
                Nombre = "Tizón Tardío"
            },
            new Enfermedad
            {
                Id = 4,
                Classname = "leaf_mold",
                Nombre = "Moho de la Hoja"
            },
            new Enfermedad
            {
                Id = 5,
                Classname = "septoria_leaf_spot",
                Nombre = "Mancha Foliar por Septoria"
            },
            new Enfermedad
            {
                Id = 6,
                Classname = "spider_mites",
                Nombre = "Ácaros Araña"
            },
            new Enfermedad
            {
                Id = 7,
                Classname = "target_spot",
                Nombre = "Mancha Objetivo"
            },
            new Enfermedad
            {
                Id = 8,
                Classname = "yellow_leaf_curl_virus",
                Nombre = "Virus del Enrollamiento Amarillo"
            },
            new Enfermedad
            {
                Id = 9,
                Classname = "mosaic_virus",
                Nombre = "Virus del Mosaico"
            },
            new Enfermedad
            {
                Id = 10,
                Classname = "bacterial_spot",
                Nombre = "Mancha Bacteriana"
            }
        };

        context.Enfermedads.AddRange(enfermedades);
        context.SaveChanges();

        Console.WriteLine("Datos semilla de enfermedades creados exitosamente:");
        foreach (var enfermedad in enfermedades)
        {
            Console.WriteLine($"- {enfermedad.Nombre} ({enfermedad.Classname})");
        }
    }

    public static void SeedEtapas(ProyectoContext context)
    {
        if (context.Etapas.Any())
        {
            return; // Ya existen etapas
        }

        var etapas = new List<Etapa>
        {
            new Etapa
            {
                Id = 1,
                Nombre = "Pilón"
            },
            new Etapa
            {
                Id = 2,
                Nombre = "Trasplante"
            },
            new Etapa
            {
                Id = 3,
                Nombre = "Crecimiento Vegetativo"
            },
            new Etapa
            {
                Id = 4,
                Nombre = "Floración"
            },
            new Etapa
            {
                Id = 5,
                Nombre = "Fructificación"
            },
            new Etapa
            {
                Id = 6,
                Nombre = "Maduración"
            },
            new Etapa
            {
                Id = 7,
                Nombre = "Cosecha"
            }
        };

        context.Etapas.AddRange(etapas);
        context.SaveChanges();

        Console.WriteLine("Datos semilla de etapas creados exitosamente:");
        foreach (var etapa in etapas)
        {
            Console.WriteLine($"- {etapa.Nombre}");
        }
    }

    public static void SeedTratamientos(ProyectoContext context)
    {
        if (context.Tratamientos.Any())
        {
            return; // Ya existen tratamientos
        }

        var tratamientos = new List<Tratamiento>
        {
            // =====================================================
            // TRATAMIENTOS PARA PLANTAS SANAS (ID 1)
            // =====================================================
            new Tratamiento { CantidadPorPlanta = 0, Nombre = "Monitoreo Preventivo Pilón", Descripcion = "Inspección visual diaria. Mantener humedad 60-70%. Ventilación adecuada. Desinfección de bandejas con hipoclorito 2%.", IdEtapa = 1, IdEnfermedad = 1 },
            new Tratamiento { CantidadPorPlanta = 0, Nombre = "Manejo Preventivo Trasplante", Descripcion = "Aplicar enraizador 5ml/planta. Riego moderado. Evitar estrés. Monitoreo de plagas vectoras.", IdEtapa = 2, IdEnfermedad = 1 },
            new Tratamiento { CantidadPorPlanta = 5, Nombre = "Nutrición Preventiva Vegetativo", Descripcion = "NPK 15-15-15, 5g/planta quincenal. Micronutrientes foliares 2ml/L. Mantener vigor óptimo.", IdEtapa = 3, IdEnfermedad = 1 },
            new Tratamiento { CantidadPorPlanta = 8, Nombre = "Fortalecimiento Preventivo Floración", Descripcion = "Boro + Calcio foliar 8ml/planta. Polinización asistida si necesario. Control preventivo hongos.", IdEtapa = 4, IdEnfermedad = 1 },
            new Tratamiento { CantidadPorPlanta = 10, Nombre = "Protección Preventiva Fructificación", Descripcion = "Potasio 10ml/planta semanal. Calcio para prevenir pudrición apical. Monitoreo constante.", IdEtapa = 5, IdEnfermedad = 1 },
            new Tratamiento { CantidadPorPlanta = 5, Nombre = "Manejo Preventivo Maduración", Descripcion = "Potasio sulfato 5ml/planta. Control de humedad. Ventilación máxima. Cosecha oportuna.", IdEtapa = 6, IdEnfermedad = 1 },
            new Tratamiento { CantidadPorPlanta = 0, Nombre = "Monitoreo Preventivo Cosecha", Descripcion = "Inspección pre-cosecha. Desinfección de herramientas. Manejo cuidadoso. Clasificación por calidad.", IdEtapa = 7, IdEnfermedad = 1 },

            // =====================================================
            // TRATAMIENTOS PARA TIZÓN TEMPRANO (ID 2)
            // =====================================================
            new Tratamiento { CantidadPorPlanta = 2, Nombre = "Control Tizón Temprano Pilón", Descripcion = "Mancozeb 2g/L, 2ml/planta. Síntomas: anillos concéntricos café. Eliminar hojas afectadas. Reducir humedad.", IdEtapa = 1, IdEnfermedad = 2 },
            new Tratamiento { CantidadPorPlanta = 5, Nombre = "Control Tizón Temprano Trasplante", Descripcion = "Azoxistrobina 0.5ml/L, 5ml/planta cada 7 días. Evitar riego aspersión. Mejorar espaciamiento plantas.", IdEtapa = 2, IdEnfermedad = 2 },
            new Tratamiento { CantidadPorPlanta = 8, Nombre = "Control Tizón Temprano Vegetativo", Descripcion = "Clorotalonil 2.5g/L, 8ml/planta cada 10 días. Rotar fungicidas. Eliminar malezas. Poda sanitaria.", IdEtapa = 3, IdEnfermedad = 2 },
            new Tratamiento { CantidadPorPlanta = 10, Nombre = "Control Tizón Temprano Floración", Descripcion = "Difenoconazol 0.5ml/L, 10ml/planta. Proteger flores. Aplicación preventiva crítica. Evitar heridas.", IdEtapa = 4, IdEnfermedad = 2 },
            new Tratamiento { CantidadPorPlanta = 15, Nombre = "Control Tizón Temprano Fructificación", Descripcion = "Propiconazol 1ml/L, 15ml/planta cada 14 días. Proteger frutos. Periodo carencia 21 días.", IdEtapa = 5, IdEnfermedad = 2 },
            new Tratamiento { CantidadPorPlanta = 10, Nombre = "Control Tizón Temprano Maduración", Descripcion = "Bicarbonato potásico 5g/L, 10ml/planta. Producto orgánico pre-cosecha. Control residual.", IdEtapa = 6, IdEnfermedad = 2 },
            new Tratamiento { CantidadPorPlanta = 5, Nombre = "Manejo Tizón Temprano Cosecha", Descripcion = "Extracto cítrico 5ml/planta. Eliminar frutos afectados. Desinfectar área cosecha. Rotación cultivo.", IdEtapa = 7, IdEnfermedad = 2 },

            // =====================================================
            // TRATAMIENTOS PARA TIZÓN TARDÍO (ID 3)
            // =====================================================
            new Tratamiento { CantidadPorPlanta = 2, Nombre = "Control Tizón Tardío Pilón", Descripcion = "Metalaxil 1g/L, 2ml/planta. Síntomas: manchas acuosas irregulares. Aislar plantas. Destruir infectadas.", IdEtapa = 1, IdEnfermedad = 3 },
            new Tratamiento { CantidadPorPlanta = 5, Nombre = "Control Tizón Tardío Trasplante", Descripcion = "Fosetil-Al 2.5g/L, 5ml/planta. Aplicación preventiva obligatoria. Mejorar drenaje. Reducir humedad.", IdEtapa = 2, IdEnfermedad = 3 },
            new Tratamiento { CantidadPorPlanta = 10, Nombre = "Control Tizón Tardío Vegetativo", Descripcion = "Cymoxanil + Mancozeb 2g/L, 10ml/planta cada 5 días. Emergencia fitosanitaria. Eliminar focos.", IdEtapa = 3, IdEnfermedad = 3 },
            new Tratamiento { CantidadPorPlanta = 12, Nombre = "Control Tizón Tardío Floración", Descripcion = "Dimetomorf 0.5g/L, 12ml/planta. Protección sistémica flores. Crítico clima húmedo-frío.", IdEtapa = 4, IdEnfermedad = 3 },
            new Tratamiento { CantidadPorPlanta = 15, Nombre = "Control Tizón Tardío Fructificación", Descripcion = "Mandipropamid 0.4ml/L, 15ml/planta cada 7 días. Salvar producción. Cosecha anticipada si necesario.", IdEtapa = 5, IdEnfermedad = 3 },
            new Tratamiento { CantidadPorPlanta = 12, Nombre = "Control Tizón Tardío Maduración", Descripcion = "Fluopicolide 0.3ml/L, 12ml/planta. Última protección. Cosechar inmediato frutos sanos.", IdEtapa = 6, IdEnfermedad = 3 },
            new Tratamiento { CantidadPorPlanta = 8, Nombre = "Salvamento Tizón Tardío Cosecha", Descripcion = "Fosfito potásico 3g/L, 8ml/planta. Rescate frutos verdes. Maduración controlada. Selección estricta.", IdEtapa = 7, IdEnfermedad = 3 },

            // =====================================================
            // TRATAMIENTOS PARA MOHO DE LA HOJA (ID 4)
            // =====================================================
            new Tratamiento { CantidadPorPlanta = 2, Nombre = "Control Moho Hoja Pilón", Descripcion = "Azufre mojable 3g/L, 2ml/planta. Síntomas: manchas amarillas/moho gris envés. Reducir HR <85%.", IdEtapa = 1, IdEnfermedad = 4 },
            new Tratamiento { CantidadPorPlanta = 5, Nombre = "Control Moho Hoja Trasplante", Descripcion = "Trifloxistrobin 0.15g/L, 5ml/planta. Mejorar circulación aire. Evitar densidad excesiva.", IdEtapa = 2, IdEnfermedad = 4 },
            new Tratamiento { CantidadPorPlanta = 8, Nombre = "Control Moho Hoja Vegetativo", Descripcion = "Ciproconazol 0.5ml/L, 8ml/planta cada 10 días. Poda hojas basales. Desinfectar herramientas.", IdEtapa = 3, IdEnfermedad = 4 },
            new Tratamiento { CantidadPorPlanta = 10, Nombre = "Control Moho Hoja Floración", Descripcion = "Boscalid 0.5g/L, 10ml/planta. No trabajar plantas húmedas. Ventilación máxima invernadero.", IdEtapa = 4, IdEnfermedad = 4 },
            new Tratamiento { CantidadPorPlanta = 12, Nombre = "Control Moho Hoja Fructificación", Descripcion = "Pyraclostrobin 0.2ml/L, 12ml/planta cada 14 días. HR <80%. Defoliación selectiva.", IdEtapa = 5, IdEnfermedad = 4 },
            new Tratamiento { CantidadPorPlanta = 10, Nombre = "Control Moho Hoja Maduración", Descripcion = "Isopirazam 0.3ml/L, 10ml/planta. Proteger frutos maduros. Ventilación constante.", IdEtapa = 6, IdEnfermedad = 4 },
            new Tratamiento { CantidadPorPlanta = 5, Nombre = "Manejo Moho Hoja Cosecha", Descripcion = "Bicarbonato sodio 5g/L, 5ml/planta. Reducir inóculo. Limpieza profunda post-cosecha.", IdEtapa = 7, IdEnfermedad = 4 },

            // =====================================================
            // TRATAMIENTOS PARA SEPTORIA (ID 5)
            // =====================================================
            new Tratamiento { CantidadPorPlanta = 2, Nombre = "Control Septoria Pilón", Descripcion = "Clorotalonil 2g/L, 2ml/planta. Síntomas: manchas circulares centro gris. Eliminar hojas afectadas.", IdEtapa = 1, IdEnfermedad = 5 },
            new Tratamiento { CantidadPorPlanta = 5, Nombre = "Control Septoria Trasplante", Descripcion = "Mancozeb 2.5g/L, 5ml/planta. Evitar salpicaduras suelo. Mulch plástico obligatorio.", IdEtapa = 2, IdEnfermedad = 5 },
            new Tratamiento { CantidadPorPlanta = 8, Nombre = "Control Septoria Vegetativo", Descripcion = "Tebuconazol 0.5ml/L, 8ml/planta cada 7 días. Quemar residuos infectados. Riego por goteo.", IdEtapa = 3, IdEnfermedad = 5 },
            new Tratamiento { CantidadPorPlanta = 10, Nombre = "Control Septoria Floración", Descripcion = "Azoxistrobina + Difenoconazol 1ml/L, 10ml/planta. No mojar follaje. Protección preventiva.", IdEtapa = 4, IdEnfermedad = 5 },
            new Tratamiento { CantidadPorPlanta = 12, Nombre = "Control Septoria Fructificación", Descripcion = "Flutriafol 0.75ml/L, 12ml/planta cada 10 días. Periodo carencia 14 días. Defoliación basal.", IdEtapa = 5, IdEnfermedad = 5 },
            new Tratamiento { CantidadPorPlanta = 10, Nombre = "Control Septoria Maduración", Descripcion = "Penthiopyrad 0.4ml/L, 10ml/planta. Última aplicación química. Preparar rotación.", IdEtapa = 6, IdEnfermedad = 5 },
            new Tratamiento { CantidadPorPlanta = 5, Nombre = "Manejo Septoria Cosecha", Descripcion = "Extractos vegetales 10ml/L, 5ml/planta. Eliminar restos. Solarización post-cosecha.", IdEtapa = 7, IdEnfermedad = 5 },

            // =====================================================
            // TRATAMIENTOS PARA ÁCAROS ARAÑA (ID 6)
            // =====================================================
            new Tratamiento { CantidadPorPlanta = 1, Nombre = "Control Ácaros Pilón", Descripcion = "Jabón potásico 10ml/L, 1ml/planta. Síntomas: punteado amarillo. Aumentar HR >60%.", IdEtapa = 1, IdEnfermedad = 6 },
            new Tratamiento { CantidadPorPlanta = 3, Nombre = "Control Ácaros Trasplante", Descripcion = "Abamectina 0.5ml/L, 3ml/planta. Mojar envés. Liberar Phytoseiulus persimilis.", IdEtapa = 2, IdEnfermedad = 6 },
            new Tratamiento { CantidadPorPlanta = 5, Nombre = "Control Ácaros Vegetativo", Descripcion = "Azufre + aceite neem, 5ml/planta. Aplicar tarde. Evitar estrés hídrico plantas.", IdEtapa = 3, IdEnfermedad = 6 },
            new Tratamiento { CantidadPorPlanta = 8, Nombre = "Control Ácaros Floración", Descripcion = "Bifenazate 0.5ml/L, 8ml/planta cada 5 días. Monitoreo lupa 10x. Alternar acaricidas.", IdEtapa = 4, IdEnfermedad = 6 },
            new Tratamiento { CantidadPorPlanta = 10, Nombre = "Control Ácaros Fructificación", Descripcion = "Spiromesifen 0.5ml/L, 10ml/planta. Proteger enemigos naturales. Evitar piretroides.", IdEtapa = 5, IdEnfermedad = 6 },
            new Tratamiento { CantidadPorPlanta = 8, Nombre = "Control Ácaros Maduración", Descripcion = "Extracto ajo 20ml/L, 8ml/planta. Producto orgánico. Lavar frutos pre-comercialización.", IdEtapa = 6, IdEnfermedad = 6 },
            new Tratamiento { CantidadPorPlanta = 5, Nombre = "Limpieza Ácaros Cosecha", Descripcion = "Agua presión + jabón, 5ml/planta. Eliminar telarañas. Desinfección estructuras.", IdEtapa = 7, IdEnfermedad = 6 },

            // =====================================================
            // TRATAMIENTOS PARA MANCHA OBJETIVO (ID 7)
            // =====================================================
            new Tratamiento { CantidadPorPlanta = 2, Nombre = "Control Mancha Objetivo Pilón", Descripcion = "Propineb 2g/L, 2ml/planta. Síntomas: anillos tipo diana. Desinfectar sustrato.", IdEtapa = 1, IdEnfermedad = 7 },
            new Tratamiento { CantidadPorPlanta = 5, Nombre = "Control Mancha Objetivo Trasplante", Descripcion = "Carbendazim 0.5g/L, 5ml/planta. Evitar heridas. pH suelo 6.0-6.5.", IdEtapa = 2, IdEnfermedad = 7 },
            new Tratamiento { CantidadPorPlanta = 8, Nombre = "Control Mancha Objetivo Vegetativo", Descripcion = "Fluxapyroxad 0.2ml/L, 8ml/planta cada 10 días. Eliminar hospederos. Mejorar drenaje.", IdEtapa = 3, IdEnfermedad = 7 },
            new Tratamiento { CantidadPorPlanta = 10, Nombre = "Control Mancha Objetivo Floración", Descripcion = "Picoxystrobin 0.25g/L, 10ml/planta. Proteger flores. Balance nutricional.", IdEtapa = 4, IdEnfermedad = 7 },
            new Tratamiento { CantidadPorPlanta = 12, Nombre = "Control Mancha Objetivo Fructificación", Descripcion = "Boscalid + Pyraclostrobin 1g/L, 12ml/planta cada 14 días. Protección frutos.", IdEtapa = 5, IdEnfermedad = 7 },
            new Tratamiento { CantidadPorPlanta = 10, Nombre = "Control Mancha Objetivo Maduración", Descripcion = "Fluopyram 0.3ml/L, 10ml/planta. Última protección química. Preparar limpieza.", IdEtapa = 6, IdEnfermedad = 7 },
            new Tratamiento { CantidadPorPlanta = 5, Nombre = "Manejo Mancha Objetivo Cosecha", Descripcion = "Trichoderma 5g/L, 5ml/planta. Control biológico. Eliminación residuos.", IdEtapa = 7, IdEnfermedad = 7 },

            // =====================================================
            // TRATAMIENTOS PARA VIRUS TYLCV (ID 8)
            // =====================================================
            new Tratamiento { CantidadPorPlanta = 0, Nombre = "Prevención TYLCV Pilón", Descripcion = "Semilla certificada libre virus. Síntomas: enrollamiento/amarillamiento. Eliminar infectadas.", IdEtapa = 1, IdEnfermedad = 8 },
            new Tratamiento { CantidadPorPlanta = 2, Nombre = "Control Vector TYLCV Trasplante", Descripcion = "Imidacloprid 0.5g/L, 2ml/planta drench. Mallas antiáfidos. Trampas amarillas.", IdEtapa = 2, IdEnfermedad = 8 },
            new Tratamiento { CantidadPorPlanta = 5, Nombre = "Control Vector TYLCV Vegetativo", Descripcion = "Thiamethoxam 0.3g/L, 5ml/planta. Trampas 1/25m². Eliminar malezas hospederas.", IdEtapa = 3, IdEnfermedad = 8 },
            new Tratamiento { CantidadPorPlanta = 8, Nombre = "Control Vector TYLCV Floración", Descripcion = "Acetamiprid 0.2g/L + aceite, 8ml/planta. Aplicar 6am. Monitoreo mosca blanca.", IdEtapa = 4, IdEnfermedad = 8 },
            new Tratamiento { CantidadPorPlanta = 10, Nombre = "Manejo TYLCV Fructificación", Descripcion = "Spiromesifen 0.5ml/L, 10ml/planta. No hay cura. Proteger plantas sanas.", IdEtapa = 5, IdEnfermedad = 8 },
            new Tratamiento { CantidadPorPlanta = 8, Nombre = "Manejo TYLCV Maduración", Descripcion = "Piriproxifen 0.3ml/L, 8ml/planta. Control vectores. Cosecha anticipada posible.", IdEtapa = 6, IdEnfermedad = 8 },
            new Tratamiento { CantidadPorPlanta = 0, Nombre = "Eliminación TYLCV Cosecha", Descripcion = "Arrancar plantas completas. Quemar residuos. Solarización. Vacío sanitario 30 días.", IdEtapa = 7, IdEnfermedad = 8 },

            // =====================================================
            // TRATAMIENTOS PARA VIRUS MOSAICO (ID 9)
            // =====================================================
            new Tratamiento { CantidadPorPlanta = 0, Nombre = "Prevención Mosaico Pilón", Descripcion = "Desinfectar semillas fosfato trisódico 10%. Síntomas: mosaico verde claro-oscuro.", IdEtapa = 1, IdEnfermedad = 9 },
            new Tratamiento { CantidadPorPlanta = 0, Nombre = "Prevención Mosaico Trasplante", Descripcion = "Desinfectar herramientas hipoclorito 2%. No fumar. Lavado manos obligatorio.", IdEtapa = 2, IdEnfermedad = 9 },
            new Tratamiento { CantidadPorPlanta = 0, Nombre = "Manejo Mosaico Vegetativo", Descripcion = "Eliminar infectadas. Leche descremada 100ml/L preventivo. No hay cura.", IdEtapa = 3, IdEnfermedad = 9 },
            new Tratamiento { CantidadPorPlanta = 5, Nombre = "Fortalecimiento Mosaico Floración", Descripcion = "Aminoácidos 2ml/L, 5ml/planta. Control áfidos vectores. Barreras físicas.", IdEtapa = 4, IdEnfermedad = 9 },
            new Tratamiento { CantidadPorPlanta = 8, Nombre = "Manejo Mosaico Fructificación", Descripcion = "Extractos vegetales 10ml/L, 8ml/planta. Cosechar frutos aunque planta afectada.", IdEtapa = 5, IdEnfermedad = 9 },
            new Tratamiento { CantidadPorPlanta = 5, Nombre = "Soporte Mosaico Maduración", Descripcion = "Bioestimulantes 5ml/planta. Acelerar maduración. Preparar eliminación plantas.", IdEtapa = 6, IdEnfermedad = 9 },
            new Tratamiento { CantidadPorPlanta = 0, Nombre = "Eliminación Mosaico Cosecha", Descripcion = "Arrancar plantas. Incinerar residuos. Desinfección total. Rotación obligatoria.", IdEtapa = 7, IdEnfermedad = 9 },

            // =====================================================
            // TRATAMIENTOS PARA MANCHA BACTERIANA (ID 10)
            // =====================================================
            new Tratamiento { CantidadPorPlanta = 2, Nombre = "Control Bacteriano Pilón", Descripcion = "Oxicloruro cobre 3g/L, 2ml/planta. Síntomas: manchas angulares acuosas. Evitar heridas.", IdEtapa = 1, IdEnfermedad = 10 },
            new Tratamiento { CantidadPorPlanta = 5, Nombre = "Control Bacteriano Trasplante", Descripcion = "Sulfato cobre 2g/L, 5ml/planta. Preventivo clima húmedo. Manipulación mínima.", IdEtapa = 2, IdEnfermedad = 10 },
            new Tratamiento { CantidadPorPlanta = 8, Nombre = "Control Bacteriano Vegetativo", Descripcion = "Kasugamicina 2ml/L, 8ml/planta cada 7 días. Poda sanitaria. Desinfección constante.", IdEtapa = 3, IdEnfermedad = 10 },
            new Tratamiento { CantidadPorPlanta = 10, Nombre = "Control Bacteriano Floración", Descripcion = "Estreptomicina 0.5g/L + cobre, 10ml/planta. Proteger flores. No trabajar húmedo.", IdEtapa = 4, IdEnfermedad = 10 },
            new Tratamiento { CantidadPorPlanta = 12, Nombre = "Control Bacteriano Fructificación", Descripcion = "Hidróxido cúprico 2g/L, 12ml/planta cada 10 días. Proteger frutos. Carencia 7 días.", IdEtapa = 5, IdEnfermedad = 10 },
            new Tratamiento { CantidadPorPlanta = 8, Nombre = "Control Bacteriano Maduración", Descripcion = "Bicarbonato potásico 5g/L, 8ml/planta. pH alcalino bacteriostático. Orgánico.", IdEtapa = 6, IdEnfermedad = 10 },
            new Tratamiento { CantidadPorPlanta = 5, Nombre = "Limpieza Bacteriana Cosecha", Descripcion = "Yodo agrícola 2ml/L, 5ml/planta. Desinfección área. Eliminación focos.", IdEtapa = 7, IdEnfermedad = 10 }
        };

        context.Tratamientos.AddRange(tratamientos);
        context.SaveChanges();

        Console.WriteLine("Datos semilla de tratamientos creados exitosamente:");
        Console.WriteLine($"- {tratamientos.Count} tratamientos creados para todas las enfermedades y etapas");
        Console.WriteLine("- Tratamientos disponibles desde pilón hasta cosecha");
        Console.WriteLine("- Incluye tratamientos preventivos y curativos específicos");
    }
}