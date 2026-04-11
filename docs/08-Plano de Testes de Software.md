# Plano de Testes de Software

<span style="color:red">Pré-requisitos: <a href="2-Especificação do Projeto.md"> Especificação do Projeto</a></span>, <a href="3-Projeto de Interface.md"> Projeto de Interface</a>

Apresente os cenários de testes utilizados na realização dos testes da sua aplicação. Escolha cenários de testes que demonstrem os requisitos sendo satisfeitos.

Não deixe de enumerar os casos de teste de forma sequencial e de garantir que o(s) requisito(s) associado(s) a cada um deles está(ão) correto(s) - de acordo com o que foi definido na seção "2 - Especificação do Projeto". 

Por exemplo:
 
| **Caso de Teste** 	| **CT01 – Cadastro do usuário** 	|
|:---:	|:---:	|
|	Requisito Associado 	| RF-01 -Realizar cadastro|
| Objetivo do Teste 	| Permitir que o usuário realize o cadastro na plataforma com sucesso. |
| Passos 	| - Acessar o navegador <br> - Informar o endereço do sistema <br> - Preencher os campos obrigatórios (Nome, e-mail, senha) <br> - Clicar em "Se inscreva" |
|Critério de Êxito | "Cadastrado realizado com sucesso" ou redirecionamento para login. |
|  	|  	|
| Caso de Teste 	| **CT02 – Login do Usuário**	|
|Requisito Associado | RF-02	- Fazer login |
| Objetivo do Teste 	| Verificar se o usuário consegue realizar login. |
| Passos 	| - Acessar o sistema <br>  - Clicar no botão "Já tenho uma conta" <br> - Preencher o campo de e-mail <br> - Preencher o campo da senha <br> - Clicar em "Faça login" |
|Critério de Êxito | O login foi realizado com sucesso. |
|  	|  	|
| Caso de Teste 	| **CT03 – Recuperar Senha**	|
|Requisito Associado | RF-03	- Recuperar senha por e-mail |
| Objetivo do Teste 	| Permitir que o usuário recupere sua senha. |
| Passos 	| - Acessar a tela de login <br>  - Clicar no botão "Esqueci minha senha" <br> - Preencher o campo de código( com o código enviado para o e-mail do usuário) <br> - Preencher o campo "Senha nova" <br> - Clicar em "Faça seu login" |
|Critério de Êxito |  Envio de e-mail para redefinição de senha. |
|  	|  	|
| Caso de Teste 	| **CT04 – Visualizar denúncias no Mapa**	|
|Requisito Associado | RF-06: Listar denúncias em mapa interativo, RF-08:Filtrar denúncias por categoria, data ou localização |
| Objetivo do Teste 	| Permitir visualizar denúncias georreferenciadas.|
| Passos 	| - Acessar a tela "Mapa"<br>  - Selecionar o filtro que deseja "Procurar Região" ou "Procurar Rua" <br> - Clicar em "Procurar"  |
|Critério de Êxito |Denúncias exibidas corretamente na tela mapa. |


