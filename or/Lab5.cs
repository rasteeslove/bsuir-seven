namespace Math.Current;

public static class Lab5
{
    public static List<(int, int)> FindMaxMatching(IEnumerable<(int Left, int Right)> edges)
    {
        const int Start = 1_000_000, Finish = 2_000_000;

        edges = edges.Select(pair => (pair.Item1 + 1, pair.Item2 + 1)).ToList();

        if (edges.Max(edge => System.Math.Max(edge.Left, edge.Right)) >= System.Math.Min(Start, Finish))
            throw new ArgumentException("Не много цифр тебе, дружок-пирожок?");

        if (edges.Select(edge => edge.Left).Intersect(edges.Select(edge => edge.Right)).Any())
            throw new ArgumentException("Граф не двудольный");

        var flowEdges = edges.Select(edge => new Lab4.Edge { From = edge.Left, To = edge.Right, MaxFlow = 1 }).ToList();
        flowEdges.AddRange(edges.Select(edge => edge.Left).Distinct().Select(vertex => new Lab4.Edge { From = Start, To = vertex, MaxFlow = 1 }));
        flowEdges.AddRange(edges.Select(edge => edge.Right).Distinct().Select(vertex => new Lab4.Edge { From = vertex, To = Finish, MaxFlow = 1 }));

        var N = System.Math.Max(System.Math.Max(Start, Finish), flowEdges.Max(edge => System.Math.Max(edge.From, edge.To)));
        var maxFlow = Lab4.FindMaxFlow(N, flowEdges, Start, Finish);
        var result = maxFlow.Select(edge => edge.Key).Where(edge => edge.From != Start && edge.To != Finish).ToList();
        return result.Select(pair => (pair.Item1 - 1, pair.Item2 - 1)).ToList();
    }
}
