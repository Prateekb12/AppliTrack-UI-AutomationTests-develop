#!/bin/bash

kubectl scale --replicas=0 deployment/rh-selenium-node-chrome
kubectl scale --replicas=0 deployment/rh-selenium-hub
kubectl scale --replicas=1 deployment/rh-idmrecruitlogin
