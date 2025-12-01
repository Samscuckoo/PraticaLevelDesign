VAR OrbesQuestId = "ExemploQuest"
VAR OrbesColetadas = 0

VAR ExemploQuestState = "REQUIREMENTS_NOT_MET"

=== Sapo ===
{ ExemploQuestState:
        - "REQUIREMENTS_NOT_MET": -> dialogoInicial
        - "IN_PROGRESS": -> checaContador
        - "CAN_FINISH": -> dialogoFinal
        - "FINISHED": -> falaPadrao
        - else: -> algoDeuErrado
}

=algoDeuErrado
"Um inseto me comeu."
-> END

=dialogoInicial
"Olá, jovem esqueleto. Bem-vindo ao nosso vilarejo."
"Como pode ver, não tem mais ninguém além de mim."
"..."
"Por que eu disse \"nosso\" vilarejo?"
"Meus amigos estão aqui também, selados dentro de orbes azuis."
"Como sou uma estáátua, não posso ir visitá-los."
"Será que você não os traria até mim?"

* [ACEITAR AJUDAR]
    "Ah! Sabia que você tinha ossos bondosos."
    ~StartQuest(OrbesQuestId)
    -> END

+ [RECUSAR]
    "O tédio também escolhe suas vítimas."
    -> END

=checaContador
{ OrbesColetadas:
        - 0: -> questEmAndamento
        - 1: -> questEmAndamento
        - 2: -> prontoParaFinalizar
        - else: -> algoDeuErrado
}

=questEmAndamento
"Como tenho tédio."
-> END

=prontoParaFinalizar
{ ExemploQuestState:
        - "IN_PROGRESS":
            ~FinishQuest(OrbesQuestId)
            -> dialogoFinal
        - else: -> dialogoFinal
}

=dialogoFinal
"Ora..."
"Parece que eles não estão mais aqui."
"Me deprime a vida do que não se mexe."
"Obrigado por sanar minha consciência, pequeno esqueleto."
"Que sua viagem seja longa."
-> END

=falaPadrao
"..."
-> END
