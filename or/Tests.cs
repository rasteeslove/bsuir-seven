namespace Math.Current;

public static class Tests
{
    public static void RunAllTests()
    {
        TestLab1();
        TestLab2();
        TestLab3();
        TestLab4();
        TestLab5();
        TestLab6();
    }

    private static void TestLab1()
    {
        decimal[] c_чёрточка;
        Matrix A_чёрточка;
        decimal[] b_чёрточка;
        decimal[] d_minus_чёрточка;
        decimal[] d_plus_чёрточка;

        {
            c_чёрточка = new[] { 1, 1 }.Select(x => (decimal)x).ToArray();

            A_чёрточка = new Matrix(new[]
            {
                new[] { 5, 9 },
                new[] { 9, 5 },
            });

            b_чёрточка = new[] { 63, 63 }.Select(x => (decimal)x).ToArray();
            d_minus_чёрточка = new[] { 1, 1 }.Select(x => (decimal)x).ToArray();
            d_plus_чёрточка = new[] { 6, 6 }.Select(x => (decimal)x).ToArray();

            var result = Lab1.FindOptimalIntegerPlan(c_чёрточка, A_чёрточка, b_чёрточка, d_minus_чёрточка, d_plus_чёрточка);

            var answer = new Matrix(new[] { 4, 4 });

            if ((new Matrix(result) - answer).ContainsNonZeroEPS())
                throw new InvalidProgramException("Тест не прошёл");
        }

        {
            c_чёрточка = new[] { 2, 3 }.Select(x => (decimal)x).ToArray();

            A_чёрточка = new Matrix(new[]
            {
                new[] { 3, 4 },
                new[] { 2, 5 },
            });

            b_чёрточка = new[] { 24, 22 }.Select(x => (decimal)x).ToArray();
            d_minus_чёрточка = new[] { 0, 0 }.Select(x => (decimal)x).ToArray();
            d_plus_чёрточка = new[] { 8, 4 }.Select(x => (decimal)x).ToArray();

            var result = Lab1.FindOptimalIntegerPlan(c_чёрточка, A_чёрточка, b_чёрточка, d_minus_чёрточка, d_plus_чёрточка);

            var answer = new Matrix(new[] { 5, 2 });

            if ((new Matrix(result) - answer).ContainsNonZeroEPS())
                throw new InvalidProgramException("Тест не прошёл");
        }

        {
            c_чёрточка = new[] { 4, 1 }.Select(x => (decimal)x).ToArray();

            A_чёрточка = new Matrix(new[]
            {
                new[] { 1, 2 },
                new[] { 3, 2 },
            });

            b_чёрточка = new[] { 4, 12 }.Select(x => (decimal)x).ToArray();
            d_minus_чёрточка = new[] { 0, 0 }.Select(x => (decimal)x).ToArray();
            d_plus_чёрточка = new[] { 3, 3 }.Select(x => (decimal)x).ToArray();

            var result = Lab1.FindOptimalIntegerPlan(c_чёрточка, A_чёрточка, b_чёрточка, d_minus_чёрточка, d_plus_чёрточка);

            var answer = new Matrix(new[] { 3, 0 });

            if ((new Matrix(result) - answer).ContainsNonZeroEPS())
                throw new InvalidProgramException("Тест не прошёл");
        }
    }

    private static void TestLab2()
    {
        Matrix A;
        decimal[] b;
        decimal[] c;

        {
            A = new Matrix(new[]
            {
                new[] { 3, 2, 1, 0 },
                new[] { -3, 2, 0, 1 },
            });

            b = new[] { 6, 0 }.Select(x => (decimal)x).ToArray();
            c = new[] { 0, 1, 0, 0 }.Select(x => (decimal)x).ToArray();

            var result = Lab2.FindIntegerPlanConstraint(A, b, c);

            var answer = (new Matrix(new[] { 0, 0, 0.25m, 0.25m, -1 }), 0.5m);

            if (!result.HasValue ||
                !(new Matrix(result.Value.Constraint) - answer.Item1).ContainsOnlyZeroEPS() ||
                !(result.Value.FreeMemberValue - answer.Item2).EqualsZeroEPS())
                throw new InvalidProgramException("Тест не прошёл");
        }

        {
            A = new Matrix(new[]
            {
                new[] { -1,  1,  1,  0,  0 },
                new[] { 1,  0,  0,  1,  0 },
                new[] { 0,  1,  0,  0,  1 },
            });

            b = new[] { 1, 3, 2 }.Select(x => (decimal)x).ToArray();
            c = new[] { 1, 1, 0, 0, 0 }.Select(x => (decimal)x).ToArray();

            var result = Lab2.FindIntegerPlanConstraint(A, b, c);

            if (result.HasValue)
                throw new InvalidProgramException("Тест не прошёл");
        }
    }

