using System;
using System.Reflection;

namespace TowerDefense.Util
{
    /// <summary>
    /// Copy values from one object to another
    /// </summary>
    public class ReflectionUtils
    {
        //Considering B fields, try to find same fields in A, and Copy A to B
        public static void CopyFromA<TA, TB>(TA objA, TB objB)
        {
            Type aType = objA.GetType();
            Type bType = objB.GetType();
            foreach (FieldInfo fieldInfo in bType.GetFields())
            {
                FieldInfo field = aType.GetField(fieldInfo.Name);
                if (field != null)
                {
                    fieldInfo.SetValue(objB, field.GetValue(objA));
                }
            }
        }
        
        //Considering B fields, try to find same fields in A, but Copy B to A
        public static void CopyFromB<TA, TB>(TA objA, TB objB)
        {
            Type aType = objA.GetType();
            Type bType = objB.GetType();
            foreach (FieldInfo fieldInfo in bType.GetFields())
            {
                FieldInfo field = aType.GetField(fieldInfo.Name);
                if (field != null)
                {
                    fieldInfo.SetValue(objA, field.GetValue(objB));
                }
            }
        }
    }
}