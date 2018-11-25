using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Security;
using System.Security.Permissions;
using System.Text;

namespace Common.Data
{
    /// <summary>
    /// 实体操作工具类
    /// </summary>
    public static class EntityUtil
    {
        /// <summary>
        /// 把DataReader中数据赋值给一个实体对象
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="o">实体对象</param>
        /// <param name="r">DataReader</param>
        public static void SetEntity<T>(T o, IDataReader r) where T : new()
        {
            if (o != null && r != null)
            {
                EntityUtilCache<T>.EmitInvoker(o, r);
            }
        }

        /// <summary>
        /// 从DataReader中读取数据，返回一个实体集合
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="r">DataReader</param>
        /// <returns>实体集合</returns>
        public static List<T> GetData<T>(IDataReader r) //where T : new()
        {
            List<T> list = new List<T>();

            if (r != null)
            {
                while (r.Read())
                {
                    T o = default(T);
                    o = EntityUtilCache<T>.EmitInvoker(o, r);
                    list.Add(o);
                }
            }

            return list;
        }
        /// <summary>
        /// 从DataTable中读取数据，返回一个实体集合
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="table">DataTable</param>
        /// <returns></returns>
        public static List<T> GetData<T>(DataTable table) where T : new()
        {
            List<T> entities = new List<T>();
            foreach (DataRow row in table.Rows)
            {
                T entity = new T();
                foreach (var item in entity.GetType().GetProperties())
                {
                    try
                    {
                        item.SetValue(entity, Convert.ChangeType(row[item.Name], item.PropertyType), null);

                    }
                    catch { }
                }
                entities.Add(entity);
            }
            return entities;
        }


        /// <summary>
        /// 把source实体对象中的数据按（同名同类型的属性）规则复制到TTarget类型的新实体对象
        /// </summary>
        /// <typeparam name="TTarget">目标实体类型</typeparam>
        /// <typeparam name="TSource">源实体类型</typeparam>
        /// <param name="source">源实体对象</param>
        /// <returns>目标实体对象</returns>
        public static TTarget FastMap<TTarget, TSource>(TSource source)
        {
            if (source != null)
                return FastMapper<TTarget, TSource>.mapReturnMethod(source);
            return default(TTarget);
        }

        /// <summary>
        /// 把source实体对象中的数据按（同名同类型的属性）规则复制到target实体对象
        /// </summary>
        /// <typeparam name="TTarget">目标实体类型</typeparam>
        /// <typeparam name="TSource">源实体类型</typeparam>
        /// <param name="target">目标实体对象</param>
        /// <param name="source">源实体对象</param>
        public static void FastMap<TTarget, TSource>(TTarget target, TSource source)
        {
            if (source != null && target != null)
            {
                FastMapper<TTarget, TSource>.mapMethod(target, source);
            }
        }

        /// <summary>
        /// 获取两个实体的属性间的不同值，返回不同内容描述
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="source">原实体</param>
        /// <param name="target">目的实体</param>
        /// <returns>不同内容描述</returns>
        internal static string GetEntityDifference<T>(T source, T target)
        {
            if (source != null && target != null)
            {
                return EntityUtilCache<T>.GetEntityDifferenceInvoker(source, target);
            }
            return "";
        }

        #region private

        private static readonly MethodInfo GetTypeFromHandleMethod = typeof(Type).GetMethod("GetTypeFromHandle", BindingFlags.Static | BindingFlags.Public);

        private static readonly MethodInfo get_ItemMethod = typeof(IDataRecord).GetMethod("get_Item", new Type[1] { typeof(string) });

        private static readonly FieldInfo DBNullValueField = typeof(DBNull).GetField("Value", BindingFlags.Public | BindingFlags.Static);

        private static readonly ConstructorInfo ExceptionConstructor = typeof(ApplicationException).GetConstructor(new Type[2] { typeof(string), typeof(Exception) });

        private static readonly MethodInfo get_MessageMethod = typeof(Exception).GetMethod("get_Message");

