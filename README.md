# celcoin

Rodando o projeto
---

### Clonando o repositório
Digite na linha de comando
`git clone https://github.com/JonRoosevelt/celcoin.git`

ou 

`git clone git@github.com:JonRoosevelt/celcoin.git`


### Configurando base de dados

É necessário ter uma instancia do postgresql em sua máquina.

Uma vez configurado o banco de dados, é só substituir a string de conexão no arquivo, na linha 3.

A string é composta de 
``Host=<endereço>;Database=<nome_da_database>;Username=<usuario_da_database>;Password=<password_do_usuario>;Port=<porta (sendo que a padrão do postgres é 5432)``

[https://github.com/JonRoosevelt/celcoin/blob/main/celcoin/appsettings.Development.json    ](/8_CnhvubS6WGE9_MEL6VDg)

```csharp=
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=celcoin;Username=postgres;Password=1q2w3e4rdocker;Port=5433;"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  }
}
```

### Rodando o projeto

A versão utilizada do dotnet core é a 3.1

Entre na pasta celcoin/Celcoin e digite

`dotnet restore`

O dotnet irá preparar o ambiente para a utilização.

Após isto digite na linha de comando

`dotnet ef database update`

Para que as migrations sejam aplicadas.

Já serão criadas instancias para a utilização.

Para visualizar a api, pode ser utilizada uma aplicação como insomnia, postman ou outro cliente http, ou até mesmo acessar pelo navegador os endpoints.


Caso opte por rodar diretamente no insomnia, segue o link.
[![Run in Insomnia}](https://insomnia.rest/images/run.svg)](https://insomnia.rest/run/?label=Celcoin&uri=https%3A%2F%2Fgithub.com%2FJonRoosevelt%2Fcelcoin%2Fblob%2Fmain%2Fcelcoin.json)




Para acessar a aplicação é só ir ao endereço:
[https://localhost:5001/maquineta](/646XFMZbQDKLqKIRQ4VyTg)

Quaisquer dúvidas, só entrar em contato.
