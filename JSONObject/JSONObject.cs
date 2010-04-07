using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;


namespace org.zensoftware
{
    public class JSONObject
    {
        protected Dictionary<string, object> _properties;

        /// <summary>
        /// Default Constructor
        /// </summary>
        public JSONObject()
        {
            _properties = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new JSON object from the Dictionary
        /// </summary>
        /// <param name="list_pair"></param>
        public JSONObject(Dictionary<string, object> list_pair)
        {
            _properties = list_pair;
        }
        /// <summary>
        /// Creates a new JSON object from the json text
        /// </summary>
        /// <param name="json_text"></param>
        public JSONObject(string json_text)
        {
            _properties = new Dictionary<string, object>();

            //Make sure the string is formatted correctly
            if (json_text.StartsWith("{") == false || json_text.EndsWith("}") == false) { throw new FormatException("JSON objects must start with { and end with }"); }

            int pos = 0;
            Dictionary<string, object> list = readObject(ref pos, ref json_text);
            add(list);
        }
        
        /// <summary>
        /// This will add all of the properties to our own properties, overriding any existing ones.
        /// </summary>
        /// <param name="properties"></param>
        public void add(Dictionary<string, object> properties)
        {
            //Add them all
            foreach (string key in properties.Keys)
            {
                if (_properties.ContainsKey(key))
                {
                    _properties[key] = properties[key];
                }
                else
                {
                    _properties.Add(key, properties[key]);
                }
            }
        }
        /// <summary>
        /// Merge the JSON data with the objects, overriding any existing values.
        /// </summary>
        /// <param name="json_text"></param>
        public void merge(string json_text)
        {
            //Check the formatting.
            if (json_text.StartsWith("{") == false || json_text.EndsWith("}") == false) { throw new FormatException("JSON objects must start with { and end with }"); }

            //Load it up
            int pos = 0;
            Dictionary<string, object> list = readObject(ref pos, ref json_text);
            add(list);
        }
        /// <summary>
        /// Merge the JSON data with this object, overriding any existing values;
        /// </summary>
        /// <param name="json"></param>
        public void merge(JSONObject json)
        {
            add(json._properties);
        }

        /// <summary>
        /// Gets/Sets the value for the key, or null if it doesn't exist.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public object this[string key]
        {
            get
            {
                if (_properties.ContainsKey(key))
                {
                    return _properties[key];
                }
                return null;
            }
            set
            {
                _properties[key] = value;
            }
        }
        /// <summary>
        /// Returns a string value for the key, or string.Empty if it doens't exist
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string stringForKey(string key)
        {
            if (_properties.ContainsKey(key))
            {
                //Is it already the type we want?
                if (_properties[key].GetType() == typeof(string))
                {
                    return (string)_properties[key];
                }
                //Try to convert it
                return Convert.ToString(_properties[key]);
            }
            return string.Empty;
        }
        /// <summary>
        /// Returns an int value for the key, or 0 if the key doens't exist.
        /// If the key exists and can not be converted, it will throw a FormatException error.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public int intForKey(string key)
        {
            if (_properties.ContainsKey(key))
            {
                //Is it already the type we want?
                if (_properties[key].GetType() == typeof(int))
                {
                    return (int)_properties[key];
                }
                //Try to convert it
                return Convert.ToInt32(_properties[key]);
            }
            return 0;
        }
        /// <summary>
        /// Returns an bool value for the key, or false if the key doens't exist.
        /// If the key exists and can not be converted, it will throw a FormatException error.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool boolForKey(string key)
        {
            if (_properties.ContainsKey(key))
            {
                //Is it already the type we want?
                if (_properties[key].GetType() == typeof(bool))
                {
                    return (bool)_properties[key];
                }
                //Try to convert it
                return Convert.ToBoolean(_properties[key]);
            }
            return false;
        }
        /// <summary>
        /// Returns an JSONObject value for the key, or null if the key doens't exist.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public JSONObject objectForKey(string key)
        {
            if (_properties.ContainsKey(key))
            {
                //Is it already the type we want?
                if (_properties[key].GetType() == typeof(JSONObject))
                {
                    return (JSONObject)_properties[key];
                }
            }
            return null;
        }
        /// <summary>
        /// Returns an List&lt;object&gt; value for the key, or and empty list if the key doens't exist.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public List<object> listForKey(string key)
        {
            if (_properties.ContainsKey(key))
            {
                //Is it already the type we want?
                if (_properties[key].GetType() == typeof(List<object>))
                {
                    return (List<object>)_properties[key];
                }
            }
            return new List<object>();
        }


        /// <summary>
        /// Creates a new JSON object from the JSON text.
        /// Same as "new JSONObject(json_text)"
        /// </summary>
        /// <param name="json_text"></param>
        /// <returns></returns>
        static public JSONObject parseJSON(string json_text)
        {
            return new JSONObject(json_text);
        }

        /// <summary>
        /// Turns the Object back into a JSON string.
        /// </summary>
        /// <returns></returns>
        public string stringify()
        {
            //Thankfully, writing back to the JSON string is easer than reading it.
            StringBuilder sb = new StringBuilder();
            sb.Append("{");

            //read all the properties
            foreach (string key in _properties.Keys)
            {
                sb.Append(key);
                sb.Append(":");
                sb.Append(stringifyValue(_properties[key]));
                
                sb.Append(",");
            }
            //Remove the last ,
            sb.Remove(sb.Length - 1, 1);

            sb.Append("}");
            return sb.ToString();
        }
        /// <summary>
        /// Returns a JSON string for the value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected string stringifyValue(object value)
        {
            if (null == value) { return "null"; }

            Type value_type = value.GetType();
            if (typeof(string) == value_type)
            {
                return "\"" + value + "\"";
            }
            else if (typeof(int) == value_type)
            {
                return value.ToString();
            }
            else if (value_type == typeof(bool))
            {
                if ((bool)value == true)
                {
                    return "true";
                }
                else
                {
                    return "false";
                }
            }
            else if (value_type == typeof(JSONObject))
            {
                return ((JSONObject)value).stringify();
            }
            else if (value_type == typeof(List<object>))
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("[");
                for (int i = 0; i < ((List<object>)value).Count; i++)
                {
                    sb.Append(stringifyValue(((List<object>)value)[i]));
                    sb.Append(",");
                }
                //Remove the last ,
                sb.Remove(sb.Length - 1, 1);
                sb.Append("]");
                return sb.ToString();
            }

            //Didn't find it
            return "null";
        }
        /// <summary>
        /// Returns the Object as a JSON string.
        /// Same as stringify()
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.stringify();
        }

