using CompreApp.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CompreApp.Infra.Data.SeedData;

public class MigracaoData(IServiceProvider serviceProvider, ILogger<MigracaoData> logger)
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;
    private readonly ILogger<MigracaoData> _logger = logger;

    public void AplicarMigracao()
    {
        using var scope = _serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApiDbContext>();

        try
        {
            _logger.LogInformation("Iniciar migração");
            dbContext.Database.Migrate();
            _logger.LogInformation("Finalizar migração");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro durante a migração");
            throw;
        }
    }
}