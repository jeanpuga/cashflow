#language: pt-BR

Funcionalidade: Cenario0001-UploadEProcessamento
** Processo de Calculo - primeira versão
Na primeira versão a entrega garante a subida dio artefato "planilha de calculo".
- etapa 1
Upload da planilha
- etapa 2
Salvar em bucket S3
- etapa 3
Ler a planilha e extrair os dados
- etapa 4
Recebe os fatorese processa o calculo

*** A validação das funcionalidades da planilha acontece mediante a ultima etapa do processo que é o calculo.
Isso acontece porque na "etapa 3" retorna dois payloads?
- o q será enviado na etapa 4 - calculo
- e o resultado do calculo pela planilha do qual pode ser comparado

@CalculateProcess

Cenário: Upload da planilha e processamento
	Dado a planilha preenchida "exemplo1.xlsx"
	Quando realizo upload da planilha
	E depois executo o processo
	Então deve apresentar os valores calculados
		| ManualTotal       | ManualAvoided      | AutomaticTotal    | AutomaticAvoided   |
		| 5.174277520532012 | 2.6447544683951905 | 5.142054355930715 | 2.6447543147978463 |
	

