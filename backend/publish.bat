ECHO OFF
CLS
ECHO Adquirindo as credenciais locais do usuario Docker
docker login
ECHO -
ECHO Buildando a aplicacao e gerando a imagem
docker build -f .\src\API\Dockerfile -t cashflow-backend .
ECHO -
ECHO Tagueando a imagem
docker tag cashflow-backend jeanpuga/cashflow-backend:latest
ECHO -
ECHO Atualizacao da imagem no DockerHub
docker push jeanpuga/cashflow-backend:latest
ECHO -
ECHO Ok
ECHO ON