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
    static void Main(string[] args)
    {
        //// Input

        List<string> input = new List<string>();
        //input.AddRange(new string[] { "3", "Xanax", "Ativan", "Viagra" });      // Jeu de test 1 => Result = 2
        //input.AddRange(new string[] { "10", "Zubsolv", "Juluca", "Invega", "Herceptin", "Cinryze", "Sustol", "Zonegran", "Tecartus", "Alfamino", "Depakote" });          // Jeu de test 2 => Result = 5
        input.AddRange(new string[] { "40", "Xeljanz", "Trisenox", "Afinitor", "Nityr", "Mylotarg", "Phesgo", "Ampyra", "Odomzo", "Delstrigo", "Enhertu", "Thiola", "Gelnique", "Nardil", "Cardura", "Cortef", "Gleevec", "Daypro", "Evista", "Myobloc", "Treanda", "Lumoxiti", "Bosulif", "Levoxyl", "Piqray", "Ciprodex", "Accupril", "Gilotrif", "Cipro", "Anafranil", "Gardasil", "Caplyta", "Imovax", "BeneFIX", "Fycompa", "Iclusig", "Thymoglobulin", "Pristiq", "Korlym", "Jardiance", "Botox" });   // Jeu de test 3 => Result = 11

        //// Résolution
        
        int answer = 0;
        //int N = int.Parse(Console.ReadLine());      // Line 1: Number of drugs
        int N = int.Parse(input[0] ?? "0");                 // Lecture de l'input (ligne 1)
        string[] drugs = new string[N];

        for (int i = 0; i < N; i++)     // Next N Lines: List of drugs (N ≤ 100)
        {
            //drugs[i] = Console.ReadLine().ToUpper();
            drugs[i] = input[i + 1].ToUpper() ?? "";       // Lecture de l'input (noms des produits)
        }

        //Console.WriteLine(answer);
        Console.WriteLine("\nLe résultat est " + answer + ".");         // The most drugs that can be used together.
    }
}
