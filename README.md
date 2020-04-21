### Autor: Fábio Delgado

Bem-vindo! Aqui ol

## Índice
1. [Nome](#nome)
2. [Projeto e Conteúdo](#conteudo)
3. [Ícone](#icone)
4. [#Suporte](#Suporte)

## Projeto e Conteúdo

Implementação simples de API utilizando Swagger + JWT Authorization usando Bearer token

https://localhost:5001/swagger
![GitHub Logo](/img/CapturarTela.PNG)

### Entedento a estrutura de projeto de uma aplicação em ASP.NET Core MVC

O ASP.NET Core é framework cross-platform com código-fonte aberto para a criação de aplicativos modernos. Você pode ler os Fundamentos básicos do ASP.NET na documentação da Microsoft. O padrão de arquitetura MVC (Model-View-Controller) separa um aplicativo em três grupos principais de componentes: Model-View-Controller (MVC) . Esse padrão ajuda a alcançar o princípio de designer de separação de interesses.

![GitHub Logo](/img/MVC.png)

### Swagger

O Swagger é uma aplicação open source que auxilia os desenvolvedores a definir, criar, documentar e consumir APIs REST;
É composto de um arquivo de configuração, que pode ser definido em YAML ou JSON;
Fornece ferramentas para: auxiliar na definição do arquivo de configuração (Swagger Editor), interagir com API através das definições do arquivo de configuração (Swagger UI) e gerar templates de código a partir do arquivo de configuração (Swagger Codegen).

fonte: https://swagger.io/resources/webinars/getting-started-with-swagger/

> Para importar esse componente para seu projeto, basta executar no prompt o comando abaixo ou utilizar o Nuget 

```shell
dotnet add package Swashbuckle.AspNetCore
```


### JSON Web Token (JWT)
O JWT nada mais é que um padrão (RFC-7519) de mercado que define como transmitir e armazenar objetos JSON de forma simples, compacta e segura entre diferentes aplicações, muito utilizado para validar serviços em Web Services pois os dados contidos no token gerado pode ser validado a qualquer momento uma vez que ele é assinado digitalmente.

JSON Web Tokens (JWT) é um padrão stateless porque o servidor autorizador não precisa manter nenhum estado; o próprio token é sulficiente para verificar a autorização de um portador de token.

Os JWTs são assinados usando um algoritmo de assinatura digital (por exemplo, RSA) que não pode ser forjado. Por isso, qualquer pessoa que confie no certificado do assinante pode confiar com segurança que o JWT é autêntico. Não há necessidade de um servidor consultar o servidor emissor de token para confirmar sua autenticidade.

fonte: https://jwt.io/introduction/

> Para importar esse componente para seu projeto, basta executar no prompt o comando abaixo ou utilizar o Nuget 

```shell
dotnet add package System.IdentityModel.Tokens.Jwt
```
### SQL Server e ADO.NET

O Entity Framework é uma ferramenta ORM da Microsoft madura e testada pelo mercado que pode ser usada para aplicações que usam o .NET Framework.

Ela não é a bala de prata que vai resolver todos os seus problemas de acesso a banco de dados relacionais e seu uso deve ser considerado dependendo da sua expectativa e o cenário do seu ambiente de desenvolvimento. Nessa implementação foi utilizado o ADO.NET para manipulação da base de dados SQL Server através de store procedures.

O ADO.NET fornece acesso consistente a fontes de dados como o SQL Server e o XML, e a fontes de dados expostas através do OLE DB e do ODBC. Os aplicativos do consumidor de compartilhamento de dados podem usar o ADO.NET para se conectar a essas fontes de dados, e para recuperar, manipular e atualizar os dados nelas contidos.

Stored Procedure, que traduzido significa Procedimento Armazenado, é uma conjunto de comandos em SQL que podem ser executados de uma só vez, como em uma função. Ele armazena tarefas repetitivas e aceita parâmetros de entrada para que a tarefa seja efetuada de acordo com a necessidade individual. Nesse projeto foram desenvolvidos. As Stored Procedures implementadas nesse projeto são: 

- sp_Clientes_InsertValue
- sp_Clientes_GetValueById
- sp_Clientes_GetAllValues
- sp_Clientes_DeleteValue


## Supporte

Por favor entre em contato conosco via [Email]