using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

public static class DataExtensions
{
    #region EntityFramework Extensions

    public static IQueryable<T> OrderBy<T>(this IQueryable<T> Source, string sortExpression)
    {
        if (Source == null)
            throw new ArgumentNullException("source", "source is null.");

        if (string.IsNullOrEmpty(sortExpression))
            throw new ArgumentException("sortExpression is null or empty.", "sortExpression");

        var parts = sortExpression.Split(' ');
        var isDescending = false;
        var propertyName = "";
        var tType = typeof(T);

        if (parts.Length > 0 && parts[0] != "")
        {
            propertyName = parts[0];

            if (parts.Length > 1)
                isDescending = parts[1].ToLower().Contains("esc");

            PropertyInfo prop = tType.GetProperty(propertyName);

            if (prop == null)
                throw new ArgumentException(string.Format("No property '{0}' on type '{1}'", propertyName, tType.Name));

            var funcType = typeof(Func<,>).MakeGenericType(tType, prop.PropertyType);

            var lambdaBuilder = typeof(Expression)
                .GetMethods()
                .First(x => x.Name == "Lambda" && x.ContainsGenericParameters && x.GetParameters().Length == 2)
                .MakeGenericMethod(funcType);

            var parameter = Expression.Parameter(tType);
            var propExpress = Expression.Property(parameter, prop);

            var sortLambda = lambdaBuilder.Invoke(null, new object[] { propExpress, new ParameterExpression[] { parameter } });

            var sorter = typeof(Queryable)
                .GetMethods()
                .FirstOrDefault(x => x.Name == (isDescending ? "OrderByDescending" : "OrderBy") && x.GetParameters().Length == 2)
                .MakeGenericMethod(new[] { tType, prop.PropertyType });

            return (IQueryable<T>)sorter.Invoke(null, new object[] { Source, sortLambda });
        }

        return Source;
    }

    public static IEnumerable<T> OrderBy<T>(this IEnumerable<T> list, string sortExpression)
    {
        sortExpression += "";
        string[] parts = sortExpression.Split(' ');
        string property = "";

        if (parts.Length > 0 && parts[0] != "")
        {
            property = parts[0];

            PropertyInfo prop = typeof(T).GetProperty(property);

            if (prop == null)
            {
                throw new Exception("No property '" + property + "' in + " + typeof(T).Name + "'");
            }

            return list.OrderBy(x => prop.GetValue(x, null));
        }

        return list;
    }

    public static IEnumerable<T> OrderByDescending<T>(this IEnumerable<T> list, string sortExpression)
    {
        sortExpression += "";
        string[] parts = sortExpression.Split(' ');
        string property = "";

        if (parts.Length > 0 && parts[0] != "")
        {
            property = parts[0];

            PropertyInfo prop = typeof(T).GetProperty(property);

            if (prop == null)
            {
                throw new Exception("No property '" + property + "' in + " + typeof(T).Name + "'");
            }

            return list.OrderByDescending(x => prop.GetValue(x, null));
        }

        return list;
    }

    public static Expression<Func<TTo, bool>> ConvertToExpression<TFrom, TTo>(this Expression<Func<TFrom, bool>> from)
    {
        return ConvertImpl<Func<TFrom, bool>, Func<TTo, bool>>(from);
    }

    private static Expression<TTo> ConvertImpl<TFrom, TTo>(Expression<TFrom> from)
        where TFrom : class
        where TTo : class
    {
        // figure out which types are different in the function-signature
        var fromTypes = from.Type.GetGenericArguments();
        var toTypes = typeof(TTo).GetGenericArguments();
        if (fromTypes.Length != toTypes.Length)
            throw new NotSupportedException(
                "Incompatible lambda function-type signatures");
        Dictionary<Type, Type> typeMap = new Dictionary<Type, Type>();
        for (int i = 0; i < fromTypes.Length; i++)
        {
            if (fromTypes[i] != toTypes[i])
                typeMap[fromTypes[i]] = toTypes[i];
        }

        // re-map all parameters that involve different types
        Dictionary<Expression, Expression> parameterMap
            = new Dictionary<Expression, Expression>();
        ParameterExpression[] newParams =
            new ParameterExpression[from.Parameters.Count];
        for (int i = 0; i < newParams.Length; i++)
        {
            Type newType;
            if (typeMap.TryGetValue(from.Parameters[i].Type, out newType))
            {
                parameterMap[from.Parameters[i]] = newParams[i] =
                    Expression.Parameter(newType, from.Parameters[i].Name);
            }
            else
            {
                newParams[i] = from.Parameters[i];
            }
        }

        // rebuild the lambda
        var body = new TypeConversionVisitor(parameterMap).Visit(from.Body);
        return Expression.Lambda<TTo>(body, newParams);
    }

    #endregion

