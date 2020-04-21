### Autor: Fábio Delgado

Olá! Seja bem vindo ;)

## Índice
1. [ApiCadastro](#ApiCadastro)
2. [Projeto e Conteúdo](#Projeto-e-Conteudo)
3. [Swagger](#Swagger)
4. [JWT](#JWT)
5. [SQL Server e ADO.NET](#SQL-Server-e-ADONET)
6. [Publicação](#Publicação)
7. [Suporte](#Suporte)

## ApiCadastro

Este repositório contém uma implementação que orientará você na criação de um aplicativo de básico contendo autenticação, documentação e integração com banco de dados.

### Como executar essa aplicação?
Para executar essa aplicação, primeiro é necessário instalar o .NET Core. Depois disso, você deve seguir os passos abaixo:
1. Clone ou faça o download deste repositório.
2. Extraia o conteúdo se o download for um arquivo zip. Verifique se os arquivos estão com read-write.
3. Execute o comando abaixo no prompt de comando.
```shell
dotnet run
```
4. A aplicação deverá estar disponivel em seu navegador no endereço: https://localhost:5001/swagger

![GitHub Logo](/img/CapturarTela.PNG)

## Projeto e Conteúdo

### Entedento a estrutura de projeto de uma aplicação em ASP.NET Core MVC

O ASP.NET Core é framework cross-platform com código-fonte aberto para a criação de aplicativos modernos. Você pode ler os Fundamentos básicos do ASP.NET na documentação da Microsoft. O padrão de arquitetura MVC (Model-View-Controller) separa um aplicativo em três grupos principais de componentes: Model-View-Controller (MVC) . Esse padrão ajuda a alcançar o princípio de designer de separação de interesses.

![MVC](/img/MVC.png)

O `Model` em um aplicativo MVC representa o estado do aplicativo e qualquer lógica de negócios ou operações que devem ser executadas por ele.

A `View` é responsável ​​por apresentar o conteúdo por meio da interface do usuário. Deve haver lógica mínima nas visualizações, e qualquer lógica nelas deve estar relacionada à apresentação de conteúdo.

O `Controller` é responsável por receber todas as requisições do usuário. Seus métodos chamados actions são responsáveis por uma página, controlando qual model usar e qual view será mostrado ao usuário.

`launchSettings.json` descreve como um projeto pode ser executado. Ele descreve o comando a ser executado, se o navegador deve ser aberto, quais variáveis ​​de ambiente devem ser definidas e assim por diante.

![launchSettings.json](/img/launchSettings.png)

`appsettings.json` é usado para armazenar informações como cadeias de conexão ou configurações específicas do aplicativo e elas são armazenadas no formato JSON, como sugere a extensão do arquivo. (Se você estiver familiarizado com o ASP.NET MVC, poderá notar que a função desse arquivo é semelhante ao Web.config)

![appsettings.json](/img/appsettings.png)

`Program.cs` é o principal ponto de entrada para o aplicativo. Em seguida, ele vai para a classe Startup.cs para finalizar a configuração do aplicativo.

![Program.cs](/img/Program.png)

`Startup.cs` possui os Configure e ConfigureServices methods e é acionado pelo Program.cs

![Startup.cs](/img/Startup.png)

O método `ConfigureServices` é um local onde você pode registrar suas classes dependentes com o  built-in IoC container (o ASP.NET Core refere-se à class as a Service). Após registrar a classe dependente, ela pode ser usada em qualquer lugar do aplicativo. Você só precisa incluí-lo no parâmetro do construtor de uma classe em que deseja usá-lo. O IoC container o injetará automaticamente.

O método `Configure` é usado para especificar como o aplicativo responde às solicitações HTTP. O pipeline de solicitação é configurado adicionando componentes de middleware a uma instância do `IApplicationBuilder`. O  `IApplicationBuilder` está disponível para o método `Configure`, mas não está registrado no contêiner de serviço. A hospedagem cria um `IApplicationBuilder` e o passa diretamente para o método `Configure`.

A sequência de execução do aplicativo é a seguinte:

![Fluxo](/img/fluxo.png)

## Swagger

O Swagger é uma aplicação open source que auxilia os desenvolvedores a definir, criar, documentar e consumir APIs REST;
É composto de um arquivo de configuração, que pode ser definido em YAML ou JSON;
Fornece ferramentas para: auxiliar na definição do arquivo de configuração (Swagger Editor), interagir com API através das definições do arquivo de configuração (Swagger UI) e gerar templates de código a partir do arquivo de configuração (Swagger Codegen).

fonte: https://swagger.io/resources/webinars/getting-started-with-swagger/

> Para importar esse componente para seu projeto, basta executar no prompt o comando abaixo ou utilizar o Nuget 

```shell
dotnet add package Swashbuckle.AspNetCore
```
#### Adicionando e connfigurando o Swagger middleware

> Na classe `Startup`, importe o seguinte namespace para utilização da classe `OpenApiInfo`:

```C#
using Microsoft.OpenApi.Models;
```

> Adicione o gerador Swagger à coleção de serviços no método Startup.ConfigureServices:

```C#
public void ConfigureServices(IServiceCollection services)
{
    services.AddDbContext<TodoContext>(opt =>
        opt.UseInMemoryDatabase("TodoList"));
    services.AddControllers();

    // Register the Swagger generator, defining 1 or more Swagger documents
    services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
    });
}
```

> No método Startup.Configure, ative o middleware para servir o documento JSON gerado e a interface do usuário do Swagger:

```C#
public void Configure(IApplicationBuilder app)
{
    // Enable middleware to serve generated Swagger as a JSON endpoint.
    app.UseSwagger();

    // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
    // specifying the Swagger JSON endpoint.
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    });

    app.UseRouting();
    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
    });
}
```

## JWT
O JWT (JSON Web Token) nada mais é que um padrão (RFC-7519) de mercado que define como transmitir e armazenar objetos JSON de forma simples, compacta e segura entre diferentes aplicações, muito utilizado para validar serviços em Web Services pois os dados contidos no token gerado pode ser validado a qualquer momento uma vez que ele é assinado digitalmente.

JSON Web Tokens (JWT) é um padrão stateless porque o servidor autorizador não precisa manter nenhum estado; o próprio token é sulficiente para verificar a autorização de um portador de token.

Os JWTs são assinados usando um algoritmo de assinatura digital (por exemplo, RSA) que não pode ser forjado. Por isso, qualquer pessoa que confie no certificado do assinante pode confiar com segurança que o JWT é autêntico. Não há necessidade de um servidor consultar o servidor emissor de token para confirmar sua autenticidade.

fonte: https://jwt.io/introduction/

> Para importar esse componente para seu projeto, basta executar no prompt o comando abaixo ou utilizar o Nuget 

```shell
dotnet add package System.IdentityModel.Tokens.Jwt
```
## SQL Server e ADO.NET

O Entity Framework é uma ferramenta ORM da Microsoft madura e testada pelo mercado que pode ser usada para aplicações que usam o .NET Framework.

Ela não é a bala de prata que vai resolver todos os seus problemas de acesso a banco de dados relacionais e seu uso deve ser considerado dependendo da sua expectativa e o cenário do seu ambiente de desenvolvimento. Nessa implementação foi utilizado o ADO.NET para manipulação da base de dados SQL Server através de store procedures.

O ADO.NET fornece acesso consistente a fontes de dados como o SQL Server e o XML, e a fontes de dados expostas através do OLE DB e do ODBC. Os aplicativos do consumidor de compartilhamento de dados podem usar o ADO.NET para se conectar a essas fontes de dados, e para recuperar, manipular e atualizar os dados nelas contidos.

Stored Procedure, que traduzido significa Procedimento Armazenado, é uma conjunto de comandos em SQL que podem ser executados de uma só vez, como em uma função. Ele armazena tarefas repetitivas e aceita parâmetros de entrada para que a tarefa seja efetuada de acordo com a necessidade individual. Nesse projeto foram desenvolvidos. As Stored Procedures implementadas nesse projeto são: 

- sp_Clientes_InsertValue
- sp_Clientes_GetValueById
- sp_Clientes_GetAllValues
- sp_Clientes_DeleteValue

## Publicação

Executáveis não são multiplataformas. São específicos para um sistema operacional e arquitetura de CPU. Ao publicar seu aplicativo e criar um executável, você pode publicar o aplicativo como independente ou dependente de tempo de execução. 
Você pode criar um executável para `-r <RID> --self-contained false` uma plataforma dotnet publish específica passando os parâmetros para o comando. Quando `-r` o parâmetro é omitido, um executável é criado para sua plataforma atual. Todos os pacotes NuGet que tenham dependências específicas da plataforma para a plataforma-alvo são copiados para a pasta de publicação.

```shell
dotnet publish -c Release -r win-x64 --self-contained true
```

## Suporte

Por favor entre em contato conosco via [Email]