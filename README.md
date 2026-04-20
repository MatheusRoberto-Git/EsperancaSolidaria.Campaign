# EsperancaSolidaria.Campanha API

Microsserviço responsável pela gestão de campanhas e processamento de intenções de doação da plataforma **Conexão Solidária**.

## Responsabilidades

- Criação e edição de campanhas (GestorONG)
- Listagem pública de campanhas ativas com valor arrecadado
- Recebimento de intenções de doação (Doador)
- Publicação de eventos de doação no RabbitMQ

## Tecnologias

- .NET 10
- SQL Server (FluentMigrator + EF Core)
- RabbitMQ (publicação de eventos)
- JWT (validação cross-service)
- Sqids para ofuscação de IDs
- Mapster para mapeamento
- FluentValidation
- Prometheus (métricas)

## Estrutura do Projeto

```
src/
├── Backend/
│   ├── EsperancaSolidaria.Campanha.API           # Controllers, Filters, Program.cs
│   ├── EsperancaSolidaria.Campanha.Application   # Use Cases, Validators, Mapster
│   ├── EsperancaSolidaria.Campanha.Domain        # Entidades, Interfaces, Events
│   └── EsperancaSolidaria.Campanha.Infrastructure # EF Core, RabbitMQ, JWT, Migrations
└── Shared/
    ├── EsperancaSolidaria.Campanha.Communication  # Requests e Responses
    └── EsperancaSolidaria.Campanha.Exceptions     # Exceções customizadas
```

## Endpoints

| Método | Rota | Acesso | Descrição |
|--------|------|--------|-----------|
| POST | `/campaign` | GestorONG | Criar campanha |
| PUT | `/campaign/{id}` | GestorONG | Editar campanha |
| GET | `/campaign` | Público | Listar campanhas ativas |
| GET | `/campaign/campanha-gestor` | GestorONG | Listar todas as campanhas |
| POST | `/campaign/donation` | Doador | Registrar intenção de doação |

## Fluxo de Doação

```
Doador → POST /campaign/donation
       → Valida JWT e role
       → Verifica campanha ativa
       → Publica DonationReceivedEvent no RabbitMQ
       → Retorna 204 NoContent
       
RabbitMQ → Worker consome o evento
         → Atualiza AmountRaised no banco
```

## Como Rodar Localmente

### Pré-requisitos

- .NET 10 SDK
- SQL Server (local ou Docker)
- RabbitMQ rodando na porta 5672
- Visual Studio 2022 ou VS Code

### Configuração

1. Clone o repositório:
```bash
git clone https://github.com/MatheusRoberto-Git/EsperancaSolidaria.Campanha.git
cd EsperancaSolidaria.Campanha
```

2. Configure o `appsettings.Development.json`:
```json
{
  "ConnectionStrings": {
    "Connection": "Data Source=localhost\\SQLEXPRESS;Initial Catalog=EsperancaSolidariaCampaign;User Id=seu_usuario;Password=sua_senha;TrustServerCertificate=True;"
  },
  "Settings": {
    "Jwt": {
      "SigningKey": "sua_chave_minimo_32_caracteres"
    },
    "RabbitMq": {
      "HostName": "localhost",
      "UserName": "guest",
      "Password": "guest"
    },
    "IdCryptographyAlphabet": "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"
  }
}
```

> A `SigningKey` deve ser a mesma configurada no Identity API.

3. Suba o RabbitMQ com Docker:
```bash
docker run -d -p 5672:5672 -p 15672:15672 rabbitmq:3-management
```

4. Execute o projeto:
```bash
dotnet run --project src/Backend/EsperancaSolidaria.Campanha.API
```

5. Acesse o Swagger:
```
http://localhost:5002/swagger
```

> As migrations são executadas automaticamente na inicialização.

### Rodar com Docker

```bash
docker build -t campanha-api:latest .
docker run -p 5002:8080 \
  -e ConnectionStrings__Connection="Server=host.docker.internal\\SQLEXPRESS;..." \
  -e Settings__Jwt__SigningKey="sua_chave" \
  -e Settings__RabbitMq__HostName="localhost" \
  campanha-api:latest
```

### Rodar com Docker Compose

Na raiz da pasta `Hackathon FIAP`:
```bash
docker-compose up campanha-api
```

## Health Check

```
GET /health
```

Retorna `Healthy` quando o serviço e o banco estão operacionais.

## Métricas

```
GET /metrics
```

Expõe métricas no formato Prometheus para coleta pelo Prometheus Server.

## Kubernetes

```bash
kubectl apply -f k8s/
kubectl get pods -l app=campanha-api
```

## Status de Campanha

| Status | Valor | Descrição |
|--------|-------|-----------|
| Ativa | 1 | Campanha aceitando doações |
| Concluida | 2 | Campanha encerrada com sucesso |
| Cancelada | 3 | Campanha cancelada |

> Transições de status são irreversíveis. Uma campanha cancelada ou concluída não pode voltar para Ativa.

## Variáveis de Ambiente

| Variável | Descrição |
|----------|-----------|
| `ConnectionStrings__Connection` | Connection string do SQL Server |
| `Settings__Jwt__SigningKey` | Chave de assinatura do JWT (mesma do Identity) |
| `Settings__RabbitMq__HostName` | Host do RabbitMQ |
| `Settings__RabbitMq__UserName` | Usuário do RabbitMQ |
| `Settings__RabbitMq__Password` | Senha do RabbitMQ |
| `Settings__IdCryptographyAlphabet` | Alfabeto para Sqids |