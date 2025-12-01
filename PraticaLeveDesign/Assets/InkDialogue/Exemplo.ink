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
Agradeço novamente por retornar o artefato.
->END

=canStart
#portrait:ExemploBase
#speaker: Guarda da cidade
Guardião do templo, bandidos roubaram o artefato sagrado do templo, por favor, você deve recuperar o artefato que foi roubado pelos bandidos 
* [ENTENDI!]
    #speaker: Guarda da cidade
    #portrait: ExemploGira
    O artefato se encontra no acampanento dos bandidos ao sul daqui. Você irá encontrar o artefato dentro da casa do líder deles, que é a casa de madeira.
    ~StartQuest(ExemploQuestId)
    -> okToGo
+ [Não entendi]
#speaker: Guarda da cidade
#portrait: Exemplo2
    Deixe-me explicar a situação novamente...
    ->canStart
*[Onde exatamente está a reliquia?]
#speaker: Guarda da cidade
#portrait:AnimaçãoExemplo
    Ele se encontra na casa do líder dos bandidos, uma casa grande feita de madeira.
    ->canStart
    

=okToGo
#portrait:CavaleroDeCosta
Após recuperar o artefato coloque-o de volta no templo ao norte e depois venha falar comigo, boa sorte.
->END

=entregaQuest
Muito obrgado por retornar o artefato!
~FinishQuest(ExemploQuestId)
->END