        private static readonly MethodInfo ConcatMethod = typeof(string).GetMethod("Concat", new Type[2] { typeof(string), typeof(string) });

        private static readonly MethodInfo ChangeTypeMethod = typeof(Convert).GetMethod("ChangeType", new Type[2] { typeof(object), typeof(Type) });

        private static readonly MethodInfo AppendFormatMethod = typeof(StringBuilder).GetMethod("AppendFormat", new Type[4] { typeof(string), typeof(object), typeof(object), typeof(object) });

        private static readonly ConstructorInfo StringBuilderConstructor = typeof(StringBuilder).GetConstructor(new Type[0] { });

        private static readonly MethodInfo ToStringMethod = typeof(object).GetMethod("ToString", new Type[0] { });

        private static readonly MethodInfo SecureReaderGetValueMethod = typeof(EntityUtil).GetMethod("SecureReaderGetValue", BindingFlags.Static | BindingFlags.NonPublic);

        #endregion

        #region nested type

        private delegate T SetPropertyValueInvoker<T>(T obj, IDataReader obj1);

        private delegate TTarget MapReturnMethod<TTarget, TSource>(TSource source);

        private delegate void MapMethod<TSource, TTarget>(TSource source, TTarget target);

        private delegate string GetEntityDifferenceMethod<T>(T t1, T t2);

        private class Property
        {
            internal PropertyInfo PropInfo;
            internal string Description;

            internal Property(PropertyInfo propInfo)
            {
                PropInfo = propInfo;
                DescriptionAttribute[] descAttrList = (DescriptionAttribute[])propInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (descAttrList != null && descAttrList.Length > 0)
                {
                    Description = descAttrList[0].Description;
                }
                else
                {
                    Description = propInfo.Name;
                }
            }
        }

        private class PropertyCollection : List<Property>
        {
            internal PropertyCollection(PropertyInfo[] arr)
                : base(arr.Length)
            {
                foreach (PropertyInfo p in arr)
                {
                    NonCompareDifferenceAttribute[] nonCompareAttrList = (NonCompareDifferenceAttribute[])p.
                        GetCustomAttributes(typeof(NonCompareDifferenceAttribute), false);
                    if (nonCompareAttrList != null && nonCompareAttrList.Length > 0)
                        continue;
                    Property property = new Property(p);
                    Add(property);
                }
            }
        }

        private static object SecureReaderGetValue(IDataReader dr, string columnName)
        {
            int count = dr.FieldCount;
            for (int i = 0; i < count; i++)
            {
                if (dr.GetName(i).Equals(columnName, StringComparison.OrdinalIgnoreCase))
                {
                    return dr.GetValue(i);
                }
            }
            return null;
        }

        private static class EntityUtilCache<T>
        {
            private static readonly Type TypeInfo;
            private static readonly PropertyInfo[] PropInfoArr;
            private static readonly PropertyCollection PropList;
            internal static readonly SetPropertyValueInvoker<T> EmitInvoker;
            internal static readonly GetEntityDifferenceMethod<T> GetEntityDifferenceInvoker;

            static EntityUtilCache()
            {
                TypeInfo = typeof(T);
                PropInfoArr = TypeInfo.GetProperties();
                PropList = new PropertyCollection(PropInfoArr);
                EmitInvoker = InternalGetEmitInvoker();
                //TestGetEmitInvoker();
                GetEntityDifferenceInvoker = InternalGetGetEntityDifferenceInvoker();
                //TestGetGetEntityDefferenceInvoker();
                TypeInfo = null;
                PropInfoArr = null;
                PropList = null;
            }

            private static SetPropertyValueInvoker<T> InternalGetEmitInvoker()
            {
                Type type = TypeInfo;

                Type ownerType = type.IsInterface ? typeof(object) : type;
                bool canSkipChecks = SecurityManager.IsGranted(new ReflectionPermission(ReflectionPermissionFlag.MemberAccess));
                DynamicMethod method = new DynamicMethod("SetPropertyValueInvoker", type,
                    new Type[2] { type, typeof(IDataReader) }, ownerType, canSkipChecks);

                ILGenerator il = method.GetILGenerator();

                InternalGeneratorEmitInvoker(il);

                return (SetPropertyValueInvoker<T>)method.CreateDelegate(typeof(SetPropertyValueInvoker<T>));
            }

