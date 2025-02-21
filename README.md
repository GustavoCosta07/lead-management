# Lead Management - Setup e Execução

## Sobre o Projeto
Este é um sistema de gerenciamento de leads composto por um frontend em React, um backend em .NET 6.0 e um banco de dados SQL Server. O ambiente é totalmente containerizado utilizando Docker.

## Como Rodar o Projeto
Antes de tudo, certifique-se de ter o [Docker](https://www.docker.com/) instalado na sua máquina.

### 1. Subir os Containers
Para iniciar o projeto, execute o seguinte comando na raiz do repositório onde se encontra o arquivo docker-compose.yaml:
```sh
docker-compose up --build
```

Esse comando vai baixar as dependências necessárias, configurar os containers e iniciar os serviços. O tempo de inicialização pode variar dependendo do hardware da sua máquina e da velocidade da sua conexão com a internet.

> **Dica:** Se for a primeira vez rodando o projeto, pode levar alguns minutos até que tudo esteja pronto. O Docker irá baixar as imagens necessárias e instalar dependências.

Caso queira rodar os containers em segundo plano, sem travar o terminal, utilize a opção `-d`:
```sh
docker-compose up -d --build
```
Dessa forma, os containers continuarão rodando em segundo plano e você poderá acompanhar os logs diretamente pelo Docker Desktop ou visualizar os logs via terminal com:
```sh
docker-compose logs -f
```

### 2. Acessar o Projeto
- **Frontend:** Acesse [http://localhost:3000](http://localhost:3000)
- **Backend:** A API está disponível em [http://localhost:5000](http://localhost:5000)
- **Banco de Dados:** O SQL Server roda na porta `1433`

Caso queira acessar o banco de dados diretamente, você pode utilizar ferramentas como [DBeaver](https://dbeaver.io/) ou SQL Server Management Studio (SSMS). As credenciais padrão do banco são:
- **Usuário:** `sa`
- **Senha:** `Admin@123`

## Testando a API via Postman
A API do backend possui quatro endpoints principais que podem ser testados utilizando o [Postman](https://www.postman.com/) ou qualquer outra ferramenta de requisições HTTP:

- **GET /api/leads** - Retorna todos os leads cadastrados.
  ```sh
  GET http://localhost:5000/api/leads
  ```

- **GET /api/leads/{status}/status** - Retorna os leads filtrados pelo status (`Accepted` ou `Invited`).
  ```sh
  GET http://localhost:5000/api/leads/{status}/status
  ```

- **POST /api/leads/{leadId}/accept** - Aceita um lead específico.
  ```sh
  POST http://localhost:5000/api/leads/{guid}/accept
  ```

- **POST /api/leads/{leadId}/decline** - Recusa um lead específico.
  ```sh
  POST http://localhost:5000/api/leads/{guid}/decline
  ```

Esses endpoints permitem visualizar e gerenciar os leads cadastrados no sistema.


## Executando Testes
O projeto inclui testes automatizados para o frontend e o backend. Eles podem ser executados manualmente da seguinte forma:

### Testes do Frontend
```sh
docker-compose run --rm frontend-tests
```
Isso irá rodar os testes dentro do container e exibir os resultados no terminal.

### Testes do Backend
```sh
docker-compose run --rm backend-tests
```
Os testes do backend são executados usando o `dotnet test`, e os resultados também serão exibidos no terminal.

## Considerações Finais
Se algum dos containers demorar a subir, não se preocupe! O Docker pode levar alguns instantes para iniciar todos os serviços corretamente. Caso precise reiniciar, basta parar a execução com `CTRL+C` e rodar `docker-compose up --build` novamente.

Se encontrar algum erro, verifique os logs do Docker para mais informações:
```sh
docker-compose logs -f
```

### Resetando os Dados do Banco
Os usuários exibidos no frontend são gerados automaticamente quando o projeto é buildado. Caso as interações se esgotem, basta rodar o build novamente para que o banco de dados volte ao estado inicial:
```sh
docker-compose down -v
```
```sh
docker-compose up --build
```
Isso removerá os volumes do banco de dados e restaurará os dados iniciais.

