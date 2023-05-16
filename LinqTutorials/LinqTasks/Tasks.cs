using LinqTasks.Extensions;
using LinqTasks.Models;
using System.Linq;

namespace LinqTasks;

public static partial class Tasks
{
    public static IEnumerable<Emp> Emps { get; set; }
    public static IEnumerable<Dept> Depts { get; set; }

    static Tasks()
    {
        Depts = LoadDepts();
        Emps = LoadEmps();
    }

    /// <summary>
    ///     SELECT * FROM Emps WHERE Job = "Backend programmer";
    /// </summary>
    public static IEnumerable<Emp> Task1()
    {
        IEnumerable<Emp> result = null;

        // Query syntax
        result = (from emp in Emps where emp.Job == "Backend programmer" select emp).ToList();

        // Lambda
        result = Emps.Where(emp => emp.Job == "Backend programmer").ToList();

        return result;
    }

    /// <summary>
    ///     SELECT * FROM Emps Job = "Frontend programmer" AND Salary>1000 ORDER BY Ename DESC;
    /// </summary>
    public static IEnumerable<Emp> Task2()
    {
        IEnumerable<Emp> result = null;

        // Query syntax
        result = (from emp in Emps where emp.Job == "Frontend programmer" && emp.Salary > 1000 orderby emp.Ename descending select emp).ToList();

        // Lambda
        result = Emps.Where(emp => emp.Job == "Frontend programmer" && emp.Salary > 1000).OrderByDescending(emp => emp.Ename).ToList();

        return result;
    }


    /// <summary>
    ///     SELECT MAX(Salary) FROM Emps;
    /// </summary>
    public static int Task3()
    {
        int result = 0;

        // Query syntax
        result = (from emp in Emps select emp.Salary).Max();

        // Lambda 
        result = Emps.Max(emp => emp.Salary);

        return result;
    }

    /// <summary>
    ///     SELECT * FROM Emps WHERE Salary=(SELECT MAX(Salary) FROM Emps);
    /// </summary>
    public static IEnumerable<Emp> Task4()
    {
        IEnumerable<Emp> result = null;

        // Query syntax
        result = (from emp in Emps where emp.Salary == Emps.Max(e => e.Salary) select emp).ToList();

        // Lambda
        result = Emps.Where(e => e.Salary == Emps.Max(e => e.Salary)).Select(e => e).ToList();

        return result;
    }

    /// <summary>
    ///    SELECT ename AS Nazwisko, job AS Praca FROM Emps;
    /// </summary>
    public static IEnumerable<object> Task5()
    {
        IEnumerable<object> result;

        // Query syntax
        result = (from emp in Emps select new { Nazwisko = emp.Ename, Praca = emp.Job }).ToList();

        // Lambda
        result = Emps.Select(emp => new { Nazwisko = emp.Ename, Praca = emp.Job }).ToList();

        return result;
    }

    /// <summary>
    ///     SELECT Emps.Ename, Emps.Job, Depts.Dname FROM Emps
    ///     INNER JOIN Depts ON Emps.Deptno=Depts.Deptno
    ///     Rezultat: Złączenie kolekcji Emps i Depts.
    /// </summary>
    public static IEnumerable<object> Task6()
    {
        IEnumerable<object> result = null;

        // Query syntax
        result = (from emp in Emps join dept in Depts on emp.Deptno equals dept.Deptno select new { Ename = emp.Ename, Job = emp.Job, Dname = dept.Dname }).ToList();

        // Lambda
        result = Emps.Join(Depts, emp => emp.Deptno, dept => dept.Deptno, (emp, dept) => new { Emps = emp, Depts = dept }).Select(s => new { s.Emps.Ename, s.Emps.Job, s.Depts.Dname }).ToList();
        
        return result;
    }

    /// <summary>
    ///     SELECT Job AS Praca, COUNT(1) LiczbaPracownikow FROM Emps GROUP BY Job;
    /// </summary>
    public static IEnumerable<object> Task7()
    {
        IEnumerable<object> result = null;

        // Query syntax
        result = (from emp in Emps group emp by emp.Job into tmp select new { Praca = tmp.Key, LiczbaPracownikow = tmp.Count() }).ToList();

        // Lambda
        result = Emps.GroupBy(e => e.Job).Select(e => new { Praca = e.Key, LiczbaPracownikow = e.Count() }).ToList();

        return result;
    }

    /// <summary>
    ///     Zwróć wartość "true" jeśli choć jeden
    ///     z elementów kolekcji pracuje jako "Backend programmer".
    /// </summary>
    public static bool Task8()
    {
        bool result;

        // Query Syntax
        result = (from emp in Emps
                       where emp.Job == "Backend programmer"
                       select emp).Any();

        // Lambda
        result = Emps.Any(emp => emp.Job == "Backend programmer");

        return result;
    }

