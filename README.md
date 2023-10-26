# Tech Challenge - SOAT1 - Grupo 13 </h1>

![GitHub](https://img.shields.io/github/license/dropbox/dropbox-sdk-java)

# Resumo do projeto
Este projeto foi desenvolvido em C# com .NET 6, seguindo os princípios da arquitetura hexagonal. É um trabalho em andamento que esté sendo realizado durante nossa pós-graduação, com o objetivo de aplicar as melhores práticas de arquitetura de software.

O propósito principal do projeto é criar uma API REST para atender as necessidades de uma rede fictícia de fast food. Essa API permitirá a realização de operações relacionadas a pedidos, gerenciamento de produtos, entre outras funcionalidades essenciais para o funcionamento de uma rede de fast food.

Ao longo do desenvolvimento, estaremos fazendo entregas incrementais e criando releases no GIT para acompanhar o progresso do projeto. Esperamos que este trabalho demonstre nosso conhecimento teórico e prático adquirido durante a pós-graduação, além de servir como um exemplo de aplicação das melhores práticas de arquitetura em projetos de software.

Sinta-se à vontade para entrar em contato conosco se tiver alguma dúvida ou sugestão. Agradecemos pelo interesse em nosso projeto!


> :construction: Projeto em construção :construction:

License: [MIT](License.txt)

# Bando de dados

Inicialmente, nosso projeto foi concebido como um monolito, e, naquela época, tínhamos a necessidade de um banco de dados com alta integridade dos dados e um bom relacionamento entre as tabelas. Portanto, escolhemos o banco de dados PostgreSQL no início do projeto devido à familiaridade do grupo com ele.

Com o desenvolvimento do projeto, surgiu a necessidade de separar o processo de autenticação em um microsserviço. Compreendemos que, em algum momento, nosso monolito será dividido em vários microsserviços, e acreditamos que é uma prática recomendada separar também a base de dados. No entanto, neste momento, consideramos que a melhor estratégia é migrar o banco de dados PostgreSQL conforme está configurado para um serviço gerenciável na nuvem, que, no nosso caso, será o AWS RDS. Isso ocorre porque a maior parte da aplicação continua como um monolito.

Nossos critérios de decisão incluem a compatibilidade e a redução da complexidade, uma vez que não desejamos fazer alterações no código neste momento, concentrando-nos principalmente na migração para a nuvem. Acreditamos que uma migração em fases é a estratégia mais apropriada, seguindo o paradigma dos "5 R's" da AWS, que incluem Rehost, Refactor, Replatform, Rebuild e Replace.

Quando ocorrer a divisão do monolito em microsserviços, nossa equipe realizará uma reavaliação e redefinição da solução de base de dados a ser utilizada por cada microsserviço.

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
Esse site tem um exemplo de como configurar no docker desktop: https://dev.to/docker/enable-kubernetes-metrics-server-on-docker-desktop-5434. Lembre-se de reiniciar o docker desktop após a configuração.

Para realizar um stress teste, dentro da pasta kubernetes voce pode executar o comando abaixo para linux:
   ```bash
   sh stress-linux.sh 0.0001 > out.txt
   ```
Para windows execute o arquivo stress-test.exe dentro da pasta stress-windows.
Lembrando que o HPA demora alguns instantes para entender que precisa escalar o pod, então é necessário aguardar alguns minutos para que o pod seja escalado. e também é necessário aguardar alguns minutos para que o pod volte ao estado normal.

# ⌨️ Testando a API
Você pode testar esta API de duas maneiras: usando o Postman ou o Swagger, que está configurado no projeto.

Acessando o Swagger:

Para acessar o Swagger do projeto localmente, utilize o seguinte link:
- http://localhost/swagger/index.html

Se você estiver usando o Kubernetes, utilize o link abaixo:
- http://localhost:31116/swagger/index.html

O Swagger já contém exemplos de chamadas com dados reais.

Se estiver testando via Swagger, lembre-se de adicionar o token obtido na resposta da chamada no menu "Authorize".

Autenticação:
As chamadas que requerem autenticação são detalhadas na documentação. Para obter um token Bearer, você pode autenticar-se em nosso serviço online - TODO: INSIRA O LINK PARA O SERVIÇO AQUI.

Se você preferir testar nosso serviço de autenticação localmente, siga as orientações no seguinte repositório:
- https://github.com/christiandmelo/TechChallenge-SOAT1-GRP13-Auth


# 📒 Documentação da API

No projeto foi instalado o REDOC e pode ser acessado através do link abaixo:

- http://localhost/api-docs/index.html
- http://localhost:31116/api-docs/index.html - No Kubernetes

# ✔️ Tecnologias utilizadas

- ``.Net 6``
- ``Postgres``
- ``Docker``


# Autores

| [<img src="https://avatars.githubusercontent.com/u/28829303?s=400&v=4" width=115><br><sub>Christian Melo</sub>](https://github.com/christiandmelo) |  [<img src="https://avatars.githubusercontent.com/u/89987201?v=4" width=115><br><sub>Luiz Soh</sub>](https://github.com/luiz-soh) |  [<img src="https://avatars.githubusercontent.com/u/21027037?v=4" width=115><br><sub>Wagner Neves</sub>](https://github.com/nevesw) |  [<img src="https://avatars.githubusercontent.com/u/34692183?v=4" width=115><br><sub>Mateus Bernardi Marcato</sub>](https://github.com/xXMateus97Xx) |
| :---: | :---: | :---: | :---: |
