using Microsoft.EntityFrameworkCore;
using BhBusMetropApi.Data;
using BhBusMetropApi.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
namespace BhBusMetropApi;

public static class OnibusEndpoints
{
    public static void MapOnibusEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Onibus").WithTags(nameof(Onibus));

        group.MapGet("/", async (BhBusMetropApiContext db) =>
        {
            return await db.Onibus.ToListAsync();
        })
        .WithName("GetAllOnibus")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<Onibus>, NotFound>> (int id, BhBusMetropApiContext db) =>
        {
            return await db.Onibus.AsNoTracking()
                .FirstOrDefaultAsync(model => model.Id == id)
                is Onibus model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetOnibusById")
        .WithOpenApi();

        group.MapPut("/onibus/{id}", async (BhBusMetropApiContext db, Onibus updateonibus, int id) =>
        {
            var onibus = await db.Onibus.FindAsync(id);
            if (onibus is null) return Results.NotFound();
            onibus.Nome = updateonibus.Nome;
            onibus.Numero = updateonibus.Numero;
            onibus.Cor = updateonibus.Cor;
            onibus.Tipo = updateonibus.Tipo;
            onibus.Peso = updateonibus.Peso;
            await db.SaveChangesAsync();
            return Results.NoContent();
        });

        group.MapPost("/", async (Onibus onibus, BhBusMetropApiContext db) =>
        {
            db.Onibus.Add(onibus);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Onibus/{onibus.Id}",onibus);
        })
        .WithName("CreateOnibus")
        .WithOpenApi();

        group.MapDelete("/onibus/{id}", async (BhBusMetropApiContext db, int id) =>
        {
            var onibus = await db.Onibus.FindAsync(id);
            if (onibus is null)
            {
                return Results.NotFound();
            }
            db.Onibus.Remove(onibus);
            await db.SaveChangesAsync();
            return Results.Ok();
        });
    }
}
