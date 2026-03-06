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
> <img width="763" height="556" alt="image" src="https://github.com/user-attachments/assets/db17877a-743e-46a7-a3fe-a46cea8a7086" />

|EU COMO... `PERSONA`| QUERO/PRECISO ... `FUNCIONALIDADE` |PARA ... `MOTIVO/VALOR`                 |
|--------------------|------------------------------------|----------------------------------------|
|Carlos  |Registrar, visualizar, confirmar e comentar denúncias sobre problemas nas ruas, além de consultar os problemas mais relatados na cidade. |Avisar outros motoristas, compartilhar informações, dar visibilidade às denúncias, evitar vias com dificuldades e planejar melhor minhas rotas.
           


|EU COMO... `PERSONA`| QUERO/PRECISO ... `FUNCIONALIDADE` |PARA ... `MOTIVO/VALOR`                 |
|--------------------|------------------------------------|----------------------------------------|
|Dona Maria  | Utilizar um aplicativo com letras grandes       |Conseguir enxergar melhor as informações         |
|Dona Maria        | Registrar problemas do bairro de forma simples               | Conseguir relatar situações sem dificuldade |
|Dona Maria        |Publicar alertas sobre locais perigosos              | Avisar outros moradores |
|Dona Maria        | Visualizar problemas relatados na região          | Evitar lugares que possam causar acidentes |
|Dona Maria       | Navegar em um sistema com linguagem fácil            |Entender o funcionamento do aplicativo |

|EU COMO... `PERSONA`| QUERO/PRECISO ... `FUNCIONALIDADE` |PARA ... `MOTIVO/VALOR`                 |
|--------------------|------------------------------------|----------------------------------------|
|Rafael | Publicar denúncias de problemas urbanos       |Colaborar com melhorias na cidade     |
|Rafael      | Interagir nas publicações de outros usuários           | Compartilhar informações relevantes |
|Rafael        |Reforçar denúncias feitas pela comunidade              | Aumentar a visibilidade dos problemas|
|Rafael         | Filtrar denúncias por categoria          | Encontrar problemas específicos |
|Rafael        |Explorar denúncias recentes da cidade        |Acompanhar a situação urbana |

|EU COMO... `PERSONA`| QUERO/PRECISO ... `FUNCIONALIDADE` |PARA ... `MOTIVO/VALOR`                 |
|--------------------|------------------------------------|----------------------------------------|
|Juliana | Relatar problemas encontrados no bairro   |Informar a comunidade    |
|Juliana      | Consultar publicações feitas por moradores    | Ver se outras pessoas têm o mesmo problema |
|Juliana        |Participar das discussões nas denúncias             | Trocar informações com a comunidade|
|Juliana        |Apoiar reclamações de outros usuários          | Fortalecer as denúncias do bairro |
|Juliana        |Identificar problemas mais comentados     |Entender o que mais afeta a região |

|EU COMO... `PERSONA`| QUERO/PRECISO ... `FUNCIONALIDADE` |PARA ... `MOTIVO/VALOR`                 |
|--------------------|------------------------------------|----------------------------------------|
|Eduardo| Visualizar denúncias registadas pela população   |Entender as demandas da cidade    |
|Eduardo      | Organizar denúncias por tipo de problema    | Facilitar a análise das ocorrências |
|Eduardo        |Identificar denúncias com maior participação             | Definir prioridades de atendimento |
|Eduardo        |Avaliar comentários feitos pelos cidadãos          | Compreender melhor os problemas relatados |
|Eduardo        |Mapear problemas recorrentes    |Planejar ações de melhoria urbana |

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

> **Links Úteis**:
> - [O que são Requisitos Funcionais e Requisitos Não Funcionais?](https://codificar.com.br/requisitos-funcionais-nao-funcionais/)
> - [O que são requisitos funcionais e requisitos não funcionais?](https://analisederequisitos.com.br/requisitos-funcionais-e-requisitos-nao-funcionais-o-que-sao/)

## Diagrama de Casos de Uso

O diagrama de casos de uso é o próximo passo após a elicitação de requisitos, que utiliza um modelo gráfico e uma tabela com as descrições sucintas dos casos de uso e dos atores. Ele contempla a fronteira do sistema e o detalhamento dos requisitos funcionais com a indicação dos atores, casos de uso e seus relacionamentos. 

As referências abaixo irão auxiliá-lo na geração do artefato “Diagrama de Casos de Uso”.

> **Links Úteis**:
> - [Criando Casos de Uso](https://www.ibm.com/docs/pt-br/elm/6.0?topic=requirements-creating-use-cases)
> - [Como Criar Diagrama de Caso de Uso: Tutorial Passo a Passo](https://gitmind.com/pt/fazer-diagrama-de-caso-uso.html/)
> - [Lucidchart](https://www.lucidchart.com/)
> - [Astah](https://astah.net/)
> - [Diagrams](https://app.diagrams.net/)
