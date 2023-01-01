namespace Math;

public partial class Matrix
{
    public override string ToString()
    {
        string result = string.Empty;

        for (int i = 0; i < Rows; i++)
        {
            result += string.Join(" ", Data[i]);
            result += " / ";
        }

        return result;
    }

    public void ConsoleWrite()
        => Console.WriteLine(ToString().Replace(" / ", Environment.NewLine));

    public void ValidateAsColumn(int Rows)
    {
        if (this.Rows != Rows || Data.Any(row => row.Length != 1))
            throw new ArgumentException("Матрица не является столбцом длины N");
    }

    public void ValidateSize(int Rows, int Columns)
    {
        if (this.Rows != Rows || this.Columns != Columns)
            throw new ArgumentException("Матрица не размера NxM");
    }

    public bool ContainsOnlyZeroEPS()
        => Data.SelectMany(row => row).All(x => x.EqualsZeroEPS());

    public bool ContainsNonZeroEPS()
        => ContainsOnlyZeroEPS() is false;

    public bool ContainsOnlyIntegerEPS()
        => Data.SelectMany(row => row).All(x => x.IsIntegerEPS());

    public bool ContainsNonIntegerEPS()
        => ContainsOnlyIntegerEPS() is false;
}
