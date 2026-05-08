using System;

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
        
        //int N = int.Parse(Console.ReadLine());      // Line 1: Number of drugs
        int N = int.Parse(input[0] ?? "0");                 // Lecture de l'input (ligne 1)
        string[] drugs = new string[N];

        for (int i = 0; i < N; i++)     // Next N Lines: List of drugs (N ≤ 100)
        {
            //drugs[i] = Console.ReadLine().ToUpper();
            drugs[i] = input[i + 1].ToUpper() ?? "";       // Lecture de l'input (noms des produits)
        }

        int maxSize = 0;
        ulong bestSet = 0;

        ulong[] conflicts = new ulong[N];

        // Chaque médicament correspond à un bit ; un ulong = 64 bits => 64 médicaments
        // Par exemple si on a 8 médicaments :
        // conflicts[0] = 0b00000000 // M0 n’a aucun conflit avec un autre médicament
        // conflicts[1] = 0b00000100 // M1 a un conflit avec le médicament M2
        // conflicts[2] = 0b00000010 // M2 a un conflit avec le médicament M1

        // Méthode de comptage des lettres partagées
        static int CountSharedLetters(string a, string b)
        {
            int[] countA = new int[26];
            int[] countB = new int[26];

            foreach (char c in a.ToUpper()) if (char.IsLetter(c)) countA[c - 'A']++;
            foreach (char c in b.ToUpper()) if (char.IsLetter(c)) countB[c - 'A']++;

            int shared = 0;
            for (int i = 0; i < 26; i++)
            {
                shared += Math.Min(countA[i], countB[i]);
            }
            return shared;
        }

        // Construire le masque de conflits
        for (int i = 0; i < N; i++)
        {
            for (int j = i + 1; j < N; j++)
            {
                if (CountSharedLetters(drugs[i], drugs[j]) >= 3)
                {
                    conflicts[i] |= 1UL << j;
                    conflicts[j] |= 1UL << i;

                    // 1UL = un unsigned long (64 bits non signé), soit une ligne de 64 zéros
                    // << j = décalage à gauche de j positions (par exemple 1UL << 3 = 0000 1000)
                    // conflicts[i] |= 1UL << j => Le médicament Mi enregistre un conflit à l'emplacement du médicament Mj
                }
            }
        }

        // Méthode de comptage du nombre de bits à 1 dans un ulong
        static int CountBits(ulong x)
        {
            int count = 0;
            while (x != 0)
            {
                count++;
                x &= x - 1;     // La formule enlève le bit à 1 le plus à droite
                // (Contrairement au OU (|), avec le ET (&) le 1 n’est conservé que s’il est présent des deux côtés)
            }
            return count;
        }

        // Méthode de Backtracking avec Bitmask
        void DFS(ulong currentSet, int index)		// DFS = Depth-First Search 
        {
            if (index == N)
            {
                int size = CountBits(currentSet);
                if (size > maxSize)
                {
                    maxSize = size;
                    bestSet = currentSet;
                }
                return;
            }

            // Option 1 : ignorer le médicament drugs[index]
            DFS(currentSet, index + 1);

            // Option 2 : ajouter le médicament s'il n'y a pas de conflit avec le set déjà constitué
            if ((currentSet & conflicts[index]) == 0)       // Pruning : on ne l’ajoute que s'il n'y a pas de conflit
            {
                DFS(currentSet | (1UL << index), index + 1);
            }
        }

        DFS(0, 0);

        // Affichage du résultat
        Console.WriteLine($"Nombre maximum de médicaments pouvant être utilisés ensemble : {maxSize}.");
        for (int i = 0; i < N; i++)
        {
            if ((bestSet & (1UL << i)) != 0)
            {
                Console.WriteLine(drugs[i]);
            }
        }

        //Console.WriteLine(maxSize);
    }
}