    private static void TestLab3()
    {
        int N;
        List<Lab3.Edge> edges;
        (decimal MaxLength, List<int> Path) result;

        {
            N = 8;
            edges = new()
            {
                new() { From = 1, To = 7, Cost = 2 },
                new() { From = 2, To = 7, Cost = 8 },
                new() { From = 2, To = 5, Cost = 1 },
                new() { From = 2, To = 3, Cost = -4 },
                new() { From = 4, To = 1, Cost = 3 },
                new() { From = 4, To = 5, Cost = 5 },
                new() { From = 6, To = 2, Cost = 2 },
                new() { From = 8, To = 6, Cost = -4 },
                new() { From = 8, To = 2, Cost = -1 },
                new() { From = 8, To = 1, Cost = 6 },
                new() { From = 8, To = 4, Cost = 4 },
            };

            result = Lab3.GetLongestPath(N, edges, 8, 1);
            if ((result.MaxLength != 7) || string.Join(",", result.Path) != "8,4,1")
                throw new InvalidProgramException("Тест не прошёл");

            result = Lab3.GetLongestPath(N, edges, 8, 2);
            if ((result.MaxLength != -1) || string.Join(",", result.Path) != "8,2")
                throw new InvalidProgramException("Тест не прошёл");

            result = Lab3.GetLongestPath(N, edges, 8, 3);
            if ((result.MaxLength != -5) || string.Join(",", result.Path) != "8,2,3")
                throw new InvalidProgramException("Тест не прошёл");

            result = Lab3.GetLongestPath(N, edges, 8, 4);
            if ((result.MaxLength != 4) || string.Join(",", result.Path) != "8,4")
                throw new InvalidProgramException("Тест не прошёл");

            result = Lab3.GetLongestPath(N, edges, 8, 5);
            if ((result.MaxLength != 9) || string.Join(",", result.Path) != "8,4,5")
                throw new InvalidProgramException("Тест не прошёл");

            result = Lab3.GetLongestPath(N, edges, 8, 6);
            if ((result.MaxLength != -4) || string.Join(",", result.Path) != "8,6")
                throw new InvalidProgramException("Тест не прошёл");

            result = Lab3.GetLongestPath(N, edges, 8, 7);
            if ((result.MaxLength != 9) || string.Join(",", result.Path) != "8,4,1,7")
                throw new InvalidProgramException("Тест не прошёл");

            result = Lab3.GetLongestPath(N, edges, 8, 8);
            if ((result.MaxLength != 0) || string.Join(",", result.Path) != "8")
                throw new InvalidProgramException("Тест не прошёл");
        }

        {
            N = 6;
            edges = new()
            {
                new() { From = 1, To = 2, Cost = 5 },
                new() { From = 1, To = 3, Cost = 3 },
                new() { From = 2, To = 3, Cost = 2 },
                new() { From = 2, To = 4, Cost = 6 },
                new() { From = 3, To = 4, Cost = 7 },
                new() { From = 3, To = 5, Cost = 4 },
                new() { From = 3, To = 6, Cost = 2 },
                new() { From = 4, To = 5, Cost = -1 },
                new() { From = 4, To = 6, Cost = 1 },
                new() { From = 5, To = 6, Cost = -2 },
            };

            result = Lab3.GetLongestPath(N, edges, 2, 2);
            if ((result.MaxLength != 0) || string.Join(",", result.Path) != "2")
                throw new InvalidProgramException("Тест не прошёл");

            result = Lab3.GetLongestPath(N, edges, 2, 3);
            if ((result.MaxLength != 2) || string.Join(",", result.Path) != "2,3")
                throw new InvalidProgramException("Тест не прошёл");

            result = Lab3.GetLongestPath(N, edges, 2, 4);
            if ((result.MaxLength != 9) || string.Join(",", result.Path) != "2,3,4")
                throw new InvalidProgramException("Тест не прошёл");

            result = Lab3.GetLongestPath(N, edges, 2, 5);
            if ((result.MaxLength != 8) || string.Join(",", result.Path) != "2,3,4,5")
                throw new InvalidProgramException("Тест не прошёл");

            result = Lab3.GetLongestPath(N, edges, 2, 6);
            if ((result.MaxLength != 10) || string.Join(",", result.Path) != "2,3,4,6")
                throw new InvalidProgramException("Тест не прошёл");
        }
    }

