Console.WriteLine(IsIsomorphic("badc", "baba"));
Console.WriteLine(IsIsomorphic("foo", "bar"));
Console.WriteLine(IsIsomorphic("egg", "add"));
Console.WriteLine(IsIsomorphic("paper", "title"));

bool IsIsomorphic(string s, string t)
{
    if (s.Length != t.Length) return false;

    Dictionary<char, char> mapS = new Dictionary<char, char>();
    Dictionary<char, char> mapT = new Dictionary<char, char>();

    for (int i = 0; i < s.Length; i++)
    {
        if (mapS.TryGetValue(s[i], out char val))
        {
            if (!val.Equals(t[i])) return false;
        }
        else
        {
            mapS.Add(s[i], t[i]);
        }

        if (mapT.TryGetValue(t[i], out char c))
        {
            if (!c.Equals(s[i])) return false;
        }
        else
        {
            mapT.Add(t[i], s[i]);
        }
    }

    return true;

}