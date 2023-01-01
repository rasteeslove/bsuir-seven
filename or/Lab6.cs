namespace Math.Current;

public static class Lab6
{
    public static (decimal Cost, int[] Targets) FindBestPlan(Matrix costs)
    {
        if (costs.Rows != costs.Columns)
            throw new ArgumentException("Кол-во производителей отличается от кол-ва потребителей");

        var N = costs.Rows;

        var alphas = Enumerable.Range(0, N).Select(i => 0m).ToList();
        var betas = Enumerable.Range(0, N).Select(i => costs.GetColumn(i).Min()).ToList();

        while (true)
        {
            var J = new List<(int, int)>();
            for (int i = 0; i < N; i++)
                for (int j = 0; j < N; j++)
                    if (alphas[i] + betas[j] == costs[i][j])
                        J.Add((i, j + N));

            var matching = Lab5.FindMaxMatching(J);

            if (matching.Count == N)
            {
                var targets = matching.OrderBy(pair => pair.Item1).Select(pair => pair.Item2 - N).ToList();
                var cost = Enumerable.Range(0, N).Sum(i => costs[i][targets[i]]);
                return (cost, targets.Select(i => i + 1).ToArray());
            }

            var edges = new List<(int, int)>();
            edges.AddRange(matching.Select(pair => (pair.Item2, pair.Item1)));
            edges.AddRange(J.Except(matching).Select(pair => (pair.Item1, pair.Item2)));

            var starts = Enumerable.Range(0, N).Except(matching.Select(pair => pair.Item1)).ToList();

            var used = new bool[N + N];

            void DFS(int vertex)
            {
                if (used[vertex]) return;
                used[vertex] = true;

                foreach (var edge in edges.Where(pair => pair.Item1 == vertex))
                    DFS(edge.Item2);
            }

            starts.ForEach(vertex => DFS(vertex));

            var theta = decimal.MaxValue;
            for (int i = 0; i < N; i++)
                for (int j = 0; j < N; j++)
                    if (used[i] && !used[j + N])
                        theta = System.Math.Min(theta, (costs[i][j] - (alphas[i] + betas[j])) / 2);

            for (int i = 0; i < N; i++)
                alphas[i] += theta * (used[i] ? 1 : -1);
            for (int j = 0; j < N; j++)
                betas[j] += theta * (used[j + N] ? -1 : 1);
        }
    }
}
