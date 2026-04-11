# Plano de Testes de Usabilidade

Os testes de usabilidade têm como objetivo avaliar a experiência do usuário ao utilizar a plataforma de registro e visualização de denúncias ambientais, verificando se as funcionalidades disponíveis são intuitivas, acessíveis e eficientes.

Neste projeto, o plano de testes de usabilidade foi elaborado com base nas principais funcionalidades do sistema, como cadastro, login, criação de denúncias, visualização no mapa e uso de filtros, considerando situações reais de uso.

Um plano de teste de usabilidade para este sistema deve conter:


## Definição do(s) objetivo(s)

Antes de iniciar os testes, é essencial definir o que se deseja avaliar na usabilidade do sistema. 
Alguns exemplos de objetivos são:
- Verificar se os usuários conseguem realizar as principais funcionalidades da plataforma, como cadastro, login e registro de denúncias, de forma simples e sem dificuldades;
- Identificar possíveis problemas na navegação e na interação com recursos como o mapa e os filtros de busca;
- Avaliar se o sistema permite que o usuário execute suas tarefas de forma rápida e eficiente;
- Analisar o nível de satisfação do usuário ao utilizar a aplicação;
- Garantir que as informações apresentadas, como denúncias e localizações, sejam claras e fáceis de entender.

## Seleção dos participantes

Para garantir que os testes representem o uso real da plataforma, serão selecionados participantes que possuam características semelhantes aos usuários que utilizarão o sistema no dia a dia.

**Critérios para selecionar participantes:**
- Perfis variados, incluindo usuários iniciantes e com maior experiência em sistemas digitais;
- Diferentes níveis de familiaridade com tecnologia e uso de aplicativos;
- Pessoas interessadas em questões ambientais ou no descarte correto de resíduos;
- Usuários que consigam interagir com funcionalidades como formulários e mapas.

**Quantidade recomendada:**
Mínimo: 3 participantes.
Ideal: Entre 3 e 6 participantes para garantir maior diversidade e qualidade nos resultados


## Definição de cenários de teste

Os cenários representam tarefas reais que os usuários executam na plataforma de registro e acompanhamento de denúncias ambientais.
Neste projeto, foram definidos cenários com base nas principais funcionalidades do sistema, como cadastro, login, criação de denúncias, acesso ao perfil do usuário e visualização no mapa e uso de filtros.

Objetivo: O que será avaliado na interação do usuário com funcionalidades como cadastro, denúncias e navegação no sistema;
Contexto: A situação que leva o usuário a utilizar a plataforma, como registrar uma denúncia ou buscar informações;
Tarefa: A ação que o usuário deve realizar dentro do sistema (ex: cadastrar, logar, criar denúncia, filtrar);
Critério de sucesso: Como identificar se a tarefa foi concluída corretamente, considerando o funcionamento esperado do sistema.


**Cenário 1- Cadastro de usuário**

**Objetivo:** Avaliar a facilidade do usuário ao realizar o cadastro na plataforma.

**Contexto:** O usuário acessa o sistema pela primeira vez e deseja se cadastrar para utilizar as funcionalidades disponíveis.

**Tarefa(s):** 
- Acessar o sistema
- Clicar em "Cadastrar"
- Preencher nome, e-mail e senha
- Confirmar o cadastro

**Critério(s) de Sucesso(s):**
- O usuário consegue preencher os dados sem dificuldades
- O cadastro é realizado com sucesso
- O sistema exibe mensagem de confirmação ou redireciona para login

**Cenário 2- Login do Usuário**

**Objetivo:** Avaliar se o usuário consegue acessar o sistema corretamente.

**Contexto:** O usuário já possui cadastro e deseja acessar sua conta.


**Tarefa(s):** 
- Acessar o sistema
-Clicar em "Já tenho uma conta"
-Informar e-mail e senha
-Clicar em "Fazer login"

**Critério(s) de Sucesso(s):**
- O login é realizado com sucesso
- O usuário é direcionado para a tela principal
- Não ocorrem erros durante o processo

