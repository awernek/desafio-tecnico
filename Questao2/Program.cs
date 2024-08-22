using Newtonsoft.Json.Linq;

public static class Program
{
    public static void Main()
    {
        string teamName = "Paris Saint-Germain";
        int year = 2013;
        int totalGoals = getTotalScoredGoals(teamName, year);

        Console.WriteLine("Team " + teamName + " scored " + totalGoals.ToString() + " goals in " + year);

        teamName = "Chelsea";
        year = 2014;
        totalGoals = getTotalScoredGoals(teamName, year);

        Console.WriteLine("Team " + teamName + " scored " + totalGoals.ToString() + " goals in " + year);

        // Output expected:
        // Team Paris Saint - Germain scored 109 goals in 2013
        // Team Chelsea scored 92 goals in 2014
    }

    public static int getTotalScoredGoals(string team, int year)
    {
        int totalGoals = 0;
        int page = 1;
        string apiUrl = $"https://jsonmock.hackerrank.com/api/football_matches?year={year}&team1={team}&page={page}";

        using (HttpClient client = new HttpClient())
        {
            while (true)
            {
                HttpResponseMessage response = client.GetAsync(apiUrl).Result;
                string jsonData = response.Content.ReadAsStringAsync().Result;
                JObject jsonObject = JObject.Parse(jsonData);

                var matches = jsonObject["data"];

                foreach (var match in matches)
                {
                    totalGoals += int.Parse(match["team1goals"].ToString());
                }

                int totalPages = int.Parse(jsonObject["total_pages"].ToString());
                if (page >= totalPages)
                    break;

                page++;
                apiUrl = $"https://jsonmock.hackerrank.com/api/football_matches?year={year}&team1={team}&page={page}";
            }
        }

        return totalGoals;
    }
}