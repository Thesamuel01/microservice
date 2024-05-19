# Projeto de Micro Serviço de Produto

## Para rodar o projeto:

- **Asp Net Core** >= 6.0
- **Postgres** >= 14.12

Antes de iniciar o projeto, configure as variáveis de ambiente contidas no arquivo `appsettings` (ambos os arquivos):

- `User ID=root` -> O usuário do banco de dados
- `Password=myPassword` -> A senha do usuário do banco de dados
- `Host=localhost` -> (Opcional) Caso o banco de dados esteja em outro host

## Executar o Postgres via Docker
Caso não tenha um Postgres server em seu host, o arquivo Dockerfile possui todas as configurações para rodar o Postgres via container. Basta adicionar as mesmas credenciais inseridas no arquivo appSettings:

 - POSTGRES_USER
 - POSTGRES_PASSWORD

Após inserir suas credênciais execute os seguintes comandos:
```bash
   # Build da imagem
   docker build -t my-postgres:1.0 .

   # Criar o container
   docker run --name my-postgres-container -d -p 5432:5432 my-postgres:1.0
```


## Para rodar o projeto há duas opções:

1. **Pela IDE Visual Studio**:
   - Com o Visual Studio instalado em sua máquina. Clique no arquivo `ProductAPI.csproj.user` e após isso, clique no botão de executar

2. **Pela linha de comando**, basta executar os comandos abaixo:
```bash
   # Restore as dependências do projeto
   dotnet restore

   # Compile o projeto
   dotnet build

   # Execute o projeto
   dotnet run --project ProductAPI
```


## Padrões Mapeados
 - Factory: Para lidar com as conexões do banco de dados.
 - Singleton: Para garantir que haja somente uma instância da classe de factories.
 - Repository Pattern: Utilizado para as classes que possuem acesso ao banco de dados.
 - Data Transfer Object: Utilizado para as tranferências de dados entre as camadas.


### Boas Práticas
SOLID: Utilizando conceitos do SOLID para desenvolvimento dos controllers, services e afins.
