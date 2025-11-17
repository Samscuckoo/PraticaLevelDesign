VAR ExemploQuestId = "ExemploQuest" 

VAR ExemploQuestState = "REQUIREMENTS_NOT_MET"

=== Exemplo ===
{ExemploQuestState:
    - "REQUIREMENTS_NOT_MET": -> semNivel
    - "CAN_START": -> canStart
    - "IN_PROGRESS": -> qualquerNome
    - "CAN_FINISH": -> entregaQuest
    - "FINISHED": -> caboQuest
    - else: -> algoAconteceu
}

=algoAconteceu
Alguma coisa quebro ai fera...
->END

=semNivel
"Cê num tem nivel!"
-> END

=qualquerNome
O nome das partes do nó só afetam dentro do nó, mas o nome tem que ser igual. Presta atenção no casing. E END sempre encerra o nó. 
->END

=caboQuest
"Cê já fez a quest!"
->END

=canStart
#portrait:ExemploBase
#speaker: Zézin Tutoriais
Aqui você escreve sua quest normal. Se tiver escolhas, faz com símbolo Mais (+) e colchetes. Estrela + colchetes significa que a escolha é única. Isso não muda muita coisa.
* [ENTENDI!]
    #speaker: Zézin Poggers
    #portrait: ExemploGira
    Mas você tem que redirecionar depois de cada escolha, parça. Pode ser pro mesmo lugar ou pra lugar diferente.
    Depois, cê tem que chamar a função pra startar a quest passando o ID. Por isso a variavel la em cima. Demoro?#portrait:CavaleroDeCosta
    ~StartQuest(ExemploQuestId)
    -> okToGo
+ [NUM INTENDI]
#speaker: Zézin Bravo
#portrait: Exemplo2
    Então vamo tentar denovo...
    ->canStart
*[NUM INTENDI COM ESTRELA]
#speaker: Zézin Paciente
#portrait:AnimaçãoExemplo
    Então vamo tentar denovo, mas essa opção vai sumir agora.
    ->canStart
    

=okToGo

Agora que cê entendeu como escrever uma quest, passa atrás do pilar pra mim.
->END

=entregaQuest
"MILAGREEE O GD SABE SEGUIR ORDENS"
Só não esquece de finalizar a quest.
~FinishQuest(ExemploQuestId)
->END


