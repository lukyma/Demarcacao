using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace web.api.demarcacao.gestao.terreno.CrossCutting
{
    public static class UtilExtensions
    {
        public static void ForEach<TEntity>(this IEnumerable<TEntity> entities, Action<TEntity> action)
        {
            foreach (var entity in entities)
            {
                action(entity);
            }
        }

        public static string GetEmptyIfNumber0(this string number)
        {
            if (Regex.IsMatch(number, @"^-?[0-9,\.]+$") && Convert.ToDouble(number) == 0)
            {
                return string.Empty;
            }

            return number;
        }
    }
}
