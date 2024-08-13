using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;

namespace DistribucionRutas.Clases
{
    public static class Util
    {
        public static T ConvertirDataRowAObjeto<T>(DataRow row) where T : new()
        {
            T obj = new T();
            Type objType = typeof(T);

            foreach (DataColumn column in row.Table.Columns)
            {
                PropertyInfo prop = objType.GetProperty(column.ColumnName);
                if (prop != null && row[column] != DBNull.Value)
                {
                    prop.SetValue(obj, Convert.ChangeType(row[column], prop.PropertyType), null);
                }
            }
            return obj;
        }

        public static void MostrarMensaje(dynamic ViewBag, string mensaje, int tipo)
        {
            ViewBag.Mensaje = mensaje;
            ViewBag.Tipo = tipo;
            ViewBag.MostrarModal = true;
        }
    }
}