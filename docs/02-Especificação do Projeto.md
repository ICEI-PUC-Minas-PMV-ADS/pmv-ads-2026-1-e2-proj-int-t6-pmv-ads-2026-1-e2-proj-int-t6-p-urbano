# Especificações do Projeto

## Personas

As personas a seguir representam diferentes perfis de cidadãos e agentes públicos que enfrentam problemas relacionados à infraestrutura urbana em Belo Horizonte. Elas ajudam a compreender necessidades, dificuldades e expectativas em relação a um sistema colaborativo de denúncias e alertas urbanos.

> **Persona 1 - Carlos Henrique Souza**

<img width="762" height="624" alt="Captura de tela 2026-03-06 201428" src="https://github.com/user-attachments/assets/d6653195-0751-4338-ad4b-f64d563cb94b" />

> **Persona 2 - Maria Aparecida**

<img width="768" height="567" alt="Captura de tela 2026-03-06 201526" src="https://github.com/user-attachments/assets/89356522-efc1-45e7-b2ec-cc11216a1096" />

> **Persona 3 - Rafael Oliveira**

<img width="766" height="598" alt="Captura de tela 2026-03-06 201610" src="https://github.com/user-attachments/assets/e4cf89d5-5cbf-4245-81cb-d852be75af52" />

> **Persona 4 - Eduardo Martins**

<img width="767" height="589" alt="Captura de tela 2026-03-06 201705" src="https://github.com/user-attachments/assets/0b7e7cfe-fa65-4144-88ff-5a1d342e8770" />

> **Persona 5 - Juliana Mendes**:

<img width="763" height="556" alt="Captura de tela 2026-03-06 201816" src="https://github.com/user-attachments/assets/9af4d39d-0bae-4190-a3a9-744f95418fc4" />


## Histórias de Usuários

Com base na análise das personas forma identificadas as seguintes histórias de usuários:

|EU COMO... `PERSONA`| QUERO/PRECISO ... `FUNCIONALIDADE` |PARA ... `MOTIVO/VALOR`                 |
|--------------------|------------------------------------|----------------------------------------|
|Carlos Henrique |Registrar, visualizar, confirmar e comentar denúncias sobre problemas nas ruas, além de consultar os problemas mais relatados na cidade. |Avisar outros motoristas, compartilhar informações, dar visibilidade às denúncias, evitar vias com dificuldades e planejar melhor minhas rotas.           
|Dona Maria  |Utilizar um aplicativo simples, com letras grandes e linguagem fácil, para registrar, publicar e visualizar problemas do bairro. |Conseguir enxergar melhor as informações, relatar situações com facilidade, avisar moradores da região, evitar locais perigosos e compreender melhor o funcionamento do aplicativo.
|Rafael Oliveira |Publicar denúncias, interagir com publicações de outros usuários, reforçar denúncias da comunidade, filtrar por categoria e explorar denúncias recentes.|Colaborar com melhorias na cidade, compartilhar informações relevantes, aumentar a visibilidade dos problemas, encontrar situações específicas e acompanhar a situação urbana.
|Eduardo Martins |Visualizar, organizar e analisar as denúncias da população, identificando os problemas mais recorrentes, os mais comentados e os que têm maior participação. |Entender melhor as demandas da cidade, facilitar a análise das ocorrências e definir prioridades para planejar ações de melhoria urbana.
|Juliana Mendes |Relatar problemas do bairro, consultar publicações de moradores, participar das discussões, apoiar reclamações e identificar problemas mais comentados. |Informar a comunidade, verificar se outras pessoas têm o mesmo problema, trocar informações com moradores, fortalecer denúncias do bairro e entender os principais problemas da região.

## Requisitos

As tabelas que se seguem apresentam os requisitos funcionais e não funcionais que detalham o escopo do projeto.

### Requisitos Funcionais

## Requisitos Funcionais

| ID    | Descrição do Requisito                                             | Prioridade |
|-------|--------------------------------------------------------------------|------------|
| RF-01 | Realizar cadastro                                                  | ALTA       |
| RF-02 | Fazer login                                                        | ALTA       |
| RF-03 | Recuperar senha por e-mail                                         | ALTA       |
| RF-04 | Gerenciar denúncias no perfil do usuário                           | ALTA       |
| RF-05 | Definir georreferência na criação da denúncia                      | ALTA       |
| RF-06 | Listar denúncias em mapa interativo                                | ALTA       |
| RF-07 | Gerenciar status das denúncias com base nas interações dos usuários| ALTA       |
| RF-08 | Filtrar denúncias por categoria, data ou localização      | MÉDIA      |
| RF-09 | Exibir denúncias em feed cronológico                               | MÉDIA      |
| RF-10 | Excluir denúncias indevidas automaticamente                        | MÉDIA      |

### Requisitos Não Funcionais

| ID      | Descrição do Requisito                                   | Prioridade |
|---------|-----------------------------------------------------------|------------|
| RNF-001 | Garantir funcionamento em dispositivos móveis e desktops  | ALTA       |
| RNF-002 | Armazenar senhas de forma criptografada                   | ALTA       |
| RNF-003 | Garantir usabilidade e navegação intuitiva na interface   | ALTA       |
| RNF-004 | Otimizar qualidade e armazenamento das imagens            | MÉDIA      |
| RNF-005 | Carregar denúncias no mapa dinamicamente                  | BAIXA      |
| RNF-006 | Garantir precisão na exibição das localizações no mapa    | BAIXA      |


## Restrições

O projeto está restrito pelos itens apresentados na tabela a seguir.

|ID| Restrições                                           |
|--|-------------------------------------------------------|
|01| A aplicação deverá possuir dois CRUD's.  |
|02| A aplicação deverá conter duas APIs.      |
|03| O backend deverá ser desenvolvido em C#.       |
|04| Deverá ser utilizado o Entity Framework para acesso e manipulação do banco de dados.        |
|05|O banco de dados utilizado deverá ser MySQL ou Microsoft SQL Server.        |


## Diagrama de Casos de Uso

O diagrama de casos de uso é o próximo passo após a elicitação de requisitos, que utiliza um modelo gráfico e uma tabela com as descrições sucintas dos casos de uso e dos atores. Ele contempla a fronteira do sistema e o detalhamento dos requisitos funcionais com a indicação dos atores, casos de uso e seus relacionamentos. 

<img width="721" height="763" alt="Image" src="https://github.com/user-attachments/assets/92c1e28f-2e7c-44b3-977c-55dccebde384" />
