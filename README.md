# Tech Challenge - SOAT1 - Grupo 13 </h1>

![GitHub](https://img.shields.io/github/license/dropbox/dropbox-sdk-java)

# Resumo do projeto
Este projeto foi desenvolvido em C# com .NET 6, seguindo os princípios da arquitetura hexagonal. É um trabalho em andamento que esté sendo realizado durante nossa pós-graduação, com o objetivo de aplicar as melhores práticas de arquitetura de software.

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


# 🛠️ Abrir e rodar o projeto utilizando o docker

Para o correto funcionamento precisa do docker instalado.

Com o docker instalado, acesse a pasta raiz do projeto e execute o comando abaixo: 

```shell
docker-compose up
```

O docker-compose.yaml do projeto, está configurado para buildar a solution projeto, subir um container da imagem projeto, um container do banco de dados e executar as migrations no banco.
Esses containers compartilham de uma mesma rede e será criado um volume no docker para utilização do container banco.
O container do projeto está mapeado para ficar exposto na porta 80 da máquina local e o banco na porta 15432.

# Abrir e rodar o projeto utilizando o Kubernetes

Antes de prosseguir, certifique-se de estar dentro da pasta "Kubernetes" localizada na raiz do projeto.

**Importante:** A ordem de execução dos comandos é fundamental para a correta criação dos recursos. Execute-os na mesma ordem em que estão listados abaixo.

## Passo 1: Configuração do PostgreSQL

1. Crie o ConfigMap e os Secrets do PostgreSQL:
   ```bash
   kubectl apply -f configmaps/postgres-configmap.yaml
   kubectl apply -f secrets/postgres-secrets.yaml
   ```

2. Crie o Volume do PostgreSQL:
   ```bash
   kubectl apply -f volumes/postgres-pv.yaml
   kubectl apply -f volumes/postgres-pvc.yaml
   ```

3. Crie o Deployment e o Service do PostgreSQL:
   ```bash
   kubectl apply -f deployments/postgres-deployment.yaml
   kubectl apply -f services/postgres-service.yaml
   ```

## Passo 2: API Deployment, Service e HPA (Horizontal Pod Autoscaler)

4. Crie o Deployment, o Service e o Horizontal Pod Autoscaler da API:
   ```bash
   kubectl apply -f deployments/api-deployment.yaml
   kubectl apply -f services/api-service.yaml
   kubectl apply -f hpa/api-hpa.yaml
   ```

Lembre-se de que os comandos acima precisam ser executados em um ambiente Kubernetes configurado corretamente. Acompanhe as saídas dos comandos para garantir que os recursos estejam sendo criados sem erros. Após a execução, você terá suas aplicações implantadas e prontas para uso.
**Importante:** Para que o hpa funcione corretamente, sua máquina tem que ter o metrics configurado corretamente. voce pode verificar se está configurado utilizando o comando:
   ```bash
   kubectl top pods
   ```

Para realizar um stress teste, dentro da pasta kubernetes voce pode executar o comando abaixo para linux:
   ```bash
   sh stress-linux.sh 0.0001 > out.txt
   ```
Para windows execute o arquivo stress-test.exe dentro da pasta stress-windows.
Lembrando que o HPA demora alguns instantes para entender que precisa escalar o pod, então é necessário aguardar alguns minutos para que o pod seja escalado. e também é necessário aguardar alguns minutos para que o pod volte ao estado normal.

# ⌨️ Testando a API

Essa API pode ser testada via Postman ou via swagger que está configurado no projeto.
Para acessar o swagger do projeto, utilize o link abaixo:
- http://localhost/swagger/index.html
Caso esteja utilizando o kubernetes, utilize o link abaixo:
http://localhost:31116/swagger/index.html

As chamadas que exigem autenticação estão informadas na documentação

O token do gestor pode ser obtido através do seguinte endpoint:
- http://localhost/Autenticacao/LogInUsuario - O usuario do exemplo do swagger ja se autentica

O swagger ja possui exemplos de chamadas com dados reais

Caso esteja testando via Swagger, é necessário adicionar o token obtido na resposta da chamada no menu "Authorize" 

# 📒 Documentação da API

No projeto foi instalado o REDOC e pode ser acessado através do link abaixo:


- http://localhost/api-docs/index.html

# ✔️ Tecnologias utilizadas

- ``.Net 6``
- ``Postgres``
- ``Docker``


# Autores

| [<img src="https://avatars.githubusercontent.com/u/28829303?s=400&v=4" width=115><br><sub>Christian Melo</sub>](https://github.com/christiandmelo) |  [<img src="https://avatars.githubusercontent.com/u/89987201?v=4" width=115><br><sub>Luiz Soh</sub>](https://github.com/luiz-soh) |  [<img src="https://avatars.githubusercontent.com/u/21027037?v=4" width=115><br><sub>Wagner Neves</sub>](https://github.com/nevesw) |  [<img src="https://avatars.githubusercontent.com/u/34692183?v=4" width=115><br><sub>Mateus Bernardi Marcato</sub>](https://github.com/xXMateus97Xx) |
| :---: | :---: | :---: | :---: |