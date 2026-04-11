# Plano de Testes de Software

Os casos de teste apresentados a seguir têm como objetivo validar as principais funcionalidades do sistema, garantindo que os requisitos definidos sejam atendidos corretamente. Cada caso de teste descreve um cenário de uso da aplicação, incluindo os passos a serem executados e os resultados esperados, assegurando o funcionamento adequado da plataforma.
 
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
|Requisito Associado | RF-06: Listar denúncias em mapa interativo |
| Objetivo do Teste 	| Permitir visualizar denúncias georreferenciadas.|
| Passos 	| - Acessar a tela "Mapa"<br>  - Selecionar o filtro que deseja "Procurar Região" ou "Procurar Rua" <br> - Clicar em "Procurar"  |
|Critério de Êxito |Denúncias exibidas corretamente na tela mapa. |
|  	|  	|
| Caso de Teste 	| **CT05 – Funcionamento da tela Feed**	|
|Requisito Associado | RF-07: Gerenciar status das denúncias com base nas interações dos usuários, RF-08:Filtrar denúncias por categoria, data ou localização, RF-09: Exibir denúncias em feed cronológico|
| Objetivo do Teste 	| Exibir denúncias, permitir refinar a busca de denúncias, através de filtros e criar post.|
| Passos 	| - Acessar a tela "Feed"<br>  - Selecionar o filtro que deseja "Tudo", "Categoria", "Data", "Região" <br> - Clicar em uma dessas opções para filtrar ou clicar em "Criar Post" |
|Critério de Êxito |Exibir as informações filtradas e o botão criar novo post. |
|  	|  	|
| Caso de Teste 	| **CT06 – Criar Denúncia**	|
|Requisito Associado | RF-04:Gerenciar denúncias,RF-05:Definir georreferência
| Objetivo do Teste 	| Permitir que o usuário registre uma denúncia.|
| Passos 	| - Acessar a sistema logado <br> - Clicar em "Criar Denúnica" no botão superior da página <br> - Preencher as informações do formulário com: "Titúlo","Descrição", "Categoria", "CEP", "Rua", "Bairro", "Ponto de referência", "Adicionar imagem" e "Postar anônimo" <br> - Clicar em "Enviar denúncia"|
|Critério de Êxito |Denúncia registrada com sucesso e visível no sistema. |
|  	|  	|
| Caso de Teste 	| **CT07 – Visualizar Postagens do Usuário**	|
|Requisito Associado | RF-04:Gerenciar denúncias no perfil do usuário|
| Objetivo do Teste 	| Validar se o sistema exibe corretamente a tela “Suas postagens” após o login do usuário.|
| Passos 	| - Acessar o sistema logado <br> - Ir té  Aba "Perfil" no canto superior direito <br> -
|Critério de Êxito |O sistema deverá exibir:Nome do usuário,Mensagem de boas-vindas,Botão Logout,Seção “Suas postagens”,Botões:,“Editar Posts”,“Criar nova denúncia” e visualizar a área destinada à exibição das postagens.|




