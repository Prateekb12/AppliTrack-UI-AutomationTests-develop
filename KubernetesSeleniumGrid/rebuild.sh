#!/bin/bash

kubectl apply -f rh-selenium-hub-dep.yml
kubectl apply -f rh-idmrecruitlogin-dep.yml
# Sleep to allow selenium grid process to spin up before starting selenium nodes.
sleep 45

kubectl apply -f rh-selenium-node-dep.yml
# wait for selenium nodes to be available
kubectl rollout status deployment rh-selenium-node-chrome -n selenium
