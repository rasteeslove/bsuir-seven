namespace Math.Current;

public static class Lab3
{
    public class Edge
    {
        public int From { get; set; }
        public int To { get; set; }
        public decimal Cost { get; set; }
    }

    public static (decimal MaxLength, List<int> Path) GetLongestPath(int N, IEnumerable<Edge> edges, int start, int finish)
    {
        var SortedVertexes = TopologicalSort(N, edges, start);

        if (!SortedVertexes.Contains(finish))
            throw new ArgumentException($"Не существует пути из {start} в {finish}");

        var MaxPossibleLengths = Enumerable.Repeat(decimal.MinValue, N + 1).ToList();
        MaxPossibleLengths[start] = 0;

        var PreviousVertex = Enumerable.Repeat(0, N + 1).ToList();

        foreach (var vertex in SortedVertexes)
        {
            var incomings = edges.Where(edge => edge.To == vertex && MaxPossibleLengths[edge.From] != decimal.MinValue).ToList();

            if (incomings.Any())
            {
                var best = incomings.MaxBy(edge => edge.Cost + MaxPossibleLengths[edge.From]);
                PreviousVertex[vertex] = best.From;
                MaxPossibleLengths[vertex] = best.Cost + MaxPossibleLengths[best.From];
            }
        }

        List<int> RestorePath()
        {
            var path = new List<int>() as IList<int>;

            var vertex = finish;
            while (vertex != start)
            {
                path.Add(vertex);
                vertex = PreviousVertex[vertex];
            }
            path.Add(vertex);

            return path.Reverse().ToList();
        }

        return (MaxPossibleLengths[finish], RestorePath());
    }

    private static List<int> TopologicalSort(int N, IEnumerable<Edge> edges, int start)
    {
        var CompletedVertexes = new List<int>() as IList<int>;
        var Colors = Enumerable.Repeat(Color.White, N + 1).ToArray();

        void DFS(int vertex)
        {
            if (Colors[vertex] == Color.Gray)
                throw new ArgumentException("Граф содержит цикл");

            Colors[vertex] = Color.Gray;

            foreach (var edge in edges.Where(edge => edge.From == vertex && Colors[edge.To] != Color.Black))
                DFS(edge.To);

            CompletedVertexes.Add(vertex);
            Colors[vertex] = Color.Black;
        }

        DFS(start);

        return CompletedVertexes.Reverse().ToList();
    }

    private enum Color
    {
        White,
        Gray,
        Black,
    }
}