    /// <summary>
    ///     SELECT TOP 1 * FROM Emp WHERE Job="Frontend programmer"
    ///     ORDER BY HireDate DESC;
    /// </summary>
    public static Emp Task9()
    {
        Emp result = null;

        // Query syntax
        result = (from emp in Emps where emp.Job == "Frontend programmer" orderby emp.HireDate descending select emp).FirstOrDefault();

        // Lambda
        result = Emps.Where(e => e.Job == "Frontend programmer").OrderByDescending(emp => emp.HireDate).First();

        return result;
    }

    /// <summary>
    ///     SELECT Ename, Job, Hiredate FROM Emps
    ///     UNION
    ///     SELECT "Brak wartości", null, null;
    /// </summary>
    public static IEnumerable<object> Task10()
    {
        IEnumerable<object> result = null;

        // Query Syntax
        var querySyntaxResult = from emp in Emps
                                select new { Ename = emp.Ename, Job = emp.Job, HireDate = emp.HireDate };

        result = querySyntaxResult.Union(new[] { new { Ename = "Brak wartości", Job = (string)null, HireDate = (DateTime?)null } });

        // Lambda
        result = Emps.Select(emp => new { Ename = emp.Ename, Job = emp.Job, HireDate = emp.HireDate })
                               .Union(new[] { new { Ename = "Brak wartości", Job = (string)null, HireDate = (DateTime?)null } });

        return result;
    }

    /// <summary>
    ///     Wykorzystując LINQ pobierz pracowników podzielony na departamenty pamiętając, że:
    ///     1. Interesują nas tylko departamenty z liczbą pracowników powyżej 1
    ///     2. Chcemy zwrócić listę obiektów o następującej srukturze:
    ///     [
    ///     {name: "RESEARCH", numOfEmployees: 3},
    ///     {name: "SALES", numOfEmployees: 5},
    ///     ...
    ///     ]
    ///     3. Wykorzystaj typy anonimowe
    /// </summary>
    public static IEnumerable<object> Task11()
    {
        // SELECT DEPT.DNAME, COUNT(*) FROM DEPT INNER JOIN EMP ON DEPT.DEPTNO = EMP.DEPTNO GROUP BY DEPT.DNAME HAVING COUNT(*) > 1;
        IEnumerable<object> result = null;

        // Query syntax
        result = (from emp in Emps
                  join dept in Depts on emp.Deptno equals dept.Deptno
                  group dept by dept.Dname into g
                  where g.Count() > 1
                  select new { name = g.Key, numOfEmployees = g.Count() }).ToList();

        return result;
    }

    /// <summary>
    ///     Napisz własną metodę rozszerzeń, która pozwoli skompilować się poniższemu fragmentowi kodu.
    ///     Metodę dodaj do klasy CustomExtensionMethods, która zdefiniowana jest poniżej.
    ///     Metoda powinna zwrócić tylko tych pracowników, którzy mają min. 1 bezpośredniego podwładnego.
    ///     Pracownicy powinny w ramach kolekcji być posortowani po nazwisku (rosnąco) i pensji (malejąco).
    /// </summary>
    public static IEnumerable<Emp> Task12()
    {
        IEnumerable<Emp> result = Emps.GetEmpsWithSubordinates();
        return result;
    }

    /// <summary>
    ///     Poniższa metoda powinna zwracać pojedyczną liczbę int.
    ///     Na wejściu przyjmujemy listę liczb całkowitych.
    ///     Spróbuj z pomocą LINQ'a odnaleźć tę liczbę, które występuja w tablicy int'ów nieparzystą liczbę razy.
    ///     Zakładamy, że zawsze będzie jedna taka liczba.
    ///     Np: {1,1,1,1,1,1,10,1,1,1,1} => 10
    /// </summary>
    public static int Task13(int[] arr)
    {
        int result = 0;

        // Query Syntax
        result = (from i in arr
                  group i by i into grp
                  orderby grp.Count() % 2 != 0 descending
                  select grp.Key).First();

        // Lambda
        result = arr.GroupBy(i => i).OrderByDescending(grp => grp.Count() % 2 != 0).Select(grp => grp.Key).First();

        return result;
    }

    /// <summary>
    ///     Zwróć tylko te departamenty, które mają 5 pracowników lub nie mają pracowników w ogóle.
    ///     Posortuj rezultat po nazwie departament rosnąco.
    /// </summary>
    public static IEnumerable<Dept> Task14()
    {
        IEnumerable<Dept> result = null;

        // Query Syntax
        result = (from dept in Depts
                  join emp in Emps on dept.Deptno equals emp.Deptno into deptEmps
                  where deptEmps.Count() == 5 || deptEmps.Count() == 0
                  orderby dept.Dname ascending
                  select dept);

        // Lambda
        result = Depts.Where(d => Emps.Count(e => e.Deptno == d.Deptno) == 5 || !Emps.Any(e => e.Deptno == d.Deptno))
                      .OrderBy(d => d.Dname)
                      .Select(d => d);

        return result;
    }
}