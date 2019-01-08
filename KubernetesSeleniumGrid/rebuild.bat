kubectl apply -f rh-selenium-hub-dep.yml
kubectl apply -f rh-idmrecruitlogin-dep.yml
:: timeout to allow selenium grid process to spin up before starting selenium nodes.
ping -n 45 127.0.0.1 > null

kubectl apply -f rh-selenium-node-dep.yml
:: wait for selenium nodes to be available
kubectl rollout status deployment rh-selenium-node-chrome -n selenium
