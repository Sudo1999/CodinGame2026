using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

// https://www.codingame.com/training/medium/drugtocompare-interactions
/* You are given a list of prescription drugs. Any two drugs with three or more letters in common 
 * (ignoring case) will have a bad interaction and should not be used together. 
 * Find the largest number of drugs in the list that can be used together.
 * Note that the same letter can match more than once, e.g. "Xanax" and "Viagra" each have two A's 
 * and thus have two matching letters.
 */

/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/
class Solution
{
    static bool compare(string drugtocompare, string drugindrugs)
    {
        int common = 0;
        bool ok = true;
        foreach (char cj in drugindrugs)
        {
            foreach (char ci in drugtocompare)
            {
                if (ci == cj)
                {
                    int indexcj = drugindrugs.IndexOf(cj);
                    int indexci = drugtocompare.IndexOf(ci);
                    drugindrugs = drugindrugs.Remove(indexcj, 1);
                    drugtocompare = drugtocompare.Remove(indexci, 1);
                    common++;
                    break;
                }
            }
            if (common > 2)
            {
                ok = false;
                break;
            }
        }
        return ok;
    }

    static List<string> checklist(List<string> drugslist)
    {
        List<string> bestlist = new List<string>(drugslist);
        List<string> workinglist = new List<string>();

        for (int i = 0; i < drugslist.Count; i++)
        {
            for (int j = i + 1; j < drugslist.Count; j++)
            {
                bool twook = compare(drugslist[i], drugslist[j]);
                if (!twook)
                {
                    workinglist = new List<string>(drugslist);
                    workinglist.RemoveAt(i);
                    List<string> bestlistmoinsi = checklist(workinglist);

                    workinglist = new List<string>(drugslist);
                    workinglist.RemoveAt(j);
                    List<string> bestlistmoinsj = checklist(workinglist);

                    bestlist = bestlistmoinsi.Count > bestlistmoinsj.Count ? bestlistmoinsi : bestlistmoinsj;
                }
            }
        }
        return bestlist;
    }

    static void Main(string[] args)
    {
        //// Input

        List<string> input = new List<string>();
        //input.AddRange(new string[] { "3", "Xanax", "Ativan", "Viagra" });      // Jeu de test 1 => Result = 2
        input.AddRange(new string[] { "10", "Zubsolv", "Juluca", "Invega", "Herceptin", "Cinryze", "Sustol", "Zonegran", "Tecartus", "Alfamino", "Depakote" });          // Jeu de test 2 => Result = 5

        //// Résolution

        int answer = 0;
        //int N = int.Parse(Console.ReadLine());      // Line 1: Number of drugs
        int N = int.Parse(input[0] ?? "0");                 // Lecture de l'input (ligne 1)
        string[] drugs = new string[N];

        for (int i = 0; i < N; i++)     // Next N Lines: List of drugs (N ≤ 100)
        {
            //drugs[i] = Console.ReadLine().ToUpper();
            drugs[i] = input[i + 1].ToUpper() ?? "";       // Lecture de l'input (noms des produits)

            HashSet<string> compatibles = new HashSet<string>();
            for (int j = 0; j < i; j++)
            {
                int common = 0;
                string drug = drugs[i];
                string drugindrugs = drugs[j];

                bool ok = compare(drug, drugindrugs);
                if (ok)
                {
                    compatibles.Add(drugs[i]);
                    compatibles.Add(drugs[j]);
                }
            }

            List<string> drugslist = new List<string>(compatibles);
            drugslist = checklist(drugslist);

            if (answer < drugslist.Count)
            {
                answer = drugslist.Count;
            }
        }

        // Write an answer using Console.WriteLine()
        // To debug: Console.Error.WriteLine("Debug messages...");

        //Console.WriteLine(answer);
        Console.WriteLine("\nLe résultat est " + answer + ".");         // The most drugs that can be used together.
    }
}
