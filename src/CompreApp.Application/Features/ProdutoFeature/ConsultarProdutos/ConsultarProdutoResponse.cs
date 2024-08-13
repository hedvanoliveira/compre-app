using System;

namespace CompreApp.Application.Features.ProdutoFeature.ConsultarProdutos;

public record ConsultarProdutoResponse(Guid Id, string Nome, string Descricao);

