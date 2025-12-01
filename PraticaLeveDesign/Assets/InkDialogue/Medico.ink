VAR MedicoId = "Medico" 

VAR MedicoState = "REQUIREMENTS_NOT_MET"

=== Medico ===
{MedicoState:
    - "REQUIREMENTS_NOT_MET": -> semNivel
    - "CAN_START": -> canStart
    - "IN_PROGRESS": -> qualquerNome
    - "CAN_FINISH": -> entregaQuest
    - "FINISHED": -> caboQuest
    - else: -> algoAconteceu
}

=algoAconteceu
"Não consegui entender o que está querendo dizer"
->END

=semNivel
"Cê num tem nivel!"
-> END

=qualquerNome
"Ainda não terminamos a consulta"
->END

=caboQuest
"Boa tarde!"
->END

=canStart
#portrait:ExemploBase
#speaker: Duende da recepção
"Boa tarde! Como posso te ajudar?"
* [Queria uma consulta.]
    #speaker: Duende da recepção
    #portrait: ExemploGira
    "Certo, deixa eu ver a disponibilidade aqui..."#portrait:CavaleroDeCosta
    ~StartQuest(MedicoId)
    -> okToGo
+ [Não te ouvi direito.]
#speaker: Duende da recepção
#portrait: Exemplo2
    "Então deixa eu perguntar de novo..."
    ->canStart

    

=okToGo
"Pode entrar no consultório e falar com o doutor."
->END

=entregaQuest
"Agora você pode pegar seus remédios na fármacia do lado."
~FinishQuest(MedicoId)
->END