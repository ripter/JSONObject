using System;
using System.Collections.Generic;
using org.zensoftware;
using NUnit.Framework;

namespace Test
{
    [TestFixture]
    public class JSONTests
    {

        [Test]
        public void simpleTest()
        {
            //Create a simple JSON object
            JSONObject json1 = new JSONObject("{id:1, \"name\": \"Chris Richards\"}");

            Assert.AreEqual(1, json1["id"], "Getting int with indexer");
            Assert.AreEqual("Chris Richards", json1["name"], "Getting string with indexer");
            Assert.IsNull(json1["rank"], "Keys that don't exist should return null");
            
            Assert.AreEqual("1", json1.stringForKey("id"), "stringForKey should convert type if possible");
            Assert.AreEqual("Chris Richards", json1.stringForKey("name"), "Getting string with stringForKey");
            Assert.AreEqual(string.Empty, json1.stringForKey("rank"), "stringForKey should return empty if the key doesn't exist");

            Assert.AreEqual(1, json1.intForKey("id"), "intForKey should return an int");
            Assert.Throws<FormatException>(delegate() { json1.intForKey("name"); }, "Exception if convert fails.");
            Assert.AreEqual(0, json1.intForKey("rank"), "Zero if key not found");
        }

        [Test]
        public void JSONArray()
        {
            //Real world JSON example
            string raw = "[{\"id\":50,\"event_id\":8,\"title\":\"Meet Bob!\",\"description\":\"Come see Bob in person! One day only!\",\"cost\":\"Free!\",\"requirements\":\"Must be 21 or over.\",\"slots\":\"3\",\"allDay\":false,\"start\":\"2009-08-20T06:00:00Z\",\"end\":\"2009-08-20T08:00:00Z\"},{\"id\":51,\"event_id\":8,\"title\":\"Meet Bob!\",\"description\":\"Come see Bob in person! One day only!\",\"cost\":\"Free!\",\"requirements\":\"Must be 21 or over.\",\"slots\":\"3\",\"allDay\":false,\"start\":\"2009-08-20T09:00:00Z\",\"end\":\"2009-08-20T10:00:00Z\"},{\"id\":52,\"event_id\":8,\"title\":\"Meet Bob!\",\"description\":\"Come see Bob in person! One day only!\",\"cost\":\"Free!\",\"requirements\":\"Must be 21 or over.\",\"slots\":\"3\",\"allDay\":false,\"start\":\"2009-08-20T12:00:00Z\",\"end\":\"2009-08-20T13:00:00Z\"},{\"id\":53,\"event_id\":8,\"title\":\"Meet Bob!\",\"description\":\"Come see Bob in person! One day only!\",\"cost\":\"Free!\",\"requirements\":\"Must be 21 or over.\",\"slots\":\"3\",\"allDay\":false,\"start\":\"2009-08-20T15:00:00Z\",\"end\":\"2009-08-20T16:00:00Z\"}]";

            List<JSONObject> list = JSONObject.parseJSONObjectArray(raw);

            //Check the Count
            Assert.AreEqual(4, list.Count);

            //Check a few random properties
            Assert.AreEqual(50, list[0].intForKey("id"));
            Assert.AreEqual("Come see Bob in person! One day only!", list[1].stringForKey("description"));
            

        }

        [Test]
        public void testQotedName()
        {
            //Create a simple JSON object
            JSONObject json1 = new JSONObject("{\"id\":1, \"name\": \"Chris Richards\"}");

            Assert.AreEqual(1, json1["id"], "Getting int with indexer");
            Assert.AreEqual("Chris Richards", json1["name"], "Getting string with indexer");
            Assert.IsNull(json1["rank"], "Keys that don't exist should return null");

            Assert.AreEqual("1", json1.stringForKey("id"), "stringForKey should convert type if possible");
            Assert.AreEqual("Chris Richards", json1.stringForKey("name"), "Getting string with stringForKey");
            Assert.AreEqual(string.Empty, json1.stringForKey("rank"), "stringForKey should return empty if the key doesn't exist");

            Assert.AreEqual(1, json1.intForKey("id"), "intForKey should return an int");
            Assert.Throws<FormatException>(delegate() { json1.intForKey("name"); }, "Exception if convert fails.");
            Assert.AreEqual(0, json1.intForKey("rank"), "Zero if key not found");
        }

        [Test]
        public void typeTest()
        {
            //Create a JSON object with all the types
            JSONObject json1 = new JSONObject("{id:1, \"name\": \"Chris Richards\", isTrue:true, isFalse:false, list:[true, false, null], object:{gum:\"Trident\", type:\"Spearmint\"}}");

            Assert.AreEqual(1, json1.intForKey("id"));
            Assert.AreEqual("Chris Richards", json1.stringForKey("name"));
            Assert.IsTrue(json1.boolForKey("isTrue"));
            Assert.IsFalse(json1.boolForKey("isFalse"));
            Assert.IsInstanceOf(typeof(System.Collections.Generic.List<object>), json1.listForKey("list"));
            Assert.IsInstanceOf<JSONObject>(json1.objectForKey("object"));

            System.Collections.Generic.List<object> list = json1.listForKey("list");
            Assert.AreEqual(3, list.Count);
            Assert.AreEqual(true, list[0]);
            Assert.AreEqual(false, list[1]);
            Assert.AreEqual(null, list[2]);

            JSONObject obj = json1.objectForKey("object");
            Assert.AreEqual("Trident", obj.stringForKey("gum"));
            Assert.AreEqual("Spearmint", obj.stringForKey("type"));
        }

        [Test]
        public void mergeTest()
        {
            //Create a JSON strings
            string sJSON1 = "{id:1, \"name\": \"Chris Richards\"}";
            string sJSON2 = "{rank: [\"Caption\", \"Major\"]}";
            string sJSON3 = "{id:4}";

            //Create our base object
            JSONObject json = JSONObject.parseJSON(sJSON1);
            Assert.AreEqual(1, json.intForKey("id"));
            
            //Verify no Rank
            Assert.IsNull(json["rank"]);
            Assert.AreEqual(0, json.listForKey("rank").Count);

            //Merge Rank
            json.merge(sJSON2);
            //Verify we have rank
            System.Collections.Generic.List<object> rank = json.listForKey("rank");
            Assert.AreEqual(2, rank.Count);
            Assert.AreEqual("Caption", rank[0]);
            Assert.AreEqual("Major", rank[1]);

            //Merge Override
            json.merge(sJSON3);
            Assert.AreEqual(4, json.intForKey("id"));
        }
    }
}
