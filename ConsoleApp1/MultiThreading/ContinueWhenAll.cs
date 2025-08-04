using System;

namespace Coding.Exercise
{
    public class Exercise
    {
        public static Task<string> FormatSquaredNumbersFrom1To(int n)
        {
            if (n < 0)
            {
                throw new ArgumentException();
            }
            List<Task<int>> squareTasks = new List<Task<int>>(n);
            for (int i = 1; i <= n; i++)
            {
                int a = i;
                squareTasks.Add(Task.Run(() => GetSquaredNumber(a)));
            }
            return Task.Factory.ContinueWhenAll(squareTasks.ToArray(), completedSqTasks => string.Join(", ", completedSqTasks.Select(x => x.Result)));
        }

        static int GetSquaredNumber(int n)
        {
            return n * n;
        }
    }
}
