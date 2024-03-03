# Tech Challenge - SOAT1 - Grupo 13 </h1>

![GitHub](https://img.shields.io/github/license/dropbox/dropbox-sdk-java)

# Resumo do projeto
Este projeto foi desenvolvido em C# com .NET 6, seguindo os princípios da arquitetura hexagonal. É um trabalho em andamento que esté sendo realizado durante nossa pós-graduação, com o objetivo de aplicar as melhores práticas de arquitetura de software.

O propósito principal do projeto é criar uma API REST para atender as necessidades de uma rede fictícia de fast food. Essa API permitirá a realização de operações relacionadas a pedidos, gerenciamento de produtos, entre outras funcionalidades essenciais para o funcionamento de uma rede de fast food.

Ao longo do desenvolvimento, estaremos fazendo entregas incrementais e criando releases no GIT para acompanhar o progresso do projeto. Esperamos que este trabalho demonstre nosso conhecimento teórico e prático adquirido durante a pós-graduação, além de servir como um exemplo de aplicação das melhores práticas de arquitetura em projetos de software.

Sinta-se à vontade para entrar em contato conosco se tiver alguma dúvida ou sugestão. Agradecemos pelo interesse em nosso projeto!


> :construction: Projeto em construção :construction:

License: [MIT](License.txt)

# Relatório de Impacto à Proteção de Dados Pessoais (RIPD)

O relatório pode ser acessado através do link: https://soat1-grp13.github.io/TechChallenge/Documentos/RIPD/index.html

# Autenticação

Com o avanço do projeto e à medida que nosso entendimento sobre o mesmo cresceu, identificamos a necessidade de migrar a funcionalidade de autenticação para a AWS. Como resultado, criamos um novo repositório dedicado exclusivamente às informações de autenticação, iniciando assim o processo de decomposição do nosso monólito em microserviços.

Você pode acessar o novo repositório por meio do seguinte link:
- https://github.com/christiandmelo/TechChallenge-SOAT1-GRP13-Auth

# 💡 Event Storm

O event storm do nosso projeto ser acessado pelo seguinte link:
- https://miro.com/app/board/uXjVMG0DfQE=/?share_link_id=33596606417

# 📁 Acesso ao projeto

Você pode acessar os arquivos do projeto clicando [aqui](https://github.com/christiandmelo/TechChallenge-SOAT1-GRP13/archive/refs/heads/main.zip), ou Clonando o projeto.

# Clean Architecture

Devido à natureza específica do framework .Net, adotamos uma nomeclatura diferente para nossa estrutura que segue os princípios da Clean Architecture (Arquitetura Limpa).

Na nossa arquitetura, a camada de Controller corresponde à Camada de API da Clean Architecture. Esta camada é responsável por lidar com as requisições externas e coordenar o fluxo de dados.

A camada de queries foi concebida como a camada de Gateways na Clean Architecture. Aqui, centralizamos a lógica relacionada à recuperação de dados, permitindo uma separação clara entre a fonte de dados e a lógica de negócios.

Para a implementação das operações de comando, optamos por utilizar a camada de command handlers, que equivale à camada de controller na Clean Architecture. Nesta camada, tratamos as ações e comandos vindos da camada de API, garantindo a execução das operações necessárias.

O projeto de Domain abriga as nossas entidades de negócio e objetos de valor (Value Objects). Esta camada é o coração do nosso sistema, encapsulando as regras de negócio essenciais.

No contexto da persistência de dados, a camada de Infraestrutura (Infra) foi designada como a camada de DB (Banco de Dados) na Clean Architecture. Aqui, lidamos com aspectos de armazenamento e recuperação de dados, mantendo a separação entre as preocupações de banco de dados e as regras de negócio.

Esta arquitetura foi adotada para promover a manutenibilidade, escalabilidade e testabilidade do nosso projeto, permitindo uma clara separação de responsabilidades em cada camada. Estamos comprometidos em seguir os princípios da Clean Architecture para alcançar um sistema robusto e bem estruturado.


# 🛠️ Abrir e rodar o projeto utilizando o docker

Para o correto funcionamento precisa do docker instalado.

Com o docker instalado, acesse a pasta raiz do projeto e execute o comando abaixo: 

```shell
docker-compose up
```

O docker-compose.yaml do projeto, está configurado para buildar a solution projeto, subir um container da imagem projeto, um container do banco de dados e executar as migrations no banco.
Esses containers compartilham de uma mesma rede e será criado um volume no docker para utilização do container banco.
O container do projeto está mapeado para ficar exposto na porta 80 da máquina local e o banco na porta 15432.

# ⌨️ Testando a API

**Importante**
Nesse módulo nos fizemos um ajuste no projeto para ele pegar o acesso ao banco de dados via secret manager. Para testar localmente, tem que realizar o ajuste no program e adicionar a connection string o appSettings.

Você pode testar esta API de duas maneiras: usando o Postman ou o Swagger, que está configurado no projeto.

Acessando o Swagger:

Para acessar o Swagger do projeto localmente, utilize o seguinte link:
- http://localhost/swagger/index.html

Se você estiver usando o Kubernetes, utilize o link abaixo:
- http://localhost:31116/swagger/index.html

O Swagger já contém exemplos de chamadas com dados reais.

Se estiver testando via Swagger, lembre-se de adicionar o token obtido na resposta da chamada no menu "Authorize".

Autenticação:
As chamadas que requerem autenticação são detalhadas na documentação. Para obter um token Bearer, você pode através do seguinte projeto: https://github.com/SOAT1-GRP13/TechChallenge-SOAT1-GRP13-Auth.

Se você preferir testar nosso serviço de autenticação localmente, siga as orientações no seguinte repositório:
- https://github.com/christiandmelo/TechChallenge-SOAT1-GRP13-Auth


# 📒 Documentação da API

No projeto foi instalado o REDOC e pode ser acessado através do link abaixo:

- http://localhost/api-docs/index.html
- http://localhost:31116/api-docs/index.html - No Kubernetes

# Possíveis fluxos de status do pedido

![fluxo_status_pedido](</Documentos/Imagens/fluxo_status.png>)

# Fluxo de comunicação entre os microserviços

![fluxo_microservicos](</Documentos/Imagens/fluxo_microservicos.png>)

# ✔️ Tecnologias utilizadas

- ``.Net 6``
- ``Postgres``
- ``Docker``


# Autores

| [<img src="https://avatars.githubusercontent.com/u/28829303?s=400&v=4" width=115><br><sub>Christian Melo</sub>](https://github.com/christiandmelo) |  [<img src="https://avatars.githubusercontent.com/u/89987201?v=4" width=115><br><sub>Luiz Soh</sub>](https://github.com/luiz-soh) |  [<img src="https://avatars.githubusercontent.com/u/21027037?v=4" width=115><br><sub>Wagner Neves</sub>](https://github.com/nevesw) |  [<img src="https://avatars.githubusercontent.com/u/34692183?v=4" width=115><br><sub>Mateus Bernardi Marcato</sub>](https://github.com/xXMateus97Xx) |
| :---: | :---: | :---: | :---: |
