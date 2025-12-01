// Variáveis que o Unity vai atualizar
// O ID tem que ser IDÊNTICO ao que está no seu ScriptableObject
VAR ColetarPistasQuestId = "ColetarPistasQuest"
VAR ColetarPistasQuestState = "REQUIREMENTS_NOT_MET"

// Nó Principal (O Porteiro que decide pra onde vai o papo)
=== ColetarPistas ===
{ColetarPistasQuestState:
- "REQUIREMENTS_NOT_MET": -> semNivel
- "CAN_START":      -> intro        // Onde a mágica começa
- "IN_PROGRESS":    -> emProgresso  // Enquanto procura as pistas
- "CAN_FINISH":     -> entregar     // Quando achou as 3
- "FINISHED":       -> concluido    // Depois que acabou tudo
- else:             -> algoQuebrou
}

// ---------------------------------------------------------
// ESTADO: REQUISITOS NÃO ATENDIDOS (Nível baixo)
// ---------------------------------------------------------
= semNivel
#speaker: Policial Chefe
#portrait: Policial_Normal
Detetive, a cena do crime está isolada. Apenas pessoal autorizado nível ouro.
-> END

// ---------------------------------------------------------
// ESTADO: PODE INICIAR (A conversa principal)
// ---------------------------------------------------------
= intro
#speaker: Policial Chefe
#portrait: Policial_Preocupado
Detetive, que bom que você chegou. A situação não está muito boa por aqui.

// Escolhas do jogador

[O que aconteceu aqui?]
#speaker: Detetive
#portrait: Detetive_Normal
O que aconteceu aqui, chefe?

#speaker: Policial Chefe
#portrait: Policial_Normal
Você ainda não está inteirada sobre o caso? Tudo bem, eu vou te explicar.
-> explicacaoDoCaso // Redireciona para a história

[É, dá pra perceber mesmo...]
#speaker: Detetive
#portrait: Detetive_Serio
É, dá pra perceber mesmo... o clima está pesado.

#speaker: Policial Chefe
#portrait: Policial_Normal
Tudo aqui está uma bagunça, mas fique tranquila, vou te atualizar sobre o andamento do caso.
-> explicacaoDoCaso // Redireciona para a história

// O bloco grande de texto (A explicação)
= explicacaoDoCaso
#speaker: Policial Chefe
#portrait: Policial_Serio
Bom, essa manhã encontraram o corpo de Henry Matthews, herdeiro legítimo da fortuna da família Matthews.

O corpo foi encontrado pela governanta ao chamar o patrão para o café da manhã.

De acordo com a análise preliminar de nossos especialistas, ele foi envenenado na noite de ontem, possivelmente morrendo durante o sono.

Até o momento possuímos dois suspeitos em custódia: a irmã mais velha, Irene Matthews, e o irmão mais novo, Klaus Matthews.

Acreditamos que ambos possuíam fortes motivos para desejar a morte do irmão.

Irene, mesmo sendo a primogênita, só poderia receber a herança caso estivesse casada. Já Klaus só poderia colocar a mão no dinheiro caso os irmãos não estivessem no caminho.

Ambos acusam um ao outro, mas acredito que você será capaz de encontrar pistas que apontem o verdadeiro culpado.

Por isso peço que encontre 3 pistas ao longo dessa casa que possam nos ajudar a prender o assassino.

Procuramos por: uma possível arma do crime, algo que tenha sido usado para envenenar, documentos escondidos que possam ser úteis e algo que possa ter sido usado para descartar provas.

Acha que consegue?

// AQUI A GENTE INICIA A QUEST NO UNITY
~ StartQuest(ColetarPistasQuestId)
-> END

// ---------------------------------------------------------
// ESTADO: EM PROGRESSO (Jogador ainda não achou as 3 pistas)
// ---------------------------------------------------------
= emProgresso
#speaker: Policial Chefe
#portrait: Policial_Normal
Detetive, encontre as 3 pistas. A perícia não pode demorar muito.
Procure por venenos ou documentos.
-> END

// ---------------------------------------------------------
// ESTADO: PODE FINALIZAR (Achou as 3 pistas!)
// ---------------------------------------------------------
= entregar
#speaker: Policial Chefe
#portrait: Policial_Feliz
Excelente, detetive! Eu sabia que poderia contar com você.

Agora será fácil pressionar aqueles dois em busca de uma confissão. Esse caso está cada vez mais perto da resolução.

// AQUI A GENTE FINALIZA A QUEST NO UNITY E DÁ A RECOMPENSA
~ FinishQuest(ColetarPistasQuestId)
-> END

// ---------------------------------------------------------
// ESTADO: FINALIZADO (Já entregou tudo)
// ---------------------------------------------------------
= concluido
#speaker: Policial Chefe
#portrait: Policial_Normal
Obrigado pela ajuda, Detetive. Vamos levar as provas para analise.
-> END

= algoQuebrou
Erro: Estado da Quest desconhecido.
-> END

