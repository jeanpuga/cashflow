<img src="./styleguide/logo-cashflow-vertical.png" align="right" />

# Cashflow
> Aplicação de fluxo de caixa

O Cashflow é uma aplicação desenvolvida com base no padrão de Design Driven Development (DDD) e orientada a eventos e vertical slices. Suas funcionalidades abrangem desde a listagem de transações operacionais até o cadastro de novas transações.

## Backend

Desenvolvido utilizando o DotNet Core 6, este projeto foi concebido com foco na escalabilidade, graças ao seu design arquitetônico que promove o desacoplamento de seus componentes, permitindo que cresçam de forma flexível conforme a necessidade.
Fora utilizado, nesse exemplo, um produtor para dois consumidores, de forma a separar os tipos de transações no fluxo de banco de dados. Esse padrão CQRS, permite que os Comandos, possam ser desvinculados das Queries.
Nesse, exemplo, existe a separação, mas por questão de processamento e máquina, não apliquei replicas ReadOnly no SQLServer, dos quais seriam complementares ao exemplo das Queries.
O endpoint de Operations GET faz as solicitações de queries, e o endpoint de Operations POST produz um evento, do qual é consumido em pods fora da stack. Essa segregação foi feita atravez de uma Feature Flag para controle da assinatura do consumidor:
```
  services.AddMassTransit(mass =>
  {
      if (featureConsumerOperationsOn)
          mass.AddConsumer<ConsumerCreateOperation>();

      mass.UsingRabbitMq((context, config) => ...
```

Atravez dessa vari[avel de ambiente, escalamos os PODs conforme nossa necessidade, isso e determonado no script de kubernete,  [backend-consumer-configmap.yaml](https://github.com/jeanpuga/cashflow/installation/backend-consumer/backend-consumer-configmap.yaml), no trecho abaixo:

```
   ...
   FeatureFlags__ConsumeOperationsFeatureOn: "true"
   ...
```



## Frontend

O frontend foi concebido em react com [CRA](https://pt-br.legacy.reactjs.org/docs/create-a-new-react-app.html) para typescript, isso garante padrões de mercado uma vez que tendências modernas "sempre bem vindas", possam influenciar a estrutura dos códigos no dia-a-dia.
Fora utilizado styled components e material design UI para React [MUI](https://mui.com/). A aplicação, utiliza também a Api Context como gerenciador de estados da interface.


## Instalação

Para rodar a aplicação Cashflow, é requisito que a maquina seja Windows com Docker, Kubernetes, e Dotnet Core na Versão 6.

### Buildar Imagens
As imagens estão publicas e disponíveis para consumo no [DockerHub](https://hub.docker.com/search?q=jeanpuga), nesse link encontramos as imagens disponíveis, porém caso haja a necessidade ou curiosidade, abaixo seguiremos com as instruções de manutenção.

#### Backend
Para buildar a aplicação do Backend, basta abrir no Visual Studio e pressionar a tecla CTRL + SHIFT + B, ou simplesmente rodar o arquivo de DOCKERFILE. Porém aqui abstraimos esse passo, e agregamos a assinatura do docker, mais o upload da imagem para o DockerHub. Basta utilizar o arquivo [publish.bat](https://github.com/jeanpuga/cashflow/backend/publish.bat), localizado na pasta do backend.
Uma vez realizado a geração, note que o tagueamento é feito em nome do logado ao DockerHub, sendo assim, mais para frente aqui nessa documentação será necessário a troca dos nomes das imagens nos aquivos de k8s.


#### Frontend
No caso do frontend, as variáveis de ambiente do Kubernete, não alcançam a aplicação, pois os trnspiladores, que são codificadores e minificadores, realizam a obfuscação no passo do build e dessa forma, caso haver a necessidade, é fato que a manutenção das variáveis de ambiente devam ser feitas antes do Build. E essas podem ser encontradas nos arquivos de "DOT ENV de produção" [.env.production](https://github.com/jeanpuga/cashflow/frontend/.env.production).

Na linha do prompt, no diretorio do frontend, podemos rodar o seguintes comandos:

Para instalação de depedências:
```
   npm start
```
Para o build:
```
   npm run build
```

Esse segundo comando irá transpilar o código do front para um javascript minificado "compactado".

A partir desse ponto uma vez o build realizado e criado na pasta "./frontend/build", podemos rodar mais um arquivo bat par agilizar o upload.
Procurar na pasta frontend o arquivo de publish.bat e executa-lo.

### Deploiar a aplicação sem refazer as imagens
Para deploiar a aplicação, basta ir a pasta [installation](https://github.com/jeanpuga/cashflow/installation) e executar o arquivo [install.bat](https://github.com/jeanpuga/cashflow/installation/install.bat). Esse irá executar a subida do planejamento orquestrado para o kubernetes.


### Deploiar a aplicação refazendo as imagens
Para renovar o deploy nas imagend é necessário trocar seus nomens dentro de dois scripts:
-[installation](https://github.com/jeanpuga/cashflow/installation/backend-consumer/backend-consumer-pod.yaml)
-[installation](https://github.com/jeanpuga/cashflow/installation/backend-producer/backend-producer-pod.yaml)

Note que no campo imagem, o nome do usuário logado, se destaca, é nesse ponto que iremos atuar a substituição, segue o exemplo abaixo:

```

    spec: 
      containers:
      - name: backend-consumer-container
        image: jeanpuga/cashflow-backend:latest
        ports:

```
Uma vez realizado, os renames, basta rodar o passo anterior "[Deploiar a aplicação sem refazer as imagens](##)"

### Banco de dados, DDL e DML
Existem duas formas que podemos fazer para implantar os scripts, uma pela linha do bash, e outra por alguma aplicação de SQL que você que está lendo esse artigo possa ter instalado. É valido lembrar que a base de dados é SQLServer, então ela não é tão agnostica como gostaríamos para essa sessão, mas atende com louvor o exercicio. 

#### Por bash
É necessário entrar dentro do POD, e executar um comando. Sendo assim o comando para entrar em um POD é:
```
kubectl exec -it deployment.apps/database-pod1  -- bash 
```
Uma vez, dentro do bash do linux, da maquina virtualizada, executaremos a seguinte sentença:

```
/opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P '1q2w3e4r@#$' -Q 'CREATE DATABASE Cashflow'
```
e depois "exit", para voltar ao Windows.

Após a criação da base, procuraremos o POD de database no kubernete e executaremos uma cópia de um script SQL para dentro do POD, e para isso precisamos de um nome fisico. O comando será:

```
kubectl get pod
```
Aqui para eu que estou escrevendo, nesse momento surgiu um nome, e com ele escrevo abaixo o comando de cópia para linux, assim:
```
kubectl cp ..\backend\script\20231010_DUMP.sql database-pod1-65d49c69f-dtcxc:/tmp/
```
Uma vez realizada a cópia, novamente voltamos dentro do POD para executar o script:
```
kubectl exec -it deployment.apps/database-pod1  -- bash

/opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P '1q2w3e4r@#$' -d Cashflow -i ./tmp/20231010_DUMP.sql
```
Uma finalizado o sistema está apto para uso.


#### Por UI
Basta logar na maquina virtualizada em localhost:31433, e executar os dois scripts que sencontram na pasta, do backend/scripts.
  Primeiro
  20231010_CREATEDATABASE.sql

  Segundo
  20231010_DUMP.sql

### Usuários


## Agradecimentos
Obrigado ao pessoal da consultoria por dispo o tem de criação dessa aplicação.

## Contribute
Contribuições são sempre bem vindas para esse artigo.

## License
[![unlicense](https://unlicense.org)