            /// <summary>
            /// 测试方法
            /// </summary>
            private static void TestGetEmitInvoker()
            {
                Type type = TypeInfo;

                ModuleBuilder m_Module;
                AssemblyBuilder m_Assembly;
                AppDomain domain = AppDomain.CurrentDomain;
                AssemblyName asmName = new AssemblyName("DynamicModule");
                m_Assembly = domain.DefineDynamicAssembly(asmName, AssemblyBuilderAccess.RunAndSave);
                m_Module = m_Assembly.DefineDynamicModule("Module", "DynamicProxy.dll");
                TypeBuilder m_TypeBuilder = m_Module.DefineType(type.Name + "_" + type.GetHashCode().ToString(),
                                            TypeAttributes.Class | TypeAttributes.Public | TypeAttributes.Sealed);
                MethodBuilder method = m_TypeBuilder.DefineMethod("SetPropertyValueInvoker",
                    MethodAttributes.Static | MethodAttributes.Public, TypeInfo, new Type[2] { TypeInfo, typeof(IDataReader) });

                ILGenerator il = method.GetILGenerator();

                InternalGeneratorEmitInvoker(il);

                Type m_Type = m_TypeBuilder.CreateType();
                m_Assembly.Save("DynamicProxy.dll");
            }

            private static void InternalGeneratorEmitInvoker(ILGenerator il)
            {
                Type type = TypeInfo;
                PropertyInfo[] propInfoArr = PropInfoArr;

                il.Emit(OpCodes.Ldarg_0);
                //if (type.IsValueType)
                //    il.Emit(OpCodes.Unbox, type);
                //else
                //    il.Emit(OpCodes.Castclass, type);
                LocalBuilder o = il.DeclareLocal(type);
                il.Emit(OpCodes.Stloc, o);
                Label lbl = il.DefineLabel();
                il.Emit(OpCodes.Ldloc, o);
                il.Emit(OpCodes.Brtrue_S, lbl);
                il.Emit(OpCodes.Newobj, type.GetConstructor(new Type[] { }));
                il.Emit(OpCodes.Stloc, o);
                il.MarkLabel(lbl);
                LocalBuilder val = il.DeclareLocal(typeof(object));

                int n = propInfoArr.Length;
                for (int i = 0; i < n; i++)
                {
                    PropertyInfo propInfo = propInfoArr[i];
                    MethodInfo mi = propInfo.GetSetMethod();
                    if (mi == null) continue;

                    Label lblElse = il.DefineLabel();
                    //il.Emit(OpCodes.Ldnull);
                    //il.Emit(OpCodes.Stloc, val);

                    //Label lblNotExists = il.DefineLabel();
                    il.Emit(OpCodes.Ldarg_1);
                    il.Emit(OpCodes.Ldstr, propInfo.Name);
                    il.Emit(OpCodes.Call, SecureReaderGetValueMethod);
                    //il.Emit(OpCodes.Brfalse_S, lblNotExists);
                    //il.Emit(OpCodes.Ldarg_1);
                    //il.Emit(OpCodes.Ldstr, propInfo.Name);
                    //il.Emit(OpCodes.Callvirt, get_ItemMethod);
                    il.Emit(OpCodes.Stloc, val);
                    //il.MarkLabel(lblNotExists);

                    //Label tryLabel = il.BeginExceptionBlock();
                    //il.Emit(OpCodes.Ldarg_1);
                    //il.Emit(OpCodes.Ldstr, propInfo.Name);
                    //il.Emit(OpCodes.Callvirt, get_ItemMethod);
                    //il.Emit(OpCodes.Stloc, val);
                    //il.BeginCatchBlock(typeof(Exception));
                    //il.Emit(OpCodes.Pop);
                    ////il.Emit(OpCodes.Ldnull);
                    ////il.Emit(OpCodes.Stloc, val);
                    //il.EndExceptionBlock();
                    il.Emit(OpCodes.Ldloc, val);
                    il.Emit(OpCodes.Brfalse_S, lblElse);
                    il.Emit(OpCodes.Ldloc, val);
                    il.Emit(OpCodes.Ldsfld, DBNullValueField);
                    //il.Emit(OpCodes.Ceq);
                    il.Emit(OpCodes.Beq_S, lblElse);
                    Label tryLabel = il.BeginExceptionBlock();
                    il.Emit(OpCodes.Ldloc, o);
                    il.Emit(OpCodes.Ldloc, val);
                    if (propInfo.PropertyType.IsGenericType)
                    {
                        il.Emit(OpCodes.Ldtoken, propInfo.PropertyType.GetGenericArguments()[0]);
                        il.Emit(OpCodes.Call, GetTypeFromHandleMethod);
                        il.Emit(OpCodes.Call, ChangeTypeMethod);
                    }
                    else if (propInfo.PropertyType.IsValueType)
                    {
                        il.Emit(OpCodes.Ldtoken, propInfo.PropertyType);
                        il.Emit(OpCodes.Call, GetTypeFromHandleMethod);
                        il.Emit(OpCodes.Call, ChangeTypeMethod);
                    }
                    if (propInfo.PropertyType.IsValueType)
                        il.Emit(OpCodes.Unbox_Any, propInfo.PropertyType);
                    else
                        il.Emit(OpCodes.Castclass, propInfo.PropertyType);
                    il.EmitCall(OpCodes.Callvirt, mi, null);
                    il.BeginCatchBlock(typeof(Exception));
                    LocalBuilder e = il.DeclareLocal(typeof(Exception));
                    il.Emit(OpCodes.Stloc, e);
                    il.Emit(OpCodes.Ldstr, "[" + propInfo.Name + "]属性赋值出现错误。");
                    il.Emit(OpCodes.Ldloc, e);
                    il.Emit(OpCodes.Callvirt, get_MessageMethod);
                    il.Emit(OpCodes.Call, ConcatMethod);
                    il.Emit(OpCodes.Ldloc, e);
                    il.Emit(OpCodes.Newobj, ExceptionConstructor);
                    il.Emit(OpCodes.Throw);
                    il.EndExceptionBlock();
                    il.MarkLabel(lblElse);
                }

                il.Emit(OpCodes.Ldloc, o);
                il.Emit(OpCodes.Ret);
            }

