using Nancy.Json;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Xml.Linq;

string json = "json1.json";
string clickgroup = "Group2";

async Task JsonAdd(string json, string clickgroup)
{
    //Читаем json
    Rootobject root = new Rootobject();
    using (FileStream fs = new FileStream("json1.json", FileMode.OpenOrCreate))
    {
        root = await System.Text.Json.JsonSerializer.DeserializeAsync<Rootobject>(fs);
    }
    //Добавляем теги
    using (FileStream fs = new FileStream("json1.json", FileMode.Create))
    {
        Tag tom = new Tag("Tag4", "int", false, "W", 10);
        Group mix = new Group(tom);
        Server server = new Server(new List<Group> { mix });
        int count = 0;
        foreach (var i in root.Server)
        {
            if (i.NameGroup == clickgroup) { root.Server[count].Group.Add(mix); break; }
            count++;
        }
        server.NameGroup = clickgroup;
        //root.Add(server);

        var options = new JsonSerializerOptions { WriteIndented = true };
        await System.Text.Json.JsonSerializer.SerializeAsync<Rootobject>(fs, root, options);
    }
}


await JsonAdd(json, clickgroup);




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

    public void Add(Tag tag)
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
    public Tag(string name, string type, bool work, string access, dynamic value)
    {
        Name = name;
        Type = type;
        Work = work;
        Access = access;
        Value = value;
    }
}
