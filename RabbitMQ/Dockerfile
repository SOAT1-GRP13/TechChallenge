FROM rabbitmq:3-management

# Instala netcat, se necessário para o script init.sh
RUN apt-get update && apt-get install -y netcat

# Copia o script init.sh para o container
COPY init.sh /init.sh
RUN chmod +x /init.sh

# Inicia o RabbitMQ em background e executa init.sh
CMD rabbitmq-server & /init.sh