            private static GetEntityDifferenceMethod<T> InternalGetGetEntityDifferenceInvoker()
            {
                Type type = TypeInfo;

                Type ownerType = type.IsInterface ? typeof(object) : type;
                bool canSkipChecks = SecurityManager.IsGranted(new ReflectionPermission(ReflectionPermissionFlag.MemberAccess));
                DynamicMethod method = new DynamicMethod("GetEntityDifference", typeof(string),
                    new Type[2] { type, type }, ownerType, canSkipChecks);

                ILGenerator il = method.GetILGenerator();

                InternalGeneratorGetEntityDifferenceInvoker(il);

                return (GetEntityDifferenceMethod<T>)method.CreateDelegate(typeof(GetEntityDifferenceMethod<T>));
            }

            private static void TestGetGetEntityDifferenceInvoker()
            {
                Type type = TypeInfo;

                ModuleBuilder m_Module;
                AssemblyBuilder m_Assembly;
                AppDomain domain = AppDomain.CurrentDomain;
                AssemblyName asmName = new AssemblyName("DynamicModule1");
                m_Assembly = domain.DefineDynamicAssembly(asmName, AssemblyBuilderAccess.RunAndSave);
                m_Module = m_Assembly.DefineDynamicModule("Module", "DynamicProxy1.dll");
                TypeBuilder m_TypeBuilder = m_Module.DefineType(type.Name + "_" + type.GetHashCode().ToString(),
                                            TypeAttributes.Class | TypeAttributes.Public | TypeAttributes.Sealed);
                MethodBuilder method = m_TypeBuilder.DefineMethod("GetEntityDifference",
                    MethodAttributes.Static | MethodAttributes.Public, typeof(string), new Type[2] { type, type });

                ILGenerator il = method.GetILGenerator();

                InternalGeneratorGetEntityDifferenceInvoker(il);

                Type m_Type = m_TypeBuilder.CreateType();
                m_Assembly.Save("DynamicProxy1.dll");
            }

