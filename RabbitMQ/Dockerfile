FROM rabbitmq:3-management
RUN apt-get update && apt-get install -y netcat
COPY init.sh /init.sh
RUN chmod +x /init.sh
CMD ["/init.sh"]
