namespace Math;

public partial class Matrix
{
    public static bool operator == (Matrix a, Matrix b)
    {
        if (a.Rows != b.Rows || a.Columns != b.Columns)
            throw new ArgumentException("Матрицы разного размера");

        for (int i = 0; i < a.Rows; i++)
            for (int j = 0; j < a.Columns; j++)
                if (a[i][j] != b[i][j])
                    return false;

        return true;
    }

    public static bool operator != (Matrix a, Matrix b)
        => !(a == b);

    public static Matrix operator * (decimal x, Matrix a)
    {
        var result = new Matrix(a.Rows, a.Columns);

        for (int i = 0; i < a.Rows; i++)
            for (int j = 0; j < a.Columns; j++)
                result[i][j] = a[i][j] * x;

        return result;
    }

    public static Matrix operator * (Matrix a, Matrix b)
    {
        if (a.Columns != b.Rows)
            throw new ArgumentException("Они ж не согласованы!");

        var result = new Matrix(a.Rows, b.Columns);

        for (int i = 0; i < a.Rows; i++)
            for (int k = 0; k < a.Columns; k++)
                if (a[i][k] != 0)
                    for (int j = 0; j < b.Columns; j++)
                        result[i][j] += a[i][k] * b[k][j];

        return result;
    }

    public static Matrix operator + (Matrix a, Matrix b)
    {
        if (a.Rows != b.Rows || a.Columns != b.Columns)
            throw new ArgumentException("Они ж разного размера!");

        var result = new Matrix(a.Rows, a.Columns);

        for (int i = 0; i < a.Rows; i++)
            for (int j = 0; j < a.Columns; j++)
                result[i][j] = a[i][j] + b[i][j];

        return result;
    }


    public static Matrix operator - (Matrix a, Matrix b)
        => a + (-1 * b);
}
