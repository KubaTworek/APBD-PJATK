using LinqTasks.Models;

namespace LinqTasks.Extensions;

public static class CustomExtensionMethods
{
    //Put your extension methods here
    public static IEnumerable<Emp> GetEmpsWithSubordinates(this IEnumerable<Emp> emps)
    {
        var result = emps.Where(emp => emps.Any(subordinate => subordinate.Mgr?.Empno == emp.Empno))
                         .OrderBy(emp => emp.Ename)
                         .ThenByDescending(emp => emp.Salary);

        return result;
    }
}