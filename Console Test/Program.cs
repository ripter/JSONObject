using System;
using System.Collections.Generic;
using org.zensoftware;

namespace Console_Test
{
    class Program
    {
        static void Main(string[] args)
        {
            //Create a simple JSON object
            //JSONObject json1 = new JSONObject("{id:1, \"name\": \"Chris Richards\", test:[true, false, null]}");
            //JSONObject json1 = new JSONObject("{id:1, \"name\": \"Chris Richards\"}");
            //JSONObject json1 = new JSONObject("{id:1, \"name\": \"Chris' Pancake house.\", desc:\"They say it's \\\"Danm Good!\\\" I say 'Yup'\"}");
            //JSONObject json1 = new JSONObject("{id:1, \"name\": \"Chris' Pancake house.\", desc: {they: \"They say it's \\\"Danm Good!\\\"\", \"me\": \"Yup\"}}");
            //JSONObject json1 = new JSONObject("{id:1, \"name\": \"Chris' Pancake house.\", desc: [\"Damn Good\", {count: 3}, \"Super Fly\"]}");
            //JSONObject json1 = new JSONObject("{id:1, \"name\": \"Chris Richards\", rank: {\"name\": \"Captan\"}, \"computers\": [\"MacBook Pro\", \"MacBook\", \"iBook\", {count: 3}]}");

            //string txt = "{\"bindings\": [{\"ircEvent\": \"PRIVMSG\", \"method\": \"newURI\", \"regex\": \"^http://.*\"},{\"ircEvent\": \"PRIVMSG\", \"method\": \"deleteURI\", \"regex\": \"^delete.*\"},{\"ircEvent\": \"PRIVMSG\", \"method\": \"randomURI\", \"regex\": \"^random.*\"}]}";
            //JSONObject json1 = JSONObject.parseJSON(txt);

            //Console.WriteLine("JSON String\n" + json1.stringify());

            //Real world JSON example
            string raw = "[{\"id\":50,\"event_id\":8,\"title\":\"Meet Bob!\",\"description\":\"Come see Bob in person! One day only!\",\"cost\":\"Free!\",\"requirements\":\"Must be 21 or over.\",\"slots\":\"3\",\"allDay\":false,\"start\":\"2009-08-20T06:00:00Z\",\"end\":\"2009-08-20T08:00:00Z\"},{\"id\":51,\"event_id\":8,\"title\":\"Meet Bob!\",\"description\":\"Come see Bob in person! One day only!\",\"cost\":\"Free!\",\"requirements\":\"Must be 21 or over.\",\"slots\":\"3\",\"allDay\":false,\"start\":\"2009-08-20T09:00:00Z\",\"end\":\"2009-08-20T10:00:00Z\"},{\"id\":52,\"event_id\":8,\"title\":\"Meet Bob!\",\"description\":\"Come see Bob in person! One day only!\",\"cost\":\"Free!\",\"requirements\":\"Must be 21 or over.\",\"slots\":\"3\",\"allDay\":false,\"start\":\"2009-08-20T12:00:00Z\",\"end\":\"2009-08-20T13:00:00Z\"},{\"id\":53,\"event_id\":8,\"title\":\"Meet Bob!\",\"description\":\"Come see Bob in person! One day only!\",\"cost\":\"Free!\",\"requirements\":\"Must be 21 or over.\",\"slots\":\"3\",\"allDay\":false,\"start\":\"2009-08-20T15:00:00Z\",\"end\":\"2009-08-20T16:00:00Z\"}]";

            List<JSONObject> list = JSONObject.parseJSONObjectArray(raw);

            Console.WriteLine("Found " + list.Count + " items");
            foreach (JSONObject item in list)
            {
                Console.WriteLine("Found Object:");
                Console.WriteLine("\tid: " + item.intForKey("id"));
                Console.WriteLine("\title: " + item.stringForKey("title"));
                Console.WriteLine("\tstart: " + item.stringForKey("start"));
            }

            Console.WriteLine("\n\nPress Anykey to Exit");
            Console.ReadKey();
        }
    }
}