            private static void InternalGeneratorGetEntityDifferenceInvoker(ILGenerator il)
            {
                string format = "{0}：“{1}”==>“{2}”|";
                LocalBuilder str = il.DeclareLocal(typeof(StringBuilder));
                il.Emit(OpCodes.Newobj, StringBuilderConstructor);
                il.Emit(OpCodes.Stloc, str);

                Type nullableType = typeof(Nullable<>);

                foreach (Property prop in PropList)
                {
                    PropertyInfo pi = prop.PropInfo;

                    MethodInfo getMethod = pi.GetGetMethod(false);
                    if (getMethod == null) continue;

                    LocalBuilder value1 = il.DeclareLocal(pi.PropertyType);
                    LocalBuilder value2 = il.DeclareLocal(pi.PropertyType);

                    if (pi.PropertyType.IsGenericType && pi.PropertyType.GetGenericTypeDefinition() == nullableType)
                    {
                        Label label1 = il.DefineLabel();
                        Label label2 = il.DefineLabel();
                        Label label3 = il.DefineLabel();
                        Label label4 = il.DefineLabel();
                        Label label5 = il.DefineLabel();

                        MethodInfo getMethodHasValue = pi.PropertyType.GetProperty("HasValue").GetGetMethod();
                        MethodInfo methodGetValueOrDefault = pi.PropertyType.GetMethod("GetValueOrDefault", new Type[] { });

                        il.Emit(OpCodes.Ldarg_0);
                        il.Emit(OpCodes.Callvirt, getMethod);
                        il.Emit(OpCodes.Stloc, value1);
                        il.Emit(OpCodes.Ldarg_1);
                        il.Emit(OpCodes.Callvirt, getMethod);
                        il.Emit(OpCodes.Stloc, value2);
                        il.Emit(OpCodes.Ldloca_S, value1);
                        il.Emit(OpCodes.Call, getMethodHasValue);
                        il.Emit(OpCodes.Ldloca_S, value2);
                        il.Emit(OpCodes.Call, getMethodHasValue);
                        il.Emit(OpCodes.Bne_Un_S, label3);
                        il.Emit(OpCodes.Ldloca_S, value1);
                        il.Emit(OpCodes.Call, getMethodHasValue);
                        il.Emit(OpCodes.Brfalse_S, label1);
                        il.Emit(OpCodes.Ldloca_S, value1);
                        il.Emit(OpCodes.Call, methodGetValueOrDefault);
                        il.Emit(OpCodes.Ldloca_S, value2);
                        il.Emit(OpCodes.Call, methodGetValueOrDefault);
                        Type valueType = pi.PropertyType.GetGenericArguments()[0];
                        MethodInfo method = valueType.GetMethod("op_Inequality");
                        if (method != null)
                        {
                            il.Emit(OpCodes.Call, method);
                            il.Emit(OpCodes.Br_S, label2);
                        }
                        else
                        {
                            il.Emit(OpCodes.Ceq);
                            il.Emit(OpCodes.Brtrue_S, label1);
                        }
                        //il.Emit(OpCodes.Call, methodOperNotEqual);
                        //il.Emit(OpCodes.Br_S, label2);
                        il.MarkLabel(label1);
                        il.Emit(OpCodes.Ldc_I4_0);
                        il.MarkLabel(label2);
                        il.Emit(OpCodes.Br_S, label4);
                        il.MarkLabel(label3);
                        il.Emit(OpCodes.Ldc_I4_1);
                        il.MarkLabel(label4);
                        il.Emit(OpCodes.Ldc_I4_0);
                        il.Emit(OpCodes.Ceq);
                        il.Emit(OpCodes.Brtrue_S, label5);
                        il.Emit(OpCodes.Ldloc, str);
                        il.Emit(OpCodes.Ldstr, format);
                        il.Emit(OpCodes.Ldstr, prop.Description);
                        il.Emit(OpCodes.Ldloc_S, value1);
                        il.Emit(OpCodes.Box, pi.PropertyType);
                        il.Emit(OpCodes.Ldloc_S, value2);
                        il.Emit(OpCodes.Box, pi.PropertyType);
                        il.Emit(OpCodes.Callvirt, AppendFormatMethod);
                        il.Emit(OpCodes.Pop);

                        il.MarkLabel(label5);
                    }
                    else
                    {
                        Label label1 = il.DefineLabel();
                        il.Emit(OpCodes.Ldarg_0);
                        il.Emit(OpCodes.Callvirt, getMethod);
                        il.Emit(OpCodes.Stloc, value1);
                        il.Emit(OpCodes.Ldarg_1);
                        il.Emit(OpCodes.Callvirt, getMethod);
                        il.Emit(OpCodes.Stloc, value2);

                        //Label tryLabel = il.BeginExceptionBlock();

                        //if (pi.PropertyType.IsValueType)
                        //{
                        il.Emit(OpCodes.Ldloc_S, value1);
                        il.Emit(OpCodes.Ldloc_S, value2);
                        //}
                        //else
                        //{
                        //    il.Emit(OpCodes.Ldloca_S, value1);
                        //    il.Emit(OpCodes.Ldloca_S, value2);
                        //}
                        MethodInfo method = pi.PropertyType.GetMethod("op_Inequality");
                        if (method != null)
                        {
                            il.Emit(OpCodes.Call, method);
                            il.Emit(OpCodes.Brfalse_S, label1);
                        }
                        else
                        {
                            il.Emit(OpCodes.Ceq);
                            il.Emit(OpCodes.Brtrue_S, label1);
                        }
                        il.Emit(OpCodes.Ldloc, str);
                        il.Emit(OpCodes.Ldstr, format);
                        il.Emit(OpCodes.Ldstr, prop.Description);
                        il.Emit(OpCodes.Ldloc, value1);
                        if (pi.PropertyType.IsValueType)
                            il.Emit(OpCodes.Box, pi.PropertyType);
                        il.Emit(OpCodes.Ldloc, value2);
                        if (pi.PropertyType.IsValueType)
                            il.Emit(OpCodes.Box, pi.PropertyType);
                        il.Emit(OpCodes.Callvirt, AppendFormatMethod);
                        il.Emit(OpCodes.Pop);

                        //il.BeginCatchBlock(typeof(Exception));
                        //LocalBuilder e = il.DeclareLocal(typeof(Exception));
                        //il.Emit(OpCodes.Stloc, e);
                        //il.Emit(OpCodes.Ldstr, "[" + pi.Name + "]属性比较出现错误。");
                        //il.Emit(OpCodes.Ldloc, e);
                        //il.Emit(OpCodes.Callvirt, get_MessageMethod);
                        //il.Emit(OpCodes.Callvirt, ConcatMethod);
                        //il.Emit(OpCodes.Ldloc, e);
                        //il.Emit(OpCodes.Newobj, ExceptionConstructor);
                        //il.Emit(OpCodes.Throw);
                        //il.EndExceptionBlock();

                        il.MarkLabel(label1);
                    }
                }

                il.Emit(OpCodes.Ldloc, str);
                il.Emit(OpCodes.Callvirt, ToStringMethod);
                il.Emit(OpCodes.Ret);
            }
        }

