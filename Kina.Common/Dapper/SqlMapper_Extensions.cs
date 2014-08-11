using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace Kina.Common.Dapper
{
    /// <summary>Dapper extensions.
    /// </summary>
    public partial class SqlMapper
    {
        // 缓存
        private static readonly ConcurrentDictionary<Type, List<string>> ParamNameCache = new ConcurrentDictionary<Type, List<string>>();

        /// <summary>
        /// 插入数据
        /// </summary>
        public static int Insert(this IDbConnection connection, dynamic data, string tablename, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            var obj = data as object;
            var properties = GetProperties(obj);
            var columns = string.Join(",", properties);
            var values = string.Join(",", properties.Select(p => ":" + p));
            var sql = string.Format("INSERT INTO {0} ({1}) VALUES ({2})", tablename, columns, values);

            return connection.Execute(sql, obj, transaction, commandTimeout);
        }
        /// <summary>
        /// 更新数据
        /// </summary>
        public static int Update(this IDbConnection connection, dynamic data, dynamic condition, string tablename, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            var obj = data as object;
            var properties = GetProperties(obj);
            var updateFields = string.Join(",", properties.Select(p => p + " = :" + p));

            var conditionObj = condition as object;
            var whereFields = string.Empty;
            var whereProperties = GetProperties(conditionObj);
            if (whereProperties.Count > 0)
            {
                whereFields = " WHERE " + string.Join(" AND ", whereProperties.Select(p => p + " = :" + p));
            }

            var sql = string.Format("UPDATE {0} SET {1}{2}", tablename, updateFields, whereFields);

            var parameters = new DynamicParameters(data);
            parameters.AddDynamicParams(condition);

            return connection.Execute(sql, parameters, transaction, commandTimeout);
        }
        /// <summary>
        /// 删除
        /// </summary>
        public static int Delete(this IDbConnection connection, dynamic condition, string tablename, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            var conditionObj = condition as object;
            var whereFields = string.Empty;
            var whereProperties = GetProperties(conditionObj);
            if (whereProperties.Count > 0)
            {
                whereFields = " WHERE " + string.Join(" AND ", whereProperties.Select(p => p + " = :" + p));
            }

            var sql = string.Format("DELETE FROM {0}{1}", tablename, whereFields);

            return connection.Execute(sql, conditionObj, transaction, commandTimeout);
        }

        /// <summary>
        /// GetCount
        /// </summary>
        public static int GetCount(this IDbConnection connection, object condition, string tablename, bool isOr = false, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return QueryList<int>(connection, condition, tablename, "COUNT(1)", isOr, transaction, commandTimeout).Single();
        }

        /// <summary>
        /// QueryList（dynamic）
        /// </summary>
        public static IEnumerable<dynamic> QueryList(this IDbConnection connection, dynamic condition, string tablename, string columns = "*", bool isOr = false, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return QueryList<dynamic>(connection, condition, tablename, columns, isOr, transaction, commandTimeout);
        }
        /// <summary>
        /// QueryList
        /// </summary>
        public static IEnumerable<T> QueryList<T>(this IDbConnection connection, object condition, string table, string columns = "*", bool isOr = false, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return connection.Query<T>(BuildQuerySql(condition, table, columns, isOr), condition, transaction, true, commandTimeout);
        }

        /// <summary>
        /// 分页查询（dynamic）
        /// </summary>
        public static IEnumerable<dynamic> QueryPaged(this IDbConnection connection, dynamic condition, string table, string orderBy, int pageIndex, int pageSize, string columns = "*", bool isOr = false, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return QueryPaged<dynamic>(connection, condition, table, orderBy, pageIndex, pageSize, columns, isOr, transaction, commandTimeout);
        }
        /// <summary>
        /// 分页查询
        /// </summary>
        public static IEnumerable<T> QueryPaged<T>(this IDbConnection connection, dynamic condition, string table, string orderBy, int pageIndex, int pageSize, string columns = "*", bool isOr = false, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            var conditionObj = condition as object;
            var whereFields = string.Empty;
            var properties = GetProperties(conditionObj);
            if (properties.Count > 0)
            {
                var separator = isOr ? " OR " : " AND ";
                whereFields = " WHERE " + string.Join(separator, properties.Select(p => p + " = :" + p));
            }
            var sql = string.Format("SELECT {0} FROM (SELECT ROW_NUMBER() OVER (ORDER BY {1}) AS RowNumber, {0} FROM {2}{3}) AS Total WHERE RowNumber >= {4} AND RowNumber <= {5}", columns, orderBy, table, whereFields, (pageIndex - 1) * pageSize + 1, pageIndex * pageSize);

            return connection.Query<T>(sql, conditionObj, transaction, true, commandTimeout);
        }

        #region 私有方法
        private static string BuildQuerySql(dynamic condition, string table, string selectPart = "*", bool isOr = false)
        {
            var conditionObj = condition as object;
            var properties = GetProperties(conditionObj);
            if (properties.Count == 0)
            {
                return string.Format("SELECT {1} FROM {0}", table, selectPart);
            }

            var separator = isOr ? " OR " : " AND ";
            var wherePart = string.Join(separator, properties.Select(p => p + " = :" + p));

            return string.Format("SELECT {2} FROM {0} WHERE {1}", table, wherePart, selectPart);
        }
        private static List<string> GetProperties(object obj)
        {
            if (obj == null)
            {
                return new List<string>();
            }
            if (obj is DynamicParameters)
            {
                return (obj as DynamicParameters).ParameterNames.ToList();
            }

            List<string> properties;
            if (ParamNameCache.TryGetValue(obj.GetType(), out properties)) return properties;
            properties = obj.GetType().GetProperties(BindingFlags.GetProperty | BindingFlags.Instance | BindingFlags.Public).Select(prop => prop.Name).ToList();
            ParamNameCache[obj.GetType()] = properties;
            return properties;
        }
        #endregion
    }
}
