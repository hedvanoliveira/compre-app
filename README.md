# API DE COMPRA DE APLICATIVOS

## Como inicializar o ambiente do projeto
Execute o comando `docker compose up` na raiz do projeto. Serão criados os containers para os serviços:

* Seq: Centralização de log estruturado
* Redis: Banco de dados de cache para armazenas as consultas
* SqlServer: Banco de dados usado para o armazenamento dos dados da API
* API: Projeto desenvolvido com .NET 8

## Acessos
Acessar o swagger da API:
http://localhost:5010/swagger/index.html

Acessar o painel do Seq para visualização dos logs da API:
http://localhost:5341/


## Cartões de teste

A Aplicação usa a API de mock https://wiremock.org/ para simular o pagamento com cartão de crédito. Cada número de cartão permite retorna um tipo de situação:

* "Erro na integração de pagamento", 
Número do cartão:
`5206930450320899`

* "Não Autorizado", 
Número do cartão:
`5387225898301960`

* "Pagamento confirmado", 
Número do cartão:
`5480145660187635`

*Obs: A aplicação inclui um mecanismo de resiliência para realizar novas tentativas de acesso à API externa em caso de erros que indiquem possível instabilidade no serviço.*

## Fluxo da API

* Cadastrar o cliente com endereço: 
`POST /api/clientes/cadastrar`

* Gerar o token de autenticação do cliente: 
`POST /api/clientes/autenticar`

* Consultar os aplicativos disponíveis para compra: 
`GET /api/aplicativos`

* Realizar a compra do aplicativo: 
`POST /api/pedidos/cadastrar`

* Consultar aplicativos comprados: 
`GET /api/pedidos`

