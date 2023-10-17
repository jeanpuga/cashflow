ECHO OFF
CLS
ECHO Adquirindo as credenciais locais do usuario Docker
docker login
ECHO -
ECHO Gerando a imagem
docker build -f .\Dockerfile -t cashflow-frontend .
ECHO -
ECHO Tagueando a imagem
docker tag cashflow-frontend jeanpuga/cashflow-frontend:latest
ECHO -
ECHO Atualizacao da imagem no DockerHub
docker push jeanpuga/cashflow-frontend:latest
ECHO -
ECHO Ok
ECHO ON