    private static void TestLab4()
    {
        int N;
        List<Lab4.Edge> edges;

        {
            N = 6;
            edges = new()
            {
                new() { From = 1, To = 2, MaxFlow = 7 },
                new() { From = 1, To = 3, MaxFlow = 4 },
                new() { From = 2, To = 3, MaxFlow = 4 },
                new() { From = 2, To = 5, MaxFlow = 2 },
                new() { From = 3, To = 4, MaxFlow = 4 },
                new() { From = 3, To = 5, MaxFlow = 8 },
                new() { From = 4, To = 6, MaxFlow = 12 },
                new() { From = 5, To = 4, MaxFlow = 4 },
                new() { From = 5, To = 6, MaxFlow = 5 },
            };

            var result = Lab4.FindMaxFlow(N, edges, 1, 6);
            if (result.Where(edge => edge.Key.From == 1).Sum(edge => edge.Value) != 10 ||
                result.Where(edge => edge.Key.To == 6).Sum(edge => edge.Value) != 10)
                throw new InvalidProgramException("Тест не прошёл");
            if (Enumerable.Range(1, N).Except(new[] { 1, 6 }).Any(vertex => 
                result.Where(edge => edge.Key.From == vertex).Sum(edge => edge.Value) !=
                result.Where(edge => edge.Key.To == vertex).Sum(edge => edge.Value)))
                throw new InvalidProgramException("Тест не прошёл");
        }

        {
            N = 12;
            edges = new()
            {
                new() { From = 11, To = 1, MaxFlow = 10 },
                new() { From = 11, To = 3, MaxFlow = 2 },
                new() { From = 11, To = 4, MaxFlow = 4 },
                new() { From = 1, To = 2, MaxFlow = 5 },
                new() { From = 1, To = 3, MaxFlow = 7 },
                new() { From = 2, To = 12, MaxFlow = 8 },
                new() { From = 3, To = 2, MaxFlow = 6 },
                new() { From = 3, To = 12, MaxFlow = 2 },
                new() { From = 4, To = 5, MaxFlow = 10 },
                new() { From = 5, To = 12, MaxFlow = 13 },
            };

            var result = Lab4.FindMaxFlow(N, edges, 11, 12);
            if (result.Where(edge => edge.Key.From == 11).Sum(edge => edge.Value) != 14 ||
                result.Where(edge => edge.Key.To == 12).Sum(edge => edge.Value) != 14)
                throw new InvalidProgramException("Тест не прошёл");
            if (Enumerable.Range(1, N).Except(new[] { 11, 12 }).Any(vertex =>
                result.Where(edge => edge.Key.From == vertex).Sum(edge => edge.Value) !=
                result.Where(edge => edge.Key.To == vertex).Sum(edge => edge.Value)))
                throw new InvalidProgramException("Тест не прошёл");
        }

        {
            N = 3;
            edges = new()
            {
                new() { From = 1, To = 2, MaxFlow = 1 },
            };

            var result = Lab4.FindMaxFlow(N, edges, 1, 3);
            if (result.Any())
                throw new InvalidProgramException("Тест не прошёл");

            result = Lab4.FindMaxFlow(N, edges, 1, 2);
            if (result.Count != 1 || result.Single().Key != (1, 2) || result.Single().Value != 1)
                throw new InvalidProgramException("Тест не прошёл");
        }

        {
            N = 25;
            edges = new()
            {
                new() { From = 1, To = 11, MaxFlow = 1 },
                new() { From = 1, To = 12, MaxFlow = 1 },
                new() { From = 1, To = 13, MaxFlow = 1 },
                new() { From = 1, To = 14, MaxFlow = 1 },

                new() { From = 11, To = 21, MaxFlow = 1 },
                new() { From = 11, To = 22, MaxFlow = 1 },
                new() { From = 12, To = 21, MaxFlow = 1 },
                new() { From = 13, To = 23, MaxFlow = 1 },
                new() { From = 13, To = 24, MaxFlow = 1 },
                new() { From = 14, To = 25, MaxFlow = 1 },

                new() { From = 21, To = 2, MaxFlow = 1 },
                new() { From = 22, To = 2, MaxFlow = 1 },
                new() { From = 23, To = 2, MaxFlow = 1 },
                new() { From = 24, To = 2, MaxFlow = 1 },
                new() { From = 25, To = 2, MaxFlow = 1 },
            };

            var result = Lab4.FindMaxFlow(N, edges, 1, 2);
            if (result.Where(edge => edge.Key.From == 1).Sum(edge => edge.Value) != 4 ||
                result.Where(edge => edge.Key.To == 2).Sum(edge => edge.Value) != 4)
                throw new InvalidProgramException("Тест не прошёл");
            if (Enumerable.Range(1, N).Except(new[] { 1, 2 }).Any(vertex =>
                result.Where(edge => edge.Key.From == vertex).Sum(edge => edge.Value) !=
                result.Where(edge => edge.Key.To == vertex).Sum(edge => edge.Value)))
                throw new InvalidProgramException("Тест не прошёл");
        }

        {
            N = 200;
            edges = new()
            {
                new() { From = 100, To = 1, MaxFlow = 1 },
                new() { From = 100, To = 2, MaxFlow = 1 },
                new() { From = 100, To = 3, MaxFlow = 1 },
                new() { From = 100, To = 4, MaxFlow = 1 },
                new() { From = 100, To = 5, MaxFlow = 1 },

                new() { From = 1, To = 7, MaxFlow = 1 },
                new() { From = 1, To = 8, MaxFlow = 1 },
                new() { From = 3, To = 6, MaxFlow = 1 },
                new() { From = 3, To = 9, MaxFlow = 1 },
                new() { From = 4, To = 10, MaxFlow = 1 },
                new() { From = 5, To = 7, MaxFlow = 1 },

                new() { From = 6, To = 200, MaxFlow = 1 },
                new() { From = 7, To = 200, MaxFlow = 1 },
                new() { From = 8, To = 200, MaxFlow = 1 },
                new() { From = 9, To = 200, MaxFlow = 1 },
                new() { From = 10, To = 200, MaxFlow = 1 },
            };

            var result = Lab4.FindMaxFlow(N, edges, 100, 200);
            if (result.Where(edge => edge.Key.From == 100).Sum(edge => edge.Value) != 4 ||
                result.Where(edge => edge.Key.To == 200).Sum(edge => edge.Value) != 4)
                throw new InvalidProgramException("Тест не прошёл");
            if (Enumerable.Range(1, N).Except(new[] { 100, 200 }).Any(vertex =>
                result.Where(edge => edge.Key.From == vertex).Sum(edge => edge.Value) !=
                result.Where(edge => edge.Key.To == vertex).Sum(edge => edge.Value)))
                throw new InvalidProgramException("Тест не прошёл");
        }

    }

