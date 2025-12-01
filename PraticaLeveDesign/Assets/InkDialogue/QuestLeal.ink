VAR QuestLealId = "LealQuest" 

VAR LealQuestState = "REQUIREMENTS_NOT_MET"

=== QuestLeal ===
{LealQuestState:
    - "REQUIREMENTS_NOT_MET": -> semNivel
    - "CAN_START": -> canStart
    - "IN_PROGRESS": -> emProgresso
    - "CAN_FINISH": -> entregaQuest
    - "FINISHED": -> caboQuest
    - else: -> algoAconteceu
}

=algoAconteceu
Aqui é gg
->END

=semNivel
Busque comer cimento e melhore
-> END

=emProgresso
Não é possível que você esqueceu uma coisa tão simples assim
->END

=caboQuest
Tá bão já, descansa um pouco aí
->END

=canStart
#portrait:ExemploBase
#speaker: Mago Implacável
Negócio é o seguinte, eu preciso que você toque as campainhas de todas as casas do vilarejo aqui. Não é nada pessoal. Vai lá fazer isso pra mim que o tio tem doce no carro pra depois.
* [Beleza, já volto]
    #speaker: Mago Implacável
    #portrait: ExemploGira
    0 perguntas, é assim que eu gosto#portrait:CavaleroDeCosta
    ~StartQuest(QuestLealId)
    -> okToGo
+ [Mas...]
#speaker: Mago Implacável
#portrait: Exemplo2
    Amigo, é o que eu acabei de falar, vai lá e não pergunta nada, depois você volta aqui.
    ~StartQuest(QuestLealId)
    ->okToGo
*[Eu não vou fazer isso]
#speaker: Mago Implacável
#portrait:AnimaçãoExemplo
    É uma pena, porque eu sou a única opção de você sair desse jog-, quer dizer, desse vilarejo horroroso
    ->END
    

=okToGo
Agora vai lá garoto, rebenta aquelas campainhas
->END

=entregaQuest
Perfeito amigo, deu tudo certo né?
Eu prometi um doce, então pode pegar na minha e...
~FinishQuest(QuestLealId)
->END


