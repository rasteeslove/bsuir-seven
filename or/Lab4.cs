using Accord.Math;

namespace Math.Current;

public static class Lab4
{
    public class Edge
    {
        public int From { get; set; }
        public int To { get; set; }
        public decimal MaxFlow { get; set; }
    }

    public static Dictionary<(int From, int To), decimal> FindMaxFlow(int N, IEnumerable<Edge> edges, int start, int finish)
    {
        var subEdges = new List<Edge>(edges.Select(edge => new Edge() { From = edge.From, To = edge.To, MaxFlow = edge.MaxFlow }));
        foreach (var edge in edges)
            if (!edges.Any(other => other.From == edge.To && other.To == edge.From))
                subEdges.Add(new() { From = edge.To, To = edge.From, MaxFlow = 0 });

        var flows = subEdges.ToDictionary(edge => (edge.From, edge.To), _ => 0m);

        while (true)
        {
            if (!FindPath(N, subEdges.Where(edge => edge.MaxFlow != 0), start, finish, out var path))
                break;

            var theta = path.Min(step => subEdges.Single(edge => edge.From == step.From && edge.To == step.To).MaxFlow);
            foreach (var step in path)
            {
                flows[(step.From, step.To)] += theta;
                subEdges.Single(edge => edge.From == step.From && edge.To == step.To).MaxFlow -= theta;
                subEdges.Single(edge => edge.From == step.To && edge.To == step.From).MaxFlow += theta;
            }
        }

        foreach (var edge in edges)
        {
            var min = System.Math.Min(flows[(edge.From, edge.To)], flows[(edge.To, edge.From)]);
            flows[(edge.From, edge.To)] -= min;
            flows[(edge.To, edge.From)] -= min;
        }

        return flows.Where(flow => flow.Value != 0).ToDictionary(flow => flow.Key, flow => flow.Value);
    }

    private static bool FindPath(int N, IEnumerable<Edge> edges, int start, int finish, out List<(int From, int To)> path)
    {
        var prevs = Enumerable.Range(1, N).ToDictionary(x => x, _ => 0);

        void DFS(int vertex)
        {
            foreach (var edge in edges.Where(edge => edge.From == vertex))
            {
                if (prevs[edge.To] == 0)
                {
                    prevs[edge.To] = vertex;
                    DFS(edge.To);
                }
            }
        }

        prevs[start] = start;
        DFS(start);

        path = new();
        var vertex = finish;
        while (vertex != start && vertex != 0)
        {
            path.Add((prevs[vertex], vertex));
            vertex = prevs[vertex];
        }

        path.Reverse();

        return prevs[finish] != 0;
    }
}
