#!/bin/bash
for i in {1..10000}; do
  curl localhost:31116/swagger/index.html
  sleep $1
done
