using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tetris : MonoBehaviour
{

    float startTime;
    float time;
    float previousTime;
    public int score;

    GameObject[,] tetris;
    GameObject spawn;
    GameObject[] current;

    int[] lines;
    int[] columns;

    public Material yellow;
    public Material orange;
    public Material purple;
    public Material blue;
    public Material cyan;
    public Material green;
    public Material gray;
    public Material red;

    Material currentColor;

    string pieceName;
    int rotation;

    float lastStep;
    float timeBetweenSteps;

    // Start is called before the first frame update
    void Start()
    {
        tetris = new GameObject[25, 10];
        fillTetrisMatrix();
;       spawn = tetris[3, 4];
        current = new GameObject[4];
        lines = new int[4];
        columns = new int[4];
        pieceName = "";
        rotation = 1;
        score = 0;
        startTime = 0.8f;
        time = startTime;
        timeBetweenSteps = 0.1f;
    }

    public void fillTetrisMatrix() {
        for(int i=0; i<25; i++) {
            for(int j=0; j<10; j++) {
                tetris[i, j] = gameObject.transform.GetChild(i).transform.GetChild(j).gameObject;
                tetris[i, j].name = "c-" + i + j;
            }
        }
    }

    public void spawnPiece() {
        //escolher dentre as 7 peças
        int randomPiece = UnityEngine.Random.Range(0,7); //0 a 7
        rotation = 1;
        
        //C
        
        if (randomPiece == 0) {
            pieceName = "c";
            current[0] = tetris[2, 5]; lines[0] = 2; columns[0] = 5;
            current[1] = tetris[1, 5]; lines[1] = 1; columns[1] = 5;
            current[2] = tetris[1, 4]; lines[2] = 1; columns[2] = 4;
            current[3] = tetris[2, 4]; lines[3] = 2; columns[3] = 4;

            currentColor = yellow;
        }

        //B
        
        if (randomPiece == 1) {
            pieceName = "b";
            current[0] = tetris[2, 3]; lines[0] = 2; columns[0] = 3;
            current[1] = tetris[2, 4]; lines[1] = 2; columns[1] = 4;
            current[2] = tetris[2, 5]; lines[2] = 2; columns[2] = 5;
            current[3] = tetris[2, 6]; lines[3] = 2; columns[3] = 6;

            currentColor = cyan;
        }

        //T
        
        if (randomPiece == 2) {
            pieceName = "t";
            current[0] = tetris[2, 4]; lines[0] = 2; columns[0] = 4; //centro
            current[1] = tetris[1, 4]; lines[1] = 1; columns[1] = 4; //cima
            current[2] = tetris[2, 3]; lines[2] = 2; columns[2] = 3;
            current[3] = tetris[2, 5]; lines[3] = 2; columns[3] = 5;

            currentColor = purple;
        }

        //L1
        
        if (randomPiece == 3) {
            pieceName = "l1";
            current[0] = tetris[2, 4]; lines[2] = 2; columns[2] = 4; //centro
            current[1] = tetris[1, 3]; lines[1] = 1; columns[1] = 3; //cima
            current[2] = tetris[2, 3]; lines[0] = 2; columns[0] = 3; //esquerda
            current[3] = tetris[2, 5]; lines[3] = 2; columns[3] = 5; //direita

            currentColor = blue;
        }

        //L2
        
        if (randomPiece == 4) {
            pieceName = "l2";
            current[0] = tetris[2, 3]; lines[0] = 2; columns[0] = 3;
            current[1] = tetris[1, 5]; lines[1] = 1; columns[1] = 5;
            current[2] = tetris[2, 4]; lines[2] = 2; columns[2] = 4;
            current[3] = tetris[2, 5]; lines[3] = 2; columns[3] = 5;

            currentColor = orange;
        }

        //S1
        
        if (randomPiece == 5) {
            pieceName = "s1";
            current[0] = tetris[2, 4]; lines[0] = 2; columns[0] = 4;
            current[1] = tetris[2, 3]; lines[1] = 2; columns[1] = 3;
            current[2] = tetris[1, 4]; lines[2] = 1; columns[2] = 4;
            current[3] = tetris[1, 5]; lines[3] = 1; columns[3] = 5; 

            currentColor = green;
        }

        //S2
        
        if (randomPiece == 6) {
            pieceName = "s2";
            current[0] = tetris[2, 4]; lines[0] = 2; columns[0] = 4;
            current[1] = tetris[1, 4]; lines[1] = 1; columns[1] = 4;
            current[2] = tetris[1, 3]; lines[2] = 1; columns[2] = 3;
            current[3] = tetris[2, 5]; lines[3] = 2; columns[3] = 5; 

            currentColor = red;
        }

        for (int i=0; i<4; i++) {
            current[i].GetComponent<Renderer>().material = currentColor;
        }

        nextLine();
        nextLine();
        nextLine();

    }

    public void nextLine() {
        for (int i = 0; i < 4; i++) {
            current[i].GetComponent<Renderer>().material = gray;
        }

        for(int i = 0; i < 4; i++) {
            lines[i] = lines[i]+1;
        }

        for (int i = 0; i < 4; i++) {
            current[i] = tetris[lines[i], columns[i]];
        }

        for (int i = 0; i < 4; i++) {
            current[i].GetComponent<Renderer>().material = currentColor;
        }
    }

    public bool isBottom() {
        bool teste = false;

        for(int i=0; i<4; i++) {
            if (lines[i] == 24) {
                teste = true;
            }
        }

        return teste;
    }

    public void moveLeft() {
        bool canMoveLeft = true;
        for(int i=0; i<4; i++) {
            if (columns[i] == 0) {
                canMoveLeft = false;
            } else {
                if (isPieceLeft()) {
                    canMoveLeft = false;
                }
            }
        }

        if (canMoveLeft) {
            for (int i = 0; i < 4; i++) {
                current[i].GetComponent<Renderer>().material = gray;
            }

            for (int i = 0; i < 4; i++) {
                columns[i] = columns[i] - 1;
            }

            for (int i = 0; i < 4; i++) {
                current[i] = tetris[lines[i], columns[i]];
            }

            for (int i = 0; i < 4; i++) {
                current[i].GetComponent<Renderer>().material = currentColor;
            }
        }
    }

    public void moveRight() {
        bool canMoveRight = true;
        for (int i = 0; i < 4; i++) {
            if (columns[i] == 9) {
                canMoveRight = false;
            } else {
                if (isPieceRight()) {
                    canMoveRight = false;
                }
            }
        }

        if (canMoveRight) {
            for (int i = 0; i < 4; i++) {
                current[i].GetComponent<Renderer>().material = gray;
            }

            for (int i = 0; i < 4; i++) {
                columns[i] = columns[i] + 1;
            }

            for (int i = 0; i < 4; i++) {
                current[i] = tetris[lines[i], columns[i]];
            }

            for (int i = 0; i < 4; i++) {
                current[i].GetComponent<Renderer>().material = currentColor;
            }
        }
    }

    public bool isPieceLeft() {
        bool teste = false;

        List<GameObject> colisoes = new List<GameObject>();

        for (int i = 0; i < 4; i++) {
            if (columns[i] - 1 >= 0) {
                GameObject leftPiece = tetris[lines[i], columns[i] - 1];
                //é uma peça e não faz parte desta peça (evitar falso positivo)
                if (!leftPiece.GetComponent<Renderer>().material.name.Contains("gray")) {
                    if (!containsPiece(leftPiece.name)) {
                        //print("L slot não faz parte da peça");

                        //print("L parte=" + tetris[lines[i], columns[i]]);
                        //print("L slot=" + leftPiece.name);

                        colisoes.Add(leftPiece);
                        teste = true;
                    } else {
                        //print("L faz parte da peça");
                        //print("L parte=" + leftPiece.name);
                    }
                } else {
                    //print("L slot cinza esquerda");
                    //print(leftPiece.name);
                }
            }
        }

        return teste;
    }

    public bool isPieceRight() {
        bool teste = false;

        List<GameObject> colisoes = new List<GameObject>();

        for (int i = 0; i < 4; i++) {
            if(columns[i] + 1 <= 9){
                GameObject rightPiece = tetris[lines[i], columns[i] + 1];
                //é uma peça e não faz parte desta peça (evitar falso positivo)
                if (!rightPiece.GetComponent<Renderer>().material.name.Contains("gray")) {
                    if (!containsPiece(rightPiece.name)) {
                        //print("R slot não faz parte da peça");

                        //print("R parte=" + tetris[lines[i], columns[i]]);
                        //print("R slot=" + rightPiece.name);

                        colisoes.Add(rightPiece);
                        teste = true;
                    } else {
                        //print("R faz parte da peça");
                        //print("R parte=" + rightPiece.name);
                    }
                } else {
                    //print("R slot cinza esquerda");
                    //print(rightPiece.name);
                }
            }
        }

        return teste;
    }

    public bool isPieceBellow() {
        bool teste = false;

        List<GameObject> colisoes = new List<GameObject>();

        for(int i=0; i<4; i++) {
            GameObject bellowPiece = tetris[lines[i] + 1, columns[i]];
            //é uma peça e não faz parte desta peça (evitar falso positivo)
            if (!bellowPiece.GetComponent<Renderer>().material.name.Contains("gray")) {
                if (!containsPiece(bellowPiece.name)) {
                    //print("slot não faz parte da peça");

                    //print("parte=" + tetris[lines[i], columns[i]]);
                    //print("slot="+bellowPiece.name);
                    
                    colisoes.Add(bellowPiece);
                    teste = true;
                } else {
                    //print("faz parte da peça");
                    //print("parte=" + bellowPiece.name);
                }
            } else {
                //print("slot cinza abaixo");
                //print(bellowPiece.name);
            }
        }

        return teste;
    }

    public bool containsPiece(string name) {
        bool teste = false;

        for(int i = 0; i < 4; i++) {
            if (current[i].name.Equals(name)) {
                teste = true;
            }
        }

        return teste;
    }

    public bool verifyLose() {
        bool teste = false;

        for(int i=0; i<4; i++) {
            if(lines[i] < 5) {
                print("perdeu");

                int highscore = PlayerPrefs.GetInt("highscore");
                if (score > highscore) {
                    highscore = score;
                }

                PlayerPrefs.SetInt("highscore", highscore);

                SceneManager.LoadScene("lose");
            }
        }

        return teste;
    }

    public void deleteCurrent() {
        current[0] = null;
        current[1] = null;
        current[2] = null;
        current[3] = null;

        for(int i = 0; i < 4; i++) {
            lines[i] = 0;
            columns[i] = 0;
        }

        verifyLines();
    }

    public void verifyLines() {
        for (int i = 5; i < 25; i++) {
            int cont = 0;
            for (int j = 0; j < 10; j++) {
                if (!tetris[i,j].GetComponent<Renderer>().material.name.Contains("gray")) {

                    cont++;

                }
            }
            if (cont == 10) {
                print("PONTO !!!");

                GetComponent<AudioSource>().Play();

                for (int i2 = i; i2 > 5; i2--) {
                    for (int k = 0; k < 10; k++) {
                        tetris[i2, k].GetComponent<Renderer>().material = 
                            tetris[i2 - 1, k].GetComponent<Renderer>().material;
                    }
                }
                score += 100;
                verifyLines();

            }
        }
    }

    public void rotatePiece() {
        int[] linesT = new int[4];
        int[] columnsT = new int[4];
        bool canRotate = true;

        print("ROTATE = " + pieceName);
        if (pieceName.Equals("c")) {

        }
        if (pieceName.Equals("b")) {
            if (rotation == 1) {
                //horizontal para vertical 1
                int l = lines[0];
                int c = columns[2];

                //VERIFICAR SE NÃO BATE EM NADA AO RODAR!
                
                for (int i = 0; i < 4; i++) {
                    current[i].GetComponent<Renderer>().material = gray;
                }

                linesT[0] = l-1; columnsT[0] = c;
                linesT[1] = l  ; columnsT[1] = c;
                linesT[2] = l+1; columnsT[2] = c;
                linesT[3] = l+2; columnsT[3] = c;

                for(int i=0; i<4; i++) {
                    if(linesT[i] > 24 || 
                        columnsT[i] > 9 || 
                        columnsT[i] < 0 || 
                        !tetris[linesT[i],columnsT[i]].GetComponent<Renderer>().material.name.Contains("gray")) {

                        canRotate = false;
                    }
                }

                if (canRotate) {
                    for (int i = 0; i < 4; i++) {
                        lines[i] = linesT[i];
                        columns[i] = columnsT[i];
                        current[i] = tetris[lines[i], columns[i]];
                        rotation = 2;
                    }
                }

                for (int i = 0; i < 4; i++) {
                    current[i].GetComponent<Renderer>().material = currentColor;
                }
               
            }else if (rotation == 2) {
                //em pé para horizontal 1
                int l = lines[1]+1;
                int c = columns[0];

                print("ROTATE BAR");

                for (int i = 0; i < 4; i++) {
                    current[i].GetComponent<Renderer>().material = gray;
                }

                linesT[0] = l; columnsT[0] = c-2;
                linesT[1] = l; columnsT[1] = c-1;
                linesT[2] = l; columnsT[2] = c;
                linesT[3] = l; columnsT[3] = c+1;

                for (int i = 0; i < 4; i++) {
                    if (linesT[i] > 24 ||
                        columnsT[i] > 9 ||
                        columnsT[i] < 0 ||
                        !tetris[linesT[i], columnsT[i]].GetComponent<Renderer>().material.name.Contains("gray")) {

                        canRotate = false;
                    }
                }

                if (canRotate) {
                    for (int i = 0; i < 4; i++) {
                        lines[i] = linesT[i];
                        columns[i] = columnsT[i];
                        current[i] = tetris[lines[i], columns[i]];
                        rotation = 3;
                    }
                }

                for (int i = 0; i < 4; i++) {
                    current[i].GetComponent<Renderer>().material = currentColor;
                }
                
            } else if (rotation == 3) {
                //horizontal para em pé 2
                int l = lines[0];
                int c = columns[1];

                for (int i = 0; i < 4; i++) {
                    current[i].GetComponent<Renderer>().material = gray;
                }

                linesT[0] = l-2; columnsT[0] = c;
                linesT[1] = l-1; columnsT[1] = c;
                linesT[2] = l  ; columnsT[2] = c;
                linesT[3] = l+1; columnsT[3] = c;

                for (int i = 0; i < 4; i++) {
                    if (linesT[i] > 24 ||
                        columnsT[i] > 9 ||
                        columnsT[i] < 0 ||
                        !tetris[linesT[i], columnsT[i]].GetComponent<Renderer>().material.name.Contains("gray")) {

                        canRotate = false;
                    }
                }

                if (canRotate) {
                    for (int i = 0; i < 4; i++) {
                        lines[i] = linesT[i];
                        columns[i] = columnsT[i];
                        current[i] = tetris[lines[i], columns[i]];
                        rotation = 4;
                    }
                }

                for (int i = 0; i < 4; i++) {
                    current[i].GetComponent<Renderer>().material = currentColor;
                }
                
            } else if (rotation == 4) {
                //em pé para horizontal 2
                int l = lines[1];
                int c = columns[0];

                print("ROTATE BAR");

                for (int i = 0; i < 4; i++) {
                    current[i].GetComponent<Renderer>().material = gray;
                }

                linesT[0] = l; columnsT[0] = c - 1;
                linesT[1] = l; columnsT[1] = c;
                linesT[2] = l; columnsT[2] = c + 1;
                linesT[3] = l; columnsT[3] = c + 2;

                for (int i = 0; i < 4; i++) {
                    if (linesT[i] > 24 ||
                        columnsT[i] > 9 ||
                        columnsT[i] < 0 ||
                        !tetris[linesT[i], columnsT[i]].GetComponent<Renderer>().material.name.Contains("gray")) {

                        canRotate = false;
                    }
                }

                if (canRotate) {
                    for (int i = 0; i < 4; i++) {
                        lines[i] = linesT[i];
                        columns[i] = columnsT[i];
                        current[i] = tetris[lines[i], columns[i]];
                        rotation = 1;
                    }
                }

                for (int i = 0; i < 4; i++) {
                    current[i].GetComponent<Renderer>().material = currentColor;
                }
            }
         }

        //T
        if (pieceName.Equals("t")) {
            if (rotation == 1) {
                //horizontal para em pé 1
             
                print("ROTATE T");

                for (int i = 0; i < 4; i++) {
                    current[i].GetComponent<Renderer>().material = gray;
                }

                linesT[0] = lines[0]; columnsT[0] = columns[0];
                linesT[1] = lines[1]; columnsT[1] = columns[1];
                linesT[2] = lines[2] + 1; columnsT[2] = columns[2] + 1; //vai pra baixo
                linesT[3] = lines[3]; columnsT[3] = columns[3];

                for (int i = 0; i < 4; i++) {
                    if (linesT[i] > 24 ||
                        columnsT[i] > 9 ||
                        columnsT[i] < 0 ||
                        !tetris[linesT[i], columnsT[i]].GetComponent<Renderer>().material.name.Contains("gray")) {

                        canRotate = false;
                    }
                }

                if (canRotate) {
                    for (int i = 0; i < 4; i++) {
                        lines[i] = linesT[i];
                        columns[i] = columnsT[i];
                        current[i] = tetris[lines[i], columns[i]];
                        rotation = 2;
                    }
                }

                for (int i = 0; i < 4; i++) {
                    current[i].GetComponent<Renderer>().material = currentColor;
                }
            }else if (rotation == 2) {
                //em pé para horizontal 1
                int l = lines[2]; //current[2] vai para baixo
                int c = columns[0];

                print("ROTATE T");

                for (int i = 0; i < 4; i++) {
                    current[i].GetComponent<Renderer>().material = gray;
                }

                linesT[0] = lines[0]; columnsT[0] = columns[0];
                linesT[1] = lines[1]+1; columnsT[1] = columns[1]-1; //cima vai para esquerda
                linesT[2] = lines[2]; columnsT[2] = columns[2]; 
                linesT[3] = lines[3]; columnsT[3] = columns[3];

                for (int i = 0; i < 4; i++) {
                    if (linesT[i] > 24 ||
                        columnsT[i] > 9 ||
                        columnsT[i] < 0 ||
                        !tetris[linesT[i], columnsT[i]].GetComponent<Renderer>().material.name.Contains("gray")) {

                        canRotate = false;
                    }
                }

                if (canRotate) {
                    for (int i = 0; i < 4; i++) {
                        lines[i] = linesT[i];
                        columns[i] = columnsT[i];
                        current[i] = tetris[lines[i], columns[i]];
                        rotation = 3;
                    }
                }

                for (int i = 0; i < 4; i++) {
                    current[i].GetComponent<Renderer>().material = currentColor;
                }
            } else if (rotation == 3) {
                //horizontal para em pé 2
                
                print("ROTATE T");

                for (int i = 0; i < 4; i++) {
                    current[i].GetComponent<Renderer>().material = gray;
                }

                linesT[0] = lines[0]; columnsT[0] = columns[0];
                linesT[1] = lines[1]; columnsT[1] = columns[1]; 
                linesT[2] = lines[2]; columnsT[2] = columns[2]; 
                linesT[3] = lines[3]-1; columnsT[3] = columns[3]-1; //direita vai pra cima

                for (int i = 0; i < 4; i++) {
                    if (linesT[i] > 24 ||
                        columnsT[i] > 9 ||
                        columnsT[i] < 0 ||
                        !tetris[linesT[i], columnsT[i]].GetComponent<Renderer>().material.name.Contains("gray")) {

                        canRotate = false;
                    }
                }

                if (canRotate) {
                    for (int i = 0; i < 4; i++) {
                        lines[i] = linesT[i];
                        columns[i] = columnsT[i];
                        current[i] = tetris[lines[i], columns[i]];
                        rotation = 4;
                    }
                }



                for (int i = 0; i < 4; i++) {
                    current[i].GetComponent<Renderer>().material = currentColor;
                }
            }else if (rotation == 4) {
                //horizontal para em pé 2
                
                print("ROTATE T");

                for (int i = 0; i < 4; i++) {
                    current[i].GetComponent<Renderer>().material = gray;
                }

                linesT[0] = lines[0]; columnsT[0] = columns[0];
                linesT[1] = lines[1]; columnsT[1] = columns[1]; 
                linesT[2] = lines[2]-1; columnsT[2] = columns[2]+1; //baixo vai pra cima 
                linesT[3] = lines[3]; columnsT[3] = columns[3]; 

                for (int i = 0; i < 4; i++) {
                    if (linesT[i] > 24 ||
                        columnsT[i] > 9 ||
                        columnsT[i] < 0 ||
                        !tetris[linesT[i], columnsT[i]].GetComponent<Renderer>().material.name.Contains("gray")) {

                        canRotate = false;
                    }
                }

                if (canRotate) {
                    for (int i = 0; i < 4; i++) {
                        lines[i] = linesT[i];
                        columns[i] = columnsT[i];
                        current[i] = tetris[lines[i], columns[i]];
                        rotation = 1;
                    }
                }

                for (int i = 0; i < 4; i++) {
                    current[i].GetComponent<Renderer>().material = currentColor;
                }

                if (canRotate) {
                    //troca 1 por 2
                    int temp = lines[1];
                    lines[1] = lines[2];
                    lines[2] = temp;

                    temp = columns[1];
                    columns[1] = columns[2];
                    columns[2] = temp;

                    GameObject goTemp = current[1];
                    current[1] = current[2];
                    current[2] = goTemp;

                    //troca 1 por 3
                    temp = lines[1];
                    lines[1] = lines[3];
                    lines[3] = temp;

                    temp = columns[1];
                    columns[1] = columns[3];
                    columns[3] = temp;

                    goTemp = current[1];
                    current[1] = current[3];
                    current[3] = goTemp;
                }
            }
        }//fim B

        if (pieceName.Equals("l1")) {
            if (rotation == 1) {
                //horizontal para em pé 1

                print("ROTATE L1");

                for (int i = 0; i < 4; i++) {
                    current[i].GetComponent<Renderer>().material = gray;
                }

                linesT[0] = lines[0]-1;   columnsT[0] = columns[0]+1; //esquerda
                linesT[1] = lines[1];   columnsT[1] = columns[1]+2; //cima
                linesT[2] = lines[2];   columnsT[2] = columns[2]; //centro não mexe
                linesT[3] = lines[3]+1;   columnsT[3] = columns[3]-1; //direita 

                for (int i = 0; i < 4; i++) {
                    if (linesT[i] > 24 ||
                        columnsT[i] > 9 ||
                        columnsT[i] < 0 ||
                        !tetris[linesT[i], columnsT[i]].GetComponent<Renderer>().material.name.Contains("gray")) {

                        canRotate = false;
                    }
                }

                if (canRotate) {
                    for (int i = 0; i < 4; i++) {
                        lines[i] = linesT[i];
                        columns[i] = columnsT[i];
                        current[i] = tetris[lines[i], columns[i]];
                        rotation = 2;
                    }
                }

                for (int i = 0; i < 4; i++) {
                    current[i].GetComponent<Renderer>().material = currentColor;
                }
            } else if (rotation == 2) {
                //horizontal para em pé 1

                print("ROTATE L1");

                for (int i = 0; i < 4; i++) {
                    current[i].GetComponent<Renderer>().material = gray;
                }

                linesT[0] = lines[0] + 1; columnsT[0] = columns[0] + 1; //esquerda 2
                linesT[1] = lines[1] + 2; columnsT[1] = columns[1]; //cima 1
                linesT[2] = lines[2]; columnsT[2] = columns[2]; //centro não mexe 0
                linesT[3] = lines[3] - 1; columnsT[3] = columns[3] - 1; //direita 3

                for (int i = 0; i < 4; i++) {
                    if (linesT[i] > 24 ||
                        columnsT[i] > 9 ||
                        columnsT[i] < 0 ||
                        !tetris[linesT[i], columnsT[i]].GetComponent<Renderer>().material.name.Contains("gray")) {

                        canRotate = false;
                    }
                }

                if (canRotate) {
                    for (int i = 0; i < 4; i++) {
                        lines[i] = linesT[i];
                        columns[i] = columnsT[i];
                        current[i] = tetris[lines[i], columns[i]];
                        rotation = 3;
                    }
                }

                for (int i = 0; i < 4; i++) {
                    current[i].GetComponent<Renderer>().material = currentColor;
                }
            }else if (rotation == 3) {
                //horizontal para em pé 1

                print("ROTATE L1");

                for (int i = 0; i < 4; i++) {
                    current[i].GetComponent<Renderer>().material = gray;
                }

                linesT[0] = lines[0]+1; columnsT[0] = columns[0]-1; //esquerda 2
                linesT[1] = lines[1]; columnsT[1] = columns[1]-2; //cima 1
                linesT[2] = lines[2]; columnsT[2] = columns[2]; //centro não mexe 0
                linesT[3] = lines[3]-1; columnsT[3] = columns[3]+1; //direita 3

                for (int i = 0; i < 4; i++) {
                    if (linesT[i] > 24 ||
                        columnsT[i] > 9 ||
                        columnsT[i] < 0 ||
                        !tetris[linesT[i], columnsT[i]].GetComponent<Renderer>().material.name.Contains("gray")) {

                        canRotate = false;
                    }
                }

                if (canRotate) {
                    for (int i = 0; i < 4; i++) {
                        lines[i] = linesT[i];
                        columns[i] = columnsT[i];
                        current[i] = tetris[lines[i], columns[i]];
                        rotation = 4;
                    }
                }

                for (int i = 0; i < 4; i++) {
                    current[i].GetComponent<Renderer>().material = currentColor;
                }
            } else if (rotation == 4) {
                //horizontal para em pé 1

                print("ROTATE L1");

                for (int i = 0; i < 4; i++) {
                    current[i].GetComponent<Renderer>().material = gray;
                }

                linesT[0] = lines[0]-1; columnsT[0] = columns[0]-1; //esquerda 2
                linesT[1] = lines[1]-2; columnsT[1] = columns[1]; //cima 1
                linesT[2] = lines[2]; columnsT[2] = columns[2]; //centro não mexe 0
                linesT[3] = lines[3]+1; columnsT[3] = columns[3]+1; //direita 3

                for (int i = 0; i < 4; i++) {
                    if (linesT[i] > 24 ||
                        columnsT[i] > 9 ||
                        columnsT[i] < 0 ||
                        !tetris[linesT[i], columnsT[i]].GetComponent<Renderer>().material.name.Contains("gray")) {

                        canRotate = false;
                    }
                }

                if (canRotate) {
                    for (int i = 0; i < 4; i++) {
                        lines[i] = linesT[i];
                        columns[i] = columnsT[i];
                        current[i] = tetris[lines[i], columns[i]];
                        rotation = 1;
                    }
                }

                for (int i = 0; i < 4; i++) {
                    current[i].GetComponent<Renderer>().material = currentColor;
                }
            }

        }//fim L1


        //L2
        if (pieceName.Equals("l2")) {
            if (rotation == 1) {
                //horizontal para em pé 1

                print("ROTATE L2");

                for (int i = 0; i < 4; i++) {
                    current[i].GetComponent<Renderer>().material = gray;
                }

                linesT[0] = lines[0]-1; columnsT[0] = columns[0]+1; //esquerda 3
                linesT[1] = lines[1]+2; columnsT[1] = columns[1];   //cima 2
                linesT[2] = lines[2]; columnsT[2] = columns[2];     //centro 0
                linesT[3] = lines[3]+1; columnsT[3] = columns[3]-1; //direita 1

                for (int i = 0; i < 4; i++) {
                    if (linesT[i] > 24 ||
                        columnsT[i] > 9 ||
                        columnsT[i] < 0 ||
                        !tetris[linesT[i], columnsT[i]].GetComponent<Renderer>().material.name.Contains("gray")) {

                        canRotate = false;
                    }
                }

                if (canRotate) {
                    for (int i = 0; i < 4; i++) {
                        lines[i] = linesT[i];
                        columns[i] = columnsT[i];
                        current[i] = tetris[lines[i], columns[i]];
                        rotation = 2;
                    }
                }

                for (int i = 0; i < 4; i++) {
                    current[i].GetComponent<Renderer>().material = currentColor;
                }
            }else if (rotation == 2) {
                //horizontal para em pé 1

                print("ROTATE L2");

                for (int i = 0; i < 4; i++) {
                    current[i].GetComponent<Renderer>().material = gray;
                }

                linesT[0] = lines[0]+1; columnsT[0] = columns[0]+1; // 3
                linesT[1] = lines[1]; columnsT[1] = columns[1]-2;   // 2
                linesT[2] = lines[2]; columnsT[2] = columns[2];     // 0
                linesT[3] = lines[3]-1; columnsT[3] = columns[3]-1; // 1

                for (int i = 0; i < 4; i++) {
                    if (linesT[i] > 24 ||
                        columnsT[i] > 9 ||
                        columnsT[i] < 0 ||
                        !tetris[linesT[i], columnsT[i]].GetComponent<Renderer>().material.name.Contains("gray")) {

                        canRotate = false;
                    }
                }

                if (canRotate) {
                    for (int i = 0; i < 4; i++) {
                        lines[i] = linesT[i];
                        columns[i] = columnsT[i];
                        current[i] = tetris[lines[i], columns[i]];
                        rotation = 3;
                    }
                }

                for (int i = 0; i < 4; i++) {
                    current[i].GetComponent<Renderer>().material = currentColor;
                }
            } else if (rotation == 3) {
                //horizontal para em pé 1

                print("ROTATE L2");

                for (int i = 0; i < 4; i++) {
                    current[i].GetComponent<Renderer>().material = gray;
                }

                linesT[0] = lines[0]+1; columnsT[0] = columns[0]-1; // 3
                linesT[1] = lines[1]-2; columnsT[1] = columns[1];   // 2
                linesT[2] = lines[2]; columnsT[2] = columns[2];     // 0
                linesT[3] = lines[3]-1; columnsT[3] = columns[3]+1; // 1

                for (int i = 0; i < 4; i++) {
                    if (linesT[i] > 24 ||
                        columnsT[i] > 9 ||
                        columnsT[i] < 0 ||
                        !tetris[linesT[i], columnsT[i]].GetComponent<Renderer>().material.name.Contains("gray")) {

                        canRotate = false;
                    }
                }

                if (canRotate) {
                    for (int i = 0; i < 4; i++) {
                        lines[i] = linesT[i];
                        columns[i] = columnsT[i];
                        current[i] = tetris[lines[i], columns[i]];
                        rotation = 4;
                    }
                }

                for (int i = 0; i < 4; i++) {
                    current[i].GetComponent<Renderer>().material = currentColor;
                }
            } else if (rotation == 4) {
                //horizontal para em pé 1

                print("ROTATE L2");

                for (int i = 0; i < 4; i++) {
                    current[i].GetComponent<Renderer>().material = gray;
                }

                linesT[0] = lines[0]-1; columnsT[0] = columns[0]-1; // 3
                linesT[1] = lines[1]; columnsT[1] = columns[1]+2;   // 2
                linesT[2] = lines[2]; columnsT[2] = columns[2];     // 0
                linesT[3] = lines[3]+1; columnsT[3] = columns[3]+1; // 1

                for (int i = 0; i < 4; i++) {
                    if (linesT[i] > 24 ||
                        columnsT[i] > 9 ||
                        columnsT[i] < 0 ||
                        !tetris[linesT[i], columnsT[i]].GetComponent<Renderer>().material.name.Contains("gray")) {

                        canRotate = false;
                    }
                }

                if (canRotate) {
                    for (int i = 0; i < 4; i++) {
                        lines[i] = linesT[i];
                        columns[i] = columnsT[i];
                        current[i] = tetris[lines[i], columns[i]];
                        rotation = 1;
                    }
                }

                for (int i = 0; i < 4; i++) {
                    current[i].GetComponent<Renderer>().material = currentColor;
                }
            }
        }//FIM L2


        //S1
        if (pieceName.Equals("s1")) {
            if (rotation == 1) {
                //horizontal para em pé 1

                print("ROTATE S1");

                for (int i = 0; i < 4; i++) {
                    current[i].GetComponent<Renderer>().material = gray;
                }

                linesT[0] = lines[0] - 1; columnsT[0] = columns[0] - 1; //2
                linesT[1] = lines[1] - 2; columnsT[1] = columns[1]; //3
                linesT[2] = lines[2]; columnsT[2] = columns[2]; //0
                linesT[3] = lines[3] + 1; columnsT[3] = columns[3] - 1; //1

                for (int i = 0; i < 4; i++) {
                    if (linesT[i] > 24 ||
                        columnsT[i] > 9 ||
                        columnsT[i] < 0 ||
                        !tetris[linesT[i], columnsT[i]].GetComponent<Renderer>().material.name.Contains("gray")) {

                        canRotate = false;
                    }
                }

                if (canRotate) {
                    for (int i = 0; i < 4; i++) {
                        lines[i] = linesT[i];
                        columns[i] = columnsT[i];
                        current[i] = tetris[lines[i], columns[i]];
                        rotation = 2;
                    }
                }

                for (int i = 0; i < 4; i++) {
                    current[i].GetComponent<Renderer>().material = currentColor;
                }
            } else if (rotation == 2) {
                //horizontal para em pé 1

                print("ROTATE S1 2");

                for (int i = 0; i < 4; i++) {
                    current[i].GetComponent<Renderer>().material = gray;
                }

                linesT[0] = lines[0] + 1; columnsT[0] = columns[0] + 1; //2
                linesT[1] = lines[1] + 2; columnsT[1] = columns[1]; //3
                linesT[2] = lines[2]; columnsT[2] = columns[2]; //0
                linesT[3] = lines[3] - 1; columnsT[3] = columns[3] + 1; //1

                for (int i = 0; i < 4; i++) {
                    if (linesT[i] > 24 ||
                        columnsT[i] > 9 ||
                        columnsT[i] < 0 ||
                        !tetris[linesT[i], columnsT[i]].GetComponent<Renderer>().material.name.Contains("gray")) {

                        canRotate = false;
                    }
                }

                if (canRotate) {
                    for (int i = 0; i < 4; i++) {
                        lines[i] = linesT[i];
                        columns[i] = columnsT[i];
                        current[i] = tetris[lines[i], columns[i]];
                        rotation = 1;
                    }
                }

                for (int i = 0; i < 4; i++) {
                    current[i].GetComponent<Renderer>().material = currentColor;
                }
            }
        }//FIM S1










        //S2
        if (pieceName.Equals("s2")) {
            if (rotation == 1) {
                //horizontal para em pé 1

                print("ROTATE S2");

                for (int i = 0; i < 4; i++) {
                    current[i].GetComponent<Renderer>().material = gray;
                }

                linesT[0] = lines[0]-1; columnsT[0] = columns[0]-1; //2
                linesT[1] = lines[1]; columnsT[1] = columns[1]; //0
                linesT[2] = lines[2]-1; columnsT[2] = columns[2]+1; //1
                linesT[3] = lines[3]; columnsT[3] = columns[3]-2; //3

                for (int i = 0; i < 4; i++) {
                    if (linesT[i] > 24 ||
                        columnsT[i] > 9 ||
                        columnsT[i] < 0 ||
                        !tetris[linesT[i], columnsT[i]].GetComponent<Renderer>().material.name.Contains("gray")) {

                        canRotate = false;
                    }
                }

                if (canRotate) {
                    for (int i = 0; i < 4; i++) {
                        lines[i] = linesT[i];
                        columns[i] = columnsT[i];
                        current[i] = tetris[lines[i], columns[i]];
                        rotation = 2;
                    }
                }

                for (int i = 0; i < 4; i++) {
                    current[i].GetComponent<Renderer>().material = currentColor;
                }
            } else if (rotation == 2) {
                //horizontal para em pé 1

                print("ROTATE S2 2");

                for (int i = 0; i < 4; i++) {
                    current[i].GetComponent<Renderer>().material = gray;
                }

                linesT[0] = lines[0]+1; columnsT[0] = columns[0]+1; //2
                linesT[1] = lines[1]; columnsT[1] = columns[1]; //0
                linesT[2] = lines[2]+1; columnsT[2] = columns[2]-1; //1
                linesT[3] = lines[3]; columnsT[3] = columns[3]+2; //3

                for (int i = 0; i < 4; i++) {
                    if (linesT[i] > 24 ||
                        columnsT[i] > 9 ||
                        columnsT[i] < 0 ||
                        !tetris[linesT[i], columnsT[i]].GetComponent<Renderer>().material.name.Contains("gray")) {

                        canRotate = false;
                    }
                }

                if (canRotate) {
                    for (int i = 0; i < 4; i++) {
                        lines[i] = linesT[i];
                        columns[i] = columnsT[i];
                        current[i] = tetris[lines[i], columns[i]];
                        rotation = 1;
                    }
                }

                for (int i = 0; i < 4; i++) {
                    current[i].GetComponent<Renderer>().material = currentColor;
                }
            }
        }

    }


    public void game() {

        if (current[0] == null) {
            spawnPiece();
        } else {
            if(!isBottom()) { //não bateu no fundo
                if (!isPieceBellow()) {
                    nextLine();
                } else {
                    //print("bateu outra peça");

                    //verifica altura pra ver se perdeu
                    if (verifyLose()) {
                        //volta para o menu
                    } else {
                        deleteCurrent();
                    }

                    
                }
            } else {
                print("chegou fim");
                //deleta current
                deleteCurrent();
            }
        }

        //print("linha");
    }


    //celular
    Vector2 firstPressPos;
    Vector2 secondPressPos;
    Vector2 currentSwipe;

    void Update()
    {
        if (Time.time - previousTime > time) {
            game();
            previousTime = Time.time;
        }

        if (Input.GetKey(KeyCode.LeftArrow)) {
            if (Time.time - lastStep > timeBetweenSteps) {
                moveLeft();
                lastStep = Time.time;
            }
        }
        if (Input.GetKey(KeyCode.RightArrow)) {
            if (Time.time - lastStep > timeBetweenSteps) {
                moveRight();
                lastStep = Time.time;
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow)) {
            time = 0.1f;
        }
        if (Input.GetKeyUp(KeyCode.DownArrow)) {
            time = startTime;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            rotatePiece();
        }

       

    }

    private Vector2 fp; // first finger position
    private Vector2 lp; // last finger position


    void FixedUpdate() {
        foreach (Touch touch in Input.touches) {
            if (touch.phase == TouchPhase.Began) {
                fp = touch.position;
                lp = touch.position;
            }
            if (touch.phase == TouchPhase.Moved) {
                lp = touch.position;
            }
            if (touch.phase == TouchPhase.Ended) {
                if ((fp.x - lp.x) > 80) // left swipe
                {
                    moveLeft();
                } else if ((fp.x - lp.x) < -80) // right swipe
                  {
                    moveRight();
                } else if ((fp.y - lp.y) < -80) // up swipe
                  {
                    rotatePiece();
                } else if ((fp.y - lp.y) > 80) // down swipe
                  {
                    Debug.Log("down swipe here...");
                }
            }
        }
    }

}
