# Especificações do Projeto

<span style="color:red">Pré-requisitos: <a href="1-Documentação de Contexto.md"> Documentação de Contexto</a></span>

Definição do problema e ideia de solução a partir da perspectiva do usuário. É composta pela definição do  diagrama de personas, histórias de usuários, requisitos funcionais e não funcionais além das restrições do projeto.

Apresente uma visão geral do que será abordado nesta parte do documento, enumerando as técnicas e/ou ferramentas utilizadas para realizar a especificações do projeto

## Personas

Identifique, em torno de, 5 personas. Para cada persona, lembre-se de descrever suas angústicas, frustrações e expectativas de vida relacionadas ao problema. Além disso, defina uma "aparência" para a persona. Para isso, você poderá utilizar sites como [https://this-person-does-not-exist.com/pt#google_vignette](https://this-person-does-not-exist.com/pt) ou https://thispersondoesnotexist.com/ 

Utilize também como referência o exemplo abaixo:

<img src="https://github.com/ICEI-PUC-Minas-PMV-ADS/IntApplicationProject-Template/blob/main/docs/img/AnaClara1.png" alt="Persona1"/>

Enumere e detalhe as personas da sua solução. Para tanto, baseie-se tanto nos documentos disponibilizados na disciplina e/ou nos seguintes links:

> **Links Úteis**:
> 
> - [Rock Content](https://rockcontent.com/blog/personas/)
> - [Hotmart](https://blog.hotmart.com/pt-br/como-criar-persona-negocio/)
> - [O que é persona?](https://resultadosdigitais.com.br/blog/persona-o-que-e/)
> - [Persona x Público-alvo](https://flammo.com.br/blog/persona-e-publico-alvo-qual-a-diferenca/)
> - [Mapa de Empatia](https://resultadosdigitais.com.br/blog/mapa-da-empatia/)
> - [Mapa de Stalkeholders](https://www.racecomunicacao.com.br/blog/como-fazer-o-mapeamento-de-stakeholders/)
>
Lembre-se que você deve ser enumerar e descrever precisamente e personalizada todos os clientes ideais que sua solução almeja.

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

|ID    | Descrição do Requisito  | Prioridade |
|------|-----------------------------------------|----|
|RF-001| O sistema deve permitir o cadastro de usuários informando, no mínimo, nome, e-mail e senha. | ALTA | 
|RF-002| O sistema deve permitir que usuários cadastrados realizem login utilizando e-mail e senha previamente registrados.     | ALTA |
|RF-003| O sistema deve permitir que usuários autenticados criem denúncias contendo obrigatoriamente título, descrição e ao menos uma imagem  | ALTA |
|RF-004| O sistema deve permitir que o usuário informe ou selecione a localização geográfica da denúncia no momento do cadastro  | ALTA |
|RF-005| O sistema deve exibir as denúncias registradas em um mapa interativo utilizando marcadores geográficos baseados na localização informada. | ALTA |
|RF-006| O sistema deve permitir filtrar denúncias por categoria, ordenar por data de publicação e realizar busca por bairro ou endereço. | ALTA |
|RF-007| O sistema deve permitir que o usuário visualize em seu perfil todas as denúncias criadas por ele.  | ALTA |
|RF-008| O sistema deve permitir a atualização do status de uma denúncia para valores pré-definidos: pendente, em andamento ou resolvido.  | ALTA |
|RF-009| O sistema deve permitir que o autor da denúncia exclua denúncias previamente publicadas em seu perfil. | MÉDIA |
|RF-010| O sistema deve exibir as denúncias também em formato de feed cronológico semelhante ao de redes sociais.  | MÉDIA |
|RF-011| O sistema deve calcular automaticamente a prioridade da denúncia com base na quantidade de interações recebidas (ex: curtidas, comentários ou confirmações)| MÉDIA |
|RF-012| O sistema deve exibir cada denúncia como um ponto no mapa e permitir que o usuário visualize seus detalhes ao clicar no marcador. | MÉDIA |

### Requisitos não Funcionais

|ID     | Descrição do Requisito  |Prioridade |
|-------|-------------------------|----|
|RNF-001| O sistema deve funcionar em dispositivos móveis e desktops (responsivo). | ALTA | 
|RNF-002| As senhas devem ser armazenadas de forma criptografada. |  ALTA | 
|RNF-003| O sistema deve suportar múltiplos usuários simultâneos. |  ALTA | 
|RNF-004| O sistema deve ter interface intuitiva e de fácil usabilidade.  |  ALTA | 
|RNF-005| O sistema deve armazenar imagens com a qualidade otimizada. |  MÉDIA | 
|RNF-006| O mapa deve carregar as denúncias dinamicamente sem recarregar a página.  |  BAIXA | 
|RNF-007| O sistema deve garantir precisão na exibição das localizações no mapa.   |  BAIXA | 


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

<img width="721" height="763" alt="Image" src="https://github.com/user-attachments/assets/92c1e28f-2e7c-44b3-977c-55dccebde384" />
