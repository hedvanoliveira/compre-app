using CompreApp.Domain.Entities;
using CompreApp.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace CompreApp.Infra.Data.SeedData;

public static class ModelBuilderExtensions
{
    public static void Seed(this ModelBuilder modelBuilder)
    {
        List<Produto> produtos = [];
        DateTime dataCadastro = DateTime.UtcNow;
        produtos.Add(new(Guid.NewGuid(), "Adobe Photoshop", "O Adobe Photoshop é um software de edição de imagens bidimensionais do tipo raster integrante do pacote Adobe.", 400, dataCadastro, SituacaoCadastroEnum.Ativo));
        produtos.Add(new(Guid.NewGuid(), "AutoCAD", "AutoCAD é um software do tipo CAD — computer aided design ou desenho auxiliado por computador.", 250, dataCadastro, SituacaoCadastroEnum.Ativo));
        produtos.Add(new(Guid.NewGuid(), "Final Cut Pro", "Final Cut Pro é um software profissional de edição de vídeo não linear.", 300, dataCadastro, SituacaoCadastroEnum.Ativo));
        produtos.Add(new(Guid.NewGuid(), "Lightroom", "O Adobe Photoshop Lightroom é um software designado a edição rápida e o armazenamento de fotos digitais.", 150, dataCadastro, SituacaoCadastroEnum.Ativo));
        modelBuilder.Entity<Produto>().HasData(produtos);
    }
}