        private static class FastMapper<TTarget, TSource>
        {
            internal static readonly MapReturnMethod<TTarget, TSource> mapReturnMethod;

            internal static readonly MapMethod<TTarget, TSource> mapMethod;

            static FastMapper()
            {
                mapReturnMethod = CreateMapReturnMethod(typeof(TTarget), typeof(TSource));
                mapMethod = CreateMapMethod(typeof(TTarget), typeof(TSource));
            }

            private static MapReturnMethod<TTarget, TSource> CreateMapReturnMethod(Type targetType, Type sourceType)
            {
                Type ownerType = targetType.IsInterface ? typeof(object) : targetType;
                bool canSkipChecks = SecurityManager.IsGranted(new ReflectionPermission(ReflectionPermissionFlag.MemberAccess));
                DynamicMethod map = new DynamicMethod("MapReturn", targetType, new Type[] { sourceType }, ownerType, canSkipChecks);

                ILGenerator il = map.GetILGenerator();
                ConstructorInfo ci = targetType.GetConstructor(new Type[0]);
                il.DeclareLocal(targetType);
                il.Emit(OpCodes.Newobj, ci);
                il.Emit(OpCodes.Stloc_0);
                PropertyInfo[] sourceProps = sourceType.GetProperties();
                PropertyInfo[] targetProps = targetType.GetProperties();
                foreach (PropertyInfo sourcePropertyInfo in sourceProps)
                {
                    MethodInfo getMethodInfo = sourcePropertyInfo.GetGetMethod();
                    if (getMethodInfo == null) continue;

                    PropertyInfo targetPropertyInfo = Array.Find(targetProps,
                            delegate(PropertyInfo p) { return p.Name == sourcePropertyInfo.Name && p.PropertyType == sourcePropertyInfo.PropertyType; }
                        );
                    if (targetPropertyInfo == null) continue;

                    MethodInfo setMethodInfo = targetPropertyInfo.GetSetMethod();
                    if (setMethodInfo == null) continue;

                    il.Emit(OpCodes.Ldloc_0);
                    il.Emit(OpCodes.Ldarg_0);
                    il.Emit(OpCodes.Callvirt, getMethodInfo);
                    il.Emit(OpCodes.Callvirt, setMethodInfo);
                }
                il.Emit(OpCodes.Ldloc_0);
                il.Emit(OpCodes.Ret);

                return (MapReturnMethod<TTarget, TSource>)map.CreateDelegate(typeof(MapReturnMethod<TTarget, TSource>));
            }

