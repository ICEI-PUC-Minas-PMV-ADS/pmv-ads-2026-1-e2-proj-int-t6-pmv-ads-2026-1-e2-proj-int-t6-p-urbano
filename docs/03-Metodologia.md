
# Metodologia

<span style="color:red">Pré-requisitos: <a href="2-Especificação do Projeto.md"> Documentação de Especificação</a></span>

A equipe adota uma abordagem baseada em metodologias ágeis, combinando práticas dos frameworks Scrum e Kanban para organizar e gerenciar as atividades do projeto.

Essa estratégia permite:

- Dividir o desenvolvimento em etapas incrementais;
- Acompanhar continuamente o progresso das entregas;
- Promover colaboração constante entre os integrantes da equipe;
- Adaptar rapidamente o planejamento conforme a evolução do projeto.
- Gestão de Atividades

O gerenciamento das atividades é realizado por meio de ferramentas online de acompanhamento de tarefas, que possibilitam:

- Registro e priorização de demandas;
- Definição clara de responsáveis;
- Visualização do status e progresso das atividades;
- Organização do fluxo de trabalho de ponta a ponta.

Dessa forma, o time mantém um fluxo estruturado, transparente e alinhado com os objetivos do projeto.

## Controle de Versão

O projeto Portal Urbano utiliza o [Git](https://git-scm.com/) como sistema de controle de versão, com o [Github](https://github.com)
como plataforma de hospedagem do repositório. Essa abordagem permite rastreabilidade das mudanças, colaboração eficiente entre os desenvolvedores e controle sobre as versões do software.

## Estratégia de Branches

O projeto adota uma estratégia de branches baseada em ambientes e ciclo de desenvolvimento:

- main: versão estável do sistema, homologada e pronta para produção
- develop: branch principal de desenvolvimento contínuo
- feature/*: utilizada para desenvolvimento de novas funcionalidades
- bugfix/*: utilizada para correção de erros identificados
- hotfix/*: utilizada para correções urgentes em produção

Essa estrutura garante organização no fluxo de desenvolvimento e maior controle sobre as entregas.

Quanto à gerência de issues, o projeto adota a seguinte convenção para
etiquetas:

- `documentation`: melhorias ou acréscimos à documentação
- `bug`: uma funcionalidade encontra-se com problemas
- `enhancement`: uma funcionalidade precisa ser melhorada
- `feature`: uma nova funcionalidade precisa ser introduzida

A configuração do projeto foi realizada criando-se um repositório central no GitHub. A gerência de commits é feita de forma frequente para evitar grandes conflitos. As merges são realizadas preferencialmente via Pull Requests após a revisão básica dos pares, garantindo que a branch main contenha apenas código funcional.

> **Links Úteis**:
> - [Tutorial GitHub](https://guides.github.com/activities/hello-world/)
> - [Git e Github](https://www.youtube.com/playlist?list=PLHz_AreHm4dm7ZULPAmadvNhH6vk9oNZA)
>  - [Comparando fluxos de trabalho](https://www.atlassian.com/br/git/tutorials/comparing-workflows)
> - [Understanding the GitHub flow](https://guides.github.com/introduction/flow/)
> - [The gitflow workflow - in less than 5 mins](https://www.youtube.com/watch?v=1SXpE08hvGs)

## Gerenciamento de Projeto

### Divisão de Papéis e Responsabilidade 

A organização da equipe do projeto foi definida com base em papéis que contribuem para o planejamento, acompanhamento e desenvolvimento das atividades. Essa estrutura visa promover uma melhor distribuição das responsabilidades, maior clareza na execução das tarefas e alinhamento entre os integrantes ao longo do desenvolvimento do projeto.

A composição da equipe está definida da seguinte forma:

**Scrum Master:**
Vitor Souza

**Product Owner:**
Ana Oliveira

**Equipe de Desenvolvimento:**
Ana Oliveira, Maria Resende, Marianna Farias, Rian Paixão, Vinicius Feitosa e Vitor Souza

**Equipe de Design:**
Maria Resende

Essa divisão de papéis contribui para a organização do trabalho em equipe, favorecendo a comunicação entre os membros e a condução eficiente das atividades previstas no projeto.

> **Links Úteis**:
> - [11 Passos Essenciais para Implantar Scrum no seu 
> Projeto](https://mindmaster.com.br/scrum-11-passos/)
> - [Scrum em 9 minutos](https://www.youtube.com/watch?v=XfvQWnRgxG0)

### Processo

Para organizar e distribuir as atividades do projeto, a equipe está utilizando o GitHub, estruturado com as seguintes colunas:

**Backlog:** Reúne as demandas que precisam ser desenvolvidas e corresponde ao Backlog do produto. Todas as tarefas identificadas ao longo do projeto são adicionadas a essa lista.

**Ready**: Corresponde às atividades que já estão refinadas, definidas e prontas para serem iniciadas pela equipe. São tarefas que possuem informações suficientes para execução e aguardam apenas o momento de serem assumidas.

**In Progress**: Contém as tarefas que já foram iniciadas e estão em desenvolvimento.

**Done**: Refere-se às atividades que foram concluídas com sucesso. Nesse status, encontram-se as entregas finalizadas e validadas dentro do escopo previsto.

O quadro kanban do grupo no GitHub está disponível no link https://github.com/orgs/ICEI-PUC-Minas-PMV-ADS/projects/2584 e é apresentado, no estado atual, na figura abaixo:

![Quadro Kaban].(img/QuadroKanban.png).
 
> **Links Úteis**:
> - [Project management, made simple](https://github.com/features/project-management/)
> - [Sobre quadros de projeto](https://docs.github.com/pt/github/managing-your-work-on-github/about-project-boards)
> - [Como criar Backlogs no Github](https://www.youtube.com/watch?v=RXEy6CFu9Hk)
> - [Tutorial Slack](https://slack.com/intl/en-br/)

### Ferramentas

As ferramentas empregadas no projeto são:

- Editor de código.
- Ferramentas de comunicação
- Ferramentas de desenho de tela (_wireframing_)

O editor de código foi escolhido porque ele possui uma integração com o
sistema de versão. As ferramentas de comunicação utilizadas possuem
integração semelhante e por isso foram selecionadas. Por fim, para criar
diagramas utilizamos essa ferramenta por melhor captar as
necessidades da nossa solução.

Liste quais ferramentas foram empregadas no desenvolvimento do projeto, justificando a escolha delas, sempre que possível.
 
> **Possíveis Ferramentas que auxiliarão no gerenciamento**: 
> - [Slack](https://slack.com/)
> - [Github](https://github.com/)
