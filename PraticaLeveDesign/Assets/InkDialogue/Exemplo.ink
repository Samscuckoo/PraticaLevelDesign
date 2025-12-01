VAR ExemploQuestId = "ExemploQuest" 

VAR ExemploQuestState = "REQUIREMENTS_NOT_MET"

=== Exemplo ===
{ExemploQuestState:
    - "REQUIREMENTS_NOT_MET": -> semNivel
    - "CAN_START": -> canStart
    - "IN_PROGRESS": -> qualquerNome
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
"Ainda não acabamos nossa consulta"
->END

=caboQuest
"Boa tarde!"
->END

===canStart===
#portrait:ExemploBase
#speaker: Papai Noel
"Boa tarde! "Quais são seus sintomas? Como aconteceu?""
+ [Escorreguei no gelo e bati o ombro.]
    #speaker: Papai Noel
    #portrait: Exemplo2
    -> maissintomas
+ [Fui atingido por um floco de neve gigante… não pergunte.]
#speaker: Papai Noel
#portrait: Exemplo2
    ->maissintomas
+ [Comi biscoitos demais e minha barriga não gostou.]
#speaker: Papai Noel
#portrait: Exemplo2
    -> maissintomas

    

===maissintomas===
"Tem mais algum sintoma?"
 +["Sim, mais um."]
 #portrait: Exemplo2
    ->canStart
*["Não, é só isso."]
 #portrait: Exemplo2
    ->diagnostico
*["Talvez… não lembro direito."]
    "Tudo bem, vamos repetir" #portrait: Exemplo2
    ->canStart
    
===diagnostico===
"Certo, obrigado. Vou analisar todos os seus sintomas natalinos estranhos." #speaker:Papai Noel #portrait: Exemplo2
    *["Ok, doutor, confio no senhor."]
        ->fimm
    *["Posso ganhar um pirulito natalino?"]
        ->fimm
    *["Prometo não correr mais atrás de renas… por hoje."]
        ->fimm
    
===fimm===
"Vai precisar de um bom repouso durante as festas e de um remédio que está em cima do criado mudo da recepção. Você pode pegar ali e  ir descansar. Boas festas!" #speaker:Papai Noel #portrait: Exemplo2
    ->END

=entregaQuest
"Agora você pode pegar seus remédios na fármacia do lado."
->END


