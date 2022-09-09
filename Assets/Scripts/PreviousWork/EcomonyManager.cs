using System;                   // Mo?na usun??.
using System.Collections;
using System.Collections.Generic;
using TKOU.SimAI;
using TMPro;
using UnityEngine;

public class EcomonyManager : MonoBehaviour     // Skrypt powinien nazywa? si? EconomyManager
{
    [Header("Zmienne")] 
    private float cash;
    public TMP_Text cashText;               // SerializeField tutaj mozna uzyc, zeby nie bylo dostepne przez inne skrypt, a tylko przez inspector.
    private Income income;
    [SerializeField] private Income outcome; // Co niczego to nie s?u?y - usun??. A poza tym nazewnictwo - Income, outcome? 
    [SerializeField]                        // SerializeField powinien byc na obu tych polach,
    private bool isCalc, canCalc;           // wi?c canCalc nie bedzie widoczny w inspektorze
    public int IntCash;                     // Po pierwsze to nic nie robi, po drugie cash jest floatem, po trzecie, je?li ju? to uzy? gettera. A jak potrzebujemy int to raczej bym zrobi? oddzielne pole na int i ustawia? w tym samym czasie kiedy cash sie zmienia.

    private List<Income> incomes;
    private PlayerController plCtr;
    
    
    
    private void Awake()
    {
        income = (Income)GetComponent(typeof(Income));      // Nie potrzebnie to jest castowane, zamiast tego: income = GetComponent<Income>();
        cashText.text = income.Cash.ToString();             // Mo?na u?y? String buildera, lub chocia? cashText.text = $"{income.Cash}"

        plCtr = FindObjectOfType<PlayerController>();       // Referencje mo?a w inne sposoby pozyska?. Lepiej unika? FindObjectOfType
    }

    // Start is called before the first frame update 
    IEnumerator Start() 
    {
        Debug.Log("Start");             // Po sko?czonej pracy - nie zamula? konsoli debug logiem. ;)
        StartCoroutine(LateStart()); 
        yield return 0;             // To alokuj? pami??. U?y? yield return null!
    }

    // Update is called once per frame
    public void Update() 
    {
        if(!isCalc) return;
        cashText.text = income.Cash.ToString();             // U?y? string buildera, kosztowne na update!
        if (income.Cash > 100)                              // 
        {                                                   // Co klatk? kolor jest ustawiany. To bardzo z?a praktyka, wp?ywa na performance i jest error prone.
            cashText.color = Color.black;                   // Mo?na to rozwi?za? na dwa sposoby: da? warunek, czy ju? kolor zosta? zmieniony, u?ywa? eventów.
        }
        else
        {
            cashText.color = new Color(1, 1, 1); 
        }
        InvokeRepeating("DoCalculations", 1, 1);            // Po prostu tego si? trzeba pozby? ;D Na update ta metoda zamuli projekt, Nie mo?e by? na Update oczywi?cie, Mo?na na Start() wywo?a?
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Untagged")         // U?y? CompareTag(), bo == s? wolne w tym wypadku
        {
            collision.gameObject.SetActive(false);
            
        }
    }

    public void AddIncome(int aVal, int bVal, int cVal, float f, bool fast) // Parametry s? nie czytelne, nie wiadomo co one oznaczaj? na pierwszy rzut oka. Poprawi? naming convention.
    {
        if (fast)
        {
            cash -= aVal + bVal + cVal;         // += powinno by?, dodajemy, czy odejmujemy?
        }
        else
        {
            cash -= ((aVal * f) + (bVal * f) + (cVal*f) * f / 3);   // lepiej ju? (aVal+bVal+cVal)*f * (f/3)) (chyba pomylone by?o?) ,te? kolejno?? wykonywania dzialan wplywa na performance
        }                                         
        incomes.Add(new Income(cash));          // incomes.Add(cash);   

    }
    
    void DoCalculations() 
    {
        
    }

    IEnumerator LateStart()
    {
        yield return new WaitForSeconds(1); 
        StartCoroutine(Start());                // Bez sensu - Startu nie chcemu znów wywo?ywa?!
    }

    public void OnDisable()                 // Uwa?a? z tym - a co je?li ponownie w??czymy objekt? Stracimy wszystkie warto?ci income!
    {
        foreach (var i in incomes)          // u?y? while, albo po prostu Clear.
        {
            incomes.Remove(i);   
        }
        
        incomes = null;
    }
}
