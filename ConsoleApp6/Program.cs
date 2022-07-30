using System.Data;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using EFReflectionTableCopy.WorldMastersRecords;

// .NET 6 Console App
Console.WriteLine("EF Table Copy using Reflection!");



// Copy one table to another in a generic manner (using Reflection) 
// context.Wmas => context.WorldMastersAthleticsRecords
// Where both EF classes for the table types have exact same properties
// And each has a separate DBContext.
using (var context = new HelperLog200Context())
{
    var recs = await context.WorldMastersRecords.ToListAsync(); ;
    context.WorldMastersRecords.RemoveRange(recs);
    await context.SaveChangesAsync();

    var reks = await context.Wmarecords.ToListAsync();
    PropertyInfo[] Props = typeof(WorldMastersRecord).GetProperties(BindingFlags.Public | BindingFlags.Instance);
    string[] propertyNames = reks[0].GetType().GetProperties().Select(p => p.Name).ToArray();
    foreach (var rek in reks)
    {
        // Create a new instance of the target
        WorldMastersRecord rec = new WorldMastersRecord();
        // Iterate through the source properties
        foreach (var prop in propertyNames)
        {
            // Get the source property value
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            object? propValue = rek.GetType().GetProperty(prop).GetValue(rek, null);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            if (propValue != null)
            {
                // Find the corresponding property in the target and set it
                foreach (PropertyInfo property in Props)
                {
                    if (property.Name != prop)
                        continue;
                    if ((property.Name != "Id") &&
                            (property.Name != "IdNew"))
                    {
                        if (property.CanWrite)
                        {
                            try
                            {
                                // All properties in both are string except Id
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                                if (property.PropertyType.Name == "String")
                                {
                                    string val = (string)propValue;
                                    property.SetValue(rec, val);
                                }
                                else if (property.PropertyType.FullName.Contains("System.Int16"))
                                {
                                    short val = (short)propValue;
                                    if (val != 0)
                                        property.SetValue(rec, val);
                                    else
                                        property.SetValue(rec, null);
                                }
                                else if (property.PropertyType.FullName.Contains("System.Int32"))
                                {
                                    int val = (int)propValue;
                                    if(val != 0)
                                        property.SetValue(rec, val);
                                    else
                                        property.SetValue(rec, null);
                                }
                                else if (property.PropertyType.FullName.Contains("System.Int64"))
                                {
                                    Int64 val = (Int64)propValue;
                                    if (val != 0)
                                        property.SetValue(rec, val);
                                    else
                                        property.SetValue(rec, null);
                                }
                                else if (property.PropertyType.FullName.Contains("System.Single"))
                                {
                                    float val = (float)propValue;
                                    if (val != 0)
                                        property.SetValue(rec, val);
                                    else
                                        property.SetValue(rec, null);
                                }
                                else if (property.PropertyType.FullName.Contains("System.Double"))
                                {
                                    double val = (double)propValue;
                                    if (val != 0)
                                        property.SetValue(rec, val);
                                    else
                                        property.SetValue(rec, null);
                                }
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                                // Can add other opetions here.
                            }
                            catch (Exception ex)
                            {
                                //CanWrite should take care of this.
                                if (ex.Message == "Property set method not found.")
                                    continue;
                            }
                        }
                    }
                }
            }
        }
        Console.WriteLine("*"); 
        await context.WorldMastersRecords.AddAsync(rec);
        await context.SaveChangesAsync();
    }
}



