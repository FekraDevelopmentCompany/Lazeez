using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

public class Mapper
{
    private static Mapper _instance;
    public Dictionary<object, object> MappingTypes { get; set; }

    public static Mapper Instance
    {
        get { return _instance ?? (_instance = new Mapper { MappingTypes = new Dictionary<object, object>() }); }
    }

    public void CreateMap<TSource, TDestination>()
        where TSource : new()
        where TDestination : new()
    {
        if (!MappingTypes.ContainsKey(typeof(TSource)))
        {
            MappingTypes.Add(typeof(TSource), typeof(TDestination));
        }
    }

    public TDestination Map<TSource, TDestination>(TSource realObject, TDestination dtoObject = default(TDestination), Dictionary<object, object> alreadyInitializedObjects = null, bool shouldMapInnerEntities = true)
        where TSource : class, new()
        where TDestination : class, new()
    {
        if (realObject == null)
        {
            return null;
        }
        if (alreadyInitializedObjects == null)
        {
            alreadyInitializedObjects = new Dictionary<object, object>();
        }
        if (dtoObject == null)
        {
            dtoObject = new TDestination();
        }

        var realObjectType = realObject.GetType();
        PropertyInfo[] properties = realObjectType.GetProperties();
        foreach (PropertyInfo currentRealProperty in properties)
        {
            PropertyInfo currentDtoProperty = dtoObject.GetType().GetProperty(currentRealProperty.Name);
            if (currentDtoProperty == null)
            {
                ////Debug.WriteLine("The property {0} was not found in the DTO object in order to be mapped. Because of that we skip to map it.", currentRealProperty.Name);
            }
            else
            {
                if (MappingTypes.ContainsKey(currentRealProperty.PropertyType) && shouldMapInnerEntities)
                {
                    object mapToObject = this.MappingTypes[currentRealProperty.PropertyType];
                    var types = new Type[] { currentRealProperty.PropertyType, (Type)mapToObject };
                    MethodInfo method = GetType().GetMethod("Map").MakeGenericMethod(types);
                    var realObjectPropertyValue = currentRealProperty.GetValue(realObject, null);
                    var objects = new[]
                    {
                            realObjectPropertyValue,
                            null,
                            alreadyInitializedObjects,
                            shouldMapInnerEntities
                        };
                    if (objects != null && realObjectPropertyValue != null)
                    {
                        if (alreadyInitializedObjects.ContainsKey(realObjectPropertyValue) && currentDtoProperty.CanWrite)
                        {
                            // Set the cached version of the same object (optimization)
                            currentDtoProperty.SetValue(dtoObject, alreadyInitializedObjects[realObjectPropertyValue]);
                        }
                        else
                        {
                            // Add the object to cached objects collection.
                            alreadyInitializedObjects.Add(realObjectPropertyValue, null);
                            // Recursively call Map method again to get the new proxy object.
                            var newProxyProperty = method.Invoke(this, objects);
                            if (currentDtoProperty.CanWrite)
                            {
                                currentDtoProperty.SetValue(dtoObject, newProxyProperty);
                            }

                            if (alreadyInitializedObjects.ContainsKey(realObjectPropertyValue) && alreadyInitializedObjects[realObjectPropertyValue] == null)
                            {
                                alreadyInitializedObjects[realObjectPropertyValue] = newProxyProperty;
                            }
                        }
                    }
                    else if (realObjectPropertyValue == null && currentDtoProperty.CanWrite)
                    {
                        // If the original value of the object was null set null to the destination property.
                        currentDtoProperty.SetValue(dtoObject, null);
                    }
                }
                else if (!MappingTypes.ContainsKey(currentRealProperty.PropertyType))
                {
                    // If the property is not custom type just set normally the value.
                    if (currentDtoProperty.CanWrite)
                    {
                        currentDtoProperty.SetValue(dtoObject, currentRealProperty.GetValue(realObject, null));
                    }
                }
            }
        }

        return dtoObject;
    }

    public List<TDestination> MapList<TSource, TDestination>(List<TSource> realObjects, Dictionary<object, object> alreadyInitializedObjects = null)
        where TSource : class, new()
        where TDestination : class, new()
    {
        return realObjects.Select(currentRealObject => Map<TSource, TDestination>(currentRealObject, alreadyInitializedObjects: alreadyInitializedObjects)).ToList();
    }
}