**Cenário 3- Recuperação de Senha**

**Objetivo:** Avaliar a facilidade de recuperação de senha.

**Contexto:** O usuário esqueceu sua senha e precisa redefini-la.

**Tarefa(s):** 
- Acessar a tela de login
-Clicar em "Esqueci minha senha"
-Informar o código enviado pelo e-mail
-Inserir nova senha

**Critério(s) de Sucesso(s):**
- O usuário consegue solicitar a recuperação
- O sistema envia o e-mail de redefinição
- A nova senha é cadastrada com sucesso

**Cenário 4- Visualizar Denúncias no Mapa**

**Objetivo:** Avaliar a visualização das denúncias no mapa.

**Contexto:** O usuário deseja consultar denúncias próximas à sua região.

**Tarefa(s):** 
- Acessar o sistema
- Ir até a tela "Mapa"
- Aplicar filtros de busca
- Visualizar os pontos no mapa

**Critério(s) de Sucesso(s):**
- As denúncias aparecem corretamente no mapa
- O usuário consegue identificar os locais
- A navegação ocorre sem dificuldades

**Cenário 5-Funcionalidade do Feed**

**Objetivo:** Avaliar a exibição e filtragem de denúncias no feed.

**Contexto:** O usuário deseja visualizar denúncias recentes e aplicar filtros.

**Tarefa(s):** 
- Acessar a tela "Feed"
- Selecionar filtros (tudo,categoria, data, região)
- Visualizar os resultados

**Critério(s) de Sucesso(s):**
- As denúncias são exibidas corretamente
- Os filtros funcionam conforme esperado
- As informações são claras e organizadas

**Cenário 6-Criar Denúncia**

**Objetivo:** Avaliar a funcionalidade de registro de denúncias.

**Contexto:** O usuário deseja registrar um problema ambiental na plataforma.

**Tarefa(s):** 
- Acessar o sistema logado
- Clicar em "Criar Denúncia"
- Preencher os campos obrigatórios
- Adicionar localização
- Enviar denúncia

**Critério(s) de Sucesso(s):**
- A denúncia é registrada com sucesso
- Os dados são salvos corretamente
- A denúncia aparece no sistema

**Cenário 7-Visualizar Postagens do Usuário**

**Objetivo:** Avaliar a visualização das postagens no perfil do usuário

**Contexto:** O usuário deseja acessar suas próprias denúncias cadastradas.

**Tarefa(s):** 
- Acessar o sistema logado
- Ir até a aba "Perfil"
- Visualizar a seção "Suas postagens"

**Critério(s) de Sucesso(s):**
- As postagens são exibidas corretamente
- Os botões "Editar Posts" e "Criar nova denúncia" estão visíveis
- As informações do usuário aparecem corretamente

## Métodos de coleta de dados

Os dados coletados têm como objetivo analisar a experiência dos usuários ao utilizar a plataforma de registro e visualização de denúncias ambientais.

A coleta será realizada por meio de:

Observação direta: acompanhamento do usuário durante a execução das tarefas, identificando dificuldades e comportamentos;
Métricas quantitativas:
Tempo gasto para concluir cada tarefa;
Quantidade de erros cometidos;
Número de tentativas até concluir a ação;
Métricas qualitativas:
Dificuldades encontradas durante o uso;
Comentários e percepções dos usuários sobre o sistema;
Facilidade de entendimento das funcionalidades.

Também será aplicado um questionário pós-teste, com perguntas como:

A interface foi fácil de utilizar?
Você encontrou dificuldades ao realizar alguma tarefa?
As informações apresentadas foram claras?
O uso do mapa e dos filtros foi intuitivo?
O que poderia ser melhorado no sistema?

Para cada participante, será importante registrar todas as métricas definidas, garantindo uma análise completa dos resultados.
Além disso, o processo de coleta de dados seguirá as diretrizes da LGPD (Lei Geral de Proteção de Dados), assegurando que nenhum dado pessoal ou sensível dos participantes será divulgado.