    private static void TestLab5()
    {
        int N;
        List<(int Left, int Right)> edges;

        {
            edges = new()
            {
                (11, 22),
                (12, 21),
                (12, 23),
            };

            var result = Lab5.FindMaxMatching(edges);
            if (result.Count != 2)
                throw new InvalidProgramException("Тест не прошёл");
        }

        {
            edges = new()
            {
                (11, 21),
                (12, 21),
                (12, 23),
                (13, 22),
                (13, 23),
                (13, 24),
                (14, 23),
                (15, 23),
            };

            var result = Lab5.FindMaxMatching(edges);
            if (result.Count != 3)
                throw new InvalidProgramException("Тест не прошёл");
        }
    }
    
    private static void TestLab6()
    {
        Matrix costs;

        {
            costs = new Matrix(new[]
            {
                new[] { 7, 2, 1, 9, 4 },
                new[] { 9, 6, 9, 5, 5 },
                new[] { 3, 8, 3, 1, 8 },
                new[] { 7, 9, 4, 2, 2 },
                new[] { 8, 4, 7, 4, 8 },
            });

            var result = Lab6.FindBestPlan(costs);
            if (result.Cost != 15 || string.Join(",", result.Targets) != "3,4,1,5,2")
                throw new InvalidProgramException("Тест не прошёл");
        }

        {
            costs = new Matrix(new[]
            {
                new[] { 2, 4, 1, 3, 3 },
                new[] { 1, 5, 4, 1, 2 },
                new[] { 3, 5, 2, 2, 4 },
                new[] { 1, 4, 3, 1, 4 },
                new[] { 3, 2, 5, 3, 5 },
            });

            var result = Lab6.FindBestPlan(costs);
            if (result.Cost != 8 || string.Join(",", result.Targets) != "3,5,4,1,2")
                throw new InvalidProgramException("Тест не прошёл");
        }


        {
            costs = new Matrix(new[]
            {
                new[] { -6, -15, -3, -12, -4, -2 },
                new[] { -14, -3, -3, -7, -2, -1 },
                new[] { -3, -2, -8, -15, -8, -12 },
                new[] { -3, -14, -3, -15, -11, -10 },
                new[] { -3, -13, -1, -9, -6, -6 },
                new[] { -15, -10, -3, -4, -5, -10 },
            });

            var result = Lab6.FindBestPlan(costs);
            if (result.Cost != -68)
                throw new InvalidProgramException("Тест не прошёл");
        }

        {
            costs = new Matrix(new[]
            {
                new[] { -15, -16, -17, -18, -19 },
                new[] { -1, -1, -3, -1, -16 },
                new[] { -2, -8, -8, -6, -17 },
                new[] { -2, -2, -2, -8, -18 },
                new[] { -4, -6, -7, -8, -19 },
            });

            var result = Lab6.FindBestPlan(costs);
            if (result.Cost != -54)
                throw new InvalidProgramException("Тест не прошёл");
        }
    }
}
