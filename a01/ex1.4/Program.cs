using System;
using System.Collections.Generic;

interface Expr
{
    int eval(Dictionary<string, int> env);
    Expr simplify();
}

class CstI : Expr
{
    public int c;
    public CstI(int c)
    {
        this.c = c;
    }

    public int eval(Dictionary<string, int> env)
    {
        return c;
    }

    public Expr simplify()
    {
        return this;
    }

    public override String ToString()
    {
        return ""+c;
    }
}

class Var : Expr
{
    public string c;
    public Var(String c)
    {
        this.c = c;
    }

    public int eval(Dictionary<string, int> env)
    {
        return env[c];
    }

    public Expr simplify()
    {
        return this;
    }

    public override String ToString()
    {
        return ""+c;
    }
}

abstract class Binop : Expr
{
    public char symbol;
    public Func<int, int, int> op;
    public Func<Expr, Expr, Expr> simpl;
    public Expr a, b;

    public Binop(Expr a, Expr b)
    {
        this.a = a;
        this.b = b;
    }

    public int eval(Dictionary<string, int> env)
    {
        int a1 = a.eval(env);
        int b1 = b.eval(env);
        return op(a1, b1);
    }

    public Expr simplify()
    {
        return simpl(a.simplify(), b.simplify());
    }

    public override String ToString()
    {
        return $"({a} {symbol} {b})";
    }
}

class Add : Binop
{
    public Add(Expr a, Expr b) : base (a, b)
    {
        this.symbol = '+';
        this.op = (x, y) => x + y;
        this.simpl = (a, b) => {
            CstI aCst = a as CstI;
            CstI bCst = b as CstI;

            if (aCst?.c == 0) return b;
            if (bCst?.c == 0) return a;
            if (aCst != null && bCst != null)
                return new CstI(op(aCst.c, bCst.c));
            return this;
        };
    }
}

class Mul : Binop
{
    public Mul(Expr a, Expr b) : base (a, b)
    {
        this.symbol = '*';
        this.op = (x, y) => x * y;
        this.simpl = (a, b) => {
            CstI aCst = a as CstI;
            CstI bCst = b as CstI;

            if (aCst?.c == 0) return new CstI(0);
            if (bCst?.c == 0) return new CstI(0);
            if (aCst?.c == 1) return b;
            if (bCst?.c == 1) return a;
            if (aCst != null && bCst != null)
                return new CstI(op(aCst.c, bCst.c));
            return this;
        };
    }
}

class Sub : Binop
{
    public Sub(Expr a, Expr b) : base (a, b)
    {
        this.symbol = '-';
        this.op = (x, y) => x - y;
        this.simpl = (a, b) => {
            CstI aCst = a as CstI;
            CstI bCst = b as CstI;

            if (bCst?.c == 0) return a;
            if (aCst != null && bCst != null)
                return new CstI(op(aCst.c, bCst.c));
            return this;
        };
    }
}

class Mod : Binop
{
    public Mod(Expr a, Expr b) : base (a, b)
    {
        this.symbol = '%';
        this.op = (x, y) => x % y;
        this.simpl = (a, b) => {
            CstI aCst = a as CstI;
            CstI bCst = b as CstI;

            if (bCst?.c == 0) return a;
            if (bCst?.c == 1) return new CstI(0);
            if (aCst != null && bCst != null)
                return new CstI(op(aCst.c, bCst.c));
            return this;
        };
    }
}

class BinAnd : Binop
{
    public BinAnd(Expr a, Expr b) : base (a, b)
    {
        this.symbol = '&';
        this.op = (x, y) => x & y;
        this.simpl = (a, b) => {
            CstI aCst = a as CstI;
            CstI bCst = b as CstI;

            if (aCst is not null && bCst is not null)
                return new CstI(op(aCst.c, bCst.c));
            return this;
        };
    }
}

class BinOr : Binop
{
    public BinOr(Expr a, Expr b) : base (a, b)
    {
        this.symbol = '|';
        this.op = (x, y) => x | y;
        this.simpl = (a, b) => {
            CstI aCst = a as CstI;
            CstI bCst = b as CstI;

            if (aCst is not null && bCst is not null)
                return new CstI(op(aCst.c, bCst.c));
            return this;
        };
    }
}

class Program
{
    public static void Main(String[] args)
    {
        Expr e = new Add(new CstI(17), new Var("z"));
        Console.WriteLine(e.ToString());

        var env = new Dictionary<string, int>()
        {
            {"z", 3}
        };
        Console.WriteLine($"{e} = {e.eval(env)}");

        Expr e3 =
            new Add(
                new Sub(
                    new CstI(5),
                    new CstI(5)
                ),
                new Mul(
                    new CstI(1),
                    new Var("z")
                )
            );
        Console.WriteLine($"{e3} = {e3.simplify()}");

        Expr e4 =
            new Mod(
                new Var("x"),
                new CstI(0)
            );
        Expr e5 =
            new Mod(
                new Var("x"),
                new CstI(1)
            );
        Expr e6 =
            new Mod(
                new Var("x"),
                new CstI(2)
            );
        Console.WriteLine($"{e4} = {e4.simplify()}");
        Console.WriteLine($"{e5} = {e5.simplify()}");
        Console.WriteLine($"{e6} = {e6.simplify()}");
    }
}