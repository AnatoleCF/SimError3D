using UnityEngine;
using System.Collections;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "new Error", menuName = "Errors/Create New Error")]
public class ErrorScriptable : ScriptableObject
{
    public int Number; //ID dans la base de données (l'excel du client)
    public string Name; //Nom de l'item
    public string Group; //Groupe pour classer comme dans la base de données
    public string[] Answers; //Les réponses à afficher au quizz
    public int[] CorrectAnswers; //Les bonnes  réponses (ID, de 0 à nbAnswers -1)
    [TextArea(minLines: 5, maxLines: 15)]
    public string ExplanationError; //Explication si le joueur s'est planté et que y'avait une erreur sur l'objet
    [TextArea(minLines: 5, maxLines: 15)]
    public string ExplanationNonError; //Explication si le joueur a success et que y'avait une erreur sur l'objet
}
