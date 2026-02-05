#!/bin/sh

# Create Rabbitmq vhost
( sleep 5 ; \
rabbitmqctl add_vhost genocs; \
echo "*** Added virtual host sucessfully. ***" ; )