    public static DataTable ConvertToDataTable<T>(this IEnumerable<T> data)
    {
        PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
        DataTable table = new DataTable();
        foreach (PropertyDescriptor prop in properties)
            table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
        foreach (T item in data)
        {
            DataRow row = table.NewRow();
            foreach (PropertyDescriptor prop in properties)
                row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
            table.Rows.Add(row);
        }
        table.AcceptChanges();
        return table;
    }

    public static DataTable ConvertToDataTable<T>(this IList<T> data)
    {
        PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
        DataTable table = new DataTable();
        foreach (PropertyDescriptor prop in properties)
            table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
        foreach (T item in data)
        {
            DataRow row = table.NewRow();
            foreach (PropertyDescriptor prop in properties)
                row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
            table.Rows.Add(row);
        }
        table.AcceptChanges();
        return table;
    }

    public static DataRow ConvertToDataRow<T>(this T item, DataTable table)
    {
        PropertyDescriptorCollection properties =
            TypeDescriptor.GetProperties(typeof(T));
        DataRow row = table.NewRow();
        foreach (PropertyDescriptor prop in properties)
            row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
        return row;
    }

    public static T ConvertToEntity<T>(this DataRow tableRow) where T : new()
    {
        // Create a new type of the entity I want
        Type t = typeof(T);
        T returnObject = new T();

        foreach (DataColumn col in tableRow.Table.Columns)
        {
            string colName = col.ColumnName;

            // Look for the object's property with the columns name, ignore case
            PropertyInfo pInfo = t.GetProperty(colName.ToLower(),
                BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

            // did we find the property ?
            if (pInfo != null)
            {
                object val = tableRow[colName];

                // is this a Nullable<> type
                bool IsNullable = (Nullable.GetUnderlyingType(pInfo.PropertyType) != null);
                if (IsNullable)
                {
                    if (val is System.DBNull)
                    {
                        val = null;
                    }
                    else
                    {
                        // Convert the db type into the T we have in our Nullable<T> type
                        val = Convert.ChangeType(val, Nullable.GetUnderlyingType(pInfo.PropertyType));
                    }
                }
                else
                {
                    // Convert the db type into the type of the property in our entity
                    SetDefaultValue(ref val, pInfo.PropertyType);
                    if (pInfo.PropertyType.IsEnum && !pInfo.PropertyType.IsGenericType)
                    {
                        val = Enum.ToObject(pInfo.PropertyType, val);
                    }
                    else
                        val = Convert.ChangeType(val, pInfo.PropertyType);
                }
                // Set the value of the property with the value from the db
                if (pInfo.CanWrite)
                    pInfo.SetValue(returnObject, val, null);
            }
        }

        // return the entity object with values
        return returnObject;
    }

    private static void SetDefaultValue(ref object val, Type propertyType)
    {
        if (val is DBNull)
        {
            val = GetDefault(propertyType);
        }
    }

    public static object GetDefault(Type type)
    {
        if (type.IsValueType)
        {
            return Activator.CreateInstance(type);
        }
        return null;
    }

    public static List<T> ConvertToList<T>(this DataTable table) where T : new()
    {
        Type t = typeof(T);

        // Create a list of the entities we want to return
        List<T> returnObject = new List<T>();

        // Iterate through the DataTable's rows
        foreach (DataRow dr in table.Rows)
        {
            // Convert each row into an entity object and add to the list
            T newRow = dr.ConvertToEntity<T>();
            returnObject.Add(newRow);
        }

        // Return the finished list
        return returnObject;
    }

    public static List<Dictionary<string, object>> ConvertToList(this DataTable table)
    {
        // Create a list of the entities we want to return
        List<Dictionary<string, object>> returnObject = new List<Dictionary<string, object>>();
        Dictionary<string, object> row = null;

        // Iterate through the DataTable's rows
        foreach (DataRow dr in table.Rows)
        {
            row = new Dictionary<string, object>();
            foreach (DataColumn col in table.Columns)
            {
                row.Add(col.ColumnName, dr[col].ToString());
            }
            returnObject.Add(row);
        }

        // Return the finished list
        return returnObject;
    }

    public static string ToEasternArabicNumerals(this string input)
    {
        UTF8Encoding utf8Encoder = new UTF8Encoding();
        Decoder utf8Decoder = utf8Encoder.GetDecoder();
        StringBuilder convertedChars = new StringBuilder();
        char[] convertedChar = new char[1];
        byte[] bytes = { 217, 160 };
        char[] inputCharArray = input.ToCharArray();
        foreach (var c in inputCharArray)
        {
            if (char.IsDigit(c))
            {
                bytes[1] = Convert.ToByte(160 + char.GetNumericValue(c));
                utf8Decoder.GetChars(bytes, 0, 2, convertedChar, 0);
                convertedChars.Append(convertedChar[0]);
            }
            else
            {
                convertedChars.Append(c);
            }
        }
        return convertedChars.ToString();
    }
}