        /// <summary>
        /// Reads and returns a JSON formatted string from the source.
        /// </summary>
        /// <param name="pos">Position to start reading.</param>
        /// <param name="json">json string to read from.</param>
        /// <returns></returns>
        protected string readString(ref int pos, ref string json)
        {
            StringBuilder sb = new StringBuilder();

            //Ending Conditions:
            // We have two "
            // We don't have any " but we reached , : ] or }

            int quotes = 0; //If this reaches two, we are finished
            while (quotes < 2)
            {
                //Whatever it is, add it
                sb.Append(json[pos]);
                
                //Is it a quote? an unescaped quote?
                if ('"' == json[pos] && '\\' != json[pos - 1])
                {
                    quotes++;
                }

                //Goto the next charactor
                pos++;

                //Is it going to be an ending char?
                if (quotes != 1 && (',' == json[pos] || ':' == json[pos] || ']' == json[pos] || '}' == json[pos]))
                {
                    //Ending charactor not in an open quote means we are done
                    quotes = 2;
                }
            }

            return sb.ToString().Trim("\" ".ToCharArray());
        }
        /// <summary>
        /// Reads and returns a JSON formatted int from the source.
        /// </summary>
        /// <param name="pos">Position to start reading.</param>
        /// <param name="json">json string to read from.</param>
        /// <returns></returns>
        protected int readInt(ref int pos, ref string json)
        {
            StringBuilder sb = new StringBuilder();

            //ints ends on , } or ]
            while (',' != json[pos] && '}' != json[pos] && ']' != json[pos])
            {
                sb.Append(json[pos]);
                pos++;
            }

            //Convert and return
            return Convert.ToInt32(sb.ToString());
        }
        /// <summary>
        /// Reads and returns a JSON value (string, int, object, array) from the source.
        /// </summary>
        /// <param name="pos">Position to start reading.</param>
        /// <param name="json">json string to read from.</param>
        /// <returns></returns>
        protected object readValue(ref int pos, ref string json)
        {
            //Skip to a valid type
            while (' ' == json[pos]) { pos++; }

            //Is it a String?
            if ('"' == json[pos])
            {
                return readString(ref pos, ref json);
            }
            //Is it an Object?
            else if ('{' == json[pos])
            {
                Dictionary<string, object> list_pair = readObject(ref pos, ref json);
                return new JSONObject(list_pair);
            }
            //Is it an Array?
            else if ('[' == json[pos])
            {
                return readArray(ref pos, ref json);
            }
            //Is it True?
            else if ('t' == json[pos] && 'e' == json[pos+3])
            {
                pos = pos + 4;
                return true;
            }
            //Is it False?
            else if ('f' == json[pos] && 'e' == json[pos + 4])
            {
                pos = pos + 5;
                return false;
            }
            //Is it null?
            else if ('n' == json[pos] && 'l' == json[pos + 3])
            {
                pos = pos + 4;
                return null;
            }
            else
            {
                return readInt(ref pos, ref json);
            }
        }
        /// <summary>
        /// Reads and returns a JSON formatted object from the source.
        /// </summary>
        /// <param name="pos">Position to start reading.</param>
        /// <param name="json">json string to read from.</param>
        /// <returns></returns>
        protected Dictionary<string, object> readObject(ref int pos, ref string json)
        {
            //Create a new pair list
            Dictionary<string, object> pair_list = new Dictionary<string, object>();

            //Objects only end on }
            while ('}' != json[pos])
            {
                //Consume { ,
                if ('{' == json[pos] || ',' == json[pos]) { pos++; }

                //Objects are just a string and value pair
                //Get the string
                string name = readString(ref pos, ref json);
                name = name.Trim("\" ".ToCharArray()); //Names don't need quotes

                //Names can't start with { [
                if ('{' == name[0] || '[' == name[0]) {throw new FormatException("Object property name is invalid. '" + name + "'"); }

                //Consume the :
                pos++;

                //Get the Value
                object value = readValue(ref pos, ref json);

                //Add the Property to the json object
                pair_list.Add(name, value);
            }
            //Objects should consume the ending }
            pos++;
            return pair_list;
        }
        /// <summary>
        /// Reads and returns a JSON formatted array from the source.
        /// </summary>
        /// <param name="pos">Position to start reading.</param>
        /// <param name="json">json string to read from.</param>
        /// <returns></returns>
        protected List<object> readArray(ref int pos, ref string json)
        {
            List<object> array = new List<object>();
            //Arrays only end on ]
            while (']' != json[pos])
            {
                //Consume [ ,
                if ('[' == json[pos] || ',' == json[pos]) { pos++; }

                //Arrays are just a list of values
                object value = readValue(ref pos, ref json);

                array.Add(value);
                Console.WriteLine("Array Item:\t" + value);
            }
            //Arrays should consume the ending ]
            pos++;

            return array;
        }

    }
}
