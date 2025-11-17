using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Nesse exemplo, a quest precisa de um lugar para o jogador passar e completar o Step.
//Então, usamos um CircleCollider2D como trigger para detectar quando o jogador entra na área.
[RequireComponent(typeof(CircleCollider2D))]
public class ExemploQuestStep : QuestStep
{
    //Repare que a classe herda de QuestStep, então você pode usar os métodos protegidos dessa classe base
    //ou alterar conforme necessário.


    //Aqui dentro, você escreve a lógica que faz sentido para o seu Step. Cada quest deve ter sua própria lógica.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            FinishQuestStep();
        }
    }

    //Você tambem deve implementar o método SetQuestStepState, mesmo que ele seja vazio.
    protected override void SetQuestStepState(string state)
    {
        //Nesse exemplo, não precisamos fazer nada com o estado
    }

    //Agora que o script tá pronto, você pode criar um prefab de ExemploQuestStep e adicionar esse script a ele.
    //Depois, é só usar esse prefab na sua Quest.
}
