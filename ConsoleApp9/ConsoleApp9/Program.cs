using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using System.Diagnostics;

async Task JsonAdd(string json, string clickgroup, Tag tom)
{
    //Читаем json
    Rootobject root = new Rootobject();
    using (FileStream fs = new FileStream("json1.json", FileMode.OpenOrCreate))
    {
        root = await System.Text.Json.JsonSerializer.DeserializeAsync<Rootobject>(fs);
    }
    //Добавляем тег
    using (FileStream fs = new FileStream("json1.json", FileMode.Create))
    {
        Group mix = new Group(tom);
        Server server = new Server(new List<Group> { mix });
        int count = 0;
        bool tagexist = false; 
        foreach (var i in root.Server)
        {
            foreach(var j in root.Server[count].Group)
            {
                if (j.Tag.Name == tom.Name) { tagexist = true; Console.WriteLine("Такой тег уже существует"); }
            }
            if (tagexist == false && i.NameGroup == clickgroup) { root.Server[count].Group.Add(mix); break; }
            count++;
        }

        var options = new JsonSerializerOptions { WriteIndented = true };
        await System.Text.Json.JsonSerializer.SerializeAsync<Rootobject>(fs, root, options);
    }
}

async Task JsonDelete(string json, string clickgroup, int indextag)
{
    //Читаем json
    Rootobject root = new Rootobject();
    using (FileStream fs = new FileStream("json1.json", FileMode.OpenOrCreate))
    {
        root = await System.Text.Json.JsonSerializer.DeserializeAsync<Rootobject>(fs);
    }
    //Удаляем тег
    using (FileStream fs = new FileStream("json1.json", FileMode.Create))
    {
        int count = 0;
        foreach (var i in root.Server)
        {
            if (indextag >= root.Server[count].Group.Count) { Console.WriteLine("Такого тега не существует"); break; }
            if (i.NameGroup == clickgroup) { root.Server[count].Group.RemoveAt(indextag); break; }
            count++;
        }

        var options = new JsonSerializerOptions { WriteIndented = true };
        await System.Text.Json.JsonSerializer.SerializeAsync<Rootobject>(fs, root, options);
    }
}

async Task JsonEdit(string json, string clickgroup, int indextag, Tag tom)
{
    //Читаем json
    Rootobject root = new Rootobject();
    using (FileStream fs = new FileStream("json1.json", FileMode.OpenOrCreate))
    {
        root = await System.Text.Json.JsonSerializer.DeserializeAsync<Rootobject>(fs);
    }
    //Удаляем тег и добавляем новый
    using (FileStream fs = new FileStream("json1.json", FileMode.Create))
    {
        Group mix = new Group(tom);
        Server server = new Server(new List<Group> { mix });
        int count = 0;
        bool tagexist = false;
        foreach (var i in root.Server)
        {
            if (indextag >= root.Server[count].Group.Count) { Console.WriteLine("Такого тега не существует"); break; }
            foreach (var j in root.Server[count].Group)
            {
                if (j.Tag.Name == tom.Name) { tagexist = true; Console.WriteLine("Такой тег уже существует"); }
            }
            if (tagexist == false && i.NameGroup == clickgroup) { root.Server[count].Group.RemoveAt(indextag); root.Server[count].Group.Add(mix); break; }
            count++;
        }

        var options = new JsonSerializerOptions { WriteIndented = true };
        await System.Text.Json.JsonSerializer.SerializeAsync<Rootobject>(fs, root, options);
    }
}

//main
string json = "json1.json";
string clickgroup = "Group2";
Tag tom = new Tag("Tag4", "String", false, "W", "abc");

//Проверка на соответсвие типа данных с указанным в json
Type type1 = tom.Value.GetType();
if (type1.Name == tom.Type)
{
    //функции добавления, удаления и редактирования тегов
    await JsonAdd(json, clickgroup, tom);
    //await JsonDelete(json, clickgroup, 2);
    //await JsonEdit(json, clickgroup, 2, tom);
}
else Console.WriteLine("Тип данных не соответсвует значению");

//Типы данных, которые можно использовать
List<string> types = new List<string>() { "Int32", "Int64", "Char", "Boolean", "String" };

public class Rootobject
{
    public bool serverwork { get; set; }
    public List<Server> Server { get; set; }


    public void Add(Server server)
    {
        Server.Add(server);
    }
}

public class Server
{
    public string NameGroup { get; set; }
    public List<Group> Group { get; set; }
    public Server(List<Group> group)
    { 
        Group = group;
    }
    
}

public class Group
{
    public Tag Tag { get; set; }
    public Group(Tag tag)
    {
        Tag = tag;
    }
}

public class Tag
{
    public string Name { get; set; }
    public string Type { get; set; }
    public bool Work { get; set; }
    public string Access { get; set; }
    public dynamic Value { get; set; }
    public Tag()
    {
        Name = "";
        Type = "String";
        Work = true;
        Access = "RW";
        Value = "";
    }
    public Tag(string name, string type, bool work, string access, dynamic value)
    {
        Name = name;
        Type = type;
        Work = work;
        Access = access;
        Value = value;
    }
}
