ECHO OFF
CLS
ECHO Criando PODs para servicos de Log
kubectl apply -f .\serilog\seqlog-pod.yaml
kubectl apply -f .\serilog\seqlog-clusterip-http.yaml
kubectl apply -f .\serilog\seqlog-clusterip-ingestion.yaml
kubectl apply -f .\serilog\seqlog-nodeport.yaml
ECHO - 
ECHO Criando PODs para servicos de Mensageria
kubectl apply -f .\broker\rabbitmq-pod.yaml
kubectl apply -f .\broker\rabbitmq-clusterip-amq.yaml
kubectl apply -f .\broker\rabbitmq-clusterip-http.yaml
kubectl apply -f .\broker\rabbitmq-nodeport.yaml
ECHO -
ECHO Criando PODs para servicos de Banco de dados
kubectl apply -f .\database\database-pod.yaml
kubectl apply -f .\database\database-clusterip.yaml
kubectl apply -f .\database\database-nodeport.yaml
ECHO -
ECHO Criando PODs para backend consumer
kubectl apply -f .\backend-consumer\backend-consumer-configmap.yaml
kubectl apply -f .\backend-consumer\backend-consumer-pod.yaml
ECHO -
ECHO Criando PODs para backend producer
kubectl apply -f .\backend-producer\backend-producer-configmap.yaml
kubectl apply -f .\backend-producer\backend-producer-pod.yaml
kubectl apply -f .\backend-producer\backend-producer-clusterip.yaml
kubectl apply -f .\backend-producer\backend-producer-nodeport.yaml
ECHO -
ECHO Criando PODs para frontend
kubectl apply -f .\frontend\frontend-configmap.yaml
kubectl apply -f .\frontend\frontend-pod.yaml
kubectl apply -f .\frontend\frontend-clusterip.yaml
kubectl apply -f .\frontend\frontend-nodeport.yaml
ECHO -
kubectl get all
ECHO Use CTRL+C para sair do WATCH
kubectl get pods --watch
ECHO ON
