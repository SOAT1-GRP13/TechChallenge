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

O relatório pode ser acessado através do link: 
- https://soat1-grp13.github.io/TechChallenge/Documentos/RIPD/index.html

# SAGA Coreografada

No desenvolvimento do nosso projeto, optamos pela implementação da saga coreografada com base em várias considerações estratégicas e técnicas. Primeiramente, o design do nosso sistema é caracterizado por sua simplicidade e clareza, onde cada microserviço é projetado para executar uma função específica dentro de uma sequência operacional bem definida. Esta abordagem promove um alto grau de autonomia entre os serviços, permitindo-lhes reagir e processar eventos de maneira independente, o que é crucial para a manutenção da flexibilidade e escalabilidade do sistema.

Além disso, a natureza orquestrada da saga facilita a visualização e o entendimento do fluxo de eventos e operações através do sistema. Isso se alinha perfeitamente com os objetivos educacionais do nosso projeto, oferecendo uma oportunidade única para observar e analisar a comunicação entre os serviços em um ambiente controlado. A transparência e a capacidade de rastrear o fluxo de mensagens entre os serviços são aspectos valiosos para a compreensão dos princípios de sistemas distribuídos e arquiteturas baseadas em microserviços.

Por fim, a escolha da saga orquestrada nos proporciona um excelente equilíbrio entre complexidade e funcionalidade, garantindo que, mesmo com uma lógica de negócios relativamente simples, possamos explorar eficientemente conceitos avançados de sistemas distribuídos, como compensação de transações e garantia de consistência eventual. Essa abordagem nos permite não apenas atender aos requisitos do projeto, mas também aprofundar nosso entendimento sobre padrões de design de microserviços e comunicação baseada em mensagens.

# Relatório OWASP Zap

O relatório pode ser acessado através do link:
- https://soat1-grp13.github.io/TechChallenge/Documentos/OWASP_ZAP/index.html

*Obs:* Após rodarmos a ferramenta de análise, não foram identificadas vulnerabilidades de alto risco. Portanto, de acordo com os requisitos do projeto, não se faz necessária nenhuma modificação no código atual.

# 💡 Event Storm

O event storm do nosso projeto ser acessado pelo seguinte link:
- https://miro.com/app/board/uXjVMG0DfQE=/?share_link_id=33596606417

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

O arquivo docker-compose.yaml incluído neste projeto é projetado para automatizar o processo de construção e implantação de nossa arquitetura de microserviços. Ao executar este docker-compose, ele iniciará um conjunto de contêineres Docker, seguindo uma estrutura bem definida: um contêiner individual para cada microserviço descrito no projeto, além de contêineres dedicados para os bancos de dados associados a esses microserviços, sempre que aplicável. Isso assegura um isolamento eficaz das dependências e facilita a gestão de recursos.

Adicionalmente, o arquivo docker-compose está encarregado de instanciar um contêiner para o RabbitMQ, que atua como nosso intermediário de mensagens Este setup garante que as filas necessárias sejam criadas dinamicamente conforme os microserviços são ativados e interagem uns com os outros.

Cada microserviço, ao ser inicializado, é responsável por executar suas próprias 'migrations', criando as tabelas necessárias e inserindo registros de exemplo, estabelecendo assim o estado inicial requerido para que o sistema funcione corretamente.

# ⌨️ Testando a API

Você pode testar esta API de duas maneiras: usando o Postman ou o Swagger, que está configurado no projeto.

**Swagger**:

Para acessar o Swagger do projeto localmente, utilize o seguinte link:
- Auth - http://localhost:5270/swagger/index.html
- Pedido - http://localhost:5271/swagger/index.html
- Produção - http://localhost:5272/swagger/index.html
- Produto - http://localhost:5273/swagger/index.html
- Pagamento - http://localhost:5274/swagger/index.html
- Notificação - http://localhost:5275/swagger/index.html
  
O Swagger já contém exemplos de chamadas com dados reais.

Se estiver testando via Swagger, lembre-se de adicionar o token obtido na resposta da chamada no menu "Authorize".
Autenticação:
As chamadas que requerem autenticação são detalhadas na documentação. Para obter um token Bearer, você pode através do seguinte projeto: https://github.com/SOAT1-GRP13/TechChallenge-SOAT1-GRP13-Auth.

**Postman**

Você pode baixar nossa Collection no link abaixo e testar todo o projeto:



# 📒 Documentação da API

Nos projetos foi instalado o REDOC e pode ser acessado através do caminho http://localhost:[PORTA]/api-docs/index.html

Caso queira testar utilizando o postman basta importar os arquivos presentes na pasta Documentos/Postman

# Possíveis fluxos de status do pedido

![fluxo_status_pedido](</Documentos/Imagens/fluxo_status.png>)

# Fluxo de comunicação entre os microserviços

![fluxo_microservicos](</Documentos/Imagens/fluxo_microservicos.png>)

# ✔️ Tecnologias utilizadas

- ``.Net 6``
- ``Postgres``
- ``Docker``
- ``RabbitMQ``


# Autores

| [<img src="https://avatars.githubusercontent.com/u/28829303?s=400&v=4" width=115><br><sub>Christian Melo</sub>](https://github.com/christiandmelo) |  [<img src="https://avatars.githubusercontent.com/u/89987201?v=4" width=115><br><sub>Luiz Soh</sub>](https://github.com/luiz-soh) |  [<img src="https://avatars.githubusercontent.com/u/21027037?v=4" width=115><br><sub>Wagner Neves</sub>](https://github.com/nevesw) |  [<img src="https://avatars.githubusercontent.com/u/34692183?v=4" width=115><br><sub>Mateus Bernardi Marcato</sub>](https://github.com/xXMateus97Xx) |
| :---: | :---: | :---: | :---: |
