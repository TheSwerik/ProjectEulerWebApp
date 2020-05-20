using System;
using System.Collections.Generic;

namespace ProjectEulerWebApp.Util
{
    public static class ProjectEulerAnswerGetter
    {
        public static Dictionary<string, long> solve(int id)
        {
            var result = new Dictionary<string, long>();
            //TODO call the packaged Jar and Exe files
            var java = GetJava(id);
            if (java != -1) result.Add("Java", java);

            var csharp = GetCSharp(id);
            if (csharp != -1) result.Add("C#", csharp);

            var cpp = GetCPlusPlus(id);
            if (cpp != -1) result.Add("C++", cpp);

            var python = GetPython(id);
            if (python != -1) result.Add("Python", python);

            return result;
        }

        private static long GetJava(int id)
        {
            //TODO
            return -1;
        }

        private static long GetCSharp(int id)
        {
            //TODO
            return -1;
        }

        private static long GetCPlusPlus(int id)
        {
            //TODO
            return -1;
        }

        private static long GetPython(int id)
        {
            //TODO
            return -1;
        }
    }
}