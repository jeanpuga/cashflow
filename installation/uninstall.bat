ECHO OFF
CLS
ECHO Excluindo PODs para frontend
kubectl delete -f .\frontend\frontend-clusterip.yaml
kubectl delete -f .\frontend\frontend-nodeport.yaml
kubectl delete -f .\frontend\frontend-pod.yaml
kubectl delete -f .\frontend\frontend-configmap.yaml
ECHO -
ECHO Excluindo PODs para backend producer
kubectl delete -f .\backend-producer\backend-producer-clusterip.yaml
kubectl delete -f .\backend-producer\backend-producer-nodeport.yaml
kubectl delete -f .\backend-producer\backend-producer-pod.yaml
kubectl delete -f .\backend-producer\backend-producer-configmap.yaml
ECHO -
ECHO Excluindo PODs para backend consumer
kubectl delete -f .\backend-consumer\backend-consumer-pod.yaml
kubectl delete -f .\backend-consumer\backend-consumer-configmap.yaml
ECHO -
ECHO Excluindo PODs para servicos de Banco de dados
kubectl delete -f .\database\database-clusterip.yaml
kubectl delete -f .\database\database-nodeport.yaml
kubectl delete -f .\database\database-pod.yaml
ECHO -
ECHO Excluindo PODs para servicos de Mensageria
kubectl delete -f .\broker\rabbitmq-clusterip-amq.yaml
kubectl delete -f .\broker\rabbitmq-clusterip-http.yaml
kubectl delete -f .\broker\rabbitmq-nodeport.yaml
kubectl delete -f .\broker\rabbitmq-pod.yaml
ECHO -
ECHO Excluindo PODs para servicos de Log
kubectl delete -f .\serilog\seqlog-clusterip-http.yaml
kubectl delete -f .\serilog\seqlog-clusterip-ingestion.yaml
kubectl delete -f .\serilog\seqlog-nodeport.yaml
kubectl delete -f .\serilog\seqlog-pod.yaml
ECHO -
kubectl get all
ECHO ON
