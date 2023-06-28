# Tech Challenge - SOAT1 - Grupo 13 </h1>

![GitHub](https://img.shields.io/github/license/dropbox/dropbox-sdk-java)

# Resumo do projeto
Este projeto foi desenvolvido em C# com .NET 6, seguindo os princípios da arquitetura hexagonal. É um trabalho em andamento realizado durante nossa pós-graduação, com o objetivo de aplicar as melhores práticas de arquitetura de software.

O propósito principal do projeto é criar uma API REST para atender as necessidades de uma rede fictícia de fast food. Essa API permitirá a realização de operações relacionadas a pedidos, gerenciamento de produtos, autenticação de usuários, entre outras funcionalidades essenciais para o funcionamento de uma rede de fast food.

Ao longo do desenvolvimento, estaremos fazendo entregas incrementais e criando releases no GIT para acompanhar o progresso do projeto. Esperamos que este trabalho demonstre nosso conhecimento teórico e prático adquirido durante a pós-graduação, além de servir como um exemplo de aplicação das melhores práticas de arquitetura em projetos de software.

Sinta-se à vontade para entrar em contato conosco se tiver alguma dúvida ou sugestão. Agradecemos pelo interesse em nosso projeto!


> :construction: Projeto em construção :construction:

License: [MIT](License.txt)

# 💡 Event Storm

O event storm do nosso projeto ser acessado pelo seguinte link:
- https://miro.com/app/board/uXjVMG0DfQE=/?share_link_id=33596606417

# 📁 Acesso ao projeto

Você pode acessar os arquivos do projeto clicando [aqui](https://github.com/christiandmelo/TechChallenge-SOAT1-GRP13/archive/refs/heads/main.zip), ou Clonando o projeto.


# 🛠️ Abrir e rodar o projeto

Para o correto funcionamento precisa do docker instalado.

Com o docker instalado, acesse a pasta raiz do projeto e execute o comando abaixo: 

```shell
docker-compose up
```

O docker-compose.yaml do projeto, está configurado para buildar a solution projeto, subir um container da imagem projeto, um container do banco de dados e executar as migrations no banco.
Esses containers compartilham de uma mesma rede e será criado um volume no docker para utilização do container banco.
O container do projeto está mapeado para ficar exposto na porta 80 da máquina local e o banco na porta 15432.


# ⌨️ Testando a API

Essa API pode ser testada via Postman ou via swagger que está configurado no projeto.
Para acessar o swagger do projeto, utilize o link abaixo:
- http://localhost/swagger/index.html

Para testar as chamadas da API, precisa passar um Bearer token de autenticacao.
Esse token pode obtido através do seguinte endpoint:
- http://localhost/Autenticacao/LogInUsuario

Também é necessário passar o seguinte Json no corpo da chamada:
```shell
{
  "nomeUsuario": "fiapUser",
  "senha": "Teste@123"
}
```

Caso esteja testando via Swagger, é necessário adicionar o token obtido na resposta da chamada no menu "Authorize" 

# 📒 Documentação da API

No projeto foi instalado o REDOC e pode ser acessado através do link abaixo:


- http://localhost/api-docs/index.html

# ✔️ Tecnologias utilizadas

- ``.Net 6``
- ``Postgres``
- ``Docker``


# Autores

| [<img src="https://avatars.githubusercontent.com/u/28829303?s=400&v=4" width=115><br><sub>Christian Melo</sub>](https://github.com/christiandmelo) |  [<img src="https://avatars.githubusercontent.com/u/89987201?v=4" width=115><br><sub>Luiz Soh</sub>](https://github.com/luiz-soh) |  [<img src="https://avatars.githubusercontent.com/u/21027037?v=4" width=115><br><sub>Wagner Neves</sub>](https://github.com/nevesw) |  [<img src="https://avatars.githubusercontent.com/u/5776353?v=4" width=115><br><sub>Luiz Bento</sub>](https://github.com/luizbento) |  [<img src="https://avatars.githubusercontent.com/u/34692183?v=4" width=115><br><sub>Mateus Bernardi Marcato</sub>](https://github.com/xXMateus97Xx) |
| :---: | :---: | :---: | :---: | :---: |
