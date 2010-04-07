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

            string txt = "{\"bindings\": [{\"ircEvent\": \"PRIVMSG\", \"method\": \"newURI\", \"regex\": \"^http://.*\"},{\"ircEvent\": \"PRIVMSG\", \"method\": \"deleteURI\", \"regex\": \"^delete.*\"},{\"ircEvent\": \"PRIVMSG\", \"method\": \"randomURI\", \"regex\": \"^random.*\"}]}";
            JSONObject json1 = JSONObject.parseJSON(txt);

            Console.WriteLine("JSON String\n" + json1.stringify());

            Console.WriteLine("\n\nPress Anykey to Exit");
            Console.ReadKey();
        }
    }
}
