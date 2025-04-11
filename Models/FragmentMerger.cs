namespace FragmentMerger.Models
{
    public static class FragmentMerger
    {
        public static string MergeFragments(string[] Fragments)
        {
            var parts = new List<string>(Fragments.Where(s => !string.IsNullOrWhiteSpace(s)).Select(s => s.Trim()));

            while (parts.Count > 1)
            {
                int maxOverlap = -1;
                int iIndex = 0, jIndex = 0;
                string merged = "";

                for (int i = 0; i < parts.Count; i++)
                {
                    for (int j = 0; j < parts.Count; j++)
                    {
                        if (i == j) continue;

                        string a = parts[i], b = parts[j];
                        int overlap = GetOverlapLength(a, b, out string combined);

                        if (overlap > maxOverlap)
                        {
                            maxOverlap = overlap;
                            merged = combined;
                            iIndex = i;
                            jIndex = j;
                        }
                    }
                }

                parts[iIndex] = merged;
                parts.RemoveAt(jIndex > iIndex ? jIndex : jIndex - 1);
            }

            return parts.FirstOrDefault() ?? "";
        }

        private static int GetOverlapLength(string str1, string str2, out string MergedString)
        {
            int max = 0;
            MergedString = str1 + str2;

            if (str1.Contains(str2))
            {
                MergedString = str1;
                return str2.Length; 
            }
            if (str2.Contains(str1))
            {
                MergedString = str2;
                return str1.Length; 
            }


            for (int i = 1; i <= Math.Min(str1.Length, str2.Length); i++)
            {
                if (str1.EndsWith(str2.Substring(0, i)))
                {
                    if (i > max)
                    {
                        max = i;
                        MergedString = str1 + str2.Substring(i);
                    }
                }
            }

            for (int i = 1; i <= Math.Min(str1.Length, str2.Length); i++)
            {
                if (str2.EndsWith(str1.Substring(0, i)))
                {
                    if (i > max)
                    {
                        max = i;
                        MergedString = str2 + str1.Substring(i);
                    }
                }
            }

            return max;
        }
    }
}
