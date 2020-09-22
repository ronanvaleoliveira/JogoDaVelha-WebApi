# API Jogo da Velha

O objetivo deste projeto foi desenvolver uma api para um jogo multiplayer de **Jogo da Velha**.



## Instruções para executar a API

1.	Clonar ou baixar o repositório do git https://github.com/ronanvaleoliveira/ProjetoApi.git
2.	Abrir o arquivo projeto utilizando Visual Studio.
3.	Executar o projeto.




## Instruções para realizar o teste
A api poderá ser testada de duas maneiras:
1. **Swagger:** Ao executar o projeto, a página inicial do swagger será apresentada listando todas as chamadas disponíveis.
https://localhost:44321/swagger/index.html
![image](https://user-images.githubusercontent.com/13223097/93942705-54f28600-fd07-11ea-902d-33a67c2f5552.png)

2. **Postman:** Caso queira utilizar o Postman, basta importar o arquivo **"Jogo da Velha.postman_collection.json"** disponibilizado neste repositório.
![image](https://user-images.githubusercontent.com/13223097/93942752-6b004680-fd07-11ea-9359-fbbde763a7e6.png)



## Chamadas

* **/v1/game:** Responsável por criar uma nova partida e sortear o primeiro jogador.
* **/v1/game/{id}/movement:** Responsável por executar o movimento de cada jogador no tabuleiro. O parâmetro **{id}** deverá conter o identificador da partida retornado na chamada anterior.

## Tabuleiro

As coordenadas X e Y representam a posição do movimento no tabuleiro. Começando do índice 0, no canto superior esquerdo. De forma que o tabuleiro fica assim:

- [0,0] [1,0] [2,0]
- [0,1] [1,1] [2,1]
- [0,2] [1,2] [2,2]

## Validações

* Partida não encontrada.
* Partida encerrada! Não será possível executar o movimento.
* Favor informar o Player que esta realizando a jogada!
* Os valores aceitos para o campo Player são 'X' ou 'O'!
* Favor informar as posições do tabuleiro!
* Favor informar a posição X do Tabuleiro!
* Favor informar a posição Y do Tabuleiro!
* Movimento inválido! A posição 'X' tem que estar entre 0 e 2.
* Movimento inválido! A posição 'Y' tem que estar entre 0 e 2.
* Não é o turno do jogador!
* Movimento inválido! A posição 'X'0'Y'0 já esta sendo utilizada.

## Importante:
Esta api trabalha com a manipulação de arquivos json, que são armazenados no diretório corrente da aplicação, dessa forma, caso você se depare com a mensagem de erro indicado acesso negado ao diretório, certifique-se que você possui permissões de administrador e/ou se o Visual Studio está sendo executado com esta permissão.


## Recursos e padrões utilizados no desenvolvimento
* .NET Core 3.1
* DDD
* CQRS
* Factory
* Flunt
* Swagger
