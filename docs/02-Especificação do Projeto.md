# Especificações do Projeto

## Personas
As personas a seguir representam diferentes perfis de cidadãos e agentes públicos que enfrentam problemas relacionados à infraestrutura urbana em Belo Horizonte. Elas ajudam a compreender necessidades, dificuldades e expectativas em relação a um sistema colaborativo de denúncias e alertas urbanos.

> **Persona 1 - Carlos Henrique Souza**
<img width="762" height="624" alt="image" src="https://github.com/user-attachments/assets/89cf6af1-689f-4ff5-85ae-d9549909a5b1" />

> **Persona 2 - Maria Aparecida**
<img width="768" height="567" alt="image" src="https://github.com/user-attachments/assets/96f7b36e-1f7f-4d4a-b823-89bd69a7175b" />


> **Persona 3 - Rafael Oliveira**
 <img width="766" height="598" alt="image" src="https://github.com/user-attachments/assets/7bc5c61a-dcc9-4c17-a515-960b82cf4b0c" />


> **Persona 4 - Eduardo Martins**
> <img width="767" height="589" alt="image" src="https://github.com/user-attachments/assets/bf906ffb-c7d4-45d2-9a5b-7c94b95b5058" />


> **Persona 5 - Julaina Mendes**
  <img width="763" height="556" alt="image" src="https://github.com/user-attachments/assets/db17877a-743e-46a7-a3fe-a46cea8a7086" />


|EU COMO... `PERSONA`| QUERO/PRECISO ... `FUNCIONALIDADE` |PARA ... `MOTIVO/VALOR`                 |
|--------------------|------------------------------------|----------------------------------------|
|Carlos  |Registrar, visualizar, confirmar e comentar denúncias sobre problemas nas ruas, além de consultar os problemas mais relatados na cidade. |Avisar outros motoristas, compartilhar informações, dar visibilidade às denúncias, evitar vias com dificuldades e planejar melhor minhas rotas.
           

|EU COMO... `PERSONA`| QUERO/PRECISO ... `FUNCIONALIDADE` |PARA ... `MOTIVO/VALOR`                 |
|--------------------|------------------------------------|----------------------------------------|
|Dona Maria |Utilizar um aplicativo simples, com letras grandes e linguagem fácil, para registrar, publicar e visualizar problemas do bairro. |Conseguir enxergar melhor as informações, relatar situações com facilidade, avisar moradores da região, evitar locais perigosos e compreender melhor o funcionamento do aplicativo.

|EU COMO... `PERSONA`| QUERO/PRECISO ... `FUNCIONALIDADE` |PARA ... `MOTIVO/VALOR`                 |
|--------------------|------------------------------------|----------------------------------------|
|Rafael |Publicar denúncias, interagir com publicações de outros usuários, reforçar denúncias da comunidade, filtrar por categoria e explorar denúncias recentes.|Colaborar com melhorias na cidade, compartilhar informações relevantes, aumentar a visibilidade dos problemas, encontrar situações específicas e acompanhar a situação urbana.

|EU COMO... `PERSONA`| QUERO/PRECISO ... `FUNCIONALIDADE` |PARA ... `MOTIVO/VALOR`                 |
|--------------------|------------------------------------|----------------------------------------|
|Juliana |Relatar problemas do bairro, consultar publicações de moradores, participar das discussões, apoiar reclamações e identificar problemas mais comentados.|Informar a comunidade, verificar se outras pessoas têm o mesmo problema, trocar informações com moradores, fortalecer denúncias do bairro e entender os principais problemas da região.

|EU COMO... `PERSONA`| QUERO/PRECISO ... `FUNCIONALIDADE` |PARA ... `MOTIVO/VALOR`                 |
|--------------------|------------------------------------|----------------------------------------|
|Eduardo |Visualizar, organizar e analisar as denúncias da população, identificando os problemas mais recorrentes, os mais comentados e os que têm maior participação.|Entender melhor as demandas da cidade, facilitar a análise das ocorrências e definir prioridades para planejar ações de melhoria urbana.


## Requisitos

As tabelas que se seguem apresentam os requisitos funcionais e não funcionais que detalham o escopo do projeto.

### Requisitos Funcionais

|ID    | Descrição do Requisito  | Prioridade |
|------|-----------------------------------------|----|
|RF-001| A aplicação deve permitir que o usuário avalie uma agência de intercâmbio com base na sua experiência| ALTA | 
|RF-002| A aplicação deve permitir que o usuário inclua comentários ao fazer uma avaliação de uma agência de intercâmbio    | ALTA |
|RF-003| A aplicação deve permitir que o usuário consulte todas as agências de intercâmbio cadastradas ordenando-as com base em suas notas | ALTA |

### Requisitos não Funcionais

|ID     | Descrição do Requisito  |Prioridade |
|-------|-------------------------|----|
|RNF-001| A aplicação deve ser responsiva | MÉDIA | 
|RNF-002| A aplicação deve processar requisições do usuário em no máximo 3s |  BAIXA | 

Com base nas Histórias de Usuário, enumere os requisitos da sua solução. Classifique esses requisitos em dois grupos:

- [Requisitos Funcionais
 (RF)](https://pt.wikipedia.org/wiki/Requisito_funcional):
 correspondem a uma funcionalidade que deve estar presente na
  plataforma (ex: cadastro de usuário).
- [Requisitos Não Funcionais
  (RNF)](https://pt.wikipedia.org/wiki/Requisito_n%C3%A3o_funcional):
  correspondem a uma característica técnica, seja de usabilidade,
  desempenho, confiabilidade, segurança ou outro (ex: suporte a
  dispositivos iOS e Android).
Lembre-se que cada requisito deve corresponder à uma e somente uma
característica alvo da sua solução. Além disso, certifique-se de que
todos os aspectos capturados nas Histórias de Usuário foram cobertos.

## Restrições

O projeto está restrito pelos itens apresentados na tabela a seguir.

|ID| Restrições                                           |
|--|-------------------------------------------------------|
|01| A aplicação deverá possuir dois CRUD's.  |
|02| A aplicação deverá conter duas APIs.      |
|03| O backend deverá ser desenvolvido em C#.       |
|04| Deverá ser utilizado o Entity Framework para acesso e manipulação do banco de dados.        |
|05|O banco de dados utilizado deverá ser MySQL ou Microsoft SQL Server.        |

Enumere as restrições à sua solução. Lembre-se de que as restrições geralmente limitam a solução candidata.


## Diagrama de Casos de Uso

O diagrama de caso de uso abaixo apresenta as principais interações entre os usuários e o sistema de denúncias urbanas. Ele ilustra como cidadãos e agentes públicos podem registrar, visualizar e acompanhar ocorrências relacionadas à infraestrutura da cidade. 

<img width="721" height="763" alt="imagem (1)" src="https://github.com/user-attachments/assets/cb252f94-acf9-497b-be79-29e80a8f5298" />

