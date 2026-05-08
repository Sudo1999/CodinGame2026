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
        //input.AddRange(new string[] { "40", "Xeljanz", "Trisenox", "Afinitor", "Nityr", "Mylotarg", "Phesgo", "Ampyra", "Odomzo", "Delstrigo", "Enhertu", "Thiola", "Gelnique", "Nardil", "Cardura", "Cortef", "Gleevec", "Daypro", "Evista", "Myobloc", "Treanda", "Lumoxiti", "Bosulif", "Levoxyl", "Piqray", "Ciprodex", "Accupril", "Gilotrif", "Cipro", "Anafranil", "Gardasil", "Caplyta", "Imovax", "BeneFIX", "Fycompa", "Iclusig", "Thymoglobulin", "Pristiq", "Korlym", "Jardiance", "Botox" });   // Jeu de test 3 => Result = 11
        input.AddRange(new string[] { "80", "Uceris", "Pulmozyme", "Crysvita", "Corvert", "Effexor", "Mepsevii", "Copaxone", "Enfamom", "Praluent", "Amitiza", "Zemaira", "Xembify", "Ziagen", "Boostrix", "Hycamtin", "Viramune", "Aptiom", "Sandostatin", "Azulfidine", "Zyvox", "Oseni", "Riabni", "Folotyn", "Qtern", "Cialis", "Herceptin", "Eloctate", "Nivestym", "Inrebic", "Briviact", "Emflaza", "Menest", "Ventolin", "Faslodex", "Saphris", "Fosrenol", "Dupixent", "Erbitux", "Herzuma", "Arranon", "Lantus", "Durysta", "Diflucan", "Hyalgan", "GlucaGen", "Sutent", "Spravato", "Mvasi", "Synagis", "Targretin", "Recarbrio", "Blincyto", "Cotellic", "Opdivo", "Jynarque", "Mekinist", "InFed", "Premphase", "Gabitril", "Padcev", "Zeposia", "Lumoxiti", "Avycaz", "Kuvan", "Breztri", "Katerzia", "Basaglar", "Vitrakvi", "Gavreto", "Protonix", "Vectra", "Synarel", "Verzenio", "Uptravi", "Shingrix", "Akynzeo", "Aromasin", "Prevymis", "Xospata", "Rayos" });        // Jeu de test 4 => Result = 13

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
        ulong bestSetA = 0;         // Présence ou absence des médicaments 0 à 63
        ulong bestSetB = 0;         // Présence ou absence des médicaments 64 à 127

        ulong[] conflictsAA = new ulong[N];         // Conflits des médicaments 0 à 63 avec les médicaments 0 à 63
        ulong[] conflictsAB = new ulong[N];         // Conflits des médicaments 0 à 63 avec les médicaments 64 à 127
        ulong[] conflictsBA = new ulong[N];         // Conflits des médicaments 64 à 127 avec les médicaments 0 à 63
        ulong[] conflictsBB = new ulong[N];         // Conflits des médicaments 64 à 127 avec les médicaments 64 à 127

        // Méthode de comptage des lettres partagées
        static int CountSharedLetters(string a, string b)
        {
            int[] counta = new int[26];
            int[] countb = new int[26];

            foreach (char c in a.ToUpper()) if (char.IsLetter(c)) counta[c - 'A']++;
            foreach (char c in b.ToUpper()) if (char.IsLetter(c)) countb[c - 'A']++;

            int shared = 0;
            for (int i = 0; i < 26; i++)
            {
                shared += Math.Min(counta[i], countb[i]);
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
                    if (i < 64 && j < 64)
                    {
                        conflictsAA[i] |= 1UL << j;
                        conflictsAA[j] |= 1UL << i;
                    }
                    if (i < 64 && j >= 64)
                    {
                        conflictsAB[i] |= 1UL << j;
                        conflictsBA[j] |= 1UL << i;
                    }
                    if (i >= 64 && j >= 64)
                    {
                        conflictsBB[i] |= 1UL << j;
                        conflictsBB[j] |= 1UL << i;
                    }
                }
            }
        }

        // Méthode de comptage du nombre de bits à 1 dans les ulongs de résultat
        static int CountBits(ulong x, ulong y)
        {
            int count = 0;
            while (x != 0)
            {
                count++;
                x &= x - 1;     // La formule enlève le bit à 1 le plus à droite
            }
            while (y != 0)
            {
                count++;
                y &= y - 1;     // La formule enlève le bit à 1 le plus à droite
            }
            return count;
        }

        // Méthode de Backtracking avec Bitmask
        void DFS(ulong currentSetA, ulong currentSetB, int index)		// DFS = Depth-First Search 
        {
            if (index == N)
            {
                int size = CountBits(currentSetA, currentSetB);
                if (size > maxSize)
                {
                    maxSize = size;
                    bestSetA = currentSetA;
                    bestSetB = currentSetB;
                }
                return;
            }

            // Option 1 : ignorer le médicament drugs[index]
            DFS(currentSetA, currentSetB, index + 1);

            // Option 2 : ajouter le médicament s'il n'y a pas de conflit avec le set déjà constitué

            //if ((currentSet & conflicts[index]) == 0)
            //{
                //DFS(currentSet | (1UL << index), index + 1);
            //}

            if (index < 64)
            {
                if ((currentSetA & conflictsAA[index]) == 0 && (currentSetB & conflictsAB[index]) == 0)
                {
                    DFS(currentSetA | (1UL << index), currentSetB, index + 1);
                }
            }
            else
            {
                if ((currentSetA & conflictsBA[index]) == 0 && (currentSetB & conflictsBB[index]) == 0)
                {
                    DFS(currentSetA, currentSetB | (1UL << index-64), index + 1);
                }
            }
        }

        DFS(0, 0, 0);

        // Affichage du résultat
        Console.WriteLine($"Nombre maximum de médicaments pouvant être utilisés ensemble : {maxSize}.");
        for (int i = 0; i < N; i++)
        {
            if (i < 64 && (bestSetA & (1UL << i)) != 0)
            {
                Console.WriteLine(drugs[i]);
            }
            else if ((bestSetB & (1UL << i)) != 0)
            {
                Console.WriteLine(drugs[i]);
            }
        }

        //Console.WriteLine(maxSize);
    }
}
