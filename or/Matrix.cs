using System.Collections;

namespace Math;

public partial class Matrix : IEnumerable<decimal[]>
{
    private decimal[][] Data { get; set; }

    public static implicit operator decimal[][](Matrix matrix) => matrix.Data;

    public int Rows => Data.Length;
    public int Columns => Data[0].Length;

    public decimal[] this[int i]
    {
        get => Data[i];
        set
        {
            if (value.Length != Columns) throw new ArgumentException("Длина не та");
            Data[i] = value;
        }
    }

    public IEnumerator<decimal[]> GetEnumerator()
        => Data.AsEnumerable().GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
        => Data.AsEnumerable().GetEnumerator();

    public decimal[] AsColumn() => Data.Select(row => row.Single()).ToArray();
    public decimal[] GetColumn(int index) => Data.Select(row => row[index]).ToArray();

    public decimal AsNumber() => Data.Single().Single();

    public Matrix Copy() => new(Data);

    public Matrix Transpose()
    {
        var result = new Matrix(Columns, Rows);

        for (int i = 0; i < Rows; i++)
            for (int j = 0; j < Columns; j++)
                result[j][i] = Data[i][j];

        return result;
    }
}