            private static MapMethod<TTarget, TSource> CreateMapMethod(Type targetType, Type sourceType)
            {
                Type ownerType = targetType.IsInterface ? typeof(object) : targetType;
                bool canSkipChecks = SecurityManager.IsGranted(new ReflectionPermission(ReflectionPermissionFlag.MemberAccess));
                DynamicMethod map = new DynamicMethod("Map", null, new Type[] { targetType, sourceType }, ownerType, canSkipChecks);

                ILGenerator il = map.GetILGenerator();
                PropertyInfo[] sourceProps = sourceType.GetProperties();
                PropertyInfo[] targetProps = targetType.GetProperties();
                foreach (PropertyInfo sourcePropertyInfo in sourceProps)
                {
                    MethodInfo getMethodInfo = sourcePropertyInfo.GetGetMethod();
                    if (getMethodInfo == null) continue;

                    PropertyInfo targetPropertyInfo = Array.Find(targetProps,
                            delegate(PropertyInfo p) { return p.Name == sourcePropertyInfo.Name && p.PropertyType == sourcePropertyInfo.PropertyType; }
                        );
                    if (targetPropertyInfo == null) continue;

                    MethodInfo setMethodInfo = targetPropertyInfo.GetSetMethod();
                    if (setMethodInfo == null) continue;

                    il.Emit(OpCodes.Ldarg_0);
                    il.Emit(OpCodes.Ldarg_1);
                    il.Emit(OpCodes.Callvirt, getMethodInfo);
                    il.Emit(OpCodes.Callvirt, setMethodInfo);
                }
                il.Emit(OpCodes.Ret);

                return (MapMethod<TTarget, TSource>)map.CreateDelegate(typeof(MapMethod<TTarget, TSource>));
            }
        }

        #endregion

        /// <summary>
        /// 不参与差异比较的数据实体属性
        /// 
        /// </summary>
        [AttributeUsage(AttributeTargets.Property)]
        public class NonCompareDifferenceAttribute : Attribute
        {

        }
    }

}
