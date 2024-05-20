using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Console : MonoBehaviour
{
    public Model model;

    delegate void myDel();
    Dictionary<string, myDel> _myDictionary;
    public InputField input;
    public Text log;
    Button button;
    string[] tokens;

    #region parametros inputField

    //addcannon
    [HideInInspector] public int numberCannons;
    [HideInInspector] public bool isRight;

    //invulnerability
    [HideInInspector] public float timeInvulnerabiltiy;

    //addlife
    [HideInInspector] public int additionalLifes;

    #endregion

    void Start()
    {
        _myDictionary = new Dictionary<string, myDel>();
        AddCommand("addcannon", AddCannon);
        AddCommand("addlife", AddLife);
        AddCommand("invulnerability", Invulnerability);
        AddCommand("offnearobstacle", OffNearObstacle);
        AddCommand("help", Help);
        AddCommand("cleancheat", CleanCheats);
    }

    void AddCommand(string command, myDel function)
    {
        if (!_myDictionary.ContainsKey(command))
            _myDictionary.Add(command, function);
    }

    //Guardo en 'string[] tokens' cada palabra que ingrese el usuario separada por ','. Verifico si pertence a una key o no.
    public void CheckInput()
    {
        tokens = input.text.Split(new[] { "," }, StringSplitOptions.None);

        if (_myDictionary.ContainsKey(tokens[0]))
        {
            CheckCommand();
        }
        else log.text += "Ingresaste un cheat invalido, escribe el comando 'help' para ver la lista de comandos. \n";

    }

    //Verifica La primer linea de string ingresada en el inputField para ver que tipo de cheat es.
    public void CheckCommand()
    {
        if (tokens.Length == 1)
        {
            if (tokens[0] == "addcannon")
            {
                log.text += "\n" + "Debes declarar: \n" + "1. Numero de cañones a ingresar; '1' , '2' \n" + "2.Posicion del cañon; 'right' , 'left' \n \n" + "Example : addcannon,1,right \n";
            }
            if (tokens[0] == "addlife")
            {
                log.text += "\n" + "Debes declarar: \n" + "1. Cantidad de vidas adicionales. Tiene que ser un numero entre '1(minimo) - 4(maximo)'\n" + "Example : addlife,4 \n";
            }
            if (tokens[0] == "invulnerability")
            {
                log.text += "\n" + "Debes declarar: \n" + "1. Tiempo de invulnerabilidad(segundos) entre ; '5(minimo) - 30(maximo)' \n" + "Example : invulnerability,15 \n";
            }
            if (tokens[0] == "offnearobstacle")
            {
                log.text += "\n" + "Debes declarar: \n" + "1. Tipo de obstaculo a destruir; \n -mediumasteroid \n -bigasteroid \n -moveasteroid \n -enemy \n. \n Example : offnearobstacle,bigasteroid \n \n";
            }
            if (tokens[0] == "help" || tokens[0] == "cleancheat") _myDictionary[tokens[0]]();
            //if (tokens[0] == "cleancheat") _myDictionary[tokens[0]]();
        }
        else if (tokens[0] == "addcannon") AddCannonParameters();
        else if (tokens[0] == "invulnerability") InvulnerabilityParameters();
        else if (tokens[0] == "addlife") AddLifeParamaters();
        else if (tokens[0] == "offnearobstacle") OffNearObstacleParameters();
    }

    //Limpia el texto en consola.
    public void CleanText()
    {
        log.text = "";
    }

    //Debuguea en la consola la lista de cheats.
    public void Help()
    {
        log.text += "\n" +
            "'Asegurese de no dejar espacios al escribir los parametros'. \n \n \n" +
            "-addcannon : agregar cañon de disparo(maxima cantidad 2), puede elegir si agregarlo a la izquierda o derecha. Uno por lado.\n Example: addcannon,1,right \n\n" +
            "-invulnerability : invulnerabilidad, debe ingresar el tiempo de duracion del efecto(segundos). El tiempo de duracion debe ser un numero entre '5(minimo) - 30(maximo)' segundos.\n Example: invulnerability,15 \n\n" +
            "-addlife : vida adicional, debe ingresar la cantidad de vidas adicionales. La cantidad de vidas a ingresar debe ser un numero entre '1(minimo) - 5(maximo)'.\n Example: addlife,5 \n \n" +
            "-offnearobstacle : desactiva 1 obstaculo a eleccion. Debes declarar cual de los siguientes obstaculos quiere destruir; \n -smallasteroid \n -mediumasteroid \n -bigasteroid \n -moveasteroid \n -enemy \n. \n Example : offnearobstacle,bigasteroid \n\n" +
            "-help : lista de cheats. \n \n"
            ;
    }

    //Resetea a todos los valores por predeterminado al ingresar un nuevo cheat.
    public void CleanCheats()
    {
        model.view.offNearObsacleCannon.SetActive(false);
        model.cannonLeft.SetActive(false);
        model.cannonRight.SetActive(false);

        model.OffCheat();
        model.canDie = true;
        model.aditionalLife = 0;
        model.view.StopAllCoroutines();
        model.view.meshModel.enabled = true;
        model.timeInvulnerability = 0f;
        model.view.Invulnerability(false);
        foreach (var item in model.view.listAdditionalLifes)
        {
            item.SetActive(false);
        }

        if (tokens[0] == "cleancheat") log.text += "\n" + "Cheat desactivado \n ";
    }


    #region Parametros de cheats

    #region Cheat: addcannon
    public void AddCannonParameters()
    {
        CleanCheats();
        if (tokens.Length >= 2 && tokens.Length <= 3)
        {
            if (tokens[1] == "1")
            {
                numberCannons = 1;
                if (tokens.Length < 3) log.text += "Falta declarar la posicion del cañon; 'right' o 'left' \n";
            }
            if (tokens[1] == "2")
            {
                if (tokens.Length < 3)
                {
                    numberCannons = 2;
                    _myDictionary[tokens[0]]();
                }
            }
            if (tokens[1] != "1" && tokens[1] != "2") log.text += "Ingresaste un parametro incorreto. El numero de cañones a ingresar es ; '1' o '2' \n";
        }

        if (tokens[1] == "1")
        {
            if (tokens.Length == 3)
            {
                if (tokens[2] == "right")
                {
                    isRight = true;
                    _myDictionary[tokens[0]]();
                }
                if (tokens[2] == "left")
                {
                    isRight = false;
                    _myDictionary[tokens[0]]();
                }
                if (tokens[2] != "right" && tokens[2] != "left") log.text += "Ingresaste un parametro incorreto.Debes decidir la posicion del cañon; 'right','left'\n";
            }
        }
        else if (tokens[1] == "2" && tokens.Length == 3) log.text += "Si ingresas 2(dos) cañones, automaticamente se añadira uno a la izquierda y otro a la derecha. Debes reescribir el cheat correctamente.\n Example : addcannon,2 \n";

        if (tokens.Length >= 4) log.text += "Codigo incorrecto. Ingresaste un parametro de mas.El limite son 2 parametros.\n Example : addcannon,1,right \n ";
    }

    public void AddCannon()
    {
        //CleanCheats();
        model.cheatAdditionalCannon = true;
        if (numberCannons == 1)
        {
            if (isRight)
            {
                model.cannonLeft.SetActive(false);
                model.cannonRight.SetActive(true);
                log.text += "Cheat activado. Cañones activos 1(derecha) \n";
            }
            if (!isRight)
            {
                model.cannonLeft.SetActive(true);
                model.cannonRight.SetActive(false);
                log.text += "Cheat activado. Cañones activos 1(izquierda) \n";
            }
        }
        if (numberCannons == 2)
        {
            model.cannonLeft.SetActive(true);
            model.cannonRight.SetActive(true);
            log.text += "Cheat activado. Cañones activos 2 \n";
        }
    }
    #endregion

    #region Cheat: invulnerability
    bool _isNumber;
    bool _activeModelFeedBuck;

    void InvulnerabilityParameters()
    {
        CleanCheats();
        _isNumber = tokens[1].All(char.IsDigit);
        if (_isNumber)
        {
            timeInvulnerabiltiy = float.Parse(tokens[1]);
            if (tokens.Length == 2)
            {
                if (timeInvulnerabiltiy >= 5 && timeInvulnerabiltiy <= 30) Invulnerability();
                else log.text += "Ingresaste un parametro incorreto.El numero a ingresar es entre '5 y 30' segundos.\n";
            }
            if (tokens.Length > 2) log.text += "Codigo incorrecto. Ingresaste un parametro de mas.El limite es 1(uno) parametro.\n Example : invulnerability,10 \n ";
        }
        else log.text += "Ingresaste un parametro incorreto.Debes ingresar un numero entre '5(minimo) y 30(maximo)' segundos.\n";
    }

    void Invulnerability()
    {
        log.text += "Cheat Activado. Seras invulnerable durante " + "(" + timeInvulnerabiltiy + ")" + " segundos.\n";
        _activeModelFeedBuck = true;
        model.timeInvulnerability = timeInvulnerabiltiy;
        model.InvulnerabilityStartegyState();
        model.view.Invulnerability(_activeModelFeedBuck);
    }
    #endregion

    #region Cheat: addlife

    void AddLifeParamaters()
    {
        CleanCheats();
        _isNumber = tokens[1].All(char.IsDigit); //_isNumber == true , si lo que contiene el string es un digito.
        if (_isNumber)
        {
            additionalLifes = int.Parse(tokens[1]);
            if (tokens.Length == 2)
            {
                if (additionalLifes >= 1 && additionalLifes <= 5) AddLife();
                else log.text += "Ingresaste un parametro incorreto.El numero a ingresar es entre '1(minimo) - 5(maximo)' segundos.\n";
            }
            if (tokens.Length > 2) log.text += "Codigo incorrecto. Ingresaste un parametro de mas.El limite es 1(uno) parametro.\n Example : addlife,3 \n ";
        }
        else log.text += "Ingresaste un parametro incorreto.Debes ingresar un numero entre '1(minimo) - 5(maximo)' segundos.\n";
    }

    void AddLife()
    {
        model.additionalLifeState = true;
        model.view.FeedBuckAdditionalLifes(additionalLifes);
        model.aditionalLife = additionalLifes;
        model.AddLifeStrategyState();
        log.text += "Codigo activado.Tiene (" + additionalLifes + ") vidas adicionales.\n";
    }

    #endregion

    #region Cheat: offnearobstacle

    int _obstacleTypeIndex;
    
    void OffNearObstacleParameters()
    {
        CleanCheats();
        if (tokens.Length == 2)
        {
            if (tokens[1] == "mediumasteroid")
            {
                _obstacleTypeIndex = 0;
                OffNearObstacle();
            }
            if (tokens[1] == "bigasteroid")
            {
                _obstacleTypeIndex = 1;
                OffNearObstacle();
            }
            if (tokens[1] == "enemy")
            {
                _obstacleTypeIndex = 2;
                OffNearObstacle();
            }
            if (tokens[1] == "moveasteroid")
            {
                _obstacleTypeIndex = 3;
                OffNearObstacle();
            }
            else if (tokens[1] == "mediumasteroid" || tokens[1] == "bigasteroid" || tokens[1] == "moveasteroid" || tokens[1] == "enemy") ;
            else log.text += "Ingresaste un parametro incorrecto. Debes declar el tipo de obstaculo a destruir;\n -mediumasteroid \n -bigasteroid \n -moveasteroid \n -enemy \n. \n Example : offnearobstacle,bigasteroid \n \n";
        }
        else if (tokens.Length > 2) log.text += "Ingresaste un parametro de mas. El maximo de parametros a ingresar es 1(uno). \n Example : offnearobstacle, enemy \n\n";
    }


    void OffNearObstacle()
    {
        model.view.offNearObsacleCannon.SetActive(true);
        model.obstacleType = _obstacleTypeIndex;
        model.OffNearObstacleStrategyState();
        log.text += "Codigo Activado. Se destruira el obstaculo : (" + tokens[1] + ") cuando estes cerca. \n";
        Debug.Log(model.obstacleType);
    }

    #endregion

    #endregion
}
