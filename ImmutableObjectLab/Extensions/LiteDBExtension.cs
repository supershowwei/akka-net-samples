using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using LiteDB;

namespace ImmutableObjectLab.Extensions
{
    public static class LiteDBExtension
    {
        private static readonly Type BsonIdAttr = typeof(BsonIdAttribute);

        private static readonly MethodInfo Deserialize = typeof(BsonMapper)
            .GetMethods(BindingFlags.NonPublic | BindingFlags.Instance)
            .Single(x => x.Name.Equals("Deserialize") && !x.IsGenericMethod);

        public static T ToImmutability<T>(this BsonDocument me)
        {
            var result = Activator.CreateInstance<T>();

            var idProp = GetIdProperty<T>();
            var id = Deserialize.Invoke(BsonMapper.Global, new object[] { idProp.PropertyType, me["_id"] });

            SetBackingFieldValue<T>(idProp.Name, id, result);

            foreach (var prop in typeof(T).GetProperties())
            {
                if (prop.Name.Equals(idProp.Name)) continue;

                if (me.Keys.Contains(prop.Name))
                {
                    object value;
                    
                    if (me[prop.Name].IsArray && !prop.PropertyType.IsArray)
                    {
                        value = DeserializeList(prop.PropertyType, me[prop.Name].AsArray);
                    }
                    else if (false) // is dictionary
                    {
                    }
                    else
                    {
                        value = Deserialize.Invoke(
                            BsonMapper.Global,
                            new object[] { prop.PropertyType, me[prop.Name] });
                    }
                    

                    SetBackingFieldValue<T>(prop.Name, value, result);
                }
            }

            return result;
        }

        public static BsonDocument ToBsonDocument<T>(this T me)
        {
            return BsonMapper.Global.ToDocument(me);
        }

        // test
        private static PropertyInfo GetIdProperty<T>()
        {
            var prop = typeof(T).GetProperties()
                .SingleOrDefault(p => p.CustomAttributes.Any(a => a.AttributeType == BsonIdAttr));

            return prop == null ? typeof(T).GetProperty("Id") : prop;
        }

        private static void SetBackingFieldValue<T>(string name, object value, object obj)
        {
            // cache

            // double check locking

            var backingField = typeof(T).GetFields(BindingFlags.Instance | BindingFlags.NonPublic)
                .Single(f => f.Name.Contains($"<{name}>k__BackingField"));

            backingField.SetValue(obj, value);
        }

        private static object DeserializeList(Type type, BsonArray value)
        {
            var itemType = type.GetTypeInfo().GenericTypeArguments[0];
            var list = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(itemType));

            foreach (var item in value)
            {
                list.Add(Deserialize.Invoke(BsonMapper.Global, new object[] { itemType, item }));
            }

            return list;
        }
    }
}