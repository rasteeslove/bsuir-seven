namespace Math.Current;

public static class Lab1
{
    public static decimal[] FindOptimalIntegerPlan(decimal[] c_чёрточка, Matrix A_чёрточка, decimal[] b_чёрточка, decimal[] d_minus_чёрточка, decimal[] d_plus_чёрточка)
    {
        var N = A_чёрточка.Columns;
        var M = A_чёрточка.Rows;

        if (c_чёрточка.Length != N ||
            b_чёрточка.Length != N ||
            d_minus_чёрточка.Length != N ||
            d_plus_чёрточка.Length != N)
            throw new ArgumentException();

        var c_чёрточка_signs = c_чёрточка.Select(System.Math.Sign).ToArray();

        for (int i = 0; i < N; i++)
        {
            if (c_чёрточка[i] > 0)
            {
                c_чёрточка[i] *= -1;

                for (int j = 0; j < A_чёрточка.Rows; j++)
                    A_чёрточка[j][i] *= -1;

                d_minus_чёрточка[i] *= -1;
                d_plus_чёрточка[i] *= -1;

                (d_minus_чёрточка[i], d_plus_чёрточка[i]) =
                    (d_plus_чёрточка[i], d_minus_чёрточка[i]);
            }
        }

        Matrix? BestIntegerPlan = null;
        decimal BestIntegerPlanValue = decimal.MinValue;
        var S = new Stack<(decimal alpha, Matrix c, Matrix A, Matrix b, Matrix d_minus, Matrix delta)>();

        var A_S = new Matrix(M + N, N + N + M);
        for (int i = 0; i < M; i++)
            for (int j = 0; j < N; j++)
                A_S[i][j] = A_чёрточка[i][j];

        for (int i = 0; i < N; i++)
            A_S[M + i][i] = 1;
        for (int i = 0; i < N + M; i++)
            A_S[i][N + i] = 1;

        S.Push(new()
        {
            alpha = 0,
            c = new Matrix(c_чёрточка.Concat(Enumerable.Repeat(0m, N + M))),
            A = A_S,
            b = new Matrix(b_чёрточка.Concat(d_plus_чёрточка)),
            d_minus = new Matrix(d_minus_чёрточка.Concat(Enumerable.Repeat(0m, N + M))),
            delta = new Matrix(d_minus_чёрточка.Concat(Enumerable.Repeat(0m, N + M))),
        });

        while (true)
        {
            if (S.Count == 0)
            {
                if (BestIntegerPlan is null)
                    throw new ArgumentException("Задача несовместна");

                var result = BestIntegerPlan.AsColumn().Take(N).ToArray();

                for (int i = 0; i < N; i++)
                    result[i] *= -1 * c_чёрточка_signs[i];

                return result;
            }

            var (alpha, c, A, b, d_minus, delta) = S.Pop();
            var alpha_кавычка = alpha + (c.Transpose() * d_minus).AsNumber();
            var b_кавычка = b - A * d_minus;

            Matrix x_тильда;
            try
            {
                var B = Enumerable.Range(N, N + M).ToArray();
                x_тильда = Previous.Lab4.FindOptimalPlan(c, A, b_кавычка, ref B);
            }
            catch (ArgumentException)
            {
                continue;
            }

            if (x_тильда.ContainsOnlyIntegerEPS())
            {
                var x_крышечка = x_тильда + delta;
                var value = (c.Transpose() * x_тильда).AsNumber() + alpha_кавычка;
                if (BestIntegerPlan is null || BestIntegerPlanValue < value)
                {
                    BestIntegerPlanValue = value;
                    BestIntegerPlan = x_крышечка;
                }
            }
            else
            {
                var index = x_тильда.AsColumn().Select((Value, Index) => (Value, Index)).First(pair => !pair.Value.IsIntegerEPS()).Index;

                var value = System.Math.Floor((c.Transpose() * x_тильда).AsNumber() + alpha_кавычка);
                if (BestIntegerPlan is null || value > BestIntegerPlanValue)
                {
                    var b_кавычка_кавычка_S = b_кавычка.Copy();
                    b_кавычка_кавычка_S[M + index][0] = System.Math.Floor(x_тильда[index][0]);

                    var d_minus_S = new Matrix(Enumerable.Repeat(0, N + N + M));
                    d_minus_S[index][0] = System.Math.Ceiling(x_тильда[index][0]);

                    var delta_S = delta + d_minus_S;

                    S.Push(new()
                    {
                        alpha = alpha_кавычка,
                        c = c,
                        A = A,
                        b = b_кавычка_кавычка_S,
                        d_minus = new Matrix(Enumerable.Repeat(0m, N + N + M)),
                        delta = delta,
                    });

                    S.Push(new()
                    {
                        alpha = alpha_кавычка,
                        c = c,
                        A = A,
                        b = b_кавычка,
                        d_minus = d_minus_S,
                        delta = delta_S,
                    });
                }
            }
        }
    }
}
