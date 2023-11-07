# [TechChallenge2](https://github.com/JairJr/TechChallenge2/edit/main/README.md)

## Introdução
- Projeto criado para o Tech Challenge dotnet da Fase 2 da PosTech em Arquitetura de Sistemas .Net com Azure da FIAP.
- Objetivo:
  1. Os endpoints que fazem gerenciamento das notícias devem ser autenticados com JWT ou Identity.
  2. O projeto deve ter um endpoint REST para a listagem das notícias via Get (retornando todas) ou GetById, retornando um através do seu Id.
  3. Como ORM, você deve utilizar o Entity Framework Core.
  4. Ela deve utilizar uma pipeline de integração e entrega contínua (CI/CD) no Azure DevOps ou no Github Actions, de modo a automatizar o processo de implantação.
  5. Os artefatos devem ser publicados em uma VM ou container com ACR e ACI.
 
  detalhes no [video](https://youtu.be/QJZsYL85JeM)
  
## Funcionalidades

### Autenticação e autorização
- Utilizamos O Identity para gerenciar os usuários que se cadastram e logam na aplicação, devolvendo ao usuário um bearer token para utilização dos demais endpoints.
  ![image](https://github.com/JairJr/TechChallenge2/assets/29376086/25f7e58c-dcf5-492c-9736-e1f9edd7e739)


### Gerenciamento de Noticias
- Endpoints para manutenção de noticias (CRUD).
  ![image](https://github.com/JairJr/TechChallenge2/assets/29376086/3850f337-ceca-4cf5-85ba-f32a90fa946a)

### ElmahCore para observabilidade de possiveis falhas ou erros
  ![image](https://github.com/JairJr/TechChallenge2/assets/29376086/c9fa0bb7-c340-46ee-88df-b4716551f0fb)


### Docker para publicação da solução
  ![image](https://github.com/JairJr/TechChallenge2/assets/29376086/587c7802-0697-4090-8e0a-a83268e5f543)
- para que o projeto possa ser executado no docker é necessário acessar a pasta NoticiasAPI e executar os seguintes comandos através do terminal:
- docker build . -t noticiaapi
- docker run -p 8080:5000 -e ASPNETCORE_ENVIRONMENT=Development noticiaapi --name noticiaapi


### Documentações e Referencias 
- Docker [Documentação do docker para .NET](https://docs.docker.com/language/dotnet/build-images/)  
- ElmahCore [Documentação do ElmahCore](https://github.com/ElmahCore/ElmahCore)




