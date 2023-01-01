namespace Math.Current;

public static class Lab2
{
    public static (List<decimal> Constraint, decimal FreeMemberValue)? FindIntegerPlanConstraint(Matrix A, decimal[] b, decimal[] c)
    {
        var A_ = A.Copy();
        var b_ = new Matrix(b);
        var plan = Previous.Lab3.FindAnyPlan(ref A_, ref b_);
        Previous.Lab2.FindOptimalPlan(new Matrix(c), A_, ref plan.x, ref plan.B);

        if (plan.x.ContainsOnlyIntegerEPS())
            return null;

        var indexOfNonIntegerX = plan.x.AsColumn().Select((Value, Index) => (Value, Index)).First(pair => !pair.Value.IsIntegerEPS()).Index;
        var k = plan.B.Select((Value, Index) => (Value, Index)).Single(pair => pair.Value == indexOfNonIntegerX).Index;

        var N = Enumerable.Range(0, plan.x.AsColumn().Length).Except(plan.B).ToList();
        var A_N = new Matrix(N.Select(i => A.GetColumn(i))).Transpose();
        var A_B = new Matrix(plan.B.Select(i => A.GetColumn(i))).Transpose();
        var A_B_reversed = new Matrix(Accord.Math.Matrix.Inverse(A_B));
        var l = (A_B_reversed * A_N)[k];

        var constraint = new List<decimal>();
        constraint.AddRange(Enumerable.Repeat(0m, A.Rows));
        constraint.AddRange(l.Select(value => value.GetRealPart()));
        constraint.Add(-1);

        var b_new = plan.x.AsColumn()[indexOfNonIntegerX].GetRealPart();

        return (constraint, b_new);
    }
}
