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
Aqui você escreve sua quest normal. Se tiver escolhas, faz com estrela + colchetes.
* [ENTENDI!]
    Mas você tem que redirecionar depois de cada escolha, parça. Pode ser pro mesmo lugar ou pra lugar diferente.
    Depois, cê tem que chamar a função pra startar a quest passando o ID. Por isso a variavel la em cima. Demoro?
    ~StartQuest(ExemploQuestId)
    -> okToGo
*[NUM INTENDI]
    Então vamo tentar denovo...
    ->canStart
    

=okToGo
Agora que cê entendeu como escrever uma quest, passa atrás do pilar pra mim.
->END

=entregaQuest
"MILAGREEE O GD SABE SEGUIR ORDENS"
->END


