# EFReflectionTableCopy
A C# Console app to use Refflection to copy an SQL Server Db table to another in the same Db where both have exact same properties except target has additional Id identity property. First table class generated by Entity Framework Data First Scaffolding (table was generated by importing CSV data in SSMS). Second table created by copy the first table class in the target app then doing a Code-First generation of it.


[See blog discussion](https://davidjones.sportronics.com.au/blazor/Blazor_Helpers_Server_App-Reverse_Reverse_Engineering_with_Entity_Framework-blazor.html)


The idea here is to use a database table that has been generated by importing a CSV file in a Blazor Server app that uses Entity Framework Core in a Code-First manner which means tha the database tables for the app are generated form the class files in the Blazor app. Consensus indicates that an app can only be Code-First or Data-First, but not both. Data-First means the tables are generated first and Entity Framework is used to generate the class files in the app. The way around this here _(look for a spoiler alert later)_ is to create a Console app that generates the class files from the existing populated table in the database using a Scaffolding command. The class file for the source table is then copied into the Blazor app with a slightly different name and a corresponding DBSet for it is added as well. The Scaffolding command is then run again and Reflection is then used _(see Program.cs)_ is used to copy the data from the original table to new one. The data is then available in the Blazor app. **THAT'S ALL A BIT OF A MOUTHFUL.** But it does work! 


The Scafolding command is

```
scaffold-dbcontext -provider Microsoft.EntityFrameworkCore.SqlServer -connection "THE CONNECTION STRING” -OutputDir Models
```

Where "THE CONNECTION STRING” is the SQL Server database connection string and Models is the Console app directory to which the generated class files are added. Note that there is a DBContext class file with the classes DBSet also created in tha folder.

Whilst there have are many other tables in the database, their class files and DBSets ahve been removed for simplicity.

- Wmarecords is the original table into which the CSV data awas imported using SSMS, **_The source_**.
- WorldMasterRecords is the table of data used in the Blazor app, **_The target_**.

## Spoiler Alert

See how this is done in [djaus2/ScanAVResults](https://github.com/djaus2/ScanAVResults)

> The project is there but haven't explain the simpler way yet .. coming.


