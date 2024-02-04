#!/bin/bash
# init.sh

# Função para esperar o RabbitMQ estar pronto
wait_for_rabbitmq() {
    echo "Aguardando RabbitMQ iniciar..."
    while ! nc -z localhost 15672; do   
      sleep 1 # Aguarda 1 segundo antes de verificar novamente
    done
    echo "RabbitMQ iniciado"
}

# Função para criar filas usando rabbitmqadmin
create_queues() {
    echo "Criando fila de pedido..."
    rabbitmqadmin --username user --password password declare queue name=pedido durable=true

    # Exemplo de criação de exchange
    #rabbitmqadmin declare exchange name=my_exchange type=direct

    # Exemplo de criação de binding
    #rabbitmqadmin declare binding source=my_exchange destination=my_queue routing_key=my_routing_key

    echo "Filas criadas"
}

# Aguarda o RabbitMQ estar pronto
wait_for_rabbitmq

# Cria as filas
create_queues

tail -f /dev/null