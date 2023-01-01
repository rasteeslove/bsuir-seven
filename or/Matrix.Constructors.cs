namespace Math;

public partial class Matrix
{
    public Matrix(int Rows, int Columns)
    {
        if (Rows <= 0 || Columns <= 0)
            throw new ArgumentException("Матрица не прямоугольна");

        Data = new decimal[Rows].Select(_ => new decimal[Columns]).ToArray();
    }

    public static Matrix CreateIdentityMatrix(int N)
    {
        var result = new Matrix(N, N);

        for (int i = 0; i < N; i++)
            result[i][i] = 1;

        return result;
    }

    public Matrix(IEnumerable<IEnumerable<decimal>> matrix)
    {
        if (matrix.Count() == 0 || matrix.First().Count() == 0 || matrix.Select(m => m.Count()).Distinct().Count() != 1)
            throw new ArgumentException("Матрица не прямоугольна");

        Data = matrix.Select(m => m.ToArray()).ToArray();
    }

    public Matrix(IEnumerable<IEnumerable<int>> matrix)
        : this(matrix.Select(row => row.Select(x => (decimal)x))) { }

    public Matrix(IEnumerable<decimal> column)
        : this(column.Select(x => new[] { x })) { }

    public Matrix(IEnumerable<int> column)
        : this(column.Select(x => new[] { x })) { }
}
